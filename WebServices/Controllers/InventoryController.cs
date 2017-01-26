using System;
using System.Security.Claims;
using System.Web.Http;

using GildedRose.WebServices.Services;

using Newtonsoft.Json;

namespace GildedRose.WebServices.Controllers
{
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
			try
			{
				var availableItems = this.inventoryService.GetAvailableItems();
				var jsonResult = JsonConvert.SerializeObject(availableItems);
				return this.Json(jsonResult);
			}
			catch (Exception)
			{
				return this.InternalServerError();
			}
		}

		[HttpPut]
		[Authorize]
		public IHttpActionResult BuyItem([FromBody] string itemId)
		{
			IHttpActionResult returnValue = null;

			if (string.IsNullOrEmpty(itemId))
			{
				return this.BadRequest("Item Id can not be null");
			}

			try
			{
				var caller = this.User as ClaimsPrincipal;
				var subjectClaim = caller?.FindFirst(UserIdentifierClaim);
				if (subjectClaim != null)
				{
					var itemBoughtSuccessfully = this.inventoryService.BuyItem(itemId);
					var jsonResult = new { ItemBoughtSuccessfully = itemBoughtSuccessfully };
					returnValue = this.Json(jsonResult);
				}
			}
			catch (Exception)
			{
				return this.InternalServerError();
			}

			return returnValue;
		}
	}
}