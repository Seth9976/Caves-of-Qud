using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000305 RID: 773
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ExternAliasDirectiveSyntax : CSharpSyntaxNode
	{
		// Token: 0x060042FA RID: 17146 RVA: 0x00111D3A File Offset: 0x0010FF3A
		internal ExternAliasDirectiveSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001149 RID: 4425
		// (get) Token: 0x060042FB RID: 17147 RVA: 0x00111D45 File Offset: 0x0010FF45
		public SyntaxToken ExternKeyword
		{
			get
			{
				return new SyntaxToken(this, ((ExternAliasDirectiveSyntax)base.Green).externKeyword, base.Position, 0);
			}
		}

		// Token: 0x1700114A RID: 4426
		// (get) Token: 0x060042FC RID: 17148 RVA: 0x00111D64 File Offset: 0x0010FF64
		public SyntaxToken AliasKeyword
		{
			get
			{
				return new SyntaxToken(this, ((ExternAliasDirectiveSyntax)base.Green).aliasKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x1700114B RID: 4427
		// (get) Token: 0x060042FD RID: 17149 RVA: 0x00111D8A File Offset: 0x0010FF8A
		public SyntaxToken Identifier
		{
			get
			{
				return new SyntaxToken(this, ((ExternAliasDirectiveSyntax)base.Green).identifier, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x1700114C RID: 4428
		// (get) Token: 0x060042FE RID: 17150 RVA: 0x00111DB0 File Offset: 0x0010FFB0
		public SyntaxToken SemicolonToken
		{
			get
			{
				return new SyntaxToken(this, ((ExternAliasDirectiveSyntax)base.Green).semicolonToken, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x060042FF RID: 17151 RVA: 0x00111DD6 File Offset: 0x0010FFD6
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x06004300 RID: 17152 RVA: 0x00111DD9 File Offset: 0x0010FFD9
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x06004301 RID: 17153 RVA: 0x00111DDC File Offset: 0x0010FFDC
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitExternAliasDirective(this);
		}

		// Token: 0x06004302 RID: 17154 RVA: 0x00111DE5 File Offset: 0x0010FFE5
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitExternAliasDirective(this);
		}

		// Token: 0x06004303 RID: 17155 RVA: 0x00111DF0 File Offset: 0x0010FFF0
		public ExternAliasDirectiveSyntax Update(SyntaxToken externKeyword, SyntaxToken aliasKeyword, SyntaxToken identifier, SyntaxToken semicolonToken)
		{
			if (!(externKeyword != this.ExternKeyword) && !(aliasKeyword != this.AliasKeyword) && !(identifier != this.Identifier) && !(semicolonToken != this.SemicolonToken))
			{
				return this;
			}
			ExternAliasDirectiveSyntax externAliasDirectiveSyntax = SyntaxFactory.ExternAliasDirective(externKeyword, aliasKeyword, identifier, semicolonToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return externAliasDirectiveSyntax;
			}
			return externAliasDirectiveSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004304 RID: 17156 RVA: 0x00111E5A File Offset: 0x0011005A
		public ExternAliasDirectiveSyntax WithExternKeyword(SyntaxToken externKeyword)
		{
			return this.Update(externKeyword, this.AliasKeyword, this.Identifier, this.SemicolonToken);
		}

		// Token: 0x06004305 RID: 17157 RVA: 0x00111E75 File Offset: 0x00110075
		public ExternAliasDirectiveSyntax WithAliasKeyword(SyntaxToken aliasKeyword)
		{
			return this.Update(this.ExternKeyword, aliasKeyword, this.Identifier, this.SemicolonToken);
		}

		// Token: 0x06004306 RID: 17158 RVA: 0x00111E90 File Offset: 0x00110090
		public ExternAliasDirectiveSyntax WithIdentifier(SyntaxToken identifier)
		{
			return this.Update(this.ExternKeyword, this.AliasKeyword, identifier, this.SemicolonToken);
		}

		// Token: 0x06004307 RID: 17159 RVA: 0x00111EAB File Offset: 0x001100AB
		public ExternAliasDirectiveSyntax WithSemicolonToken(SyntaxToken semicolonToken)
		{
			return this.Update(this.ExternKeyword, this.AliasKeyword, this.Identifier, semicolonToken);
		}
	}
}
