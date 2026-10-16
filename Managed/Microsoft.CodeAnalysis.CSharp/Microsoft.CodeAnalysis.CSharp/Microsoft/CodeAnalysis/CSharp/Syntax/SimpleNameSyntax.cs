using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200027C RID: 636
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public abstract class SimpleNameSyntax : NameSyntax
	{
		// Token: 0x06003C9C RID: 15516 RVA: 0x00105B2D File Offset: 0x00103D2D
		internal sealed override SimpleNameSyntax GetUnqualifiedName()
		{
			return this;
		}

		// Token: 0x06003C9D RID: 15517 RVA: 0x00105B30 File Offset: 0x00103D30
		internal SimpleNameSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000FDE RID: 4062
		// (get) Token: 0x06003C9E RID: 15518
		public abstract SyntaxToken Identifier { get; }

		// Token: 0x06003C9F RID: 15519 RVA: 0x00105B3B File Offset: 0x00103D3B
		public SimpleNameSyntax WithIdentifier(SyntaxToken identifier)
		{
			return this.WithIdentifierCore(identifier);
		}

		// Token: 0x06003CA0 RID: 15520
		internal abstract SimpleNameSyntax WithIdentifierCore(SyntaxToken identifier);
	}
}
