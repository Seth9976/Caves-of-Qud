using System;
using System.Collections.Generic;
using XRL.World.Effects;

namespace XRL.World.Skills.Cooking
{
	// Token: 0x02000BB3 RID: 2995
	[Serializable]
	public class CloacaSurprise : CookingRecipe
	{
		// Token: 0x0600771F RID: 30495 RVA: 0x002CE1A4 File Offset: 0x002CC3A4
		public CloacaSurprise()
		{
			this.Components.Add(new PreparedCookingRecipieComponentLiquid("goo", 1));
			this.Components.Add(new PreparedCookingRecipieComponentLiquid("sludge", 1));
			this.Components.Add(new PreparedCookingRecipieComponentLiquid("ooze", 1));
			this.Effects.Add(new CookingRecipeResultProceduralEffect(ProceduralCookingEffect.CreateSpecific(new List<string> { "CookingDomainSpecial_UnitSlogTransform" }, null, null)));
		}

		// Token: 0x06007720 RID: 30496 RVA: 0x002CE220 File Offset: 0x002CC420
		public override string GetDescription()
		{
			return "?????";
		}

		// Token: 0x06007721 RID: 30497 RVA: 0x002CE227 File Offset: 0x002CC427
		public override string GetApplyMessage()
		{
			return "";
		}

		// Token: 0x06007722 RID: 30498 RVA: 0x002CE22E File Offset: 0x002CC42E
		public override string GetDisplayName()
		{
			return "{{W|Cloaca Surprise}}";
		}

		// Token: 0x06007723 RID: 30499 RVA: 0x002CE238 File Offset: 0x002CC438
		public override bool ApplyEffectsTo(GameObject target, bool showMessage = true)
		{
			string text = "";
			if (showMessage)
			{
				text = this.GetApplyMessage();
			}
			foreach (ICookingRecipeResult cookingRecipeResult in this.Effects)
			{
				text += cookingRecipeResult.apply(target);
				text += "\n";
			}
			return true;
		}
	}
}
