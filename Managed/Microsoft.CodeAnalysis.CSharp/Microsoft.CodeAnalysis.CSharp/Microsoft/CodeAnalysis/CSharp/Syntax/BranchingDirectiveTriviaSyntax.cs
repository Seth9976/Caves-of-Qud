using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000343 RID: 835
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public abstract class BranchingDirectiveTriviaSyntax : DirectiveTriviaSyntax
	{
		// Token: 0x060046BC RID: 18108 RVA: 0x00118D6E File Offset: 0x00116F6E
		internal BranchingDirectiveTriviaSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001219 RID: 4633
		// (get) Token: 0x060046BD RID: 18109
		public abstract bool BranchTaken { get; }

		// Token: 0x060046BE RID: 18110 RVA: 0x00118D79 File Offset: 0x00116F79
		public new BranchingDirectiveTriviaSyntax WithHashToken(SyntaxToken hashToken)
		{
			return (BranchingDirectiveTriviaSyntax)this.WithHashTokenCore(hashToken);
		}

		// Token: 0x060046BF RID: 18111 RVA: 0x00118D87 File Offset: 0x00116F87
		public new BranchingDirectiveTriviaSyntax WithEndOfDirectiveToken(SyntaxToken endOfDirectiveToken)
		{
			return (BranchingDirectiveTriviaSyntax)this.WithEndOfDirectiveTokenCore(endOfDirectiveToken);
		}
	}
}
