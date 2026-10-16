using System;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200053E RID: 1342
	// (Invoke) Token: 0x06007124 RID: 28964
	internal delegate void ReportMismatchInReturnType<TArg>(BindingDiagnosticBag bag, MethodSymbol overriddenMethod, MethodSymbol overridingMethod, bool topLevel, TArg arg);
}
