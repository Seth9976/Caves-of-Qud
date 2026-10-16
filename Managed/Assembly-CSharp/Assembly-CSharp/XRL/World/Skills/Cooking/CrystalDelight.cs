using System;
using System.Collections.Generic;
using XRL.World.Effects;

namespace XRL.World.Skills.Cooking
{
	// Token: 0x02000BB4 RID: 2996
	[Serializable]
	public class CrystalDelight : CookingRecipe
	{
		// Token: 0x06007724 RID: 30500 RVA: 0x002CE2B0 File Offset: 0x002CC4B0
		public CrystalDelight()
		{
			this.Components.Add(new PreparedCookingRecipeUnusualComponentBlueprint("Crystal of Eve", null, 1));
			this.Components.Add(new PreparedCookingRecipieComponentLiquid("warmstatic", 1));
			this.Components.Add(new PreparedCookingRecipeUnusualComponentBlueprint("GlitterGrenade3", null, 1));
			this.Components.Add(new PreparedCookingRecipeUnusualComponentBlueprint("Gentling Collar", null, 1));
			this.Effects.Add(new CookingRecipeResultProceduralEffect(ProceduralCookingEffect.CreateSpecific(new List<string> { "CookingDomainSpecial_UnitCrystalTransform" }, null, null)));
		}

		// Token: 0x06007725 RID: 30501 RVA: 0x002CE345 File Offset: 0x002CC545
		public override string GetDescription()
		{
			return "?????";
		}

		// Token: 0x06007726 RID: 30502 RVA: 0x002CE34C File Offset: 0x002CC54C
		public override string GetApplyMessage()
		{
			return "";
		}

		// Token: 0x06007727 RID: 30503 RVA: 0x002CE353 File Offset: 0x002CC553
		public override string GetDisplayName()
		{
			return "{{W|Crystal Delight}}";
		}

		// Token: 0x06007728 RID: 30504 RVA: 0x002CE35C File Offset: 0x002CC55C
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
