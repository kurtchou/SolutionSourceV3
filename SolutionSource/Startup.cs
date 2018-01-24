using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SolutionSource.Startup))]
namespace SolutionSource
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
