using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000317 RID: 791
	public abstract class TypeParameterConstraintSyntax : CSharpSyntaxNode
	{
		// Token: 0x06004467 RID: 17511 RVA: 0x00114D56 File Offset: 0x00112F56
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal TypeParameterConstraintSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}
	}
}
