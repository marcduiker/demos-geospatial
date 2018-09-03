using GeoSpatial.Console.Models;
using Newtonsoft.Json;


namespace GeoSpatial.Console
{
    public class FeatureDto
    {
        public FeatureDto(
            string address,
            string name, 
            string category, 
            PointDto pointGeometry)
        {
            Address = address;
            Name = name;
            Category = category;
            PointGeometry = pointGeometry;
            Type = "feature";
        }

        [JsonProperty("address")]
        public string Address { get; }

        [JsonProperty("name")]
        public string Name { get; }

        [JsonProperty("category")]
        public string Category { get; }

        [JsonProperty("type")]
        public string Type { get; }

        [JsonProperty("geometry")]
        public PointDto PointGeometry { get; }
    }
}
