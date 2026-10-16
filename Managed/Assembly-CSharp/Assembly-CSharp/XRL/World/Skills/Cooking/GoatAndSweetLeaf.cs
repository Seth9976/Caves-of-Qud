using System;
using System.Collections.Generic;
using XRL.World.Effects;

namespace XRL.World.Skills.Cooking
{
	// Token: 0x02000BB0 RID: 2992
	[Serializable]
	public class GoatAndSweetLeaf : CookingRecipe
	{
		// Token: 0x06007713 RID: 30483 RVA: 0x002CDF88 File Offset: 0x002CC188
		public GoatAndSweetLeaf()
		{
			this.Components.Add(new PreparedCookingRecipieComponentBlueprint("Goat Jerky", null, 1));
			this.Components.Add(new PreparedCookingRecipieComponentBlueprint("Sun-Dried Banana", null, 1));
			this.Components.Add(new PreparedCookingRecipieComponentBlueprint("Fermented Yondercane", null, 1));
			this.Effects.Add(new CookingRecipeResultProceduralEffect(ProceduralCookingEffect.CreateSpecific(new List<string> { "CookingDomainArtifact_UnitPsychometry" }, new List<string> { "CookingDomainHP_OnDamaged" }, new List<string> { "CookingDomainTeleport_MassTeleportOther_ProceduralCookingTriggeredAction" })));
		}

		// Token: 0x06007714 RID: 30484 RVA: 0x002CE025 File Offset: 0x002CC225
		public override string GetDescription()
		{
			return "Can use Psychometry at level 4-5.\nWhenever you take damage, there's a 12-15% chance you teleport all creatures surrounding you.";
		}

		// Token: 0x06007715 RID: 30485 RVA: 0x002CE02C File Offset: 0x002CC22C
		public override string GetApplyMessage()
		{
			return "";
		}

		// Token: 0x06007716 RID: 30486 RVA: 0x002CE033 File Offset: 0x002CC233
		public override string GetDisplayName()
		{
			return "{{W|Goat in Sweet Leaf}}";
		}
	}
}
