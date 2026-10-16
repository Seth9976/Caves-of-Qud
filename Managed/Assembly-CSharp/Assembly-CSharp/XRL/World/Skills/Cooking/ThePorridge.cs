using System;
using System.Collections.Generic;
using XRL.World.Effects;

namespace XRL.World.Skills.Cooking
{
	// Token: 0x02000BAE RID: 2990
	[Serializable]
	public class ThePorridge : CookingRecipe
	{
		// Token: 0x0600770B RID: 30475 RVA: 0x002CDE28 File Offset: 0x002CC028
		public ThePorridge()
		{
			this.Components.Add(new PreparedCookingRecipieComponentBlueprint("Fermented Yuckwheat Stem", null, 1));
			this.Components.Add(new PreparedCookingRecipieComponentBlueprint("Spark Tick Plasma", null, 1));
			this.Components.Add(new PreparedCookingRecipieComponentLiquid("honey", 1));
			this.Effects.Add(new CookingRecipeResultProceduralEffect(ProceduralCookingEffect.CreateSpecific(new List<string> { "CookingDomainMedicinal_DiseaseResistUnit" }, new List<string> { "CookingDomainMedicinal_OnEatYuckwheat" }, new List<string> { "CookingDomainElectric_Discharge_ProceduralCookingTriggeredAction" })));
		}

		// Token: 0x0600770C RID: 30476 RVA: 0x002CDEC4 File Offset: 0x002CC0C4
		public override string GetDescription()
		{
			return "+3 to saves vs. disease\nWhenever you eat an unfermented yuckwheat stem, you release an electrical discharge per Electrical Generation at level 5-6.";
		}

		// Token: 0x0600770D RID: 30477 RVA: 0x002CDECB File Offset: 0x002CC0CB
		public override string GetApplyMessage()
		{
			return "";
		}

		// Token: 0x0600770E RID: 30478 RVA: 0x002CDED2 File Offset: 0x002CC0D2
		public override string GetDisplayName()
		{
			return "{{W|The Porridge}}";
		}
	}
}
