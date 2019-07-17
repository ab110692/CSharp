using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Br.Com.Posi.Util.Network
{
    public static class IPAddressExtension
    {
        public static IPAddress GetMyIP(this IPAddress ip)
        {
            return Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(i => i.AddressFamily == AddressFamily.InterNetwork).FirstOrDefault();
        }
    }
}
