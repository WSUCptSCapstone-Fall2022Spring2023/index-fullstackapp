using index_editor_app_engine;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Reflection.Metadata;
using System.Windows.Forms;

namespace index_editor_app
{
    public partial class Form1 : Form
    {
        IndexAPIClient indexClient;

        Events[] events;
        //News[] news;
        //Meetings[] meetings;

        string eventsJson;
        string newsJson;
        string meetingsJson;

        int eventsCount = 0;
        int newsCount = 0;
        int meetingsCount = 0;

        int edtingEventIndex;
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_LoadAsync(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            //create instance of client
            this.indexClient = new IndexAPIClient();

            //test connection to API and get data
            if (await TestConnection())
            {
                this.eventsJson = await indexClient.GetDocument();
                this.events = JsonConvert.DeserializeObject<Events[]>(eventsJson);
                this.eventsCount = events.Count();
                InitializeDataGrid();
            }
        }
        public async Task<bool> TestConnection()
        {
            bool test = await indexClient.TestConnection();
            if (test)
            {
                return true;
            }
            System.Windows.Forms.MessageBox.Show("Connection fialed!" + "\n" + "See \"some ducument.txt\" for help");
            return false;
        }

        public void InitializeDataGrid()
        {
            //clear the datagrid
            this.dataGridView1.CancelEdit();
            this.dataGridView1.Columns.Clear();
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.DataSource = null;

            //add event headers
            this.dataGridView1.Columns.Add("title", "Title");
            this.dataGridView1.Columns.Add("time", "Creation Date");
            DataGridViewColumn TitleColumn = dataGridView1.Columns[0];
            DataGridViewColumn DateColumn = dataGridView1.Columns[1];
            DateColumn.Width = 70;
            TitleColumn.Width = 565;


            //add row index
            this.dataGridView1.Rows.Add(events.Count());
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                row.HeaderCell.Value = string.Format("{0}", row.Index + 1);
            }

            //add event data
            for (int i = 0; i < eventsCount; i++)
            {
                dataGridView1[0, i].Value = events[i].Title;
                //dataGridView1[1, i].Value = events[i].Time; 
                dataGridView1[1, i].Value = DateTime.Now.ToString("MM/dd/yyyy");
            }

            //add edit button
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn);
            btn.Width = 60;
            btn.HeaderText = "Edit";//column header
            btn.Text = "Edit";
            btn.Name = "Edit";
            btn.UseColumnTextForButtonValue = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            descriptionBox1.Text = e.ColumnIndex.ToString();

            if (e.ColumnIndex == 2)
            {
                //temporary image until functionality is added
                pictureBox1.ImageLocation = "C:/Users/josh/Desktop/testimage.png";

                //events.json, need to add creationDate, start and end time
                this.edtingEventIndex = e.RowIndex;
                dateTimePicker1.Text = DateTime.Now.ToString();

                descriptionBox1.Text = events[edtingEventIndex].Description;
                LinktextBox.Text = events[edtingEventIndex].Link;

            }
        }



        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to delete this item ??", "Confirm Delete!!", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                descriptionBox1.Text = "Deleted" + events[edtingEventIndex].Title;
            }
            else
            {
                //textBox1.Text = "Deleted";
            }


        }



        //private async void buttonSend_Click(object sender, EventArgs e)
        //{
        //    //create index client
        //    IndexAPIClient indexClient = new IndexAPIClient();
        //    //test connection
        //    bool test = await indexClient.TestConnection();
        //    string message = "";
        //    if (test)
        //    {
        //        message = "Successfuly connected to index";
        //    }
        //    else
        //    {
        //        textBox1.Text = "failed to connect to index";
        //        return;
        //    }

        //    //read the updated string
        //    string updatedjson = textBox1.Text;
        //    string output = "[" + updatedjson.Split('[', ']')[1] + "]";
        //    textBox1.Text = output;
        //    indexClient.PutDocument(output);

        //}

        //private async void buttonGET_Click(object sender, EventArgs e)
        //{
        //    //create index client
        //    IndexAPIClient indexClient = new IndexAPIClient();
        //    //test connection
        //    bool test = await indexClient.TestConnection();
        //    string message = "";
        //    if (test)
        //    {
        //        message = "Successfuly connected to index";
        //    }
        //    else
        //    {
        //        textBox1.Text = "failed to connect to index";
        //        return;
        //    }

        //    //GetEvents
        //    string eventsJson = await indexClient.GetDocument();

        //    //store json events data as a class
        //    Events[] events = JsonConvert.DeserializeObject<Events[]>(eventsJson);

        //    textBox1.Text = message + Environment.NewLine + "Current events" + Environment.NewLine + eventsJson;
        //}
    }
}