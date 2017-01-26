using System;
using System.Collections.Generic;
using System.Linq;

using GildedRose.WebServices.Models;

namespace GildedRose.WebServices.DataAccess
{
	public class DataStore : IDataStore
	{
		private readonly IDataProvider dataProvider;

		private IList<Item> items;

		public DataStore(IDataProvider dataProvider)
		{
			if (dataProvider == null)
			{
				throw new ArgumentNullException(nameof(dataProvider));
			}

			this.dataProvider = dataProvider;
		}

		private IList<Item> Items
		{
			get
			{
				if (this.items == null)
				{
					this.items = this.dataProvider.ItitializeData();
				}

				return this.items;
			}
		}

		public IList<Item> GetItems()
		{
			return this.Items;
		}

		public Item GetItem(string id)
		{
			return this.Items.FirstOrDefault(item => item.Id == id);
		}

		public void SaveItem(Item itemToSave)
		{
			// find it by Id
			Item existingItem = null;
			if (!string.IsNullOrEmpty(itemToSave.Id))
			{
				existingItem = this.Items.FirstOrDefault(i => i.Id == itemToSave.Id);
				if (existingItem != null)
				{
					// if it's there, replace it entirely
					this.Items[this.Items.IndexOf(existingItem)] = itemToSave;
				}
			}

			if (existingItem == null)
			{ 
				// Id is empty or Id is not found - this is a new item
				if (string.IsNullOrEmpty(itemToSave.Id))
				{
					// Id not suppplied, provide a new one
					itemToSave.Id = Guid.NewGuid().ToString();
				}

				this.Items.Add(itemToSave);
			}
		}
	}
}