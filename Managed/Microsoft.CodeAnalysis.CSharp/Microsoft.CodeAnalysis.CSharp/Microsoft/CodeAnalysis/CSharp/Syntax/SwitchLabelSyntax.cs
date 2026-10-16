using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002FB RID: 763
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public abstract class SwitchLabelSyntax : CSharpSyntaxNode
	{
		// Token: 0x06004272 RID: 17010 RVA: 0x00110E46 File Offset: 0x0010F046
		internal SwitchLabelSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001127 RID: 4391
		// (get) Token: 0x06004273 RID: 17011
		public abstract SyntaxToken Keyword { get; }

		// Token: 0x06004274 RID: 17012 RVA: 0x00110E51 File Offset: 0x0010F051
		public SwitchLabelSyntax WithKeyword(SyntaxToken keyword)
		{
			return this.WithKeywordCore(keyword);
		}

		// Token: 0x06004275 RID: 17013
		internal abstract SwitchLabelSyntax WithKeywordCore(SyntaxToken keyword);

		// Token: 0x17001128 RID: 4392
		// (get) Token: 0x06004276 RID: 17014
		public abstract SyntaxToken ColonToken { get; }

		// Token: 0x06004277 RID: 17015 RVA: 0x00110E5A File Offset: 0x0010F05A
		public SwitchLabelSyntax WithColonToken(SyntaxToken colonToken)
		{
			return this.WithColonTokenCore(colonToken);
		}

		// Token: 0x06004278 RID: 17016
		internal abstract SwitchLabelSyntax WithColonTokenCore(SyntaxToken colonToken);
	}
}
