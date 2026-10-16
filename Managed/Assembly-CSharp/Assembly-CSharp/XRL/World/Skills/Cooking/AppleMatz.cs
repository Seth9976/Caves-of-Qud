using System;
using System.Collections.Generic;
using XRL.World.Effects;

namespace XRL.World.Skills.Cooking
{
	// Token: 0x02000BAB RID: 2987
	[Serializable]
	public class AppleMatz : CookingRecipe
	{
		// Token: 0x060076FF RID: 30463 RVA: 0x002CDC8C File Offset: 0x002CBE8C
		public AppleMatz()
		{
			this.Components.Add(new PreparedCookingRecipieComponentBlueprint("Vinewafer Sheaf", null, 1));
			this.Components.Add(new PreparedCookingRecipieComponentBlueprint("Starapple Preserves", null, 1));
			this.Effects.Add(new CookingRecipeResultProceduralEffect(ProceduralCookingEffect.CreateSpecific(new List<string> { "CookingDomainRegenLowtier_RegenerationUnit", "ProceduralCookingEffectUnit_LessThirst" }, null, null)));
		}

		// Token: 0x06007700 RID: 30464 RVA: 0x002CDCFF File Offset: 0x002CBEFF
		public override string GetDescription()
		{
			return "+10-15% to natural healing rate\nYou thirst at half rate.";
		}

		// Token: 0x06007701 RID: 30465 RVA: 0x002CDD06 File Offset: 0x002CBF06
		public override string GetApplyMessage()
		{
			return "";
		}

		// Token: 0x06007702 RID: 30466 RVA: 0x002CDD0D File Offset: 0x002CBF0D
		public override string GetDisplayName()
		{
			return "{{W|Apple Matz}}";
		}
	}
}
