using System;
using XRL.Wish;
using XRL.World.Parts;

namespace XRL.World.Biomes
{
	// Token: 0x02000A27 RID: 2599
	[HasWishCommand]
	public static class SlimewalkerTemplate
	{
		// Token: 0x06006D72 RID: 28018 RVA: 0x0024B9E4 File Offset: 0x00249BE4
		public static void Apply(GameObject GO)
		{
			if (!GO.IsCombatObject())
			{
				return;
			}
			GO.Slimewalking = true;
			if (GO.HasBodyPart("Foot") || GO.HasBodyPart("Feet"))
			{
				GO.RequirePart<DisplayNameAdjectives>(false).AddAdjective("web-toed");
			}
			else
			{
				GO.RequirePart<DisplayNameAdjectives>(false).AddAdjective("slimy-finned");
			}
			GO.RequirePart<SlimewalkerIconColor>(false);
		}

		// Token: 0x06006D73 RID: 28019 RVA: 0x0024BA48 File Offset: 0x00249C48
		[WishCommand("slimewalker", null)]
		public static void Wish(string Blueprint)
		{
			WishResult wishResult = WishSearcher.SearchForBlueprint(Blueprint);
			GameObject gameObject = GameObjectFactory.Factory.CreateObject(wishResult.Result, 0, 0, null, null, null, "Wish", null);
			SlimewalkerTemplate.Apply(gameObject);
			The.PlayerCell.getClosestEmptyCell().AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
		}
	}
}
