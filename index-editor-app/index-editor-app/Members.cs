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
        


        private void MemberNameTextbox_TextChanged(object sender, EventArgs e)
        {

        }


        public void InitializeMembersDataGrid()
        {
            //clear the datagrid
            this.dataGridView2.CancelEdit();
            this.dataGridView2.Columns.Clear();
            this.dataGridView2.Rows.Clear();
            this.dataGridView2.DataSource = null;
            this.dataGridView2.Columns.Add("name", "Name");

            //add row index
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

        public void InitializeSpecialtyCheckBox()
        {
            foreach (string specialtyName in membersHandler.MemberSpecialtyDict.Keys)
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
        }

        private async Task LoadMemberDataAsync(int index)
        {


            BoardMember m = membersHandler.GetMemberByIndex(index);

            PositionTextBox.Text = m.Position;
            MemberNameTextbox.Text = m.Name;
            BioTextBox.Text = m.BioDescription;
            QuoteTextBox.Text = m.BioQuote;
            OfficeLocationTextBox.Text = m.OfficeLocation;           
            BioLinkTextBox.Text = m.BioLink.ToString();
            EmployeeSinceTextBox.Text = m.EmployeeSince.ToString();
            OfficeLinkTextBox.Text = m.OfficeLink;

            ////uncheck all specialties
            for (int i = 0; i < SpecialtyCheckedListBox.Items.Count; i++)
            {
                SpecialtyCheckedListBox.SetItemChecked(i, false);
            }

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

            pictureBox1.Image = null;
            if (m.Image != "")
            {
                MemberPictureBox.Image = System.Drawing.Image.FromStream(await membersHandler.LoadMemberImageHandlerAsync(m.Name));
            }
        }



        private void CreateMemberButton_Click(object sender, EventArgs e)
        {

        }







        //Below is for modify specialties. It will be moved to a new dedicated tab for handling specialties in the future

        //button to edit specialties is clicked
        //private void button2_Click(object sender, EventArgs e)
        //{
        //    // create new form
        //    var createSpecialtyForm = new CreateSpecialtyForm(SpecialtyCheckedListBox);
        //    createSpecialtyForm.FormClosed += (formVisibleChanged);
        //    createSpecialtyForm.ShowDialog();
        //    //createSpecialtyForm.VisibleChanged += new EventHandler(formVisibleChanged);

        //}

        //private void formVisibleChanged(object sender, EventArgs e)
        //{
        //    CreateSpecialtyForm specialtyForm = (CreateSpecialtyForm)sender;

        //}



    }
}