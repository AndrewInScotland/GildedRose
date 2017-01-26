namespace GildedRose.IdentityServer
{
	using IdentityServer3.Core.Configuration;
	using Owin;

	/// <summary>
	/// The Startup class for the Owin web server.
	/// </summary>
	public class Startup
    {
		/// <summary>
		/// Configures the specified Owin application.
		/// </summary>
		/// <param name="app">The Owin application.</param>
		public void Configuration(IAppBuilder app)
        {
            var options = new IdentityServerOptions
            {
                Factory = new IdentityServerServiceFactory()
                            .UseInMemoryClients(Clients.Get())
                            .UseInMemoryScopes(Scopes.Get())
                            .UseInMemoryUsers(Users.Get()),

                RequireSsl = false
            };

            app.UseIdentityServer(options);
        }
    }
}