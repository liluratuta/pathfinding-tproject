using TestProject.GameField;
using TestProject.PathFinding;
using TestProject.Services.Assets;
using TestProject.Services.FieldConfig;
using TestProject.Services.StaticData;
using TestProject.Utility;
using TestProject.View;
using UnityEngine;

namespace TestProject
{
    public class EntryPoint : MonoBehaviour
    {
        public GameFieldView GameFieldView;
        public PathPointsSelector PathPointsSelector;
        public PathView PathView;

        private readonly AssetProvider _assets = new AssetProvider();
        private readonly StaticDataProvider _staticDataProvider = new StaticDataProvider();
        
        private FieldConfigProvider _fieldConfigProvider;
        private Field _field;
        private PathFinder _pathFinder;
        private PositionConverter _positionConverter;
        
        private void Awake()
        {
            InitServices();
            InitViews();
        }

        private void InitServices()
        {
            _staticDataProvider.Load();
            _fieldConfigProvider = new FieldConfigProvider(_staticDataProvider);
            _fieldConfigProvider.Load();
            _field = new Field(_fieldConfigProvider.Size, _fieldConfigProvider.Heights);
            _pathFinder = new PathFinder(_field);
            _positionConverter = new PositionConverter(_staticDataProvider);
        }

        private void InitViews()
        {
            GameFieldView.Init(_field, _assets, _positionConverter);
            PathPointsSelector.Init(_field, _positionConverter, _staticDataProvider.FieldData);
            PathView.Init(_positionConverter, _pathFinder, _field);
        }
    }
}