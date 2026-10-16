using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200016A RID: 362
	public struct MatchMakingKeyValuePair_t
	{
		// Token: 0x0600049F RID: 1183 RVA: 0x00004777 File Offset: 0x00002977
		private MatchMakingKeyValuePair_t(string strKey, string strValue)
		{
			this.m_szKey = strKey;
			this.m_szValue = strValue;
		}

		// Token: 0x04000A75 RID: 2677
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string m_szKey;

		// Token: 0x04000A76 RID: 2678
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string m_szValue;
	}
}
