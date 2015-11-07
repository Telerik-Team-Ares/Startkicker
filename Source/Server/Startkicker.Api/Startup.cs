using Microsoft.Owin;
using Owin;
[assembly: OwinStartup(typeof(Startkicker.Api.Startup))]

namespace Startkicker.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
