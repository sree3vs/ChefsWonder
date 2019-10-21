using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChefsWonders.Startup))]
namespace ChefsWonders
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
