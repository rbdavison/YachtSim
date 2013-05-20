namespace YachtSim
{
    partial class YachtSimServerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(YachtSimServerForm));
            this.buttonQuit = new System.Windows.Forms.Button();
            this.numericUpDownWindSpeed = new System.Windows.Forms.NumericUpDown();
            this.labelWindSpeed = new System.Windows.Forms.Label();
            this.numericUpDownWindDirection = new System.Windows.Forms.NumericUpDown();
            this.labelWindDirection = new System.Windows.Forms.Label();
            this.buttonRemoveYacht = new System.Windows.Forms.Button();
            this.numericUpDownWindVariability = new System.Windows.Forms.NumericUpDown();
            this.listViewYachts = new System.Windows.Forms.ListView();
            this.columnHeaderId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWindSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWindDirection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWindVariability)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonQuit
            // 
            this.buttonQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonQuit.Location = new System.Drawing.Point(345, 231);
            this.buttonQuit.Name = "buttonQuit";
            this.buttonQuit.Size = new System.Drawing.Size(75, 23);
            this.buttonQuit.TabIndex = 0;
            this.buttonQuit.Text = "Quit";
            this.buttonQuit.UseVisualStyleBackColor = true;
            this.buttonQuit.Click += new System.EventHandler(this.buttonQuit_Click);
            // 
            // numericUpDownWindSpeed
            // 
            this.numericUpDownWindSpeed.Location = new System.Drawing.Point(12, 15);
            this.numericUpDownWindSpeed.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDownWindSpeed.Name = "numericUpDownWindSpeed";
            this.numericUpDownWindSpeed.Size = new System.Drawing.Size(46, 20);
            this.numericUpDownWindSpeed.TabIndex = 1;
            this.numericUpDownWindSpeed.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownWindSpeed.ValueChanged += new System.EventHandler(this.numericUpDownWindSpeed_ValueChanged);
            // 
            // labelWindSpeed
            // 
            this.labelWindSpeed.AutoSize = true;
            this.labelWindSpeed.Location = new System.Drawing.Point(64, 17);
            this.labelWindSpeed.Name = "labelWindSpeed";
            this.labelWindSpeed.Size = new System.Drawing.Size(93, 13);
            this.labelWindSpeed.TabIndex = 2;
            this.labelWindSpeed.Text = "Wind Speed (m/s)";
            // 
            // numericUpDownWindDirection
            // 
            this.numericUpDownWindDirection.Location = new System.Drawing.Point(12, 44);
            this.numericUpDownWindDirection.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numericUpDownWindDirection.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericUpDownWindDirection.Name = "numericUpDownWindDirection";
            this.numericUpDownWindDirection.Size = new System.Drawing.Size(45, 20);
            this.numericUpDownWindDirection.TabIndex = 3;
            this.numericUpDownWindDirection.ValueChanged += new System.EventHandler(this.numericUpDownWindDirection_ValueChanged);
            // 
            // labelWindDirection
            // 
            this.labelWindDirection.AutoSize = true;
            this.labelWindDirection.Location = new System.Drawing.Point(63, 46);
            this.labelWindDirection.Name = "labelWindDirection";
            this.labelWindDirection.Size = new System.Drawing.Size(126, 13);
            this.labelWindDirection.TabIndex = 4;
            this.labelWindDirection.Text = "Wind Direction (Degrees)";
            // 
            // buttonRemoveYacht
            // 
            this.buttonRemoveYacht.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRemoveYacht.Location = new System.Drawing.Point(12, 231);
            this.buttonRemoveYacht.Name = "buttonRemoveYacht";
            this.buttonRemoveYacht.Size = new System.Drawing.Size(91, 23);
            this.buttonRemoveYacht.TabIndex = 9;
            this.buttonRemoveYacht.Text = "Remove Yacht";
            this.buttonRemoveYacht.UseVisualStyleBackColor = true;
            this.buttonRemoveYacht.Click += new System.EventHandler(this.buttonRemoveYacht_Click);
            // 
            // numericUpDownWindVariability
            // 
            this.numericUpDownWindVariability.Location = new System.Drawing.Point(12, 72);
            this.numericUpDownWindVariability.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.numericUpDownWindVariability.Name = "numericUpDownWindVariability";
            this.numericUpDownWindVariability.Size = new System.Drawing.Size(46, 20);
            this.numericUpDownWindVariability.TabIndex = 11;
            this.numericUpDownWindVariability.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownWindVariability.ValueChanged += new System.EventHandler(this.numericUpDownWindVariability_ValueChanged);
            // 
            // listViewYachts
            // 
            this.listViewYachts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewYachts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderId,
            this.columnHeaderType,
            this.columnHeaderName});
            this.listViewYachts.FullRowSelect = true;
            this.listViewYachts.GridLines = true;
            this.listViewYachts.Location = new System.Drawing.Point(12, 98);
            this.listViewYachts.Name = "listViewYachts";
            this.listViewYachts.Size = new System.Drawing.Size(408, 127);
            this.listViewYachts.TabIndex = 12;
            this.listViewYachts.UseCompatibleStateImageBehavior = false;
            this.listViewYachts.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderId
            // 
            this.columnHeaderId.Text = "Yacht Id";
            // 
            // columnHeaderType
            // 
            this.columnHeaderType.Text = "Simulator";
            this.columnHeaderType.Width = 100;
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Name";
            this.columnHeaderName.Width = 242;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Wind Direction Variability (Degrees)";
            // 
            // YachtSimServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 266);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listViewYachts);
            this.Controls.Add(this.numericUpDownWindVariability);
            this.Controls.Add(this.buttonRemoveYacht);
            this.Controls.Add(this.labelWindDirection);
            this.Controls.Add(this.numericUpDownWindDirection);
            this.Controls.Add(this.labelWindSpeed);
            this.Controls.Add(this.numericUpDownWindSpeed);
            this.Controls.Add(this.buttonQuit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "YachtSimServerForm";
            this.Text = "Sailing Simulator Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.YachtSimServerForm_FormClosing);
            this.Load += new System.EventHandler(this.YachtSimServerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWindSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWindDirection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWindVariability)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonQuit;
        private System.Windows.Forms.NumericUpDown numericUpDownWindSpeed;
        private System.Windows.Forms.Label labelWindSpeed;
        private System.Windows.Forms.NumericUpDown numericUpDownWindDirection;
        private System.Windows.Forms.Label labelWindDirection;
        private System.Windows.Forms.Button buttonRemoveYacht;
        private System.Windows.Forms.NumericUpDown numericUpDownWindVariability;
        private System.Windows.Forms.ListView listViewYachts;
        private System.Windows.Forms.ColumnHeader columnHeaderId;
        private System.Windows.Forms.ColumnHeader columnHeaderType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
    }
}

