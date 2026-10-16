using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200016C RID: 364
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundExtractedFinallyBlock : BoundStatement
	{
		// Token: 0x06001DCF RID: 7631 RVA: 0x0009FD7A File Offset: 0x0009DF7A
		public BoundExtractedFinallyBlock(SyntaxNode syntax, BoundBlock finallyBlock, bool hasErrors = false)
			: base(BoundKind.ExtractedFinallyBlock, syntax, hasErrors || finallyBlock.HasErrors())
		{
			this.FinallyBlock = finallyBlock;
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001DD0 RID: 7632 RVA: 0x0009FD98 File Offset: 0x0009DF98
		public BoundBlock FinallyBlock { get; }

		// Token: 0x06001DD1 RID: 7633 RVA: 0x0009FDA0 File Offset: 0x0009DFA0
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitExtractedFinallyBlock(this);
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x0009FDA9 File Offset: 0x0009DFA9
		public BoundExtractedFinallyBlock Update(BoundBlock finallyBlock)
		{
			if (finallyBlock != this.FinallyBlock)
			{
				BoundExtractedFinallyBlock boundExtractedFinallyBlock = new BoundExtractedFinallyBlock(this.Syntax, finallyBlock, base.HasErrors);
				boundExtractedFinallyBlock.CopyAttributes(this);
				return boundExtractedFinallyBlock;
			}
			return this;
		}
	}
}
