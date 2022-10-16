using index_editor_app_engine;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace index_editor_app
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_LoadAsync(object sender, EventArgs e)
        {
            //create index client
            IndexAPIClient indexClient = new IndexAPIClient();
            //test connection
            bool test = await indexClient.TestConnection();
            string message = "";
            if (test)
            {
                message = "Successfuly connected to index";
            }
            else
            {
                message = "failed to connect to index";
            }

            //GetEvents
            string eventsJson = await indexClient.GetEvents();


            //store json events data as a class
            Events[] events = JsonConvert.DeserializeObject<Events[]>(eventsJson);
            //make changes to the event
            events[0].Time = "Updated time";
            events[0].Image = "Updated image";
            
            //convert back to string json
            string document = JsonConvert.SerializeObject(events);


            //Put request to send the new updated ducment back
            indexClient.PutDocument(document);



            //Get events to varify the update
            string updatedeventsJson = await indexClient.GetEvents();


            //display test connection result and events.json
            textBox1.Text = "Before PUT" + Environment.NewLine + eventsJson + Environment.NewLine + Environment.NewLine + "After  PUT" + Environment.NewLine + updatedeventsJson;

        }
    }
}