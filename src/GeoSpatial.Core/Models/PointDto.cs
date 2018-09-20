using Newtonsoft.Json;

namespace GeoSpatial.Core.Models
{
    public class PointDto
    {
        public PointDto(double longitude, double latitude)
        {
            Type = "Point";
            Coordinates = new[] { longitude, latitude };
        }

        [JsonProperty("type")]
        public string Type { get; }

        [JsonProperty("coordinates")]
        public double[] Coordinates { get; }
    }
}
