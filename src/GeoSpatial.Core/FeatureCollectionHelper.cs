using System;
using System.Collections.Generic;
using GeoAPI.Geometries;
using NetTopologySuite.Features;

namespace GeoSpatial.Core
{
    public class FeatureCollectionHelper
    {
        private const string PointGeometryType = "Point";

        public static IEnumerable<Coordinate> GetPointCoordinatesFromFeatureCollection(FeatureCollection featureCollection)
        {
            foreach (var feature in featureCollection.Features)
            {
                if (feature.Geometry.GeometryType.Equals(PointGeometryType, StringComparison.OrdinalIgnoreCase))
                {
                    yield return feature.Geometry.Coordinate;
                }
            }
        }
    }
}
