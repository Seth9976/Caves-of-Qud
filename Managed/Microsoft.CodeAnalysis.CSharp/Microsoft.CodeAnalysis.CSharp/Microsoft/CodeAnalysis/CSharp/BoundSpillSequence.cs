using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000BD RID: 189
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundSpillSequence : BoundExpression
	{
		// Token: 0x06000D89 RID: 3465 RVA: 0x00056DC2 File Offset: 0x00054FC2
		public BoundSpillSequence(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> sideEffects, BoundExpression value, TypeSymbol type, bool hasErrors = false)
			: this(syntax, locals, BoundSpillSequence.MakeStatements(sideEffects), value, type, hasErrors)
		{
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00056DD8 File Offset: 0x00054FD8
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private static ImmutableArray<BoundStatement> MakeStatements([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> expressions)
		{
			return expressions.SelectAsArray<BoundExpression, BoundStatement>((BoundExpression expression) => new BoundExpressionStatement(expression.Syntax, expression, expression.HasErrors));
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x00056DFF File Offset: 0x00054FFF
		public BoundSpillSequence(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundStatement> sideEffects, BoundExpression value, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.SpillSequence, syntax, type, hasErrors || sideEffects.HasErrors<BoundStatement>() || value.HasErrors())
		{
			this.Locals = locals;
			this.SideEffects = sideEffects;
			this.Value = value;
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x00056E3B File Offset: 0x0005503B
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000D8D RID: 3469 RVA: 0x00056E43 File Offset: 0x00055043
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<LocalSymbol> Locals
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x00056E4B File Offset: 0x0005504B
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundStatement> SideEffects
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x00056E53 File Offset: 0x00055053
		public BoundExpression Value { get; }

		// Token: 0x06000D90 RID: 3472 RVA: 0x00056E5B File Offset: 0x0005505B
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitSpillSequence(this);
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x00056E64 File Offset: 0x00055064
		public BoundSpillSequence Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundStatement> sideEffects, BoundExpression value, TypeSymbol type)
		{
			if (locals != this.Locals || sideEffects != this.SideEffects || value != this.Value || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundSpillSequence boundSpillSequence = new BoundSpillSequence(this.Syntax, locals, sideEffects, value, type, base.HasErrors);
				boundSpillSequence.CopyAttributes(this);
				return boundSpillSequence;
			}
			return this;
		}
	}
}
