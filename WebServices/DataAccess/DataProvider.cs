using System.Collections.Generic;

using GildedRose.WebServices.Models;

namespace GildedRose.WebServices.DataAccess
{
	public class DataProvider : IDataProvider
	{
		public IList<Item> ItitializeData()
		{
			return new List<Item>
						{
							new Item
								{
									Id = "746E4CFB-7DF8-46BC-9A94-923F29D17907",
									Name = "CSY 37",
									Description = "37 foot cutter-rigged sloop",
									Price = 40000,
									InventoryCount = 3
								},
							new Item
								{
									Id = "2552E1C7-64C5-460F-B550-60F3A720033F",
									Name = "Venture 24",
									Description = "24 foot swing keel",
									Price = 5000,
									InventoryCount = 10
								}
						};
		}
	}
}