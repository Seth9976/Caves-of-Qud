using System;
using XRL.Wish;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace XRL.World.Biomes
{
	// Token: 0x02000A28 RID: 2600
	[HasWishCommand]
	public static class SlimespitterTemplate
	{
		// Token: 0x06006D74 RID: 28020 RVA: 0x0024BAA0 File Offset: 0x00249CA0
		public static void Apply(GameObject GO)
		{
			if (!GO.IsCombatObject())
			{
				return;
			}
			GO.Slimewalking = true;
			GO.RequirePart<DisplayNameAdjectives>(false).AddAdjective("slime-spitting");
			GO.RequirePart<SlimespitterIconColor>(false);
			if (!GO.HasPart<SlimeGlands>())
			{
				GO.RequirePart<Mutations>(false).AddMutation(new LiquidSpitter("slime"), 1, true);
			}
		}

		// Token: 0x06006D75 RID: 28021 RVA: 0x0024BAF8 File Offset: 0x00249CF8
		[WishCommand("slimespitter", null)]
		public static void Wish(string Blueprint)
		{
			WishResult wishResult = WishSearcher.SearchForBlueprint(Blueprint);
			GameObject gameObject = GameObjectFactory.Factory.CreateObject(wishResult.Result, 0, 0, null, null, null, "Wish", null);
			SlimespitterTemplate.Apply(gameObject);
			The.PlayerCell.getClosestEmptyCell().AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
		}
	}
}
