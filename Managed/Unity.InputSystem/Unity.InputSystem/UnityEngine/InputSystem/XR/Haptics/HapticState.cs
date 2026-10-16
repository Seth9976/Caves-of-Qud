using System;

namespace UnityEngine.InputSystem.XR.Haptics
{
	// Token: 0x02000075 RID: 117
	public struct HapticState
	{
		// Token: 0x06000A77 RID: 2679 RVA: 0x00039202 File Offset: 0x00037402
		public HapticState(uint samplesQueued, uint samplesAvailable)
		{
			this.samplesQueued = samplesQueued;
			this.samplesAvailable = samplesAvailable;
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x00039212 File Offset: 0x00037412
		// (set) Token: 0x06000A79 RID: 2681 RVA: 0x0003921A File Offset: 0x0003741A
		public uint samplesQueued { readonly get; private set; }

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x00039223 File Offset: 0x00037423
		// (set) Token: 0x06000A7B RID: 2683 RVA: 0x0003922B File Offset: 0x0003742B
		public uint samplesAvailable { readonly get; private set; }
	}
}
