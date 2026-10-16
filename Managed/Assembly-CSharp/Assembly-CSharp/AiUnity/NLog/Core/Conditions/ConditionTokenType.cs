using System;

namespace AiUnity.NLog.Core.Conditions
{
	// Token: 0x020002B0 RID: 688
	internal enum ConditionTokenType
	{
		// Token: 0x04000E54 RID: 3668
		EndOfInput,
		// Token: 0x04000E55 RID: 3669
		BeginningOfInput,
		// Token: 0x04000E56 RID: 3670
		Number,
		// Token: 0x04000E57 RID: 3671
		String,
		// Token: 0x04000E58 RID: 3672
		Keyword,
		// Token: 0x04000E59 RID: 3673
		Whitespace,
		// Token: 0x04000E5A RID: 3674
		FirstPunct,
		// Token: 0x04000E5B RID: 3675
		LessThan,
		// Token: 0x04000E5C RID: 3676
		GreaterThan,
		// Token: 0x04000E5D RID: 3677
		LessThanOrEqualTo,
		// Token: 0x04000E5E RID: 3678
		GreaterThanOrEqualTo,
		// Token: 0x04000E5F RID: 3679
		EqualTo,
		// Token: 0x04000E60 RID: 3680
		NotEqual,
		// Token: 0x04000E61 RID: 3681
		LeftParen,
		// Token: 0x04000E62 RID: 3682
		RightParen,
		// Token: 0x04000E63 RID: 3683
		Dot,
		// Token: 0x04000E64 RID: 3684
		Comma,
		// Token: 0x04000E65 RID: 3685
		Not,
		// Token: 0x04000E66 RID: 3686
		And,
		// Token: 0x04000E67 RID: 3687
		Or,
		// Token: 0x04000E68 RID: 3688
		Minus,
		// Token: 0x04000E69 RID: 3689
		LastPunct,
		// Token: 0x04000E6A RID: 3690
		Invalid,
		// Token: 0x04000E6B RID: 3691
		ClosingCurlyBrace,
		// Token: 0x04000E6C RID: 3692
		Colon,
		// Token: 0x04000E6D RID: 3693
		Exclamation,
		// Token: 0x04000E6E RID: 3694
		Ampersand,
		// Token: 0x04000E6F RID: 3695
		Pipe
	}
}
