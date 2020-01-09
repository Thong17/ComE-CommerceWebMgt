using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(E_CommerceAssignment.Startup))]
namespace E_CommerceAssignment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
