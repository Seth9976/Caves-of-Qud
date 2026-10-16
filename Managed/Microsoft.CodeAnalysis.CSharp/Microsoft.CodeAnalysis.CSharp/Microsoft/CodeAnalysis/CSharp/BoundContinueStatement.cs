using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000196 RID: 406
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundContinueStatement : BoundStatement
	{
		// Token: 0x06001EB1 RID: 7857 RVA: 0x000A1225 File Offset: 0x0009F425
		public BoundContinueStatement(SyntaxNode syntax, GeneratedLabelSymbol label, bool hasErrors)
			: base(BoundKind.ContinueStatement, syntax, hasErrors)
		{
			this.Label = label;
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x000A1238 File Offset: 0x0009F438
		public BoundContinueStatement(SyntaxNode syntax, GeneratedLabelSymbol label)
			: base(BoundKind.ContinueStatement, syntax)
		{
			this.Label = label;
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001EB3 RID: 7859 RVA: 0x000A124A File Offset: 0x0009F44A
		public GeneratedLabelSymbol Label { get; }

		// Token: 0x06001EB4 RID: 7860 RVA: 0x000A1252 File Offset: 0x0009F452
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitContinueStatement(this);
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x000A125B File Offset: 0x0009F45B
		public BoundContinueStatement Update(GeneratedLabelSymbol label)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(label, this.Label))
			{
				BoundContinueStatement boundContinueStatement = new BoundContinueStatement(this.Syntax, label, base.HasErrors);
				boundContinueStatement.CopyAttributes(this);
				return boundContinueStatement;
			}
			return this;
		}
	}
}
