using System.Collections.Generic;
using System.Linq;
using GeoAPI.Geometries;
using GeoSpatial.Console.Models;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;

namespace GeoSpatial.Console
{
    public class FeatureConverter
    {
        private readonly GeometryFactory _geometryFactory;
        
        public FeatureConverter()
        {
            _geometryFactory = new GeometryFactory();
        }
        
        public Feature ConvertRowToFeature(string[] row)
        {
            Dictionary<string, object> featureAttributeDictionary = new Dictionary<string, object>();
            var address = row[0];
            featureAttributeDictionary.Add("address", address);
            var lat = double.Parse(row[3]);
            var lng = double.Parse(row[4]);
            if (row.Length > 6)
            {
                var name = row[6];
                featureAttributeDictionary.Add("name", name);
            }

            if (row.Length > 9)
            {
                var category = row[9];
                featureAttributeDictionary.Add("category", category);
            }

            var point = _geometryFactory.CreatePoint(new Coordinate(lng, lat));
            var feature = new Feature(point, new AttributesTable(featureAttributeDictionary));

            return feature;
        }

        public FeatureDto ConvertFeatureToDto(Feature feature)
        {
            var pointDto = new PointDto(
                feature.Geometry.Coordinate.X,
                feature.Geometry.Coordinate.Y);
            var address = feature.Attributes["address"].ToString();
            var attributesNames = feature.Attributes.GetNames();
            string name = string.Empty;
            if (attributesNames.Contains("name"))
            {
                name = feature.Attributes["name"].ToString();
            }

            string category = string.Empty;
            if (attributesNames.Contains("category"))
            {
                category = feature.Attributes["category"].ToString();
            }

            var featureDto = new FeatureDto(
                address,
                name,
                category,
                pointDto);

            return featureDto;
        }
    }
}
