using FontAwesome.Sharp;
using index_editor_app_engine;
using index_editor_app_engine.JsonClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        int editingResourceIndex = -1;
        bool ResourceSystemEditing = true;


        public void InitializeResourcs()
        {
            InitializeResourcesDataGrid();
        }

        public void InitializeResourcesDataGrid()
        {
            this.ResourcesDataGridView3.CancelEdit();
            this.ResourcesDataGridView3.Columns.Clear();
            this.ResourcesDataGridView3.Rows.Clear();
            this.ResourcesDataGridView3.DataSource = null;
            this.ResourcesDataGridView3.Columns.Add("name", "Page Title");

            this.ResourcesDataGridView3.Rows.Add(resourcesHandler.ResourcesCount());

            foreach (DataGridViewRow row in this.ResourcesDataGridView3.Rows)
            {
                row.HeaderCell.Value = string.Format("{0}", row.Index + 1);
            }

            for (int i = 0; i < resourcesHandler.ResourcesCount(); i++)
            {
                ResourcesDataGridView3[0, i].Value = resourcesHandler.GetPageTitle(i);
            }

            //add edit button
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            ResourcesDataGridView3.Columns.Add(btn);
            btn.Width = 60;
            btn.HeaderText = "Edit";//column header
            btn.Text = "Edit";
            btn.Name = "Edit";
            btn.UseColumnTextForButtonValue = true;
        }


        private void ResourcesDataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in ResourcesDataGridView3.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.White;
            }

            //column 4 = edit button
            if (e.ColumnIndex == 1 && e.RowIndex != -1)
            {
                editingResourceIndex = e.RowIndex;
                //highlight the column being edited
                ResourcesDataGridView3.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;

                //load Resource into fields
                LoadResourceDataAsync(e.RowIndex);
            }
        }

        public async Task LoadResourceDataAsync(int index)
        {
            ResourceSystemEditing = true;
            ClearResourceFields();

            Resource r = resourcesHandler.GetResource(index);
            //ResourcePageTitleTextBox
            //ResourcePageLinkTextBox
            //ResourcePageDescriptionTextBox

            //ResourcePictureBox
            //ResourceIconPictureBox
            //ResourceIconComboBox
            //ResourceCheckedListBox
            //ResourcePhraseLinkCheckedListBox
            //AddResourceBulletpointButton
            //ResourceBulletpointLinkTextBox
            //ResourceBulletpointDescriptionTextBox
            //ResourceLinkPhraseTextBox
            //ResourceLinkPhraseLinkTextBox
            //AddResourceLinkPhraseButton

            ResourcePageTitleTextBox.Text = r.PageTitle;
            ResourcePageLinkTextBox.Text = r.PageLink;
            ResourcePageDescriptionTextBox.Text = r.PageDescription;


            if (r.Bulletpoints.Count() > 0)
            {
                foreach (Bulletpoint bulletpoint in r.Bulletpoints)
                {
                    ResourceBulletpointCheckedListBox.Items.Add(bulletpoint.Title);
                }
            }

            ResourcePictureBox.Image = null;
            ResourcePictureBox.Image = await resourcesHandler.GetImageAsync(editingResourceIndex);

            //// load icon
            //string iconName = specialtiesHandler.GetIcon(index);
            //SpecialtyIconComboBox.SelectedIndex = SpecialtyIconComboBox.FindStringExact(iconName);
            //IconChar selectedEnumValue = (IconChar)Enum.Parse(typeof(IconChar), iconName);
            //SpecialtyIconPictureBox.Image = FormsIconHelper.ToBitmap(selectedEnumValue, Color.Black);

            ResourceSystemEditing = false;
        }

        public void ClearResourceFields()
        {
            ResourcePageLinkTextBox.Clear();
            ResourcePageDescriptionTextBox.Clear();
            ResourcePictureBox.Image = null;
            ResourceIconPictureBox.Image = null;
            ResourceBulletpointCheckedListBox.Items.Clear();
            ResourcePhraseLinkCheckedListBox.Items.Clear();
            ResourceBulletpointHeaderTextBox.Clear();
            ResourceBulletpointLinkTextBox.Clear();
            ResourceBulletpointDescriptionTextBox.Clear();
            ResourceLinkPhraseTextBox.Clear();
            ResourceLinkPhraseLinkTextBox.Clear();



            ResourceIconComboBox.SelectedIndex = -1;
        }

        private void ResourceCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (NoResourceSelectedCheck() || ResourceSystemEditing) { return; }
            ResourceSystemEditing = true;

            for (int i = 0; i < ResourceBulletpointCheckedListBox.Items.Count; ++i)
            {
                if (i != e.Index) ResourceBulletpointCheckedListBox.SetItemChecked(i, false);
            }
            if (e.NewValue != CheckState.Unchecked)
            {
                Bulletpoint bulletpoint = resourcesHandler.GetResourceBulletpoint(editingResourceIndex, e.Index);

                ResourceBulletpointHeaderTextBox.Text = bulletpoint.Title;
                ResourceBulletpointLinkTextBox.Text = bulletpoint.Link;
                ResourceBulletpointDescriptionTextBox.Text = bulletpoint.Description;

                foreach (LinkPhrase l in bulletpoint.LinkPhrases)
                {
                    ResourcePhraseLinkCheckedListBox.Items.Add(l.Phrase);
                }

                AddResourceBulletpointButton.Text = "Update Bulletpoint";
            }
            else
            {
                ResourceBulletpointHeaderTextBox.Text = "";
                ResourceBulletpointLinkTextBox.Text = "";
                ResourceBulletpointDescriptionTextBox.Text = "";
                ResourceLinkPhraseTextBox.Text = "";
                ResourceLinkPhraseLinkTextBox.Text = "";
                ResourcePhraseLinkCheckedListBox.Items.Clear();
                AddResourceBulletpointButton.Text = "Add Bulletpoint";
                ResourceBulletpointCheckedListBox.SelectedIndex = -1;
            }
            ResourceSystemEditing = false;
        }

        private void ResourcePhraseLinkCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (NoResourceSelectedCheck() || ResourceSystemEditing) { return; }
            ResourceSystemEditing = true;

            for (int i = 0; i < ResourcePhraseLinkCheckedListBox.Items.Count; ++i)
            {
                if (i != e.Index) ResourcePhraseLinkCheckedListBox.SetItemChecked(i, false);
            }
            if (e.NewValue != CheckState.Unchecked)
            {
                LinkPhrase linkPhrase = resourcesHandler.GetResourceBulletpointLinkPhrase(editingResourceIndex, SelectedBulletpointIndex(), e.Index);

                ResourceLinkPhraseTextBox.Text = linkPhrase.Phrase;
                ResourceLinkPhraseLinkTextBox.Text = linkPhrase.Link;
                AddResourceLinkPhraseButton.Text = "Update Embeded Link";
            }
            else
            {
                ResourceLinkPhraseTextBox.Text = "";
                ResourceLinkPhraseLinkTextBox.Text = "";
                AddResourceLinkPhraseButton.Text = "Add New Embeded Link";
                ResourcePhraseLinkCheckedListBox.SelectedIndex = -1;
            }
            ResourceSystemEditing = false;
        }

        public bool NoResourceSelectedCheck()
        {
            if (editingResourceIndex == -1)
            {
                System.Windows.Forms.MessageBox.Show("No Resource selected! Edit a Specialty or click create new.");
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool NoBulletpointSelectedCheck()
        {
            if (SelectedBulletpointIndex() == -1)
            {
                System.Windows.Forms.MessageBox.Show("No Bulletpoint selected! Edit a Specialty or click create new.");
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool NoPhraseSelectedCheck()
        {
            if (SelectedLinkPhraseIndex() == -1)
            {
                System.Windows.Forms.MessageBox.Show("No Phrase selected! Edit a Specialty or click create new.");
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ResourcePageTitleTextBox_TextChanged(object sender, EventArgs e)
        {
            if (NoResourceSelectedCheck() || ResourceSystemEditing) { return; }

            Resource r = resourcesHandler.GetResource(editingResourceIndex);
            r.PageTitle = ResourcePageTitleTextBox.Text;
        }

        private void ResourcePageLinkTextBox_TextChanged(object sender, EventArgs e)
        {
            if (NoResourceSelectedCheck() || ResourceSystemEditing) { return; }

            Resource r = resourcesHandler.GetResource(editingResourceIndex);
            r.PageLink = ResourcePageLinkTextBox.Text;
        }

        private void ResourcePageDescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            if (NoResourceSelectedCheck() || ResourceSystemEditing) { return; }

            Resource r = resourcesHandler.GetResource(editingResourceIndex);
            r.PageDescription = ResourcePageDescriptionTextBox.Text;
        }

        private void ResourceBulletpointHeaderTextBox_TextChanged(object sender, EventArgs e)
        {
            
            if (NoResourceSelectedCheck() || ResourceSystemEditing) { return; }
            if (NoBulletpointSelectedCheck()) { return; }

            Bulletpoint b = resourcesHandler.GetResourceBulletpoint(editingResourceIndex, SelectedBulletpointIndex());
            b.Title = ResourceBulletpointHeaderTextBox.Text;
        }

        private void ResourceBulletpointLinkTextBox_TextChanged(object sender, EventArgs e) 
        {
            if (NoResourceSelectedCheck() || ResourceSystemEditing) { return; }
            if (NoBulletpointSelectedCheck()) { return; }

            Bulletpoint b = resourcesHandler.GetResourceBulletpoint(editingResourceIndex, SelectedBulletpointIndex());
            b.Link = ResourceBulletpointLinkTextBox.Text;
        }

        private void ResourceBulletpointDescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            if (NoResourceSelectedCheck() || ResourceSystemEditing) { return; }
            if (NoBulletpointSelectedCheck()) { return; }

            Bulletpoint b = resourcesHandler.GetResourceBulletpoint(editingResourceIndex, SelectedBulletpointIndex());
            b.Description = ResourceBulletpointDescriptionTextBox.Text;
        }

        //The phrase to be linked
        private void ResourceLinkPhraseTextBox_TextChanged(object sender, EventArgs e)
        {
            if (NoResourceSelectedCheck() || ResourceSystemEditing) { return; }
            if (NoBulletpointSelectedCheck()) { return; }
            if (NoPhraseSelectedCheck()) { return; }

            LinkPhrase l = resourcesHandler.GetResourceBulletpointLinkPhrase(editingResourceIndex, SelectedBulletpointIndex(), SelectedLinkPhraseIndex());
            l.Phrase = ResourceLinkPhraseTextBox.Text;
        }

        //the link for the phrase
        private void ResourceLinkPhraseLinkTextBox_TextChanged(object sender, EventArgs e)
        {
            if (NoResourceSelectedCheck() || ResourceSystemEditing) { return; }
            if (NoBulletpointSelectedCheck()) { return; }
            if (NoPhraseSelectedCheck()) { return; }

            LinkPhrase l = resourcesHandler.GetResourceBulletpointLinkPhrase(editingResourceIndex, SelectedBulletpointIndex(), SelectedLinkPhraseIndex());
            l.Link = ResourceLinkPhraseLinkTextBox.Text;
        }

        private void AddResourceImageButton_Click(object sender, EventArgs e)
        {
            if (NoResourceSelectedCheck() || ResourceSystemEditing) { return; }

            this.openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",                                         //change to images
                FilterIndex = 2,
                RestoreDirectory = true,
            };

            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                resourcesHandler.AddImage(openFileDialog1.FileName, editingResourceIndex);
            }
        }

        private void AddResourceBulletpointButton_Click(object sender, EventArgs e)
        {
            if (NoResourceSelectedCheck() || ResourceSystemEditing) { return; }
            //TODO add update/refresh the checkbox
            resourcesHandler.AddBulletPoint(editingResourceIndex);
        }

        private void AddResourceLinkPhraseButton_Click(object sender, EventArgs e)
        {
            if (NoResourceSelectedCheck() || ResourceSystemEditing) { return; }
            if (NoBulletpointSelectedCheck()) { return; }
            //TODO add update/refresh the checkbox
            resourcesHandler.AddLinkPhrase(editingResourceIndex, SelectedBulletpointIndex());
        }

        public int SelectedBulletpointIndex()
        {
            return ResourceBulletpointCheckedListBox.SelectedIndex;
        }

        public int SelectedLinkPhraseIndex()
        {
            return ResourcePhraseLinkCheckedListBox.SelectedIndex;
        }
        private void ValidateLinkPhrasesButton_Click(object sender, EventArgs e)
        {
            string validationMessage = resourcesHandler.ValidateLinkPhrases(editingResourceIndex, SelectedBulletpointIndex());

            System.Windows.Forms.MessageBox.Show(validationMessage);
        }
    }
}
