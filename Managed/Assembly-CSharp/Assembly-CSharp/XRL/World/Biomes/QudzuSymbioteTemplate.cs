using System;
using XRL.Wish;
using XRL.World.Parts;

namespace XRL.World.Biomes
{
	// Token: 0x02000A24 RID: 2596
	[HasWishCommand]
	public static class QudzuSymbioteTemplate
	{
		// Token: 0x06006D69 RID: 28009 RVA: 0x0024B140 File Offset: 0x00249340
		public static void Apply(GameObject GO, Zone Z)
		{
			if (((GO != null) ? GO.Render : null) != null && GO.HasPart<Body>() && !GO.GetBlueprint(true).DescendsFrom("BaseRobot"))
			{
				GO.AddPart<RustOnHit>(new RustOnHit(), true, false);
				GO.RequirePart<SocialRoles>(false).RequireRole("{{r|qudzu}} symbiote");
				GO.RequirePart<QudzuSymbioteIconColor>(false);
				if (GO.Brain != null && !Z.GetZoneProperty("relaxedbiomes", "").EqualsNoCase("true"))
				{
					GO.Brain.Allegiance["Vines"] = 100;
				}
			}
		}

		// Token: 0x06006D6A RID: 28010 RVA: 0x0024B1D8 File Offset: 0x002493D8
		[WishCommand("qudzusymbiote", null)]
		public static void Wish(string Blueprint)
		{
			WishResult wishResult = WishSearcher.SearchForBlueprint(Blueprint);
			GameObject gameObject = GameObjectFactory.Factory.CreateObject(wishResult.Result, 0, 0, null, null, null, "Wish", null);
			QudzuSymbioteTemplate.Apply(gameObject, The.ActiveZone);
			The.PlayerCell.getClosestEmptyCell().AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
		}
	}
}
