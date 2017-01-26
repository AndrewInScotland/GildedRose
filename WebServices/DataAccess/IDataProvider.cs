namespace GildedRose.WebServices.DataAccess
{
	using System.Collections.Generic;
	using GildedRose.WebServices.Models;

	public interface IDataProvider
	{
		IList<Item> ItitializeData();
	}
}