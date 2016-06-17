using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(itforum_teamwork.Startup))]
namespace itforum_teamwork
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}