using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.CodeAnalysis.CSharp.Emit
{
	// Token: 0x02000481 RID: 1153
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class CSharpLambdaSyntaxFacts : LambdaSyntaxFacts
	{
		// Token: 0x0600606B RID: 24683 RVA: 0x00174CBC File Offset: 0x00172EBC
		private CSharpLambdaSyntaxFacts()
		{
		}

		// Token: 0x0600606C RID: 24684 RVA: 0x00174CC4 File Offset: 0x00172EC4
		public override SyntaxNode GetLambda(SyntaxNode lambdaOrLambdaBodySyntax)
		{
			return LambdaUtilities.GetLambda(lambdaOrLambdaBodySyntax);
		}

		// Token: 0x0600606D RID: 24685 RVA: 0x00174CCC File Offset: 0x00172ECC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode TryGetCorrespondingLambdaBody(SyntaxNode previousLambdaSyntax, SyntaxNode lambdaOrLambdaBodySyntax)
		{
			return LambdaUtilities.TryGetCorrespondingLambdaBody(lambdaOrLambdaBodySyntax, previousLambdaSyntax);
		}

		// Token: 0x0600606E RID: 24686 RVA: 0x00174CD5 File Offset: 0x00172ED5
		public override int GetDeclaratorPosition(SyntaxNode node)
		{
			return LambdaUtilities.GetDeclaratorPosition(node);
		}

		// Token: 0x0400194D RID: 6477
		public static readonly LambdaSyntaxFacts Instance = new CSharpLambdaSyntaxFacts();
	}
}
