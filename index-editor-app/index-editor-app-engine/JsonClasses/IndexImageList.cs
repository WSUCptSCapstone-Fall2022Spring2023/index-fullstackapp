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
    using System.Reflection;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class IndexImageList
    {
        [JsonProperty("events")]
        public List<string> Events { get; set; }

        [JsonProperty("members")]
        public List<string> Members { get; set; }

        [JsonProperty("news")]
        public List<string> News { get; set; }

        [JsonProperty("specialties")]
        public List<string> Specialties { get; set; }

        [JsonProperty("Resources")]
        public List<string> Resources { get; set; }

        public List<string> GetPropertyNames()
        {
            return this.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType == typeof(List<string>))
                .Select(p => p.Name)
                .ToList();
        }
    }


    public partial class IndexImageList
    {
        public static IndexImageList FromJson(string json) => JsonConvert.DeserializeObject<IndexImageList>(json, index_editor_app_engine.Converter.Settings);
    }

    public static class SerializeImageList
    {
        public static string ToJson(this IndexImageList self) => JsonConvert.SerializeObject(self, index_editor_app_engine.Converter.Settings);
    }

    internal static class ConverterImageList
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


