using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000193 RID: 403
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundThrowStatement : BoundStatement
	{
		// Token: 0x06001EA4 RID: 7844 RVA: 0x000A1115 File Offset: 0x0009F315
		public BoundThrowStatement(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression expressionOpt, bool hasErrors = false)
			: base(BoundKind.ThrowStatement, syntax, hasErrors || expressionOpt.HasErrors())
		{
			this.ExpressionOpt = expressionOpt;
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001EA5 RID: 7845 RVA: 0x000A1133 File Offset: 0x0009F333
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundExpression ExpressionOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x06001EA6 RID: 7846 RVA: 0x000A113B File Offset: 0x0009F33B
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitThrowStatement(this);
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x000A1144 File Offset: 0x0009F344
		public BoundThrowStatement Update([global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression expressionOpt)
		{
			if (expressionOpt != this.ExpressionOpt)
			{
				BoundThrowStatement boundThrowStatement = new BoundThrowStatement(this.Syntax, expressionOpt, base.HasErrors);
				boundThrowStatement.CopyAttributes(this);
				return boundThrowStatement;
			}
			return this;
		}
	}
}
