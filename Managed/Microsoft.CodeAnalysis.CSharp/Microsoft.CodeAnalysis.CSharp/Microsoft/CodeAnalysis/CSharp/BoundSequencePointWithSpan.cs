using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000187 RID: 391
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundSequencePointWithSpan : BoundStatement
	{
		// Token: 0x06001E6B RID: 7787 RVA: 0x000A0BDC File Offset: 0x0009EDDC
		public BoundSequencePointWithSpan(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] BoundStatement statementOpt, TextSpan span, bool hasErrors = false)
			: base(BoundKind.SequencePointWithSpan, syntax, hasErrors || statementOpt.HasErrors())
		{
			this.StatementOpt = statementOpt;
			this.Span = span;
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001E6C RID: 7788 RVA: 0x000A0C02 File Offset: 0x0009EE02
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundStatement StatementOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001E6D RID: 7789 RVA: 0x000A0C0A File Offset: 0x0009EE0A
		public TextSpan Span { get; }

		// Token: 0x06001E6E RID: 7790 RVA: 0x000A0C12 File Offset: 0x0009EE12
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitSequencePointWithSpan(this);
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x000A0C1B File Offset: 0x0009EE1B
		public BoundSequencePointWithSpan Update([global::System.Runtime.CompilerServices.Nullable(2)] BoundStatement statementOpt, TextSpan span)
		{
			if (statementOpt != this.StatementOpt || span != this.Span)
			{
				BoundSequencePointWithSpan boundSequencePointWithSpan = new BoundSequencePointWithSpan(this.Syntax, statementOpt, span, base.HasErrors);
				boundSequencePointWithSpan.CopyAttributes(this);
				return boundSequencePointWithSpan;
			}
			return this;
		}
	}
}
