using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002F4 RID: 756
	public abstract class VariableDesignationSyntax : CSharpSyntaxNode
	{
		// Token: 0x06004226 RID: 16934 RVA: 0x00110923 File Offset: 0x0010EB23
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal VariableDesignationSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}
	}
}
