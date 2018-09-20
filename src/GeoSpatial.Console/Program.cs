using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GeoSpatial.Core;
using GeoSpatial.Core.Models;

namespace GeoSpatial.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            string fileName = args.Length > 0 ? args[0] : @"C:\temp\rome-poi-cleaned.csv";
            List<FeatureDto> featureDtoCollection = ProcessCsvFile(fileName).ToList();
            var task = SaveGeoJson(featureDtoCollection);
            Task.WaitAll(task);
            System.Console.WriteLine("Done.");
            System.Console.ReadLine();
        }

        private static async Task SaveGeoJson(IEnumerable<FeatureDto> geoJsonCollection)
        {
            var storage = new AzureStorage();
            foreach (var geoJson in geoJsonCollection)
            {
                await storage.Save(geoJson);
            }
        }

        private static IEnumerable<FeatureDto> ProcessCsvFile(string fileName)
        {
            var converter = new FeatureConverter();
            var readFile = new StreamReader(fileName);
            string line;

            // first line contains the headers which can be ignored
            readFile.ReadLine();

            while ((line = readFile.ReadLine()) != null)
            {
                string[] row = line.Split(',');
                var feature = converter.ConvertRowToFeature(row);
                var featureDto = converter.ConvertFeatureToDto(feature);

                yield return featureDto;
            }
            readFile.Close();
        }
    }
}
