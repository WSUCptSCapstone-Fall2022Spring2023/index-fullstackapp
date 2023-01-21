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
        //List<Event> events;

        int eventsCount = 0;
        int editingEventIndex = -1;


        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize the app
        /// </summary>
        private async void Form1_LoadAsync(object sender, EventArgs e)
        {
            InitializeMembersDataGrid();



            //tabControl1.TabPages.Remove(tabPage1);


            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;


            this.indexClient = new IndexAPIClient();
            if (await TestConnection())
            {
                eventsHandler = new EventsHandler(await indexClient.GetDocument(), indexClient);
                InitializeEventsDataGrid();
            }
        }



        /// <summary>
        /// Button click events
        /// </summary>

        private void EditButton_Click(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.White;
            }

            //column 4 = edit button
            if (e.ColumnIndex == 4 && e.RowIndex != -1)
            {

                //highlight the column being edited
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;

                //load events into fields
                LoadEventIntoFields(e.RowIndex);
            }
        }//Edit button clicked => LoadEventIntoFields

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (editingEventIndex == -1)
            {
                NoEventSelectedCheck();
                return;
            }

            var confirmResult = MessageBox.Show("Are you sure to delete this item?", "Confirm Delete!", MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                eventsHandler.DeleteEvent(editingEventIndex);
                clearInputFields();
                editingEventIndex = -1;
                InitializeEventsDataGrid();
            }
        }

        private void addImageButton_Click(object sender, EventArgs e)
        {
            if (editingEventIndex == -1)                                                                     //CREATE NO EVENT SELECTED FUNCTION
            {
                NoEventSelectedCheck();
                return;
            }

            this.openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",                                         //change to images
                FilterIndex = 2,
                RestoreDirectory = true,
            };

            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                eventsHandler.AddImage(openFileDialog1.FileName, editingEventIndex);
                LoadEventIntoFields(editingEventIndex);
            }
        }//add local image to event

        private void CreateEvent_Click(object sender, EventArgs e)
        {
            //create events
            eventsHandler.CreateEvent();
            //reset datagrid
            InitializeEventsDataGrid();
            //change deiting index
            //editingEventIndex = 0;
            //load new event
            //LoadEventIntoFields(0);
            //highlight
           // dataGridView1.Rows[0].DefaultCellStyle.BackColor = Color.Yellow;
        }//create an event

        private void ValidateChangesButton_Click(object sender, EventArgs e)
        {
            if (editingEventIndex == -1)
            {
                NoEventSelectedCheck();
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

        }//Display event validation

        private void Update_Website_Button_Click(object sender, EventArgs e)//Push events to website
        {

            if (textBoxConfirmUpdate.Text != "confirm")
            {
                System.Windows.Forms.MessageBox.Show("Please type \"confirm\" to update");
                return;
            }

            //get validation response
            string validation = eventsHandler.ValidatePut();

            if (validation == string.Empty)
            {
                System.Windows.Forms.MessageBox.Show("The website is being updated...");
                var httpreseponse = eventsHandler.PutEventsJson();
                System.Windows.Forms.MessageBox.Show(httpreseponse.ToString());
            }
            else
            {
                var confirmResult = MessageBox.Show("Would you like to continue with the following errors?" + Environment.NewLine + validation, "Errors found!", MessageBoxButtons.YesNo);
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




        /// <summary>
        /// utility functions
        /// </summary>
        private void clearInputFields()//clears all the input fields
        {
            descriptionBox1.Text = "";
            titleTextBox.Text = "";
            timeRangeTextBox.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            LinktextBox.Text = "";
            pictureBox1.Image = null;
        }

        private async Task LoadEventIntoFields(int eventindex)//Load event[i] into fields
        {
            this.editingEventIndex = eventindex;
            Event e = eventsHandler.GetEventByIndex(eventindex);

            // load images
            if (e.Image != "")
            {
                pictureBox1.Image = System.Drawing.Image.FromStream(await eventsHandler.LoadImageHandlerAsync(e.CreatedOn));
            }
            else
            {
                pictureBox1.Image = null;
            }

            //load start date
            if (e.StartDate != "")
            {
                dateTimePicker1.Value = DateTime.Parse(e.StartDate);
            }
            else
            {
                dateTimePicker1.Text = "";
            }

            creationDateLabel.Text = "You created this event on: " + e.CreatedOn;
            timeRangeTextBox.Text = e.TimeRange;
            descriptionBox1.Text = e.Description;
            LinktextBox.Text = e.Link;
            titleTextBox.Text = e.Title;
            editingEventNumberLabel.Text = "You are editing event #" + (editingEventIndex + 1);
        }

        public void InitializeEventsDataGrid()//Initialize based on size of event[]
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
            creationDatecolumn.Width = 100;
            titleColumn.Width = 411;
            startDateColumn.Width = 80;
            descriptionColumn.Width = 668;

            //add row index
            this.eventsCount = eventsHandler.GetEventCount();
            this.dataGridView1.Rows.Add(eventsCount);
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                row.HeaderCell.Value = string.Format("{0}", row.Index + 1);
            }

            //add event data
            for (int i = 0; i < eventsCount; i++)
            {
                Event e = eventsHandler.GetEventByIndex(i);
                dataGridView1[0, i].Value = e.CreatedOn;
                dataGridView1[1, i].Value = e.Title;
                dataGridView1[2, i].Value = e.StartDate;
                dataGridView1[3, i].Value = e.Description;
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

            //descriptionBox1.Text = eventsHandler.eventsJson;
        }

        public async Task<bool> TestConnection()//test connection to index API
        {
            bool test = await indexClient.TestConnection();
            if (test)
            {
                return true;
            }
            System.Windows.Forms.MessageBox.Show("Connection fialed!" + "\n" + "See \"some ducument.txt\" for help");
            return false;
        }







        /// <summary>
        /// fields changing events, updates event data from inputs
        /// </summary>
        private void Title_TextChanged(object sender, EventArgs e)
        {
            if (NoEventSelectedCheck()) { return; }
            Event currentEvent = eventsHandler.GetEventByIndex(editingEventIndex);
            currentEvent.Title = titleTextBox.Text;
            dataGridView1[1, editingEventIndex].Value = titleTextBox.Text;
        }

        private void Description_TextChanged(object sender, EventArgs e)
        {
            if (NoEventSelectedCheck()) { return; }
            Event currentEvent = eventsHandler.GetEventByIndex(editingEventIndex);
            currentEvent.Description = descriptionBox1.Text;
            dataGridView1[3, editingEventIndex].Value = descriptionBox1.Text;
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)//start date
        {
            if (NoEventSelectedCheck()) { return; }
            Event currentEvent = eventsHandler.GetEventByIndex(editingEventIndex);
            currentEvent.StartDate = dateTimePicker1.Text;
            dataGridView1[2, editingEventIndex].Value = dateTimePicker1.Value.ToShortDateString();
        }

        private void TimeRange_TextChanged(object sender, EventArgs e)
        {
            if (NoEventSelectedCheck()) { return; }
            Event currentEvent = eventsHandler.GetEventByIndex(editingEventIndex);
            currentEvent.TimeRange = timeRangeTextBox.Text;
        }

        private void LinktextBox_TextChanged(object sender, EventArgs e)
        {
            if (NoEventSelectedCheck()) { return; }
            Event currentEvent = eventsHandler.GetEventByIndex(editingEventIndex);
            currentEvent.Link = LinktextBox.Text;

        }





        /// <summary>
        /// push confirmation items
        /// </summary>
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





        /// <summary>
        /// WORKING ON GRAPHICS
        /// </summary>
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





        /// <summary>
        /// No event selected check and message
        /// </summary>
        public bool NoEventSelectedCheck()
        {
            if (editingEventIndex == -1)
            {
                System.Windows.Forms.MessageBox.Show("No event selected! Edit an event or click create new.");
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}