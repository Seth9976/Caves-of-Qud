using System;
using UnityEngine;

namespace AiUnity.Common.Attributes
{
	// Token: 0x020002E7 RID: 743
	public class DividerAttribute : PropertyAttribute
	{
		// Token: 0x0600197A RID: 6522 RVA: 0x00081615 File Offset: 0x0007F815
		public DividerAttribute()
		{
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x0008163E File Offset: 0x0007F83E
		public DividerAttribute(string col)
		{
			this.col = col;
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x0008166E File Offset: 0x0007F86E
		public DividerAttribute(float widthPct)
		{
			this.widthPct = widthPct;
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x0008169E File Offset: 0x0007F89E
		public DividerAttribute(string col, float thickness)
		{
			this.col = col;
			this.thickness = thickness;
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x000816D5 File Offset: 0x0007F8D5
		public DividerAttribute(float widthPct, float thickness)
		{
			this.widthPct = widthPct;
			this.thickness = thickness;
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x0008170C File Offset: 0x0007F90C
		public DividerAttribute(string col, float thickness, float widthPct)
		{
			this.col = col;
			this.thickness = thickness;
			this.widthPct = widthPct;
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x0008174A File Offset: 0x0007F94A
		public DividerAttribute(float widthPct, float thickness, float space)
		{
			this.widthPct = widthPct;
			this.thickness = thickness;
			this.space = space;
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x00081788 File Offset: 0x0007F988
		public DividerAttribute(string col, float thickness, float widthPct, float space)
		{
			this.col = col;
			this.thickness = thickness;
			this.widthPct = widthPct;
			this.space = space;
		}

		// Token: 0x04000EE8 RID: 3816
		public readonly string col = "grey";

		// Token: 0x04000EE9 RID: 3817
		public readonly float space;

		// Token: 0x04000EEA RID: 3818
		public readonly float thickness = 1f;

		// Token: 0x04000EEB RID: 3819
		public float widthPct = 1f;
	}
}
