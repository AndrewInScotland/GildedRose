namespace GildedRose.WebServices.Models
{
	/// <summary>
	/// Represents the result of the purchase of an inventory item.
	/// </summary>
	public class PurchaseResult
	{
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="PurchaseResult"/> is success.
		/// </summary>
		/// <value>
		///   <c>true</c> if success; otherwise, <c>false</c>.
		/// </value>
		public bool Success { get; set; }
	}
}