using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000BE RID: 190
	[CallbackIdentity(3409)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GetUserItemVoteResult_t
	{
		// Token: 0x040002C7 RID: 711
		public const int k_iCallback = 3409;

		// Token: 0x040002C8 RID: 712
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040002C9 RID: 713
		public EResult m_eResult;

		// Token: 0x040002CA RID: 714
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bVotedUp;

		// Token: 0x040002CB RID: 715
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bVotedDown;

		// Token: 0x040002CC RID: 716
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bVoteSkipped;
	}
}
