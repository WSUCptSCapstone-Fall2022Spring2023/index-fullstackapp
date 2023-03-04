using index_editor_app_engine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace index_editor_app
{
    //[System.ComponentModel.DesignerCategory("Code")]
    public partial class Form1
    {
        int editingMemberIndex = -1;
        bool systemLoadingChecks = true;


        public void InitializeMembersDataGrid()
        {
            //clear the datagrid
            this.dataGridView2.CancelEdit();
            this.dataGridView2.Columns.Clear();
            this.dataGridView2.Rows.Clear();
            this.dataGridView2.DataSource = null;
            this.dataGridView2.Columns.Add("name", "Name");

            //add row index
            //implement a check for empty/no members
            this.dataGridView2.Rows.Add(membersHandler.GetMemberCount());

            foreach (DataGridViewRow row in this.dataGridView2.Rows)
            {
                row.HeaderCell.Value = string.Format("{0}", row.Index + 1);
            }

            //add member data
            for (int i = 0; i < membersHandler.GetMemberCount(); i++)
            {
                BoardMember m = membersHandler.GetMemberByIndex(i);
                dataGridView2[0, i].Value = m.Name;
            }

            //add edit button
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            dataGridView2.Columns.Add(btn);
            btn.Width = 60;
            btn.HeaderText = "Edit";//column header
            btn.Text = "Edit";
            btn.Name = "Edit";
            btn.UseColumnTextForButtonValue = true;
        }

        public void InitializeMemberSpecialtyCheckBox()
        {
            SpecialtyCheckedListBox.Items.Clear();
            foreach (string specialtyName in specialtiesHandler.GetSpecialtyNames())
            {
                SpecialtyCheckedListBox.Items.Add(specialtyName);
            }
        }


        private void EditMemberButtonClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.White;
            }

            //column 4 = edit button
            if (e.ColumnIndex == 1 && e.RowIndex != -1)
            {
                editingMemberIndex = e.RowIndex;
                //highlight the column being edited
                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;

                //load memebr into fields
                LoadMemberDataAsync(e.RowIndex);
            }
        }

        private void LoadMembersData()
        {
            PageDescriptionTextBox.Text = membersHandler.memberspage.PageDescription;
            ApplicationLinkTextBox.Text = membersHandler.memberspage.ApplicationLink.ToString();
            LinkPhraseTextbox.Text = membersHandler.memberspage.PhraseLink;
        }

        private async Task LoadMemberDataAsync(int index)
        {
            editingMemberIndex = index;
            BoardMember m = membersHandler.GetMemberByIndex(index);

            PositionTextBox.Text = m.Position;
            MemberNameTextbox.Text = m.Name;
            BioTextBox.Text = m.BioDescription;
            QuoteTextBox.Text = m.BioQuote;
            OfficeLocationTextBox.Text = m.OfficeLocation;           
            BioLinkTextBox.Text = m.BioLink.ToString();
            EmployeeSinceTextBox.Text = m.EmployeeSince.ToString();
            OfficeLinkTextBox.Text = m.OfficeLink;

            systemLoadingChecks = true;
            ////uncheck all specialties
            UncheckAllSpecialties();

            // recheck specialties
            foreach (var specialty in m.Specialties)
            {
                for (int i = 0; i < SpecialtyCheckedListBox.Items.Count; i++)
                {
                    if (SpecialtyCheckedListBox.Items[i].ToString() == specialty.Name)
                    {
                        SpecialtyCheckedListBox.SetItemChecked(i, true);
                    }
                }
            }
            systemLoadingChecks = false;


            MemberPictureBox.Image = null;
            if (m.Image != "")
            {
                MemberPictureBox.Image = System.Drawing.Image.FromStream(await membersHandler.LoadMemberImageHandlerAsync(m.Name));
            }
        }

        private void CreateMemberButton_Click(object sender, EventArgs e)
        {
            membersHandler.CreateBoardMember();
            InitializeMembersDataGrid();
        }

        private void AddMemberImageButton_Click(object sender, EventArgs e)
        {
            NoMemberSelectedCheck();
            if (string.IsNullOrEmpty(membersHandler.memberspage.BoardMembers[editingMemberIndex].Name))
            {
                System.Windows.Forms.MessageBox.Show("Warning, A name is required before adding an image");
                return;
            }

            //check this out
            if (editingMemberIndex == -1)                                                                     //CREATE NO EVENT SELECTED FUNCTION
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
                membersHandler.AddMemberImage(openFileDialog1.FileName, editingMemberIndex);
                LoadMemberDataAsync(editingMemberIndex);
            }
        }

        private void SpecialtyCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!systemLoadingChecks)
            {
                membersHandler.UpdateMemberSpecialties((e.NewValue.ToString() == "Checked"), e.Index, editingMemberIndex);
            }
        }

        private void DeleteMemberButton_Click(object sender, EventArgs e)
        {
            ClearMemberFields();
            membersHandler.DeleteBoardMember(editingMemberIndex);
            InitializeMembersDataGrid();
            editingMemberIndex = -1;
        }

        private void ConfirmMemberChangesButton_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("The website is being updated...");
            var httpreseponse = membersHandler.UpdateMembersPage();
            System.Windows.Forms.MessageBox.Show(httpreseponse.ToString());
        }

        public void ClearMemberFields()
        {
            systemLoadingChecks = true;
            UncheckAllSpecialties();
            PositionTextBox.Text = "";
            MemberNameTextbox.Text = "";
            BioTextBox.Text = "";
            QuoteTextBox.Text = "";
            OfficeLocationTextBox.Text = "";
            BioLinkTextBox.Text = "";
            EmployeeSinceTextBox.Text = "";
            OfficeLinkTextBox.Text = "";
            MemberPictureBox.Image = null;
        }



        public bool NoMemberSelectedCheck()
        {
            if (editingMemberIndex == -1)
            {
                System.Windows.Forms.MessageBox.Show("No member selected! Edit a member or click create new.");
                return true;
            }
            else
            {
                return false;
            }
        }

        public void UncheckAllSpecialties()
        {
            for (int i = 0; i < SpecialtyCheckedListBox.Items.Count; i++)
            {
                SpecialtyCheckedListBox.SetItemChecked(i, false);
            }
        }



        private void MemberNameTextbox_TextChanged(object sender, EventArgs e)
        {
            if (NoMemberSelectedCheck()) { return; }
            membersHandler.memberspage.BoardMembers[editingMemberIndex].Name = MemberNameTextbox.Text;
        }

        private void PositionTextBox_TextChanged(object sender, EventArgs e)
        {
            if (NoMemberSelectedCheck()) { return; }
            membersHandler.memberspage.BoardMembers[editingMemberIndex].Position = PositionTextBox.Text;
        }

        private void BioTextBox_TextChanged(object sender, EventArgs e)
        {
            if (NoMemberSelectedCheck()) { return; }
            membersHandler.memberspage.BoardMembers[editingMemberIndex].BioDescription = BioTextBox.Text;
        }

        private void QuoteTextBox_TextChanged(object sender, EventArgs e)
        {
            if (NoMemberSelectedCheck()) { return; }
            membersHandler.memberspage.BoardMembers[editingMemberIndex].BioQuote = QuoteTextBox.Text;
        }

        private void BioLinkTextBox_TextChanged(object sender, EventArgs e)
        {
            if (NoMemberSelectedCheck()) { return; }
            membersHandler.memberspage.BoardMembers[editingMemberIndex].BioLink = BioLinkTextBox.Text;
        }

        private void EmployeeSinceTextBox_TextChanged(object sender, EventArgs e)
        {
            if (NoMemberSelectedCheck()) { return; }
            membersHandler.memberspage.BoardMembers[editingMemberIndex].EmployeeSince = EmployeeSinceTextBox.Text;
        }

        private void OfficeLinkTextBox_TextChanged(object sender, EventArgs e)
        {
            if (NoMemberSelectedCheck()) { return; }
            membersHandler.memberspage.BoardMembers[editingMemberIndex].OfficeLink = OfficeLinkTextBox.Text;
        }

        private void OfficeLocationTextBox_TextChanged(object sender, EventArgs e)
        {
            if (NoMemberSelectedCheck()) { return; }
            membersHandler.memberspage.BoardMembers[editingMemberIndex].OfficeLocation = OfficeLocationTextBox.Text;
        }

        private void PageDescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            membersHandler.memberspage.PageDescription = PageDescriptionTextBox.Text;
        }

        private void ApplicationLinkTextBox_TextChanged(object sender, EventArgs e)
        {
            membersHandler.memberspage.ApplicationLink = ApplicationLinkTextBox.Text;
        }

        private void LinkPhraseTextbox_TextChanged(object sender, EventArgs e)
        {
            membersHandler.memberspage.PhraseLink = LinkPhraseTextbox.Text;
        }
    }
}