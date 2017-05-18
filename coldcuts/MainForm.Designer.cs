namespace ColdCutsNS
{
    partial class MainForm
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
        protected void InitializeComponent(){
            this.sourceBrowseButton = new System.Windows.Forms.Button();
            this.sourceFilePathTextBox = new System.Windows.Forms.TextBox();
            this.sourceLabel = new System.Windows.Forms.Label();
            this.destinationBrowseButton = new System.Windows.Forms.Button();
            this.destinationLabel = new System.Windows.Forms.Label();
            this.destinationFilePathTextBox = new System.Windows.Forms.TextBox();
            this.inputFileGroupbox = new System.Windows.Forms.GroupBox();
            this.lengthInputLabel = new System.Windows.Forms.Label();
            this.lengthLabel = new System.Windows.Forms.Label();
            this.feedBackLabel2 = new System.Windows.Forms.Label();
            this.feedBackLabel = new System.Windows.Forms.Label();
            this.artistInputLabel = new System.Windows.Forms.Label();
            this.titleInputLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.artistLabel = new System.Windows.Forms.Label();
            this.encodeButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.endSecTextBox = new System.Windows.Forms.TextBox();
            this.endMinTextBox = new System.Windows.Forms.TextBox();
            this.startSecTextBox = new System.Windows.Forms.TextBox();
            this.startMinTextBox = new System.Windows.Forms.TextBox();
            this.endLabel = new System.Windows.Forms.Label();
            this.startLabel = new System.Windows.Forms.Label();
            this.deleteButton = new System.Windows.Forms.Button();
            this.commentOutputTextBox = new System.Windows.Forms.TextBox();
            this.commentOutputLabel = new System.Windows.Forms.Label();
            this.albumOutputTextBox = new System.Windows.Forms.TextBox();
            this.fileNameOutputBox = new System.Windows.Forms.TextBox();
            this.titleOutputTextBox = new System.Windows.Forms.TextBox();
            this.fileNameLabel = new System.Windows.Forms.Label();
            this.artistOutputTextBox = new System.Windows.Forms.TextBox();
            this.artistOutputLabel = new System.Windows.Forms.Label();
            this.titleOutputLabel = new System.Windows.Forms.Label();
            this.albumOutputLabel = new System.Windows.Forms.Label();
            this.fileLeftButton = new System.Windows.Forms.Button();
            this.fileRightButton = new System.Windows.Forms.Button();
            this.addFileButton = new System.Windows.Forms.Button();
            this.editPositionLabel = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.gridtracknumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridtrackname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridstarttime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridendtime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inputFileGroupbox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            //
            // sourceBrowseButton
            //
            this.sourceBrowseButton.Location = new System.Drawing.Point(11, 52);
            this.sourceBrowseButton.Name = "sourceBrowseButton";
            this.sourceBrowseButton.Size = new System.Drawing.Size(67, 30);
            this.sourceBrowseButton.TabIndex = 0;
            this.sourceBrowseButton.Text = "Browse...";
            this.sourceBrowseButton.UseVisualStyleBackColor = true;
            this.sourceBrowseButton.Click += new System.EventHandler(this.browseButton_Click);
            //
            // sourceFilePathTextBox
            //
            this.sourceFilePathTextBox.Location = new System.Drawing.Point(11, 26);
            this.sourceFilePathTextBox.Name = "sourceFilePathTextBox";
            this.sourceFilePathTextBox.Size = new System.Drawing.Size(310, 20);
            this.sourceFilePathTextBox.TabIndex = 2;
            //
            // sourceLabel
            //
            this.sourceLabel.AutoSize = true;
            this.sourceLabel.Location = new System.Drawing.Point(9, 9);
            this.sourceLabel.Name = "sourceLabel";
            this.sourceLabel.Size = new System.Drawing.Size(71, 13);
            this.sourceLabel.TabIndex = 3;
            this.sourceLabel.Text = "Source path: ";
            //
            // destinationBrowseButton
            //
            this.destinationBrowseButton.Enabled = false;
            this.destinationBrowseButton.Location = new System.Drawing.Point(11, 130);
            this.destinationBrowseButton.Name = "destinationBrowseButton";
            this.destinationBrowseButton.Size = new System.Drawing.Size(67, 30);
            this.destinationBrowseButton.TabIndex = 4;
            this.destinationBrowseButton.Text = "Browse...";
            this.destinationBrowseButton.UseVisualStyleBackColor = true;
            this.destinationBrowseButton.Click += new System.EventHandler(this.destinationBrowseButton_Click);
            //
            // destinationLabel
            //
            this.destinationLabel.AutoSize = true;
            this.destinationLabel.Location = new System.Drawing.Point(9, 88);
            this.destinationLabel.Name = "destinationLabel";
            this.destinationLabel.Size = new System.Drawing.Size(90, 13);
            this.destinationLabel.TabIndex = 5;
            this.destinationLabel.Text = "Destination path: ";
            //
            // destinationFilePathTextBox
            //
            this.destinationFilePathTextBox.Enabled = false;
            this.destinationFilePathTextBox.Location = new System.Drawing.Point(11, 104);
            this.destinationFilePathTextBox.Name = "destinationFilePathTextBox";
            this.destinationFilePathTextBox.Size = new System.Drawing.Size(310, 20);
            this.destinationFilePathTextBox.TabIndex = 6;
            //
            // inputFileGroupbox
            //
            this.inputFileGroupbox.Controls.Add(this.lengthInputLabel);
            this.inputFileGroupbox.Controls.Add(this.lengthLabel);
            this.inputFileGroupbox.Controls.Add(this.feedBackLabel2);
            this.inputFileGroupbox.Controls.Add(this.feedBackLabel);
            this.inputFileGroupbox.Controls.Add(this.artistInputLabel);
            this.inputFileGroupbox.Controls.Add(this.titleInputLabel);
            this.inputFileGroupbox.Controls.Add(this.titleLabel);
            this.inputFileGroupbox.Controls.Add(this.artistLabel);
            this.inputFileGroupbox.Location = new System.Drawing.Point(14, 176);
            this.inputFileGroupbox.Name = "inputFileGroupbox";
            this.inputFileGroupbox.Size = new System.Drawing.Size(309, 135);
            this.inputFileGroupbox.TabIndex = 9;
            this.inputFileGroupbox.TabStop = false;
            this.inputFileGroupbox.Text = "Source File Information: ";
            //
            // lengthInputLabel
            //
            this.lengthInputLabel.AutoSize = true;
            this.lengthInputLabel.Location = new System.Drawing.Point(63, 79);
            this.lengthInputLabel.Name = "lengthInputLabel";
            this.lengthInputLabel.Size = new System.Drawing.Size(32, 13);
            this.lengthInputLabel.TabIndex = 11;
            this.lengthInputLabel.Text = "xxxxx";
            //
            // lengthLabel
            //
            this.lengthLabel.AutoSize = true;
            this.lengthLabel.Location = new System.Drawing.Point(6, 79);
            this.lengthLabel.Name = "lengthLabel";
            this.lengthLabel.Size = new System.Drawing.Size(62, 13);
            this.lengthLabel.TabIndex = 11;
            this.lengthLabel.Text = "File Length:";
            //
            // feedBackLabel2
            //
            this.feedBackLabel2.AutoSize = true;
            this.feedBackLabel2.Location = new System.Drawing.Point(6, 114);
            this.feedBackLabel2.Name = "feedBackLabel2";
            this.feedBackLabel2.Size = new System.Drawing.Size(62, 13);
            this.feedBackLabel2.TabIndex = 12;
            this.feedBackLabel2.Text = "Percentage";
            this.feedBackLabel2.Visible = false;
            //
            // feedBackLabel
            //
            this.feedBackLabel.AutoSize = true;
            this.feedBackLabel.Location = new System.Drawing.Point(6, 101);
            this.feedBackLabel.Name = "feedBackLabel";
            this.feedBackLabel.Size = new System.Drawing.Size(61, 13);
            this.feedBackLabel.TabIndex = 11;
            this.feedBackLabel.Text = "Encoding...";
            this.feedBackLabel.Visible = false;
            //
            // artistInputLabel
            //
            this.artistInputLabel.Location = new System.Drawing.Point(36, 16);
            this.artistInputLabel.Name = "artistInputLabel";
            this.artistInputLabel.Size = new System.Drawing.Size(267, 26);
            this.artistInputLabel.TabIndex = 3;
            this.artistInputLabel.Text = "xxxxx";
            //
            // titleInputLabel
            //
            this.titleInputLabel.Location = new System.Drawing.Point(36, 46);
            this.titleInputLabel.Name = "titleInputLabel";
            this.titleInputLabel.Size = new System.Drawing.Size(267, 27);
            this.titleInputLabel.TabIndex = 2;
            this.titleInputLabel.Text = "xxxxx";
            //
            // titleLabel
            //
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(6, 46);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(30, 13);
            this.titleLabel.TabIndex = 1;
            this.titleLabel.Text = "Title:";
            //
            // artistLabel
            //
            this.artistLabel.AutoSize = true;
            this.artistLabel.Location = new System.Drawing.Point(6, 16);
            this.artistLabel.Name = "artistLabel";
            this.artistLabel.Size = new System.Drawing.Size(33, 13);
            this.artistLabel.TabIndex = 0;
            this.artistLabel.Text = "Artist:";
            //
            // encodeButton
            //
            this.encodeButton.Enabled = false;
            this.encodeButton.Location = new System.Drawing.Point(343, 365);
            this.encodeButton.Name = "encodeButton";
            this.encodeButton.Size = new System.Drawing.Size(80, 26);
            this.encodeButton.TabIndex = 10;
            this.encodeButton.Text = "Split MP3 File";
            this.encodeButton.UseVisualStyleBackColor = true;
            this.encodeButton.Click += new System.EventHandler(this.encodeButton_Click);
            //
            // groupBox1
            //
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.endSecTextBox);
            this.groupBox1.Controls.Add(this.endMinTextBox);
            this.groupBox1.Controls.Add(this.startSecTextBox);
            this.groupBox1.Controls.Add(this.startMinTextBox);
            this.groupBox1.Controls.Add(this.endLabel);
            this.groupBox1.Controls.Add(this.startLabel);
            this.groupBox1.Controls.Add(this.deleteButton);
            this.groupBox1.Controls.Add(this.commentOutputTextBox);
            this.groupBox1.Controls.Add(this.commentOutputLabel);
            this.groupBox1.Controls.Add(this.albumOutputTextBox);
            this.groupBox1.Controls.Add(this.fileNameOutputBox);
            this.groupBox1.Controls.Add(this.titleOutputTextBox);
            this.groupBox1.Controls.Add(this.fileNameLabel);
            this.groupBox1.Controls.Add(this.artistOutputTextBox);
            this.groupBox1.Controls.Add(this.artistOutputLabel);
            this.groupBox1.Controls.Add(this.titleOutputLabel);
            this.groupBox1.Controls.Add(this.albumOutputLabel);
            this.groupBox1.Location = new System.Drawing.Point(342, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(250, 321);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Output File: ";
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Min : Sec";
            //
            // endSecTextBox
            //
            this.endSecTextBox.Enabled = false;
            this.endSecTextBox.Location = new System.Drawing.Point(72, 60);
            this.endSecTextBox.Name = "endSecTextBox";
            this.endSecTextBox.Size = new System.Drawing.Size(31, 20);
            this.endSecTextBox.TabIndex = 31;
            this.endSecTextBox.Leave += new System.EventHandler(this.Leave);
            //
            // endMinTextBox
            //
            this.endMinTextBox.Enabled = false;
            this.endMinTextBox.Location = new System.Drawing.Point(35, 60);
            this.endMinTextBox.Name = "endMinTextBox";
            this.endMinTextBox.Size = new System.Drawing.Size(31, 20);
            this.endMinTextBox.TabIndex = 30;
            this.endMinTextBox.Leave += new System.EventHandler(this.Leave);
            //
            // startSecTextBox
            //
            this.startSecTextBox.Enabled = false;
            this.startSecTextBox.Location = new System.Drawing.Point(72, 36);
            this.startSecTextBox.Name = "startSecTextBox";
            this.startSecTextBox.Size = new System.Drawing.Size(31, 20);
            this.startSecTextBox.TabIndex = 29;
            this.startSecTextBox.Leave += new System.EventHandler(this.Leave);
            //
            // startMinTextBox
            //
            this.startMinTextBox.Enabled = false;
            this.startMinTextBox.Location = new System.Drawing.Point(35, 36);
            this.startMinTextBox.Name = "startMinTextBox";
            this.startMinTextBox.Size = new System.Drawing.Size(31, 20);
            this.startMinTextBox.TabIndex = 28;
            this.startMinTextBox.Leave += new System.EventHandler(this.Leave);
            //
            // endLabel
            //
            this.endLabel.AutoSize = true;
            this.endLabel.Location = new System.Drawing.Point(6, 66);
            this.endLabel.Name = "endLabel";
            this.endLabel.Size = new System.Drawing.Size(26, 13);
            this.endLabel.TabIndex = 27;
            this.endLabel.Text = "End";
            //
            // startLabel
            //
            this.startLabel.AutoSize = true;
            this.startLabel.Location = new System.Drawing.Point(5, 43);
            this.startLabel.Name = "startLabel";
            this.startLabel.Size = new System.Drawing.Size(29, 13);
            this.startLabel.TabIndex = 26;
            this.startLabel.Text = "Start";
            //
            // deleteButton
            //
            this.deleteButton.Enabled = false;
            this.deleteButton.Location = new System.Drawing.Point(142, 24);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(71, 24);
            this.deleteButton.TabIndex = 25;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            //
            // commentOutputTextBox
            //
            this.commentOutputTextBox.Enabled = false;
            this.commentOutputTextBox.Location = new System.Drawing.Point(6, 253);
            this.commentOutputTextBox.Multiline = true;
            this.commentOutputTextBox.Name = "commentOutputTextBox";
            this.commentOutputTextBox.Size = new System.Drawing.Size(217, 59);
            this.commentOutputTextBox.TabIndex = 24;
            this.commentOutputTextBox.Leave += new System.EventHandler(this.Leave);
            //
            // commentOutputLabel
            //
            this.commentOutputLabel.AutoSize = true;
            this.commentOutputLabel.Location = new System.Drawing.Point(6, 239);
            this.commentOutputLabel.Name = "commentOutputLabel";
            this.commentOutputLabel.Size = new System.Drawing.Size(57, 13);
            this.commentOutputLabel.TabIndex = 20;
            this.commentOutputLabel.Text = "Comment: ";
            //
            // albumOutputTextBox
            //
            this.albumOutputTextBox.Enabled = false;
            this.albumOutputTextBox.Location = new System.Drawing.Point(6, 217);
            this.albumOutputTextBox.Name = "albumOutputTextBox";
            this.albumOutputTextBox.Size = new System.Drawing.Size(217, 20);
            this.albumOutputTextBox.TabIndex = 23;
            this.albumOutputTextBox.Leave += new System.EventHandler(this.Leave);
            //
            // fileNameOutputBox
            //
            this.fileNameOutputBox.Enabled = false;
            this.fileNameOutputBox.Location = new System.Drawing.Point(6, 98);
            this.fileNameOutputBox.Name = "fileNameOutputBox";
            this.fileNameOutputBox.Size = new System.Drawing.Size(217, 20);
            this.fileNameOutputBox.TabIndex = 16;
            this.fileNameOutputBox.Leave += new System.EventHandler(this.Leave);
            //
            // titleOutputTextBox
            //
            this.titleOutputTextBox.Enabled = false;
            this.titleOutputTextBox.Location = new System.Drawing.Point(6, 177);
            this.titleOutputTextBox.Name = "titleOutputTextBox";
            this.titleOutputTextBox.Size = new System.Drawing.Size(217, 20);
            this.titleOutputTextBox.TabIndex = 22;
            //
            // fileNameLabel
            //
            this.fileNameLabel.AutoSize = true;
            this.fileNameLabel.Location = new System.Drawing.Point(6, 83);
            this.fileNameLabel.Name = "fileNameLabel";
            this.fileNameLabel.Size = new System.Drawing.Size(57, 13);
            this.fileNameLabel.TabIndex = 15;
            this.fileNameLabel.Text = "File Name:";
            //
            // artistOutputTextBox
            //
            this.artistOutputTextBox.Enabled = false;
            this.artistOutputTextBox.Location = new System.Drawing.Point(6, 139);
            this.artistOutputTextBox.Name = "artistOutputTextBox";
            this.artistOutputTextBox.Size = new System.Drawing.Size(217, 20);
            this.artistOutputTextBox.TabIndex = 21;
            this.artistOutputTextBox.Leave += new System.EventHandler(this.Leave);
            //
            // artistOutputLabel
            //
            this.artistOutputLabel.AutoSize = true;
            this.artistOutputLabel.Location = new System.Drawing.Point(6, 123);
            this.artistOutputLabel.Name = "artistOutputLabel";
            this.artistOutputLabel.Size = new System.Drawing.Size(36, 13);
            this.artistOutputLabel.TabIndex = 17;
            this.artistOutputLabel.Text = "Artist: ";
            //
            // titleOutputLabel
            //
            this.titleOutputLabel.AutoSize = true;
            this.titleOutputLabel.Location = new System.Drawing.Point(6, 161);
            this.titleOutputLabel.Name = "titleOutputLabel";
            this.titleOutputLabel.Size = new System.Drawing.Size(33, 13);
            this.titleOutputLabel.TabIndex = 18;
            this.titleOutputLabel.Text = "Title: ";
            //
            // albumOutputLabel
            //
            this.albumOutputLabel.AutoSize = true;
            this.albumOutputLabel.Location = new System.Drawing.Point(5, 201);
            this.albumOutputLabel.Name = "albumOutputLabel";
            this.albumOutputLabel.Size = new System.Drawing.Size(42, 13);
            this.albumOutputLabel.TabIndex = 19;
            this.albumOutputLabel.Text = "Album: ";
            //
            // fileLeftButton
            //
            this.fileLeftButton.Enabled = false;
            this.fileLeftButton.Location = new System.Drawing.Point(343, 12);
            this.fileLeftButton.Name = "fileLeftButton";
            this.fileLeftButton.Size = new System.Drawing.Size(28, 26);
            this.fileLeftButton.TabIndex = 12;
            this.fileLeftButton.Text = "<<";
            this.fileLeftButton.Click += new System.EventHandler(this.fileLeftButton_Click);
            //
            // fileRightButton
            //
            this.fileRightButton.Enabled = false;
            this.fileRightButton.Location = new System.Drawing.Point(377, 12);
            this.fileRightButton.Name = "fileRightButton";
            this.fileRightButton.Size = new System.Drawing.Size(28, 26);
            this.fileRightButton.TabIndex = 13;
            this.fileRightButton.Text = ">>";
            this.fileRightButton.Click += new System.EventHandler(this.fileRightButton_Click);
            //
            // addFileButton
            //
            this.addFileButton.Enabled = false;
            this.addFileButton.Location = new System.Drawing.Point(411, 12);
            this.addFileButton.Name = "addFileButton";
            this.addFileButton.Size = new System.Drawing.Size(28, 26);
            this.addFileButton.TabIndex = 14;
            this.addFileButton.Text = "+";
            this.addFileButton.Click += new System.EventHandler(this.addFileButton_Click);
            //
            // editPositionLabel
            //
            this.editPositionLabel.AutoSize = true;
            this.editPositionLabel.Location = new System.Drawing.Point(448, 21);
            this.editPositionLabel.Name = "editPositionLabel";
            this.editPositionLabel.Size = new System.Drawing.Size(99, 13);
            this.editPositionLabel.TabIndex = 15;
            this.editPositionLabel.Text = "Editing Output File: ";
            //
            // dataGridView1
            //
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gridtracknumber,
            this.gridtrackname,
            this.gridstarttime,
            this.gridendtime});
            this.dataGridView1.Location = new System.Drawing.Point(608, 37);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(315, 353);
            this.dataGridView1.TabIndex = 16;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewClickedNewRow);
            this.dataGridView1.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewLeave);
            //
            // gridtracknumber
            //
            this.gridtracknumber.HeaderText = "#";
            this.gridtracknumber.Name = "gridtracknumber";
            this.gridtracknumber.ReadOnly = true;
            this.gridtracknumber.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.gridtracknumber.Width = 20;
            //
            // gridtrackname
            //
            this.gridtrackname.HeaderText = "File Name";
            this.gridtrackname.Name = "gridtrackname";
            this.gridtrackname.ReadOnly = true;
            this.gridtrackname.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.gridtrackname.Width = 150;
            //
            // gridstarttime
            //
            this.gridstarttime.HeaderText = "Start Time";
            this.gridstarttime.Name = "gridstarttime";
            this.gridstarttime.ReadOnly = true;
            this.gridstarttime.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.gridstarttime.Width = 50;
            //
            // gridendtime
            //
            this.gridendtime.HeaderText = "End Time";
            this.gridendtime.Name = "gridendtime";
            this.gridendtime.ReadOnly = true;
            this.gridendtime.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.gridendtime.Width = 50;
            //
            // MainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 396);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.editPositionLabel);
            this.Controls.Add(this.addFileButton);
            this.Controls.Add(this.fileRightButton);
            this.Controls.Add(this.fileLeftButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.encodeButton);
            this.Controls.Add(this.inputFileGroupbox);
            this.Controls.Add(this.destinationFilePathTextBox);
            this.Controls.Add(this.destinationLabel);
            this.Controls.Add(this.destinationBrowseButton);
            this.Controls.Add(this.sourceLabel);
            this.Controls.Add(this.sourceFilePathTextBox);
            this.Controls.Add(this.sourceBrowseButton);
            this.Name = "MainForm";
            this.Text = "Cold Cuts";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.inputFileGroupbox.ResumeLayout(false);
            this.inputFileGroupbox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        public System.Windows.Forms.Button sourceBrowseButton;
        public System.Windows.Forms.TextBox sourceFilePathTextBox;
        public System.Windows.Forms.Label sourceLabel;
        public System.Windows.Forms.Button destinationBrowseButton;
        public System.Windows.Forms.Label destinationLabel;
        public  System.Windows.Forms.TextBox destinationFilePathTextBox;
        public System.Windows.Forms.GroupBox inputFileGroupbox;
        public System.Windows.Forms.Label artistLabel;
        public System.Windows.Forms.Label titleLabel;
        public System.Windows.Forms.Label titleInputLabel;
        public System.Windows.Forms.Label artistInputLabel;
        public System.Windows.Forms.Button encodeButton;
        public System.Windows.Forms.Label feedBackLabel;
        public System.Windows.Forms.Label feedBackLabel2;
        public System.Windows.Forms.Label lengthLabel;
        public System.Windows.Forms.Label lengthInputLabel;
        public System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Button fileLeftButton;
        public System.Windows.Forms.Button fileRightButton;
        public System.Windows.Forms.Button addFileButton;
        public System.Windows.Forms.Label fileNameLabel;
        public System.Windows.Forms.TextBox fileNameOutputBox;
        public System.Windows.Forms.Label artistOutputLabel;
        public System.Windows.Forms.Label titleOutputLabel;
        public System.Windows.Forms.Label albumOutputLabel;
        public System.Windows.Forms.Label commentOutputLabel;
        public System.Windows.Forms.TextBox artistOutputTextBox;
        public System.Windows.Forms.TextBox titleOutputTextBox;
        public System.Windows.Forms.TextBox albumOutputTextBox;
        public System.Windows.Forms.TextBox commentOutputTextBox;
        public System.Windows.Forms.Label editPositionLabel;
        public System.Windows.Forms.Button deleteButton;
        public System.Windows.Forms.Label startLabel;
        public System.Windows.Forms.Label endLabel;
        public System.Windows.Forms.TextBox startMinTextBox;
        public System.Windows.Forms.TextBox startSecTextBox;
        public System.Windows.Forms.TextBox endMinTextBox;
        public System.Windows.Forms.TextBox endSecTextBox;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridtracknumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridtrackname;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridstarttime;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridendtime;
    }
}

