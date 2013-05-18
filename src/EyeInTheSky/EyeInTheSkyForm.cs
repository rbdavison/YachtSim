using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net.Sockets;
using System.Net;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.IconLib;
using System.Drawing.IconLib.ColorProcessing;

namespace EyeInTheSky
{
    public partial class EyeInTheSkyForm : Form
    {
        #region Constructors

        private fvw.IMap fvMap;
        private fvw.ILayer fvLayer;
        private Int32 fvLayerHandle;
        private FvCallback fvCallback;
        private TcpClient server;
        private NetworkStream clientStream;
        private volatile Dictionary<int, YachtItem> yachtList = new Dictionary<int, YachtItem>();
        private int selectedYacht;
        private int mapCategory = 0;
        private int mapHandle = 0;
        private int mapZoom = 0;
        private int mapProj = 0;

        public EyeInTheSkyForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Windows Forms events handlers

        private void buttonQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void checkBoxCentreOnYacht_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxZoom.Enabled = checkBoxCentreOnYacht.Checked;
        }

        private void EyeInTheSkyForm_Load(object sender, EventArgs e)
        {
            if (!StartFalconView())
            {
                MessageBox.Show("No FalconView Connection", "Falied to connect to FalconView", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Close();
            }
            IPAddress ipAddress;
            if (!IPAddress.TryParse(ConfigurationManager.AppSettings["ServerIP"], out ipAddress))
            {
                MessageBox.Show("No Server IP Address", "Falied to parse the [ServerIP] Address in the configuration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Close();
            }
            int port;
            if (!int.TryParse(ConfigurationManager.AppSettings["ServerPort"], out port))
            {
                MessageBox.Show("No Server Port", "Falied to parse the [ServerPort] in the configuration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Close();
            }
            server = new TcpClient();
            IPEndPoint serverEndPoint = new IPEndPoint(ipAddress, port);
            server.Connect(serverEndPoint);
            clientStream = server.GetStream();

            //object MapList = null;
            //object MapDegPerPixelList = null;
            //int numOfMaps = 0;
            //const int CLIENT_RASTER_CATEGORY = 2;

            //fvMap.QueryMapTypes(CLIENT_RASTER_CATEGORY, ref MapList, ref MapDegPerPixelList, ref numOfMaps, 1);

            //// now unpack the array
            //Array MapHandles = (Array)MapList;
            //Array DegPix = (Array)MapDegPerPixelList;
            //for (int i = 0; i < MapHandles.Length; i++)
            //{
            //    comboBoxMapHandles.Items.Add(new MapItem((int)MapHandles.GetValue(i)));
            //}

            timerYachtUpdate.Start();
            listViewWaypoints.ListViewItemSorter = new Sorter();
        }

        private void EyeInTheSkyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (server != null)
            {
                server.Close();
            }
            CloseFalconView();
        }

        private void listBoxYachts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxYachts.SelectedIndex != -1)
            {
                YachtItem yachtItem = listBoxYachts.SelectedItem as YachtItem;
                if (yachtItem != null)
                {
                    if (selectedYacht != yachtItem.Id)
                    {
                        selectedYacht = yachtItem.Id;
                        SendToServer(yachtItem.Id.ToString() + " get waypoints");
                    }
                }
            }
        }

