using System;
using System.Collections.Generic;
using XRL.World.Effects;

namespace XRL.World.Skills.Cooking
{
	// Token: 0x02000BAF RID: 2991
	[Serializable]
	public class MushroomCider : CookingRecipe
	{
		// Token: 0x0600770F RID: 30479 RVA: 0x002CDEDC File Offset: 0x002CC0DC
		public MushroomCider()
		{
			this.Components.Add(new PreparedCookingRecipieComponentBlueprint("Pickled Mushrooms", null, 1));
			this.Components.Add(new PreparedCookingRecipieComponentBlueprint("Starapple Preserves", null, 1));
			this.Components.Add(new PreparedCookingRecipieComponentLiquid("cider", 1));
			this.Effects.Add(new CookingRecipeResultProceduralEffect(ProceduralCookingEffect.CreateSpecific(new List<string> { "CookingDomainQuickness_UnitQuickness", "CookingDomainRegenLowtier_BleedResistUnit", "CookingDomainFungus_FungusResistUnit" }, null, null)));
		}

		// Token: 0x06007710 RID: 30480 RVA: 0x002CDF70 File Offset: 0x002CC170
		public override string GetDescription()
		{
			return "+4-5 Quickness\n+8-12 to saves vs. bleeding\n75% chance that itchy skin doesn't develop into a fungal infection.";
		}

		// Token: 0x06007711 RID: 30481 RVA: 0x002CDF77 File Offset: 0x002CC177
		public override string GetApplyMessage()
		{
			return "";
		}

		// Token: 0x06007712 RID: 30482 RVA: 0x002CDF7E File Offset: 0x002CC17E
		public override string GetDisplayName()
		{
			return "{{W|Mulled Mushroom Cider}}";
		}
	}
}
