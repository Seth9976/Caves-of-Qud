using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200013E RID: 318
	internal sealed class LazyUseSiteDiagnosticsInfoForNullableType : LazyDiagnosticInfo
	{
		// Token: 0x06001921 RID: 6433 RVA: 0x000875DB File Offset: 0x000857DB
		internal LazyUseSiteDiagnosticsInfoForNullableType(LanguageVersion languageVersion, TypeWithAnnotations possiblyNullableTypeSymbol)
		{
			this._languageVersion = languageVersion;
			this._possiblyNullableTypeSymbol = possiblyNullableTypeSymbol;
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x000875F1 File Offset: 0x000857F1
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		protected override DiagnosticInfo ResolveInfo()
		{
			if (this._possiblyNullableTypeSymbol.IsNullableType())
			{
				return this._possiblyNullableTypeSymbol.Type.OriginalDefinition.GetUseSiteInfo().DiagnosticInfo;
			}
			return Binder.GetNullableUnconstrainedTypeParameterDiagnosticIfNecessary(this._languageVersion, in this._possiblyNullableTypeSymbol);
		}

		// Token: 0x04000D10 RID: 3344
		private readonly LanguageVersion _languageVersion;

		// Token: 0x04000D11 RID: 3345
		private readonly TypeWithAnnotations _possiblyNullableTypeSymbol;
	}
}
