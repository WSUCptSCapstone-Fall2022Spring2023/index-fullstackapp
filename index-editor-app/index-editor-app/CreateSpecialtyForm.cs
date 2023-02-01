using index_editor_app_engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace index_editor_app
{
    public partial class CreateSpecialtyForm : Form
    {
        public CheckedListBox _specialtiesCheckBox;
        public List<MemberSpecialty> newSpecialties;
        public List<MemberSpecialty> removedSpecialties;


        public CreateSpecialtyForm(CheckedListBox specialties)
        {
            InitializeComponent();
            _specialtiesCheckBox = specialties;
            newSpecialties = new List<MemberSpecialty>();
            removedSpecialties = new List<MemberSpecialty>();
            InitializeSpecialties();
        }


        private void InitializeSpecialties()
        {
            int i = 0;
            int k = 0;
            i = _specialtiesCheckBox.Items.Count;
            for (k = 0; k != i; k++)
            {
                SpecialtiesCheckedListBox.Items.Add(_specialtiesCheckBox.Items[k]);
            }
        }

        private void DeleteSpecialtyButton_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("The specialties will be removed from existing memebrs" + Environment.NewLine + "Are you sure?", "Errors found!", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                for (int i = SpecialtiesCheckedListBox.CheckedItems.Count - 1; i >= 0; i--)
                {
                    if (newSpecialties.Count > 0)
                    {
                        foreach (MemberSpecialty specialty in newSpecialties.ToList())
                        {
                            if (SpecialtiesCheckedListBox.CheckedItems[i].ToString() == specialty.Name)
                            {
                                removedSpecialties.Add(specialty);
                                newSpecialties.Remove(specialty);
                            }
                        }
                    }

                    SpecialtiesCheckedListBox.Items.Remove(SpecialtiesCheckedListBox.CheckedItems[i]);
                }
                System.Windows.Forms.MessageBox.Show("Specialties Removed");
            }
        }

        private void AddSpecialtyButton_Click(object sender, EventArgs e)
        {
            MemberSpecialty s = new MemberSpecialty();
            s.Name = SpecialtyNameTextBox.Text;
            s.Link = SpecialtyLinkTextBox.Text;
            newSpecialties.Add(s);

            SpecialtiesCheckedListBox.Items.Add(s.Name);
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {

            //int i = 0;
            //int k = 0;
            //i = SpecialtiesCheckedListBox.Items.Count;
            //for (k = 0; k != i; k++)
            //{
            //    _specialtiesCheckBox.Items.Add(SpecialtiesCheckedListBox.Items[k]);
            //}

            this.Close();


        }
    }
}
