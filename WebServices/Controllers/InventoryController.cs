namespace GildedRose.WebServices.Controllers
{
	using System;
	using System.Security.Claims;
	using System.Web.Http;
	using Newtonsoft.Json;
	using Services;

	[Route("Inventory")]
	public class InventoryController : ApiController
	{
		// "sub" is the standard name for the user identifer claim
		private const string UserIdentifierClaim = "sub";

		private readonly IInventoryService inventoryService;

		public InventoryController(IInventoryService inventoryService)
		{
			if (inventoryService == null)
			{
				throw new ArgumentNullException(nameof(inventoryService));
			}

			this.inventoryService = inventoryService;
		}

		[HttpGet]
		public IHttpActionResult Get()
		{
			IHttpActionResult returnValue;

			try
			{
				var availableItems = this.inventoryService.GetAvailableItems();
				var jsonResult = JsonConvert.SerializeObject(availableItems);
				returnValue = this.Json(jsonResult);
			}
			catch (Exception)
			{
				returnValue = this.InternalServerError();
			}

			return returnValue;
		}

		[HttpPut]
		[Authorize]
		public IHttpActionResult BuyItem([FromBody] string itemId)
		{
			IHttpActionResult returnValue;

			if (string.IsNullOrEmpty(itemId))
			{
				return this.BadRequest("Item Id can not be null");
			}

			try
			{
				var caller = this.User as ClaimsPrincipal;
				var subjectClaim = caller?.FindFirst(UserIdentifierClaim);
				if (subjectClaim == null)
				{
					returnValue = this.Unauthorized();
				}
				else
				{
					var itemBoughtSuccessfully = this.inventoryService.BuyItem(itemId);
					
					// add structure to the Json by serializing an anonymous type
					var jsonResult = JsonConvert.SerializeObject(new { ItemBoughtSuccessfully = itemBoughtSuccessfully });
					returnValue = this.Json(jsonResult);
				}
			}
			catch (Exception)
			{
				returnValue = this.InternalServerError();
			}

			return returnValue;
		}
	}
}