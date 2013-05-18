using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace YachtSim
{
    internal class Constants
    {
        internal const string SERVER_IPADDRESS = "127.0.0.1";

        internal const string SERVER_COMMAND_ADD = "ADD";
        internal const string SERVER_COMMAND_GET = "GET";
        internal const string SERVER_COMMAND_HEADING = "HEADING";
        internal const string SERVER_COMMAND_HEELING = "HEELING";
        internal const string SERVER_COMMAND_LOCATION = "LOCATION";
        internal const string SERVER_COMMAND_NAME = "NAME";
        internal const string SERVER_COMMAND_QUIT = "QUIT";
        internal const string SERVER_COMMAND_REMOVE = "REMOVE";
        internal const string SERVER_COMMAND_RUDDER = "RUDDER";
        internal const string SERVER_COMMAND_SAIL = "SAIL";
        internal const string SERVER_COMMAND_SAILDIRECTION = "SAILDIRECTION";
        internal const string SERVER_COMMAND_SET = "SET";
        internal const string SERVER_COMMAND_SIMULATORS = "SIMULATORS";
        internal const string SERVER_COMMAND_YACHT = "YACHT";
        internal const string SERVER_COMMAND_WAYPOINT = "WAYPOINT";
        internal const string SERVER_COMMAND_WAYPOINTREMOVE = "WAYPOINTREMOVE";
        internal const string SERVER_COMMAND_WAYPOINTS = "WAYPOINTS";
        internal const string SERVER_COMMAND_WINDANGLE = "WINDANGLE";
        internal const string SERVER_COMMAND_WINDVELOCITY = "WINDVELOCITY";
        internal const string SERVER_COMMAND_YACHTS = "YACHTS";

        internal const string SERVER_REPLY_ADDED = "ADDED ";
        internal const string SERVER_REPLY_ERROR = "ERROR ON SERVER";
        internal const string SERVER_REPLY_HEADING = "HEADING ";
        internal const string SERVER_REPLY_HEELING = "HEELING ";
        internal const string SERVER_REPLY_LOCATION = "LOCATION ";
        internal const string SERVER_REPLY_NAME = "NAME ";
        internal const string SERVER_REPLY_REMOVED = "REMOVED ";
        internal const string SERVER_REPLY_RUDDER = "RUDDER ";
        internal const string SERVER_REPLY_SAIL = "SAIL ";
        internal const string SERVER_REPLY_SAILDIRECTION = "SAILDIRECTION ";
        internal const string SERVER_REPLY_SIMULATORS = "SIMULATORS ";
        internal const string SERVER_REPLY_UNKNOWN_COMMAND = "UNKNOWN COMMAND";
        internal const string SERVER_REPLY_WAYPOINT = "WAYPOINT ";
        internal const string SERVER_REPLY_WAYPOINTADDED = "WAYPOINTADDED ";
        internal const string SERVER_REPLY_WAYPOINTREMOVED = "WAYPOINTREMOVED ";
        internal const string SERVER_REPLY_WAYPOINTS = "WAYPOINTS ";
        internal const string SERVER_REPLY_WINDANGLE = "WINDANGLE ";
        internal const string SERVER_REPLY_WINDVELOCITY = "WINDVELOCITY ";
        internal const string SERVER_REPLY_YACHT = "YACHT ";
        internal const string SERVER_REPLY_YACHTS = "YACHTS ";

        // Used by FalconView
        internal const int FAILURE = -1;
        internal const int SUCCESS = 0;
    }
}
