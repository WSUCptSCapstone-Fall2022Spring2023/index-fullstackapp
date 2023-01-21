using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            this.dataGridView2.Rows.Add(5);
            foreach (DataGridViewRow row in this.dataGridView2.Rows)
            {
                row.HeaderCell.Value = string.Format("{0}", row.Index + 1);
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
                LoadMemberIntoFields();
            }
        }


        private void LoadMemberIntoFields()
        {

        }


    }
}