using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Asio
{
	// Token: 0x0200014E RID: 334
	[StructLayout(0, Pack = 4)]
	internal struct ASIOTimeCode
	{
		// Token: 0x0400076B RID: 1899
		public double speed;

		// Token: 0x0400076C RID: 1900
		public ASIO64Bit timeCodeSamples;

		// Token: 0x0400076D RID: 1901
		public ASIOTimeCodeFlags flags;

		// Token: 0x0400076E RID: 1902
		[MarshalAs(23, SizeConst = 64)]
		public string future;
	}
}
