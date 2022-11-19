using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace index_editor_app_engine
{
    public class EventsHandler
    {
        public List<Events> events;// List of Events
        public string eventsJson;  //Events json string
        public IndexAPIClient indexClient; //API client
        int count;

        public int Count { get => count; set => count = value; }

        public EventsHandler(string EventsJson, IndexAPIClient client)
        {
            this.indexClient = client;
            this.eventsJson = EventsJson;
            this.events = JsonConvert.DeserializeObject<Events[]>(eventsJson).ToList<Events>();
        }

        public async Task GetEvents() // load events into class
        {
            this.eventsJson = await indexClient.GetDocument();
            this.events = JsonConvert.DeserializeObject<Events[]>(eventsJson).ToList<Events>();
            this.count = events.Count();
            Console.WriteLine("THIS IS THE COUNT RIGHT NOW: " + count);
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
            foreach (Events e in events)
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
            Events[] updatedEvents = events.ToArray();
            string updatedEventsJsonString = JsonConvert.SerializeObject(updatedEvents);
            var httpResponse = indexClient.PutDocument(updatedEventsJsonString);
            return httpResponse;
        }

    }
}
