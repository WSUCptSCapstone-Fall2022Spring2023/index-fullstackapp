using index_editor_app_engine;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection.Metadata;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace index_editor_app
{
    public partial class Form1 : Form
    {
        IndexAPIClient indexClient;
        EventsHandler eventsHandler;
        MembersHandler membersHandler;
        SpecialtyHandler specialtiesHandler;
        NewsHandler newsHandler;
        EditorInstances editorInstances;
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_LoadAsync(object sender, EventArgs e)
        {
            this.indexClient = new IndexAPIClient();
            if (await TestConnection())
            {
                eventsHandler = new EventsHandler(await indexClient.GetDocument("events"), indexClient);
                membersHandler = new MembersHandler(await indexClient.GetDocument("members"), await indexClient.GetDocument("specialties"), indexClient);
                specialtiesHandler = new SpecialtyHandler(await indexClient.GetDocument("specialties"), indexClient);
                newsHandler = new NewsHandler(await indexClient.GetDocument("news"), indexClient);

                editorInstances = new EditorInstances();
                editorInstances.eventsHandler = eventsHandler;
                editorInstances.membersHandler = membersHandler;
                editorInstances.specialtiesHandler = specialtiesHandler;
                editorInstances.newsHandler = newsHandler;

                InitializeEventsDataGrid();
                InitializeMembersDataGrid();
                LoadMembersData();
                InitializeMemberSpecialtyCheckBox();
                InitializeSpecialties();
                InitializeNews();
            }



            eventsHandler.AddOrdinalSuffix("Friday, December 15");
        }

        /// <summary>
        /// Test connection to index API
        /// </summary>
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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                // Opening Event tab
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                // Opening Members tab
                InitializeMemberSpecialtyCheckBox();
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                // Opening News tab
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                // Opening Specialties tab
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    SaveLoad.Save
                        (
                        fbd.SelectedPath,
                        eventsHandler.GetJsonString(),
                        membersHandler.GetJsonString(),
                        specialtiesHandler.GetJsonString(),
                        newsHandler.GetJsonString()
                        );
                }
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Warning, current data will be lost when loading from backup. Continue?", "Confirm Load!", MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                using (var fbd = new FolderBrowserDialog())
                {
                    DialogResult result = fbd.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        try
                        {
                            SaveLoad.Load(fbd.SelectedPath, editorInstances);
                            InitializeEventsDataGrid();
                            InitializeMembersDataGrid();
                            LoadMembersData();
                            InitializeMemberSpecialtyCheckBox();
                            InitializeSpecialties();
                            InitializeNews();
                        }
                        catch (Exception error)
                        {
                            var errorMessageBox = MessageBox.Show(error.Message);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// description and demonstration from original
        /// instead of test plans we have test cases (for each test case there are 5 subsections whats being tested expected result what actually happened, test case requirements)
        /// include at least one from ad least one functional and non functional
        /// 
        /// code delivery, how to deleiver code to clients
        /// 
        /// feb 17 capstone meeting https://wsu.zoom.us/j/97286634407?pwd=dFZtZ3NiaGFZZk5GSjZGcE1Dc2hwZz09
        /// 
        /// </summary>

    }
}