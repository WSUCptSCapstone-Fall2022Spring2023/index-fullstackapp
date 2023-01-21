using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace index_editor_app_engine
{
    public class EventsHandler
    {
        public List<Event> events;// List of Events
        public List<Event> origrinalEvents;// List of Original events
        public string eventsJson;  //Events json string
        public IndexAPIClient indexClient; //API client

        public Dictionary<string, string> ImageDict = new Dictionary<string, string> { }; //links events to local image paths
        int count;

        public int Count { get => count; set => count = value; }

        public EventsHandler(string EventsJson, IndexAPIClient client)
        {
            this.indexClient = client;
            this.eventsJson = EventsJson;
            this.events = JsonConvert.DeserializeObject<Event[]>(eventsJson).ToList<Event>();
        }

        public async Task LoadEventsFromAPI() // load events into class
        {
            this.eventsJson = await indexClient.GetDocument();
            this.events = JsonConvert.DeserializeObject<Event[]>(eventsJson).ToList<Event>();
            this.count = events.Count();
            Console.WriteLine("THIS IS THE COUNT RIGHT NOW: " + count);
        }

        public Event GetEventByIndex(int index)
        {
            return events.ElementAt(index);
        }












        //returns API image given image name
        public async Task<MemoryStream> LoadImageAPI(string name)
        {
            byte[] image = await indexClient.GetImageAsync(name);
            MemoryStream ms = new MemoryStream(image, 0, image.Length);
            return ms;
        }

        //returns LOCAL image given path
        public MemoryStream LoadImageLocal(string path)
        {
            byte[] image = File.ReadAllBytes(path);
            MemoryStream ms = new MemoryStream(image, 0, image.Length);
            return ms;
        }

        //returns either local or API image
        public async Task<MemoryStream> LoadImageHandlerAsync(string createdOn)
        {
            Event e = events.Find(e => e.CreatedOn == createdOn);

            if (e.Image == "")
            {
                return null;
            }

            if (ImageDict.ContainsKey(createdOn))
            {
                return LoadImageLocal(ImageDict[createdOn]);
            }
            else
            {
                return await LoadImageAPI(e.Image);
            }
        }

        //add local image to dict. key = event.createdOn
        public void AddImage(string path, int index)
        {
            ImageDict[events.ElementAt(index).CreatedOn] = path;
        }



        public bool HasLocalImage(string createdOn)
        {
            if (ImageDict.ContainsKey(createdOn))
            {
                return true;
            }
            else
            {
                return false;
            }
        }













        public void DeleteEvent(int index)
        {
            this.events.RemoveAt(index);
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
            int size = events.Count();

            events.Insert(0, newEvent);
        }

        public int GetEventCount()
        {
            return events.Count();
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
            foreach (Event e in events)
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



        //from ImageDict, upload images
        public void PutImages()
        {
            //key = event index
            foreach (string key in ImageDict.Keys)
            {

            }
        }

        //Sets the "image" field to uploaded image name
        public void LinkImages()
        {

        }


        public Task<HttpResponseMessage> PutEventsJson()
        {
            //upload images
            //add image names to events

            List<string> tempImages = new List<string>
            {
                "https://dacnw.org/wp-content/uploads/2022/10/Screen-Shot-2022-10-28-at-9.57.41-AM-338x225.png",
                "https://dacnw.org/wp-content/uploads/2022/10/Screen-Shot-2022-10-28-at-10.21.47-AM-318x225.png",
                "https://dacnw.org/wp-content/uploads/2022/10/Screen-Shot-2022-10-28-at-9.57.41-AM-338x225.png",
                "https://index-webapp.s3.us-east-1.amazonaws.com/img/eventimages/Test1.png?response-content-disposition=inline&X-Amz-Security-Token=IQoJb3JpZ2luX2VjEDsaCXVzLXdlc3QtMiJHMEUCIFy668i6k9GCBtdG6hSKP7zM2xoxWUa1E1sBKaj17udxAiEAxUQKihIsy8LK90e3X%2F292Jw5kGIFH8J9qwdlKqpQpPQq5AIIZBAAGgw4MTU5OTA5NzY3MTMiDLFiOY6kDWyqmDUkJyrBAk1qYX2fvYVY%2Bj27FX36NcFkFmyVqy4FfAIRLqWJ%2FJ4st%2FsdXKTbPbMtD8C6REVRFYODNWJFjUiduQkJPkMjO4XiAXTWfJObAT8bBzXuSU4pICkQ7b%2BUk1Kh246GQ95mD26RB9af6OsZy84GWtCYgg7DjrXu7HUnzpTtmzN5haPCW%2B52bx8u%2BMou%2FmR01bAu4%2Fh0NGAWhB0YW9wukrTaYL89wRF6kWMx9sAiyuTwmkapJAN3ORSgTzKl7srhhFPGvVcEPnUCJHZp9bkBLACO6nL%2BCPhuKlmSI773pf9ASQSgmGZGr5Dmj4%2BtFRAB1ATLE3jfJfFED1zg4dZorxQeTmg9GsIy1laQ8vUTLQp1vIw8RZNrWMwg%2BHN0ygrXrSOQ0G%2F%2FH5WkArMsJmUO2wJTavgoZtyp1hAxgy1Yx4db9BuK6TDJgs6cBjqzAh3lVFBPeXbf%2FTHPQMz1A4BBj8El8lVTq%2FMbDMTqRL5Si8SNaz88NdPta7KMJVRZujKBxgE4O7cwXbTE5HqGa54PhYudk6wE2vpLnfm4g8eBs9v1oJN8rBtyltQjfZc8knwj5ftLiYT4S7GCC4OKdm0Yqt1ur8M3UxyMMjBLsolR%2FvQi9b%2ByPnntcLPCOB5%2FN3yQMd3POm3nPEOyyS4Wz50HkctuEpFHiPQQhZy72uho%2F3uc4EK2tlcTA0a6CjMkQ9S7JoRU4E75eX4UuRKNFsusL3oQhAnFEqODImk3qZtUQ1OoYW2V2EOd9GAZcMxsdfebdoahsgE58m0cSEbk0ArUMWIG%2FfjSq2j8Lkmalb9%2Fa6fmMQkfq88ZAN9CxWmCWwBppKKS6eIeKSxmxSMYV2%2BbvgQ%3D&X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Date=20221209T193052Z&X-Amz-SignedHeaders=host&X-Amz-Expires=300&X-Amz-Credential=ASIA337G2GDE7VHPLBWN%2F20221209%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Signature=ee09cb33f8ce6c7582794fcfc48fb0f243878dbbe1799131125172c667b9df4d",
                "https://index-webapp.s3.us-east-1.amazonaws.com/img/eventimages/Test3.png?response-content-disposition=inline&X-Amz-Security-Token=IQoJb3JpZ2luX2VjEDsaCXVzLXdlc3QtMiJHMEUCIFy668i6k9GCBtdG6hSKP7zM2xoxWUa1E1sBKaj17udxAiEAxUQKihIsy8LK90e3X%2F292Jw5kGIFH8J9qwdlKqpQpPQq5AIIZBAAGgw4MTU5OTA5NzY3MTMiDLFiOY6kDWyqmDUkJyrBAk1qYX2fvYVY%2Bj27FX36NcFkFmyVqy4FfAIRLqWJ%2FJ4st%2FsdXKTbPbMtD8C6REVRFYODNWJFjUiduQkJPkMjO4XiAXTWfJObAT8bBzXuSU4pICkQ7b%2BUk1Kh246GQ95mD26RB9af6OsZy84GWtCYgg7DjrXu7HUnzpTtmzN5haPCW%2B52bx8u%2BMou%2FmR01bAu4%2Fh0NGAWhB0YW9wukrTaYL89wRF6kWMx9sAiyuTwmkapJAN3ORSgTzKl7srhhFPGvVcEPnUCJHZp9bkBLACO6nL%2BCPhuKlmSI773pf9ASQSgmGZGr5Dmj4%2BtFRAB1ATLE3jfJfFED1zg4dZorxQeTmg9GsIy1laQ8vUTLQp1vIw8RZNrWMwg%2BHN0ygrXrSOQ0G%2F%2FH5WkArMsJmUO2wJTavgoZtyp1hAxgy1Yx4db9BuK6TDJgs6cBjqzAh3lVFBPeXbf%2FTHPQMz1A4BBj8El8lVTq%2FMbDMTqRL5Si8SNaz88NdPta7KMJVRZujKBxgE4O7cwXbTE5HqGa54PhYudk6wE2vpLnfm4g8eBs9v1oJN8rBtyltQjfZc8knwj5ftLiYT4S7GCC4OKdm0Yqt1ur8M3UxyMMjBLsolR%2FvQi9b%2ByPnntcLPCOB5%2FN3yQMd3POm3nPEOyyS4Wz50HkctuEpFHiPQQhZy72uho%2F3uc4EK2tlcTA0a6CjMkQ9S7JoRU4E75eX4UuRKNFsusL3oQhAnFEqODImk3qZtUQ1OoYW2V2EOd9GAZcMxsdfebdoahsgE58m0cSEbk0ArUMWIG%2FfjSq2j8Lkmalb9%2Fa6fmMQkfq88ZAN9CxWmCWwBppKKS6eIeKSxmxSMYV2%2BbvgQ%3D&X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Date=20221209T193131Z&X-Amz-SignedHeaders=host&X-Amz-Expires=300&X-Amz-Credential=ASIA337G2GDE7VHPLBWN%2F20221209%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Signature=fd4610473090439b6f8afc052ad888b7edc7ffda83d11518e76395f4d7f046f5"
            };

            int eventCount = events.Count();
            for (int i = 0; i > eventCount; i++)
            {
                events[i].Image = tempImages[i];
            }

            Event[] updatedEvents = events.ToArray();
            string updatedEventsJsonString = JsonConvert.SerializeObject(updatedEvents);
            var httpResponse = indexClient.PutDocument(updatedEventsJsonString);
            return httpResponse;
        }

    }
}
