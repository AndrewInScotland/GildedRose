namespace GildedRose.WebServices.Controllers
{
	using System;
	using System.Security.Claims;
	using System.Web.Http;
	using Services;

	/// <summary>
	/// Provides Inventory methods.
	/// </summary>
	/// <seealso cref="System.Web.Http.ApiController" />
	[Route("Inventory")]
	public class InventoryController : ApiController
	{
		// "sub" is the standard name for the user identifer claim
		private const string UserIdentifierClaim = "sub";

		private readonly IInventoryService inventoryService;

		/// <summary>
		/// Initializes a new instance of the <see cref="InventoryController"/> class.
		/// </summary>
		/// <param name="inventoryService">The inventory service.</param>
		/// <exception cref="System.ArgumentNullException">Thrown when dependencies are null.</exception>
		public InventoryController(IInventoryService inventoryService)
		{
			if (inventoryService == null)
			{
				throw new ArgumentNullException(nameof(inventoryService));
			}

			this.inventoryService = inventoryService;
		}

		/// <summary>
		/// Gets all Inventory items.
		/// </summary>
		/// <returns>A list of inventory items as a Json string.</returns>
		[HttpGet]
		public IHttpActionResult Get()
		{
			IHttpActionResult returnValue;

			try
			{
				var availableItems = this.inventoryService.GetAvailableItems();
				returnValue = this.Json(availableItems);
			}
			catch (Exception)
			{
				// something went wrong. We should log this, but hide the internals from the API user.
				returnValue = this.InternalServerError();
			}

			return returnValue;
		}

		/// <summary>
		/// Buys the item, if there is sufficient inventory.
		/// </summary>
		/// <param name="itemId">The item identifier.</param>
		/// <returns>A Json string indicating whether or not the purchase was successful.</returns>
		[HttpPut]
		[Authorize]
		public IHttpActionResult BuyItem([FromBody] string itemId)
		{
			IHttpActionResult returnValue;

			try
			{
				if (string.IsNullOrEmpty(itemId))
				{
					returnValue = this.BadRequest("Item Id can not be null");
				}
				else
				{
					var caller = this.User as ClaimsPrincipal;
					var subjectClaim = caller?.FindFirst(UserIdentifierClaim);
					if (subjectClaim == null)
					{
						returnValue = this.Unauthorized();
					}
					else
					{
						var purchaseResult = this.inventoryService.BuyItem(itemId);
						returnValue = this.Json(purchaseResult);
					}
				}
			}
			catch (Exception)
			{
				// something went wrong. We should log this, but hide the internals from the API user.
				returnValue = this.InternalServerError();
			}

			return returnValue;
		}
	}
}