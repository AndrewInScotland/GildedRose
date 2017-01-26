using System.Collections.Generic;

using GildedRose.WebServices.Models;

namespace GildedRose.WebServices.DataAccess
{
	public interface IDataProvider
	{
		IList<Item> ItitializeData();
	}
}