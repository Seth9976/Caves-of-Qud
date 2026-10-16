using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Emit
{
	// Token: 0x0200048B RID: 1163
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class LambdaSyntaxFacts
	{
		// Token: 0x06002DBF RID: 11711
		public abstract SyntaxNode GetLambda(SyntaxNode lambdaOrLambdaBodySyntax);

		// Token: 0x06002DC0 RID: 11712
		[return: Nullable(2)]
		public abstract SyntaxNode TryGetCorrespondingLambdaBody(SyntaxNode previousLambdaSyntax, SyntaxNode lambdaOrLambdaBodySyntax);

		// Token: 0x06002DC1 RID: 11713
		public abstract int GetDeclaratorPosition(SyntaxNode node);
	}
}
