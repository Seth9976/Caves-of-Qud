using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Compression
{
	// Token: 0x02000164 RID: 356
	internal struct AcmFormatTagDetails
	{
		// Token: 0x040007EA RID: 2026
		public const int FormatTagDescriptionChars = 48;

		// Token: 0x040007EB RID: 2027
		public int structureSize;

		// Token: 0x040007EC RID: 2028
		public int formatTagIndex;

		// Token: 0x040007ED RID: 2029
		public int formatTag;

		// Token: 0x040007EE RID: 2030
		public int formatSize;

		// Token: 0x040007EF RID: 2031
		public AcmDriverDetailsSupportFlags supportFlags;

		// Token: 0x040007F0 RID: 2032
		public int standardFormatsCount;

		// Token: 0x040007F1 RID: 2033
		[MarshalAs(23, SizeConst = 48)]
		public string formatDescription;
	}
}
