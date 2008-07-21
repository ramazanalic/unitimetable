namespace UniTimetable
{
    partial class FormImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImport));
            this.panelBottom = new System.Windows.Forms.Panel();
            this.etchedLine1 = new UniTimetable.EtchedLine();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnFinish = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listBoxFormats = new System.Windows.Forms.ListBox();
            this.lblTitle1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtFileInstructions = new System.Windows.Forms.TextBox();
            this.txtFile3 = new System.Windows.Forms.TextBox();
            this.txtFile2 = new System.Windows.Forms.TextBox();
            this.txtFile1 = new System.Windows.Forms.TextBox();
            this.lblFile3 = new System.Windows.Forms.Label();
            this.btnBrowse3 = new System.Windows.Forms.Button();
            this.lblFile2 = new System.Windows.Forms.Label();
            this.btnBrowse2 = new System.Windows.Forms.Button();
            this.lblFile1 = new System.Windows.Forms.Label();
            this.btnBrowse1 = new System.Windows.Forms.Button();
            this.lblTitle2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.boxStreamDetails = new System.Windows.Forms.GroupBox();
            this.timetableControl1 = new UniTimetable.TimetableControl();
            this.txtTreeDetails = new System.Windows.Forms.TextBox();
            this.treePreview = new System.Windows.Forms.TreeView();
            this.lblTitle3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblClashNotice = new System.Windows.Forms.Label();
            this.lblIgnored = new System.Windows.Forms.Label();
            this.lblTitle4 = new System.Windows.Forms.Label();
            this.lblRequired = new System.Windows.Forms.Label();
            this.listViewRequired = new System.Windows.Forms.ListView();
            this.Code = new System.Windows.Forms.ColumnHeader();
            this.TypeName = new System.Windows.Forms.ColumnHeader();
            this.btnRequire = new System.Windows.Forms.Button();
            this.btnIgnore = new System.Windows.Forms.Button();
            this.listViewIgnored = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.etchedLine2 = new UniTimetable.EtchedLine();
            this.panelBottom.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.boxStreamDetails.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.etchedLine1);
            this.panelBottom.Controls.Add(this.btnBack);
            this.panelBottom.Controls.Add(this.btnCancel);
            this.panelBottom.Controls.Add(this.btnNext);
            this.panelBottom.Controls.Add(this.btnFinish);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 397);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(540, 40);
            this.panelBottom.TabIndex = 0;
            // 
            // etchedLine1
            // 
            this.etchedLine1.Dock = System.Windows.Forms.DockStyle.Top;
            this.etchedLine1.Location = new System.Drawing.Point(0, 0);
            this.etchedLine1.Name = "etchedLine1";
            this.etchedLine1.Size = new System.Drawing.Size(540, 8);
            this.etchedLine1.TabIndex = 1;
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBack.Location = new System.Drawing.Point(274, 10);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "< &Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(453, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.Location = new System.Drawing.Point(355, 10);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "&Next >";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnFinish
            // 
            this.btnFinish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFinish.Location = new System.Drawing.Point(355, 10);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(75, 23);
            this.btnFinish.TabIndex = 1;
            this.btnFinish.Text = "&Finish";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Visible = false;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listBoxFormats);
            this.panel1.Controls.Add(this.lblTitle1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(540, 397);
            this.panel1.TabIndex = 1;
            // 
            // listBoxFormats
            // 
            this.listBoxFormats.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBoxFormats.FormattingEnabled = true;
            this.listBoxFormats.IntegralHeight = false;
            this.listBoxFormats.ItemHeight = 100;
            this.listBoxFormats.Location = new System.Drawing.Point(12, 56);
            this.listBoxFormats.Name = "listBoxFormats";
            this.listBoxFormats.Size = new System.Drawing.Size(516, 329);
            this.listBoxFormats.TabIndex = 1;
            this.listBoxFormats.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxFormats_MouseDoubleClick);
            this.listBoxFormats.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBoxFormats_DrawItem);
            // 
            // lblTitle1
            // 
            this.lblTitle1.AutoSize = true;
            this.lblTitle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle1.Location = new System.Drawing.Point(8, 9);
            this.lblTitle1.Name = "lblTitle1";
            this.lblTitle1.Size = new System.Drawing.Size(322, 24);
            this.lblTitle1.TabIndex = 0;
            this.lblTitle1.Text = "Select University Format to Import";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtFileInstructions);
            this.panel2.Controls.Add(this.txtFile3);
            this.panel2.Controls.Add(this.txtFile2);
            this.panel2.Controls.Add(this.txtFile1);
            this.panel2.Controls.Add(this.lblFile3);
            this.panel2.Controls.Add(this.btnBrowse3);
            this.panel2.Controls.Add(this.lblFile2);
            this.panel2.Controls.Add(this.btnBrowse2);
            this.panel2.Controls.Add(this.lblFile1);
            this.panel2.Controls.Add(this.btnBrowse1);
            this.panel2.Controls.Add(this.lblTitle2);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(540, 397);
            this.panel2.TabIndex = 4;
            // 
            // txtFileInstructions
            // 
            this.txtFileInstructions.AcceptsReturn = true;
            this.txtFileInstructions.AcceptsTab = true;
            this.txtFileInstructions.Location = new System.Drawing.Point(12, 182);
            this.txtFileInstructions.Multiline = true;
            this.txtFileInstructions.Name = "txtFileInstructions";
            this.txtFileInstructions.ReadOnly = true;
            this.txtFileInstructions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFileInstructions.Size = new System.Drawing.Size(516, 203);
            this.txtFileInstructions.TabIndex = 2;
            // 
            // txtFile3
            // 
            this.txtFile3.Location = new System.Drawing.Point(12, 150);
            this.txtFile3.Name = "txtFile3";
            this.txtFile3.ReadOnly = true;
            this.txtFile3.Size = new System.Drawing.Size(435, 20);
            this.txtFile3.TabIndex = 2;
            // 
            // txtFile2
            // 
            this.txtFile2.Location = new System.Drawing.Point(12, 111);
            this.txtFile2.Name = "txtFile2";
            this.txtFile2.ReadOnly = true;
            this.txtFile2.Size = new System.Drawing.Size(435, 20);
            this.txtFile2.TabIndex = 2;
            // 
            // txtFile1
            // 
            this.txtFile1.Location = new System.Drawing.Point(12, 72);
            this.txtFile1.Name = "txtFile1";
            this.txtFile1.ReadOnly = true;
            this.txtFile1.Size = new System.Drawing.Size(435, 20);
            this.txtFile1.TabIndex = 2;
            // 
            // lblFile3
            // 
            this.lblFile3.AutoSize = true;
            this.lblFile3.Location = new System.Drawing.Point(9, 134);
            this.lblFile3.Name = "lblFile3";
            this.lblFile3.Size = new System.Drawing.Size(88, 13);
            this.lblFile3.TabIndex = 1;
            this.lblFile3.Text = "File 3 Description";
            // 
            // btnBrowse3
            // 
            this.btnBrowse3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse3.Location = new System.Drawing.Point(453, 149);
            this.btnBrowse3.Name = "btnBrowse3";
            this.btnBrowse3.Size = new System.Drawing.Size(75, 22);
            this.btnBrowse3.TabIndex = 0;
            this.btnBrowse3.Text = "Browse";
            this.btnBrowse3.UseVisualStyleBackColor = true;
            this.btnBrowse3.Click += new System.EventHandler(this.btnBrowse3_Click);
            // 
            // lblFile2
            // 
            this.lblFile2.AutoSize = true;
            this.lblFile2.Location = new System.Drawing.Point(9, 95);
            this.lblFile2.Name = "lblFile2";
            this.lblFile2.Size = new System.Drawing.Size(88, 13);
            this.lblFile2.TabIndex = 1;
            this.lblFile2.Text = "File 2 Description";
            // 
            // btnBrowse2
            // 
            this.btnBrowse2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse2.Location = new System.Drawing.Point(453, 110);
            this.btnBrowse2.Name = "btnBrowse2";
            this.btnBrowse2.Size = new System.Drawing.Size(75, 22);
            this.btnBrowse2.TabIndex = 0;
            this.btnBrowse2.Text = "Browse";
            this.btnBrowse2.UseVisualStyleBackColor = true;
            this.btnBrowse2.Click += new System.EventHandler(this.btnBrowse2_Click);
            // 
            // lblFile1
            // 
            this.lblFile1.AutoSize = true;
            this.lblFile1.Location = new System.Drawing.Point(9, 56);
            this.lblFile1.Name = "lblFile1";
            this.lblFile1.Size = new System.Drawing.Size(88, 13);
            this.lblFile1.TabIndex = 1;
            this.lblFile1.Text = "File 1 Description";
            // 
            // btnBrowse1
            // 
            this.btnBrowse1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse1.Location = new System.Drawing.Point(453, 71);
            this.btnBrowse1.Name = "btnBrowse1";
            this.btnBrowse1.Size = new System.Drawing.Size(75, 22);
            this.btnBrowse1.TabIndex = 0;
            this.btnBrowse1.Text = "Browse";
            this.btnBrowse1.UseVisualStyleBackColor = true;
            this.btnBrowse1.Click += new System.EventHandler(this.btnBrowse1_Click);
            // 
            // lblTitle2
            // 
            this.lblTitle2.AutoSize = true;
            this.lblTitle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle2.Location = new System.Drawing.Point(8, 9);
            this.lblTitle2.Name = "lblTitle2";
            this.lblTitle2.Size = new System.Drawing.Size(201, 24);
            this.lblTitle2.TabIndex = 0;
            this.lblTitle2.Text = "Load Timetable Data";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.boxStreamDetails);
            this.panel3.Controls.Add(this.treePreview);
            this.panel3.Controls.Add(this.lblTitle3);
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(540, 397);
            this.panel3.TabIndex = 5;
            // 
            // boxStreamDetails
            // 
            this.boxStreamDetails.Controls.Add(this.timetableControl1);
            this.boxStreamDetails.Controls.Add(this.txtTreeDetails);
            this.boxStreamDetails.Location = new System.Drawing.Point(265, 50);
            this.boxStreamDetails.Name = "boxStreamDetails";
            this.boxStreamDetails.Size = new System.Drawing.Size(263, 335);
            this.boxStreamDetails.TabIndex = 3;
            this.boxStreamDetails.TabStop = false;
            this.boxStreamDetails.Text = "Details";
            // 
            // timetableControl1
            // 
            this.timetableControl1.EnableDrag = false;
            this.timetableControl1.Grayscale = false;
            this.timetableControl1.HourEnd = 21;
            this.timetableControl1.HourStart = 8;
            this.timetableControl1.Location = new System.Drawing.Point(22, 170);
            this.timetableControl1.Name = "timetableControl1";
            this.timetableControl1.ShowAll = true;
            this.timetableControl1.ShowDays = false;
            this.timetableControl1.ShowGrayArea = false;
            this.timetableControl1.ShowLocation = false;
            this.timetableControl1.ShowText = false;
            this.timetableControl1.ShowTimes = false;
            this.timetableControl1.ShowWeekend = true;
            this.timetableControl1.Size = new System.Drawing.Size(220, 155);
            this.timetableControl1.TabIndex = 3;
            // 
            // txtTreeDetails
            // 
            this.txtTreeDetails.AcceptsReturn = true;
            this.txtTreeDetails.AcceptsTab = true;
            this.txtTreeDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTreeDetails.Location = new System.Drawing.Point(13, 19);
            this.txtTreeDetails.Multiline = true;
            this.txtTreeDetails.Name = "txtTreeDetails";
            this.txtTreeDetails.ReadOnly = true;
            this.txtTreeDetails.Size = new System.Drawing.Size(238, 142);
            this.txtTreeDetails.TabIndex = 2;
            // 
            // treePreview
            // 
            this.treePreview.HideSelection = false;
            this.treePreview.Location = new System.Drawing.Point(12, 56);
            this.treePreview.Name = "treePreview";
            this.treePreview.Size = new System.Drawing.Size(241, 329);
            this.treePreview.TabIndex = 1;
            this.treePreview.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treePreview_AfterSelect);
            // 
            // lblTitle3
            // 
            this.lblTitle3.AutoSize = true;
            this.lblTitle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle3.Location = new System.Drawing.Point(8, 9);
            this.lblTitle3.Name = "lblTitle3";
            this.lblTitle3.Size = new System.Drawing.Size(202, 24);
            this.lblTitle3.TabIndex = 0;
            this.lblTitle3.Text = "Preview Stream Data";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lblClashNotice);
            this.panel4.Controls.Add(this.lblIgnored);
            this.panel4.Controls.Add(this.lblTitle4);
            this.panel4.Controls.Add(this.lblRequired);
            this.panel4.Controls.Add(this.listViewRequired);
            this.panel4.Controls.Add(this.btnRequire);
            this.panel4.Controls.Add(this.btnIgnore);
            this.panel4.Controls.Add(this.listViewIgnored);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(540, 397);
            this.panel4.TabIndex = 5;
            // 
            // lblClashNotice
            // 
            this.lblClashNotice.BackColor = System.Drawing.Color.Red;
            this.lblClashNotice.Location = new System.Drawing.Point(308, 10);
            this.lblClashNotice.Name = "lblClashNotice";
            this.lblClashNotice.Size = new System.Drawing.Size(220, 23);
            this.lblClashNotice.TabIndex = 12;
            this.lblClashNotice.Text = "Red indicates an unavoidable clash";
            this.lblClashNotice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblIgnored
            // 
            this.lblIgnored.AutoSize = true;
            this.lblIgnored.Location = new System.Drawing.Point(9, 50);
            this.lblIgnored.Name = "lblIgnored";
            this.lblIgnored.Size = new System.Drawing.Size(43, 13);
            this.lblIgnored.TabIndex = 8;
            this.lblIgnored.Text = "Ignored";
            // 
            // lblTitle4
            // 
            this.lblTitle4.AutoSize = true;
            this.lblTitle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle4.Location = new System.Drawing.Point(8, 9);
            this.lblTitle4.Name = "lblTitle4";
            this.lblTitle4.Size = new System.Drawing.Size(183, 24);
            this.lblTitle4.TabIndex = 0;
            this.lblTitle4.Text = "Streams to Include";
            // 
            // lblRequired
            // 
            this.lblRequired.AutoSize = true;
            this.lblRequired.Location = new System.Drawing.Point(305, 50);
            this.lblRequired.Name = "lblRequired";
            this.lblRequired.Size = new System.Drawing.Size(50, 13);
            this.lblRequired.TabIndex = 6;
            this.lblRequired.Text = "Required";
            // 
            // listViewRequired
            // 
            this.listViewRequired.AllowColumnReorder = true;
            this.listViewRequired.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewRequired.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Code,
            this.TypeName});
            this.listViewRequired.FullRowSelect = true;
            this.listViewRequired.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewRequired.Location = new System.Drawing.Point(308, 69);
            this.listViewRequired.MultiSelect = false;
            this.listViewRequired.Name = "listViewRequired";
            this.listViewRequired.Size = new System.Drawing.Size(220, 316);
            this.listViewRequired.TabIndex = 7;
            this.listViewRequired.UseCompatibleStateImageBehavior = false;
            this.listViewRequired.View = System.Windows.Forms.View.Details;
            this.listViewRequired.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewRequired_MouseDoubleClick);
            this.listViewRequired.SelectedIndexChanged += new System.EventHandler(this.listViewRequired_SelectedIndexChanged);
            this.listViewRequired.Enter += new System.EventHandler(this.listViewRequired_Enter);
            // 
            // Code
            // 
            this.Code.Text = "Code";
            this.Code.Width = 30;
            // 
            // TypeName
            // 
            this.TypeName.Text = "Type";
            this.TypeName.Width = 160;
            // 
            // btnRequire
            // 
            this.btnRequire.Location = new System.Drawing.Point(238, 201);
            this.btnRequire.Name = "btnRequire";
            this.btnRequire.Size = new System.Drawing.Size(64, 23);
            this.btnRequire.TabIndex = 10;
            this.btnRequire.Text = ">>>>";
            this.btnRequire.UseVisualStyleBackColor = true;
            this.btnRequire.Click += new System.EventHandler(this.btnRequire_Click);
            // 
            // btnIgnore
            // 
            this.btnIgnore.Location = new System.Drawing.Point(238, 231);
            this.btnIgnore.Name = "btnIgnore";
            this.btnIgnore.Size = new System.Drawing.Size(64, 23);
            this.btnIgnore.TabIndex = 11;
            this.btnIgnore.Text = "<<<<";
            this.btnIgnore.UseVisualStyleBackColor = true;
            this.btnIgnore.Click += new System.EventHandler(this.btnIgnore_Click);
            // 
            // listViewIgnored
            // 
            this.listViewIgnored.AllowColumnReorder = true;
            this.listViewIgnored.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewIgnored.FullRowSelect = true;
            this.listViewIgnored.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewIgnored.Location = new System.Drawing.Point(12, 69);
            this.listViewIgnored.MultiSelect = false;
            this.listViewIgnored.Name = "listViewIgnored";
            this.listViewIgnored.Size = new System.Drawing.Size(220, 316);
            this.listViewIgnored.TabIndex = 9;
            this.listViewIgnored.UseCompatibleStateImageBehavior = false;
            this.listViewIgnored.View = System.Windows.Forms.View.Details;
            this.listViewIgnored.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewIgnored_MouseDoubleClick);
            this.listViewIgnored.SelectedIndexChanged += new System.EventHandler(this.listViewIgnored_SelectedIndexChanged);
            this.listViewIgnored.Enter += new System.EventHandler(this.listViewIgnored_Enter);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Code";
            this.columnHeader1.Width = 30;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Type";
            this.columnHeader2.Width = 160;
            // 
            // etchedLine2
            // 
            this.etchedLine2.Location = new System.Drawing.Point(0, 42);
            this.etchedLine2.Name = "etchedLine2";
            this.etchedLine2.Size = new System.Drawing.Size(540, 4);
            this.etchedLine2.TabIndex = 6;
            // 
            // FormImport
            // 
            this.AcceptButton = this.btnNext;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(540, 437);
            this.Controls.Add(this.etchedLine2);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormImport";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import Wizard";
            this.Load += new System.EventHandler(this.FormImport_Load);
            this.panelBottom.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.boxStreamDetails.ResumeLayout(false);
            this.boxStreamDetails.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnCancel;
        private EtchedLine etchedLine1;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitle1;
        private System.Windows.Forms.ListBox listBoxFormats;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblTitle2;
        private System.Windows.Forms.Label lblFile1;
        private System.Windows.Forms.TextBox txtFile1;
        private System.Windows.Forms.Button btnBrowse1;
        private System.Windows.Forms.TextBox txtFile3;
        private System.Windows.Forms.TextBox txtFile2;
        private System.Windows.Forms.Label lblFile3;
        private System.Windows.Forms.Button btnBrowse3;
        private System.Windows.Forms.Label lblFile2;
        private System.Windows.Forms.Button btnBrowse2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblTitle3;
        private System.Windows.Forms.TreeView treePreview;
        private System.Windows.Forms.GroupBox boxStreamDetails;
        private System.Windows.Forms.TextBox txtTreeDetails;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblTitle4;
        private System.Windows.Forms.Label lblIgnored;
        private System.Windows.Forms.Label lblRequired;
        private System.Windows.Forms.ListView listViewRequired;
        private System.Windows.Forms.ColumnHeader Code;
        private System.Windows.Forms.ColumnHeader TypeName;
        private System.Windows.Forms.Button btnRequire;
        private System.Windows.Forms.Button btnIgnore;
        private System.Windows.Forms.ListView listViewIgnored;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label lblClashNotice;
        private System.Windows.Forms.TextBox txtFileInstructions;
        private EtchedLine etchedLine2;
        private TimetableControl timetableControl1;
    }
}