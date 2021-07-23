using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(E_learning.Startup))]
namespace E_learning
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
