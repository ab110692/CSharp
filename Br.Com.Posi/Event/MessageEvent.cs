using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Com.Posi.Event
{
    public class MessageArgs
    {
        public string Message { get; set; }
    }

    public delegate void MessageEventHandler(MessageArgs args);
    public delegate MessageArgs MessageReportEventHandler();
    public static class MessageEvent
    {
        public static void Raises(this MessageEventHandler handler, MessageArgs args)
        {
            handler?.Invoke(args);
        }
    }
}
