using index_editor_app_engine.JsonClasses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
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
        public IndexAPIClient indexClient;
        public MembersPage memberspage;
        private ImageHandler imageHandler;
        public Specialties specialties;
        public MembersHandler(string MembersJson, IndexAPIClient client, ImageHandler imageHandler)
        {
            this.indexClient = client;
            this.memberspage = JsonConvert.DeserializeObject<MembersPage>(MembersJson);
            this.imageHandler = imageHandler;
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
        public async Task<System.Drawing.Image> GetImageAsync(int index)
        {
            return System.Drawing.Image.FromStream(await imageHandler.GetImageAsync(memberspage.BoardMembers[index].Image));
        }
        public void AddImage(string path, int index)
        {
            memberspage.BoardMembers[index].Image = path;
        }

        public Task<HttpResponseMessage> UpdateMembersPage()
        {
            imageHandler.UploadMemberImages(memberspage);

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

        public string GetJsonString()
        {
            MembersPage updatedMembersPage = memberspage;
            string updatedMembersPageString = JsonConvert.SerializeObject(updatedMembersPage);
            return updatedMembersPageString;
        }
        public List<string> GetImageList()
        {
            List<string> urls = new List<string>();
            foreach (BoardMember b in memberspage.BoardMembers)
            {
                urls.Add(b.Image);
            }
            return urls;
        }

        public bool CheckSchema(out IList<string> errors)
        {
            // Get the Schema
            string schemaFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JsonSchemas", "membersSchema.json");
            string schemaContent = File.ReadAllText(schemaFilePath);
            JSchema schema = JSchema.Parse(schemaContent);

            // get the current json
            JObject json = JObject.Parse(GetJsonString());

            return json.IsValid(schema, out errors);
        }
    }
}
