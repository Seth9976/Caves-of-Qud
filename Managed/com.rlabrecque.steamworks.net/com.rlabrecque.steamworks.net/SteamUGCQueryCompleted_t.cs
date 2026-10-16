using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000B6 RID: 182
	[CallbackIdentity(3401)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamUGCQueryCompleted_t
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x00004617 File Offset: 0x00002817
		// (set) Token: 0x0600048A RID: 1162 RVA: 0x00004624 File Offset: 0x00002824
		public string m_rgchNextCursor
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchNextCursor_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchNextCursor_, 256);
			}
		}

		// Token: 0x040002A4 RID: 676
		public const int k_iCallback = 3401;

		// Token: 0x040002A5 RID: 677
		public UGCQueryHandle_t m_handle;

		// Token: 0x040002A6 RID: 678
		public EResult m_eResult;

		// Token: 0x040002A7 RID: 679
		public uint m_unNumResultsReturned;

		// Token: 0x040002A8 RID: 680
		public uint m_unTotalMatchingResults;

		// Token: 0x040002A9 RID: 681
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bCachedData;

		// Token: 0x040002AA RID: 682
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		private byte[] m_rgchNextCursor_;
	}
}