        private void listViewWaypoints_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewWaypoints.Items)
            {
                item.BackColor = Color.White;
            }
            if (listViewWaypoints.SelectedIndices.Count == 1)
            {
                ListViewItem item = listViewWaypoints.SelectedItems[0];
                item.BackColor = Color.LightGray;
                if (yachtList != null)
                {
                    yachtList[selectedYacht].Waypoint = (Geospatial.Location)item.Tag;
                }
            }
        }

        private void timerYachtUpdate_Tick(object sender, EventArgs e)
        {
            SendToServer("Yachts");
            foreach (YachtItem yachtItem in yachtList.Values.ToArray())
            {
                if (yachtItem != null)
                {
                    SendToServer(yachtItem.Id.ToString() + " get heading");
                    SendToServer(yachtItem.Id.ToString() + " get sailDirection");
                    SendToServer(yachtItem.Id.ToString() + " get windangle");
                    SendToServer(yachtItem.Id.ToString() + " get location");
                }
            }
        }

        private void trackBarIconSize_Scroll(object sender, EventArgs e)
        {
            foreach (YachtItem yachtItem in yachtList.Values)
            {
                yachtItem.IconSize = 32 + trackBarIconSize.Value * 2;
            }
        }

        #endregion

        #region Methods

        private void AddBreadcrumb(int yachtId)
        {
            if (yachtList[yachtId].PreviousLocation != null && yachtList[yachtId].Location != null)
            {
                fvLayer.AddLine(fvLayerHandle, yachtList[yachtId].PreviousLocation.Latitude.TotalDegrees,
                                               yachtList[yachtId].PreviousLocation.Longitude.TotalDegrees,
                                               yachtList[yachtId].Location.Latitude.TotalDegrees,
                                               yachtList[yachtId].Location.Longitude.TotalDegrees,
                                               0, 0);
            }
        }

        private void AddWaypoints()
        {
            try
            {
                if (yachtList[selectedYacht].Waypoints.Count > 0)
                {
                    for (int i = 1; i <= yachtList[selectedYacht].Waypoints.Count; i++)
                    {
                        fvLayer.AddLine(fvLayerHandle, yachtList[selectedYacht].Waypoints[i - 1].Latitude.TotalDegrees,
                                                       yachtList[selectedYacht].Waypoints[i - 1].Longitude.TotalDegrees,
                                                       yachtList[selectedYacht].Waypoints[i].Latitude.TotalDegrees,
                                                       yachtList[selectedYacht].Waypoints[i].Longitude.TotalDegrees,
                                                       0, 0);
                    }
                }
            }
            catch
            {
            }
        }

        private void CloseFalconView()
        {
            try
            {
                string fvw = Constants.FALCONVIEW_PROCESS_NAME;
                foreach (Process process in Process.GetProcesses())
                {
                    if (process.ProcessName.ToUpperInvariant() == fvw.ToUpperInvariant())
                    {
                        if (process.CloseMainWindow())
                        {
                        }
                        else
                        {
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private bool ProcessIsRunning(string name)
        {
            if (name != null)
            {
                foreach (Process process in Process.GetProcesses())
                {
                    if (process.ProcessName.ToUpperInvariant() == name.ToUpperInvariant())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void SendToServer(string message)
        {
            ASCIIEncoding encoder = new ASCIIEncoding();
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

        private void ProcessCommand(string command)
        {
            try
            {
                int yachtId;
                if (int.TryParse(Regex.Replace(command, " .*", ""), out yachtId))
                {
                    command = Regex.Replace(command, "^" + yachtId + " ", "");
                    switch (Regex.Replace(command, " .*", "").ToUpperInvariant())
                    {
                        case "HEADING":
                            if (yachtList != null)
                            {
                                yachtList[yachtId].Heading = (float)Convert.ToDouble(command.Replace("HEADING ", ""));
                            }
                            break;
                        case "LOCATION":
                            try
                            {
                                if (yachtList != null && yachtList[yachtId] != null)
                                {
                                    yachtList[yachtId].Location = Geospatial.Location.Parse(Regex.Replace(command, "LOCATION ", "", RegexOptions.IgnoreCase), Geospatial.LocationStyles.Iso, CultureInfo.CurrentCulture);
                                    AddBreadcrumb(yachtId);
                                    UpdateYachtImage(yachtId);
                                    UpdateMap();
                                }
                            }
                            catch { }
                            break;
                        case "SAILDIRECTION":
                            if (yachtList != null && yachtList[yachtId] != null)
                            {
                                yachtList[yachtId].SailDirection = (float)Convert.ToDouble(command.Replace("SAILDIRECTION ", ""));
                            }
                            break;
                        case "WAYPOINTS":
                            listViewWaypoints.Items.Clear();
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
                                            ListViewItem item = listViewWaypoints.Items.Add(waypointId.ToString());
                                            item.SubItems.Add(location.Latitude.ToString());
                                            item.SubItems.Add(location.Longitude.ToString());
                                            item.Tag = location;
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                            }
                            listViewWaypoints.Sort();
                            foreach (ListViewItem item in listViewWaypoints.Items)
                            {
                                Geospatial.Location location = item.Tag as Geospatial.Location;
                                if (location != null)
                                {
                                    yachtList[yachtId].Waypoints.Add(location);
                                }
                            }
                            AddWaypoints();
                            break;
                        case "WINDANGLE":
                            if (yachtList != null)
                            {
                                yachtList[yachtId].WindAngle = (float)Convert.ToDouble(command.Replace("WINDANGLE ", ""));
                            }
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (Regex.Replace(command, " .*", "").ToUpperInvariant())
                    {
                        case "YACHTS":
                            YachtItem currentlySelectedYacht = listBoxYachts.SelectedItem as YachtItem;
                            listBoxYachts.Items.Clear();
                            foreach (var item in Regex.Split(Regex.Replace(command, "YACHTS ", "", RegexOptions.IgnoreCase), ";"))
                            {
                                if (!string.IsNullOrEmpty(item))
                                {
                                    YachtItem yachtItem = new YachtItem(item);

                                    if (currentlySelectedYacht != null)
                                    {
                                        if (currentlySelectedYacht.Id == yachtItem.Id)
                                        {
                                            listBoxYachts.SelectedIndex = listBoxYachts.Items.Add(currentlySelectedYacht);
                                        }
                                        else
                                        {
                                            listBoxYachts.Items.Add(yachtItem);
                                            if (!yachtList.ContainsKey(yachtItem.Id))
                                            {
                                                try
                                                {
                                                    yachtList.Add(yachtItem.Id, yachtItem);
                                                }
                                                catch
                                                {
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        listBoxYachts.Items.Add(yachtItem);
                                        if (!yachtList.ContainsKey(yachtItem.Id))
                                        {
                                            try
                                            {
                                                yachtList.Add(yachtItem.Id, yachtItem);
                                            }
                                            catch
                                            {
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                        case "REMOVED":
                            int id = Convert.ToInt32(command.Replace("REMOVED ", ""));
                            if (yachtList.ContainsKey(id))
                            {
                                yachtList.Remove(id);
                            }
                            fvLayer.DeleteAllObjects(fvLayerHandle);
                            fvLayer.Refresh(fvLayerHandle);
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

        private void UpdateMap()
        {
            if (yachtList.ContainsKey(selectedYacht))
            {
                //Bitmap map = GetMapImage();
                //pictureBoxMap.Image = map;

                if (checkBoxCentreOnYacht.Checked)
                {
                    if (checkBoxZoom.Checked)
                    {
                        int result = fvMap.SetMapDisplay(yachtList[selectedYacht].Location.Latitude.TotalDegrees,
                                                         yachtList[selectedYacht].Location.Longitude.TotalDegrees,
                                                         0.0,
                                                         mapCategory,
                                                         -3,
                                                         mapZoom,
                                                         19,
                                                         mapProj);
                    }
                    else
                    {
                        int result = fvMap.SetMapDisplay(yachtList[selectedYacht].Location.Latitude.TotalDegrees,
                                                         yachtList[selectedYacht].Location.Longitude.TotalDegrees,
                                                         0.0,
                                                         mapCategory,
                                                         -1,
                                                         mapZoom,
                                                         19,
                                                         mapProj);
                    }
                }
            }
        }

        //private Bitmap GetMapImage()
        //{
        //    MapItem mapItem = comboBoxMapHandles.SelectedItem as MapItem;
        //    if (mapItem != null)
        //    {
        //        short no_data = 0;
        //        object dib = null;
        //        object corners = null;
        //        double azimuth = checkBoxNorthUp.Checked ? 0.0 : 360 - yachtList[selectedYacht].Heading;
        //        int result = fvMap.CreateMap(yachtList[selectedYacht].Location.Latitude.TotalDegrees,
        //                                     yachtList[selectedYacht].Location.Longitude.TotalDegrees,
        //                                     2,
        //                                     mapItem.Handle,
        //                                     azimuth,
        //                                     100,
        //                                     0,
        //                                     0.0,
        //                                     (short)1,
        //                                     pictureBoxMap.Width,
        //                                     pictureBoxMap.Height,
        //                                     (short)1,
        //                                     ref no_data,
        //                                     ref dib,
        //                                     ref corners);
        //        if (result == Constants.SUCCESS)
        //        {
        //            Array dib_array = (dib as Array);

        //            int e4 = Convert.ToInt32(dib_array.GetValue(4));
        //            int e5 = Convert.ToInt32(dib_array.GetValue(5));
        //            int e6 = Convert.ToInt32(dib_array.GetValue(6));
        //            int e7 = Convert.ToInt32(dib_array.GetValue(7));
        //            int e8 = Convert.ToInt32(dib_array.GetValue(8));
        //            int e9 = Convert.ToInt32(dib_array.GetValue(9));
        //            int e10 = Convert.ToInt32(dib_array.GetValue(10));
        //            int e11 = Convert.ToInt32(dib_array.GetValue(11));

        //            int width = (e7 << 24) + (e6 << 16) + (e5 << 8) + e4;
        //            int height = (e11 << 24) + (e10 << 16) + (e9 << 8) + e8;

        //            // Get a pinned GC handle to the returned DIB bytes
        //            System.Runtime.InteropServices.GCHandle handle =
        //                System.Runtime.InteropServices.GCHandle.Alloc(dib, System.Runtime.InteropServices.GCHandleType.Pinned);

        //            // Get a pointer to the DIB bytes
        //            IntPtr handlePtr = handle.AddrOfPinnedObject();

        //            // Get a pointer to the start of the image data within the DIB bytes.
        //            // The image data starts 40 bytes into the DIB.
        //            // This code is written to handle IntPtr instances that could be
        //            // 32-bit or 64-bit.
        //            IntPtr scan0 = (IntPtr.Size == 4) ?
        //                new IntPtr(handlePtr.ToInt32() + 40) :
        //                new IntPtr(handlePtr.ToInt64() + 40);

        //            // Create a new bitmap object from the raw data
        //            // This assumes a 3 bits per pixel RGB format
        //            Bitmap bitmap = new Bitmap(width, height, width * 3, System.Drawing.Imaging.PixelFormat.Format24bppRgb, scan0);

        //            // Rotate the image since the bitmap is
        //            // originally created upside-down
        //            bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);

        //            // Free the pinned GC handle for the DIB bytes
        //            handle.Free();

        //            //Array corners_array = (corners as Array);

        //            //double ul_lat = Convert.ToDouble(corners_array.GetValue(2));
        //            //double ul_lon = Convert.ToDouble(corners_array.GetValue(3));
        //            //double lr_lat = Convert.ToDouble(corners_array.GetValue(6));
        //            //double lr_lon = Convert.ToDouble(corners_array.GetValue(7));

        //            //Coordinate center = new Coordinate(lat, lon);
        //            //Coordinate upperLeft = new Coordinate(ul_lat, ul_lon);
        //            //Coordinate lowerRight = new Coordinate(lr_lat, lr_lon);

        //            return bitmap;
        //        }
        //    }
        //    return null;
        //}

        private bool StartFalconView()
        {
            // Make sure FalconView is running. We do this so we can set it to full screen.
            string fvw = Constants.FALCONVIEW_PROCESS_NAME;
            if (!ProcessIsRunning(fvw))
            {
                string falconViewExe = Constants.FALCONVIEW_EXE_NAME;
                Process falconViewProc = new Process();
                falconViewProc.StartInfo.FileName = falconViewExe;
                falconViewProc.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                falconViewProc.Start();
                while (!falconViewProc.Responding) ;
            }

            // Connect to FalconView
            try
            {
                fvMap = new fvw.Map();
                double lat = 0.0, lon = 0.0, rot = 0.0;
                while (Constants.SUCCESS != fvMap.GetMapDisplay(ref lat, ref lon, ref rot, ref mapCategory, ref mapHandle, ref mapZoom, ref mapProj))
                {
                    System.Threading.Thread.Sleep(500);
                }
            }
            catch
            {
                return false;
            }

            // Now add our layer to FalconView
            try
            {
                fvLayer = new fvw.Layer();
                fvCallback = new FvCallback();
                Int32 result = fvLayer.RegisterWithMapServer(Constants.FALCONVIEW_LAYER_NAME, (int)this.Handle, fvCallback);
                fvLayerHandle = fvLayer.CreateLayer(Constants.FALCONVIEW_LAYER_NAME);
                fvLayer.Refresh(fvLayerHandle);
            }
            catch
            {
                return false;
            }

            return true;
        }

        private void UpdateYachtImage(int yachtId)
        {
            Bitmap background = new Bitmap(yachtList[yachtId].IconSize, yachtList[yachtId].IconSize, PixelFormat.Format32bppArgb);

            Image yachtImage = imageListYacht.Images["Yacht"];
            PointF yachtOffset = new PointF((background.Width - yachtImage.Width) / 2, (background.Height - yachtImage.Height) / 2);
            using (Graphics gimage = Graphics.FromImage(background))
            {
                gimage.TranslateTransform(background.Width / 2, background.Height / 2);
                //if (checkBoxNorthUp.Checked)
                //{
                    gimage.RotateTransform(yachtList[yachtId].Heading);
                //}
                //else
                //{
                //    gimage.RotateTransform(yachtList[yachtId].Heading);
                //}
                gimage.TranslateTransform(-background.Width / 2, -background.Height / 2);
                gimage.DrawImage(yachtImage, yachtOffset.X, yachtOffset.Y);
            }

            Image sail = imageListYacht.Images["Sail"];
            PointF sailOffset = new PointF((background.Width - sail.Width) / 2, (background.Height - sail.Height) / 2);
            using (Graphics gimage = Graphics.FromImage(background))
            {
                gimage.TranslateTransform(background.Width / 2, background.Height / 2);
                //if (checkBoxNorthUp.Checked)
                //{
                    gimage.RotateTransform(yachtList[yachtId].SailDirection);
                //}
                //else
                //{
                //    gimage.RotateTransform((yachtList[yachtId].SailDirection - yachtList[yachtId].Heading + 360) % 360);
                //}
                gimage.TranslateTransform(-background.Width / 2, -background.Height / 2);
                gimage.DrawImage(sail, sailOffset.X, sailOffset.Y);
            }

            Image wind = imageListYacht.Images["Wind"];
            PointF windOffset = new PointF((background.Width - wind.Width) / 2, (background.Height - wind.Height) / 2);
            using (Graphics gimage = Graphics.FromImage(background))
            {
                gimage.TranslateTransform(background.Width / 2, background.Height / 2);
                //if (checkBoxNorthUp.Checked)
                //{
                    gimage.RotateTransform(yachtList[yachtId].WindAngle);
                //}
                //else
                //{
                //    gimage.RotateTransform((yachtList[yachtId].WindAngle - yachtList[yachtId].Heading + 360) % 360);
                //}
                gimage.TranslateTransform(-background.Width / 2, -background.Height / 2);
                gimage.DrawImage(wind, windOffset.X, windOffset.Y);
            }

            try
            {
                MultiIcon multiIcon = new System.Drawing.IconLib.MultiIcon();

                SingleIcon sIcon = multiIcon.Add("Icon1");
                sIcon.CreateFrom(background, IconOutputFormat.Vista);

                if (yachtList[yachtId].FalconViewIconHandle != -1)
                {
                    int result = fvLayer.DeleteObject(fvLayerHandle, yachtList[yachtId].FalconViewIconHandle);
                    fvLayer.Refresh(fvLayerHandle);
                }
                //yachtList[yachtId].NorthUp = checkBoxNorthUp.Checked;
                if (!File.Exists(yachtList[yachtId].IconFileName))
                {
                    //File.Delete(yachtList[yachtId].IconFileName);
                    sIcon.Save(yachtList[yachtId].IconFileName);
                }
            }
            catch
            { }

            if (File.Exists(yachtList[yachtId].IconFileName))
            {
                yachtList[yachtId].FalconViewIconHandle = fvLayer.AddIcon(fvLayerHandle, yachtList[yachtId].IconFileName, yachtList[yachtId].Location.Latitude.TotalDegrees, yachtList[yachtId].Location.Longitude.TotalDegrees, yachtList[yachtId].Text);
            }
            fvLayer.Refresh(fvLayerHandle);
        }

        #endregion

    }
}
