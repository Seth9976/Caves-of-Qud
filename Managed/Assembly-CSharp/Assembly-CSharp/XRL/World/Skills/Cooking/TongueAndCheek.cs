using System;
using System.Collections.Generic;
using XRL.World.Effects;

namespace XRL.World.Skills.Cooking
{
	// Token: 0x02000BB2 RID: 2994
	[Serializable]
	public class TongueAndCheek : CookingRecipe
	{
		// Token: 0x0600771B RID: 30491 RVA: 0x002CE0F0 File Offset: 0x002CC2F0
		public TongueAndCheek()
		{
			this.Components.Add(new PreparedCookingRecipieComponentBlueprint("Fermented Tongue", null, 1));
			this.Components.Add(new PreparedCookingRecipieComponentBlueprint("Sliced Bop Cheek", null, 1));
			this.Components.Add(new PreparedCookingRecipieComponentBlueprint("Congealed Love", null, 1));
			this.Effects.Add(new CookingRecipeResultProceduralEffect(ProceduralCookingEffect.CreateSpecific(new List<string> { "CookingDomainTongue_UnitStickyTongue" }, new List<string> { "CookingDomainRubber_OnJump" }, new List<string> { "CookingDomainLove_BeguilingCharge_ProceduralCookingTriggeredAction" })));
		}

		// Token: 0x0600771C RID: 30492 RVA: 0x002CE18D File Offset: 0x002CC38D
		public override string GetDescription()
		{
			return "Can use Sticky Tongue at rank 4-5.\nWhenever you jump, you beguile a creature as per Beguiling at rank 7-8 for the duration of this effect.";
		}

		// Token: 0x0600771D RID: 30493 RVA: 0x002CE194 File Offset: 0x002CC394
		public override string GetApplyMessage()
		{
			return "";
		}

		// Token: 0x0600771E RID: 30494 RVA: 0x002CE19B File Offset: 0x002CC39B
		public override string GetDisplayName()
		{
			return "{{W|Tongue and Cheek}}";
		}
	}
}
