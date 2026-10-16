using System;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200013C RID: 316
	internal sealed class LazyObsoleteDiagnosticInfo : DiagnosticInfo
	{
		// Token: 0x0600191D RID: 6429 RVA: 0x000874AA File Offset: 0x000856AA
		internal LazyObsoleteDiagnosticInfo(object symbol, Symbol containingSymbol, BinderFlags binderFlags)
			: base(Microsoft.CodeAnalysis.CSharp.MessageProvider.Instance, -1)
		{
			this._symbolOrSymbolWithAnnotations = symbol;
			this._containingSymbol = containingSymbol;
			this._binderFlags = binderFlags;
			this._lazyActualObsoleteDiagnostic = null;
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x000874D4 File Offset: 0x000856D4
		internal override DiagnosticInfo GetResolvedInfo()
		{
			if (this._lazyActualObsoleteDiagnostic == null)
			{
				Symbol symbol = (this._symbolOrSymbolWithAnnotations as Symbol) ?? ((TypeWithAnnotations)this._symbolOrSymbolWithAnnotations).Type;
				symbol.ForceCompleteObsoleteAttribute();
				DiagnosticInfo diagnosticInfo = ((ObsoleteAttributeHelpers.GetObsoleteDiagnosticKind(symbol, this._containingSymbol, true) == ObsoleteDiagnosticKind.Diagnostic) ? ObsoleteAttributeHelpers.CreateObsoleteDiagnostic(symbol, this._binderFlags) : null);
				Interlocked.CompareExchange<DiagnosticInfo>(ref this._lazyActualObsoleteDiagnostic, diagnosticInfo ?? CSDiagnosticInfo.VoidDiagnosticInfo, null);
			}
			return this._lazyActualObsoleteDiagnostic;
		}

		// Token: 0x04000D09 RID: 3337
		private DiagnosticInfo _lazyActualObsoleteDiagnostic;

		// Token: 0x04000D0A RID: 3338
		private readonly object _symbolOrSymbolWithAnnotations;

		// Token: 0x04000D0B RID: 3339
		private readonly Symbol _containingSymbol;

		// Token: 0x04000D0C RID: 3340
		private readonly BinderFlags _binderFlags;
	}
}
