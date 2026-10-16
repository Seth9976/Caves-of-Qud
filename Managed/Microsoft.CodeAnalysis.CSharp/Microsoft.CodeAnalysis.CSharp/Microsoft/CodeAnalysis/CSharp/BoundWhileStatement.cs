using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200019C RID: 412
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundWhileStatement : BoundConditionalLoopStatement
	{
		// Token: 0x06001ECE RID: 7886 RVA: 0x000A152C File Offset: 0x0009F72C
		public BoundWhileStatement(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, BoundExpression condition, BoundStatement body, GeneratedLabelSymbol breakLabel, GeneratedLabelSymbol continueLabel, bool hasErrors = false)
			: base(BoundKind.WhileStatement, syntax, locals, condition, body, breakLabel, continueLabel, hasErrors || condition.HasErrors() || body.HasErrors())
		{
		}

		// Token: 0x06001ECF RID: 7887 RVA: 0x000A1560 File Offset: 0x0009F760
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitWhileStatement(this);
		}

		// Token: 0x06001ED0 RID: 7888 RVA: 0x000A156C File Offset: 0x0009F76C
		public BoundWhileStatement Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, BoundExpression condition, BoundStatement body, GeneratedLabelSymbol breakLabel, GeneratedLabelSymbol continueLabel)
		{
			if (locals != base.Locals || condition != base.Condition || body != base.Body || !SymbolEqualityComparer.ConsiderEverything.Equals(breakLabel, base.BreakLabel) || !SymbolEqualityComparer.ConsiderEverything.Equals(continueLabel, base.ContinueLabel))
			{
				BoundWhileStatement boundWhileStatement = new BoundWhileStatement(this.Syntax, locals, condition, body, breakLabel, continueLabel, base.HasErrors);
				boundWhileStatement.CopyAttributes(this);
				return boundWhileStatement;
			}
			return this;
		}
	}
}
