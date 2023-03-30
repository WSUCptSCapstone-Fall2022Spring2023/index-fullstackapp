using FontAwesome.Sharp;
using index_editor_app_engine;
using index_editor_app_engine.JsonClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

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

        int editingSpecialtyIndex = -1;
        bool SpecialtySystemEditing = true;

        public void InitializeSpecialties()
        {
            InitializeSpecialtiesDataGrid();
            InitializeSpecialtiesIcons();
        }

        //add icon options to ComboBox
        public void InitializeSpecialtiesIcons()
        {
            SpecialtyIconComboBox.Items.Add("None");
            foreach (string name in specialtiesHandler.GetIconList())
            {
                SpecialtyIconComboBox.Items.Add(name);
            }
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

        public void EditSpecialtyButtonClick(object sender, DataGridViewCellEventArgs e)
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
            SpecialtySystemEditing = true;
            ClearSpecialtyFields();

            Specialty s = specialtiesHandler.specialtyPage.SpecialtiesList[index];
            SpecialtyNameTextBox.Text = s.Name;
            SpecialtySubtitleTextBox.Text = s.Subtitle;
            SpecialtyLinkTextBox.Text = s.Link;
            SpecialtyDescriptionTextBox.Text = s.Description;
            if (s.Bulletpoints.Count > 0)
            {
                foreach (string bulletpoint in s.Bulletpoints)
                {
                    SpecialtyCheckedListBox1.Items.Add(bulletpoint);
                }
            }

            SpecialtyPictureBox.Image = null;
            SpecialtyPictureBox.Image = await specialtiesHandler.GetImageAsync(editingSpecialtyIndex);

            // load icon
            string iconName = specialtiesHandler.GetIcon(index);
            SpecialtyIconComboBox.SelectedIndex = SpecialtyIconComboBox.FindStringExact(iconName);
            IconChar selectedEnumValue = (IconChar)Enum.Parse(typeof(IconChar), iconName);
            SpecialtyIconPictureBox.Image = FormsIconHelper.ToBitmap(selectedEnumValue, Color.Black);

            SpecialtySystemEditing = false;
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
            SpecialtyIconComboBox.SelectedIndex = -1;
            SpecialtyIconPictureBox.Image = null;
            // TODO
            //Icon = null?
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
            if (NoSpecialtySelectedCheck() || SpecialtySystemEditing) { return; }
            specialtiesHandler.specialtyPage.SpecialtiesList[editingSpecialtyIndex].Name = SpecialtyNameTextBox.Text;
        }

        private void SpecialtySubtitleTextBox_TextChanged(object sender, EventArgs e)
        {
            if (NoSpecialtySelectedCheck() || SpecialtySystemEditing) { return; }
            specialtiesHandler.specialtyPage.SpecialtiesList[editingSpecialtyIndex].Subtitle = SpecialtySubtitleTextBox.Text;
        }

        private void SpecialtyLinkTextBox_TextChanged(object sender, EventArgs e)
        {
            if (NoSpecialtySelectedCheck() || SpecialtySystemEditing) { return; }
            specialtiesHandler.specialtyPage.SpecialtiesList[editingSpecialtyIndex].Link = SpecialtyLinkTextBox.Text;
        }

        private void SpecialtyDescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            if (NoSpecialtySelectedCheck() || SpecialtySystemEditing) { return; }
            specialtiesHandler.specialtyPage.SpecialtiesList[editingSpecialtyIndex].Description = SpecialtyDescriptionTextBox.Text;
        }

        private void DeleteSpecialtyBulletpointButton_Click(object sender, EventArgs e)
        {
            if (NoSpecialtySelectedCheck() || SpecialtySystemEditing) { return; }
            foreach (var item in SpecialtyCheckedListBox1.CheckedItems.OfType<string>().ToList())
            {
                SpecialtyCheckedListBox1.Items.Remove(item);
                specialtiesHandler.specialtyPage.SpecialtiesList[editingSpecialtyIndex].Bulletpoints.Remove(item.ToString());
            }
            LoadSpecialtyDataAsync(editingSpecialtyIndex);
        }

        private void AddBulletpointButton_Click(object sender, EventArgs e)
        {
            if (NoSpecialtySelectedCheck() || SpecialtySystemEditing) { return; }
            //specialtiesHandler.specialtyPage.SpecialtiesList[editingSpecialtyIndex].Bulletpoints.Add(SpecialtyBulletPointTextBox.Text);
            if (AddBulletpointButton.Text.Trim() == "Add Bulletpoint")
            {
                specialtiesHandler.specialtyPage.SpecialtiesList[editingSpecialtyIndex].Bulletpoints.Add(SpecialtyBulletPointTextBox.Text);
            }
            else
            {
                int index = 0;
                foreach (var item in SpecialtyCheckedListBox1.Items)
                {
                    if (SpecialtyCheckedListBox1.CheckedItems.Contains(item))
                    {
                        index = SpecialtyCheckedListBox1.Items.IndexOf(item);
                    }
                }
                specialtiesHandler.specialtyPage.SpecialtiesList[editingSpecialtyIndex].Bulletpoints[index] = SpecialtyBulletPointTextBox.Text;
                SpecialtyBulletPointTextBox.Text = "";
                AddBulletpointButton.Text = "Add Bulletpoint";
            }

            LoadSpecialtyDataAsync(editingSpecialtyIndex);
        }



        private void SpecialtyCheckedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (NoSpecialtySelectedCheck() || SpecialtySystemEditing) { return; }

            for(int i = 0; i < SpecialtyCheckedListBox1.Items.Count; ++i)
            {
                if (i != e.Index) SpecialtyCheckedListBox1.SetItemChecked(i, false);
            }
            if (e.NewValue != CheckState.Unchecked)
            {
                SpecialtyBulletPointTextBox.Text = SpecialtyCheckedListBox1.Items[e.Index].ToString();
                AddBulletpointButton.Text = "Update Bulletpoint";
            }
            else
            {
                SpecialtyBulletPointTextBox.Text = "";
                AddBulletpointButton.Text = "Add Bulletpoint";
            }
        }

        private void CreateSpecialtyButton_Click(object sender, EventArgs e)
        {
            int index = specialtiesHandler.CreateNewSpecialty();
            membersHandler.specialties = specialtiesHandler.specialtyPage;
            InitializeMemberSpecialtyCheckBox();
            InitializeSpecialtiesDataGrid();
        }

        private void DeleteSpecialtyButton_Click(object sender, EventArgs e)
        {
            specialtiesHandler.DeleteSpecialty(editingSpecialtyIndex);
            InitializeSpecialtiesDataGrid();
            SpecialtySystemEditing = true;
            ClearSpecialtyFields();
            SpecialtySystemEditing = false;
            editingSpecialtyIndex = -1;
        }

        private void AddSpecialtyImageButton_Click(object sender, EventArgs e)
        {
            if (NoSpecialtySelectedCheck()) { return; }

            this.openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",                                         //change to images
                FilterIndex = 2,
                RestoreDirectory = true,
            };

            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                specialtiesHandler.AddImage(openFileDialog1.FileName, editingSpecialtyIndex);
            }
        }


        private void SpecialtyIconComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (NoSpecialtySelectedCheck() || SpecialtySystemEditing) { return; }
            string iconName = SpecialtyIconComboBox.GetItemText(SpecialtyIconComboBox.SelectedItem);
            IconChar selectedEnumValue = (IconChar)Enum.Parse(typeof(IconChar), iconName);
            SpecialtyIconPictureBox.Image = FormsIconHelper.ToBitmap(selectedEnumValue, Color.Black);
            specialtiesHandler.UpdateIcon(iconName, editingSpecialtyIndex);
        }

    }
}
