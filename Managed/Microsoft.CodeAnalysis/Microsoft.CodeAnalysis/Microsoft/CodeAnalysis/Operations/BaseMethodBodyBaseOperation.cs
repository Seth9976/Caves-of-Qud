using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200043C RID: 1084
	[NullableContext(2)]
	[Nullable(0)]
	internal abstract class BaseMethodBodyBaseOperation : Operation, IMethodBodyBaseOperation, IOperation
	{
		// Token: 0x06002988 RID: 10632 RVA: 0x0007882D File Offset: 0x00076A2D
		protected BaseMethodBodyBaseOperation(IBlockOperation blockBody, IBlockOperation expressionBody, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.BlockBody = Operation.SetParentOperation<IBlockOperation>(blockBody, this);
			this.ExpressionBody = Operation.SetParentOperation<IBlockOperation>(expressionBody, this);
		}

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x06002989 RID: 10633 RVA: 0x00078854 File Offset: 0x00076A54
		public IBlockOperation BlockBody { get; }

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x0600298A RID: 10634 RVA: 0x0007885C File Offset: 0x00076A5C
		public IBlockOperation ExpressionBody { get; }
	}
}
