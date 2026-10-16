using System;

namespace LitJson
{
	// Token: 0x0200032F RID: 815
	internal enum ParserToken
	{
		// Token: 0x04000FE1 RID: 4065
		None = 65536,
		// Token: 0x04000FE2 RID: 4066
		Number,
		// Token: 0x04000FE3 RID: 4067
		True,
		// Token: 0x04000FE4 RID: 4068
		False,
		// Token: 0x04000FE5 RID: 4069
		Null,
		// Token: 0x04000FE6 RID: 4070
		CharSeq,
		// Token: 0x04000FE7 RID: 4071
		Char,
		// Token: 0x04000FE8 RID: 4072
		Text,
		// Token: 0x04000FE9 RID: 4073
		Object,
		// Token: 0x04000FEA RID: 4074
		ObjectPrime,
		// Token: 0x04000FEB RID: 4075
		Pair,
		// Token: 0x04000FEC RID: 4076
		PairRest,
		// Token: 0x04000FED RID: 4077
		Array,
		// Token: 0x04000FEE RID: 4078
		ArrayPrime,
		// Token: 0x04000FEF RID: 4079
		Value,
		// Token: 0x04000FF0 RID: 4080
		ValueRest,
		// Token: 0x04000FF1 RID: 4081
		String,
		// Token: 0x04000FF2 RID: 4082
		End,
		// Token: 0x04000FF3 RID: 4083
		Epsilon
	}
}
