// ReSharper disable NotAccessedVariable to handle cases where expected exceptions are thrown before the instance can be used
// ReSharper disable PossibleNullReferenceException to allow unexpected NullRefExceptions to fail the test
namespace GildedRose.WebServices.Test
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Security.Claims;
	using System.Web.Http.Results;

	using FluentAssertions;

	using Controllers;
	using Models;
	using Services;

	using Moq;

	using NUnit.Framework;

	[TestFixture]
	public class InventoryControllerTests
	{
		[Test]
		public void Constructor_NoService_ThrowsArgumentNullException()
		{
			// arrange
			InventoryController controller = null;

			// act
			Action action = () => controller = new InventoryController(null);

			// assert
			action.ShouldThrow<ArgumentNullException>();
		}

		[Test]
		public void Get_NoItems_ReturnsEmptyArray()
		{
			// arrange
			var inventoryServiceMock = new Mock<IInventoryService>();
			inventoryServiceMock.Setup(m => m.GetAvailableItems()).Returns(new List<Item>());

			// act
			var controller = new InventoryController(inventoryServiceMock.Object);
			var jsonResponse = controller.Get() as JsonResult<IList<Item>>;

			// assert
			jsonResponse.Content.Count.Should().Be(0);
		}

		[Test]
		public void Get_WithItems_ReturnsPopulatedArray()
		{
			// arrange
			var inventoryServiceMock = new Mock<IInventoryService>();
			inventoryServiceMock.Setup(m => m.GetAvailableItems())
				.Returns(new List<Item> { new Item { Name = "item 1" }, new Item { Name = "item 2" } });

			// act
			var controller = new InventoryController(inventoryServiceMock.Object);
			var jsonResponse = controller.Get() as JsonResult<IList<Item>>;

			// assert
			jsonResponse.Content.Count.Should().Be(2);
		}

		[Test]
		public void Get_WithItems_ReturnsGoodJson()
		{
			// arrange
			var inventoryServiceMock = new Mock<IInventoryService>();
			inventoryServiceMock.Setup(m => m.GetAvailableItems())
				.Returns(new List<Item> { new Item { Name = "item 1" }, new Item { Name = "item 2" } });

			// act
			var controller = new InventoryController(inventoryServiceMock.Object);
			var jsonResponse = controller.Get() as JsonResult<IList<Item>>;

			// assert
			var itemOne = jsonResponse?.Content.First();
			itemOne.Name.Should().Be("item 1");
			var itemTwo = jsonResponse?.Content.Last();
			itemTwo.Name.Should().Be("item 2");
		}

		[Test]
		public void Get_WithServiceError_ReturnsInternalServerError()
		{
			// arrange
			var inventoryServiceMock = new Mock<IInventoryService>();
			inventoryServiceMock.Setup(m => m.GetAvailableItems()).Throws(new Exception("some service exception"));

			// act
			var controller = new InventoryController(inventoryServiceMock.Object);
			var response = controller.Get();
			
			// assert
			response.GetType().Should().Be(typeof(InternalServerErrorResult));
		}

		[Test]
		public void BuyItem_NoItemId_ReturnsBadRequestError()
		{
			// arrange
			var inventoryServiceMock = new Mock<IInventoryService>();
			inventoryServiceMock.Setup(m => m.BuyItem(It.IsAny<string>())).Returns(new PurchaseResult { Success = true });

			// act
			var controller = new InventoryController(inventoryServiceMock.Object);
			var response = controller.BuyItem(null);

			// assert
			response.GetType().Should().Be(typeof(BadRequestErrorMessageResult));
		}

		[Test]
		public void BuyItem_NoClaims_ReturnsUnauthorized()
		{
			// arrange
			var inventoryServiceMock = new Mock<IInventoryService>();
			inventoryServiceMock.Setup(m => m.BuyItem(It.IsAny<string>())).Returns(new PurchaseResult { Success = true });

			// act
			var controller = new InventoryController(inventoryServiceMock.Object);
			var response = controller.BuyItem("item id 1");

			// assert
			response.GetType().Should().Be(typeof(UnauthorizedResult));
		}

		[Test]
		public void BuyItem_WithClaimsAndAvailableInventory_ReturnsJsonAsTrue()
		{
			// arrange
			var inventoryServiceMock = new Mock<IInventoryService>();
			inventoryServiceMock.Setup(m => m.BuyItem(It.IsAny<string>())).Returns(new PurchaseResult { Success = true });
			var controller = new InventoryController(inventoryServiceMock.Object)
			{
				User = new ClaimsPrincipal(CreateControllerIdentity())
			};

			// act
			var jsonResult = controller.BuyItem("item id 1") as JsonResult<PurchaseResult>;

			// assert
			jsonResult.Content.Success.Should().BeTrue();
		}

		[Test]
		public void BuyItem_WithClaimsAndNoInventory_ReturnsJsonAsFalse()
		{
			// arrange
			var inventoryServiceMock = new Mock<IInventoryService>();
			inventoryServiceMock.Setup(m => m.BuyItem(It.IsAny<string>())).Returns(new PurchaseResult { Success = false });
			var controller = new InventoryController(inventoryServiceMock.Object)
			{
				User = new ClaimsPrincipal(CreateControllerIdentity())
			};

			// act
			var jsonResult = controller.BuyItem("item id 1") as JsonResult<PurchaseResult>;

			// assert
			jsonResult.Content.Success.Should().BeFalse();
		}

		private static ClaimsIdentity CreateControllerIdentity()
		{
			ClaimsIdentity identity = new ClaimsIdentity();
			identity.AddClaim(new Claim("sub", "some.user"));
			return identity;
		}
	}
}
