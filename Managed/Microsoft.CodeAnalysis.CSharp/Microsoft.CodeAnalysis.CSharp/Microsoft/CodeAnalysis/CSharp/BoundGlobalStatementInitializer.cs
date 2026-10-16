using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000164 RID: 356
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundGlobalStatementInitializer : BoundInitializer
	{
		// Token: 0x06001DA8 RID: 7592 RVA: 0x0009FA98 File Offset: 0x0009DC98
		public BoundGlobalStatementInitializer(SyntaxNode syntax, BoundStatement statement, bool hasErrors = false)
			: base(BoundKind.GlobalStatementInitializer, syntax, hasErrors || statement.HasErrors())
		{
			this.Statement = statement;
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001DA9 RID: 7593 RVA: 0x0009FAB5 File Offset: 0x0009DCB5
		public BoundStatement Statement { get; }

		// Token: 0x06001DAA RID: 7594 RVA: 0x0009FABD File Offset: 0x0009DCBD
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitGlobalStatementInitializer(this);
		}

		// Token: 0x06001DAB RID: 7595 RVA: 0x0009FAC6 File Offset: 0x0009DCC6
		public BoundGlobalStatementInitializer Update(BoundStatement statement)
		{
			if (statement != this.Statement)
			{
				BoundGlobalStatementInitializer boundGlobalStatementInitializer = new BoundGlobalStatementInitializer(this.Syntax, statement, base.HasErrors);
				boundGlobalStatementInitializer.CopyAttributes(this);
				return boundGlobalStatementInitializer;
			}
			return this;
		}
	}
}
