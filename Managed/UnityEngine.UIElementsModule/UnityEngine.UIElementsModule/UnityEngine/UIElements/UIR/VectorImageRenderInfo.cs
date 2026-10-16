using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000680 RID: 1664
	internal class VectorImageRenderInfo : LinkedPoolItem<VectorImageRenderInfo>
	{
		// Token: 0x06003074 RID: 12404 RVA: 0x000B79C4 File Offset: 0x000B5BC4
		public void Reset()
		{
			this.useCount = 0;
			this.firstGradientRemap = null;
			this.gradientSettingsAlloc = default(Alloc);
		}

		// Token: 0x040017CC RID: 6092
		public int useCount;

		// Token: 0x040017CD RID: 6093
		public GradientRemap firstGradientRemap;

		// Token: 0x040017CE RID: 6094
		public Alloc gradientSettingsAlloc;
	}
}
