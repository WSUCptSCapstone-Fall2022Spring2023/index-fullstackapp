using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace index_editor_app_engine
{
    public class EventPage
    {
        string title;
        string image;
        string link;
        string description;
        string timeRange;
        string startDate;
        DateTime creationDate;

        public EventPage()
        {

        }

        public DateTime CreationDate { get => creationDate; set => creationDate = value; }
        public string StartDate { get => startDate; set => startDate = value; }
        public string TimeRange { get => timeRange; set => timeRange = value; }
        public string Description { get => description; set => description = value; }
        public string Link { get => link; set => link = value; }
        public string Image { get => image; set => image = value; }
        public string Title { get => title; set => title = value; }




    }
}
