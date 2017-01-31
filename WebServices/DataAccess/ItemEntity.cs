namespace GildedRose.WebServices.DataAccess
{
	using System;

	using GildedRose.WebServices.Models;

	/// <summary>
	/// Represents a data entity of type Item.
	/// </summary>
	/// <remarks>
	/// The ItemEntity class represents a an object ready for storage, or ready to be converted to a model.
	/// This serves as an isolation layer between the Presentation Layer and the Data Layer.
	/// See also the Item class, which represents the model (or Data Transfer Object). 
	/// </remarks>
	public class ItemEntity
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ItemEntity"/> class.
		/// </summary>
		public ItemEntity()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ItemEntity" /> class.
		/// </summary>
		/// <param name="itemModel">The item model from which to initialize the ItemEntity properties.</param>
		/// <exception cref="System.ArgumentNullException">Thrown when dependencies are null.</exception>
		public ItemEntity(Item itemModel)
		{
			if (itemModel == null)
			{
				throw new ArgumentNullException(nameof(itemModel));
			}

			this.Id = itemModel.Id;
			this.Name = itemModel.Name;
			this.Description = itemModel.Description;
			this.Price = itemModel.Price;
			this.InventoryCount = itemModel.InventoryCount;
		}
		
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

		/// <summary>
		/// Converts the Entity to a Model.
		/// </summary>
		/// <returns>An item.</returns>
		public Item ToModel()
		{
			return new Item
						{
							Id = this.Id,
							Name = this.Name,
							Description = this.Description,
							Price = this.Price,
							InventoryCount = this.InventoryCount
						};
		}
	}
}