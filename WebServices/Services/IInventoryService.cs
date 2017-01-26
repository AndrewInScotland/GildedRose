namespace GildedRose.WebServices.Services
{
	using System.Collections.Generic;
	using Models;

	/// <summary>
	/// Interface for the Inventory Service, which provides business logic for various Inventory activities.
	/// </summary>
	public interface IInventoryService
	{
		/// <summary>
		/// Gets all the items available at the Gilded Rose.
		/// </summary>
		/// <returns>A list of all the items.</returns>
		IList<Item> GetAvailableItems();

		/// <summary>
		/// Buys the item.
		/// </summary>
		/// <param name="itemId">The item identifier.</param>
		/// <returns>A value indicating whether or not the item could be purchased.</returns>
		bool BuyItem(string itemId);
	}
}