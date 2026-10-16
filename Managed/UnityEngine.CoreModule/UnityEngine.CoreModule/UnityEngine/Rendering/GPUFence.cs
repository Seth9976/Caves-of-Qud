using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200047C RID: 1148
	[Obsolete("GPUFence has been deprecated. Use GraphicsFence instead (UnityUpgradable) -> GraphicsFence", false)]
	public struct GPUFence
	{
		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06002D7B RID: 11643 RVA: 0x000563D4 File Offset: 0x000545D4
		public bool passed
		{
			get
			{
				return true;
			}
		}
	}
}
