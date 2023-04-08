using FontAwesome.Sharp;
using index_editor_app_engine.JsonClasses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Shapes;

namespace index_editor_app_engine
{
    public class SpecialtyHandler
    {
        public Specialties specialtyPage;
        public IndexAPIClient indexClient;
        private ImageHandler imageHandler;
        private Icons icons;

        public SpecialtyHandler(string SpecialtiesJson, IndexAPIClient client, ImageHandler imageHandler, Icons icons)
        {
            this.imageHandler = imageHandler;
            this.indexClient = client;
            this.specialtyPage = JsonConvert.DeserializeObject<Specialties>(SpecialtiesJson);
            this.icons = icons;
        }

        public async Task<System.Drawing.Image> GetImageAsync(int index)
        {
            return System.Drawing.Image.FromStream(await imageHandler.GetImageAsync(specialtyPage.SpecialtiesList[index].Image));
        }

        public void AddImage(string path, int index)
        {
            specialtyPage.SpecialtiesList[index].Image = path;
        }

        public int SpecialtyCount()
        {
            return specialtyPage.SpecialtiesList.Count;
        }

        public Task<HttpResponseMessage> Upload()
        {
            // put all of the local images
            imageHandler.UploadSpecialtyImages(specialtyPage);

            string updatedSpecialtiesPageString = JsonConvert.SerializeObject(specialtyPage);
            var httpResponse = indexClient.PutDocument(updatedSpecialtiesPageString, "specialties");
            return httpResponse;
        }

        public int CreateNewSpecialty()
        {
            Specialty s = new Specialty();
            s.Name = "NEW SPECIALTY";
            s.Link = "";
            s.Subtitle = "";
            s.Description = "";
            s.Image = "";
            s.Bulletpoints = new List<string>();
            s.Bulletpoints.Add(" ");
            s.Icon = "";
            specialtyPage.SpecialtiesList.Add(s);
            return specialtyPage.SpecialtiesList.IndexOf(s);
        }

        public void DeleteSpecialty(int index)
        {
            specialtyPage.SpecialtiesList.RemoveAt(index);
        }

        public void UpdateIcon(string iconName, int index)
        {
            if (iconName == "None")
            {
                specialtyPage.SpecialtiesList[index].Icon = "";
            }
            else
            {
                //specialtyPage.SpecialtiesList[index].Icon = String.Format("fa fa-{0}", IconConversionToCssIcon[iconName]);
                specialtyPage.SpecialtiesList[index].Icon = icons.IconToCss(iconName);
            }
        }

        public string GetIcon(int index)
        {
            return icons.GetIcon(index, specialtyPage.SpecialtiesList[index].Icon.Substring(6));
        }

        public List<string> GetIconList()
        {
            return icons.GetIconList();
            //return IconConversionToCharIcon.Values.ToList();
        }

        public List<string> GetSpecialtyNames()
        {
            List<string> specialtyNames = new List<string>();
            foreach (Specialty s in specialtyPage.SpecialtiesList)
            {
                specialtyNames.Add(s.Name);
            }
            return specialtyNames;
        }

        public string GetJsonString()
        {
            string updatedSpecialtiesPageString = JsonConvert.SerializeObject(specialtyPage);
            return updatedSpecialtiesPageString;
        }
        public List<string> GetImageList()
        {
            List<string> urls = new List<string>();
            foreach (Specialty s in specialtyPage.SpecialtiesList)
            {
                urls.Add(s.Image);
            }
            return urls;
        }
    }
}
