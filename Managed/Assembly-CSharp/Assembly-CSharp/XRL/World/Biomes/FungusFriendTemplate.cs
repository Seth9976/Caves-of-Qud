using System;
using XRL.Wish;
using XRL.World.Effects;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace XRL.World.Biomes
{
	// Token: 0x02000A20 RID: 2592
	[HasWishCommand]
	public static class FungusFriendTemplate
	{
		// Token: 0x06006D60 RID: 28000 RVA: 0x0024AAEC File Offset: 0x00248CEC
		public static void Apply(GameObject GO, string InfectionBlueprint, Zone Z)
		{
			if (((GO != null) ? GO.Render : null) != null && GO.HasPart<Body>())
			{
				GO.RequirePart<SocialRoles>(false).RequireRole("friend to fungi");
				GO.RequirePart<FungiFriendIconColor>(false);
				if (33.in100())
				{
					FungalSporeInfection.ApplyFungalInfection(GO, InfectionBlueprint, null);
				}
				if (15.in100())
				{
					FungalSporeInfection.ApplyFungalInfection(GO, InfectionBlueprint, null);
				}
				if (!Z.GetZoneProperty("relaxedbiomes", "").EqualsNoCase("true") && GO.Brain != null)
				{
					GO.Brain.Allegiance["Fungi"] = 100;
				}
			}
		}

		// Token: 0x06006D61 RID: 28001 RVA: 0x0024AB8C File Offset: 0x00248D8C
		[WishCommand("fungusfriend", null)]
		public static void Wish(string Blueprint)
		{
			WishResult wishResult = WishSearcher.SearchForBlueprint(Blueprint);
			GameObject gameObject = GameObjectFactory.Factory.CreateObject(wishResult.Result, 0, 0, null, null, null, "Wish", null);
			FungusFriendTemplate.Apply(gameObject, SporePuffer.InfectionObjectList.GetRandomElement<string>(null), The.ActiveZone);
			The.PlayerCell.getClosestEmptyCell().AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
		}
	}
}
