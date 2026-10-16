using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000CD RID: 205
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundGotoStatement : BoundStatement
	{
		// Token: 0x06001112 RID: 4370 RVA: 0x0005DAAF File Offset: 0x0005BCAF
		public BoundGotoStatement(SyntaxNode syntax, LabelSymbol label, bool hasErrors = false)
			: this(syntax, label, null, null, hasErrors)
		{
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x0005DABC File Offset: 0x0005BCBC
		public BoundGotoStatement(SyntaxNode syntax, LabelSymbol label, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression caseExpressionOpt, [global::System.Runtime.CompilerServices.Nullable(2)] BoundLabel labelExpressionOpt, bool hasErrors = false)
			: base(BoundKind.GotoStatement, syntax, hasErrors || caseExpressionOpt.HasErrors() || labelExpressionOpt.HasErrors())
		{
			this.Label = label;
			this.CaseExpressionOpt = caseExpressionOpt;
			this.LabelExpressionOpt = labelExpressionOpt;
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06001114 RID: 4372 RVA: 0x0005DAF3 File Offset: 0x0005BCF3
		public LabelSymbol Label { get; }

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06001115 RID: 4373 RVA: 0x0005DAFB File Offset: 0x0005BCFB
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundExpression CaseExpressionOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x0005DB03 File Offset: 0x0005BD03
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundLabel LabelExpressionOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x0005DB0B File Offset: 0x0005BD0B
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitGotoStatement(this);
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x0005DB14 File Offset: 0x0005BD14
		public BoundGotoStatement Update(LabelSymbol label, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression caseExpressionOpt, [global::System.Runtime.CompilerServices.Nullable(2)] BoundLabel labelExpressionOpt)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(label, this.Label) || caseExpressionOpt != this.CaseExpressionOpt || labelExpressionOpt != this.LabelExpressionOpt)
			{
				BoundGotoStatement boundGotoStatement = new BoundGotoStatement(this.Syntax, label, caseExpressionOpt, labelExpressionOpt, base.HasErrors);
				boundGotoStatement.CopyAttributes(this);
				return boundGotoStatement;
			}
			return this;
		}
	}
}
