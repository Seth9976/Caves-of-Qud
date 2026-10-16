using System;
using XRL.Rules;
using XRL.World.Effects;
using XRL.World.Parts;

namespace XRL.World.Biomes
{
	// Token: 0x02000A21 RID: 2593
	public static class FungusColonizedTemplate
	{
		// Token: 0x06006D62 RID: 28002 RVA: 0x0024ABF4 File Offset: 0x00248DF4
		public static void Apply(GameObject GO, string InfectionBlueprint, Zone Z)
		{
			if (((GO != null) ? GO.Render : null) != null && GO.HasPart<Body>())
			{
				GO.RequirePart<DisplayNameAdjectives>(false).RequireAdjective("fungus-ridden");
				GO.RequirePart<FungusRiddenIconColor>(false);
				FungalSporeInfection.ApplyFungalInfection(GO, InfectionBlueprint, null);
				FungalSporeInfection.ApplyFungalInfection(GO, InfectionBlueprint, null);
				for (int i = Stat.Random(1, 4); i > 0; i--)
				{
					FungalSporeInfection.ApplyFungalInfection(GO, InfectionBlueprint, null);
				}
				if (!Z.GetZoneProperty("relaxedbiomes", "").EqualsNoCase("true"))
				{
					if (!GO.Brain.Allegiance.ContainsKey("Fungi"))
					{
						GO.Brain.Allegiance.Add("Fungi", 100);
						return;
					}
					GO.Brain.Allegiance["Fungi"] = 100;
				}
			}
		}
	}
}
