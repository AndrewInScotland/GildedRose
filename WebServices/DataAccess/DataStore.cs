namespace GildedRose.WebServices.DataAccess
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Models;

	/// <summary>
	/// The storage layer for inventory data.
	/// </summary>
	/// <seealso cref="GildedRose.WebServices.DataAccess.IDataStore" />
	public class DataStore : IDataStore
	{
		private readonly IDataProvider dataProvider;

		private IList<Item> items;

		/// <summary>
		/// Initializes a new instance of the <see cref="DataStore"/> class.
		/// </summary>
		/// <param name="dataProvider">The data provider.</param>
		/// <exception cref="System.ArgumentNullException">Thrown when dependencies are null.</exception>
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

		/// <summary>
		/// Gets all of the items from storage.
		/// </summary>
		/// <returns> A list of items.</returns>
		public IList<Item> GetItems()
		{
			return this.Items;
		}

		/// <summary>
		/// Gets the specified item from storage.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns> The item.</returns>
		public Item GetItem(string id)
		{
			return this.Items.FirstOrDefault(item => item.Id == id);
		}

		/// <summary>
		/// Saves the specified item to storage.
		/// </summary>
		/// <param name="itemToSave">The item to save.</param>
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