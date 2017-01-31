namespace GildedRose.WebServices.DataAccess
{
	using System.Collections.Generic;

	/// <summary>
	/// Interface for the data provider. Allows for temporary in-memory instances as needed.
	/// </summary>
	public interface IDataProvider
	{
		/// <summary>
		/// Populates this data provider with ititial data.
		/// </summary>
		/// <returns>A list of inventory items.</returns>
		IList<ItemEntity> ItitializeData();
	}
}