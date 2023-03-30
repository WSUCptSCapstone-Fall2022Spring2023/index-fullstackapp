using index_editor_app_engine.JsonClasses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace index_editor_app_engine
{
    public class ImageHandler
    {
        private IndexAPIClient indexClient;
        public ImageHandler(IndexAPIClient indexAPIClient)
        {
            indexClient = indexAPIClient;
        }

        //newsimages
        //eventimages
        //memberimages
        //specialtyimages
        //Resourceimages

        //for news,  dict[newsPage.NewsItems.ElementAt(editingNewsIndex).Title] = fileName
        //string dirName = new DirectoryInfo(fileName).Name;
        //string url = "https://index-webapp.s3.amazonaws.com/img/newsimages/" + dirName;
        //newsPage.NewsItems.ElementAt(editingNewsIndex).Image = url;


        //add ((KEY) path) to dictionary (for local fetch and uploading)
        //get filename
        //create url

        //given a path add image to corresponding dictionary

        public Dictionary<NewsItem, string> NewsImageDict = new Dictionary<NewsItem, string> { }; //links memebers to local image paths
        public Dictionary<string, string> MemberImageDict = new Dictionary<string, string> { }; //links memebers to local image paths
        public Dictionary<Specialty, string> SpecialtyImageDict = new Dictionary<Specialty, string> { }; //links memebers to local image paths
        public Dictionary<string, string> EventImageDict = new Dictionary<string, string> { }; //links events to local image paths
        public Dictionary<Resource, string> ResourceImageDict = new Dictionary<Resource, string> { }; //links events to local image paths
        public async Task<MemoryStream> GetImageAsync(NewsItem n)
        {
            if (n.Image == "" || n.Image == null)   //No Image
            {
                return null;
            }
            else if (NewsImageDict.ContainsKey(n))                              //Local
            {
                byte[] image = File.ReadAllBytes(NewsImageDict[n]);
                MemoryStream ms = new MemoryStream(image, 0, image.Length);
                return ms;
            }
            else                                                //API
            {
                byte[] image = await indexClient.GetImageAsync(n.Image);
                MemoryStream ms = new MemoryStream(image, 0, image.Length);
                return ms;
            }
        }

        public async Task<MemoryStream?> GetImageAsync(Specialty s)
        {
            if (s.Image == "" || s.Image == null)   //No Image
            {
                return null;
            }
            else if (SpecialtyImageDict.ContainsKey(s))                              //Local
            {
                byte[] image = File.ReadAllBytes(SpecialtyImageDict[s]);
                MemoryStream ms = new MemoryStream(image, 0, image.Length);
                return ms;
            }
            else                                                //API
            {
                byte[] image = await indexClient.GetImageAsync(s.Image);
                MemoryStream ms = new MemoryStream(image, 0, image.Length);
                return ms;
            }
        }

        public async Task<MemoryStream?> GetImageAsync(Resource r)
        {
            if (r.PageImage == "" || r.PageImage == null)   //No Image
            {
                return null;
            }
            else if (ResourceImageDict.ContainsKey(r))                              //Local
            {
                byte[] image = File.ReadAllBytes(ResourceImageDict[r]);
                MemoryStream ms = new MemoryStream(image, 0, image.Length);
                return ms;
            }
            else                                                //API
            {
                byte[] image = await indexClient.GetImageAsync(r.PageImage);
                MemoryStream ms = new MemoryStream(image, 0, image.Length);
                return ms;
            }
        }

        public void AddImage(NewsItem n, string path)
        {
            NewsImageDict[n] = path;
            string dirName = new DirectoryInfo(path).Name;
            string url = "https://index-webapp.s3.amazonaws.com/img/newsimages/" + dirName;
            n.Image = url;
            NewsImageDict[n] = path;
        }

        public void AddImage(Specialty s, string path)
        {
            SpecialtyImageDict[s] = path;
            string dirName = new DirectoryInfo(path).Name;
            string url = "https://index-webapp.s3.amazonaws.com/img/specialtyimages/" + dirName;
            s.Image = url;
            SpecialtyImageDict[s] = path;
        }

        public void AddImage(Resource r, string path)
        {
            ResourceImageDict[r] = path;
            string dirName = new DirectoryInfo(path).Name;
            string url = "https://index-webapp.s3.amazonaws.com/img/resourceimages/" + dirName;
            r.PageImage = url;
            ResourceImageDict[r] = path;
        }



        // TODO: finish refactoring upload functionality
        public void UploadMemberImages()
        {

        }

        public void UploadNewsImages()
        {

        }

        public void UploadEventImages()
        {

        }

        public void UploadSpecialtyImages()
        {
            // put all of the local images
            //foreach (var key in SpecialtyImageDict.Keys)
            //{
            //    indexClient.PutImageAsync(SpecialtyImageDict[key], "specialtyimages");
            //}

        }

        public void UploadResourceImages()
        {

        }
    }
}
