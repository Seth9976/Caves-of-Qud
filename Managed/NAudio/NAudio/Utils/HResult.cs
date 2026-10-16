using System;
using System.Runtime.InteropServices;

namespace NAudio.Utils
{
	// Token: 0x02000112 RID: 274
	public static class HResult
	{
		// Token: 0x060005E0 RID: 1504 RVA: 0x000131DC File Offset: 0x000113DC
		public static int MAKE_HRESULT(int sev, int fac, int code)
		{
			return (sev << 31) | (fac << 16) | code;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x000131E9 File Offset: 0x000113E9
		public static int GetHResult(this COMException exception)
		{
			return exception.ErrorCode;
		}

		// Token: 0x040006A5 RID: 1701
		public const int S_OK = 0;

		// Token: 0x040006A6 RID: 1702
		public const int S_FALSE = 1;

		// Token: 0x040006A7 RID: 1703
		public const int E_INVALIDARG = -2147483645;

		// Token: 0x040006A8 RID: 1704
		private const int FACILITY_AAF = 18;

		// Token: 0x040006A9 RID: 1705
		private const int FACILITY_ACS = 20;

		// Token: 0x040006AA RID: 1706
		private const int FACILITY_BACKGROUNDCOPY = 32;

		// Token: 0x040006AB RID: 1707
		private const int FACILITY_CERT = 11;

		// Token: 0x040006AC RID: 1708
		private const int FACILITY_COMPLUS = 17;

		// Token: 0x040006AD RID: 1709
		private const int FACILITY_CONFIGURATION = 33;

		// Token: 0x040006AE RID: 1710
		private const int FACILITY_CONTROL = 10;

		// Token: 0x040006AF RID: 1711
		private const int FACILITY_DISPATCH = 2;

		// Token: 0x040006B0 RID: 1712
		private const int FACILITY_DPLAY = 21;

		// Token: 0x040006B1 RID: 1713
		private const int FACILITY_HTTP = 25;

		// Token: 0x040006B2 RID: 1714
		private const int FACILITY_INTERNET = 12;

		// Token: 0x040006B3 RID: 1715
		private const int FACILITY_ITF = 4;

		// Token: 0x040006B4 RID: 1716
		private const int FACILITY_MEDIASERVER = 13;

		// Token: 0x040006B5 RID: 1717
		private const int FACILITY_MSMQ = 14;

		// Token: 0x040006B6 RID: 1718
		private const int FACILITY_NULL = 0;

		// Token: 0x040006B7 RID: 1719
		private const int FACILITY_RPC = 1;

		// Token: 0x040006B8 RID: 1720
		private const int FACILITY_SCARD = 16;

		// Token: 0x040006B9 RID: 1721
		private const int FACILITY_SECURITY = 9;

		// Token: 0x040006BA RID: 1722
		private const int FACILITY_SETUPAPI = 15;

		// Token: 0x040006BB RID: 1723
		private const int FACILITY_SSPI = 9;

		// Token: 0x040006BC RID: 1724
		private const int FACILITY_STORAGE = 3;

		// Token: 0x040006BD RID: 1725
		private const int FACILITY_SXS = 23;

		// Token: 0x040006BE RID: 1726
		private const int FACILITY_UMI = 22;

		// Token: 0x040006BF RID: 1727
		private const int FACILITY_URT = 19;

		// Token: 0x040006C0 RID: 1728
		private const int FACILITY_WIN32 = 7;

		// Token: 0x040006C1 RID: 1729
		private const int FACILITY_WINDOWS = 8;

		// Token: 0x040006C2 RID: 1730
		private const int FACILITY_WINDOWS_CE = 24;
	}
}
