using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000174 RID: 372
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundArrayAccess : BoundExpression
	{
		// Token: 0x06001E00 RID: 7680 RVA: 0x000A0214 File Offset: 0x0009E414
		public BoundArrayAccess(SyntaxNode syntax, BoundExpression expression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> indices, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.ArrayAccess, syntax, type, hasErrors || expression.HasErrors() || indices.HasErrors<BoundExpression>())
		{
			this.Expression = expression;
			this.Indices = indices;
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001E01 RID: 7681 RVA: 0x000A0244 File Offset: 0x0009E444
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001E02 RID: 7682 RVA: 0x000A024C File Offset: 0x0009E44C
		public BoundExpression Expression { get; }

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001E03 RID: 7683 RVA: 0x000A0254 File Offset: 0x0009E454
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundExpression> Indices
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06001E04 RID: 7684 RVA: 0x000A025C File Offset: 0x0009E45C
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitArrayAccess(this);
		}

		// Token: 0x06001E05 RID: 7685 RVA: 0x000A0268 File Offset: 0x0009E468
		public BoundArrayAccess Update(BoundExpression expression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> indices, TypeSymbol type)
		{
			if (expression != this.Expression || indices != this.Indices || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundArrayAccess boundArrayAccess = new BoundArrayAccess(this.Syntax, expression, indices, type, base.HasErrors);
				boundArrayAccess.CopyAttributes(this);
				return boundArrayAccess;
			}
			return this;
		}
	}
}
