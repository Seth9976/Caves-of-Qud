using System;

namespace NAudio.Wave.Compression
{
	// Token: 0x02000162 RID: 354
	[Flags]
	internal enum AcmFormatSuggestFlags
	{
		// Token: 0x040007E4 RID: 2020
		FormatTag = 65536,
		// Token: 0x040007E5 RID: 2021
		Channels = 131072,
		// Token: 0x040007E6 RID: 2022
		SamplesPerSecond = 262144,
		// Token: 0x040007E7 RID: 2023
		BitsPerSample = 524288,
		// Token: 0x040007E8 RID: 2024
		TypeMask = 16711680
	}
}
