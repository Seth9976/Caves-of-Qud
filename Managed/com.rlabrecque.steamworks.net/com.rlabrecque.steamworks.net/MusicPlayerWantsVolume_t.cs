using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200008B RID: 139
	[CallbackIdentity(4011)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct MusicPlayerWantsVolume_t
	{
		// Token: 0x040001FB RID: 507
		public const int k_iCallback = 4011;

		// Token: 0x040001FC RID: 508
		public float m_flNewVolume;
	}
}
