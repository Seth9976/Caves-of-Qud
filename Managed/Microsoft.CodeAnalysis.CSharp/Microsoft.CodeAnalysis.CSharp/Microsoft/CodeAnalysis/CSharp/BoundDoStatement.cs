using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200019B RID: 411
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundDoStatement : BoundConditionalLoopStatement
	{
		// Token: 0x06001ECB RID: 7883 RVA: 0x000A1474 File Offset: 0x0009F674
		public BoundDoStatement(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, BoundExpression condition, BoundStatement body, GeneratedLabelSymbol breakLabel, GeneratedLabelSymbol continueLabel, bool hasErrors = false)
			: base(BoundKind.DoStatement, syntax, locals, condition, body, breakLabel, continueLabel, hasErrors || condition.HasErrors() || body.HasErrors())
		{
		}

		// Token: 0x06001ECC RID: 7884 RVA: 0x000A14A8 File Offset: 0x0009F6A8
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitDoStatement(this);
		}

		// Token: 0x06001ECD RID: 7885 RVA: 0x000A14B4 File Offset: 0x0009F6B4
		public BoundDoStatement Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, BoundExpression condition, BoundStatement body, GeneratedLabelSymbol breakLabel, GeneratedLabelSymbol continueLabel)
		{
			if (locals != base.Locals || condition != base.Condition || body != base.Body || !SymbolEqualityComparer.ConsiderEverything.Equals(breakLabel, base.BreakLabel) || !SymbolEqualityComparer.ConsiderEverything.Equals(continueLabel, base.ContinueLabel))
			{
				BoundDoStatement boundDoStatement = new BoundDoStatement(this.Syntax, locals, condition, body, breakLabel, continueLabel, base.HasErrors);
				boundDoStatement.CopyAttributes(this);
				return boundDoStatement;
			}
			return this;
		}
	}
}
