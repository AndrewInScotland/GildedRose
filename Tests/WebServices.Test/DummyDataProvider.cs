using System.Collections.Generic;
using GildedRose.WebServices.DataAccess;

namespace GildedRose.WebServices.Test
{
	public class DummyDataProvider : IDataProvider
	{
		public IList<ItemEntity> ItitializeData()
		{
			return new List<ItemEntity>
						{
							new ItemEntity
								{
									Id = "some boat Id",
									Name = "some boat name",
									Description = "some boat description"
								},
							new ItemEntity
								{
									Id = "some other boat Id",
									Name = "some other boat name",
									Description = "some other boat description"
								}
						};
		}
	}
}