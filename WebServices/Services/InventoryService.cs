namespace GildedRose.WebServices.Services
{
	using System;
	using System.Collections.Generic;
	using System.Transactions;
	using DataAccess;
	using Models;

	/// <summary>
	/// The Inventory Service, which provides business logic for various Inventory activities.
	/// </summary>
	public class InventoryService : IInventoryService
	{
		private readonly IDataStore dataStore;

		/// <summary>
		/// Initializes a new instance of the <see cref="InventoryService"/> class.
		/// </summary>
		/// <param name="dataStore">The data store.</param>
		/// <exception cref="System.ArgumentNullException">Thrown when dependencies are null.</exception>
		public InventoryService(IDataStore dataStore)
		{
			if (dataStore == null)
			{
				throw new ArgumentNullException(nameof(dataStore));
			}

			this.dataStore = dataStore;
		}

		/// <summary>
		/// Gets all the items available at the Gilded Rose.
		/// </summary>
		/// <returns>
		/// A list of all the items.
		/// </returns>
		public IList<Item> GetAvailableItems()
		{
			return this.dataStore.GetItems();
		}

		/// <summary>
		/// Buys the item.
		/// </summary>
		/// <param name="itemId">The item identifier.</param>
		/// <returns>
		/// A value indicating whether or not the item could be purchased.
		/// </returns>
		public bool BuyItem(string itemId)
		{
			bool itemBoughtSuccessfully = false;

			using (TransactionScope scope = new TransactionScope())
			{
				var item = this.dataStore.GetItem(itemId);

				if (item?.InventoryCount > 0)
				{
					item.InventoryCount--;
					this.dataStore.SaveItem(item);
					scope.Complete();
					itemBoughtSuccessfully = true;
				}
			}

			return itemBoughtSuccessfully;
		}
	}
}