using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace index_editor_app_engine
{
    public class EventsHandler
    {
        public Events[] events;// = JsonConvert.DeserializeObject<Events[]>(eventsJson);
        IndexAPIClient indexClient;
        public EventsHandler()
        {
            indexClient = new IndexAPIClient();
        }

        public async Task GetEvents()
        {
            string eventsJson = await indexClient.GetDocument();
            this.events = JsonConvert.DeserializeObject<Events[]>(eventsJson);
        }

        public static Events CreateNewEvent(string title, string description, string link, string image, string time)
        {
            Events newevent = new Events();
            newevent.Title = title;
            newevent.Description = description; 
            newevent.Link = link;
            newevent.Image = image;
            newevent.TimeRange = time;

            return newevent;
        }

        public Events GetEventByName(string name)
        {
            Events first = Array.Find(events, element => element.Title.ToLower() == name);
            return first;
        }

        public void UpdateEventByName(string name, string title, string description, string link, string image, string time)
        {
            foreach (Events e in events)
            {
                if (e.Title.ToLower() == name.ToLower())
                {
                    e.Title = title;
                    e.Description = description;
                    e.Link = link;
                    e.Image = image;
                    e.TimeRange = time;
                }
            }
        }




        //TODO sending events
        //serialize updated Event object
        //check against schema
        //call PUT

    }
}
