// ReSharper disable NotAccessedVariable to handle cases where expected exceptions are thrown before the instance can be used
using NUnit.Framework;
using System;
using System.Linq;
using FluentAssertions;
using GildedRose.WebServices.DataAccess;
using GildedRose.WebServices.Services;

namespace GildedRose.WebServices.Test
{
	[TestFixture]
	public class InventoryServiceTests
	{

		[Test]
		public void Constructor_NoDataStore_ThrowsArgumentNullException()
		{
			// arrange
			IInventoryService service = null;

			// act
			Action action = () => service = new InventoryService(null);

			// assert
			action.ShouldThrow<ArgumentNullException>();
		}

		[Test]
		public void GetAvailableItems_UsingDummyData_ReturnsItems()
		{
			// arrange
			var dataStore = new DataStore(new DummyDataProvider());
			var service = new InventoryService(dataStore);
			
			// act
			var allItems = service.GetAvailableItems();
			
			// assert
			allItems.Count.Should().Be(2);
		}

		[Test]
		public void GetAvailableItems_AfterSettingModelProperty_ReturnsStoredItem()
		{
			/* An unusual test, to be sure. We just want to make sure that the memory-only storage system
			 * does not contain direct references to our Item model. 
			 */
			
			// arrange
			var dataStore = new DataStore(new DummyDataProvider());
			var service = new InventoryService(dataStore);
			var firstResultSet = service.GetAvailableItems();
			var editedItem = firstResultSet.First();
			editedItem.Description = "This item has been changed";
			
			// act
			var secondResultSet = service.GetAvailableItems();

			// assert
			secondResultSet.First().Description.Should().NotContain("has been changed");
		}
		
		[Test]
		public void BuyItem_WithInventory_ReturnsTrue()
		{
			// arrange
			var dataStore = new DataStore(new DummyDataProvider());
			var service = new InventoryService(dataStore);
			// ensure the item has some inventory
			var firstItem = dataStore.GetItems().First();
			firstItem.InventoryCount = 1;
			dataStore.SaveItem(firstItem);

			// act
			var purchaseResult = service.BuyItem(firstItem.Id);

			// assert
			purchaseResult.Success.Should().BeTrue();
		}


		[Test]
		public void BuyItem_WithInventory_ReducesInventory()
		{
			// arrange
			var dataStore = new DataStore(new DummyDataProvider());
			var service = new InventoryService(dataStore);
			// ensure the item has some inventory
			var firstItem = dataStore.GetItems().First();
			firstItem.InventoryCount = 2;
			dataStore.SaveItem(firstItem);

			// act
			service.BuyItem(firstItem.Id);

			// assert
			var loadedItem = dataStore.GetItem(firstItem.Id);
			loadedItem.InventoryCount.Should().Be(1);
		}

		[Test]
		public void BuyItem_WithoutInventory_ReturnsFalse()
		{
			// arrange
			var dataStore = new DataStore(new DummyDataProvider());
			var service = new InventoryService(dataStore);
			// ensure the item has no inventory left
			var firstItem = dataStore.GetItems().First();
			firstItem.InventoryCount = 0;
			dataStore.SaveItem(firstItem);

			// act
			var purchaseResult = service.BuyItem(firstItem.Id);

			// assert
			purchaseResult.Success.Should().BeFalse();
		}

		[Test]
		public void BuyItem_WithoutInventory_DoesNotReduceFurther()
		{
			// arrange
			var dataStore = new DataStore(new DummyDataProvider());
			var service = new InventoryService(dataStore);
			// ensure the item has no inventory left
			var firstItem = dataStore.GetItems().First();
			firstItem.InventoryCount = 0;
			dataStore.SaveItem(firstItem);

			// act
			service.BuyItem(firstItem.Id);

			// assert
			var loadedItem = dataStore.GetItem(firstItem.Id);
			loadedItem.InventoryCount.Should().Be(0);
		}

		[Test]
		public void BuyItem_InvalidId_ReturnsFalse()
		{
			// arrange
			var dataStore = new DataStore(new DummyDataProvider());
			var service = new InventoryService(dataStore);

			// act
			var purchaseResult = service.BuyItem("some invalid id");

			// assert
			purchaseResult.Success.Should().BeFalse();
		}
	}
}
