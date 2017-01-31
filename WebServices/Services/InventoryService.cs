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
			var items = new List<Item>();
			var itemEntities = this.dataStore.GetItems();

			// this could be converted into a pretty Linq statement, but I prefer the clarity and debugability of the loop.
			foreach (var itemEntity in itemEntities)
			{
				items.Add(itemEntity.ToModel());
			}

			return items;
		}

		/// <summary>
		/// Buys the item.
		/// </summary>
		/// <param name="itemId">The item identifier.</param>
		/// <returns>
		/// A result value indicating whether or not the item could be purchased.
		/// </returns>
		public PurchaseResult BuyItem(string itemId)
		{
			var result = new PurchaseResult { Success = false };

			// use a transaction to ensure atomicty of this operation
			using (TransactionScope scope = new TransactionScope())
			{
				var item = this.dataStore.GetItem(itemId);

				if (item?.InventoryCount > 0)
				{
					item.InventoryCount--;
					this.dataStore.SaveItem(item);
					scope.Complete();
					result.Success = true;
				}
			}

			return result;
		}
	}
}