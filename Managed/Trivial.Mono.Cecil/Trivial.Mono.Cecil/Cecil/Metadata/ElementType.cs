using System;

namespace Trivial.Mono.Cecil.Metadata
{
	// Token: 0x020000DD RID: 221
	internal enum ElementType : byte
	{
		// Token: 0x0400038B RID: 907
		None,
		// Token: 0x0400038C RID: 908
		Void,
		// Token: 0x0400038D RID: 909
		Boolean,
		// Token: 0x0400038E RID: 910
		Char,
		// Token: 0x0400038F RID: 911
		I1,
		// Token: 0x04000390 RID: 912
		U1,
		// Token: 0x04000391 RID: 913
		I2,
		// Token: 0x04000392 RID: 914
		U2,
		// Token: 0x04000393 RID: 915
		I4,
		// Token: 0x04000394 RID: 916
		U4,
		// Token: 0x04000395 RID: 917
		I8,
		// Token: 0x04000396 RID: 918
		U8,
		// Token: 0x04000397 RID: 919
		R4,
		// Token: 0x04000398 RID: 920
		R8,
		// Token: 0x04000399 RID: 921
		String,
		// Token: 0x0400039A RID: 922
		Ptr,
		// Token: 0x0400039B RID: 923
		ByRef,
		// Token: 0x0400039C RID: 924
		ValueType,
		// Token: 0x0400039D RID: 925
		Class,
		// Token: 0x0400039E RID: 926
		Var,
		// Token: 0x0400039F RID: 927
		Array,
		// Token: 0x040003A0 RID: 928
		GenericInst,
		// Token: 0x040003A1 RID: 929
		TypedByRef,
		// Token: 0x040003A2 RID: 930
		I = 24,
		// Token: 0x040003A3 RID: 931
		U,
		// Token: 0x040003A4 RID: 932
		FnPtr = 27,
		// Token: 0x040003A5 RID: 933
		Object,
		// Token: 0x040003A6 RID: 934
		SzArray,
		// Token: 0x040003A7 RID: 935
		MVar,
		// Token: 0x040003A8 RID: 936
		CModReqD,
		// Token: 0x040003A9 RID: 937
		CModOpt,
		// Token: 0x040003AA RID: 938
		Internal,
		// Token: 0x040003AB RID: 939
		Modifier = 64,
		// Token: 0x040003AC RID: 940
		Sentinel,
		// Token: 0x040003AD RID: 941
		Pinned = 69,
		// Token: 0x040003AE RID: 942
		Type = 80,
		// Token: 0x040003AF RID: 943
		Boxed,
		// Token: 0x040003B0 RID: 944
		Enum = 85
	}
}
