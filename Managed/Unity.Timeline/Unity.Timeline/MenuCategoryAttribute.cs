using System;

namespace UnityEngine.Timeline
{
	// Token: 0x0200004B RID: 75
	[AttributeUsage(AttributeTargets.Class)]
	internal class MenuCategoryAttribute : Attribute
	{
		// Token: 0x060002D7 RID: 727 RVA: 0x00009EA3 File Offset: 0x000080A3
		public MenuCategoryAttribute(string category)
		{
			this.category = category ?? string.Empty;
		}

		// Token: 0x040000FC RID: 252
		public readonly string category;
	}
}
