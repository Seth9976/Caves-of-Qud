using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000080 RID: 128
	[CallbackIdentity(4002)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct VolumeHasChanged_t
	{
		// Token: 0x040001ED RID: 493
		public const int k_iCallback = 4002;

		// Token: 0x040001EE RID: 494
		public float m_flNewVolume;
	}
}
