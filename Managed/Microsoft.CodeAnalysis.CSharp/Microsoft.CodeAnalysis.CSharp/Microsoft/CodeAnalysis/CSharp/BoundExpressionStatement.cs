using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000194 RID: 404
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundExpressionStatement : BoundStatement
	{
		// Token: 0x06001EA8 RID: 7848 RVA: 0x000A116A File Offset: 0x0009F36A
		public BoundExpressionStatement(SyntaxNode syntax, BoundExpression expression, bool hasErrors = false)
			: base(BoundKind.ExpressionStatement, syntax, hasErrors || expression.HasErrors())
		{
			this.Expression = expression;
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001EA9 RID: 7849 RVA: 0x000A1188 File Offset: 0x0009F388
		public BoundExpression Expression { get; }

		// Token: 0x06001EAA RID: 7850 RVA: 0x000A1190 File Offset: 0x0009F390
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitExpressionStatement(this);
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x000A1199 File Offset: 0x0009F399
		public BoundExpressionStatement Update(BoundExpression expression)
		{
			if (expression != this.Expression)
			{
				BoundExpressionStatement boundExpressionStatement = new BoundExpressionStatement(this.Syntax, expression, base.HasErrors);
				boundExpressionStatement.CopyAttributes(this);
				return boundExpressionStatement;
			}
			return this;
		}
	}
}
