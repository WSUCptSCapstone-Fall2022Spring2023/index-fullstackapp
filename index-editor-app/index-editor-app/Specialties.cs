using index_editor_app_engine;
using index_editor_app_engine.JsonClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace index_editor_app
{
    public partial class Form1
    {
        //STEPS
        //1. create json
        //2. create API enpoint
        //3. create C# class
        //4. Create handler
        //5. Create form1 extension with handler in it
        //6. Form1_LoadAsync: init handler and Initialize___form1 extension___();
        //7. Each form should....
        // Initialize_______(); load data into fields
        // all TextBox_TextChanged events
        // clear all textbox method
        // editing index
        // 





        //get the specialties from api
        //store the specialties
        SpecialtyHandler specialtiesHandler;

        int editingSpecialtyIndex = -1;
        bool SpecialtySystemEditing = true;

        public void InitializeSpecialties()
        {
            InitializeSpecialtiesDataGrid();
        }

        public void InitializeSpecialtiesCheckBox()
        {

        }

        public void InitializeSpecialtiesDataGrid()
        {
            
            this.SpecialtiesdataGridView3.CancelEdit();
            this.SpecialtiesdataGridView3.Columns.Clear();
            this.SpecialtiesdataGridView3.Rows.Clear();
            this.SpecialtiesdataGridView3.DataSource = null;
            this.SpecialtiesdataGridView3.Columns.Add("name", "Name");
            this.SpecialtiesdataGridView3.Rows.Add(specialtiesHandler.SpecialtyCount());

            foreach (DataGridViewRow row in this.SpecialtiesdataGridView3.Rows)
            {
                row.HeaderCell.Value = string.Format("{0}", row.Index + 1);
            }

            for (int i = 0; i < specialtiesHandler.SpecialtyCount(); i++)
            {
                SpecialtiesdataGridView3[0, i].Value = specialtiesHandler.specialtyPage.SpecialtiesList[i].Name;
            }

            //add edit button
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            SpecialtiesdataGridView3.Columns.Add(btn);
            btn.Width = 60;
            btn.HeaderText = "Edit";//column header
            btn.Text = "Edit";
            btn.Name = "Edit";
            btn.UseColumnTextForButtonValue = true;
        }

        private void EditSpecialtyButtonClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in SpecialtiesdataGridView3.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.White;
            }

            //column 4 = edit button
            if (e.ColumnIndex == 1 && e.RowIndex != -1)
            {
                editingSpecialtyIndex = e.RowIndex;
                //highlight the column being edited
                SpecialtiesdataGridView3.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;

                //load memebr into fields
                LoadSpecialtyDataAsync(e.RowIndex);
            }
        }

        public async Task LoadSpecialtyDataAsync(int index)
        {
            RemoveSpecialtyEvents();
            ClearSpecialtyFields();

            Specialty s = specialtiesHandler.specialtyPage.SpecialtiesList[index];
            SpecialtyNameTextBox.Text = s.Name;
            SpecialtySubtitleTextBox.Text = s.Subtitle;
            SpecialtyLinkTextBox.Text = s.Link;
            SpecialtyDescriptionTextBox.Text = s.Description;

            foreach (string bulletpoint in s.Bulletpoints)
            {
                SpecialtyCheckedListBox1.Items.Add(bulletpoint);
            }

            SpecialtyPictureBox.Image = null;
            if (s.Image != "")
            {
                SpecialtyPictureBox.Image = System.Drawing.Image.FromStream(await specialtiesHandler.LoadSpecialtyImageHandlerAsync(s.Name));
            }

            AddSpecialtyEvents();
        }

        private void UploadSpecialtiesButton_Click(object sender, EventArgs e)
        {
            var httpreseponse = specialtiesHandler.Upload();
            System.Windows.Forms.MessageBox.Show(httpreseponse.ToString());
        }

        public void ClearSpecialtyFields()
        {
            SpecialtyNameTextBox.Clear();
            SpecialtySubtitleTextBox.Clear();
            SpecialtyLinkTextBox.Clear();
            SpecialtyDescriptionTextBox.Clear();
            SpecialtyCheckedListBox1.Items.Clear();
        }

        public void RemoveSpecialtyEvents()
        {
            SpecialtyNameTextBox.TextChanged -= new System.EventHandler(SpecialtyNameTextBox_TextChanged);
            SpecialtySubtitleTextBox.TextChanged -= new System.EventHandler(SpecialtySubtitleTextBox_TextChanged);
            SpecialtyLinkTextBox.TextChanged -= new System.EventHandler(SpecialtyLinkTextBox_TextChanged);
            SpecialtyDescriptionTextBox.TextChanged -= new System.EventHandler(SpecialtyDescriptionTextBox_TextChanged);
            //SpecialtyCheckedListBox1.Items.TextChanged -= new System.EventHandler(SpecialtyNameTextBox_TextChanged);
        }

        public void AddSpecialtyEvents()
        {
            SpecialtyNameTextBox.TextChanged += new System.EventHandler(SpecialtyNameTextBox_TextChanged);
            SpecialtySubtitleTextBox.TextChanged += new System.EventHandler(SpecialtySubtitleTextBox_TextChanged);
            SpecialtyLinkTextBox.TextChanged += new System.EventHandler(SpecialtyLinkTextBox_TextChanged);
            SpecialtyDescriptionTextBox.TextChanged += new System.EventHandler(SpecialtyDescriptionTextBox_TextChanged);
        }

        public bool NoSpecialtySelectedCheck()
        {
            if (editingSpecialtyIndex == -1)
            {
                System.Windows.Forms.MessageBox.Show("No Specialty selected! Edit a Specialty or click create new.");
                return true;
            }
            else
            {
                return false;
            }
        }


        private void SpecialtyNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (NoSpecialtySelectedCheck()) { return; }
            specialtiesHandler.specialtyPage.SpecialtiesList[editingSpecialtyIndex].Name = SpecialtyNameTextBox.Text;
        }

        private void SpecialtySubtitleTextBox_TextChanged(object sender, EventArgs e)
        {
            if (NoSpecialtySelectedCheck()) { return; }
            specialtiesHandler.specialtyPage.SpecialtiesList[editingSpecialtyIndex].Subtitle = SpecialtySubtitleTextBox.Text;
        }

        private void SpecialtyLinkTextBox_TextChanged(object sender, EventArgs e)
        {
            if (NoSpecialtySelectedCheck()) { return; }
            specialtiesHandler.specialtyPage.SpecialtiesList[editingSpecialtyIndex].Link = SpecialtyLinkTextBox.Text;
        }

        private void SpecialtyDescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            if (NoSpecialtySelectedCheck()) { return; }
            specialtiesHandler.specialtyPage.SpecialtiesList[editingSpecialtyIndex].Description = SpecialtyDescriptionTextBox.Text;
        }

        private void DeleteSpecialtyBulletpointButton_Click(object sender, EventArgs e)
        {
            if (NoSpecialtySelectedCheck()) { return; }
            foreach (var item in SpecialtyCheckedListBox1.CheckedItems.OfType<string>().ToList())
            {
                SpecialtyCheckedListBox1.Items.Remove(item);
                specialtiesHandler.specialtyPage.SpecialtiesList[editingSpecialtyIndex].Bulletpoints.Remove(item.ToString());
            }
            LoadSpecialtyDataAsync(editingSpecialtyIndex);
        }

        private void AddBulletpointButton_Click(object sender, EventArgs e)
        {
            if (NoSpecialtySelectedCheck()) { return; }
            specialtiesHandler.specialtyPage.SpecialtiesList[editingSpecialtyIndex].Bulletpoints.Add(SpecialtyBulletPointTextBox.Text);
            LoadSpecialtyDataAsync(editingSpecialtyIndex);
        }

        private void AddSpecialtyImageButton_Click(object sender, EventArgs e)
        {
            if (NoSpecialtySelectedCheck()) { return; }
            if (string.IsNullOrEmpty(specialtiesHandler.specialtyPage.SpecialtiesList[editingSpecialtyIndex].Name))
            {
                System.Windows.Forms.MessageBox.Show("Warning, A name is required before adding an image");
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
                specialtiesHandler.AddSpecialtyImage(openFileDialog1.FileName, editingSpecialtyIndex);
                LoadSpecialtyDataAsync(editingSpecialtyIndex);
            }
        }

    }
}
