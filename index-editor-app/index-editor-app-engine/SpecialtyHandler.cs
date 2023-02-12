using index_editor_app_engine.JsonClasses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace index_editor_app_engine
{
    public class SpecialtyHandler
    {
        public Specialties specialtyPage;
        public Dictionary<string, string> MemberSpecialtyDict = new Dictionary<string, string> { }; //links memebers to local image paths
        public IndexAPIClient indexClient; //API client
        public Dictionary<string, string> SpecialtyImageDict = new Dictionary<string, string> { }; //links memebers to local image paths


        public SpecialtyHandler(string SpecialtiesJson, IndexAPIClient client)
        {
            this.indexClient = client;
            this.specialtyPage = JsonConvert.DeserializeObject<Specialties>(SpecialtiesJson);
        }

        public void AddSpecialtyImage(string fileName, int editingSpecialtyIndex)
        {
            SpecialtyImageDict[specialtyPage.SpecialtiesList.ElementAt(editingSpecialtyIndex).Name] = fileName;
            string dirName = new DirectoryInfo(fileName).Name;
            string url = "https://index-webapp.s3.amazonaws.com/img/specialtyimages/" + dirName;
            specialtyPage.SpecialtiesList.ElementAt(editingSpecialtyIndex).Image = url;
        }

        public async Task<MemoryStream> LoadSpecialtyImageHandlerAsync(string name)
        {
            Specialty s = specialtyPage.SpecialtiesList.First(item => item.Name == name);

            if (s.Image == "")
            {
                return null;
            }
            if (SpecialtyImageDict.ContainsKey(name))
            {
                return LoadImageLocal(SpecialtyImageDict[name]);
            }
            else
            {
                return await LoadImageAPI(s.Image);
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
    }
}
