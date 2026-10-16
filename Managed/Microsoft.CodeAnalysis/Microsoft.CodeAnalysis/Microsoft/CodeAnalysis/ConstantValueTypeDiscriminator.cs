using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200015B RID: 347
	internal enum ConstantValueTypeDiscriminator : byte
	{
		// Token: 0x040003D0 RID: 976
		Nothing,
		// Token: 0x040003D1 RID: 977
		Null = 0,
		// Token: 0x040003D2 RID: 978
		Bad,
		// Token: 0x040003D3 RID: 979
		SByte,
		// Token: 0x040003D4 RID: 980
		Byte,
		// Token: 0x040003D5 RID: 981
		Int16,
		// Token: 0x040003D6 RID: 982
		UInt16,
		// Token: 0x040003D7 RID: 983
		Int32,
		// Token: 0x040003D8 RID: 984
		UInt32,
		// Token: 0x040003D9 RID: 985
		Int64,
		// Token: 0x040003DA RID: 986
		UInt64,
		// Token: 0x040003DB RID: 987
		NInt,
		// Token: 0x040003DC RID: 988
		NUInt,
		// Token: 0x040003DD RID: 989
		Char,
		// Token: 0x040003DE RID: 990
		Boolean,
		// Token: 0x040003DF RID: 991
		Single,
		// Token: 0x040003E0 RID: 992
		Double,
		// Token: 0x040003E1 RID: 993
		String,
		// Token: 0x040003E2 RID: 994
		Decimal,
		// Token: 0x040003E3 RID: 995
		DateTime
	}
}
