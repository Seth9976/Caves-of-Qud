using System;

namespace NAudio.Wave.Compression
{
	// Token: 0x02000161 RID: 353
	[Flags]
	public enum AcmFormatEnumFlags
	{
		// Token: 0x040007D9 RID: 2009
		None = 0,
		// Token: 0x040007DA RID: 2010
		Convert = 1048576,
		// Token: 0x040007DB RID: 2011
		Hardware = 4194304,
		// Token: 0x040007DC RID: 2012
		Input = 8388608,
		// Token: 0x040007DD RID: 2013
		Channels = 131072,
		// Token: 0x040007DE RID: 2014
		SamplesPerSecond = 262144,
		// Token: 0x040007DF RID: 2015
		Output = 16777216,
		// Token: 0x040007E0 RID: 2016
		Suggest = 2097152,
		// Token: 0x040007E1 RID: 2017
		BitsPerSample = 524288,
		// Token: 0x040007E2 RID: 2018
		FormatTag = 65536
	}
}
