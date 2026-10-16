using System;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000512 RID: 1298
	internal static class RefKindExtensions
	{
		// Token: 0x06006D1E RID: 27934 RVA: 0x001926FE File Offset: 0x001908FE
		public static bool IsManagedReference(this RefKind refKind)
		{
			return refKind > RefKind.None;
		}

		// Token: 0x06006D1F RID: 27935 RVA: 0x00192704 File Offset: 0x00190904
		public static RefKind GetRefKind(this SyntaxKind syntaxKind)
		{
			if (syntaxKind == SyntaxKind.None)
			{
				return RefKind.None;
			}
			switch (syntaxKind)
			{
			case SyntaxKind.RefKeyword:
				return RefKind.Ref;
			case SyntaxKind.OutKeyword:
				return RefKind.Out;
			case SyntaxKind.InKeyword:
				return RefKind.In;
			default:
				throw ExceptionUtilities.UnexpectedValue(syntaxKind);
			}
		}

		// Token: 0x06006D20 RID: 27936 RVA: 0x00192736 File Offset: 0x00190936
		public static bool IsWritableReference(this RefKind refKind)
		{
			switch (refKind)
			{
			case RefKind.None:
			case RefKind.In:
				return false;
			case RefKind.Ref:
			case RefKind.Out:
				return true;
			default:
				throw ExceptionUtilities.UnexpectedValue(refKind);
			}
		}
	}
}
