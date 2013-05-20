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

        private YachtSimServer yachtSimServer = YachtSimServer.GetInstance;

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
                yachtSimServer.RemoveYacht(yacht);
                UpdateYachtList();
            }
        }

        private void buttonQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void numericUpDownWindSpeed_ValueChanged(object sender, EventArgs e)
        {
            yachtSimServer.Wind.Velocity = Convert.ToDouble(numericUpDownWindSpeed.Value);
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

            yachtSimServer.Wind.Direction = Angle.FromDegrees(Convert.ToDouble(numericUpDownWindDirection.Value));
        }

        private void numericUpDownWindVariability_ValueChanged(object sender, EventArgs e)
        {
            yachtSimServer.Wind.DirectionVariability = Convert.ToInt32(numericUpDownWindVariability.Value);
        }

        private void yachtSimServer_onYachtListUpdated()
        {
            UpdateYachtList();
        }

        private void YachtSimServerForm_Load(object sender, EventArgs e)
        {
            numericUpDownWindDirection.Value = Convert.ToDecimal(yachtSimServer.Wind.Direction.Degrees);
            numericUpDownWindSpeed.Value = Convert.ToDecimal(yachtSimServer.Wind.Velocity);
            numericUpDownWindVariability.Value = Convert.ToDecimal(yachtSimServer.Wind.DirectionVariability);
            yachtSimServer.onYachtListUpdated += new YachtListUpdatedHandler(yachtSimServer_onYachtListUpdated);
        }

        private void YachtSimServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Quit();
        }

        #endregion

        #region Methods

        private void Quit()
        {
            // We send a quit message to the server because the server is busy listening for client connections.
            TcpClient client = new TcpClient();
            string serverIP = ConfigurationManager.AppSettings["ServerIP"];
            if (string.IsNullOrEmpty(serverIP))
            {
                serverIP = "127.0.0.1";
            }
            string serverPort = ConfigurationManager.AppSettings["ServerPort"];
            if (string.IsNullOrEmpty(serverPort))
            {
                serverPort = "3000";
            }
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), Convert.ToInt32(serverPort));
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

            yachtSimServer.Dispose();
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
