using System.Collections.Generic;
using System.Linq;
using GeoAPI.Geometries;
using NetTopologySuite.Geometries;

namespace GeoSpatial.Core
{
    public class CentroidHelper
    {
        public static IPoint GetCentroidForCoordinates(IList<Coordinate> coordinates, bool forcePolygon = false)
        {
            IList<Coordinate> coordinateList = new List<Coordinate>(coordinates);
            var geometryFactory = new GeometryFactory();
            var firstCoordinate = coordinateList.First();
            var lastCoordinate = coordinateList.Last();
            const double tolerance = 0.0001;
            
            // Add the first coordinate to the coordinate collection to force a polygon.
            if (forcePolygon)
            {
                coordinateList.Add(firstCoordinate);
                lastCoordinate = coordinateList.Last();
            }

            IPoint centroid;
            if (firstCoordinate.Equals2D(lastCoordinate, tolerance))
            {
                var polygon = geometryFactory.CreatePolygon(coordinateList.ToArray());
                centroid = polygon.Centroid;
            }
            else
            {
                var lineString = geometryFactory.CreateLineString(coordinateList.ToArray());
                centroid = lineString.Centroid;
            }

            return centroid;
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
