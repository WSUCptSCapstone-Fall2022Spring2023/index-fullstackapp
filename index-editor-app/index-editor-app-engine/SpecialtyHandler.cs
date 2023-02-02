using index_editor_app_engine.JsonClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace index_editor_app_engine
{
    public class SpecialtyHandler
    {
        public Specialties specialties;
        public Dictionary<string, string> MemberSpecialtyDict = new Dictionary<string, string> { }; //links memebers to local image paths

        SpecialtyHandler(string SpecialtiesJson, IndexAPIClient client)
        {
            //refactoring specialties out of MembersHandler to this new handler in the future
        }


    }
}
