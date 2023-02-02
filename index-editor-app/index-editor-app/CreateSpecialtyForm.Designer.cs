namespace index_editor_app
{
    partial class CreateSpecialtyForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SpecialtyNameTextBox = new System.Windows.Forms.TextBox();
            this.SpecialtyLinkTextBox = new System.Windows.Forms.TextBox();
            this.AddSpecialtyButton = new System.Windows.Forms.Button();
            this.SpecialtiesCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.DeleteSpecialtyButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ConfirmButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SpecialtyNameTextBox
            // 
            this.SpecialtyNameTextBox.Location = new System.Drawing.Point(502, 236);
            this.SpecialtyNameTextBox.Multiline = true;
            this.SpecialtyNameTextBox.Name = "SpecialtyNameTextBox";
            this.SpecialtyNameTextBox.Size = new System.Drawing.Size(286, 45);
            this.SpecialtyNameTextBox.TabIndex = 0;
            // 
            // SpecialtyLinkTextBox
            // 
            this.SpecialtyLinkTextBox.Location = new System.Drawing.Point(502, 307);
            this.SpecialtyLinkTextBox.Multiline = true;
            this.SpecialtyLinkTextBox.Name = "SpecialtyLinkTextBox";
            this.SpecialtyLinkTextBox.Size = new System.Drawing.Size(286, 43);
            this.SpecialtyLinkTextBox.TabIndex = 1;
            // 
            // AddSpecialtyButton
            // 
            this.AddSpecialtyButton.Location = new System.Drawing.Point(620, 375);
            this.AddSpecialtyButton.Name = "AddSpecialtyButton";
            this.AddSpecialtyButton.Size = new System.Drawing.Size(75, 23);
            this.AddSpecialtyButton.TabIndex = 2;
            this.AddSpecialtyButton.Text = "Add";
            this.AddSpecialtyButton.UseVisualStyleBackColor = true;
            this.AddSpecialtyButton.Click += new System.EventHandler(this.AddSpecialtyButton_Click);
            // 
            // SpecialtiesCheckedListBox
            // 
            this.SpecialtiesCheckedListBox.FormattingEnabled = true;
            this.SpecialtiesCheckedListBox.Location = new System.Drawing.Point(56, 32);
            this.SpecialtiesCheckedListBox.Name = "SpecialtiesCheckedListBox";
            this.SpecialtiesCheckedListBox.Size = new System.Drawing.Size(270, 328);
            this.SpecialtiesCheckedListBox.TabIndex = 3;
            // 
            // DeleteSpecialtyButton
            // 
            this.DeleteSpecialtyButton.Location = new System.Drawing.Point(332, 67);
            this.DeleteSpecialtyButton.Name = "DeleteSpecialtyButton";
            this.DeleteSpecialtyButton.Size = new System.Drawing.Size(169, 23);
            this.DeleteSpecialtyButton.TabIndex = 4;
            this.DeleteSpecialtyButton.Text = "Delete Selected";
            this.DeleteSpecialtyButton.UseVisualStyleBackColor = true;
            this.DeleteSpecialtyButton.Click += new System.EventHandler(this.DeleteSpecialtyButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(617, 212);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(627, 289);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Link";
            // 
            // ConfirmButton
            // 
            this.ConfirmButton.Location = new System.Drawing.Point(315, 403);
            this.ConfirmButton.Name = "ConfirmButton";
            this.ConfirmButton.Size = new System.Drawing.Size(152, 23);
            this.ConfirmButton.TabIndex = 8;
            this.ConfirmButton.Text = "Confirm Changes";
            this.ConfirmButton.UseVisualStyleBackColor = true;
            this.ConfirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // CreateSpecialtyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ConfirmButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DeleteSpecialtyButton);
            this.Controls.Add(this.SpecialtiesCheckedListBox);
            this.Controls.Add(this.AddSpecialtyButton);
            this.Controls.Add(this.SpecialtyLinkTextBox);
            this.Controls.Add(this.SpecialtyNameTextBox);
            this.Name = "CreateSpecialtyForm";
            this.Text = "CreateSpecialtyForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox SpecialtyNameTextBox;
        private TextBox SpecialtyLinkTextBox;
        private Button AddSpecialtyButton;
        private CheckedListBox SpecialtiesCheckedListBox;
        private Button DeleteSpecialtyButton;
        private Label label1;
        private Label label3;
        private Button ConfirmButton;
    }
}