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
        private ImageHandler imageHandler;

        public ResourcesHandler(string NewsJson, IndexAPIClient client, ImageHandler imageHandler)
        {
            this.indexClient = client;
            this.resourcesPage = JsonConvert.DeserializeObject<ResourcesPage>(NewsJson);
            this.imageHandler = imageHandler;
        }

        public int ResourcesCount()
        {
            return resourcesPage.Resources.Count();
        }

        public string GetPageTitle(int i)
        {
            return resourcesPage.Resources[i].PageTitle;
        }

        public Resource GetResource(int i)
        {
            return resourcesPage.Resources[i];
        }

        public void SetResource(int i, Resource newResource)
        {
            resourcesPage.Resources[i] = newResource;
        }

        public Bulletpoint GetResourceBulletpoint(int resourceIndex, int bulletpointIndex)
        {
            return resourcesPage.Resources[resourceIndex].Bulletpoints[bulletpointIndex];
        }

        public void SetResourceBulletpoint(int resourceIndex, int bulletpointIndex, Bulletpoint newBulletpoint)
        {
            resourcesPage.Resources[resourceIndex].Bulletpoints[bulletpointIndex] = newBulletpoint;
        }

        public LinkPhrase GetResourceBulletpointLinkPhrase(int resourceIndex, int bulletpointIndex, int linkPhraseIndex)
        {
            return resourcesPage.Resources[resourceIndex].Bulletpoints[bulletpointIndex].LinkPhrases[linkPhraseIndex];
        }

        public void SetResourceBulletpointLinkPhrase(int resourceIndex, int bulletpointIndex, int linkPhraseIndex, LinkPhrase linkPhrase)
        {
            resourcesPage.Resources[resourceIndex].Bulletpoints[bulletpointIndex].LinkPhrases[linkPhraseIndex] = linkPhrase;
        }

        public async Task<System.Drawing.Image> GetImageAsync(int index)
        {
            return System.Drawing.Image.FromStream(await imageHandler.GetImageAsync(resourcesPage.Resources[index]));
        }

        public void AddImage(string path, int index)
        {
            imageHandler.AddImage(GetResource(index), path);
        }

        public void AddBulletPoint(int resourceIndex)
        {
            Bulletpoint bulletpoint = new Bulletpoint();
            bulletpoint.Title = "New bulletpoint! Edit me";
            bulletpoint.Description = "";
            bulletpoint.Link = "";
            bulletpoint.LinkPhrases = new List<LinkPhrase>();

            resourcesPage.Resources[resourceIndex].Bulletpoints.Add(bulletpoint);
        }

        public void AddLinkPhrase(int resourceIndex, int bulletpointIndex)
        {
            LinkPhrase linkPhrase = new LinkPhrase();
            linkPhrase.Phrase = "EDIT ME";
            linkPhrase.Link = "";
            resourcesPage.Resources[resourceIndex].Bulletpoints[bulletpointIndex].LinkPhrases.Add(linkPhrase);
        }

        public string ValidateLinkPhrases(int resourceIndex, int bulletpointIndex)
        {
            string validationMessage = "";
            Bulletpoint bulletpoint = GetResourceBulletpoint(resourceIndex, bulletpointIndex);

            foreach (LinkPhrase linkphrase in bulletpoint.LinkPhrases)
            {
                if (!bulletpoint.Description.Contains(linkphrase.Phrase))
                {
                    validationMessage += "The phrase: \"" + linkphrase.Phrase + "\" was not found in bulletpoint description\n";
                }
            }
            if (validationMessage == "")
            {
                validationMessage = "All links are correctly embedded";
            }
            return validationMessage;
        }

        //public async Task<MemoryStream> LoadNewsImageHandlerAsync(string title)
        //{
        //    NewsItem n = newsPage.NewsItems.First(item => item.Title == title);

        //    if (n.Image == "")
        //    {
        //        return null;
        //    }
        //    if (NewsImageDict.ContainsKey(title))
        //    {
        //        return LoadImageLocal(NewsImageDict[title]);
        //    }
        //    else
        //    {
        //        return await LoadImageAPI(n.Image);
        //    }
        //}

        //public async Task<MemoryStream> LoadImageAPI(string name)
        //{
        //    byte[] image = await indexClient.GetImageAsync(name);
        //    MemoryStream ms = new MemoryStream(image, 0, image.Length);
        //    return ms;
        //}

        ////returns LOCAL image given path
        //public MemoryStream LoadImageLocal(string path)
        //{
        //    byte[] image = File.ReadAllBytes(path);
        //    MemoryStream ms = new MemoryStream(image, 0, image.Length);
        //    return ms;
        //}









    }
}
