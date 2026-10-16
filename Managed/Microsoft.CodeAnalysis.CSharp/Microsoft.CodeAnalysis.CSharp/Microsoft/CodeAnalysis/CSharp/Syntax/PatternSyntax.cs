using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002DD RID: 733
	public abstract class PatternSyntax : ExpressionOrPatternSyntax
	{
		// Token: 0x06004144 RID: 16708 RVA: 0x0010F2F3 File Offset: 0x0010D4F3
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal PatternSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}
	}
}
