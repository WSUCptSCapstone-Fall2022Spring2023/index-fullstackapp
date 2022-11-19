using index_editor_app_engine;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Reflection.Metadata;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace index_editor_app
{
    public partial class Form1 : Form
    {
        IndexAPIClient indexClient;
        EventsHandler eventsHandler;
        List<Events> events;

        int eventsCount = 0;
        int edtingEventIndex = -1;
        int editingFlag = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_LoadAsync(object sender, EventArgs e)
        {
            //tabControl1.TabPages.Remove(tabPage1);///////////////////////////////////////////////////////////////////////////////////////////////


            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            //Create APIClient, eventsHandler and test connection
            this.indexClient = new IndexAPIClient();
            if (await TestConnection())
            {
                eventsHandler = new EventsHandler(await indexClient.GetDocument(), indexClient);
                events = eventsHandler.events;
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
            titleColumn.Width = 411;
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


        //Edit button clicked
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.White;
            }

            if (e.ColumnIndex == 4)//edit button column
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;

                LoadEventIntoFields(e.RowIndex);
            }
        }

        //on edit click, load that even into fields
        private void LoadEventIntoFields(int eventindex)
        {
            //set global variable to index of event being edited
            this.edtingEventIndex = eventindex;

            Events editEvent = events[edtingEventIndex];
            pictureBox1.ImageLocation = "C:/Users/josh/Desktop/testimage.png";
            creationDateLabel.Text = "You created this event on: " + editEvent.CreatedOn;
            dateTimePicker1.Text = editEvent.StartDate;
            timeRangeTextBox.Text = editEvent.TimeRange;
            descriptionBox1.Text = editEvent.Description;
            LinktextBox.Text = events[edtingEventIndex].Link;
            titleTextBox.Text = editEvent.Title;
            editingEventNumberLabel.Text = "You are editing event #" + (edtingEventIndex + 1);
        }

        //when the delete button is clicked
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (edtingEventIndex == -1)
            {
                DisplayNoEventSelectedMessage();
                return;
            }
            var confirmResult = MessageBox.Show("Are you sure to delete this item?", "Confirm Delete!", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                events.RemoveAt(edtingEventIndex);
                edtingEventIndex = -1;
                InitializeDataGrid();
            }
        }

        private void addImageButton_Click(object sender, EventArgs e)
        {
            if (edtingEventIndex == -1)
            {
                DisplayNoEventSelectedMessage();
                return;
            }

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
            Events newEvent = new Events();
            newEvent.CreatedOn = DateTime.Now.ToString();
            newEvent.Title = "New event created! Edit me!";
            events.Insert(0, newEvent);
            InitializeDataGrid();
            edtingEventIndex = 0;
            LoadEventIntoFields(0);
            dataGridView1.Rows[0].DefaultCellStyle.BackColor = Color.Yellow;
        }

        private void validateChangesButton_Click(object sender, EventArgs e)
        {
            if (edtingEventIndex == -1)
            {
                DisplayNoEventSelectedMessage();
                return;
            }
            string status = EventsHandler.CheckEvent(titleTextBox.Text, descriptionBox1.Text, timeRangeTextBox.Text, dateTimePicker1.Value.ToString(), LinktextBox.Text, pictureBox1.ImageLocation);
            if (status == string.Empty)
            {
                System.Windows.Forms.MessageBox.Show("No problems found");
            }
            else
            {
                String message = "There are propblems with the event!" + Environment.NewLine + status;
                System.Windows.Forms.MessageBox.Show(message);
            }

        }

        public void DisplayNoEventSelectedMessage()
        {
            System.Windows.Forms.MessageBox.Show("No event selected! Edit an event or click create new.");
        }

        //clears all the input fields
        private void clearInputFields()
        {
            descriptionBox1.Text = "";
            titleTextBox.Text = "";
            timeRangeTextBox.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            LinktextBox.Text = "";
        }

        private void titleTextBox_TextChanged(object sender, EventArgs e)
        {
            if (edtingEventIndex != -1)
            {
                events[edtingEventIndex].Title = titleTextBox.Text;
                dataGridView1[1, edtingEventIndex].Value = titleTextBox.Text;
            }
            else
            {
                DisplayNoEventSelectedMessage();
            }
        }

        private void descriptionBox1_TextChanged(object sender, EventArgs e)
        {
            if (edtingEventIndex != -1)
            {
                events[edtingEventIndex].Description = descriptionBox1.Text;
                dataGridView1[3, edtingEventIndex].Value = descriptionBox1.Text;
            }
            else
            {
                DisplayNoEventSelectedMessage();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)//start date
        {
            if (edtingEventIndex != -1)
            {
                events[edtingEventIndex].StartDate = dateTimePicker1.Text;
                dataGridView1[2, edtingEventIndex].Value = dateTimePicker1.Value.ToShortDateString();
            }
            else
            {
                DisplayNoEventSelectedMessage();
            }
        }

        private void timeRangeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (edtingEventIndex != -1)
            {
                events[edtingEventIndex].TimeRange = timeRangeTextBox.Text;
            }
            else
            {
                DisplayNoEventSelectedMessage();
            }
        }

        private void LinktextBox_TextChanged(object sender, EventArgs e)
        {
            if (edtingEventIndex != -1)
            {
                events[edtingEventIndex].Link = LinktextBox.Text;
            }
            else
            {
                DisplayNoEventSelectedMessage();
            }
        }

        private void update_Website_Button_Click(object sender, EventArgs e)
        {

            if (textBoxConfirmUpdate.Text != "confirm")
            {
                System.Windows.Forms.MessageBox.Show("Please type \"Confirm\" to update");
                return;
            }

            eventsHandler.events = this.events;
            string validation = eventsHandler.ValidatePut();

            if (validation == string.Empty)
            {
                System.Windows.Forms.MessageBox.Show("The website is being updated...");
                var httpreseponse = eventsHandler.PutEventsJson();
                System.Windows.Forms.MessageBox.Show(httpreseponse.ToString());
            }
            else
            {
                var confirmResult = MessageBox.Show("Would you like to continue with the following errors?" + Environment.NewLine + validation, "Errors found!",  MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    System.Windows.Forms.MessageBox.Show("The website is being updated...");
                    var httpreseponse = eventsHandler.PutEventsJson();
                    System.Windows.Forms.MessageBox.Show(httpreseponse.ToString());
                }
            }
            textBoxConfirmUpdate.ForeColor = Color.Gray;
            textBoxConfirmUpdate.Text = "Type \"confirm\"";
            button1.BackColor = Color.Gray;

        }


        private void Confirm_TextBox_Enter(object sender, EventArgs e)
        {
            if (textBoxConfirmUpdate.Text == "Type \"confirm\"")
            {
                textBoxConfirmUpdate.ForeColor = Color.Black;
                textBoxConfirmUpdate.Text = "";
            }
        }

        private void Confirm_TextBox_Leave(object sender, EventArgs e)
        {
            if (textBoxConfirmUpdate.Text.Length == 0)
            {
                textBoxConfirmUpdate.ForeColor = Color.Gray;
                textBoxConfirmUpdate.Text = "Type \"confirm\"";
            }
            else
            {

            }
        }

        private void textBoxConfirmUpdate_TextChanged(object sender, EventArgs e)
        {
            if (textBoxConfirmUpdate.Text == "confirm")
            {
                button1.BackColor = Color.Green;
            }
        }













        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {

            Graphics g = e.Graphics;
            Brush _textBrush;

            // Get the item from the collection.
            TabPage _tabPage = tabControl1.TabPages[e.Index];

            // Get the real bounds for the tab rectangle.
            Rectangle _tabBounds = tabControl1.GetTabRect(e.Index);

            if (e.State == DrawItemState.Selected)
            {

                // Draw a different background color, and don't paint a focus rectangle.
                _textBrush = new SolidBrush(Color.Red);
                g.FillRectangle(Brushes.Gray, e.Bounds);
            }
            else
            {
                _textBrush = new System.Drawing.SolidBrush(e.ForeColor);
                e.DrawBackground();
            }

            // Use our own font.
            Font _tabFont = new Font("Arial", 10.0f, FontStyle.Bold, GraphicsUnit.Pixel);

            // Draw string. Center the text.
            StringFormat _stringFlags = new StringFormat();
            _stringFlags.Alignment = StringAlignment.Center;
            _stringFlags.LineAlignment = StringAlignment.Center;
            g.DrawString(_tabPage.Text, _tabFont, _textBrush, _tabBounds, new StringFormat(_stringFlags));
        }

        private void button2_Click(object sender, EventArgs e)
        {

            var response = indexClient.PutImage("test");
            descriptionBox1.Text = response.ToString();
        }

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    tabPage1.Hide();
        //    tabControl1.TabPages.Add(tabPage1);
        //    tabPage1.Hide();
        //}








    }
}