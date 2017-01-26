using System.Collections.Generic;

using GildedRose.WebServices.Models;

namespace GildedRose.WebServices.DataAccess
{
	public interface IDataStore
	{
		IList<Item> GetItems();

		Item GetItem(string id);

		void SaveItem(Item itemToSave);
	}
}