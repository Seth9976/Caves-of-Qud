using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002EE RID: 750
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class InterpolationAlignmentClauseSyntax : CSharpSyntaxNode
	{
		// Token: 0x060041EA RID: 16874 RVA: 0x001103A9 File Offset: 0x0010E5A9
		internal InterpolationAlignmentClauseSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700110B RID: 4363
		// (get) Token: 0x060041EB RID: 16875 RVA: 0x001103B4 File Offset: 0x0010E5B4
		public SyntaxToken CommaToken
		{
			get
			{
				return new SyntaxToken(this, ((InterpolationAlignmentClauseSyntax)base.Green).commaToken, base.Position, 0);
			}
		}

		// Token: 0x1700110C RID: 4364
		// (get) Token: 0x060041EC RID: 16876 RVA: 0x001103D3 File Offset: 0x0010E5D3
		public ExpressionSyntax Value
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.value, 1);
			}
		}

		// Token: 0x060041ED RID: 16877 RVA: 0x001103E2 File Offset: 0x0010E5E2
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed<ExpressionSyntax>(ref this.value, 1);
		}

		// Token: 0x060041EE RID: 16878 RVA: 0x001103F7 File Offset: 0x0010E5F7
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.value;
		}

		// Token: 0x060041EF RID: 16879 RVA: 0x00110405 File Offset: 0x0010E605
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitInterpolationAlignmentClause(this);
		}

		// Token: 0x060041F0 RID: 16880 RVA: 0x0011040E File Offset: 0x0010E60E
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitInterpolationAlignmentClause(this);
		}

		// Token: 0x060041F1 RID: 16881 RVA: 0x00110418 File Offset: 0x0010E618
		public InterpolationAlignmentClauseSyntax Update(SyntaxToken commaToken, ExpressionSyntax value)
		{
			if (!(commaToken != this.CommaToken) && value == this.Value)
			{
				return this;
			}
			InterpolationAlignmentClauseSyntax interpolationAlignmentClauseSyntax = SyntaxFactory.InterpolationAlignmentClause(commaToken, value);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return interpolationAlignmentClauseSyntax;
			}
			return interpolationAlignmentClauseSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060041F2 RID: 16882 RVA: 0x0011045D File Offset: 0x0010E65D
		public InterpolationAlignmentClauseSyntax WithCommaToken(SyntaxToken commaToken)
		{
			return this.Update(commaToken, this.Value);
		}

		// Token: 0x060041F3 RID: 16883 RVA: 0x0011046C File Offset: 0x0010E66C
		public InterpolationAlignmentClauseSyntax WithValue(ExpressionSyntax value)
		{
			return this.Update(this.CommaToken, value);
		}

		// Token: 0x040014BE RID: 5310
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax value;
	}
}
