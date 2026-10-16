using System;

namespace System.IO.Compression
{
	// Token: 0x02000024 RID: 36
	internal enum InflaterState
	{
		// Token: 0x0400013E RID: 318
		ReadingHeader,
		// Token: 0x0400013F RID: 319
		ReadingBFinal = 2,
		// Token: 0x04000140 RID: 320
		ReadingBType,
		// Token: 0x04000141 RID: 321
		ReadingNumLitCodes,
		// Token: 0x04000142 RID: 322
		ReadingNumDistCodes,
		// Token: 0x04000143 RID: 323
		ReadingNumCodeLengthCodes,
		// Token: 0x04000144 RID: 324
		ReadingCodeLengthCodes,
		// Token: 0x04000145 RID: 325
		ReadingTreeCodesBefore,
		// Token: 0x04000146 RID: 326
		ReadingTreeCodesAfter,
		// Token: 0x04000147 RID: 327
		DecodeTop,
		// Token: 0x04000148 RID: 328
		HaveInitialLength,
		// Token: 0x04000149 RID: 329
		HaveFullLength,
		// Token: 0x0400014A RID: 330
		HaveDistCode,
		// Token: 0x0400014B RID: 331
		UncompressedAligning = 15,
		// Token: 0x0400014C RID: 332
		UncompressedByte1,
		// Token: 0x0400014D RID: 333
		UncompressedByte2,
		// Token: 0x0400014E RID: 334
		UncompressedByte3,
		// Token: 0x0400014F RID: 335
		UncompressedByte4,
		// Token: 0x04000150 RID: 336
		DecodingUncompressed,
		// Token: 0x04000151 RID: 337
		StartReadingFooter,
		// Token: 0x04000152 RID: 338
		ReadingFooter,
		// Token: 0x04000153 RID: 339
		VerifyingFooter,
		// Token: 0x04000154 RID: 340
		Done
	}
}
