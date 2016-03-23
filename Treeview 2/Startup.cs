using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Treeview_2.Startup))]
namespace Treeview_2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
