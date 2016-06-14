using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;

namespace Common
{
    public static class GetIDAddress
    {
        public static string myIPAddress()
        {
            string hostName= Dns.GetHostName();
            string myIP= Dns.GetHostByName(hostName).AddressList[0].ToString();
            return myIP;
        }
    }
}
