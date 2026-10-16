using System;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000139 RID: 313
	internal sealed class LazyArrayElementCantBeRefAnyDiagnosticInfo : LazyDiagnosticInfo
	{
		// Token: 0x06001911 RID: 6417 RVA: 0x000872C6 File Offset: 0x000854C6
		internal LazyArrayElementCantBeRefAnyDiagnosticInfo(TypeWithAnnotations possiblyRestrictedTypeSymbol)
		{
			this._possiblyRestrictedTypeSymbol = possiblyRestrictedTypeSymbol;
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x000872D5 File Offset: 0x000854D5
		protected override DiagnosticInfo ResolveInfo()
		{
			if (this._possiblyRestrictedTypeSymbol.IsRestrictedType(false))
			{
				return new CSDiagnosticInfo(ErrorCode.ERR_ArrayElementCantBeRefAny, new object[] { this._possiblyRestrictedTypeSymbol.Type });
			}
			return null;
		}

		// Token: 0x04000D05 RID: 3333
		private readonly TypeWithAnnotations _possiblyRestrictedTypeSymbol;
	}
}
