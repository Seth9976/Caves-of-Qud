using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000CB RID: 203
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundReturnStatement : BoundStatement
	{
		// Token: 0x06001108 RID: 4360 RVA: 0x0005D9FE File Offset: 0x0005BBFE
		public static BoundReturnStatement Synthesized(SyntaxNode syntax, RefKind refKind, BoundExpression expression, bool hasErrors = false)
		{
			return new BoundReturnStatement(syntax, refKind, expression, hasErrors)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x0005DA10 File Offset: 0x0005BC10
		public BoundReturnStatement(SyntaxNode syntax, RefKind refKind, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression expressionOpt, bool hasErrors = false)
			: base(BoundKind.ReturnStatement, syntax, hasErrors || expressionOpt.HasErrors())
		{
			this.RefKind = refKind;
			this.ExpressionOpt = expressionOpt;
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x0600110A RID: 4362 RVA: 0x0005DA36 File Offset: 0x0005BC36
		public RefKind RefKind { get; }

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x0600110B RID: 4363 RVA: 0x0005DA3E File Offset: 0x0005BC3E
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundExpression ExpressionOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x0005DA46 File Offset: 0x0005BC46
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitReturnStatement(this);
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x0005DA4F File Offset: 0x0005BC4F
		public BoundReturnStatement Update(RefKind refKind, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression expressionOpt)
		{
			if (refKind != this.RefKind || expressionOpt != this.ExpressionOpt)
			{
				BoundReturnStatement boundReturnStatement = new BoundReturnStatement(this.Syntax, refKind, expressionOpt, base.HasErrors);
				boundReturnStatement.CopyAttributes(this);
				return boundReturnStatement;
			}
			return this;
		}
	}
}
