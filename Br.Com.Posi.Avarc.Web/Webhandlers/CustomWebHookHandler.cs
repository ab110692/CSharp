using Microsoft.AspNet.WebHooks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Br.Com.Posi.Avarc.Web.Controllers
{
    public class CustomWebHookHandler : WebHookHandler
    {

        public CustomWebHookHandler()
        {
            //this.Receiver = CustomWebHookReceiver.ReceiverName;
            this.Receiver = "custom";
        }

        public override Task ExecuteAsync(string generator, WebHookHandlerContext context)
        {
            // Get data from WebHook
            CustomNotifications data = context.GetDataOrDefault<CustomNotifications>();

            // Get data from each notification in this WebHook
            foreach (IDictionary<string, object> notification in data.Notifications)
            {
                // Process data
            }

            return Task.FromResult(true);
        }

    }
}