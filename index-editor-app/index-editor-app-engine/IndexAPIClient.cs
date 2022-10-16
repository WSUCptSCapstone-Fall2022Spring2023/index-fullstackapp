using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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
        public string TestGet()
        {
            return root["INDEX_API_ENDPOINT"];
        }

        public async Task<string?> GetEvents()
        {
            var builder = new UriBuilder(root["INDEX_API_ENDPOINT"] + "events.json");


            var httpResponse = await httpClient.GetAsync(builder.ToString());

            if (!httpResponse.IsSuccessStatusCode)
            {

            }

            return await httpResponse.Content.ReadAsStringAsync();
        }


        public async Task<bool> TestConnection()
        {
            try
            {
                var builder = new UriBuilder(root["INDEX_API_ENDPOINT"] + "events.json");
                var httpResponse = await httpClient.GetAsync(builder.ToString());

                return httpResponse.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }


}
