using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace index_editor_app_engine.JsonClasses
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Specialties
    {
        [JsonProperty("specialties")]
        public List<Specialty> SpecialtiesList { get; set; }
    }

    public partial class Specialty
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("subtitle")]
        public string Subtitle { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("bulletpoints")]
        public List<string> Bulletpoints { get; set; }
    }

    public partial class Specialties
    {
        public static Specialties FromJson(string json) => JsonConvert.DeserializeObject<Specialties>(json, index_editor_app_engine.JsonClasses.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Specialties self) => JsonConvert.SerializeObject(self, index_editor_app_engine.JsonClasses.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
