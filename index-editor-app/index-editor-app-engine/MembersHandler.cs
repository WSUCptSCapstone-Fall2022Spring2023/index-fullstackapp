using index_editor_app_engine.JsonClasses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace index_editor_app_engine
{
    public class MembersHandler
    {
        string memberPageString;
        public IndexAPIClient indexClient; //API client
        public MembersPage memberspage;
        public Specialties specialties;

        public Dictionary<string, string> MemberSpecialtyDict = new Dictionary<string, string> { }; //links memebers to local image paths

        public Dictionary<string, string> MemberImageDict = new Dictionary<string, string> { }; //links memebers to local image paths

        public MembersHandler(string MembersJson, string SpecialtiesJson, IndexAPIClient client)
        {
            this.indexClient = client;
            this.memberPageString = MembersJson;
            this.memberspage = JsonConvert.DeserializeObject<MembersPage>(MembersJson);
            this.specialties = JsonConvert.DeserializeObject<Specialties>(SpecialtiesJson);
            InitializeSpecialties();
        }

        public void InitializeSpecialties()
        {
            //refactoring specialties to new handler
            foreach (Specialty s in specialties.SpecialtiesList)
            {
                MemberSpecialtyDict[s.Name] = s.Link;
            }
        }



        public int GetMemberCount()
        {
            return memberspage.BoardMembers.Count();
        }

        public BoardMember GetMemberByIndex(int index)
        {
            return memberspage.BoardMembers[index];
        }

        public async Task<MemoryStream> LoadMemberImageHandlerAsync(string name)
        {
            BoardMember m = Array.Find(memberspage.BoardMembers, member => member.Name == name);

            if (m.Image == "")
            {
                return null;
            }

            if (MemberImageDict.ContainsKey(name))
            {
                return LoadImageLocal(MemberImageDict[name]);
            }
            else
            {
                return await LoadImageAPI(m.Image);
            }
        }

        //returns API image given image name
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
    }
}
