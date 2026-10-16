using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000329 RID: 809
	public abstract class CrefSyntax : CSharpSyntaxNode
	{
		// Token: 0x06004587 RID: 17799 RVA: 0x00116DAA File Offset: 0x00114FAA
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal CrefSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}
	}
}
