using Microsoft.Extensions.Configuration;
using System.Text;

namespace index_editor_app_engine
{
    public class IndexAPIClient
    {
        private HttpClient httpClient;
        IConfigurationRoot root;
        public IndexAPIClient()
        {
            //get configurations from appsettings
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true);
            root = builder.Build();

            //create http client with auth
            httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromMinutes(3);
            httpClient.DefaultRequestHeaders.Add("x-api-key", root["API_KEY"]);
        }

        public async Task<HttpResponseMessage> PutDocument(string document, string endpoint)
        {
            if (root["INDEX_API_ENDPOINT"] == null)
            {
                throw new Exception("ERROR LOADING APPSETTINGS");
            }

            var stringContent = new StringContent(document, Encoding.UTF8, "application/json");
            var httpResponse = await httpClient.PutAsync(root["INDEX_API_ENDPOINT"] + "/" + endpoint, stringContent);

            return httpResponse;
        }

        public async Task<string?> GetDocument(string endpoint)
        {
            var builder = new UriBuilder(root["INDEX_API_ENDPOINT"] + "/" + endpoint);
            var httpResponse = await httpClient.GetAsync(builder.ToString());
            if (!httpResponse.IsSuccessStatusCode)
            {

            }
            return await httpResponse.Content.ReadAsStringAsync();
        }

        public async Task<bool> PutImageAsync(string imageEndpoint, ByteArrayContent imageBinaryContent)
        {
            var Putresult = await httpClient.PutAsync(imageEndpoint, imageBinaryContent);

            return Putresult.IsSuccessStatusCode;
        }

        public async Task<bool> TestConnection()
        {
            try
            {
                var builder = new UriBuilder(root["INDEX_API_ENDPOINT"] + "/events");
                var httpResponse = await httpClient.GetAsync(builder.ToString());

                return httpResponse.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<byte[]> GetImageAsync(string imageURL)
        {
            var result = await httpClient.GetAsync(imageURL);

            byte[] image = await result.Content.ReadAsByteArrayAsync();

            return image;
        }
    }
}
