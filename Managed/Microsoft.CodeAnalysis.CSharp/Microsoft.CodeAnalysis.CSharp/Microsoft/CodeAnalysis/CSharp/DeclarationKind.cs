using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000122 RID: 290
	internal enum DeclarationKind : byte
	{
		// Token: 0x04000692 RID: 1682
		Namespace,
		// Token: 0x04000693 RID: 1683
		Class,
		// Token: 0x04000694 RID: 1684
		Interface,
		// Token: 0x04000695 RID: 1685
		Struct,
		// Token: 0x04000696 RID: 1686
		Enum,
		// Token: 0x04000697 RID: 1687
		Delegate,
		// Token: 0x04000698 RID: 1688
		Script,
		// Token: 0x04000699 RID: 1689
		Submission,
		// Token: 0x0400069A RID: 1690
		ImplicitClass,
		// Token: 0x0400069B RID: 1691
		SimpleProgram,
		// Token: 0x0400069C RID: 1692
		Record,
		// Token: 0x0400069D RID: 1693
		RecordStruct
	}
}
