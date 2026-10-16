using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200019E RID: 414
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamDatagramRelayAuthTicket
	{
		// Token: 0x060009C6 RID: 2502 RVA: 0x0000F401 File Offset: 0x0000D601
		public void Clear()
		{
		}

		// Token: 0x04000ABD RID: 2749
		private SteamNetworkingIdentity m_identityGameserver;

		// Token: 0x04000ABE RID: 2750
		private SteamNetworkingIdentity m_identityAuthorizedClient;

		// Token: 0x04000ABF RID: 2751
		private uint m_unPublicIP;

		// Token: 0x04000AC0 RID: 2752
		private RTime32 m_rtimeTicketExpiry;

		// Token: 0x04000AC1 RID: 2753
		private SteamDatagramHostedAddress m_routing;

		// Token: 0x04000AC2 RID: 2754
		private uint m_nAppID;

		// Token: 0x04000AC3 RID: 2755
		private int m_nRestrictToVirtualPort;

		// Token: 0x04000AC4 RID: 2756
		private const int k_nMaxExtraFields = 16;

		// Token: 0x04000AC5 RID: 2757
		private int m_nExtraFields;

		// Token: 0x04000AC6 RID: 2758
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		private SteamDatagramRelayAuthTicket.ExtraField[] m_vecExtraFields;

		// Token: 0x020001F3 RID: 499
		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		private struct ExtraField
		{
			// Token: 0x04000B65 RID: 2917
			private SteamDatagramRelayAuthTicket.ExtraField.EType m_eType;

			// Token: 0x04000B66 RID: 2918
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 28)]
			private byte[] m_szName;

			// Token: 0x04000B67 RID: 2919
			private SteamDatagramRelayAuthTicket.ExtraField.OptionValue m_val;

			// Token: 0x020001FA RID: 506
			private enum EType
			{
				// Token: 0x04000B74 RID: 2932
				k_EType_String,
				// Token: 0x04000B75 RID: 2933
				k_EType_Int,
				// Token: 0x04000B76 RID: 2934
				k_EType_Fixed64
			}

			// Token: 0x020001FB RID: 507
			[StructLayout(LayoutKind.Explicit)]
			private struct OptionValue
			{
				// Token: 0x04000B77 RID: 2935
				[FieldOffset(0)]
				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
				private byte[] m_szStringValue;

				// Token: 0x04000B78 RID: 2936
				[FieldOffset(0)]
				private long m_nIntValue;

				// Token: 0x04000B79 RID: 2937
				[FieldOffset(0)]
				private ulong m_nFixed64Value;
			}
		}
	}
}
