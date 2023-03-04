namespace index_editor_app_engine.JsonClasses
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class NewsPage
    {
        [JsonProperty("pageTitle")]
        public string PageTitle { get; set; }

        [JsonProperty("pageSubtitle")]
        public string PageSubtitle { get; set; }

        [JsonProperty("newsItems")]
        public List<NewsItem> NewsItems { get; set; }
    }

    public partial class NewsItem
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("editor_date_time")]
        public string EditorDateTime { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("newsLink")]
        public string NewsLink { get; set; }

        [JsonProperty("postedBy")]
        public string PostedBy { get; set; }
    }

    public partial class NewsPage
    {
        public static NewsPage FromJson(string json) => JsonConvert.DeserializeObject<NewsPage>(json, index_editor_app_engine.JsonClasses.Converter.Settings);
    }

    public static class NewsSerialize
    {
        public static string ToJson(this NewsPage self) => JsonConvert.SerializeObject(self, index_editor_app_engine.JsonClasses.Converter.Settings);
    }

    internal static class NewsConverter
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