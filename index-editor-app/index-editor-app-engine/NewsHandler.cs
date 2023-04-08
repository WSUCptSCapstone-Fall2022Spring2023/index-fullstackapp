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
        public IndexAPIClient indexClient;
        private ImageHandler imageHandler;

        public NewsHandler(string NewsJson, IndexAPIClient client, ImageHandler imageHandler)
        {
            this.indexClient = client;
            this.imageHandler = imageHandler;
            this.newsPage = JsonConvert.DeserializeObject<NewsPage>(NewsJson);
        }

        public async Task<System.Drawing.Image> GetImageAsync(int index)
        {
            return System.Drawing.Image.FromStream(await imageHandler.GetImageAsync(newsPage.NewsItems[index].Image));
        }

        public void AddImage(string path, int index)
        {
            newsPage.NewsItems[index].Image = path;
        }

        public int NewsCount()
        {
            return newsPage.NewsItems.Count;
        }

        public Task<HttpResponseMessage> Upload()
        {
            imageHandler.UploadNewsImages(newsPage);

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

        public List<string> GetImageList()
        {
            List<string> urls = new List<string>();
            foreach (NewsItem n in newsPage.NewsItems)
            {
                urls.Add(n.Image);
            }
            return urls;
        }
    }
}

