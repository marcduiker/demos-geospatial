using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace GeoSpatial.Console
{
    public class AzureStorage
    {
        private static readonly string _endpointUrl = "https://localhost:8081";
        private static readonly string _authorizationKey = "";
        private static readonly string _databaseId = "Rome";
        private static readonly string _collectionId = "Data";
        private static DocumentClient _client;
        private static Uri _collectionUri;

        public AzureStorage()
        {
            Initialize();
        }

        public async Task<(bool succes, Exception error)> Save(object geoJson)
        {
            try
            {
                using (_client = new DocumentClient(new Uri(_endpointUrl), _authorizationKey))
                {
                    var response = await _client.UpsertDocumentAsync(_collectionUri, geoJson);
                    if (response.StatusCode == HttpStatusCode.Created)
                    {
                        return (true, null);
                    }

                    return (false, null);
                }
            }
            catch (DocumentClientException de)
            {
                return (false, de.GetBaseException());
            }
            catch (Exception e)
            {
                return (false, e.GetBaseException());
            }
        }

        private static void Initialize()
        {
            _collectionUri = UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId);
        }
    }
}
