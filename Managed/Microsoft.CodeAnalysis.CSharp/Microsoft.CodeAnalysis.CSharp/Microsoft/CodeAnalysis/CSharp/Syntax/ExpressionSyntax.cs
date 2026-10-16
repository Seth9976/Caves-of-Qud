using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200029D RID: 669
	public abstract class ExpressionSyntax : ExpressionOrPatternSyntax
	{
		// Token: 0x06003E59 RID: 15961 RVA: 0x0010A24B File Offset: 0x0010844B
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal ExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}
	}
}
