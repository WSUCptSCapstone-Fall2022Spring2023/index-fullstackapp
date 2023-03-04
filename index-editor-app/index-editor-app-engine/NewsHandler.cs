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


        public NewsHandler(string NewsJson, IndexAPIClient client)
        {
            this.indexClient = client;
            this.newsPage = JsonConvert.DeserializeObject<NewsPage>(NewsJson);
        }

        public void AddNewsImage(string fileName, int editingNewsIndex)
        {
            NewsImageDict[newsPage.NewsItems.ElementAt(editingNewsIndex).Title] = fileName;
            string dirName = new DirectoryInfo(fileName).Name;
            string url = "https://index-webapp.s3.amazonaws.com/img/newsimages/" + dirName;
            newsPage.NewsItems.ElementAt(editingNewsIndex).Image = url;
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
    }
}

