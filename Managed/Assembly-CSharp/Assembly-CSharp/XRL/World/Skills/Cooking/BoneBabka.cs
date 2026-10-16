using System;
using System.Collections.Generic;
using XRL.World.Effects;

namespace XRL.World.Skills.Cooking
{
	// Token: 0x02000BB1 RID: 2993
	[Serializable]
	public class BoneBabka : CookingRecipe
	{
		// Token: 0x06007717 RID: 30487 RVA: 0x002CE03C File Offset: 0x002CC23C
		public BoneBabka()
		{
			this.Components.Add(new PreparedCookingRecipieComponentBlueprint("Compacted Bone Meal", null, 1));
			this.Components.Add(new PreparedCookingRecipieComponentBlueprint("Sun-Dried Banana", null, 1));
			this.Components.Add(new PreparedCookingRecipieComponentBlueprint("Congealed Skulk", null, 1));
			this.Effects.Add(new CookingRecipeResultProceduralEffect(ProceduralCookingEffect.CreateSpecific(new List<string> { "CookingDomainBurrowing_UnitBurrowingClaws" }, new List<string> { "CookingDomainArtifact_OnIdentify" }, new List<string> { "CookingDomainArmor_LargeAVBuff_ProceduralCookingTriggeredAction" })));
		}

		// Token: 0x06007718 RID: 30488 RVA: 0x002CE0D9 File Offset: 0x002CC2D9
		public override string GetDescription()
		{
			return "Can use Burrowing Claws at level 5-6. If you already have Burrowing Claws, it's enhanced by 3-4 levels.\nWhenever you identify an artifact, you gain +6 AV for 50 turns.";
		}

		// Token: 0x06007719 RID: 30489 RVA: 0x002CE0E0 File Offset: 0x002CC2E0
		public override string GetApplyMessage()
		{
			return "";
		}

		// Token: 0x0600771A RID: 30490 RVA: 0x002CE0E7 File Offset: 0x002CC2E7
		public override string GetDisplayName()
		{
			return "{{W|Bone Babka}}";
		}
	}
}
