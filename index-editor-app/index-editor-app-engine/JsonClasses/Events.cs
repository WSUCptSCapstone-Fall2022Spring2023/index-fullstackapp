namespace index_editor_app_engine
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class EventsPage
    {
        [JsonProperty("Events")]
        public Event[] Events { get; set; }
    }

    public partial class Event
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("start_date")]
        public string StartDate { get; set; }

        [JsonProperty("editor_date_time")]
        public string EditorDateTime { get; set; }

        [JsonProperty("time_range")]
        public string TimeRange { get; set; }

        [JsonProperty("created_on")]
        public string CreatedOn { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }
    }




    public partial class EventsPage
    {
        public static EventsPage FromJson(string json) => JsonConvert.DeserializeObject<EventsPage>(json, index_editor_app_engine.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this EventsPage self) => JsonConvert.SerializeObject(self, index_editor_app_engine.Converter.Settings);
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