namespace GildedRose.WebServices.Services
{
	using System.Collections.Generic;
	using Models;

	public interface IInventoryService
	{
		IList<Item> GetAvailableItems();

		bool BuyItem(string itemId);
	}
}