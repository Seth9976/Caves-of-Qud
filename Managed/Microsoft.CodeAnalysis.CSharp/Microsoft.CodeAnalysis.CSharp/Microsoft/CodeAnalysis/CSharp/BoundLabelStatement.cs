using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001A7 RID: 423
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundLabelStatement : BoundStatement
	{
		// Token: 0x06001F1B RID: 7963 RVA: 0x000A1E15 File Offset: 0x000A0015
		public BoundLabelStatement(SyntaxNode syntax, LabelSymbol label, bool hasErrors)
			: base(BoundKind.LabelStatement, syntax, hasErrors)
		{
			this.Label = label;
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x000A1E28 File Offset: 0x000A0028
		public BoundLabelStatement(SyntaxNode syntax, LabelSymbol label)
			: base(BoundKind.LabelStatement, syntax)
		{
			this.Label = label;
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001F1D RID: 7965 RVA: 0x000A1E3A File Offset: 0x000A003A
		public LabelSymbol Label { get; }

		// Token: 0x06001F1E RID: 7966 RVA: 0x000A1E42 File Offset: 0x000A0042
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitLabelStatement(this);
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x000A1E4B File Offset: 0x000A004B
		public BoundLabelStatement Update(LabelSymbol label)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(label, this.Label))
			{
				BoundLabelStatement boundLabelStatement = new BoundLabelStatement(this.Syntax, label, base.HasErrors);
				boundLabelStatement.CopyAttributes(this);
				return boundLabelStatement;
			}
			return this;
		}
	}
}
