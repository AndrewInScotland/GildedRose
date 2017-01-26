namespace GildedRose.WebServices.Models
{
	/// <summary>
	/// Represents an item for sale at the Gilded Rose.
	/// </summary>
	public class Item
	{
		public string Id { get; set; }
		
		public string Name { get; set; }

		public string Description { get; set; }

		public int Price { get; set; }

		public int InventoryCount { get; set; }
	}
}