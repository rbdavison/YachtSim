using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SailSimClient
{
    public partial class EditWaypointForm : Form
    {
        public EditWaypointForm()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void EditWaypointForm_Load(object sender, EventArgs e)
        {
            Geospatial.Location location = Geospatial.Location.Parse("0 0");
            if (location != null)
            {
                textBoxLocation.Text = location.ToString();
                this.Tag = location;
            }
        }

        private void textBoxLocation_TextChanged(object sender, EventArgs e)
        {
            Geospatial.Location location;
            if (Geospatial.Location.TryParse(textBoxLocation.Text, out location))
            {
                textBoxLocation.BackColor = Color.White;
                buttonAdd.Enabled = true;
            }
            else
            {
                textBoxLocation.BackColor = Color.Salmon;
                buttonAdd.Enabled = false;
            }
        }

        private void textBoxLocation_Validating(object sender, CancelEventArgs e)
        {
            Geospatial.Location location;
            if (Geospatial.Location.TryParse(textBoxLocation.Text, out location))
            {
                this.Tag = location;
                textBoxLocation.Text = location.ToString();
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
