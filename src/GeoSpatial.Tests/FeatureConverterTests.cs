using System.Collections.Generic;
using GeoAPI.Geometries;
using GeoSpatial.Core;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using Xunit;

namespace GeoSpatial.Tests
{
    public class FeatureConverterTests
    {
        [Fact]
        public void ConvertRowToFeature_ShouldReturnFeatureWithCoordinate()
        {
            // Arrange
            const string csvLine =
                @"Piazza di Pasquino,poi,244,41.897832155228,12.471773,Rome,Statua di Pasquino,4adcdac6f964a520275321e3,0,Monument / Landmark";
            const string expectedName = "Statua di Pasquino";
            const string expectedCategory = "Monument / Landmark";
            const double expectedLong = 12.471773d;
            const double expectedLat = 41.897832155228d;
            var row = csvLine.Split(',');
            var converter = new FeatureConverter();

            // Act
            var feature = converter.ConvertRowToFeature(row);

            // Assert
            Assert.Equal(expectedName, feature.Attributes["name"]);
            Assert.Equal(expectedCategory, feature.Attributes["category"]);
            Assert.Equal(expectedLong, feature.Geometry.Coordinate.X);
            Assert.Equal(expectedLat, feature.Geometry.Coordinate.Y);
        }

        [Fact]
        public void GivenFeatureWithAllAttributes_WhenConvertFeatureToDtoIsCalled_ThenDtoShouldBeHaveAllPropertiesSet()
        {
            // Arrange
            const string address = "Piazza di Pasquino";
            const string name = "Statua di Pasquino";
            const string category = "Monument / Landmark";
            const double longValue = 12.471773d;
            const double latValue = 41.897832155228d;
            var geometryFactory = new GeometryFactory();
            var point = geometryFactory.CreatePoint(new Coordinate(longValue, latValue));
            var feature = new Feature(point, new AttributesTable(new Dictionary<string, object> { { "address", address}, { "name", name}, { "category", category} }));
            var converter = new FeatureConverter();

            // Act
            var featureDto = converter.ConvertFeatureToDto(feature);

            // Assert
            
            Assert.Equal(name, featureDto.Name);
            Assert.Equal(longValue, featureDto.PointGeometry.Coordinates[0]);
            Assert.Equal(latValue, featureDto.PointGeometry.Coordinates[1]);
        }

        [Fact]
        public void GivenFeatureWithoutName_WhenConvertFeatureToDtoIsCalled_ThenDtoNameShouldBeEmpty()
        {
            // Arrange
            const string address = "Piazza di Pasquino";
            const double longValue = 12.471773d;
            const double latValue = 41.897832155228d;
            var geometryFactory = new GeometryFactory();
            var point = geometryFactory.CreatePoint(new Coordinate(longValue, latValue));
            var feature = new Feature(point, new AttributesTable(new Dictionary<string, object> { { "address", address } }));
            var converter = new FeatureConverter();

            // Act
            var featureDto = converter.ConvertFeatureToDto(feature);

            // Assert

            Assert.Empty(featureDto.Name);
        }

        [Fact]
        public void GivenFeatureWithoutCategory_WhenConvertFeatureToDtoIsCalled_ThenDtoCategoryShouldBeEmpty()
        {
            // Arrange
            const string address = "Piazza di Pasquino";
            const double longValue = 12.471773d;
            const double latValue = 41.897832155228d;
            var geometryFactory = new GeometryFactory();
            var point = geometryFactory.CreatePoint(new Coordinate(longValue, latValue));
            var feature = new Feature(point, new AttributesTable(new Dictionary<string, object> { { "address", address } }));
            var converter = new FeatureConverter();

            // Act
            var featureDto = converter.ConvertFeatureToDto(feature);

            // Assert

            Assert.Empty(featureDto.Category);
        }
    }
}
