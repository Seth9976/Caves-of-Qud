using System;
using XRL.World.Skills.Cooking;

namespace XRL.World.Parts
{
	// Token: 0x02000F0C RID: 3852
	[Serializable]
	public class TeachesDish : IPart
	{
		// Token: 0x060099A0 RID: 39328 RVA: 0x0037CB04 File Offset: 0x0037AD04
		public TeachesDish()
		{
		}

		// Token: 0x060099A1 RID: 39329 RVA: 0x0037CB0C File Offset: 0x0037AD0C
		public TeachesDish(CookingRecipe recipe, string text)
		{
			this.Recipe = recipe;
			this.Text = text;
		}

		// Token: 0x04004003 RID: 16387
		public string Text;

		// Token: 0x04004004 RID: 16388
		public CookingRecipe Recipe;
	}
}
