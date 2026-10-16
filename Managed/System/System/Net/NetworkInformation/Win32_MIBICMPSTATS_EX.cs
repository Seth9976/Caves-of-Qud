using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200073B RID: 1851
	internal struct Win32_MIBICMPSTATS_EX
	{
		// Token: 0x040022C5 RID: 8901
		public uint Msgs;

		// Token: 0x040022C6 RID: 8902
		public uint Errors;

		// Token: 0x040022C7 RID: 8903
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		public uint[] Counts;
	}
}
