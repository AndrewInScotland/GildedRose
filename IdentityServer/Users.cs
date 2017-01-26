using System.Collections.Generic;
using IdentityServer3.Core.Services.InMemory;

namespace GildedRose.IdentityServer
{
	/// <summary>
	/// In-memory implementation of the registered users.
	/// </summary>
	public static class Users
    {
		/// <summary>
		/// Gets the list of registered Users.
		/// </summary>
		/// <returns>A list of registered Users.</returns>
		public static List<InMemoryUser> Get()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Username = "bob",
                    Password = "secret",
                    Subject = "UniqueId-1"
                },
                new InMemoryUser
                {
                    Username = "alice",
                    Password = "secret",
                    Subject = "UniqueId-2"
				}
            };
        }
    }
}