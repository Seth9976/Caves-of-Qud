using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001B7 RID: 439
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundDagFieldEvaluation : BoundDagEvaluation
	{
		// Token: 0x06001F6E RID: 8046 RVA: 0x000A26AB File Offset: 0x000A08AB
		public BoundDagFieldEvaluation(SyntaxNode syntax, FieldSymbol field, BoundDagTemp input, bool hasErrors = false)
			: base(BoundKind.DagFieldEvaluation, syntax, input, hasErrors || input.HasErrors())
		{
			this.Field = field;
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001F6F RID: 8047 RVA: 0x000A26CB File Offset: 0x000A08CB
		public FieldSymbol Field { get; }

		// Token: 0x06001F70 RID: 8048 RVA: 0x000A26D3 File Offset: 0x000A08D3
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitDagFieldEvaluation(this);
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x000A26DC File Offset: 0x000A08DC
		public BoundDagFieldEvaluation Update(FieldSymbol field, BoundDagTemp input)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(field, this.Field) || input != base.Input)
			{
				BoundDagFieldEvaluation boundDagFieldEvaluation = new BoundDagFieldEvaluation(this.Syntax, field, input, base.HasErrors);
				boundDagFieldEvaluation.CopyAttributes(this);
				return boundDagFieldEvaluation;
			}
			return this;
		}
	}
}
