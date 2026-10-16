using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000025 RID: 37
	[CallbackIdentity(337)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GameRichPresenceJoinRequested_t
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x0000441B File Offset: 0x0000261B
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x00004428 File Offset: 0x00002628
		public string m_rgchConnect
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchConnect_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchConnect_, 256);
			}
		}

		// Token: 0x04000083 RID: 131
		public const int k_iCallback = 337;

		// Token: 0x04000084 RID: 132
		public CSteamID m_steamIDFriend;

		// Token: 0x04000085 RID: 133
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		private byte[] m_rgchConnect_;
	}
}
