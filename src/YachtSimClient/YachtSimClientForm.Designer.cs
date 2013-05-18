namespace SailSimClient
{
    partial class YachtSimClientForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(YachtSimClientForm));
            this.buttonQuit = new System.Windows.Forms.Button();
            this.buttonAddYacht = new System.Windows.Forms.Button();
            this.comboBoxYachtTypes = new System.Windows.Forms.ComboBox();
            this.timerYachtUpdater = new System.Windows.Forms.Timer(this.components);
            this.labelHeading = new System.Windows.Forms.Label();
            this.labelHeeling = new System.Windows.Forms.Label();
            this.labelLatitude = new System.Windows.Forms.Label();
            this.labelLongitude = new System.Windows.Forms.Label();
            this.buttonRemoveYacht = new System.Windows.Forms.Button();
            this.labelSetSailAngle = new System.Windows.Forms.Label();
            this.labelSetRudderAngle = new System.Windows.Forms.Label();
            this.labelWindAngle = new System.Windows.Forms.Label();
            this.labelWindSpeed = new System.Windows.Forms.Label();
            this.labelSailDirection = new System.Windows.Forms.Label();
            this.pictureBoxYacht = new System.Windows.Forms.PictureBox();
            this.imageListYacht = new System.Windows.Forms.ImageList(this.components);
            this.textBoxLatitude = new System.Windows.Forms.TextBox();
            this.textBoxLongitude = new System.Windows.Forms.TextBox();
            this.trackBarRudder = new System.Windows.Forms.TrackBar();
            this.trackBarSailAngle = new System.Windows.Forms.TrackBar();
            this.listViewWaypoints = new System.Windows.Forms.ListView();
            this.columnHeaderRemove = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLatitude = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLongitude = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBoxWaypoints = new System.Windows.Forms.GroupBox();
            this.buttonUpdateWaypoints = new System.Windows.Forms.Button();
            this.buttonRemoveWaypoint = new System.Windows.Forms.Button();
            this.buttonAddWaypoint = new System.Windows.Forms.Button();
            this.labelTime = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxYacht)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRudder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSailAngle)).BeginInit();
            this.groupBoxWaypoints.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonQuit
            // 
            this.buttonQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonQuit.Location = new System.Drawing.Point(468, 255);
            this.buttonQuit.Name = "buttonQuit";
            this.buttonQuit.Size = new System.Drawing.Size(75, 23);
            this.buttonQuit.TabIndex = 0;
            this.buttonQuit.Text = "Quit";
            this.buttonQuit.UseVisualStyleBackColor = true;
            this.buttonQuit.Click += new System.EventHandler(this.buttonQuit_Click);
            // 
            // buttonAddYacht
            // 
            this.buttonAddYacht.Location = new System.Drawing.Point(132, 10);
            this.buttonAddYacht.Name = "buttonAddYacht";
            this.buttonAddYacht.Size = new System.Drawing.Size(75, 23);
            this.buttonAddYacht.TabIndex = 1;
            this.buttonAddYacht.Text = "Add Yacht";
            this.buttonAddYacht.UseVisualStyleBackColor = true;
            this.buttonAddYacht.Click += new System.EventHandler(this.buttonAddYacht_Click);
            // 
            // comboBoxYachtTypes
            // 
            this.comboBoxYachtTypes.FormattingEnabled = true;
            this.comboBoxYachtTypes.Location = new System.Drawing.Point(12, 12);
            this.comboBoxYachtTypes.Name = "comboBoxYachtTypes";
            this.comboBoxYachtTypes.Size = new System.Drawing.Size(114, 21);
            this.comboBoxYachtTypes.TabIndex = 2;
            // 
            // timerYachtUpdater
            // 
            this.timerYachtUpdater.Tick += new System.EventHandler(this.timerYachtUpdater_Tick);
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.Location = new System.Drawing.Point(12, 91);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(59, 13);
            this.labelHeading.TabIndex = 3;
            this.labelHeading.Text = "Heading: 0";
            // 
            // labelHeeling
            // 
            this.labelHeeling.AutoSize = true;
            this.labelHeeling.Location = new System.Drawing.Point(12, 113);
            this.labelHeeling.Name = "labelHeeling";
            this.labelHeeling.Size = new System.Drawing.Size(55, 13);
            this.labelHeeling.TabIndex = 4;
            this.labelHeeling.Text = "Heeling: 0";
            // 
            // labelLatitude
            // 
            this.labelLatitude.AutoSize = true;
            this.labelLatitude.Location = new System.Drawing.Point(16, 42);
            this.labelLatitude.Name = "labelLatitude";
            this.labelLatitude.Size = new System.Drawing.Size(45, 13);
            this.labelLatitude.TabIndex = 5;
            this.labelLatitude.Text = "Latitude";
            // 
            // labelLongitude
            // 
            this.labelLongitude.AutoSize = true;
            this.labelLongitude.Location = new System.Drawing.Point(7, 68);
            this.labelLongitude.Name = "labelLongitude";
            this.labelLongitude.Size = new System.Drawing.Size(54, 13);
            this.labelLongitude.TabIndex = 6;
            this.labelLongitude.Text = "Longitude";
            // 
            // buttonRemoveYacht
            // 
            this.buttonRemoveYacht.Location = new System.Drawing.Point(213, 10);
            this.buttonRemoveYacht.Name = "buttonRemoveYacht";
            this.buttonRemoveYacht.Size = new System.Drawing.Size(75, 23);
            this.buttonRemoveYacht.TabIndex = 9;
            this.buttonRemoveYacht.Text = "Remove";
            this.buttonRemoveYacht.UseVisualStyleBackColor = true;
            this.buttonRemoveYacht.Click += new System.EventHandler(this.buttonRemoveYacht_Click);
            // 
            // labelSetSailAngle
            // 
            this.labelSetSailAngle.AutoSize = true;
            this.labelSetSailAngle.Location = new System.Drawing.Point(200, 182);
            this.labelSetSailAngle.Name = "labelSetSailAngle";
            this.labelSetSailAngle.Size = new System.Drawing.Size(54, 13);
            this.labelSetSailAngle.TabIndex = 10;
            this.labelSetSailAngle.Text = "Sail Angle";
            // 
            // labelSetRudderAngle
            // 
            this.labelSetRudderAngle.AutoSize = true;
            this.labelSetRudderAngle.Location = new System.Drawing.Point(200, 233);
            this.labelSetRudderAngle.Name = "labelSetRudderAngle";
            this.labelSetRudderAngle.Size = new System.Drawing.Size(72, 13);
            this.labelSetRudderAngle.TabIndex = 11;
            this.labelSetRudderAngle.Text = "Rudder Angle";
            // 
            // labelWindAngle
            // 
            this.labelWindAngle.AutoSize = true;
            this.labelWindAngle.Location = new System.Drawing.Point(12, 135);
            this.labelWindAngle.Name = "labelWindAngle";
            this.labelWindAngle.Size = new System.Drawing.Size(120, 13);
            this.labelWindAngle.TabIndex = 12;
            this.labelWindAngle.Text = "Apparent Wind Angle: 0";
            // 
            // labelWindSpeed
            // 
            this.labelWindSpeed.AutoSize = true;
            this.labelWindSpeed.Location = new System.Drawing.Point(12, 157);
            this.labelWindSpeed.Name = "labelWindSpeed";
            this.labelWindSpeed.Size = new System.Drawing.Size(124, 13);
            this.labelWindSpeed.TabIndex = 13;
            this.labelWindSpeed.Text = "Apparent Wind Speed: 0";
            // 
            // labelSailDirection
            // 
            this.labelSailDirection.AutoSize = true;
            this.labelSailDirection.Location = new System.Drawing.Point(206, 135);
            this.labelSailDirection.Name = "labelSailDirection";
            this.labelSailDirection.Size = new System.Drawing.Size(43, 13);
            this.labelSailDirection.TabIndex = 14;
            this.labelSailDirection.Text = "Sail Dir:";
            // 
            // pictureBoxYacht
            // 
            this.pictureBoxYacht.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBoxYacht.Location = new System.Drawing.Point(201, 65);
            this.pictureBoxYacht.Name = "pictureBoxYacht";
            this.pictureBoxYacht.Size = new System.Drawing.Size(48, 48);
            this.pictureBoxYacht.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxYacht.TabIndex = 15;
            this.pictureBoxYacht.TabStop = false;
            // 
            // imageListYacht
            // 
            this.imageListYacht.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListYacht.ImageStream")));
            this.imageListYacht.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListYacht.Images.SetKeyName(0, "Yacht");
            this.imageListYacht.Images.SetKeyName(1, "Sail");
            this.imageListYacht.Images.SetKeyName(2, "Wind");
            // 
            // textBoxLatitude
            // 
            this.textBoxLatitude.Location = new System.Drawing.Point(67, 39);
            this.textBoxLatitude.Name = "textBoxLatitude";
            this.textBoxLatitude.Size = new System.Drawing.Size(100, 20);
            this.textBoxLatitude.TabIndex = 16;
            this.textBoxLatitude.Text = "0";
            // 
            // textBoxLongitude
            // 
            this.textBoxLongitude.Location = new System.Drawing.Point(67, 65);
            this.textBoxLongitude.Name = "textBoxLongitude";
            this.textBoxLongitude.Size = new System.Drawing.Size(100, 20);
            this.textBoxLongitude.TabIndex = 17;
            this.textBoxLongitude.Text = "0";
            // 
            // trackBarRudder
            // 
            this.trackBarRudder.LargeChange = 10;
            this.trackBarRudder.Location = new System.Drawing.Point(12, 233);
            this.trackBarRudder.Maximum = 90;
            this.trackBarRudder.Minimum = -90;
            this.trackBarRudder.Name = "trackBarRudder";
            this.trackBarRudder.Size = new System.Drawing.Size(182, 45);
            this.trackBarRudder.TabIndex = 18;
            this.trackBarRudder.TickFrequency = 30;
            this.trackBarRudder.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarRudder.Scroll += new System.EventHandler(this.trackBarRudder_Scroll);
            // 
            // trackBarSailAngle
            // 
            this.trackBarSailAngle.Location = new System.Drawing.Point(12, 182);
            this.trackBarSailAngle.Maximum = 90;
            this.trackBarSailAngle.Name = "trackBarSailAngle";
            this.trackBarSailAngle.Size = new System.Drawing.Size(182, 45);
            this.trackBarSailAngle.TabIndex = 19;
            this.trackBarSailAngle.TickFrequency = 10;
            this.trackBarSailAngle.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarSailAngle.Value = 30;
            this.trackBarSailAngle.Scroll += new System.EventHandler(this.trackBarSailAngle_Scroll);
            // 
            // listViewWaypoints
            // 
            this.listViewWaypoints.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewWaypoints.CheckBoxes = true;
            this.listViewWaypoints.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderRemove,
            this.columnHeaderLatitude,
            this.columnHeaderLongitude});
            this.listViewWaypoints.FullRowSelect = true;
            this.listViewWaypoints.GridLines = true;
            this.listViewWaypoints.Location = new System.Drawing.Point(6, 19);
            this.listViewWaypoints.MultiSelect = false;
            this.listViewWaypoints.Name = "listViewWaypoints";
            this.listViewWaypoints.Size = new System.Drawing.Size(237, 183);
            this.listViewWaypoints.TabIndex = 20;
            this.listViewWaypoints.UseCompatibleStateImageBehavior = false;
            this.listViewWaypoints.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderRemove
            // 
            this.columnHeaderRemove.Text = "";
            this.columnHeaderRemove.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeaderRemove.Width = 19;
            // 
            // columnHeaderLatitude
            // 
            this.columnHeaderLatitude.Text = "Latitude";
            this.columnHeaderLatitude.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeaderLatitude.Width = 100;
            // 
            // columnHeaderLongitude
            // 
            this.columnHeaderLongitude.Text = "Longitude";
            this.columnHeaderLongitude.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeaderLongitude.Width = 100;
            // 
            // groupBoxWaypoints
            // 
            this.groupBoxWaypoints.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxWaypoints.Controls.Add(this.buttonUpdateWaypoints);
            this.groupBoxWaypoints.Controls.Add(this.buttonRemoveWaypoint);
            this.groupBoxWaypoints.Controls.Add(this.buttonAddWaypoint);
            this.groupBoxWaypoints.Controls.Add(this.listViewWaypoints);
            this.groupBoxWaypoints.Location = new System.Drawing.Point(294, 12);
            this.groupBoxWaypoints.Name = "groupBoxWaypoints";
            this.groupBoxWaypoints.Size = new System.Drawing.Size(249, 237);
            this.groupBoxWaypoints.TabIndex = 21;
            this.groupBoxWaypoints.TabStop = false;
            this.groupBoxWaypoints.Text = "Waypoints";
            // 
            // buttonUpdateWaypoints
            // 
            this.buttonUpdateWaypoints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonUpdateWaypoints.Location = new System.Drawing.Point(87, 208);
            this.buttonUpdateWaypoints.Name = "buttonUpdateWaypoints";
            this.buttonUpdateWaypoints.Size = new System.Drawing.Size(75, 23);
            this.buttonUpdateWaypoints.TabIndex = 23;
            this.buttonUpdateWaypoints.Text = "Update";
            this.buttonUpdateWaypoints.UseVisualStyleBackColor = true;
            this.buttonUpdateWaypoints.Click += new System.EventHandler(this.buttonUpdateWaypoints_Click);
            // 
            // buttonRemoveWaypoint
            // 
            this.buttonRemoveWaypoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemoveWaypoint.Location = new System.Drawing.Point(168, 208);
            this.buttonRemoveWaypoint.Name = "buttonRemoveWaypoint";
            this.buttonRemoveWaypoint.Size = new System.Drawing.Size(75, 23);
            this.buttonRemoveWaypoint.TabIndex = 22;
            this.buttonRemoveWaypoint.Text = "Remove";
            this.buttonRemoveWaypoint.UseVisualStyleBackColor = true;
            this.buttonRemoveWaypoint.Click += new System.EventHandler(this.buttonRemoveWaypoint_Click);
            // 
            // buttonAddWaypoint
            // 
            this.buttonAddWaypoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddWaypoint.Location = new System.Drawing.Point(6, 208);
            this.buttonAddWaypoint.Name = "buttonAddWaypoint";
            this.buttonAddWaypoint.Size = new System.Drawing.Size(75, 23);
            this.buttonAddWaypoint.TabIndex = 21;
            this.buttonAddWaypoint.Text = "Add";
            this.buttonAddWaypoint.UseVisualStyleBackColor = true;
            this.buttonAddWaypoint.Click += new System.EventHandler(this.buttonAddWaypoint_Click);
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Location = new System.Drawing.Point(173, 42);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(36, 13);
            this.labelTime.TabIndex = 22;
            this.labelTime.Text = "Time: ";
            // 
            // YachtSimClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 290);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.groupBoxWaypoints);
            this.Controls.Add(this.trackBarRudder);
            this.Controls.Add(this.textBoxLongitude);
            this.Controls.Add(this.textBoxLatitude);
            this.Controls.Add(this.pictureBoxYacht);
            this.Controls.Add(this.labelSailDirection);
            this.Controls.Add(this.labelWindSpeed);
            this.Controls.Add(this.labelWindAngle);
            this.Controls.Add(this.labelSetRudderAngle);
            this.Controls.Add(this.labelSetSailAngle);
            this.Controls.Add(this.buttonRemoveYacht);
            this.Controls.Add(this.labelLongitude);
            this.Controls.Add(this.labelLatitude);
            this.Controls.Add(this.labelHeeling);
            this.Controls.Add(this.labelHeading);
            this.Controls.Add(this.comboBoxYachtTypes);
            this.Controls.Add(this.buttonAddYacht);
            this.Controls.Add(this.buttonQuit);
            this.Controls.Add(this.trackBarSailAngle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(563, 324);
            this.Name = "YachtSimClientForm";
            this.Text = "Sailing Simulator Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SailSimClientForm_FormClosing);
            this.Load += new System.EventHandler(this.SailSimClientForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxYacht)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRudder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSailAngle)).EndInit();
            this.groupBoxWaypoints.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonQuit;
        private System.Windows.Forms.Button buttonAddYacht;
        private System.Windows.Forms.ComboBox comboBoxYachtTypes;
        private System.Windows.Forms.Timer timerYachtUpdater;
        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.Label labelHeeling;
        private System.Windows.Forms.Label labelLatitude;
        private System.Windows.Forms.Label labelLongitude;
        private System.Windows.Forms.Button buttonRemoveYacht;
        private System.Windows.Forms.Label labelSetSailAngle;
        private System.Windows.Forms.Label labelSetRudderAngle;
        private System.Windows.Forms.Label labelWindAngle;
        private System.Windows.Forms.Label labelWindSpeed;
        private System.Windows.Forms.Label labelSailDirection;
        private System.Windows.Forms.PictureBox pictureBoxYacht;
        private System.Windows.Forms.ImageList imageListYacht;
        private System.Windows.Forms.TextBox textBoxLatitude;
        private System.Windows.Forms.TextBox textBoxLongitude;
        private System.Windows.Forms.TrackBar trackBarRudder;
        private System.Windows.Forms.TrackBar trackBarSailAngle;
        private System.Windows.Forms.ListView listViewWaypoints;
        private System.Windows.Forms.ColumnHeader columnHeaderLatitude;
        private System.Windows.Forms.ColumnHeader columnHeaderLongitude;
        private System.Windows.Forms.GroupBox groupBoxWaypoints;
        private System.Windows.Forms.Button buttonRemoveWaypoint;
        private System.Windows.Forms.Button buttonAddWaypoint;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Button buttonUpdateWaypoints;
        private System.Windows.Forms.ColumnHeader columnHeaderRemove;
    }
}

