using System;
using System.Collections.Generic;
using XRL.World.Effects;

namespace XRL.World.Skills.Cooking
{
	// Token: 0x02000BAD RID: 2989
	[Serializable]
	public class MahLahSoup : CookingRecipe
	{
		// Token: 0x06007707 RID: 30471 RVA: 0x002CDD9C File Offset: 0x002CBF9C
		public MahLahSoup()
		{
			this.Components.Add(new PreparedCookingRecipieComponentBlueprint("Dried Lah Petals", null, 1));
			this.Components.Add(new PreparedCookingRecipieComponentBlueprint("Vinewafer Sheaf", null, 1));
			this.Effects.Add(new CookingRecipeResultProceduralEffect(ProceduralCookingEffect.CreateSpecific(null, new List<string> { "OnDrinkWaterProceduralCookingTrigger" }, new List<string> { "CookingDomainFear_FearImmunity_ProceduralCookingTriggeredAction" })));
		}

		// Token: 0x06007708 RID: 30472 RVA: 0x002CDE13 File Offset: 0x002CC013
		public override string GetDescription()
		{
			return "Whenever you drink freshwater, there's a 25% chance you become immune to fear for 6 hours.";
		}

		// Token: 0x06007709 RID: 30473 RVA: 0x002CDE1A File Offset: 0x002CC01A
		public override string GetApplyMessage()
		{
			return "";
		}

		// Token: 0x0600770A RID: 30474 RVA: 0x002CDE21 File Offset: 0x002CC021
		public override string GetDisplayName()
		{
			return "{{W|Mah Lah Soup}}";
		}
	}
}
