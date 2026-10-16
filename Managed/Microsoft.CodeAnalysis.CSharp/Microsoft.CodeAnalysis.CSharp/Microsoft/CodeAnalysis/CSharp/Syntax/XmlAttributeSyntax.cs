using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200033B RID: 827
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public abstract class XmlAttributeSyntax : CSharpSyntaxNode
	{
		// Token: 0x06004640 RID: 17984 RVA: 0x00118100 File Offset: 0x00116300
		internal XmlAttributeSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170011FB RID: 4603
		// (get) Token: 0x06004641 RID: 17985
		public abstract XmlNameSyntax Name { get; }

		// Token: 0x06004642 RID: 17986 RVA: 0x0011810B File Offset: 0x0011630B
		public XmlAttributeSyntax WithName(XmlNameSyntax name)
		{
			return this.WithNameCore(name);
		}

		// Token: 0x06004643 RID: 17987
		internal abstract XmlAttributeSyntax WithNameCore(XmlNameSyntax name);

		// Token: 0x170011FC RID: 4604
		// (get) Token: 0x06004644 RID: 17988
		public abstract SyntaxToken EqualsToken { get; }

		// Token: 0x06004645 RID: 17989 RVA: 0x00118114 File Offset: 0x00116314
		public XmlAttributeSyntax WithEqualsToken(SyntaxToken equalsToken)
		{
			return this.WithEqualsTokenCore(equalsToken);
		}

		// Token: 0x06004646 RID: 17990
		internal abstract XmlAttributeSyntax WithEqualsTokenCore(SyntaxToken equalsToken);

		// Token: 0x170011FD RID: 4605
		// (get) Token: 0x06004647 RID: 17991
		public abstract SyntaxToken StartQuoteToken { get; }

		// Token: 0x06004648 RID: 17992 RVA: 0x0011811D File Offset: 0x0011631D
		public XmlAttributeSyntax WithStartQuoteToken(SyntaxToken startQuoteToken)
		{
			return this.WithStartQuoteTokenCore(startQuoteToken);
		}

		// Token: 0x06004649 RID: 17993
		internal abstract XmlAttributeSyntax WithStartQuoteTokenCore(SyntaxToken startQuoteToken);

		// Token: 0x170011FE RID: 4606
		// (get) Token: 0x0600464A RID: 17994
		public abstract SyntaxToken EndQuoteToken { get; }

		// Token: 0x0600464B RID: 17995 RVA: 0x00118126 File Offset: 0x00116326
		public XmlAttributeSyntax WithEndQuoteToken(SyntaxToken endQuoteToken)
		{
			return this.WithEndQuoteTokenCore(endQuoteToken);
		}

		// Token: 0x0600464C RID: 17996
		internal abstract XmlAttributeSyntax WithEndQuoteTokenCore(SyntaxToken endQuoteToken);
	}
}
