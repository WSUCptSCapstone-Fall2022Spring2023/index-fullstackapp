using index_editor_app_engine;
using Microsoft.Extensions.Configuration;

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

            //display test connection result and events.json
            textBox1.Text = message + Environment.NewLine + eventsJson;
        }
    }
}