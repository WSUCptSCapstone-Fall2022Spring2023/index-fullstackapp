using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace index_editor_app_engine
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class MembersPage
    {
        [JsonProperty("pageDescription")]
        public string PageDescription { get; set; }

        [JsonProperty("applicationLink")]
        public string ApplicationLink { get; set; }

        [JsonProperty("boardMembers")]
        public List<BoardMember> BoardMembers { get; set; }
    }

    public partial class BoardMember
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("bioLink")]
        public string BioLink { get; set; }

        [JsonProperty("bioDescription")]
        public string BioDescription { get; set; }

        [JsonProperty("bioQuote")]
        public string BioQuote { get; set; }

        [JsonProperty("employeeSince")]
        public string EmployeeSince { get; set; }

        [JsonProperty("officeLocation")]
        public string OfficeLocation { get; set; }

        [JsonProperty("officeLink")]
        public string OfficeLink { get; set; }

        [JsonProperty("specialties")]
        public List<MemberSpecialty> Specialties { get; set; }

        [JsonProperty("created_on")]
        public string CreatedOn { get; set; }
    }

    public partial class MemberSpecialty
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }
    }

    public partial class MembersPage
    {
        public static MembersPage FromJson(string json) => JsonConvert.DeserializeObject<MembersPage>(json, index_editor_app_engine.Converter.Settings);
    }

    public static class MembersSerialize
    {
        public static string ToJson(this MembersPage self) => JsonConvert.SerializeObject(self, index_editor_app_engine.Converter.Settings);
    }

    internal static class MembersConverter
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

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
