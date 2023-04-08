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
        public IndexAPIClient indexClient;
        private ImageHandler imageHandler;
        private Icons icons;

        public ResourcesHandler(string resourceJson, IndexAPIClient client, ImageHandler imageHandler, Icons icons)
        {
            this.indexClient = client;
            this.resourcesPage = JsonConvert.DeserializeObject<ResourcesPage>(resourceJson);
            this.imageHandler = imageHandler;
            this.icons = icons;
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
            return System.Drawing.Image.FromStream(await imageHandler.GetImageAsync(resourcesPage.Resources[index].PageImage));
        }

        public void AddImage(string path, int index)
        {
            resourcesPage.Resources[index].PageImage = path;
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

        public void UpdateIcon(string iconName, int index)
        {
            if (iconName == "None")
            {
                resourcesPage.Resources[index].PageIcon = "";
            }
            else
            {
                resourcesPage.Resources[index].PageIcon = icons.IconToCss(iconName);
            }
        }

        public List<string> GetIconList()
        {
            return icons.GetIconList();
        }
        public string GetIcon(int index)
        {
            return icons.GetIcon(index, resourcesPage.Resources[index].PageIcon.Substring(6));
        }

        public void CreateResource()
        {
            Resource resource = new Resource();
            resource.PageTitle = "New Resource";
            resource.PageIcon = "";
            resource.PageLink = "";
            resource.PageImage = "";
            resource.PageDescription = "";
            resource.Bulletpoints = new List<Bulletpoint>();
            resourcesPage.Resources.Add(resource);
        }
        public void DeleteResource(int resourceIndex)
        {
            resourcesPage.Resources.RemoveAt(resourceIndex);
        }
        public Task<HttpResponseMessage> UpdateResourcePage()
        {
            imageHandler.UploadResourceImages(resourcesPage);

            string updatedResourcePageString = JsonConvert.SerializeObject(resourcesPage);
            var httpResponse = indexClient.PutDocument(updatedResourcePageString, "resources");
            return httpResponse;
        }

        public string GetJsonString()
        {
            string updatedSpecialtiesPageString = JsonConvert.SerializeObject(resourcesPage);
            return updatedSpecialtiesPageString;
        }
        public List<string> GetImageList()
        {
            List<string> urls = new List<string>();
            foreach (Resource r in resourcesPage.Resources)
            {
                urls.Add(r.PageImage);
            }
            return urls;
        }
    }
}
