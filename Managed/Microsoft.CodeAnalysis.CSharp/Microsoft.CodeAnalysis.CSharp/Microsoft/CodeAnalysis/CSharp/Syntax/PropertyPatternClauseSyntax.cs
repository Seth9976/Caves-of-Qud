using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002E3 RID: 739
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class PropertyPatternClauseSyntax : CSharpSyntaxNode
	{
		// Token: 0x0600417E RID: 16766 RVA: 0x0010F922 File Offset: 0x0010DB22
		internal PropertyPatternClauseSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170010F4 RID: 4340
		// (get) Token: 0x0600417F RID: 16767 RVA: 0x0010F92D File Offset: 0x0010DB2D
		public SyntaxToken OpenBraceToken
		{
			get
			{
				return new SyntaxToken(this, ((PropertyPatternClauseSyntax)base.Green).openBraceToken, base.Position, 0);
			}
		}

		// Token: 0x170010F5 RID: 4341
		// (get) Token: 0x06004180 RID: 16768 RVA: 0x0010F94C File Offset: 0x0010DB4C
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<SubpatternSyntax> Subpatterns
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				SyntaxNode red = base.GetRed(ref this.subpatterns, 1);
				if (red == null)
				{
					return default(SeparatedSyntaxList<SubpatternSyntax>);
				}
				return new SeparatedSyntaxList<SubpatternSyntax>(red, base.GetChildIndex(1));
			}
		}

		// Token: 0x170010F6 RID: 4342
		// (get) Token: 0x06004181 RID: 16769 RVA: 0x0010F981 File Offset: 0x0010DB81
		public SyntaxToken CloseBraceToken
		{
			get
			{
				return new SyntaxToken(this, ((PropertyPatternClauseSyntax)base.Green).closeBraceToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x06004182 RID: 16770 RVA: 0x0010F9A7 File Offset: 0x0010DBA7
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed(ref this.subpatterns, 1);
		}

		// Token: 0x06004183 RID: 16771 RVA: 0x0010F9BC File Offset: 0x0010DBBC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.subpatterns;
		}

		// Token: 0x06004184 RID: 16772 RVA: 0x0010F9CA File Offset: 0x0010DBCA
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitPropertyPatternClause(this);
		}

		// Token: 0x06004185 RID: 16773 RVA: 0x0010F9D3 File Offset: 0x0010DBD3
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitPropertyPatternClause(this);
		}

		// Token: 0x06004186 RID: 16774 RVA: 0x0010F9DC File Offset: 0x0010DBDC
		public PropertyPatternClauseSyntax Update(SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<SubpatternSyntax> subpatterns, SyntaxToken closeBraceToken)
		{
			if (!(openBraceToken != this.OpenBraceToken) && !(subpatterns != this.Subpatterns) && !(closeBraceToken != this.CloseBraceToken))
			{
				return this;
			}
			PropertyPatternClauseSyntax propertyPatternClauseSyntax = SyntaxFactory.PropertyPatternClause(openBraceToken, subpatterns, closeBraceToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return propertyPatternClauseSyntax;
			}
			return propertyPatternClauseSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004187 RID: 16775 RVA: 0x0010FA35 File Offset: 0x0010DC35
		public PropertyPatternClauseSyntax WithOpenBraceToken(SyntaxToken openBraceToken)
		{
			return this.Update(openBraceToken, this.Subpatterns, this.CloseBraceToken);
		}

		// Token: 0x06004188 RID: 16776 RVA: 0x0010FA4A File Offset: 0x0010DC4A
		public PropertyPatternClauseSyntax WithSubpatterns([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<SubpatternSyntax> subpatterns)
		{
			return this.Update(this.OpenBraceToken, subpatterns, this.CloseBraceToken);
		}

		// Token: 0x06004189 RID: 16777 RVA: 0x0010FA5F File Offset: 0x0010DC5F
		public PropertyPatternClauseSyntax WithCloseBraceToken(SyntaxToken closeBraceToken)
		{
			return this.Update(this.OpenBraceToken, this.Subpatterns, closeBraceToken);
		}

		// Token: 0x0600418A RID: 16778 RVA: 0x0010FA74 File Offset: 0x0010DC74
		public PropertyPatternClauseSyntax AddSubpatterns(params SubpatternSyntax[] items)
		{
			return this.WithSubpatterns(this.Subpatterns.AddRange(items));
		}

		// Token: 0x040014B1 RID: 5297
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode subpatterns;
	}
}
