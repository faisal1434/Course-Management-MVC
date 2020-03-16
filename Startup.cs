using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CourseManagement_MVC.Startup))]
namespace CourseManagement_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
