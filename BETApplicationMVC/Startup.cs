using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BETApplicationMVC.Shopify.Startup))]
namespace BETApplicationMVC.Shopify
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
