using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebAppAspNetMvcGrid.Startup))]
namespace WebAppAspNetMvcGrid
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
