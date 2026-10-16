using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200019D RID: 413
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundForStatement : BoundLoopStatement
	{
		// Token: 0x06001ED1 RID: 7889 RVA: 0x000A15E4 File Offset: 0x0009F7E4
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public BoundForStatement(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> outerLocals, [global::System.Runtime.CompilerServices.Nullable(2)] BoundStatement initializer, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> innerLocals, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression condition, [global::System.Runtime.CompilerServices.Nullable(2)] BoundStatement increment, BoundStatement body, GeneratedLabelSymbol breakLabel, GeneratedLabelSymbol continueLabel, bool hasErrors = false)
			: base(BoundKind.ForStatement, syntax, breakLabel, continueLabel, hasErrors || initializer.HasErrors() || condition.HasErrors() || increment.HasErrors() || body.HasErrors())
		{
			this.OuterLocals = outerLocals;
			this.Initializer = initializer;
			this.InnerLocals = innerLocals;
			this.Condition = condition;
			this.Increment = increment;
			this.Body = body;
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001ED2 RID: 7890 RVA: 0x000A1654 File Offset: 0x0009F854
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<LocalSymbol> OuterLocals
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06001ED3 RID: 7891 RVA: 0x000A165C File Offset: 0x0009F85C
		public BoundStatement Initializer { get; }

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06001ED4 RID: 7892 RVA: 0x000A1664 File Offset: 0x0009F864
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<LocalSymbol> InnerLocals
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001ED5 RID: 7893 RVA: 0x000A166C File Offset: 0x0009F86C
		public BoundExpression Condition { get; }

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001ED6 RID: 7894 RVA: 0x000A1674 File Offset: 0x0009F874
		public BoundStatement Increment { get; }

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001ED7 RID: 7895 RVA: 0x000A167C File Offset: 0x0009F87C
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public BoundStatement Body
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get;
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x000A1684 File Offset: 0x0009F884
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitForStatement(this);
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x000A1690 File Offset: 0x0009F890
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public BoundForStatement Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> outerLocals, [global::System.Runtime.CompilerServices.Nullable(2)] BoundStatement initializer, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> innerLocals, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression condition, [global::System.Runtime.CompilerServices.Nullable(2)] BoundStatement increment, BoundStatement body, GeneratedLabelSymbol breakLabel, GeneratedLabelSymbol continueLabel)
		{
			if (outerLocals != this.OuterLocals || initializer != this.Initializer || innerLocals != this.InnerLocals || condition != this.Condition || increment != this.Increment || body != this.Body || !SymbolEqualityComparer.ConsiderEverything.Equals(breakLabel, base.BreakLabel) || !SymbolEqualityComparer.ConsiderEverything.Equals(continueLabel, base.ContinueLabel))
			{
				BoundForStatement boundForStatement = new BoundForStatement(this.Syntax, outerLocals, initializer, innerLocals, condition, increment, body, breakLabel, continueLabel, base.HasErrors);
				boundForStatement.CopyAttributes(this);
				return boundForStatement;
			}
			return this;
		}
	}
}
