using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000078 RID: 120
	public enum MetadataType : byte
	{
		// Token: 0x04000120 RID: 288
		Void = 1,
		// Token: 0x04000121 RID: 289
		Boolean,
		// Token: 0x04000122 RID: 290
		Char,
		// Token: 0x04000123 RID: 291
		SByte,
		// Token: 0x04000124 RID: 292
		Byte,
		// Token: 0x04000125 RID: 293
		Int16,
		// Token: 0x04000126 RID: 294
		UInt16,
		// Token: 0x04000127 RID: 295
		Int32,
		// Token: 0x04000128 RID: 296
		UInt32,
		// Token: 0x04000129 RID: 297
		Int64,
		// Token: 0x0400012A RID: 298
		UInt64,
		// Token: 0x0400012B RID: 299
		Single,
		// Token: 0x0400012C RID: 300
		Double,
		// Token: 0x0400012D RID: 301
		String,
		// Token: 0x0400012E RID: 302
		Pointer,
		// Token: 0x0400012F RID: 303
		ByReference,
		// Token: 0x04000130 RID: 304
		ValueType,
		// Token: 0x04000131 RID: 305
		Class,
		// Token: 0x04000132 RID: 306
		Var,
		// Token: 0x04000133 RID: 307
		Array,
		// Token: 0x04000134 RID: 308
		GenericInstance,
		// Token: 0x04000135 RID: 309
		TypedByReference,
		// Token: 0x04000136 RID: 310
		IntPtr = 24,
		// Token: 0x04000137 RID: 311
		UIntPtr,
		// Token: 0x04000138 RID: 312
		FunctionPointer = 27,
		// Token: 0x04000139 RID: 313
		Object,
		// Token: 0x0400013A RID: 314
		MVar = 30,
		// Token: 0x0400013B RID: 315
		RequiredModifier,
		// Token: 0x0400013C RID: 316
		OptionalModifier,
		// Token: 0x0400013D RID: 317
		Sentinel = 65,
		// Token: 0x0400013E RID: 318
		Pinned = 69
	}
}
