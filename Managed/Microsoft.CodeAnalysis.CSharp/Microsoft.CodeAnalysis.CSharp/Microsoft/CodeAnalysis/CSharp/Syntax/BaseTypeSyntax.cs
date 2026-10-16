using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000313 RID: 787
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public abstract class BaseTypeSyntax : CSharpSyntaxNode
	{
		// Token: 0x0600443F RID: 17471 RVA: 0x00114966 File Offset: 0x00112B66
		internal BaseTypeSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001192 RID: 4498
		// (get) Token: 0x06004440 RID: 17472
		public abstract TypeSyntax Type { get; }

		// Token: 0x06004441 RID: 17473 RVA: 0x00114971 File Offset: 0x00112B71
		public BaseTypeSyntax WithType(TypeSyntax type)
		{
			return this.WithTypeCore(type);
		}

		// Token: 0x06004442 RID: 17474
		internal abstract BaseTypeSyntax WithTypeCore(TypeSyntax type);
	}
}
