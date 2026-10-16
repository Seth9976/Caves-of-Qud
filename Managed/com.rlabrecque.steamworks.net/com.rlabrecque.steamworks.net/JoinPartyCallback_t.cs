using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000079 RID: 121
	[CallbackIdentity(5301)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct JoinPartyCallback_t
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x000044B7 File Offset: 0x000026B7
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x000044C4 File Offset: 0x000026C4
		public string m_rgchConnectString
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchConnectString_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchConnectString_, 256);
			}
		}

		// Token: 0x040001DD RID: 477
		public const int k_iCallback = 5301;

		// Token: 0x040001DE RID: 478
		public EResult m_eResult;

		// Token: 0x040001DF RID: 479
		public PartyBeaconID_t m_ulBeaconID;

		// Token: 0x040001E0 RID: 480
		public CSteamID m_SteamIDBeaconOwner;

		// Token: 0x040001E1 RID: 481
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		private byte[] m_rgchConnectString_;
	}
}
