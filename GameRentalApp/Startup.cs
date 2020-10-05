using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GameRentalApp.Startup))]
namespace GameRentalApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
