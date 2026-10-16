using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002CA RID: 714
	public abstract class QueryClauseSyntax : CSharpSyntaxNode
	{
		// Token: 0x0600406F RID: 16495 RVA: 0x0010DA1A File Offset: 0x0010BC1A
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal QueryClauseSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}
	}
}
