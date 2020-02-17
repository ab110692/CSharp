using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Br.Com.Posi.Avarc.Web.Startup))]
namespace Br.Com.Posi.Avarc.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
