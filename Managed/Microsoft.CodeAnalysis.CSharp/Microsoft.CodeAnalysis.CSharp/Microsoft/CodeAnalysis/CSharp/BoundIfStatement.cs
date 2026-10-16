using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000198 RID: 408
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundIfStatement : BoundStatement
	{
		// Token: 0x06001EBD RID: 7869 RVA: 0x000A1356 File Offset: 0x0009F556
		public BoundIfStatement(SyntaxNode syntax, BoundExpression condition, BoundStatement consequence, [global::System.Runtime.CompilerServices.Nullable(2)] BoundStatement alternativeOpt, bool hasErrors = false)
			: base(BoundKind.IfStatement, syntax, hasErrors || condition.HasErrors() || consequence.HasErrors() || alternativeOpt.HasErrors())
		{
			this.Condition = condition;
			this.Consequence = consequence;
			this.AlternativeOpt = alternativeOpt;
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001EBE RID: 7870 RVA: 0x000A1395 File Offset: 0x0009F595
		public BoundExpression Condition { get; }

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001EBF RID: 7871 RVA: 0x000A139D File Offset: 0x0009F59D
		public BoundStatement Consequence { get; }

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001EC0 RID: 7872 RVA: 0x000A13A5 File Offset: 0x0009F5A5
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundStatement AlternativeOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x000A13AD File Offset: 0x0009F5AD
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitIfStatement(this);
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x000A13B6 File Offset: 0x0009F5B6
		public BoundIfStatement Update(BoundExpression condition, BoundStatement consequence, [global::System.Runtime.CompilerServices.Nullable(2)] BoundStatement alternativeOpt)
		{
			if (condition != this.Condition || consequence != this.Consequence || alternativeOpt != this.AlternativeOpt)
			{
				BoundIfStatement boundIfStatement = new BoundIfStatement(this.Syntax, condition, consequence, alternativeOpt, base.HasErrors);
				boundIfStatement.CopyAttributes(this);
				return boundIfStatement;
			}
			return this;
		}
	}
}
