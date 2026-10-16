using System;
using System.Collections.Generic;
using XRL.World.Effects;

namespace XRL.World.Skills.Cooking
{
	// Token: 0x02000BAC RID: 2988
	[Serializable]
	public class HotandSpiny : CookingRecipe
	{
		// Token: 0x06007703 RID: 30467 RVA: 0x002CDD14 File Offset: 0x002CBF14
		public HotandSpiny()
		{
			this.Components.Add(new PreparedCookingRecipieComponentBlueprint("Cured Dawnglider Tail", null, 1));
			this.Components.Add(new PreparedCookingRecipieComponentBlueprint("Spine Fruit Jam", null, 1));
			this.Effects.Add(new CookingRecipeResultProceduralEffect(ProceduralCookingEffect.CreateSpecific(new List<string> { "CookingDomainHeat_UnitResist", "CookingDomainReflect_UnitReflectDamage" }, null, null)));
		}

		// Token: 0x06007704 RID: 30468 RVA: 0x002CDD87 File Offset: 0x002CBF87
		public override string GetDescription()
		{
			return "+10-15 Heat Resist\nReflect 3-4% damage back at your attackers, rounded up.";
		}

		// Token: 0x06007705 RID: 30469 RVA: 0x002CDD8E File Offset: 0x002CBF8E
		public override string GetApplyMessage()
		{
			return "";
		}

		// Token: 0x06007706 RID: 30470 RVA: 0x002CDD95 File Offset: 0x002CBF95
		public override string GetDisplayName()
		{
			return "{{W|Hot and Spiny}}";
		}
	}
}
