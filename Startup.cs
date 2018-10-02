using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CPP2.Startup))]

namespace CPP2
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			ConfigureAuth(app);
		}
	}
}