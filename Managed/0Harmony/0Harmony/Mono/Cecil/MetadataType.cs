using System;

namespace Mono.Cecil
{
	// Token: 0x02000275 RID: 629
	public enum MetadataType : byte
	{
		// Token: 0x04000559 RID: 1369
		Void = 1,
		// Token: 0x0400055A RID: 1370
		Boolean,
		// Token: 0x0400055B RID: 1371
		Char,
		// Token: 0x0400055C RID: 1372
		SByte,
		// Token: 0x0400055D RID: 1373
		Byte,
		// Token: 0x0400055E RID: 1374
		Int16,
		// Token: 0x0400055F RID: 1375
		UInt16,
		// Token: 0x04000560 RID: 1376
		Int32,
		// Token: 0x04000561 RID: 1377
		UInt32,
		// Token: 0x04000562 RID: 1378
		Int64,
		// Token: 0x04000563 RID: 1379
		UInt64,
		// Token: 0x04000564 RID: 1380
		Single,
		// Token: 0x04000565 RID: 1381
		Double,
		// Token: 0x04000566 RID: 1382
		String,
		// Token: 0x04000567 RID: 1383
		Pointer,
		// Token: 0x04000568 RID: 1384
		ByReference,
		// Token: 0x04000569 RID: 1385
		ValueType,
		// Token: 0x0400056A RID: 1386
		Class,
		// Token: 0x0400056B RID: 1387
		Var,
		// Token: 0x0400056C RID: 1388
		Array,
		// Token: 0x0400056D RID: 1389
		GenericInstance,
		// Token: 0x0400056E RID: 1390
		TypedByReference,
		// Token: 0x0400056F RID: 1391
		IntPtr = 24,
		// Token: 0x04000570 RID: 1392
		UIntPtr,
		// Token: 0x04000571 RID: 1393
		FunctionPointer = 27,
		// Token: 0x04000572 RID: 1394
		Object,
		// Token: 0x04000573 RID: 1395
		MVar = 30,
		// Token: 0x04000574 RID: 1396
		RequiredModifier,
		// Token: 0x04000575 RID: 1397
		OptionalModifier,
		// Token: 0x04000576 RID: 1398
		Sentinel = 65,
		// Token: 0x04000577 RID: 1399
		Pinned = 69
	}
}
