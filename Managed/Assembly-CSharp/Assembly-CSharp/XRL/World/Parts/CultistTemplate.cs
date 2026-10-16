using System;

namespace XRL.World.Parts
{
	// Token: 0x02000E63 RID: 3683
	public static class CultistTemplate
	{
		// Token: 0x060094F1 RID: 38129 RVA: 0x0035DD8D File Offset: 0x0035BF8D
		public static void Apply(GameObject GO, string CultFaction)
		{
			GO.Brain.Allegiance.Clear();
			GO.Brain.Allegiance.Add(CultFaction, 100);
		}
	}
}
