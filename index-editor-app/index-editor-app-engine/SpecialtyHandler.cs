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
        public Dictionary<string, string> MemberSpecialtyDict = new Dictionary<string, string> { }; //links memebers to local image paths
        public IndexAPIClient indexClient; //API client
        public Dictionary<string, string> SpecialtyImageDict = new Dictionary<string, string> { }; //links memebers to local image paths
        //public Dictionary<string, string> IconConversionToCharIcon = new Dictionary<string, string> { }; //links html name to IconChar class name
        //public Dictionary<string, string> IconConversionToCssIcon = new Dictionary<string, string> { }; //links html name to IconChar class name
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
            return System.Drawing.Image.FromStream(await imageHandler.GetImageAsync(specialtyPage.SpecialtiesList[index]));
        }

        public void AddImage(string path, int index)
        {
            imageHandler.AddImage(specialtyPage.SpecialtiesList[index], path);
        }


        //public void AddSpecialtyImage(string fileName, int editingSpecialtyIndex)
        //{
        //    SpecialtyImageDict[specialtyPage.SpecialtiesList.ElementAt(editingSpecialtyIndex).Name] = fileName;
        //    string dirName = new DirectoryInfo(fileName).Name;
        //    string url = "https://index-webapp.s3.amazonaws.com/img/specialtyimages/" + dirName;
        //    specialtyPage.SpecialtiesList.ElementAt(editingSpecialtyIndex).Image = url;
        //}

        //public async Task<MemoryStream> LoadSpecialtyImageHandlerAsync(string name)
        //{
        //    Specialty s = specialtyPage.SpecialtiesList.First(item => item.Name == name);

        //    if (s.Image == "")
        //    {
        //        return null;
        //    }
        //    if (SpecialtyImageDict.ContainsKey(name))
        //    {
        //        return LoadImageLocal(SpecialtyImageDict[name]);
        //    }
        //    else
        //    {
        //        return await LoadImageAPI(s.Image);
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


        public int SpecialtyCount()
        {
            return specialtyPage.SpecialtiesList.Count;
        }

        public Task<HttpResponseMessage> Upload()
        {
            // put all of the local images
            foreach (string key in SpecialtyImageDict.Keys)
            {
                indexClient.PutImageAsync(SpecialtyImageDict[key], "specialtyimages");
            }

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
    }
}
