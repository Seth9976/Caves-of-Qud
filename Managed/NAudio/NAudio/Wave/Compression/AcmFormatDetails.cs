using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Compression
{
	// Token: 0x02000160 RID: 352
	[StructLayout(0, Pack = 4)]
	internal struct AcmFormatDetails
	{
		// Token: 0x040007D0 RID: 2000
		public const int FormatDescriptionChars = 128;

		// Token: 0x040007D1 RID: 2001
		public int structSize;

		// Token: 0x040007D2 RID: 2002
		public int formatIndex;

		// Token: 0x040007D3 RID: 2003
		public int formatTag;

		// Token: 0x040007D4 RID: 2004
		public AcmDriverDetailsSupportFlags supportFlags;

		// Token: 0x040007D5 RID: 2005
		public IntPtr waveFormatPointer;

		// Token: 0x040007D6 RID: 2006
		public int waveFormatByteSize;

		// Token: 0x040007D7 RID: 2007
		[MarshalAs(23, SizeConst = 128)]
		public string formatDescription;
	}
}
