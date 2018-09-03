using System.Linq;
using GeoSpatial.Console;
using Xunit;
using Xunit.Abstractions;

namespace GeoSpatial.Tests
{
    public class CentroidHelperTests
    {
        private readonly ITestOutputHelper _output;

        public CentroidHelperTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void CalculateCentroidFromGeoJsonData()
        {
            const string jsonData =
@"{
""type"": ""FeatureCollection"",
""name"": ""Locations"",
""crs"": { ""type"": ""name"", ""properties"": { ""name"": ""urn:ogc:def:crs:OGC:1.3:CRS84"" } },
""features"": [
{ ""type"": ""Feature"", ""properties"": { ""Name"": ""Basilica Parrocchiale Santa Maria del Popolo"", ""description"": null }, ""geometry"": { ""type"": ""Point"", ""coordinates"": [ 12.476311, 41.91145 ] } },
{ ""type"": ""Feature"", ""properties"": { ""Name"": ""Santa Maria della Vittoria Church"", ""description"": null }, ""geometry"": { ""type"": ""Point"", ""coordinates"": [ 12.494436, 41.904736111111113 ] } },
{ ""type"": ""Feature"", ""properties"": { ""Name"": ""Castel Sant'Angelo"", ""description"": null }, ""geometry"": { ""type"": ""Point"", ""coordinates"": [ 12.466396, 41.903027 ] } },
{ ""type"": ""Feature"", ""properties"": { ""Name"": ""Saint Marcello Al Corso"", ""description"": null }, ""geometry"": { ""type"": ""Point"", ""coordinates"": [ 12.481887, 41.8986 ] } }
]
}";
            var featureCollection = FeatureCollectionLoader.LoadFeatureCollectionFromGeoJsonString(jsonData); 
            var coordinates = FeatureCollectionHelper.GetPointCoordinatesFromFeatureCollection(featureCollection).ToArray();
            var centroid = CentroidHelper.GetCentroidForCoordinates(coordinates);
            var coordinateWithLargestDistanceFromCentroid = CentroidHelper.GetLargestDistanceFromCentroid(coordinates, centroid);

            _output.WriteLine($"centroid: {centroid}");
            _output.WriteLine($"coordinate with largest distance from centroid: {coordinateWithLargestDistanceFromCentroid.CoordinateValue}");
        }
    }
}
