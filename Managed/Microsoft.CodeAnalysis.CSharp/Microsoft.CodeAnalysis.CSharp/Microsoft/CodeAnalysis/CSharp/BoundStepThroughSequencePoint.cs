using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200018A RID: 394
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundStepThroughSequencePoint : BoundStatement
	{
		// Token: 0x06001E7A RID: 7802 RVA: 0x000A0D08 File Offset: 0x0009EF08
		public BoundStepThroughSequencePoint(SyntaxNode syntax, TextSpan span, bool hasErrors)
			: base(BoundKind.StepThroughSequencePoint, syntax, hasErrors)
		{
			this.Span = span;
		}

		// Token: 0x06001E7B RID: 7803 RVA: 0x000A0D1B File Offset: 0x0009EF1B
		public BoundStepThroughSequencePoint(SyntaxNode syntax, TextSpan span)
			: base(BoundKind.StepThroughSequencePoint, syntax)
		{
			this.Span = span;
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001E7C RID: 7804 RVA: 0x000A0D2D File Offset: 0x0009EF2D
		public TextSpan Span { get; }

		// Token: 0x06001E7D RID: 7805 RVA: 0x000A0D35 File Offset: 0x0009EF35
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitStepThroughSequencePoint(this);
		}

		// Token: 0x06001E7E RID: 7806 RVA: 0x000A0D3E File Offset: 0x0009EF3E
		public BoundStepThroughSequencePoint Update(TextSpan span)
		{
			if (span != this.Span)
			{
				BoundStepThroughSequencePoint boundStepThroughSequencePoint = new BoundStepThroughSequencePoint(this.Syntax, span, base.HasErrors);
				boundStepThroughSequencePoint.CopyAttributes(this);
				return boundStepThroughSequencePoint;
			}
			return this;
		}
	}
}
