using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002EB RID: 747
	public abstract class InterpolatedStringContentSyntax : CSharpSyntaxNode
	{
		// Token: 0x060041D1 RID: 16849 RVA: 0x001100DB File Offset: 0x0010E2DB
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal InterpolatedStringContentSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}
	}
}
