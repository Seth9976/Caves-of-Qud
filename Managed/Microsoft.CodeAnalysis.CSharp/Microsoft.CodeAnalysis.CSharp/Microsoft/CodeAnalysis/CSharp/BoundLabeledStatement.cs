using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001A8 RID: 424
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundLabeledStatement : BoundStatement
	{
		// Token: 0x06001F20 RID: 7968 RVA: 0x000A1E7B File Offset: 0x000A007B
		public BoundLabeledStatement(SyntaxNode syntax, LabelSymbol label, BoundStatement body, bool hasErrors = false)
			: base(BoundKind.LabeledStatement, syntax, hasErrors || body.HasErrors())
		{
			this.Label = label;
			this.Body = body;
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001F21 RID: 7969 RVA: 0x000A1EA1 File Offset: 0x000A00A1
		public LabelSymbol Label { get; }

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001F22 RID: 7970 RVA: 0x000A1EA9 File Offset: 0x000A00A9
		public BoundStatement Body { get; }

		// Token: 0x06001F23 RID: 7971 RVA: 0x000A1EB1 File Offset: 0x000A00B1
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitLabeledStatement(this);
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x000A1EBA File Offset: 0x000A00BA
		public BoundLabeledStatement Update(LabelSymbol label, BoundStatement body)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(label, this.Label) || body != this.Body)
			{
				BoundLabeledStatement boundLabeledStatement = new BoundLabeledStatement(this.Syntax, label, body, base.HasErrors);
				boundLabeledStatement.CopyAttributes(this);
				return boundLabeledStatement;
			}
			return this;
		}
	}
}
