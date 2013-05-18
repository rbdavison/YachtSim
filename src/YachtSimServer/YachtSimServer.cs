using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Geospatial;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace YachtSim
{
    public class YachtSimServer : IDisposable
    {
        private Dictionary<int, IYacht> yachts = new Dictionary<int, IYacht>();
        private static volatile YachtSimServer _instance;
        private static object lockObject = new object();
        private int yachtIds = 0;
        private TcpListener tcpListener;
        private Thread listenThread;

        public static YachtSimServer GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new YachtSimServer();
                            if (_instance.listenThread == null)
                            {
                                _instance.listenThread = new Thread(new ThreadStart(_instance.ListenForClients));
                                _instance.listenThread.Start();
                            }
                        }
                    }
                }
                return _instance;
            }
        }

        private YachtSimServer()
        {
            this.Wind = new Wind();
            this.KeepAlive = true;
            this.tcpListener = new TcpListener(IPAddress.Any, Convert.ToInt32(ConfigurationManager.AppSettings["ServerPort"]));
        }

        private string AddYacht(string simClass)
        {
            int yachtId = yachtIds++;
            try
            {
                string value = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(simClass.Trim().ToLowerInvariant());
                string entryPoint = value + ".Simulator, " + value + ".sim";
                IYacht simulator = Activator.CreateInstance(Type.GetType(entryPoint)) as IYacht;
                simulator.SetSimServer(this);
                simulator.Id = yachtId;
                yachts.Add(simulator.Id, simulator);

                return yachtId.ToString();
            }
            catch
            {
            }
            return "-1";
        }

        private string CheckYachtExists(int yachtId)
        {
            string value = string.Empty;
            try
            {
                IYacht yacht = yachts[yachtId];
            }
            catch (KeyNotFoundException)
            {
                value = Constants.SERVER_REPLY_REMOVED;
            }
            catch { }
            return value;
        }

        public void Dispose()
        {
            foreach (var yacht in yachts.Values)
            {
                yacht.Dispose();
            }
        }

        private string GetHeading(int yachtId)
        {
            string value = string.Empty;
            try
            {
                value = yachts[yachtId].Heading.TotalDegrees.ToString("F1");
            }
            catch { }
            return value;
        }

        private string GetHeeling(int yachtId)
        {
            string value = string.Empty;
            try
            {
                value = yachts[yachtId].Heeling.TotalDegrees.ToString("F1");
            }
            catch { }
            return value;
        }

        private string GetLocation(int yachtId)
        {
            string value = string.Empty;
            try
            {
                if (yachts[yachtId].Location != null)
                {
                    value = yachts[yachtId].Location.ToString("ISO", CultureInfo.CurrentCulture);
                }
            }
            catch { }
            return value;
        }

        private string GetName(int yachtId)
        {
            string value = string.Empty;
            try
            {
                value = yachts[yachtId].Name;
            }
            catch { }
            return value;
        }

        private string GetRudderAngle(int yachtId)
        {
            string value = string.Empty;
            try
            {
                value = yachts[yachtId].RudderAngle.TotalDegrees.ToString("F1");
            }
            catch { }
            return value;
        }

        private string GetSailAngle(int yachtId)
        {
            string value = string.Empty;
            try
            {
                value = yachts[yachtId].SailAngle.TotalDegrees.ToString("F1");
            }
            catch { }
            return value;
        }

        private string GetSailDirection(int yachtId)
        {
            string value = string.Empty;
            try
            {
                value = yachts[yachtId].SailDirection.TotalDegrees.ToString("F1");
            }
            catch { }
            return value;
        }

        private string GetSimulators()
        {
            string value = string.Empty;
            try
            {
                foreach (var file in Directory.GetFiles(Application.StartupPath))
                {
                    if (Regex.Match(file, @".*\.sim\.dll$", RegexOptions.IgnoreCase).Success)
                    {
                        if (!string.IsNullOrEmpty(value))
                        {
                            value += ";";
                        }
                        value += Path.GetFileName(Regex.Replace(file, ".sim.dll", "", RegexOptions.IgnoreCase));
                    }
                }
            }
            catch { }
            return value;
        }

        private string GetWaypoint(int yachtId, int waypointId)
        {
            string value = string.Empty;
            try
            {
                if (yachts[yachtId].Waypoints != null)
                {
                    foreach (var wp in yachts[yachtId].Waypoints.ToArray())
                    {
                        if (wp.Id == waypointId)
                        {
                            value = wp.Id + "=" + wp.Location.ToString("ISO", CultureInfo.CurrentCulture);
                        }
                    }
                }
            }
            catch { }
            return value;
        }

        private string GetWaypoints(int yachtId)
        {
            string value = string.Empty;
            try
            {
                foreach (var wp in yachts[yachtId].Waypoints.ToArray())
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        value += ";";
                    }
                    value += wp.Id + "=" + wp.Location.ToString("ISO", CultureInfo.CurrentCulture);
                }
            }
            catch { }
            return value;
        }

        private string GetWindAngle(int yachtId)
        {
            string value = string.Empty;
            try
            {
                value = yachts[yachtId].ApparentWindAngle.TotalDegrees.ToString("F0");
            }
            catch { }
            return value;
        }

        private string GetWindVelocity(int yachtId)
        {
            string value = string.Empty;
            try
            {
                value = yachts[yachtId].ApparentWindVelocity.ToString("F2");
            }
            catch { }
            return value;
        }

        private string GetYachts()
        {
            string value = string.Empty;
            try
            {
                foreach (var yacht in yachts)
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        value += ";";
                    }
                    value += yacht.Key.ToString() + "=" + yacht.Value.Name;
                }
            }
            catch { }
            return value;
        }

        private bool KeepAlive { get; set; }

        private void ListenForClients()
        {
            this.tcpListener.Start();

            while (_instance.KeepAlive)
            {
                //blocks until a client has connected to the server
                TcpClient client = this.tcpListener.AcceptTcpClient();

                // create a thread to handle communication with connected client
                Thread recvThread = new Thread(new ParameterizedThreadStart(ReceiveFromClient));
                recvThread.Start(client);
            }
        }

        private void ProcessAddCommand(TcpClient tcpClient, int yachtId, string command)
        {
            switch (Regex.Replace(command, " .*", "").ToUpperInvariant())
            {
                case Constants.SERVER_COMMAND_WAYPOINT:
                    int waypointIdAdd;
                    if (int.TryParse(Regex.Replace(command, Constants.SERVER_COMMAND_WAYPOINT + " (.*) .*", "$1", RegexOptions.IgnoreCase), out waypointIdAdd))
                    {
                        Geospatial.Location location = Geospatial.Location.Parse(Regex.Replace(command, Constants.SERVER_COMMAND_WAYPOINT + " " + waypointIdAdd + " ", "", RegexOptions.IgnoreCase),
                                                                                 Geospatial.LocationStyles.Iso, CultureInfo.CurrentCulture);
                        _instance.SetWaypoint(yachtId, waypointIdAdd, location);
                        _instance.SendToClient(tcpClient, yachtId + " " + Constants.SERVER_REPLY_WAYPOINTADDED + _instance.GetWaypoint(yachtId, waypointIdAdd));
                    }
                    else
                    {
                        _instance.SendToClient(tcpClient, yachtId + " " + Constants.SERVER_REPLY_ERROR);
                    } break;
                default:
                    _instance.SendToClient(tcpClient, yachtId + " " + Constants.SERVER_REPLY_UNKNOWN_COMMAND);
                    break;
            }
        }

        private void ProcessCommand(TcpClient tcpClient, string command)
        {
            int yachtId;
            if (int.TryParse(Regex.Replace(command, " .*", ""), out yachtId))
            {
                if (_instance.CheckYachtExists(yachtId) == Constants.SERVER_REPLY_REMOVED)
                {
                    _instance.SendToClient(tcpClient, Constants.SERVER_REPLY_REMOVED + yachtId);
                }
                else
                {
                    string subCommand = Regex.Replace(command, yachtId + " (.*)", "$1");
                    ProcessYachtCommand(tcpClient, yachtId, subCommand);
                }
            }
            else
            {
                switch (Regex.Replace(command, " .*", "", RegexOptions.IgnoreCase).ToUpperInvariant())
                {
                    case Constants.SERVER_COMMAND_ADD:
                        _instance.SendToClient(tcpClient, Constants.SERVER_REPLY_ADDED + _instance.AddYacht(command.ToUpperInvariant().Replace(Constants.SERVER_COMMAND_ADD + " ", "")));
                        break;
                    case Constants.SERVER_COMMAND_QUIT:
                        _instance.KeepAlive = false;
                        break;
                    case Constants.SERVER_COMMAND_SIMULATORS:
                        _instance.SendToClient(tcpClient, Constants.SERVER_REPLY_SIMULATORS + _instance.GetSimulators());
                        break;
                    case Constants.SERVER_COMMAND_YACHTS:
                        _instance.SendToClient(tcpClient, Constants.SERVER_REPLY_YACHTS + _instance.GetYachts());
                        break;
                    default:
                        _instance.SendToClient(tcpClient, Constants.SERVER_REPLY_UNKNOWN_COMMAND);
                        break;
                }
            }
        }

        private void ProcessGetCommand(TcpClient tcpClient, int yachtId, string command)
        {
            switch (Regex.Replace(command, " .*", "").ToUpperInvariant())
            {
                case Constants.SERVER_COMMAND_HEADING:
                    _instance.SendToClient(tcpClient, yachtId + " " + Constants.SERVER_REPLY_HEADING + _instance.GetHeading(yachtId));
                    break;
                case Constants.SERVER_COMMAND_HEELING:
                    _instance.SendToClient(tcpClient, yachtId + " " + Constants.SERVER_REPLY_HEELING + _instance.GetHeeling(yachtId));
                    break;
                case Constants.SERVER_COMMAND_LOCATION:
                    _instance.SendToClient(tcpClient, yachtId + " " + Constants.SERVER_REPLY_LOCATION + _instance.GetLocation(yachtId));
                    break;
                case Constants.SERVER_COMMAND_NAME:
                    _instance.SendToClient(tcpClient, yachtId + " " + Constants.SERVER_REPLY_NAME + _instance.GetName(yachtId));
                    break;
                case Constants.SERVER_COMMAND_RUDDER:
                    _instance.SendToClient(tcpClient, yachtId + " " + Constants.SERVER_REPLY_RUDDER + _instance.GetRudderAngle(yachtId));
                    break;
                case Constants.SERVER_COMMAND_SAIL:
                    _instance.SendToClient(tcpClient, yachtId + " " + Constants.SERVER_REPLY_SAIL + _instance.GetSailAngle(yachtId));
                    break;
                case Constants.SERVER_COMMAND_SAILDIRECTION:
                    _instance.SendToClient(tcpClient, yachtId + " " + Constants.SERVER_REPLY_SAILDIRECTION + _instance.GetSailDirection(yachtId));
                    break;
                case Constants.SERVER_COMMAND_WAYPOINT:
                    int waypointId;
                    if (int.TryParse(Regex.Replace(command, Constants.SERVER_COMMAND_WAYPOINT + " (.*) .*", "$1"), out waypointId))
                    {
                        _instance.SendToClient(tcpClient, yachtId + " " + Constants.SERVER_REPLY_WAYPOINT + _instance.GetWaypoint(yachtId, waypointId));
                    }
                    else
                    {
                        _instance.SendToClient(tcpClient, Constants.SERVER_REPLY_ERROR);
                    }
                    break;
                case Constants.SERVER_COMMAND_WAYPOINTS:
                    _instance.SendToClient(tcpClient, yachtId + " " + Constants.SERVER_REPLY_WAYPOINTS + _instance.GetWaypoints(yachtId));
                    break;
                case Constants.SERVER_COMMAND_WINDANGLE:
                    _instance.SendToClient(tcpClient, yachtId + " " + Constants.SERVER_REPLY_WINDANGLE + _instance.GetWindAngle(yachtId));
                    break;
                case Constants.SERVER_COMMAND_WINDVELOCITY:
                    _instance.SendToClient(tcpClient, yachtId + " " + Constants.SERVER_REPLY_WINDVELOCITY + _instance.GetWindVelocity(yachtId));
                    break;
                default:
                    _instance.SendToClient(tcpClient, yachtId + " " + Constants.SERVER_REPLY_UNKNOWN_COMMAND);
                    break;
            }
        }

        private void ProcessRemoveCommand(TcpClient tcpClient, int yachtId, string command)
        {
            switch (Regex.Replace(command, " .*", "").ToUpperInvariant())
            {
                case Constants.SERVER_COMMAND_YACHT:
                    _instance.RemoveYacht(yachtId);
                    _instance.SendToClient(tcpClient, Constants.SERVER_REPLY_REMOVED + yachtId);
                    break;
                case Constants.SERVER_COMMAND_WAYPOINT:
                    int waypointIdRemove;
                    if (int.TryParse(Regex.Replace(command, Constants.SERVER_COMMAND_WAYPOINT + " ", "", RegexOptions.IgnoreCase), out waypointIdRemove))
                    {
                        _instance.RemoveWaypoint(yachtId, waypointIdRemove);
                        _instance.SendToClient(tcpClient, yachtId + " " + Constants.SERVER_REPLY_WAYPOINTREMOVED + waypointIdRemove);
                    }
                    break;
                default:
                    _instance.SendToClient(tcpClient, Constants.SERVER_REPLY_UNKNOWN_COMMAND);
                    break;
            }
        }

        private void ProcessSetCommand(TcpClient tcpClient, int yachtId, string command)
        {
            switch (Regex.Replace(command, " .*", "").ToUpperInvariant())
            {
                case Constants.SERVER_COMMAND_LOCATION:
                    try
                    {
                        Geospatial.Location location = Geospatial.Location.Parse(command.ToUpperInvariant().Replace(Constants.SERVER_COMMAND_LOCATION + " ", ""), Geospatial.LocationStyles.Iso, CultureInfo.CurrentCulture);
                        _instance.SetLocation(yachtId, location);
                        _instance.SendToClient(tcpClient, yachtId + " " + Constants.SERVER_REPLY_LOCATION + _instance.GetLocation(yachtId));
                    }
                    catch
                    {
                        _instance.SendToClient(tcpClient, yachtId + " " + Constants.SERVER_REPLY_ERROR);
                    }
                    break;
                case Constants.SERVER_COMMAND_NAME:
                    _instance.SetName(yachtId, Regex.Replace(command, Constants.SERVER_COMMAND_NAME + " ", "", RegexOptions.IgnoreCase));
                    _instance.SendToClient(tcpClient, yachtId + " " + Constants.SERVER_REPLY_NAME + _instance.GetName(yachtId));
                    break;
                case Constants.SERVER_COMMAND_RUDDER:
                    int rudderValue;
                    if (int.TryParse(Regex.Replace(command, Constants.SERVER_COMMAND_RUDDER + " ", "", RegexOptions.IgnoreCase), out rudderValue))
                    {
                        _instance.SetRudderAngle(yachtId, RudderAngle.FromDegrees(rudderValue));
                        _instance.SendToClient(tcpClient, yachtId + " " + Constants.SERVER_REPLY_RUDDER + _instance.GetRudderAngle(yachtId));
                    }
                    else
                    {
                        _instance.SendToClient(tcpClient, yachtId + " " + Constants.SERVER_REPLY_ERROR);
                    }
                    break;
                case Constants.SERVER_COMMAND_SAIL:
                    int sailValue;
                    if (int.TryParse(Regex.Replace(command, Constants.SERVER_COMMAND_SAIL + " ", "", RegexOptions.IgnoreCase), out sailValue))
                    {
                        _instance.SetSailAngle(yachtId, SailAngle.FromDegrees(sailValue));
                        _instance.SendToClient(tcpClient, yachtId + " " + Constants.SERVER_REPLY_SAIL + _instance.GetSailAngle(yachtId));
                    }
                    else
                    {
                        _instance.SendToClient(tcpClient, yachtId + " " + Constants.SERVER_REPLY_ERROR);
                    }
                    break;
                case Constants.SERVER_COMMAND_WAYPOINT:
                    int waypointIdAdd;
                    if (int.TryParse(Regex.Replace(command.ToUpperInvariant(), Constants.SERVER_COMMAND_WAYPOINT + " (.*) .*", "$1"), out waypointIdAdd))
                    {
                        Geospatial.Location location = Geospatial.Location.Parse(command.ToUpperInvariant().Replace(Constants.SERVER_COMMAND_WAYPOINT +
                                                                                 " " + waypointIdAdd +
                                                                                 " ", ""),
                                                                                 Geospatial.LocationStyles.Iso, CultureInfo.CurrentCulture);
                        _instance.SetWaypoint(yachtId, waypointIdAdd, location);
                        _instance.SendToClient(tcpClient, yachtId + " " + Constants.SERVER_REPLY_WAYPOINTADDED + _instance.GetWaypoint(yachtId, waypointIdAdd));
                    }
                    else
                    {
                        _instance.SendToClient(tcpClient, yachtId + " " + Constants.SERVER_REPLY_ERROR);
                    }
                    break;
                default:
                    _instance.SendToClient(tcpClient, yachtId + " " + Constants.SERVER_REPLY_UNKNOWN_COMMAND);
                    break;
            }
        }

        private void ProcessYachtCommand(TcpClient tcpClient, int yachtId, string command)
        {
            switch (Regex.Replace(command, " .*", "").ToUpperInvariant())
            {
                case Constants.SERVER_COMMAND_ADD:
                    string addCommand = Regex.Replace(command, Constants.SERVER_COMMAND_ADD + " ", "", RegexOptions.IgnoreCase);
                    ProcessAddCommand(tcpClient, yachtId, addCommand);
                    break;
                case Constants.SERVER_COMMAND_GET:
                    string getCommand = Regex.Replace(command, Constants.SERVER_COMMAND_GET + " ", "", RegexOptions.IgnoreCase);
                    ProcessGetCommand(tcpClient, yachtId, getCommand);
                    break;
                case Constants.SERVER_COMMAND_SET:
                    string setCommand = Regex.Replace(command, Constants.SERVER_COMMAND_SET + " ", "", RegexOptions.IgnoreCase);
                    ProcessSetCommand(tcpClient, yachtId, setCommand);
                    break;
                case Constants.SERVER_COMMAND_REMOVE:
                    string removeCommand = Regex.Replace(command, Constants.SERVER_COMMAND_REMOVE + " ", "", RegexOptions.IgnoreCase);
                    ProcessRemoveCommand(tcpClient, yachtId, removeCommand);
                    break;
                default:
                    _instance.SendToClient(tcpClient, yachtId + " " + Constants.SERVER_REPLY_UNKNOWN_COMMAND);
                    break;
            }
        }

        private void ReceiveFromClient(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            NetworkStream clientStream = tcpClient.GetStream();

            byte[] message = new byte[4096];
            int bytesRead;

            while (_instance.KeepAlive)
            {
                bytesRead = 0;

                try
                {
                    // blocks until a client sends a message
                    bytesRead = clientStream.Read(message, 0, 4096);
                }
                catch
                {
                    // a socket error has occured
                    break;
                }

                if (bytesRead == 0)
                {
                    // the client has disconnected from the server
                    break;
                }

                // message has successfully been received
                ASCIIEncoding encoder = new ASCIIEncoding();
                string command = encoder.GetString(message, 0, bytesRead);
                try
                {
                    ProcessCommand(tcpClient, command);
                }
                catch
                {
                    _instance.SendToClient(tcpClient, Constants.SERVER_REPLY_ERROR);
                }
            }

            tcpClient.Close();
        }

        internal void RemoveYacht(int yachtId)
        {
            try
            {
                yachts[yachtId].Dispose();
                yachts.Remove(yachtId);
            }
            catch { }
        }

        private void RemoveWaypoint(int yachtId, int waypointId)
        {
            try
            {
                foreach (Waypoint wp in yachts[yachtId].Waypoints.ToArray())
                {
                    if (wp.Id == waypointId)
                    {
                        yachts[yachtId].Waypoints.Remove(wp);
                    }
                }
            }
            catch { }
        }

        private void SendToClient(TcpClient client, string message)
        {
            NetworkStream clientStream = client.GetStream();
            ASCIIEncoding encoder = new ASCIIEncoding();

            byte[] buffer = encoder.GetBytes(message);
            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();
        }

        private void SetLocation(int yachtId, Location location)
        {
            try
            {
                yachts[yachtId].SetLocation(location);
            }
            catch { }
        }

        private void SetName(int yachtId, string name)
        {
            try
            {
                yachts[yachtId].Name = name;
            }
            catch { }
        }

        private void SetRudderAngle(int yachtId, RudderAngle rudderAngle)
        {
            try
            {
                yachts[yachtId].SetRudderAngle(rudderAngle);
            }
            catch { }
        }

        private void SetSailAngle(int yachtId, SailAngle sailAngle)
        {
            try
            {
                yachts[yachtId].SetSailAngle(sailAngle);
            }
            catch { }
        }

        private void SetWaypoint(int yachtId, int waypointId, Location location)
        {
            try
            {
                bool found = false;
                int index = 0;
                foreach (Waypoint wp in yachts[yachtId].Waypoints.ToArray())
                {
                    if (wp.Id == waypointId)
                    {
                        yachts[yachtId].Waypoints[index] = new Waypoint(waypointId, location);
                        if (waypointId == 0)
                        {
                            yachts[yachtId].SetLocation(location);
                        }
                        found = true;
                    }
                    index++;
                }
                if (!found)
                {
                    yachts[yachtId].Waypoints.Add(new Waypoint(waypointId, location));
                    if (waypointId == 0)
                    {
                        yachts[yachtId].SetLocation(location);
                    }
                }
            }
            catch { }
        }

        internal List<IYacht> Yachts
        {
            get
            {
                List<IYacht> yachtList = new List<IYacht>();
                yachtList.AddRange(yachts.Values);
                return yachtList;
            }
        }

        public Wind Wind { get; set; }
    }
}
