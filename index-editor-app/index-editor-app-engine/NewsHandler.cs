using index_editor_app_engine.JsonClasses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace index_editor_app_engine
{
    public class NewsHandler
    {
        public NewsPage newsPage;
        //public Dictionary<string, string> MemberSpecialtyDict = new Dictionary<string, string> { }; //links memebers to local image paths
        public IndexAPIClient indexClient; //API client
        public Dictionary<string, string> NewsImageDict = new Dictionary<string, string> { }; //links memebers to local image paths
        private ImageHandler imageHandler;

        public NewsHandler(string NewsJson, IndexAPIClient client, ImageHandler imageHandler)
        {
            this.indexClient = client;
            this.imageHandler = imageHandler;
            this.newsPage = JsonConvert.DeserializeObject<NewsPage>(NewsJson);
        }




        public async Task<System.Drawing.Image> GetImageAsync(int index)
        {
            return System.Drawing.Image.FromStream(await imageHandler.GetImageAsync(newsPage.NewsItems[index]));
        }

        public void AddImage(string path, int index)
        {
            imageHandler.AddImage(newsPage.NewsItems[index], path);
        }



        public void AddNewsImage(string fileName, int editingNewsIndex)
        {
            NewsImageDict[newsPage.NewsItems.ElementAt(editingNewsIndex).Title] = fileName;
            string dirName = new DirectoryInfo(fileName).Name;
            string url = "https://index-webapp.s3.amazonaws.com/img/newsimages/" + dirName;
            newsPage.NewsItems.ElementAt(editingNewsIndex).Image = url;
        }

        public int NewsCount()
        {
            return newsPage.NewsItems.Count;
        }

        public Task<HttpResponseMessage> Upload()
        {
            // put all of the local images
            foreach (string key in NewsImageDict.Keys)
            {
                indexClient.PutImageAsync(NewsImageDict[key], "newsimages");
            }

            string updatedNewsPageString = JsonConvert.SerializeObject(newsPage);
            var httpResponse = indexClient.PutDocument(updatedNewsPageString, "news");
            return httpResponse;
        }

        public int CreateNewNews()
        {
            NewsItem n = new NewsItem();
            n.Title = "NEW NEWSLETTER";
            n.Date = "";
            n.EditorDateTime = DateTime.Now.ToString();
            n.Image = "";
            n.Description = "";
            n.NewsLink = "";
            n.PostedBy = "";
            newsPage.NewsItems.Add(n);
            return newsPage.NewsItems.IndexOf(n);
        }

        public void DeleteNews(int index)
        {
            newsPage.NewsItems.RemoveAt(index);
        }

        public string GetJsonString()
        {
            string updatedNewsPageString = JsonConvert.SerializeObject(newsPage);
            return updatedNewsPageString;
        }
    }
}

