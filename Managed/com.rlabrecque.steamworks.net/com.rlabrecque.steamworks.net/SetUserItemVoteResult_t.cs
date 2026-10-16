using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000BD RID: 189
	[CallbackIdentity(3408)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SetUserItemVoteResult_t
	{
		// Token: 0x040002C3 RID: 707
		public const int k_iCallback = 3408;

		// Token: 0x040002C4 RID: 708
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040002C5 RID: 709
		public EResult m_eResult;

		// Token: 0x040002C6 RID: 710
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bVoteUp;
	}
}
