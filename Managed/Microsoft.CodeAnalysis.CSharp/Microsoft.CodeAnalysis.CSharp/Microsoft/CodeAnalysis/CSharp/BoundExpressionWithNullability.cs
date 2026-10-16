using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000B0 RID: 176
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundExpressionWithNullability : BoundExpression
	{
		// Token: 0x06000D2D RID: 3373 RVA: 0x000562CA File Offset: 0x000544CA
		public BoundExpressionWithNullability(SyntaxNode syntax, BoundExpression expression, NullableAnnotation nullableAnnotation, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type)
			: this(syntax, expression, nullableAnnotation, type, false)
		{
			base.IsSuppressed = expression.IsSuppressed;
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x000562E4 File Offset: 0x000544E4
		public BoundExpressionWithNullability(SyntaxNode syntax, BoundExpression expression, NullableAnnotation nullableAnnotation, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.ExpressionWithNullability, syntax, type, hasErrors || expression.HasErrors())
		{
			this.Expression = expression;
			this.NullableAnnotation = nullableAnnotation;
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000D2F RID: 3375 RVA: 0x0005630F File Offset: 0x0005450F
		public BoundExpression Expression { get; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x00056317 File Offset: 0x00054517
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public new TypeSymbol Type
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.Type;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000D31 RID: 3377 RVA: 0x0005631F File Offset: 0x0005451F
		public NullableAnnotation NullableAnnotation { get; }

		// Token: 0x06000D32 RID: 3378 RVA: 0x00056327 File Offset: 0x00054527
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitExpressionWithNullability(this);
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x00056330 File Offset: 0x00054530
		public BoundExpressionWithNullability Update(BoundExpression expression, NullableAnnotation nullableAnnotation, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type)
		{
			if (expression != this.Expression || nullableAnnotation != this.NullableAnnotation || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundExpressionWithNullability boundExpressionWithNullability = new BoundExpressionWithNullability(this.Syntax, expression, nullableAnnotation, type, base.HasErrors);
				boundExpressionWithNullability.CopyAttributes(this);
				return boundExpressionWithNullability;
			}
			return this;
		}
	}
}
