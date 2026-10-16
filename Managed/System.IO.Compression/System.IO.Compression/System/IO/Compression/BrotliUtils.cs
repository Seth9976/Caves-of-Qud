using System;

namespace System.IO.Compression
{
	// Token: 0x0200000F RID: 15
	internal static class BrotliUtils
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00002FA2 File Offset: 0x000011A2
		internal static int GetQualityFromCompressionLevel(CompressionLevel level)
		{
			switch (level)
			{
			case CompressionLevel.Optimal:
				return 11;
			case CompressionLevel.Fastest:
				return 1;
			case CompressionLevel.NoCompression:
				return 0;
			default:
				return (int)level;
			}
		}

		// Token: 0x0400009E RID: 158
		public const int WindowBits_Min = 10;

		// Token: 0x0400009F RID: 159
		public const int WindowBits_Default = 22;

		// Token: 0x040000A0 RID: 160
		public const int WindowBits_Max = 24;

		// Token: 0x040000A1 RID: 161
		public const int Quality_Min = 0;

		// Token: 0x040000A2 RID: 162
		public const int Quality_Default = 11;

		// Token: 0x040000A3 RID: 163
		public const int Quality_Max = 11;

		// Token: 0x040000A4 RID: 164
		public const int MaxInputSize = 2147483132;
	}
}
