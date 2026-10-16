using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001BB RID: 443
	[Serializable]
	public struct SteamNetworkingMessage_t
	{
		// Token: 0x06000AC4 RID: 2756 RVA: 0x000101F8 File Offset: 0x0000E3F8
		public void Release()
		{
			throw new NotImplementedException("Please use the static Release function instead which takes an IntPtr.");
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00010204 File Offset: 0x0000E404
		public static void Release(IntPtr pointer)
		{
			NativeMethods.SteamAPI_SteamNetworkingMessage_t_Release(pointer);
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0001020C File Offset: 0x0000E40C
		public static SteamNetworkingMessage_t FromIntPtr(IntPtr pointer)
		{
			return (SteamNetworkingMessage_t)Marshal.PtrToStructure(pointer, typeof(SteamNetworkingMessage_t));
		}

		// Token: 0x04000B16 RID: 2838
		public IntPtr m_pData;

		// Token: 0x04000B17 RID: 2839
		public int m_cbSize;

		// Token: 0x04000B18 RID: 2840
		public HSteamNetConnection m_conn;

		// Token: 0x04000B19 RID: 2841
		public SteamNetworkingIdentity m_identityPeer;

		// Token: 0x04000B1A RID: 2842
		public long m_nConnUserData;

		// Token: 0x04000B1B RID: 2843
		public SteamNetworkingMicroseconds m_usecTimeReceived;

		// Token: 0x04000B1C RID: 2844
		public long m_nMessageNumber;

		// Token: 0x04000B1D RID: 2845
		public IntPtr m_pfnFreeData;

		// Token: 0x04000B1E RID: 2846
		internal IntPtr m_pfnRelease;

		// Token: 0x04000B1F RID: 2847
		public int m_nChannel;

		// Token: 0x04000B20 RID: 2848
		public int m_nFlags;

		// Token: 0x04000B21 RID: 2849
		public long m_nUserData;

		// Token: 0x04000B22 RID: 2850
		public ushort m_idxLane;

		// Token: 0x04000B23 RID: 2851
		public ushort _pad1__;
	}
}
