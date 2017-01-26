using System.Collections.Generic;

using GildedRose.WebServices.Models;

namespace GildedRose.WebServices.Services
{
	public interface IInventoryService
	{
		IList<Item> GetAvailableItems();

		bool BuyItem(string itemId);
	}
}