// ReSharper disable NotAccessedVariable to handle cases where expected exceptions are thrown before the instance can be used
namespace GildedRose.WebServices.Test
{
	using System;

	using FluentAssertions;

	using GildedRose.WebServices.DataAccess;
	using GildedRose.WebServices.Models;

	using NUnit.Framework;

	[TestFixture]
	public class ItemEntityTests
	{
		[Test]
		public void ItemEntity_ItemCtorWithNull_Throws()
		{
			// arrange 
			ItemEntity itemEntity;

			// act
			Action action = () => itemEntity = new ItemEntity(null);

			// assert
			action.ShouldThrow<ArgumentNullException>();
		}

		[Test]
		public void ItemEntity_ItemCtorWithModel_CopiesProperties()
		{
			// arrange 
			var itemModel = new Item
								{
									Id = "some Id",
									Name = "some name",
									Description = "some description",
									Price = 1,
									InventoryCount = 2
								};

			// act
			var itemEntity = new ItemEntity(itemModel);

			// assert
			itemEntity.Id.Should().Be("some Id");
			itemEntity.Name.Should().Be("some name");
			itemEntity.Description.Should().Be("some description");
			itemEntity.Price.Should().Be(1);
			itemEntity.InventoryCount.Should().Be(2);
		}

		[Test]
		public void ItemEntity_ToModel_CopiesProperties()
		{
			// arrange 
			var itemEntity = new ItemEntity
			{
				Id = "some Id",
				Name = "some name",
				Description = "some description",
				Price = 1,
				InventoryCount = 2
			};

			// act
			var itemModel = itemEntity.ToModel();

			// assert
			itemModel.Id.Should().Be("some Id");
			itemModel.Name.Should().Be("some name");
			itemModel.Description.Should().Be("some description");
			itemModel.Price.Should().Be(1);
			itemModel.InventoryCount.Should().Be(2);
		}
	}
}
