using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200019D RID: 413
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamDatagramHostedAddress
	{
		// Token: 0x060009C5 RID: 2501 RVA: 0x0000F3E8 File Offset: 0x0000D5E8
		public void Clear()
		{
			this.m_cbSize = 0;
			this.m_data = new byte[128];
		}

		// Token: 0x04000ABB RID: 2747
		public int m_cbSize;

		// Token: 0x04000ABC RID: 2748
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		public byte[] m_data;
	}
}
