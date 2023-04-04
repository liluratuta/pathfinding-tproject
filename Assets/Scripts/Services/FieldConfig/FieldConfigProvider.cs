using System;
using System.Collections.Generic;
using System.Linq;
using TestProject.Services.StaticData;
using UnityEngine;

namespace TestProject.Services.FieldConfig
{
    public class FieldConfigProvider
    {
        private const string RowSeparator = "\n";
        private const string ValuesSeparator = " ";

        public Vector2Int Size { get; private set; }
        public List<int> Heights { get; private set; }
        
        private readonly IStaticDataProvider _staticDataProvider;

        public FieldConfigProvider(IStaticDataProvider staticDataProvider)
        {
            _staticDataProvider = staticDataProvider;
        }

        public void Load()
        {
            var rows = GetRows();

            if (rows.Length < 2)
                throw new Exception("Config not valid");

            Size = ReadSize(rows[0]);
            Heights = ReadHeights(rows
                .Skip(1)
                .ToArray());
        }

        private string[] GetRows()
        {
            var dirtyRows = _staticDataProvider.FieldData.Config.text.Split(RowSeparator);
            return dirtyRows.Select(Clean).Where(x => !string.IsNullOrEmpty(x)).ToArray();
            
            string Clean(string row) =>
                row.Replace("\n", "").Replace("\r", "");
        }

        private List<int> ReadHeights(string[] rows)
        {
            if (rows.Length != Size.y)
                throw new Exception("Field values height not equal SizeY");

            var heights = new List<int>();

            foreach (var row in rows)
            {
                var values = row.Split(ValuesSeparator);

                if (values.Length != Size.x)
                    throw new Exception("Field values width not equal SizeX");
                
                foreach (var value in values)
                {
                    if (!int.TryParse(value, out var height))
                        throw new Exception($"Height value \"{value}\" not parse to int");

                    heights.Add(height);
                }
            }

            return heights;
        }

        private Vector2Int ReadSize(string row)
        {
            var sizeValues = row.Split(ValuesSeparator);

            if (sizeValues.Length != 2)
                throw new Exception("Size in config not valid");

            if (!int.TryParse(sizeValues[0], out var sizeX))
                throw new Exception("Size X value not valid");
            
            if (!int.TryParse(sizeValues[1], out var sizeY))
                throw new Exception("Size Y value not valid");

            return new Vector2Int(sizeX, sizeY);
        }
    }
}