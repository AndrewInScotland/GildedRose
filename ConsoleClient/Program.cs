namespace GildedRose.ConsoleClient
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net.Http;
	using IdentityModel.Client;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

	/// <summary>
	/// The Console Client startup class.
	/// </summary>
	public class Program
    {
	    private const string IdentityServiceUrl = "http://localhost:5000/connect/token";
		private const string WebApiUrl = "http://localhost:14869/Inventory";

		/// <summary>
		/// Main method of the Console Startup class.
		/// </summary>
		/// <param name="args">The command-line arguments, if any.</param>
		public static void Main(string[] args)
        {
	        try
	        {
				// GetAllItems returns a collection of dynamic Json objects
				var allItems = GetAllItems();
		        if (allItems.Count == 0)
		        {
			        Console.WriteLine("Could not buy an item because no items are available.");
		        }
		        else
		        {
					// arbitrarily buy the first item in the list
					var firstItem = allItems.First();
			        BuyItem(firstItem);
		        }
	        }
	        catch (Exception ex)
	        {
		        Console.WriteLine(ex.Message + Environment.NewLine + ex);
		        Environment.Exit(-1);
	        }
		}

	    private static IList<dynamic> GetAllItems()
	    {
		    var allItems = new List<dynamic>();
			var client = new HttpClient();
		    var response = client.GetAsync(WebApiUrl).Result;
		    if (!response.IsSuccessStatusCode)
		    {
			    Console.WriteLine("Get Items Failed: " + response.StatusCode);
		    }
		    else
		    {
				Console.WriteLine("Get Items Succeeded:");
				var responseBody = response.Content.ReadAsStringAsync().Result;
			    var jsonResponse = JsonConvert.DeserializeObject<string>(responseBody);
			    JArray jsonArray = JArray.Parse(jsonResponse);
			    dynamic boats = jsonArray;
				foreach (dynamic boat in boats)
				{
					Console.WriteLine($"Boat Name:'{boat.Name}' Decription:'{boat.Description}' Price:{boat.Price} Inventory Remaining:{boat.InventoryCount}");
					allItems.Add(boat);
				}

				Console.WriteLine();
			}

		    return allItems;
	    }

	    private static void BuyItem(dynamic item)
		{
			Console.WriteLine($"Attempting to buy a '{item.Name}'");

			// this request requires authentication, so set up the bearer token
			var tokenResponse = GetUserToken();
			var client = new HttpClient();
			client.SetBearerToken(tokenResponse.AccessToken);

			var response = client.PutAsJsonAsync(WebApiUrl, (string)item.Id).Result;
			if (!response.IsSuccessStatusCode)
			{
				Console.WriteLine("Http Put on Iventory Failed: " + response.StatusCode);
			}
			else
			{
				// decode the json result
				string responseBody = response.Content.ReadAsStringAsync().Result;
				var jsonResponse = JsonConvert.DeserializeObject<string>(responseBody);
				dynamic json = JToken.Parse(jsonResponse);
				bool itemBoughtSuccessfully = json.ItemBoughtSuccessfully;

				if (itemBoughtSuccessfully)
				{
					Console.WriteLine($"Item '{item.Name}' was bought successfully!");
				}
				else
				{
					Console.WriteLine($"Item '{item.Name}' could not be bought - there are no more left!");
				}
			}
		}

		private static TokenResponse GetUserToken()
		{
			var client = new TokenClient(
				   IdentityServiceUrl,
				   "gildedrose",
			 "8D29FA9F-2F39-4985-8ED8-3DCBED217FED");

			return client.RequestResourceOwnerPasswordAsync("bob", "secret", "gildedroseapi").Result;
		}
	}
}