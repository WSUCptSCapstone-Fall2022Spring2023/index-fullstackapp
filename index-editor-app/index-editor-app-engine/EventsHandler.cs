﻿using Newtonsoft.Json;
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
            string dirName = new DirectoryInfo(path).Name;
            string url = "https://index-webapp.s3.amazonaws.com/img/eventimages/" + dirName;
            events.ElementAt(index).Image = url;
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

            // put all of the local images
            foreach (string key in ImageDict.Keys)
            {
                indexClient.PutImageAsync(ImageDict[key]);
            }

            //put the json
            Event[] updatedEvents = events.ToArray();
            string updatedEventsJsonString = JsonConvert.SerializeObject(updatedEvents);
            var httpResponse = indexClient.PutDocument(updatedEventsJsonString);
            return httpResponse;
        }

    }
}
