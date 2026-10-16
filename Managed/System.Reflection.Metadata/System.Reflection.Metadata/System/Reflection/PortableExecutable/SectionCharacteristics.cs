using System;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x02000034 RID: 52
	[Flags]
	public enum SectionCharacteristics : uint
	{
		// Token: 0x04000113 RID: 275
		TypeReg = 0U,
		// Token: 0x04000114 RID: 276
		TypeDSect = 1U,
		// Token: 0x04000115 RID: 277
		TypeNoLoad = 2U,
		// Token: 0x04000116 RID: 278
		TypeGroup = 4U,
		// Token: 0x04000117 RID: 279
		TypeNoPad = 8U,
		// Token: 0x04000118 RID: 280
		TypeCopy = 16U,
		// Token: 0x04000119 RID: 281
		ContainsCode = 32U,
		// Token: 0x0400011A RID: 282
		ContainsInitializedData = 64U,
		// Token: 0x0400011B RID: 283
		ContainsUninitializedData = 128U,
		// Token: 0x0400011C RID: 284
		LinkerOther = 256U,
		// Token: 0x0400011D RID: 285
		LinkerInfo = 512U,
		// Token: 0x0400011E RID: 286
		TypeOver = 1024U,
		// Token: 0x0400011F RID: 287
		LinkerRemove = 2048U,
		// Token: 0x04000120 RID: 288
		LinkerComdat = 4096U,
		// Token: 0x04000121 RID: 289
		MemProtected = 16384U,
		// Token: 0x04000122 RID: 290
		NoDeferSpecExc = 16384U,
		// Token: 0x04000123 RID: 291
		GPRel = 32768U,
		// Token: 0x04000124 RID: 292
		MemFardata = 32768U,
		// Token: 0x04000125 RID: 293
		MemSysheap = 65536U,
		// Token: 0x04000126 RID: 294
		MemPurgeable = 131072U,
		// Token: 0x04000127 RID: 295
		Mem16Bit = 131072U,
		// Token: 0x04000128 RID: 296
		MemLocked = 262144U,
		// Token: 0x04000129 RID: 297
		MemPreload = 524288U,
		// Token: 0x0400012A RID: 298
		Align1Bytes = 1048576U,
		// Token: 0x0400012B RID: 299
		Align2Bytes = 2097152U,
		// Token: 0x0400012C RID: 300
		Align4Bytes = 3145728U,
		// Token: 0x0400012D RID: 301
		Align8Bytes = 4194304U,
		// Token: 0x0400012E RID: 302
		Align16Bytes = 5242880U,
		// Token: 0x0400012F RID: 303
		Align32Bytes = 6291456U,
		// Token: 0x04000130 RID: 304
		Align64Bytes = 7340032U,
		// Token: 0x04000131 RID: 305
		Align128Bytes = 8388608U,
		// Token: 0x04000132 RID: 306
		Align256Bytes = 9437184U,
		// Token: 0x04000133 RID: 307
		Align512Bytes = 10485760U,
		// Token: 0x04000134 RID: 308
		Align1024Bytes = 11534336U,
		// Token: 0x04000135 RID: 309
		Align2048Bytes = 12582912U,
		// Token: 0x04000136 RID: 310
		Align4096Bytes = 13631488U,
		// Token: 0x04000137 RID: 311
		Align8192Bytes = 14680064U,
		// Token: 0x04000138 RID: 312
		AlignMask = 15728640U,
		// Token: 0x04000139 RID: 313
		LinkerNRelocOvfl = 16777216U,
		// Token: 0x0400013A RID: 314
		MemDiscardable = 33554432U,
		// Token: 0x0400013B RID: 315
		MemNotCached = 67108864U,
		// Token: 0x0400013C RID: 316
		MemNotPaged = 134217728U,
		// Token: 0x0400013D RID: 317
		MemShared = 268435456U,
		// Token: 0x0400013E RID: 318
		MemExecute = 536870912U,
		// Token: 0x0400013F RID: 319
		MemRead = 1073741824U,
		// Token: 0x04000140 RID: 320
		MemWrite = 2147483648U
	}
}
