using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Compression
{
	// Token: 0x0200015E RID: 350
	[StructLayout(0, CharSet = 4, Pack = 4)]
	internal struct AcmFormatChoose
	{
		// Token: 0x040007B8 RID: 1976
		public int structureSize;

		// Token: 0x040007B9 RID: 1977
		public AcmFormatChooseStyleFlags styleFlags;

		// Token: 0x040007BA RID: 1978
		public IntPtr ownerWindowHandle;

		// Token: 0x040007BB RID: 1979
		public IntPtr selectedWaveFormatPointer;

		// Token: 0x040007BC RID: 1980
		public int selectedWaveFormatByteSize;

		// Token: 0x040007BD RID: 1981
		[MarshalAs(22)]
		public string title;

		// Token: 0x040007BE RID: 1982
		[MarshalAs(23, SizeConst = 48)]
		public string formatTagDescription;

		// Token: 0x040007BF RID: 1983
		[MarshalAs(23, SizeConst = 128)]
		public string formatDescription;

		// Token: 0x040007C0 RID: 1984
		[MarshalAs(22)]
		public string name;

		// Token: 0x040007C1 RID: 1985
		public int nameByteSize;

		// Token: 0x040007C2 RID: 1986
		public AcmFormatEnumFlags formatEnumFlags;

		// Token: 0x040007C3 RID: 1987
		public IntPtr waveFormatEnumPointer;

		// Token: 0x040007C4 RID: 1988
		public IntPtr instanceHandle;

		// Token: 0x040007C5 RID: 1989
		[MarshalAs(22)]
		public string templateName;

		// Token: 0x040007C6 RID: 1990
		public IntPtr customData;

		// Token: 0x040007C7 RID: 1991
		public AcmInterop.AcmFormatChooseHookProc windowCallbackFunction;
	}
}
