using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001B7 RID: 439
	[Serializable]
	public struct SteamNetworkingConfigValue_t
	{
		// Token: 0x04000AE9 RID: 2793
		public ESteamNetworkingConfigValue m_eValue;

		// Token: 0x04000AEA RID: 2794
		public ESteamNetworkingConfigDataType m_eDataType;

		// Token: 0x04000AEB RID: 2795
		public SteamNetworkingConfigValue_t.OptionValue m_val;

		// Token: 0x020001F7 RID: 503
		[StructLayout(LayoutKind.Explicit)]
		public struct OptionValue
		{
			// Token: 0x04000B6E RID: 2926
			[FieldOffset(0)]
			public int m_int32;

			// Token: 0x04000B6F RID: 2927
			[FieldOffset(0)]
			public long m_int64;

			// Token: 0x04000B70 RID: 2928
			[FieldOffset(0)]
			public float m_float;

			// Token: 0x04000B71 RID: 2929
			[FieldOffset(0)]
			public IntPtr m_string;

			// Token: 0x04000B72 RID: 2930
			[FieldOffset(0)]
			public IntPtr m_functionPtr;
		}
	}
}
