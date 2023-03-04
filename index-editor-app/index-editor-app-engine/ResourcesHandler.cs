using index_editor_app_engine.JsonClasses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace index_editor_app_engine
{
    public class ResourcesHandler
    {
        public ResourcesPage resourcesPage;
        public IndexAPIClient indexClient; //API client
        public Dictionary<string, string> resourcesImageDict = new Dictionary<string, string> { }; //links memebers to local image paths


        public ResourcesHandler(string NewsJson, IndexAPIClient client)
        {
            this.indexClient = client;
            this.resourcesPage = JsonConvert.DeserializeObject<ResourcesPage>(NewsJson);
        }



        public async Task<MemoryStream> LoadNewsImageHandlerAsync(string title)
        {
            NewsItem n = newsPage.NewsItems.First(item => item.Title == title);

            if (n.Image == "")
            {
                return null;
            }
            if (NewsImageDict.ContainsKey(title))
            {
                return LoadImageLocal(NewsImageDict[title]);
            }
            else
            {
                return await LoadImageAPI(n.Image);
            }
        }

        public async Task<MemoryStream> LoadImageAPI(string name)
        {
            byte[] image = await indexClient.GetImageAsync(name);
            MemoryStream ms = new MemoryStream(image, 0, image.Length);
            return ms;
        }

        //returns LOCAL image given path
        public MemoryStream LoadImageLocal(string path)
        {
            byte[] image = File.ReadAllBytes(path);
            MemoryStream ms = new MemoryStream(image, 0, image.Length);
            return ms;
        }









    }
}
