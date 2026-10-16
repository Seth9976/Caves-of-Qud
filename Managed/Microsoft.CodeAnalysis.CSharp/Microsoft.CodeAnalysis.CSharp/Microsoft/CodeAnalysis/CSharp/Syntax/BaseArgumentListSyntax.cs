using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002B9 RID: 697
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public abstract class BaseArgumentListSyntax : CSharpSyntaxNode
	{
		// Token: 0x06003F8E RID: 16270 RVA: 0x0010C2D6 File Offset: 0x0010A4D6
		internal BaseArgumentListSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001081 RID: 4225
		// (get) Token: 0x06003F8F RID: 16271
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public abstract SeparatedSyntaxList<ArgumentSyntax> Arguments
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06003F90 RID: 16272 RVA: 0x0010C2E1 File Offset: 0x0010A4E1
		public BaseArgumentListSyntax WithArguments([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ArgumentSyntax> arguments)
		{
			return this.WithArgumentsCore(arguments);
		}

		// Token: 0x06003F91 RID: 16273
		internal abstract BaseArgumentListSyntax WithArgumentsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ArgumentSyntax> arguments);

		// Token: 0x06003F92 RID: 16274 RVA: 0x0010C2EA File Offset: 0x0010A4EA
		public BaseArgumentListSyntax AddArguments(params ArgumentSyntax[] items)
		{
			return this.AddArgumentsCore(items);
		}

		// Token: 0x06003F93 RID: 16275
		internal abstract BaseArgumentListSyntax AddArgumentsCore(params ArgumentSyntax[] items);
	}
}
