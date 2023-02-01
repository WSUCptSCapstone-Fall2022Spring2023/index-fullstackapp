using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
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
            if (root["INDEX_API_ENDPOINT"] == null)
            {
                throw new Exception("ERROR LOADING APPSETTINGS");
            }


            var stringContent = new StringContent(document, Encoding.UTF8, "application/json");
            var httpResponse = await httpClient.PutAsync(root["INDEX_API_ENDPOINT"] + "/events", stringContent);

            return httpResponse;
        }

        public async Task<string?> GetDocument()
        {
            var builder = new UriBuilder(root["INDEX_API_ENDPOINT"] + "/events");
            var httpResponse = await httpClient.GetAsync(builder.ToString());
            if (!httpResponse.IsSuccessStatusCode)
            {

            }
            return await httpResponse.Content.ReadAsStringAsync();
        }

        static byte[] GetFileByteArray(string filename)
        {
            FileStream oFileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);

            // Create a byte array of file size.
            byte[] FileByteArrayData = new byte[oFileStream.Length];

            //Read file in bytes from stream into the byte array
            oFileStream.Read(FileByteArrayData, 0, System.Convert.ToInt32(oFileStream.Length));

            //Close the File Stream
            oFileStream.Close();

            return FileByteArrayData; //return the byte data
        }

        public async Task<bool> PutImageAsync(string imagePath)
        {
            byte[] myImageFile = GetFileByteArray(imagePath);

            var imageBinaryContent = new ByteArrayContent(myImageFile);

            imageBinaryContent.Headers.Add("Content-Type", "image/png");

            string dirName = new DirectoryInfo(imagePath).Name;

            string imageEndpoint = "https://bz682vosnb.execute-api.us-east-1.amazonaws.com/dev1/img/eventimages/" + dirName;

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




        public async Task<string?> GetMembers()
        {
            var builder = new UriBuilder(root["INDEX_API_ENDPOINT"] + "/members");
            var httpResponse = await httpClient.GetAsync(builder.ToString());
            if (!httpResponse.IsSuccessStatusCode)
            {

            }
            return await httpResponse.Content.ReadAsStringAsync();
        }

        public async Task<string?> GetSpecialties()
        {
            var builder = new UriBuilder(root["INDEX_API_ENDPOINT"] + "/specialties");
            var httpResponse = await httpClient.GetAsync(builder.ToString());
            if (!httpResponse.IsSuccessStatusCode)
            {

            }
            return await httpResponse.Content.ReadAsStringAsync();
        }


    }
}
