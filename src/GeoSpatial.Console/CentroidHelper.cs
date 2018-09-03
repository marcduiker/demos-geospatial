using System.Collections.Generic;
using System.Linq;
using GeoAPI.Geometries;
using NetTopologySuite.Geometries;

namespace GeoSpatial.Console
{
    public class CentroidHelper
    {
        public static IPoint GetCentroidForCoordinates(IEnumerable<Coordinate> coordinates)
        {
            var geometryFactory = new GeometryFactory();
            var lineString = geometryFactory.CreateLineString(coordinates.ToArray());

            return lineString.Centroid;
        }

        public static Coordinate GetLargestDistanceFromCentroid(IEnumerable<Coordinate> coordinates, IPoint centroid)
        {
            Coordinate largestDistanceFromCentroid = new Coordinate();
            double longestDistance = 0;
            foreach (var coordinate in coordinates)
            {
                var distance = coordinate.Distance(centroid.Coordinate);
                if (distance > longestDistance)
                {
                    longestDistance = distance;
                    largestDistanceFromCentroid = coordinate;
                }
            }

            return largestDistanceFromCentroid;
        }
    }
}
