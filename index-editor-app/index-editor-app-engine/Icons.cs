using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace index_editor_app_engine
{
    public class Icons
    {
        public Dictionary<string, string> IconConversionToCharIcon = new Dictionary<string, string> { };
        public Dictionary<string, string> IconConversionToCssIcon = new Dictionary<string, string> { };

        public Icons()
        {
            InitIconDict();
        }

        private void InitIconDict()
        {
            List<string> CSSIconNames = ReadIcons();
            List<string> iconCharIconNames = new List<string>();
            foreach (var name in Enum.GetNames(typeof(IconChar)))
            {
                iconCharIconNames.Add(name);
            }

            string iconCharIconName = "";
            foreach (string CSSIconName in CSSIconNames)
            {
                iconCharIconName = "";
                Boolean isMultiPart = CSSIconName.Contains('-');
                if (isMultiPart)
                {
                    string[] words = CSSIconName.Split('-');
                    for (int i = 0; i < words.Length; i++)
                    {
                        string word = words[i];
                        string upperCaseWord = char.ToUpper(word[0]) + word.Substring(1);
                        words[i] = upperCaseWord;
                    }
                    foreach (string s in words)
                    {
                        iconCharIconName += s;
                    }
                }
                else
                {
                    iconCharIconName = char.ToUpper(CSSIconName[0]) + CSSIconName.Substring(1);
                }
                if (iconCharIconNames.Contains(iconCharIconName))
                {
                    IconConversionToCharIcon[CSSIconName] = iconCharIconName;
                    IconConversionToCssIcon[iconCharIconName] = CSSIconName;
                }
            }
        }
        private List<string> ReadIcons()
        {
            List<string> icons = new List<string>();
            string path = System.IO.Path.Combine("Icons", "Icons.txt");
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    icons.Add(line);
                }
            }
            return icons;
        }

        public List<string> GetIconList()
        {
            return IconConversionToCharIcon.Values.ToList();
        }

        public string GetIcon(int index, string iconName)
        {
            if (iconName != "")
            {
                return IconConversionToCharIcon[iconName];
            }
            else
            {
                return "None";
            }
        }

        public string IconToCss(string iconName)
        {
            return String.Format("fa fa-{0}", IconConversionToCssIcon[iconName]);
        }

        //public string CssToIcon(string iconName)
        //{
        //    //return String.Format("fa fa-{0}", IconConversionToCssIcon[iconName]);
        //}

    }
}
