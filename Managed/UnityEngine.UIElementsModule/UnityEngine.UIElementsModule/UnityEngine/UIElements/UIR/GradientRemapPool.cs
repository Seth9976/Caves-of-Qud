using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000681 RID: 1665
	internal class GradientRemapPool : LinkedPool<GradientRemap>
	{
		// Token: 0x06003076 RID: 12406 RVA: 0x000B79EC File Offset: 0x000B5BEC
		public GradientRemapPool()
			: base(() => new GradientRemap(), delegate(GradientRemap gradientRemap)
			{
				gradientRemap.Reset();
			}, 10000)
		{
		}
	}
}
