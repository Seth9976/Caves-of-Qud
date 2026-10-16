using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000039 RID: 57
	internal enum LookupResultKind : byte
	{
		// Token: 0x040000AE RID: 174
		Empty,
		// Token: 0x040000AF RID: 175
		NotATypeOrNamespace,
		// Token: 0x040000B0 RID: 176
		NotAnAttributeType,
		// Token: 0x040000B1 RID: 177
		WrongArity,
		// Token: 0x040000B2 RID: 178
		NotCreatable,
		// Token: 0x040000B3 RID: 179
		Inaccessible,
		// Token: 0x040000B4 RID: 180
		NotReferencable,
		// Token: 0x040000B5 RID: 181
		NotAValue,
		// Token: 0x040000B6 RID: 182
		NotAVariable,
		// Token: 0x040000B7 RID: 183
		NotInvocable,
		// Token: 0x040000B8 RID: 184
		NotLabel,
		// Token: 0x040000B9 RID: 185
		StaticInstanceMismatch,
		// Token: 0x040000BA RID: 186
		OverloadResolutionFailure,
		// Token: 0x040000BB RID: 187
		Ambiguous,
		// Token: 0x040000BC RID: 188
		MemberGroup,
		// Token: 0x040000BD RID: 189
		Viable
	}
}
