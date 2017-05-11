using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(foodtruckaliService.Startup))]

namespace foodtruckaliService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}