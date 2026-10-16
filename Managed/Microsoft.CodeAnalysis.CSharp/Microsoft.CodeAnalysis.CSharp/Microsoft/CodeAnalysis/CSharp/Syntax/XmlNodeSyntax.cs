using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000334 RID: 820
	public abstract class XmlNodeSyntax : CSharpSyntaxNode
	{
		// Token: 0x060045F3 RID: 17907 RVA: 0x0011788A File Offset: 0x00115A8A
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal XmlNodeSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}
	}
}
