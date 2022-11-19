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

        public async Task<HttpResponseMessage> PutDocument(string document)
        {
            var stringContent = new StringContent(document, Encoding.UTF8, "application/json");
            var httpResponse = await httpClient.PutAsync(root["INDEX_API_ENDPOINT"] + "events.json", stringContent);
            return httpResponse;
        }

        public async Task<string?> GetDocument()
        {
            var builder = new UriBuilder(root["INDEX_API_ENDPOINT"] + "events.json");
            var httpResponse = await httpClient.GetAsync(builder.ToString());
            if (!httpResponse.IsSuccessStatusCode)
            {

            }
            return await httpResponse.Content.ReadAsStringAsync();
        }

        //https://bz682vosnb.execute-api.us-east-1.amazonaws.com/dev1/img/eventimages/NAME.png
        public async Task<string> PutImage(string imagePath)
        {
            imagePath = "C:/Users/josh/Desktop/eventImages";
            string asdur = "https://bz682vosnb.execute-api.us-east-1.amazonaws.com/dev1/img/eventimages/TEST22.png";


            using (var client = new System.Net.Http.HttpClient())
            {

                var uri = new System.Uri(asdur);

                // Load the file:
                var file = new System.IO.FileInfo(imagePath);
                if (!file.Exists)
                    throw new ArgumentException($"Unable to access file at: {imagePath}", nameof(imagePath));

                using (var stream = file.OpenRead())
                {
                    var multipartContent = new System.Net.Http.MultipartFormDataContent();
                    multipartContent.Add(
                        new System.Net.Http.StreamContent(stream),
                        "test.png", // this is the name of FormData field
                        file.Name);

                    System.Net.Http.HttpRequestMessage request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Post, uri);
                    request.Content = multipartContent;

                    var response = await httpClient.SendAsync(request);
                    response.EnsureSuccessStatusCode(); // this throws an exception on non HTTP success codes
                    return response.ToString();
                }
            }
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

        //private byte[] GetFileByteArray(string filename)
        //{
        //    FileStream oFileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);

        //    // Create a byte array of file size.
        //    byte[] FileByteArrayData = new byte[oFileStream.Length];

        //    //Read file in bytes from stream into the byte array
        //    oFileStream.Read(FileByteArrayData, 0, System.Convert.ToInt32(oFileStream.Length));

        //    //Close the File Stream
        //    oFileStream.Close();

        //    return FileByteArrayData; //return the byte data
        //}


    }


}
