using System;
using XRL.Wish;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace XRL.World.Biomes
{
	// Token: 0x02000A2A RID: 2602
	[HasWishCommand]
	public static class KindlethumbedTemplate
	{
		// Token: 0x06006D7B RID: 28027 RVA: 0x0024BF44 File Offset: 0x0024A144
		public static void Apply(GameObject GO)
		{
			GO.Slimewalking = true;
			if (GO.HasBodyPart("Hand"))
			{
				GO.RequirePart<DisplayNameAdjectives>(false).AddAdjective("kindlethumbed");
				GO.RequirePart<DisplayNameColor>(false).SetColorByPriority("r", 10);
				GO.RequirePart<KindlethumbedIconColor>(false);
				if (!GO.HasPart(typeof(FlamingRay)))
				{
					GO.RequirePart<Mutations>(false).AddMutation(new FlamingRay(), 1, true);
				}
			}
		}

		// Token: 0x06006D7C RID: 28028 RVA: 0x0024BFB8 File Offset: 0x0024A1B8
		[WishCommand("kindlethumbed", null)]
		public static void Wish(string Blueprint)
		{
			WishResult wishResult = WishSearcher.SearchForBlueprint(Blueprint);
			GameObject gameObject = GameObjectFactory.Factory.CreateObject(wishResult.Result, 0, 0, null, null, null, "Wish", null);
			KindlethumbedTemplate.Apply(gameObject);
			The.PlayerCell.getClosestEmptyCell().AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
		}
	}
}
