using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002E2 RID: 738
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class PositionalPatternClauseSyntax : CSharpSyntaxNode
	{
		// Token: 0x06004171 RID: 16753 RVA: 0x0010F7AD File Offset: 0x0010D9AD
		internal PositionalPatternClauseSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170010F1 RID: 4337
		// (get) Token: 0x06004172 RID: 16754 RVA: 0x0010F7B8 File Offset: 0x0010D9B8
		public SyntaxToken OpenParenToken
		{
			get
			{
				return new SyntaxToken(this, ((PositionalPatternClauseSyntax)base.Green).openParenToken, base.Position, 0);
			}
		}

		// Token: 0x170010F2 RID: 4338
		// (get) Token: 0x06004173 RID: 16755 RVA: 0x0010F7D8 File Offset: 0x0010D9D8
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

		// Token: 0x170010F3 RID: 4339
		// (get) Token: 0x06004174 RID: 16756 RVA: 0x0010F80D File Offset: 0x0010DA0D
		public SyntaxToken CloseParenToken
		{
			get
			{
				return new SyntaxToken(this, ((PositionalPatternClauseSyntax)base.Green).closeParenToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x06004175 RID: 16757 RVA: 0x0010F833 File Offset: 0x0010DA33
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed(ref this.subpatterns, 1);
		}

		// Token: 0x06004176 RID: 16758 RVA: 0x0010F848 File Offset: 0x0010DA48
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.subpatterns;
		}

		// Token: 0x06004177 RID: 16759 RVA: 0x0010F856 File Offset: 0x0010DA56
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitPositionalPatternClause(this);
		}

		// Token: 0x06004178 RID: 16760 RVA: 0x0010F85F File Offset: 0x0010DA5F
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitPositionalPatternClause(this);
		}

		// Token: 0x06004179 RID: 16761 RVA: 0x0010F868 File Offset: 0x0010DA68
		public PositionalPatternClauseSyntax Update(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<SubpatternSyntax> subpatterns, SyntaxToken closeParenToken)
		{
			if (!(openParenToken != this.OpenParenToken) && !(subpatterns != this.Subpatterns) && !(closeParenToken != this.CloseParenToken))
			{
				return this;
			}
			PositionalPatternClauseSyntax positionalPatternClauseSyntax = SyntaxFactory.PositionalPatternClause(openParenToken, subpatterns, closeParenToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return positionalPatternClauseSyntax;
			}
			return positionalPatternClauseSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600417A RID: 16762 RVA: 0x0010F8C1 File Offset: 0x0010DAC1
		public PositionalPatternClauseSyntax WithOpenParenToken(SyntaxToken openParenToken)
		{
			return this.Update(openParenToken, this.Subpatterns, this.CloseParenToken);
		}

		// Token: 0x0600417B RID: 16763 RVA: 0x0010F8D6 File Offset: 0x0010DAD6
		public PositionalPatternClauseSyntax WithSubpatterns([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<SubpatternSyntax> subpatterns)
		{
			return this.Update(this.OpenParenToken, subpatterns, this.CloseParenToken);
		}

		// Token: 0x0600417C RID: 16764 RVA: 0x0010F8EB File Offset: 0x0010DAEB
		public PositionalPatternClauseSyntax WithCloseParenToken(SyntaxToken closeParenToken)
		{
			return this.Update(this.OpenParenToken, this.Subpatterns, closeParenToken);
		}

		// Token: 0x0600417D RID: 16765 RVA: 0x0010F900 File Offset: 0x0010DB00
		public PositionalPatternClauseSyntax AddSubpatterns(params SubpatternSyntax[] items)
		{
			return this.WithSubpatterns(this.Subpatterns.AddRange(items));
		}

		// Token: 0x040014B0 RID: 5296
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode subpatterns;
	}
}
