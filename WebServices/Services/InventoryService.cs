using System;
using System.Collections.Generic;

using System.Transactions;

using GildedRose.WebServices.DataAccess;
using GildedRose.WebServices.Models;

namespace GildedRose.WebServices.Services
{
	public class InventoryService : IInventoryService
	{
		private readonly IDataStore dataStore;

		public InventoryService(IDataStore dataStore)
		{
			if (dataStore == null)
			{
				throw new ArgumentNullException(nameof(dataStore));
			}

			this.dataStore = dataStore;
		}

		public IList<Item> GetAvailableItems()
		{
			return this.dataStore.GetItems();
		}

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