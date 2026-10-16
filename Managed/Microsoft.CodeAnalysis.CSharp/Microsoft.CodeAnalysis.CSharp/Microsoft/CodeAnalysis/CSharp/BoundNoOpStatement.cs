using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000191 RID: 401
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundNoOpStatement : BoundStatement
	{
		// Token: 0x06001E9B RID: 7835 RVA: 0x000A1064 File Offset: 0x0009F264
		public BoundNoOpStatement(SyntaxNode syntax, NoOpStatementFlavor flavor, bool hasErrors)
			: base(BoundKind.NoOpStatement, syntax, hasErrors)
		{
			this.Flavor = flavor;
		}

		// Token: 0x06001E9C RID: 7836 RVA: 0x000A1077 File Offset: 0x0009F277
		public BoundNoOpStatement(SyntaxNode syntax, NoOpStatementFlavor flavor)
			: base(BoundKind.NoOpStatement, syntax)
		{
			this.Flavor = flavor;
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001E9D RID: 7837 RVA: 0x000A1089 File Offset: 0x0009F289
		public NoOpStatementFlavor Flavor { get; }

		// Token: 0x06001E9E RID: 7838 RVA: 0x000A1091 File Offset: 0x0009F291
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitNoOpStatement(this);
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x000A109A File Offset: 0x0009F29A
		public BoundNoOpStatement Update(NoOpStatementFlavor flavor)
		{
			if (flavor != this.Flavor)
			{
				BoundNoOpStatement boundNoOpStatement = new BoundNoOpStatement(this.Syntax, flavor, base.HasErrors);
				boundNoOpStatement.CopyAttributes(this);
				return boundNoOpStatement;
			}
			return this;
		}
	}
}
