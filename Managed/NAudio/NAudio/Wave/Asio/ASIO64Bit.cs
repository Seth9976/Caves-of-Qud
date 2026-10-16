using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Asio
{
	// Token: 0x0200014F RID: 335
	[StructLayout(0, Pack = 4)]
	internal struct ASIO64Bit
	{
		// Token: 0x0400076F RID: 1903
		public uint hi;

		// Token: 0x04000770 RID: 1904
		public uint lo;
	}
}
