namespace GildedRose.WebServices.DataAccess
{
	using System.Collections.Generic;
	using Models;

	public interface IDataStore
	{
		IList<Item> GetItems();

		Item GetItem(string id);

		void SaveItem(Item itemToSave);
	}
}