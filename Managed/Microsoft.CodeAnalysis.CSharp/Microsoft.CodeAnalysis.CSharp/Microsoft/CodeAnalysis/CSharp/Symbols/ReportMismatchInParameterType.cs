using System;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200053F RID: 1343
	// (Invoke) Token: 0x06007128 RID: 28968
	internal delegate void ReportMismatchInParameterType<TArg>(BindingDiagnosticBag bag, MethodSymbol overriddenMethod, MethodSymbol overridingMethod, ParameterSymbol parameter, bool topLevel, TArg arg);
}
