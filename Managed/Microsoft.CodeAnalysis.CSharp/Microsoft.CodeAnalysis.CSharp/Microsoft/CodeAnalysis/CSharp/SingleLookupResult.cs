using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000071 RID: 113
	internal struct SingleLookupResult
	{
		// Token: 0x06000A88 RID: 2696 RVA: 0x0004F2F3 File Offset: 0x0004D4F3
		internal SingleLookupResult(LookupResultKind kind, Symbol symbol, DiagnosticInfo error)
		{
			this.Kind = kind;
			this.Symbol = symbol;
			this.Error = error;
		}

		// Token: 0x04000464 RID: 1124
		internal readonly LookupResultKind Kind;

		// Token: 0x04000465 RID: 1125
		internal readonly Symbol Symbol;

		// Token: 0x04000466 RID: 1126
		internal readonly DiagnosticInfo Error;
	}
}
