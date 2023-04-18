using index_editor_app_engine.JsonClasses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace index_editor_app_engine
{
    public class EventsHandler
    {
        public EventsPage eventsPage;
        public IndexAPIClient indexClient;
        private ImageHandler imageHandler;

        public EventsHandler(string eventsJson, IndexAPIClient client, ImageHandler imageHandler)
        {
            this.indexClient = client;
            this.eventsPage = JsonConvert.DeserializeObject<EventsPage>(eventsJson);
            this.imageHandler = imageHandler;
        }

        public Event GetEventByIndex(int index)
        {
            return eventsPage.Events[index];
        }

        public async Task<System.Drawing.Image> GetImageAsync(int index)
        {
            return System.Drawing.Image.FromStream(await imageHandler.GetImageAsync(eventsPage.Events[index].Image));
        }

        public void AddImage(string path, int index)
        {
            eventsPage.Events[index].Image = path;
        }
        public void DeleteEvent(int index)
        {
            eventsPage.Events.RemoveAt(index);
        }

        public void CreateEvent()
        {
            Event newEvent = new Event();
            newEvent.CreatedOn = DateTime.Now.ToString();
            newEvent.Title = "New event created! Edit me!";
            newEvent.StartDate = "";
            newEvent.TimeRange = "";
            newEvent.Description = "New event created! Edit me!";
            newEvent.Image = "";

            eventsPage.Events.Add(newEvent);
        }

        public int GetEventCount()
        {
            return eventsPage.Events.Count();
        }

        public static string CheckEvent(string title, string des, string timeRange, string startdate, string link, string image)
        {
            string result = string.Empty;

            if (title == string.Empty || title == "New event created! Edit me!" || title == null)
            {
                result += "Missing Title" + Environment.NewLine;
            }
            else
            {
                string[] titleList = title.Split(' ');
                foreach (string word in titleList)
                {
                    if (!Char.IsUpper(word[0]))
                    {
                        result += "Title has lowercase words" + Environment.NewLine;
                        break;
                    }
                }
            }

            if (des == string.Empty || des == null)
            {
                result += "Missing description" + Environment.NewLine;
            }


            if (timeRange == string.Empty || timeRange == null)
            {
                result += "Missing time range" + Environment.NewLine;
            }


            if (startdate == string.Empty || startdate == null)
            {
                result += "Missing startdate" + Environment.NewLine;
            }


            if (link == string.Empty || link == null)
            {
                result += "Missing URL link" + Environment.NewLine;
            }


            if (image == string.Empty || image == "C:/Users/josh/Desktop/testimage.png" || image == null)
            {
                result += "Missing image link" + Environment.NewLine;
            }

            return result;
        }
        public string ValidatePut()
        {
            string basicErros = string.Empty;
            string otherErrors = string.Empty;
            int i = 1;
            foreach (Event e in eventsPage.Events)
            {
                string basicValidation = EventsHandler.CheckEvent(e.Title, e.Description, e.TimeRange, e.StartDate, e.Link, e.Image);
                if (basicValidation != string.Empty)
                {
                    basicErros += "event #" + i + Environment.NewLine +  basicValidation;
                }
                //add more checks later for otherchecks
            }

            return basicErros;
        }

        public Task<HttpResponseMessage> PutEventsJson()
        {
            // update datetime to have ordinal suffix
            foreach (Event e in eventsPage.Events)
            {
                e.StartDate = AddOrdinalSuffix(e.StartDate);
            }

            imageHandler.UploadEventImages(eventsPage);

            string updatedEventsJsonString = JsonConvert.SerializeObject(eventsPage);
            var httpResponse = indexClient.PutDocument(updatedEventsJsonString, "events");
            return httpResponse;
        }

        public string AddOrdinalSuffix(string date)
        {
            char number1 = date[date.Length - 2];
            char number2 = date[date.Length - 1];
            char[] chars = { number1, number2 };
            string number = new string(chars);

            if (Char.IsDigit(number1) & Char.IsDigit(number2))
            {
                string ordinalSuffix = ordinal_suffix_of(int.Parse(number));
                date = date.Substring(0, date.Length-2) +  ordinalSuffix;
            }
            else if (Char.IsDigit(number2))
            {
                string ordinalSuffix = ordinal_suffix_of(int.Parse(number2.ToString()));
                date = date.Substring(0, date.Length - 1) + ordinalSuffix;
            }
            return date;
        }

        public string ordinal_suffix_of(int i)
        {
            var j = i % 10;
            var k = i % 100;

            if (j == 1 && k != 11)
            {
                return i + "st";
            }
            if (j == 2 && k != 12)
            {
                return i + "nd";
            }
            if (j == 3 && k != 13)
            {
                return i + "rd";
            }
            return i + "th";
        }

        public string GetJsonString()
        {
            string updatedEventsJsonString = JsonConvert.SerializeObject(eventsPage);
            return updatedEventsJsonString;
        }

        public List<string> GetImageList()
        {
            List<string> urls = new List<string>();
            foreach(Event e in eventsPage.Events)
            {
                urls.Add(e.Image);
            }
            return urls;
        }


        public bool CheckSchema(out IList<string> errors)
        {
            // Get the Schema
            string schemaFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"JsonSchemas", "eventsSchema.json");
            string schemaContent = File.ReadAllText(schemaFilePath);
            JSchema schema = JSchema.Parse(schemaContent);

            // get the current json
            JObject json = JObject.Parse(GetJsonString());

            return json.IsValid(schema, out errors);
        }
    }
}
