namespace GildedRose.WebServices.DataAccess
{
	using System.Collections.Generic;

	/// <summary>
	/// A temporary memory-only data provider.
	/// </summary>
	/// <seealso cref="GildedRose.WebServices.DataAccess.IDataProvider" />
	public class DataProvider : IDataProvider
	{
		/// <summary>
		/// Populates this data provider with ititial data.
		/// </summary>
		/// <returns>A list of inventory items.</returns>
		public IList<ItemEntity> ItitializeData()
		{
			return new List<ItemEntity>
						{
							new ItemEntity
								{
									Id = "746E4CFB-7DF8-46BC-9A94-923F29D17907",
									Name = "CSY 37",
									Description = "37 foot cutter-rigged sloop",
									Price = 40000,
									InventoryCount = 3
								},
							new ItemEntity
								{
									Id = "2552E1C7-64C5-460F-B550-60F3A720033F",
									Name = "Venture 24",
									Description = "24 foot swing keel",
									Price = 5000,
									InventoryCount = 10
								}
						};
		}
	}
}