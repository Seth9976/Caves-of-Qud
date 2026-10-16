using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200029C RID: 668
	public abstract class ExpressionOrPatternSyntax : CSharpSyntaxNode
	{
		// Token: 0x06003E58 RID: 15960 RVA: 0x0010A240 File Offset: 0x00108440
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal ExpressionOrPatternSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}
	}
}
