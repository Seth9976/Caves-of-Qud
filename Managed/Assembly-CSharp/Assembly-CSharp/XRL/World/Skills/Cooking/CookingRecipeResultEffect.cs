using System;

namespace XRL.World.Skills.Cooking
{
	// Token: 0x02000BBB RID: 3003
	[Serializable]
	public class CookingRecipeResultEffect : ICookingRecipeResult, IComposite
	{
		// Token: 0x0600775B RID: 30555 RVA: 0x002CEF94 File Offset: 0x002CD194
		private CookingRecipeResultEffect()
		{
		}

		// Token: 0x0600775C RID: 30556 RVA: 0x002CEF9C File Offset: 0x002CD19C
		public CookingRecipeResultEffect(string effect)
		{
			this.effect = effect;
		}

		// Token: 0x0600775D RID: 30557 RVA: 0x002CEFAB File Offset: 0x002CD1AB
		public string GetCampfireDescription()
		{
			return (Activator.CreateInstance(ModManager.ResolveType("XRL.World.Effects." + this.effect, false, false, true)) as Effect).GetDescription();
		}

		// Token: 0x0600775E RID: 30558 RVA: 0x002CEFD4 File Offset: 0x002CD1D4
		public string apply(GameObject eater)
		{
			Effect effect = Activator.CreateInstance(ModManager.ResolveType("XRL.World.Effects." + this.effect, false, false, true)) as Effect;
			eater.ApplyEffect(effect, null);
			return effect.GetDescription();
		}

		// Token: 0x040036D4 RID: 14036
		public string effect;
	}
}
