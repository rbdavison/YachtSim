using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;
using System.Globalization;
using System.IO;
using System.Configuration;

namespace SailSimClient
{
    public partial class YachtSimClientForm : Form
    {
        #region Constructors

        private int thisYachtId;
        private TcpClient server;
        private IPEndPoint serverEndPoint;
        private NetworkStream clientStream;
        private ASCIIEncoding encoder;
        private Geospatial.Location lastLocation;
        private float heading = 0;
        private float sailAngel = 180;
        private float windAngel = 0;
        private Dictionary<int, Geospatial.Location> waypoints = new Dictionary<int, Geospatial.Location>();

        public YachtSimClientForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Windows Forms event handlers

        private void buttonAddYacht_Click(object sender, EventArgs e)
        {
            if (comboBoxYachtTypes.SelectedIndex != -1)
            {
                SendToServer("add " + comboBoxYachtTypes.SelectedItem.ToString());
                timerYachtUpdater.Start();
            }

            SendToServer(thisYachtId + " set name " + comboBoxYachtTypes.SelectedItem.ToString() + " " + thisYachtId.ToString());
        }

        private void buttonAddWaypoint_Click(object sender, EventArgs e)
        {
            using (EditWaypointForm form = new EditWaypointForm())
            {
                form.ShowDialog();
                if (form.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    Geospatial.Location location = form.Tag as Geospatial.Location;
                    if (location != null)
                    {
                        int id = 0;
                        for (int i = 0; i <= waypoints.Count; i++)
                        {
                            if (!waypoints.ContainsKey(i))
                            {
                                id = i;
                                break;
                            }
                        }
                        AddWaypoint(id, location);
                    }
                }
            }
        }

        private void buttonRemoveYacht_Click(object sender, EventArgs e)
        {
            timerYachtUpdater.Stop();
            SendToServer(thisYachtId + " remove yacht");
            buttonAddYacht.Enabled = true;
        }

        private void buttonRemoveWaypoint_Click(object sender, EventArgs e)
        {
            if (listViewWaypoints.CheckedItems.Count > 0)
            {
                foreach (ListViewItem item in listViewWaypoints.CheckedItems)
                {
                    listViewWaypoints.Items.Remove(item);
                    waypoints.Remove((int)item.Tag);
                    SendToServer(thisYachtId + " remove waypoint " + item.Tag.ToString());
                }
            }
        }

        private void buttonQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonUpdateWaypoints_Click(object sender, EventArgs e)
        {
            listViewWaypoints.Items.Clear();
            waypoints.Clear();
            SendToServer(thisYachtId + " get waypoints");
        }

        private void SailSimClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Quit();
        }

        private void SailSimClientForm_Load(object sender, EventArgs e)
        {
            server = new TcpClient();
            serverEndPoint = new IPEndPoint(IPAddress.Parse(ConfigurationManager.AppSettings["ServerIP"]), Convert.ToInt32(ConfigurationManager.AppSettings["ServerPort"]));
            server.Connect(serverEndPoint);
            clientStream = server.GetStream();
            encoder = new ASCIIEncoding();
            SendToServer("SIMULATORS");
        }

        private void timerYachtUpdater_Tick(object sender, EventArgs e)
        {
            SendToServer(thisYachtId + " get heading");
            SendToServer(thisYachtId + " get heeling");
            SendToServer(thisYachtId + " get location");
            SendToServer(thisYachtId + " get saildirection");
            SendToServer(thisYachtId + " get windangle");
            SendToServer(thisYachtId + " get windvelocity");
            UpdateYachtImage();
        }

        private void trackBarRudder_Scroll(object sender, EventArgs e)
        {
            SendToServer(thisYachtId + " set rudder " + trackBarRudder.Value.ToString());
        }

        private void trackBarSailAngle_Scroll(object sender, EventArgs e)
        {
            SendToServer(thisYachtId + " set sail " + trackBarSailAngle.Value.ToString());
        }

        #endregion

        #region Methods

        private void AddWaypoint(int id, Geospatial.Location location)
        {
            SendToServer(thisYachtId + " add waypoint " + id + " " + location.ToString("ISO", CultureInfo.CurrentCulture));
            waypoints.Add(id, location);
            var item = listViewWaypoints.Items.Add("");
            item.SubItems.Add(location.Latitude.ToString());
            item.SubItems.Add(location.Longitude.ToString());
            item.Tag = id;
        }

