using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001BB RID: 443
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundSequencePointExpression : BoundExpression
	{
		// Token: 0x06001F81 RID: 8065 RVA: 0x000A28D3 File Offset: 0x000A0AD3
		public BoundSequencePointExpression(SyntaxNode syntax, BoundExpression expression, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.SequencePointExpression, syntax, type, hasErrors || expression.HasErrors())
		{
			this.Expression = expression;
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001F82 RID: 8066 RVA: 0x000A28F6 File Offset: 0x000A0AF6
		public BoundExpression Expression { get; }

		// Token: 0x06001F83 RID: 8067 RVA: 0x000A28FE File Offset: 0x000A0AFE
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitSequencePointExpression(this);
		}

		// Token: 0x06001F84 RID: 8068 RVA: 0x000A2907 File Offset: 0x000A0B07
		public BoundSequencePointExpression Update(BoundExpression expression, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type)
		{
			if (expression != this.Expression || !TypeSymbol.Equals(type, base.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundSequencePointExpression boundSequencePointExpression = new BoundSequencePointExpression(this.Syntax, expression, type, base.HasErrors);
				boundSequencePointExpression.CopyAttributes(this);
				return boundSequencePointExpression;
			}
			return this;
		}
	}
}
