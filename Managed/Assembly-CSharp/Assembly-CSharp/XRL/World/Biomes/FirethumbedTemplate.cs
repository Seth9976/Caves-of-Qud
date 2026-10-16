using System;
using XRL.Wish;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace XRL.World.Biomes
{
	// Token: 0x02000A2B RID: 2603
	[HasWishCommand]
	public static class FirethumbedTemplate
	{
		// Token: 0x06006D7D RID: 28029 RVA: 0x0024C010 File Offset: 0x0024A210
		public static void Apply(GameObject GO)
		{
			GO.Slimewalking = true;
			if (GO.HasBodyPart("Hand"))
			{
				GO.RequirePart<DisplayNameAdjectives>(false).AddAdjective("firethumbed");
				GO.RequirePart<DisplayNameColor>(false).SetColorByPriority("R", 10);
				GO.RequirePart<FirethumbedIconColor>(false);
				if (!GO.HasPart(typeof(FlamingRay)))
				{
					GO.RequirePart<Mutations>(false).AddMutation(new FlamingRay(), 6, true);
				}
			}
		}

		// Token: 0x06006D7E RID: 28030 RVA: 0x0024C084 File Offset: 0x0024A284
		[WishCommand("firethumbed", null)]
		public static void Wish(string Blueprint)
		{
			WishResult wishResult = WishSearcher.SearchForBlueprint(Blueprint);
			GameObject gameObject = GameObjectFactory.Factory.CreateObject(wishResult.Result, 0, 0, null, null, null, "Wish", null);
			FirethumbedTemplate.Apply(gameObject);
			The.PlayerCell.getClosestEmptyCell().AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
		}
	}
}
