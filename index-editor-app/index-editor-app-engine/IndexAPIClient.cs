using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace index_editor_app_engine
{
    public class IndexAPIClient
    {
        
        private HttpClient httpClient;
        private object _config;

        public IndexAPIClient()
        {
            //create http client
            httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromMinutes(3);

        }

        public async Task<bool> TestConnection()
        {
            try
            {
                var builder = new UriBuilder($"{_config.OPEN_SEARCH_ENDPOINT}");
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
