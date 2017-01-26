using System.Linq;

using GildedRose.WebServices.DataAccess;
using NUnit.Framework;
using System;

using FluentAssertions;

using GildedRose.WebServices.Models;
namespace GildedRose.WebServices.Test
{
	using Newtonsoft.Json.Linq;

	[TestFixture]
	public class DataStoreTests
	{
		[Test]
		public void Constructor_NoProvider_ThrowsArgumentNullException()
		{
			// arrange
			// ReSharper disable once NotAccessedVariable
			IDataStore dataStore;

			// act
			Action action = () => dataStore = new DataStore(null);

			// assert
			action.ShouldThrow<ArgumentNullException>();
		}

		[Test]
		public void GetItems_AfterInitialization_ReturnsOriginalCount()
		{
			// arrange
			var dataStore = new DataStore(new DummyDataProvider());

			// act
			var items = dataStore.GetItems();

			// assert
			items.Count.Should().Be(2);
		}

		[Test]
		public void GetItems_AfterSavingExistingItem_ReturnsOriginalCount()
		{
			// arrange
			var dataStore = new DataStore(new DummyDataProvider());
			var items = dataStore.GetItems();
			var existingItem = items.First();
			dataStore.SaveItem(existingItem);

			// act
			items = dataStore.GetItems();

			// assert
			items.Count.Should().Be(2);
		}

		[Test]
		public void GetItems_AfterSavingNewItem_ReturnsUpdatedCount()
		{
			// arrange
			var dataStore = new DataStore(new DummyDataProvider());
			var newItem = new Item { Id = "some new id", Name = "some new name" };
			dataStore.SaveItem(newItem);

			// act
			var items = dataStore.GetItems();

			// assert
			items.Count.Should().Be(3);
		}

		[Test]
		public void GetItems_AfterSavingNewItemWithNullId_ReturnsUpdatedCount()
		{
			// arrange
			var dataStore = new DataStore(new DummyDataProvider());
			var newItem = new Item { Id = null, Name = "some new name" };
			dataStore.SaveItem(newItem);

			// act
			var items = dataStore.GetItems();

			// assert
			items.Count.Should().Be(3);
		}

		[Test]
		public void SaveItem_NullId_CreatesNewId()
		{
			// arrange
			var dataStore = new DataStore(new DummyDataProvider());

			// act
			var newItem = new Item { Id = null, Name = "some new name" };
			dataStore.SaveItem(newItem);

			// assert
			newItem.Id.Length.Should().Be(Guid.NewGuid().ToString().Length);
		}

		[Test]
		public void SaveItem_ValidId_KeepsExistingId()
		{
			// arrange
			var dataStore = new DataStore(new DummyDataProvider());

			// act
			var newItem = new Item { Id = "some new id", Name = "some new name" };
			dataStore.SaveItem(newItem);

			// assert
			newItem.Id.Should().Be("some new id");
		}

		[Test]
		public void SaveItem_NewName_SavesNewName()
		{
			// arrange
			var dataStore = new DataStore(new DummyDataProvider());
			var existingItem = dataStore.GetItems().First();

			// act
			existingItem.Name = "some changed name";
			dataStore.SaveItem(existingItem);

			// assert
			var loadedItem = dataStore.GetItem(existingItem.Id);
			loadedItem.Name.Should().Be("some changed name");
		}

		[Test]
		public void GetItem_ByFirstItemId_ReturnsFirstItem()
		{
			// arrange
			var dataStore = new DataStore(new DummyDataProvider());
			var firstItem = dataStore.GetItems().First();

			// act
			var loadedItem = dataStore.GetItem(firstItem.Id);

			// assert
			loadedItem.Should().Be(firstItem);
		}

		[Test]
		public void GetItem_ByLastItemId_ReturnsLastItem()
		{
			// arrange
			var dataStore = new DataStore(new DummyDataProvider());
			var lastItem = dataStore.GetItems().Last();

			// act
			var loadedItem = dataStore.GetItem(lastItem.Id);

			// assert
			loadedItem.Should().Be(lastItem);
		}
	}
}
