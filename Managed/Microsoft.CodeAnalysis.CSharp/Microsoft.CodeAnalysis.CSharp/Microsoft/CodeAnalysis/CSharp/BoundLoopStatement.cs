using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000199 RID: 409
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class BoundLoopStatement : BoundStatement
	{
		// Token: 0x06001EC3 RID: 7875 RVA: 0x000A13F0 File Offset: 0x0009F5F0
		protected BoundLoopStatement(BoundKind kind, SyntaxNode syntax, GeneratedLabelSymbol breakLabel, GeneratedLabelSymbol continueLabel, bool hasErrors)
			: base(kind, syntax, hasErrors)
		{
			this.BreakLabel = breakLabel;
			this.ContinueLabel = continueLabel;
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x000A140B File Offset: 0x0009F60B
		protected BoundLoopStatement(BoundKind kind, SyntaxNode syntax, GeneratedLabelSymbol breakLabel, GeneratedLabelSymbol continueLabel)
			: base(kind, syntax)
		{
			this.BreakLabel = breakLabel;
			this.ContinueLabel = continueLabel;
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001EC5 RID: 7877 RVA: 0x000A1424 File Offset: 0x0009F624
		public GeneratedLabelSymbol BreakLabel { get; }

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001EC6 RID: 7878 RVA: 0x000A142C File Offset: 0x0009F62C
		public GeneratedLabelSymbol ContinueLabel { get; }
	}
}
