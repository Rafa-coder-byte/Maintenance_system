using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maintenance_calibration_system.DataAccess.Tests.Utilities
{
    public static class ConnectionStringProvider
    {
        public static string GetConnectingString() => "Data Source=Data.sqlite";
    }
}
