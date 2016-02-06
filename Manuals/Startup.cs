using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Manuals.Startup))]
namespace Manuals
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
