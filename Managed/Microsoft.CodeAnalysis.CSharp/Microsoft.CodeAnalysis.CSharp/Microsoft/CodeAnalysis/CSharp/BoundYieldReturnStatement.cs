using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000192 RID: 402
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundYieldReturnStatement : BoundStatement
	{
		// Token: 0x06001EA0 RID: 7840 RVA: 0x000A10C0 File Offset: 0x0009F2C0
		public BoundYieldReturnStatement(SyntaxNode syntax, BoundExpression expression, bool hasErrors = false)
			: base(BoundKind.YieldReturnStatement, syntax, hasErrors || expression.HasErrors())
		{
			this.Expression = expression;
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001EA1 RID: 7841 RVA: 0x000A10DE File Offset: 0x0009F2DE
		public BoundExpression Expression { get; }

		// Token: 0x06001EA2 RID: 7842 RVA: 0x000A10E6 File Offset: 0x0009F2E6
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitYieldReturnStatement(this);
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x000A10EF File Offset: 0x0009F2EF
		public BoundYieldReturnStatement Update(BoundExpression expression)
		{
			if (expression != this.Expression)
			{
				BoundYieldReturnStatement boundYieldReturnStatement = new BoundYieldReturnStatement(this.Syntax, expression, base.HasErrors);
				boundYieldReturnStatement.CopyAttributes(this);
				return boundYieldReturnStatement;
			}
			return this;
		}
	}
}
