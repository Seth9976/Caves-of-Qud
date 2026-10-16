using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000066 RID: 102
	[CallbackIdentity(4705)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamInventoryRequestPricesResult_t
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0000449B File Offset: 0x0000269B
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x000044A8 File Offset: 0x000026A8
		public string m_rgchCurrency
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchCurrency_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchCurrency_, 4);
			}
		}

		// Token: 0x04000184 RID: 388
		public const int k_iCallback = 4705;

		// Token: 0x04000185 RID: 389
		public EResult m_result;

		// Token: 0x04000186 RID: 390
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		private byte[] m_rgchCurrency_;
	}
}
