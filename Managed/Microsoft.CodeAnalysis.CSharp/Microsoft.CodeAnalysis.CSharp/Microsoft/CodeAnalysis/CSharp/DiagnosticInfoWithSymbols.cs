using System;
using System.Collections.Immutable;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000136 RID: 310
	internal class DiagnosticInfoWithSymbols : DiagnosticInfo
	{
		// Token: 0x060018F8 RID: 6392 RVA: 0x0008560C File Offset: 0x0008380C
		internal DiagnosticInfoWithSymbols(ErrorCode errorCode, object[] arguments, ImmutableArray<Symbol> symbols)
			: base(Microsoft.CodeAnalysis.CSharp.MessageProvider.Instance, (int)errorCode, arguments)
		{
			this.Symbols = symbols;
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x00085622 File Offset: 0x00083822
		internal DiagnosticInfoWithSymbols(bool isWarningAsError, ErrorCode errorCode, object[] arguments, ImmutableArray<Symbol> symbols)
			: base(Microsoft.CodeAnalysis.CSharp.MessageProvider.Instance, isWarningAsError, (int)errorCode, arguments)
		{
			this.Symbols = symbols;
		}

		// Token: 0x040006E6 RID: 1766
		internal readonly ImmutableArray<Symbol> Symbols;
	}
}
