using TestProject.GameField;
using TestProject.Services.Assets;
using TestProject.Utility;
using UnityEngine;

namespace TestProject.View
{
    public class GameFieldView : MonoBehaviour
    {
        private IField _field;
        private IAssetProvider _assets;
        private IPositionConverter _positionConverter;

        public void Init(IField field, IAssetProvider assets, IPositionConverter positionConverter)
        {
            _field = field;
            _assets = assets;
            _positionConverter = positionConverter;
        }

        private void Start()
        {
            ShowFloor();
            ShowBlocks();
        }

        private void ShowFloor()
        {
            var floorPrefab = _assets.Get(AssetPath.Floor);

            for (var y = 0; y < _field.Size.y; y++)
            {
                for (var x = 0; x < _field.Size.x; x++)
                {
                    var floor = Instantiate(floorPrefab, transform);
                    floor.transform.position = _positionConverter.ConvertToWorld(new Vector2(x, y), useOffset: true);
                }
            }
        }

        private void ShowBlocks()
        {
            var blockPrefab = _assets.Get(AssetPath.Block);
            
            for (var y = 0; y < _field.Size.y; y++)
            {
                for (var x = 0; x < _field.Size.x; x++)
                {
                    var height = _field.GetHeight(new Vector2Int(x, y));
                    
                    if (height <= 0)
                        continue;
                    
                    ShowBlock(blockPrefab, x, y, height);
                }
            }
        }

        private void ShowBlock(GameObject blockPrefab, int x, int y, int height)
        {
            var block = Instantiate(blockPrefab, transform);
            var blockTransform = block.transform;
            var position = _positionConverter.ConvertToWorld(new Vector2(x, y), useOffset: true);
            position.y = height / 2f;
            blockTransform.position = position;
            blockTransform.localScale = new Vector3(1, height, 1);
        }
    }
}