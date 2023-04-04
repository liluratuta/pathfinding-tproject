using TestProject.GameField;
using TestProject.StaticData;
using TestProject.Utility;
using UnityEngine;

namespace TestProject.View
{
    public class PathPointsSelector : MonoBehaviour
    {
        public GameObject StartPoint;
        public GameObject EndPoint;
        public PathView PathView;

        private LayerMask _blockLayer;
        private Camera _camera;
        private IPositionConverter _positionConverter;
        private IField _field;

        public void Init(IField field, IPositionConverter positionConverter, FieldStaticData fieldData)
        {
            _field = field;
            _positionConverter = positionConverter;
            _blockLayer = fieldData.BlockLayer;
        }
        
        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)) 
                return;

            if (!IsStartSelected())
            {
                SelectStart();
                return;
            }

            if (!IsEndSelected())
            {
                SelectEnd();
                BuildPath();
                return;
            }

            HidePath();
            UnselectPoints();
            SelectStart();
        }

        private bool IsStartSelected() => 
            StartPoint.activeSelf;

        private bool IsEndSelected() => 
            EndPoint.activeSelf;

        private void SelectStart()
        {
            if (!TryCastBlock(out var blockPosition))
                return;

            var topPosition = GetBlockTopPosition(blockPosition);
            StartPoint.transform.position = topPosition;
            StartPoint.SetActive(true);
        }

        private void SelectEnd()
        {
            if (!TryCastBlock(out var blockPosition))
                return;

            var topPosition = GetBlockTopPosition(blockPosition);

            if (topPosition == StartPoint.transform.position)
                return;

            EndPoint.transform.position = topPosition;
            EndPoint.SetActive(true);
        }

        private void UnselectPoints()
        {
            StartPoint.SetActive(false);
            EndPoint.SetActive(false);
        }

        private void HidePath() => 
            PathView.Hide();

        private void BuildPath()
        {
            if (!IsStartSelected() || !IsEndSelected())
                return;
            
            PathView.Show(
                _positionConverter.ConvertToField(StartPoint.transform.position, useOffset: true), 
                _positionConverter.ConvertToField(EndPoint.transform.position, useOffset: true));
        }

        private bool TryCastBlock(out Vector3 blockPosition)
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitInfo, 100f, _blockLayer))
            {
                blockPosition = hitInfo.transform.position; 
                return true;
            }

            blockPosition = default;
            return false;
        }

        private Vector3 GetBlockTopPosition(Vector3 blockPosition)
        {
            var fieldPosition = _positionConverter.ConvertToField(blockPosition, useOffset: true);
            var top = _field.GetTop(fieldPosition);
            return _positionConverter.ConvertToWorld(top, useOffset: true);
        }
    }
}