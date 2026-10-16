using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200067E RID: 1662
	internal class VectorImageRenderInfoPool : LinkedPool<VectorImageRenderInfo>
	{
		// Token: 0x0600306F RID: 12399 RVA: 0x000B7950 File Offset: 0x000B5B50
		public VectorImageRenderInfoPool()
			: base(() => new VectorImageRenderInfo(), delegate(VectorImageRenderInfo vectorImageInfo)
			{
				vectorImageInfo.Reset();
			}, 10000)
		{
		}
	}
}
