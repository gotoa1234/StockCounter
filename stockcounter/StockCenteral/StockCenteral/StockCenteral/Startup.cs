using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StockCenteral.Startup))]
namespace StockCenteral
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
