using System.Web.Http;
using GildedRose.WebServices.DataAccess;
using GildedRose.WebServices.Services;
using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin;
using Microsoft.Practices.Unity;
using Owin;

// The OwinStartup attribute indicates to Owin which class to instantiate as the Startup class
[assembly: OwinStartup(typeof(GildedRose.WebServices.Startup))]

namespace GildedRose.WebServices
{
	/// <summary>
	/// The startup class for the Web Services web app.
	/// </summary>
	public class Startup
    {
		private const string IdentityServerUrl = "http://localhost:5000";

		/// <summary>
		/// Configures the specified Owin application.
		/// </summary>
		/// <param name="app">The Owin application.</param>
		public void Configuration(IAppBuilder app)
        {
            // accept access tokens from identityserver and require a scope of 'api1'
	        app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
                {
                    Authority = IdentityServerUrl,
                    ValidationMode = ValidationMode.ValidationEndpoint,
					RequiredScopes = new[] { "gildedroseapi" }
                });

			// configure our web api
			var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

			// dependency injection
			var container = new UnityContainer();
			
			// register the data store dependencies as a single instance per container to allow for in-memory peristence
			container.RegisterType<IDataProvider, DataProvider>(new ContainerControlledLifetimeManager());
			container.RegisterType<IDataStore, DataStore>(new ContainerControlledLifetimeManager());
			
			// the balance of dependencies can be new instances each time
			container.RegisterType<IInventoryService, InventoryService>();
			config.DependencyResolver = new UnityDependencyResolver(container);

			// start as a Web Api
			app.UseWebApi(config);
        }
    }
}