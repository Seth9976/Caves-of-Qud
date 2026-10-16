using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002AC RID: 684
	public abstract class InstanceExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06003EF9 RID: 16121 RVA: 0x0010B2D1 File Offset: 0x001094D1
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal InstanceExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}
	}
}
