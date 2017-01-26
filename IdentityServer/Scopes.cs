namespace GildedRose.IdentityServer
{
	using System.Collections.Generic;
	using IdentityServer3.Core.Models;

	/// <summary>
	/// In-memory implementation of the scopes known by the system.
	/// </summary>
	public static class Scopes
    {
		/// <summary>
		/// The API scope
		/// </summary>
		public const string ApiScope = "gildedroseapi";

		/// <summary>
		/// Gets the list of registered Scopes.
		/// </summary>
		/// <returns>A list of registered Scopes.</returns>
		public static List<Scope> Get()
        {
	        return new List<Scope>
            {
                new Scope
                {
                    Name = ApiScope
                }
            };
        }
    }
}