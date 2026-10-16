using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002CB RID: 715
	public abstract class SelectOrGroupClauseSyntax : CSharpSyntaxNode
	{
		// Token: 0x06004070 RID: 16496 RVA: 0x0010DA25 File Offset: 0x0010BC25
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal SelectOrGroupClauseSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}
	}
}
