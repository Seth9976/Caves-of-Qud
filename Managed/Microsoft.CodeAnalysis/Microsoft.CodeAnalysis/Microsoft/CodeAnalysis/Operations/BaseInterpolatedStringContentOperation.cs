using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000435 RID: 1077
	internal abstract class BaseInterpolatedStringContentOperation : Operation, IInterpolatedStringContentOperation, IOperation
	{
		// Token: 0x06002951 RID: 10577 RVA: 0x000783A7 File Offset: 0x000765A7
		[NullableContext(1)]
		protected BaseInterpolatedStringContentOperation([Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
		}
	}
}
