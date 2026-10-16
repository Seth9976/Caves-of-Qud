using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Compression
{
	// Token: 0x0200015A RID: 346
	[StructLayout(0, Pack = 2)]
	internal struct AcmDriverDetails
	{
		// Token: 0x04000796 RID: 1942
		private const int ShortNameChars = 32;

		// Token: 0x04000797 RID: 1943
		private const int LongNameChars = 128;

		// Token: 0x04000798 RID: 1944
		private const int CopyrightChars = 80;

		// Token: 0x04000799 RID: 1945
		private const int LicensingChars = 128;

		// Token: 0x0400079A RID: 1946
		private const int FeaturesChars = 512;

		// Token: 0x0400079B RID: 1947
		public int structureSize;

		// Token: 0x0400079C RID: 1948
		public uint fccType;

		// Token: 0x0400079D RID: 1949
		public uint fccComp;

		// Token: 0x0400079E RID: 1950
		public ushort manufacturerId;

		// Token: 0x0400079F RID: 1951
		public ushort productId;

		// Token: 0x040007A0 RID: 1952
		public uint acmVersion;

		// Token: 0x040007A1 RID: 1953
		public uint driverVersion;

		// Token: 0x040007A2 RID: 1954
		public AcmDriverDetailsSupportFlags supportFlags;

		// Token: 0x040007A3 RID: 1955
		public int formatTagsCount;

		// Token: 0x040007A4 RID: 1956
		public int filterTagsCount;

		// Token: 0x040007A5 RID: 1957
		public IntPtr hicon;

		// Token: 0x040007A6 RID: 1958
		[MarshalAs(23, SizeConst = 32)]
		public string shortName;

		// Token: 0x040007A7 RID: 1959
		[MarshalAs(23, SizeConst = 128)]
		public string longName;

		// Token: 0x040007A8 RID: 1960
		[MarshalAs(23, SizeConst = 80)]
		public string copyright;

		// Token: 0x040007A9 RID: 1961
		[MarshalAs(23, SizeConst = 128)]
		public string licensing;

		// Token: 0x040007AA RID: 1962
		[MarshalAs(23, SizeConst = 512)]
		public string features;
	}
}
