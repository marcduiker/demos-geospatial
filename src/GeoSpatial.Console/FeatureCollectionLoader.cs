using System.IO;
using NetTopologySuite.Features;
using NetTopologySuite.IO;

namespace GeoSpatial.Console
{
    public class FeatureCollectionLoader
    {
        public static FeatureCollection LoadFeatureCollectionFromGeoJsonFile(string fileName)
        {
            var jsonString = File.ReadAllText(fileName);

            return LoadFeatureCollectionFromGeoJsonString(jsonString);
        }

        public static FeatureCollection LoadFeatureCollectionFromGeoJsonString(string geoJson)
        {
            var reader = new GeoJsonReader();
            var featureCollection = reader.Read<FeatureCollection>(geoJson);

            return featureCollection;
        }
    }
}
