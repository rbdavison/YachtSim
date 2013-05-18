using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Geospatial;
using System.Net.Sockets;
using System.Net;
using System.Configuration;

namespace YachtSim
{
    public partial class YachtSimServerForm : Form
    {
        #region Constructors

        private YachtSimServer _simSail = YachtSimServer.GetInstance;

        public YachtSimServerForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Windows forms event handlers

        private void buttonRemoveYacht_Click(object sender, EventArgs e)
        {
            if (listViewYachts.SelectedItems.Count == 1)
            {
                int yacht = Convert.ToInt32(listViewYachts.SelectedItems[0].Text);
                _simSail.RemoveYacht(yacht);
                listViewYachts.Items.Remove(listViewYachts.SelectedItems[0]);
            }
        }

        private void buttonQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void numericUpDownWindSpeed_ValueChanged(object sender, EventArgs e)
        {
            _simSail.Wind.Velocity = Convert.ToDouble(numericUpDownWindSpeed.Value);
        }

        private void numericUpDownWindDirection_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownWindDirection.Value == 360)
            {
                numericUpDownWindDirection.Value = 0;
            }
            if (numericUpDownWindDirection.Value == -1)
            {
                numericUpDownWindDirection.Value = 359;
            }

            _simSail.Wind.Direction = Angle.FromDegrees(Convert.ToDouble(numericUpDownWindDirection.Value));
        }

        private void numericUpDownWindVariability_ValueChanged(object sender, EventArgs e)
        {
            _simSail.Wind.DirectionVariability = Convert.ToInt32(numericUpDownWindVariability.Value);
        }

        private void timerYachtsUpdater_Tick(object sender, EventArgs e)
        {
            UpdateYachtList();
        }

        private void YachtSimServerForm_Load(object sender, EventArgs e)
        {
            numericUpDownWindDirection.Value = Convert.ToDecimal(_simSail.Wind.Direction.Degrees);
            numericUpDownWindSpeed.Value = Convert.ToDecimal(_simSail.Wind.Velocity);
            numericUpDownWindVariability.Value = Convert.ToDecimal(_simSail.Wind.DirectionVariability);
            timerYachtsUpdater.Start();
        }

        private void YachtSimServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Quit();
        }

        #endregion

        #region Methods

        private void Quit()
        {
            TcpClient client = new TcpClient();
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(ConfigurationManager.AppSettings["ServerIP"]), Convert.ToInt32(ConfigurationManager.AppSettings["ServerPort"]));
            client.Connect(serverEndPoint);
            NetworkStream clientStream = client.GetStream();
            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] buffer = encoder.GetBytes("QUIT");
            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();
            clientStream.Close();
            client.Close();
            System.Threading.Thread.Sleep(100);
            client = new TcpClient();
            client.Connect(serverEndPoint);
            clientStream = client.GetStream();
            encoder = new ASCIIEncoding();
            buffer = encoder.GetBytes("QUIT");
            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();
            clientStream.Close();
            client.Close();

            _simSail.Dispose();
        }

        private void UpdateYachtList()
        {
            if (InvokeRequired)
            {
                MethodInvoker invoker = delegate { UpdateYachtList(); };
                this.BeginInvoke(invoker);
                return;
            }
            listViewYachts.Items.Clear();
            foreach (var yacht in YachtSimServer.GetInstance.Yachts.ToArray())
            {
                ListViewItem item = listViewYachts.Items.Add(yacht.Id.ToString());
                item.SubItems.Add(yacht.SimulatorName);
                item.SubItems.Add(yacht.Name);
            }
        }

        #endregion
    }
}
