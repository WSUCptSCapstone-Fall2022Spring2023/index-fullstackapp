using index_editor_app_engine;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Reflection.Metadata;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace index_editor_app
{
    public partial class Form1 : Form
    {
        IndexAPIClient indexClient;

        List<Events> events;
        //News[] news;
        //Meetings[] meetings;

        string eventsJson;
        string newsJson;
        string meetingsJson;

        int eventsCount = 0;
        int newsCount = 0;
        int meetingsCount = 0;

        int edtingEventIndex = -1;
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
                this.events = JsonConvert.DeserializeObject<Events[]>(eventsJson).ToList<Events>();
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
            this.dataGridView1.Columns.Add("creation date", "Creation Date");
            this.dataGridView1.Columns.Add("title", "Title");
            this.dataGridView1.Columns.Add("start date", "Start date");
            this.dataGridView1.Columns.Add("description", "Description");
            DataGridViewColumn creationDatecolumn = dataGridView1.Columns[0];
            DataGridViewColumn titleColumn = dataGridView1.Columns[1];
            DataGridViewColumn startDateColumn = dataGridView1.Columns[2];
            DataGridViewColumn descriptionColumn = dataGridView1.Columns[3];
            creationDatecolumn.Width = 130;
            titleColumn.Width = 500;
            startDateColumn.Width = 80;
            descriptionColumn.Width = 638;

            //add row index
            this.eventsCount = events.Count();
            this.dataGridView1.Rows.Add(eventsCount);
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                row.HeaderCell.Value = string.Format("{0}", row.Index + 1);
            }

            //add event data
            for (int i = 0; i < eventsCount; i++)
            {
                dataGridView1[0, i].Value = events[i].CreatedOn;
                dataGridView1[1, i].Value = events[i].Title;
                dataGridView1[2, i].Value = events[i].StartDate;
                dataGridView1[3, i].Value = events[i].Description;
            }

            //add edit button
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn);
            btn.Width = 60;
            btn.HeaderText = "Edit";//column header
            btn.Text = "Edit";
            btn.Name = "Edit";
            btn.UseColumnTextForButtonValue = true;
            editingEventNumberLabel.Text = "You are editing event #" + "(no event selected)";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 4)
            {
                LoadEventIntoFields(e.RowIndex);
            }
        }

        private void LoadEventIntoFields(int eventindex)
        {
            InitializeDataGrid();
            clearInputFields();
            if (eventindex == -1)
            {
                editingEventNumberLabel.Text = "You are editing event #" + "(no event selected)";
                return;
            }
            //set global variable to index of event being edited
            this.edtingEventIndex = eventindex;
            Events editEvent = events[edtingEventIndex];

            //temporary image until functionality is added
            pictureBox1.ImageLocation = "C:/Users/josh/Desktop/testimage.png";

            //load selected event data into editing boxes
            creationDateLabel.Text = "You created this event on: " + editEvent.CreatedOn;
            dateTimePicker1.Text = editEvent.StartDate;
            timeRangeTextBox.Text = editEvent.TimeRange;
            descriptionBox1.Text = editEvent.Description;
            LinktextBox.Text = events[edtingEventIndex].Link;
            titleTextBox.Text = editEvent.Title;
            editingEventNumberLabel.Text = "You are editing event #" + (edtingEventIndex + 1);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (edtingEventIndex == -1)
            {
                System.Windows.Forms.MessageBox.Show("No event selected.");
                return;
            }
            var confirmResult = MessageBox.Show("Are you sure to delete this item ??", "Confirm Delete!!", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {

                events.RemoveAt(edtingEventIndex);
                edtingEventIndex = -1;
                LoadEventIntoFields(edtingEventIndex);
            }

        }

        private void addImageButton_Click(object sender, EventArgs e)
        {
            this.openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*", //change to images
                FilterIndex = 2,
                RestoreDirectory = true,
            };

            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var fs = this.openFileDialog1.OpenFile();
                StreamReader reader = new(fs);
                this.LoadImage(reader);
            }

        }

        private void LoadImage(StreamReader reader)
        {
            //adding images coming soon.
        }

        private void CreateEvent_Click(object sender, EventArgs e)
        {
            clearInputFields();
            Events newEvent = new Events();
            newEvent.CreatedOn = DateTime.Now.ToString();
            newEvent.Title = "New event created! Edit me!";
            events.Insert(0, newEvent);
            LoadEventIntoFields(0);
        }

        private void confirmChangesButton_Click(object sender, EventArgs e)
        {
            //load entered data into the event being edited
            events[edtingEventIndex].Description = descriptionBox1.Text;
            events[edtingEventIndex].Title = titleTextBox.Text;
            events[edtingEventIndex].TimeRange = timeRangeTextBox.Text;
            events[edtingEventIndex].StartDate = dateTimePicker1.Value.ToString();
            events[edtingEventIndex].Image = "functionality incomplete";

            LoadEventIntoFields(edtingEventIndex);
        }

        private void clearInputFields()
        {
            descriptionBox1.Text = "";
            titleTextBox.Text = "";
            timeRangeTextBox.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            LinktextBox.Text = "";

        }
    }
}