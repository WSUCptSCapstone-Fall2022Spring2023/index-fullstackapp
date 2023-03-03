using index_editor_app_engine.JsonClasses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
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

        public Dictionary<string, string> MemberImageDict = new Dictionary<string, string> { }; //links memebers to local image paths

        public MembersHandler(string MembersJson, string SpecialtiesJson, IndexAPIClient client)
        {
            this.indexClient = client;
            this.memberPageString = MembersJson;
            this.memberspage = JsonConvert.DeserializeObject<MembersPage>(MembersJson);
            this.specialties = JsonConvert.DeserializeObject<Specialties>(SpecialtiesJson);
        }

        public void CreateBoardMember()
        {
            BoardMember newMember = new BoardMember();
            newMember.BioDescription = "";
            newMember.BioQuote = "";
            newMember.CreatedOn = DateTime.Now.ToString();
            newMember.BioLink = "";
            newMember.OfficeLink = "";
            newMember.OfficeLocation = "";
            newMember.EmployeeSince = "";
            newMember.Image = "";
            newMember.Specialties = new List<MemberSpecialty>();
            memberspage.BoardMembers.Add(newMember);

        }

        public void DeleteBoardMember(int index)
        {
            memberspage.BoardMembers.RemoveAt(index);
        }

        public void AddMemberImage(string path, int index)
        {
            MemberImageDict[memberspage.BoardMembers.ElementAt(index).Name] = path;
            string dirName = new DirectoryInfo(path).Name;
            string url = "https://index-webapp.s3.amazonaws.com/img/memberimages/" + dirName;
            memberspage.BoardMembers.ElementAt(index).Image = url;
        }



        public Task<HttpResponseMessage> UpdateMembersPage()
        {
            // put all of the local images
            foreach (string key in MemberImageDict.Keys)
            {
                indexClient.PutImageAsync(MemberImageDict[key], "memberimages");
            }

            MembersPage updatedMembersPage = memberspage;
            string updatedMembersPageString = JsonConvert.SerializeObject(updatedMembersPage);
            var httpResponse = indexClient.PutDocument(updatedMembersPageString, "members");
            return httpResponse;
        }

        public void UpdateMemberSpecialties(bool checkboxValue, int specialtyIndex, int memberIndex)
        {
            if (checkboxValue == true)
            {
                MemberSpecialty s = new MemberSpecialty();
                s.Name = specialties.SpecialtiesList[specialtyIndex].Name;
                s.Link = specialties.SpecialtiesList[specialtyIndex].Link;
                memberspage.BoardMembers[memberIndex].Specialties.Add(s);
            }
            else
            {
                memberspage.BoardMembers[memberIndex].Specialties.RemoveAll(s => s.Name == specialties.SpecialtiesList[specialtyIndex].Name);
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
            BoardMember m = memberspage.BoardMembers.First(item => item.Name == name);

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
