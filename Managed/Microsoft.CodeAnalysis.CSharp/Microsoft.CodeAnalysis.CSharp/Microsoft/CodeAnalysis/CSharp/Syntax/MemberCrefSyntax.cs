using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200032C RID: 812
	public abstract class MemberCrefSyntax : CrefSyntax
	{
		// Token: 0x0600459C RID: 17820 RVA: 0x00116F96 File Offset: 0x00115196
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal MemberCrefSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}
	}
}
