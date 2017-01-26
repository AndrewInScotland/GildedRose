// ReSharper disable NotAccessedVariable
namespace GildedRose.WebServices.Test
{
	using System;
	using System.Collections.Generic;
	using System.Web.Http.Results;

	using FluentAssertions;

	using GildedRose.WebServices.Controllers;
	using GildedRose.WebServices.Models;
	using GildedRose.WebServices.Services;

	using Moq;

	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

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
			var jsonResponse = controller.Get() as System.Web.Http.Results.JsonResult<string>;

			// assert
			JArray jsonArray = JArray.Parse(jsonResponse?.Content);
			jsonArray.Count.Should().Be(0);
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
			var jsonResponse = controller.Get() as System.Web.Http.Results.JsonResult<string>;

			// assert
			JArray jsonArray = JArray.Parse(jsonResponse?.Content);
			jsonArray.Count.Should().Be(2);
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
			var jsonResponse = controller.Get() as System.Web.Http.Results.JsonResult<string>;

			// assert
			JArray jsonArray = JArray.Parse(jsonResponse?.Content);
			dynamic itemOne = jsonArray[0];
			((string)itemOne.Name).Should().Be("item 1");
			dynamic itemTwo = jsonArray[1];
			((string)itemTwo.Name).Should().Be("item 2");
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
	}
}
