using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003E3 RID: 995
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class BaseLoopOperation : Operation, ILoopOperation, IOperation
	{
		// Token: 0x06002618 RID: 9752 RVA: 0x0007375B File Offset: 0x0007195B
		protected BaseLoopOperation(IOperation body, [Nullable(new byte[] { 0, 1 })] ImmutableArray<ILocalSymbol> locals, ILabelSymbol continueLabel, ILabelSymbol exitLabel, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Body = Operation.SetParentOperation<IOperation>(body, this);
			this.Locals = locals;
			this.ContinueLabel = continueLabel;
			this.ExitLabel = exitLabel;
		}

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06002619 RID: 9753
		public abstract LoopKind LoopKind { get; }

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x0600261A RID: 9754 RVA: 0x0007378C File Offset: 0x0007198C
		public IOperation Body { get; }

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x0600261B RID: 9755 RVA: 0x00073794 File Offset: 0x00071994
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ILocalSymbol> Locals
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x0600261C RID: 9756 RVA: 0x0007379C File Offset: 0x0007199C
		public ILabelSymbol ContinueLabel { get; }

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x0600261D RID: 9757 RVA: 0x000737A4 File Offset: 0x000719A4
		public ILabelSymbol ExitLabel { get; }
	}
}
