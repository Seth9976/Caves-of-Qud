using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Asio
{
	// Token: 0x02000153 RID: 339
	[StructLayout(0, Pack = 4)]
	internal struct ASIOTime
	{
		// Token: 0x04000785 RID: 1925
		public int reserved1;

		// Token: 0x04000786 RID: 1926
		public int reserved2;

		// Token: 0x04000787 RID: 1927
		public int reserved3;

		// Token: 0x04000788 RID: 1928
		public int reserved4;

		// Token: 0x04000789 RID: 1929
		public AsioTimeInfo timeInfo;

		// Token: 0x0400078A RID: 1930
		public ASIOTimeCode timeCode;
	}
}
