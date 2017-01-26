namespace GildedRose.WebServices.DataAccess
{
	using System.Collections.Generic;
	using Models;

	/// <summary>
	/// Interface for the data storage layer.
	/// </summary>
	public interface IDataStore
	{
		/// <summary>
		/// Gets all of the items from storage.
		/// </summary>
		/// <returns>A list of items.</returns>
		IList<Item> GetItems();

		/// <summary>
		/// Gets the specified item from storage.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>The item.</returns>
		Item GetItem(string id);

		/// <summary>
		/// Saves the specified item to storage.
		/// </summary>
		/// <param name="itemToSave">The item to save.</param>
		void SaveItem(Item itemToSave);
	}
}