namespace index_editor_app
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxConfirmUpdate = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.editingEventNumberLabel = new System.Windows.Forms.Label();
            this.addImageButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.validateChangesButton = new System.Windows.Forms.Button();
            this.creationDateLabel = new System.Windows.Forms.Label();
            this.CreateEvent = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.timeRangeTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.LinktextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.descriptionBox1 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.ItemSize = new System.Drawing.Size(25, 100);
            this.tabControl1.Location = new System.Drawing.Point(12, 46);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1500, 548);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 6;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.textBoxConfirmUpdate);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.editingEventNumberLabel);
            this.tabPage1.Controls.Add(this.addImageButton);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.titleTextBox);
            this.tabPage1.Controls.Add(this.validateChangesButton);
            this.tabPage1.Controls.Add(this.creationDateLabel);
            this.tabPage1.Controls.Add(this.CreateEvent);
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.dateTimePicker1);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.timeRangeTextBox);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.LinktextBox);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.DeleteButton);
            this.tabPage1.Controls.Add(this.descriptionBox1);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(104, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1392, 540);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Events";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1357, 405);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 15);
            this.label7.TabIndex = 25;
            // 
            // textBoxConfirmUpdate
            // 
            this.textBoxConfirmUpdate.Location = new System.Drawing.Point(1225, 419);
            this.textBoxConfirmUpdate.Name = "textBoxConfirmUpdate";
            this.textBoxConfirmUpdate.Size = new System.Drawing.Size(161, 23);
            this.textBoxConfirmUpdate.TabIndex = 24;
            this.textBoxConfirmUpdate.Text = "Type \"confirm\"";
            this.textBoxConfirmUpdate.TextChanged += new System.EventHandler(this.textBoxConfirmUpdate_TextChanged);
            this.textBoxConfirmUpdate.Enter += new System.EventHandler(this.Confirm_TextBox_Enter);
            this.textBoxConfirmUpdate.Leave += new System.EventHandler(this.Confirm_TextBox_Leave);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightGray;
            this.button1.Location = new System.Drawing.Point(1225, 445);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(161, 38);
            this.button1.TabIndex = 23;
            this.button1.Text = "Update Website";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.update_Website_Button_Click);
            // 
            // editingEventNumberLabel
            // 
            this.editingEventNumberLabel.AutoSize = true;
            this.editingEventNumberLabel.Location = new System.Drawing.Point(1003, 225);
            this.editingEventNumberLabel.Name = "editingEventNumberLabel";
            this.editingEventNumberLabel.Size = new System.Drawing.Size(128, 15);
            this.editingEventNumberLabel.TabIndex = 22;
            this.editingEventNumberLabel.Text = "You are editing event #";
            // 
            // addImageButton
            // 
            this.addImageButton.Location = new System.Drawing.Point(846, 187);
            this.addImageButton.Name = "addImageButton";
            this.addImageButton.Size = new System.Drawing.Size(105, 23);
            this.addImageButton.TabIndex = 21;
            this.addImageButton.Text = "Upload image";
            this.addImageButton.UseVisualStyleBackColor = true;
            this.addImageButton.Click += new System.EventHandler(this.addImageButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 197);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 15);
            this.label5.TabIndex = 20;
            this.label5.Text = "Title";
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new System.Drawing.Point(6, 215);
            this.titleTextBox.Multiline = true;
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(584, 35);
            this.titleTextBox.TabIndex = 19;
            this.titleTextBox.TextChanged += new System.EventHandler(this.titleTextBox_TextChanged);
            // 
            // validateChangesButton
            // 
            this.validateChangesButton.BackColor = System.Drawing.Color.DarkOrange;
            this.validateChangesButton.Location = new System.Drawing.Point(1225, 230);
            this.validateChangesButton.Name = "validateChangesButton";
            this.validateChangesButton.Size = new System.Drawing.Size(161, 38);
            this.validateChangesButton.TabIndex = 18;
            this.validateChangesButton.Text = "Validate Changes";
            this.validateChangesButton.UseVisualStyleBackColor = false;
            this.validateChangesButton.Click += new System.EventHandler(this.validateChangesButton_Click);
            // 
            // creationDateLabel
            // 
            this.creationDateLabel.AutoSize = true;
            this.creationDateLabel.Location = new System.Drawing.Point(164, 191);
            this.creationDateLabel.Name = "creationDateLabel";
            this.creationDateLabel.Size = new System.Drawing.Size(142, 15);
            this.creationDateLabel.TabIndex = 17;
            this.creationDateLabel.Text = "This event was created on";
            // 
            // CreateEvent
            // 
            this.CreateEvent.BackColor = System.Drawing.Color.DarkOrange;
            this.CreateEvent.Location = new System.Drawing.Point(1225, 185);
            this.CreateEvent.Name = "CreateEvent";
            this.CreateEvent.Size = new System.Drawing.Size(161, 38);
            this.CreateEvent.TabIndex = 16;
            this.CreateEvent.Text = "Create new event";
            this.CreateEvent.UseVisualStyleBackColor = false;
            this.CreateEvent.Click += new System.EventHandler(this.CreateEvent_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(673, 187);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(167, 141);
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(627, 240);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 15);
            this.label6.TabIndex = 14;
            this.label6.Text = "Image";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(673, 362);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(263, 23);
            this.dateTimePicker1.TabIndex = 11;
            this.dateTimePicker1.Value = new System.DateTime(2022, 11, 5, 0, 0, 0, 0);
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(610, 368);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Start date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(601, 405);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Time range";
            // 
            // timeRangeTextBox
            // 
            this.timeRangeTextBox.Location = new System.Drawing.Point(673, 402);
            this.timeRangeTextBox.Multiline = true;
            this.timeRangeTextBox.Name = "timeRangeTextBox";
            this.timeRangeTextBox.Size = new System.Drawing.Size(263, 23);
            this.timeRangeTextBox.TabIndex = 7;
            this.timeRangeTextBox.TextChanged += new System.EventHandler(this.timeRangeTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(614, 448);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "URL Link";
            // 
            // LinktextBox
            // 
            this.LinktextBox.Location = new System.Drawing.Point(673, 445);
            this.LinktextBox.Multiline = true;
            this.LinktextBox.Name = "LinktextBox";
            this.LinktextBox.Size = new System.Drawing.Size(263, 24);
            this.LinktextBox.TabIndex = 5;
            this.LinktextBox.TextChanged += new System.EventHandler(this.LinktextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 253);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Description";
            // 
            // DeleteButton
            // 
            this.DeleteButton.BackColor = System.Drawing.Color.OrangeRed;
            this.DeleteButton.Location = new System.Drawing.Point(1225, 271);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(161, 38);
            this.DeleteButton.TabIndex = 2;
            this.DeleteButton.Text = "Delete selected event";
            this.DeleteButton.UseVisualStyleBackColor = false;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // descriptionBox1
            // 
            this.descriptionBox1.BackColor = System.Drawing.SystemColors.Window;
            this.descriptionBox1.Location = new System.Drawing.Point(6, 271);
            this.descriptionBox1.Multiline = true;
            this.descriptionBox1.Name = "descriptionBox1";
            this.descriptionBox1.Size = new System.Drawing.Size(584, 216);
            this.descriptionBox1.TabIndex = 1;
            this.descriptionBox1.TextChanged += new System.EventHandler(this.descriptionBox1_TextChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 6);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(1380, 175);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(104, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1392, 540);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Meetings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(104, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1392, 540);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "News";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1536, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.loadToolStripMenuItem.Text = "Load";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(962, 305);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(215, 23);
            this.button2.TabIndex = 26;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkOrange;
            this.ClientSize = new System.Drawing.Size(1536, 606);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_LoadAsync);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private DataGridView dataGridView1;
        private TabPage tabPage3;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem menuToolStripMenuItem;
        private ToolStripMenuItem refreshToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem;
        private TextBox descriptionBox1;
        private Button DeleteButton;
        private Button CreateEvent;
        private PictureBox pictureBox1;
        private Label label6;
        private DateTimePicker dateTimePicker1;
        private Label label4;
        private Label label3;
        private TextBox timeRangeTextBox;
        private Label label2;
        private TextBox LinktextBox;
        private Label label1;
        private Label creationDateLabel;
        private Button validateChangesButton;
        private OpenFileDialog openFileDialog1;
        private Label label5;
        private TextBox titleTextBox;
        private Button addImageButton;
        private Label editingEventNumberLabel;
        private Button button1;
        private Label label7;
        private TextBox textBoxConfirmUpdate;
        private Button button2;
    }
}