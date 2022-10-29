using index_editor_app_engine;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Reflection.Metadata;
using System.Windows.Forms;

namespace index_editor_app
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_LoadAsync(object sender, EventArgs e)
        {


        }

        private async void buttonSend_Click(object sender, EventArgs e)
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
                textBox1.Text = "failed to connect to index";
                return;
            }

            //read the updated string
            string updatedjson = textBox1.Text;
            string output = "[" + updatedjson.Split('[', ']')[1] + "]";
            textBox1.Text = output;
            indexClient.PutDocument(output);

        }

        private async void buttonGET_Click(object sender, EventArgs e)
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
                textBox1.Text = "failed to connect to index";
                return;
            }

            //GetEvents
            string eventsJson = await indexClient.GetDocument();

            //store json events data as a class
            Events[] events = JsonConvert.DeserializeObject<Events[]>(eventsJson);

            textBox1.Text = message + Environment.NewLine + "Current events" + Environment.NewLine + eventsJson;
        }
    }
}