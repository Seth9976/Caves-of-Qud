using System;

namespace NAudio.Wave.Asio
{
	// Token: 0x0200006E RID: 110
	public enum AsioSampleType
	{
		// Token: 0x04000359 RID: 857
		Int16MSB,
		// Token: 0x0400035A RID: 858
		Int24MSB,
		// Token: 0x0400035B RID: 859
		Int32MSB,
		// Token: 0x0400035C RID: 860
		Float32MSB,
		// Token: 0x0400035D RID: 861
		Float64MSB,
		// Token: 0x0400035E RID: 862
		Int32MSB16 = 8,
		// Token: 0x0400035F RID: 863
		Int32MSB18,
		// Token: 0x04000360 RID: 864
		Int32MSB20,
		// Token: 0x04000361 RID: 865
		Int32MSB24,
		// Token: 0x04000362 RID: 866
		Int16LSB = 16,
		// Token: 0x04000363 RID: 867
		Int24LSB,
		// Token: 0x04000364 RID: 868
		Int32LSB,
		// Token: 0x04000365 RID: 869
		Float32LSB,
		// Token: 0x04000366 RID: 870
		Float64LSB,
		// Token: 0x04000367 RID: 871
		Int32LSB16 = 24,
		// Token: 0x04000368 RID: 872
		Int32LSB18,
		// Token: 0x04000369 RID: 873
		Int32LSB20,
		// Token: 0x0400036A RID: 874
		Int32LSB24,
		// Token: 0x0400036B RID: 875
		DSDInt8LSB1 = 32,
		// Token: 0x0400036C RID: 876
		DSDInt8MSB1,
		// Token: 0x0400036D RID: 877
		DSDInt8NER8 = 40
	}
}
