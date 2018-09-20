using System.Linq;
using GeoAPI.Geometries;
using GeoSpatial.Core;
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
        public void GetCentroidForCoordinates_ShouldReturnCentroidForLineString_WhenCoordinatesAreLineString()
        {
            // Arrange
            var jsonData = GetGeoJson();
            var featureCollection = FeatureCollectionLoader.LoadFeatureCollectionFromGeoJsonString(jsonData);
            var coordinates = FeatureCollectionHelper.GetPointCoordinatesFromFeatureCollection(featureCollection).ToArray();
            var expectedCentroidLineString = new Coordinate(12.480333076209453, 41.904384802740324);
            const double tolerance = 0.00001;

            // Act
            var resultCentroid = CentroidHelper.GetCentroidForCoordinates(coordinates);
            
            // Assert
            _output.WriteLine($"centroid for line string: {resultCentroid}");
            Assert.True(expectedCentroidLineString.Equals2D(resultCentroid.Coordinate, tolerance));
        }

        [Fact]
        public void GetCentroidForCoordinates_ShouldReturnCentroidForPolygon_WhenForcePolygonIsTrue()
        {
            // Arrange
            var jsonData = GetGeoJson();
            var featureCollection = FeatureCollectionLoader.LoadFeatureCollectionFromGeoJsonString(jsonData);
            var coordinates = FeatureCollectionHelper.GetPointCoordinatesFromFeatureCollection(featureCollection).ToArray();
            var expectedCentroidForPolygon = new Coordinate(12.495307072902103, 41.914354766677732);
            const double tolerance = 0.00001;
            const bool forcePolygon = true;

            // Act
            var resultCentroid = CentroidHelper.GetCentroidForCoordinates(coordinates, forcePolygon);

            // Assert
            _output.WriteLine($"centroid for polygon: {resultCentroid}");
            Assert.True(expectedCentroidForPolygon.Equals2D(resultCentroid.Coordinate, tolerance));
        }


        private static string GetGeoJson()
        {
            return @"{
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
        }
    }
}
