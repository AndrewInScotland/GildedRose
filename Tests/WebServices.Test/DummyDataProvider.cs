using System.Collections.Generic;
using GildedRose.WebServices.DataAccess;
using GildedRose.WebServices.Models;

namespace GildedRose.WebServices.Test
{
	public class DummyDataProvider : IDataProvider
	{
		public IList<Item> ItitializeData()
		{
			return new List<Item>
						{
							new Item
								{
									Id = "some boat Id",
									Name = "some boat name",
									Description = "some boat description"
								},
							new Item
								{
									Id = "some other boat Id",
									Name = "some other boat name",
									Description = "some other boat description"
								}
						};
		}
	}
}