        private void ProcessCommand(string command)
        {
            try
            {
                int yachtId;
                if (int.TryParse(Regex.Replace(command, " .*", ""), out yachtId))
                {
                    command = Regex.Replace(command, "^" + yachtId + " ", "");
                    switch (Regex.Replace(command, "(.*?) .*", "$1").ToUpperInvariant())
                    {
                        case "HEADING":
                            labelHeading.Text = "Heading: " + Regex.Replace(command, "HEADING ", "", RegexOptions.IgnoreCase);
                            this.heading = (float)Convert.ToDouble(Regex.Replace(command, "HEADING ", "", RegexOptions.IgnoreCase));
                            break;
                        case "HEELING":
                            labelHeeling.Text = "Heeling: " + Regex.Replace(command, "HEELING ", "", RegexOptions.IgnoreCase);
                            break;
                        case "LOCATION":
                            try
                            {
                                Geospatial.Location location = Geospatial.Location.Parse(Regex.Replace(command, "LOCATION ", "", RegexOptions.IgnoreCase), Geospatial.LocationStyles.Iso, CultureInfo.CurrentCulture);
                                if (lastLocation != null)
                                {
                                }
                                lastLocation = location;
                                textBoxLatitude.Text = location.Latitude.ToString();
                                textBoxLongitude.Text = location.Longitude.ToString();
                            }
                            catch { }
                            break;
                        case "NAME":
                            break;
                        case "REMOVED":
                            int id = Convert.ToInt32(Regex.Replace(command, "REMOVED ", "", RegexOptions.IgnoreCase));
                            if (id == yachtId)
                            {
                                yachtId = -1;
                                timerYachtUpdater.Stop();
                                buttonAddYacht.Enabled = true;
                            }
                            break;
                        case "RUDDER":
                            labelSetRudderAngle.Text = "Rudder Angle " + Regex.Replace(command, "RUDDER ", "", RegexOptions.IgnoreCase);
                            break;
                        case "SAIL":
                            labelSetSailAngle.Text = "Sail Angle " + Regex.Replace(command, "SAIL ", "", RegexOptions.IgnoreCase);
                            break;
                        case "SAILDIRECTION":
                            labelSailDirection.Text = "Sail Angle: " + Regex.Replace(command, "SAILDIRECTION ", "", RegexOptions.IgnoreCase);
                            this.sailAngel = (float)Convert.ToDouble(Regex.Replace(command, "SAILDIRECTION ", "", RegexOptions.IgnoreCase));
                            break;
                        case "WAYPOINT":
                            break;
                        case "WAYPOINTS":
                            foreach (var waypoint in Regex.Split(Regex.Replace(command, "WAYPOINTS ", "", RegexOptions.IgnoreCase), ";"))
                            {
                                int waypointId;
                                if (int.TryParse(Regex.Replace(waypoint, "=.*", ""), out waypointId))
                                {
                                    Geospatial.Location location;
                                    if (Geospatial.Location.TryParse(waypoint.Replace(waypointId + "=", ""), Geospatial.LocationStyles.Iso, CultureInfo.CurrentCulture, out location))
                                    {
                                        try
                                        {
                                            waypoints.Add(waypointId, location);
                                            var item = listViewWaypoints.Items.Add("");
                                            item.SubItems.Add(location.Latitude.ToString());
                                            item.SubItems.Add(location.Longitude.ToString());
                                            item.Tag = waypointId;
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                            }
                            break;
                        case "WINDANGLE":
                            labelWindAngle.Text = "Apparent Wind Angle: " + Regex.Replace(command, "WINDANGLE ", "", RegexOptions.IgnoreCase);
                            this.windAngel = (float)Convert.ToDouble(Regex.Replace(command, "WINDANGLE ", "", RegexOptions.IgnoreCase));
                            break;
                        case "WINDVELOCITY":
                            labelWindSpeed.Text = "Apparent Wind Speed: " + Regex.Replace(command, "WINDVELOCITY ", "", RegexOptions.IgnoreCase) + " m/s";
                            break;
                        case "ERROR":
                            break;
                        case "UNKNOWN":
                            break;
                        case "WAYPOINTADDED":
                            break;
                        case "WAYPOINTREMOVED":
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (Regex.Replace(command, "(.*?) .*", "$1").ToUpperInvariant())
                    {
                        case "ADDED":
                            thisYachtId = Convert.ToInt32(Regex.Replace(command, "ADDED ", "", RegexOptions.IgnoreCase));
                            if (thisYachtId != -1)
                            {
                                buttonAddYacht.Enabled = false;
                                SendToServer(yachtId + " set sail " + trackBarSailAngle.Value.ToString());
                                SendToServer(yachtId + " set rudder " + trackBarRudder.Value.ToString());
                            }
                            break;
                        case "REMOVED":
                            int id = Convert.ToInt32(Regex.Replace(command, "REMOVED ", "", RegexOptions.IgnoreCase));
                            if (id == yachtId)
                            {
                                yachtId = -1;
                                timerYachtUpdater.Stop();
                                buttonAddYacht.Enabled = true;
                            }
                            break;
                        case "SIMULATORS":
                            foreach (var simulator in Regex.Split(Regex.Replace(command, "SIMULATORS ", "", RegexOptions.IgnoreCase), ";"))
                            {
                                comboBoxYachtTypes.Items.Add(simulator);
                            }
                            if (comboBoxYachtTypes.Items.Count > 0 && comboBoxYachtTypes.SelectedIndex == -1)
                            {
                                comboBoxYachtTypes.SelectedIndex = 0;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch
            {
            }
        }

        private void Quit()
        {
            SendToServer(thisYachtId + " remove yacht");
        }

        private void SendToServer(string message)
        {
            try
            {
                byte[] buffer = encoder.GetBytes(message);
                clientStream.Write(buffer, 0, buffer.Length);
                clientStream.Flush();

                buffer = new byte[4096];
                int bytesRead;

                bytesRead = 0;

                try
                {
                    //blocks until a client sends a message
                    bytesRead = clientStream.Read(buffer, 0, 4096);
                }
                catch
                {
                    //a socket error has occured
                    return;
                }

                if (bytesRead == 0)
                {
                    //the client has disconnected from the server
                    return;
                }

                //message has successfully been received
                encoder = new ASCIIEncoding();
                string command = encoder.GetString(buffer, 0, bytesRead);
                ProcessCommand(command);
            }
            catch (IOException)
            {
            }
        }

        private void UpdateYachtImage()
        {
            Bitmap background = new Bitmap(48, 48, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

            Image yacht = imageListYacht.Images["Yacht"];
            PointF yachtOffset = new PointF((background.Width - yacht.Width) / 2, (background.Height - yacht.Height) / 2);
            using (Graphics gimage = Graphics.FromImage(background))
            {
                gimage.TranslateTransform(background.Width / 2, background.Height / 2);
                gimage.RotateTransform(heading);
                gimage.TranslateTransform(-background.Width / 2, -background.Height / 2);
                gimage.DrawImage(yacht, yachtOffset.X, yachtOffset.Y);
            }

            Image sail = imageListYacht.Images["Sail"];
            PointF sailOffset = new PointF((background.Width - sail.Width) / 2, (background.Height - sail.Height) / 2);
            using (Graphics gimage = Graphics.FromImage(background))
            {
                gimage.TranslateTransform(background.Width / 2, background.Height / 2);
                gimage.RotateTransform(sailAngel);
                gimage.TranslateTransform(-background.Width / 2, -background.Height / 2);
                gimage.DrawImage(sail, sailOffset.X, sailOffset.Y);
            }

            Image wind = imageListYacht.Images["Wind"];
            PointF windOffset = new PointF((background.Width - wind.Width) / 2, (background.Height - wind.Height) / 2);
            using (Graphics gimage = Graphics.FromImage(background))
            {
                gimage.TranslateTransform(background.Width / 2, background.Height / 2);
                gimage.RotateTransform(windAngel);
                gimage.TranslateTransform(-background.Width / 2, -background.Height / 2);
                gimage.DrawImage(wind, windOffset.X, windOffset.Y);
            }

            pictureBoxYacht.Image = background;
        }

        #endregion

    }
}
