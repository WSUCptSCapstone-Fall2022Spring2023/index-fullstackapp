using index_editor_app_engine.JsonClasses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace index_editor_app_engine
{
    public static class SaveLoad
    {
        static string[] requiredFiles = { "Events.json", "Members.json", "News.json", "Specialties.json", "Resources.json" };

        public static void Save(string path, string events, string members, string news, string specialties, string resources)
        {
            JObject Events = JObject.Parse(events);
            JObject Members = JObject.Parse(members);
            JObject News = JObject.Parse(news);
            JObject Specialties = JObject.Parse(specialties);
            JObject Resources = JObject.Parse(resources);
            string folderName = "\\" + "IndexBackup" + DateTime.Now.ToString("_MMddyyyy_HHmmss");
            path = path + folderName;
            Directory.CreateDirectory(path);

            File.WriteAllText(path + "\\Events.json", Events.ToString());
            File.WriteAllText(path + "\\Members.json", Members.ToString());
            File.WriteAllText(path + "\\News.json", News.ToString());
            File.WriteAllText(path + "\\Specialties.json", Specialties.ToString());
            File.WriteAllText(path + "\\Resources.json", Resources.ToString());
        }

        public static void Load(string path, EditorInstances instacnes)
        {
            List<string> missingFiles = new List<string>();
            Dictionary<string, string> fileContents = new Dictionary<string, string>();

            foreach (string file in requiredFiles)
            {
                string filePath = Path.Combine(path, file);
                if (!File.Exists(filePath))
                {
                    missingFiles.Add(file);
                }
                else
                {
                    string fileContent = File.ReadAllText(filePath);
                    fileContents.Add(file, fileContent);
                }
            }

            if (missingFiles.Any())
            {
                string errorMessage = "The following files are missing: " + string.Join(", ", missingFiles);
                throw new Exception(errorMessage);
            }
            else
            {
                instacnes.eventsHandler.eventsPage = JsonConvert.DeserializeObject<EventsPage>(fileContents["Events.json"]);
                instacnes.membersHandler.memberspage = JsonConvert.DeserializeObject<MembersPage>(fileContents["Members.json"]);
                instacnes.newsHandler.newsPage = JsonConvert.DeserializeObject<NewsPage>(fileContents["News.json"]);
                instacnes.specialtiesHandler.specialtyPage = JsonConvert.DeserializeObject<Specialties>(fileContents["Specialties.json"]);
                instacnes.resourcesHandler.resourcesPage = JsonConvert.DeserializeObject<ResourcesPage>(fileContents["Resources.json"]);
                //instacnes.newsHandler
                //instacnes.specialtiesHandler

            }
        }
    }
}
