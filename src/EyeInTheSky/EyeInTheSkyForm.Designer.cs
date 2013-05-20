namespace EyeInTheSky
{
    partial class EyeInTheSkyForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EyeInTheSkyForm));
            this.buttonQuit = new System.Windows.Forms.Button();
            this.timerYachtUpdate = new System.Windows.Forms.Timer(this.components);
            this.listViewWaypoints = new System.Windows.Forms.ListView();
            this.columnHeaderOrder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLatitude = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLongitude = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.checkBoxCentreOnYacht = new System.Windows.Forms.CheckBox();
            this.imageListYacht = new System.Windows.Forms.ImageList(this.components);
            this.checkBoxZoom = new System.Windows.Forms.CheckBox();
            this.labelIconSize = new System.Windows.Forms.Label();
            this.trackBarIconSize = new System.Windows.Forms.TrackBar();
            this.listBoxYachts = new System.Windows.Forms.ListBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.openFileDialogFvw = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarIconSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonQuit
            // 
            this.buttonQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonQuit.Location = new System.Drawing.Point(405, 231);
            this.buttonQuit.Name = "buttonQuit";
            this.buttonQuit.Size = new System.Drawing.Size(75, 23);
            this.buttonQuit.TabIndex = 100;
            this.buttonQuit.Text = "Quit";
            this.buttonQuit.UseVisualStyleBackColor = true;
            this.buttonQuit.Click += new System.EventHandler(this.buttonQuit_Click);
            // 
            // timerYachtUpdate
            // 
            this.timerYachtUpdate.Interval = 1000;
            this.timerYachtUpdate.Tick += new System.EventHandler(this.timerYachtUpdate_Tick);
            // 
            // listViewWaypoints
            // 
            this.listViewWaypoints.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderOrder,
            this.columnHeaderLatitude,
            this.columnHeaderLongitude});
            this.listViewWaypoints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewWaypoints.FullRowSelect = true;
            this.listViewWaypoints.GridLines = true;
            this.listViewWaypoints.Location = new System.Drawing.Point(0, 0);
            this.listViewWaypoints.Name = "listViewWaypoints";
            this.listViewWaypoints.Size = new System.Drawing.Size(315, 209);
            this.listViewWaypoints.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewWaypoints.TabIndex = 4;
            this.listViewWaypoints.Tag = "Numeric";
            this.listViewWaypoints.UseCompatibleStateImageBehavior = false;
            this.listViewWaypoints.View = System.Windows.Forms.View.Details;
            this.listViewWaypoints.SelectedIndexChanged += new System.EventHandler(this.listViewWaypoints_SelectedIndexChanged);
            // 
            // columnHeaderOrder
            // 
            this.columnHeaderOrder.Tag = "Numeric";
            this.columnHeaderOrder.Text = "Order";
            this.columnHeaderOrder.Width = 48;
            // 
            // columnHeaderLatitude
            // 
            this.columnHeaderLatitude.Tag = "Numeric";
            this.columnHeaderLatitude.Text = "Latitude";
            this.columnHeaderLatitude.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeaderLatitude.Width = 92;
            // 
            // columnHeaderLongitude
            // 
            this.columnHeaderLongitude.Tag = "Numeric";
            this.columnHeaderLongitude.Text = "Longitude";
            this.columnHeaderLongitude.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeaderLongitude.Width = 106;
            // 
            // checkBoxCentreOnYacht
            // 
            this.checkBoxCentreOnYacht.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxCentreOnYacht.AutoSize = true;
            this.checkBoxCentreOnYacht.Location = new System.Drawing.Point(12, 231);
            this.checkBoxCentreOnYacht.Name = "checkBoxCentreOnYacht";
            this.checkBoxCentreOnYacht.Size = new System.Drawing.Size(101, 17);
            this.checkBoxCentreOnYacht.TabIndex = 1;
            this.checkBoxCentreOnYacht.Text = "Centre on yacht";
            this.checkBoxCentreOnYacht.UseVisualStyleBackColor = true;
            this.checkBoxCentreOnYacht.CheckedChanged += new System.EventHandler(this.checkBoxCentreOnYacht_CheckedChanged);
            // 
            // imageListYacht
            // 
            this.imageListYacht.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListYacht.ImageStream")));
            this.imageListYacht.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListYacht.Images.SetKeyName(0, "Yacht");
            this.imageListYacht.Images.SetKeyName(1, "Sail");
            this.imageListYacht.Images.SetKeyName(2, "Wind");
            // 
            // checkBoxZoom
            // 
            this.checkBoxZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxZoom.AutoSize = true;
            this.checkBoxZoom.Location = new System.Drawing.Point(119, 231);
            this.checkBoxZoom.Name = "checkBoxZoom";
            this.checkBoxZoom.Size = new System.Drawing.Size(53, 17);
            this.checkBoxZoom.TabIndex = 2;
            this.checkBoxZoom.Text = "Zoom";
            this.checkBoxZoom.UseVisualStyleBackColor = true;
            // 
            // labelIconSize
            // 
            this.labelIconSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelIconSize.AutoSize = true;
            this.labelIconSize.Location = new System.Drawing.Point(288, 232);
            this.labelIconSize.Name = "labelIconSize";
            this.labelIconSize.Size = new System.Drawing.Size(49, 13);
            this.labelIconSize.TabIndex = 13;
            this.labelIconSize.Text = "Icon size";
            // 
            // trackBarIconSize
            // 
            this.trackBarIconSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.trackBarIconSize.LargeChange = 2;
            this.trackBarIconSize.Location = new System.Drawing.Point(178, 231);
            this.trackBarIconSize.Maximum = 16;
            this.trackBarIconSize.Name = "trackBarIconSize";
            this.trackBarIconSize.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.trackBarIconSize.Size = new System.Drawing.Size(104, 45);
            this.trackBarIconSize.TabIndex = 3;
            this.trackBarIconSize.TickFrequency = 2;
            this.trackBarIconSize.Scroll += new System.EventHandler(this.trackBarIconSize_Scroll);
            // 
            // listBoxYachts
            // 
            this.listBoxYachts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxYachts.FormattingEnabled = true;
            this.listBoxYachts.Location = new System.Drawing.Point(0, 0);
            this.listBoxYachts.Name = "listBoxYachts";
            this.listBoxYachts.Size = new System.Drawing.Size(141, 209);
            this.listBoxYachts.TabIndex = 101;
            this.listBoxYachts.SelectedIndexChanged += new System.EventHandler(this.listBoxYachts_SelectedIndexChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listBoxYachts);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listViewWaypoints);
            this.splitContainer1.Size = new System.Drawing.Size(468, 213);
            this.splitContainer1.SplitterDistance = 145;
            this.splitContainer1.TabIndex = 8;
            // 
            // openFileDialogFvw
            // 
            this.openFileDialogFvw.FileName = "fvw.exe";
            this.openFileDialogFvw.Filter = "FalconView|fvw.exe";
            this.openFileDialogFvw.InitialDirectory = "C:\\";
            // 
            // EyeInTheSkyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 266);
            this.Controls.Add(this.trackBarIconSize);
            this.Controls.Add(this.labelIconSize);
            this.Controls.Add(this.checkBoxZoom);
            this.Controls.Add(this.checkBoxCentreOnYacht);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.buttonQuit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(500, 300);
            this.Name = "EyeInTheSkyForm";
            this.Text = "Eye in the sky";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EyeInTheSkyForm_FormClosing);
            this.Load += new System.EventHandler(this.EyeInTheSkyForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarIconSize)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonQuit;
        private System.Windows.Forms.Timer timerYachtUpdate;
        private System.Windows.Forms.ListView listViewWaypoints;
        private System.Windows.Forms.ColumnHeader columnHeaderOrder;
        private System.Windows.Forms.ColumnHeader columnHeaderLatitude;
        private System.Windows.Forms.ColumnHeader columnHeaderLongitude;
        private System.Windows.Forms.CheckBox checkBoxCentreOnYacht;
        private System.Windows.Forms.ImageList imageListYacht;
        private System.Windows.Forms.CheckBox checkBoxZoom;
        private System.Windows.Forms.Label labelIconSize;
        private System.Windows.Forms.TrackBar trackBarIconSize;
        private System.Windows.Forms.ListBox listBoxYachts;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.OpenFileDialog openFileDialogFvw;
    }
}

