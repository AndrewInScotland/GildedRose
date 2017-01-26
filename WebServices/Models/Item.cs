namespace GildedRose.WebServices.Models
{
	/// <summary>
	/// Represents an item for sale at the Gilded Rose.
	/// </summary>
	public class Item
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value> The identifier. </value>
		public string Id { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value> The name. </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value> The description. </value>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the price.
		/// </summary>
		/// <value> The price. </value>
		public int Price { get; set; }

		/// <summary>
		/// Gets or sets the inventory count.
		/// </summary>
		/// <value> The inventory count. </value>
		public int InventoryCount { get; set; }
	}
}