using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200042F RID: 1071
	[NullableContext(2)]
	[Nullable(0)]
	internal abstract class BaseCaseClauseOperation : Operation, ICaseClauseOperation, IOperation
	{
		// Token: 0x06002919 RID: 10521 RVA: 0x00077FAA File Offset: 0x000761AA
		protected BaseCaseClauseOperation(ILabelSymbol label, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Label = label;
		}

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x0600291A RID: 10522
		public abstract CaseKind CaseKind { get; }

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x0600291B RID: 10523 RVA: 0x00077FBD File Offset: 0x000761BD
		public ILabelSymbol Label { get; }
	}
}
