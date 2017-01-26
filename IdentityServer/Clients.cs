namespace GildedRose.IdentityServer
{
	using System.Collections.Generic;
	using IdentityServer3.Core.Models;

	/// <summary>
	/// In-memory implementation of the client apps known by the system.
	/// </summary>
	public static class Clients
    {
		/// <summary>
		/// Gets the list of registered Client Apps.
		/// </summary>
		/// <returns>A list of registered Client Apps.</returns>
		public static List<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientName = "Gilden Rose Web App",
                    ClientId = "gildedrose",
                    Enabled = true,
                    AccessTokenType = AccessTokenType.Reference,

	                // the gildedrose API will be accessed by humans, not systems, so use the ResourceOwner flow
                    Flow = Flows.ResourceOwner,
                    
                    ClientSecrets = new List<Secret>
                    {
						new Secret("8D29FA9F-2F39-4985-8ED8-3DCBED217FED".Sha256())
					},

                    AllowedScopes = new List<string>
                    {
						Scopes.ApiScope
					}
                }
            };
        }
    }
}