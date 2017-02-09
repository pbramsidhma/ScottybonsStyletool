using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ScottybonsStylist.Startup))]
namespace ScottybonsStylist
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
