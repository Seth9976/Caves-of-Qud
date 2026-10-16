using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002ED RID: 749
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class InterpolationSyntax : InterpolatedStringContentSyntax
	{
		// Token: 0x060041DA RID: 16858 RVA: 0x0011016C File Offset: 0x0010E36C
		internal InterpolationSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001106 RID: 4358
		// (get) Token: 0x060041DB RID: 16859 RVA: 0x00110177 File Offset: 0x0010E377
		public SyntaxToken OpenBraceToken
		{
			get
			{
				return new SyntaxToken(this, ((InterpolationSyntax)base.Green).openBraceToken, base.Position, 0);
			}
		}

		// Token: 0x17001107 RID: 4359
		// (get) Token: 0x060041DC RID: 16860 RVA: 0x00110196 File Offset: 0x0010E396
		public ExpressionSyntax Expression
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.expression, 1);
			}
		}

		// Token: 0x17001108 RID: 4360
		// (get) Token: 0x060041DD RID: 16861 RVA: 0x001101A5 File Offset: 0x0010E3A5
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public InterpolationAlignmentClauseSyntax AlignmentClause
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<InterpolationAlignmentClauseSyntax>(ref this.alignmentClause, 2);
			}
		}

		// Token: 0x17001109 RID: 4361
		// (get) Token: 0x060041DE RID: 16862 RVA: 0x001101B4 File Offset: 0x0010E3B4
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public InterpolationFormatClauseSyntax FormatClause
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<InterpolationFormatClauseSyntax>(ref this.formatClause, 3);
			}
		}

		// Token: 0x1700110A RID: 4362
		// (get) Token: 0x060041DF RID: 16863 RVA: 0x001101C3 File Offset: 0x0010E3C3
		public SyntaxToken CloseBraceToken
		{
			get
			{
				return new SyntaxToken(this, ((InterpolationSyntax)base.Green).closeBraceToken, this.GetChildPosition(4), base.GetChildIndex(4));
			}
		}

		// Token: 0x060041E0 RID: 16864 RVA: 0x001101EC File Offset: 0x0010E3EC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			switch (index)
			{
			case 1:
				syntaxNode = base.GetRed<ExpressionSyntax>(ref this.expression, 1);
				break;
			case 2:
				syntaxNode = base.GetRed<InterpolationAlignmentClauseSyntax>(ref this.alignmentClause, 2);
				break;
			case 3:
				syntaxNode = base.GetRed<InterpolationFormatClauseSyntax>(ref this.formatClause, 3);
				break;
			default:
				syntaxNode = null;
				break;
			}
			return syntaxNode;
		}

		// Token: 0x060041E1 RID: 16865 RVA: 0x00110244 File Offset: 0x0010E444
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			switch (index)
			{
			case 1:
				syntaxNode = this.expression;
				break;
			case 2:
				syntaxNode = this.alignmentClause;
				break;
			case 3:
				syntaxNode = this.formatClause;
				break;
			default:
				syntaxNode = null;
				break;
			}
			return syntaxNode;
		}

		// Token: 0x060041E2 RID: 16866 RVA: 0x00110285 File Offset: 0x0010E485
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitInterpolation(this);
		}

		// Token: 0x060041E3 RID: 16867 RVA: 0x0011028E File Offset: 0x0010E48E
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitInterpolation(this);
		}

		// Token: 0x060041E4 RID: 16868 RVA: 0x00110298 File Offset: 0x0010E498
		public InterpolationSyntax Update(SyntaxToken openBraceToken, ExpressionSyntax expression, [global::System.Runtime.CompilerServices.Nullable(2)] InterpolationAlignmentClauseSyntax alignmentClause, [global::System.Runtime.CompilerServices.Nullable(2)] InterpolationFormatClauseSyntax formatClause, SyntaxToken closeBraceToken)
		{
			if (!(openBraceToken != this.OpenBraceToken) && expression == this.Expression && alignmentClause == this.AlignmentClause && formatClause == this.FormatClause && !(closeBraceToken != this.CloseBraceToken))
			{
				return this;
			}
			InterpolationSyntax interpolationSyntax = SyntaxFactory.Interpolation(openBraceToken, expression, alignmentClause, formatClause, closeBraceToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return interpolationSyntax;
			}
			return interpolationSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060041E5 RID: 16869 RVA: 0x00110304 File Offset: 0x0010E504
		public InterpolationSyntax WithOpenBraceToken(SyntaxToken openBraceToken)
		{
			return this.Update(openBraceToken, this.Expression, this.AlignmentClause, this.FormatClause, this.CloseBraceToken);
		}

		// Token: 0x060041E6 RID: 16870 RVA: 0x00110325 File Offset: 0x0010E525
		public InterpolationSyntax WithExpression(ExpressionSyntax expression)
		{
			return this.Update(this.OpenBraceToken, expression, this.AlignmentClause, this.FormatClause, this.CloseBraceToken);
		}

		// Token: 0x060041E7 RID: 16871 RVA: 0x00110346 File Offset: 0x0010E546
		public InterpolationSyntax WithAlignmentClause([global::System.Runtime.CompilerServices.Nullable(2)] InterpolationAlignmentClauseSyntax alignmentClause)
		{
			return this.Update(this.OpenBraceToken, this.Expression, alignmentClause, this.FormatClause, this.CloseBraceToken);
		}

		// Token: 0x060041E8 RID: 16872 RVA: 0x00110367 File Offset: 0x0010E567
		public InterpolationSyntax WithFormatClause([global::System.Runtime.CompilerServices.Nullable(2)] InterpolationFormatClauseSyntax formatClause)
		{
			return this.Update(this.OpenBraceToken, this.Expression, this.AlignmentClause, formatClause, this.CloseBraceToken);
		}

		// Token: 0x060041E9 RID: 16873 RVA: 0x00110388 File Offset: 0x0010E588
		public InterpolationSyntax WithCloseBraceToken(SyntaxToken closeBraceToken)
		{
			return this.Update(this.OpenBraceToken, this.Expression, this.AlignmentClause, this.FormatClause, closeBraceToken);
		}

		// Token: 0x040014BB RID: 5307
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expression;

		// Token: 0x040014BC RID: 5308
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private InterpolationAlignmentClauseSyntax alignmentClause;

		// Token: 0x040014BD RID: 5309
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private InterpolationFormatClauseSyntax formatClause;
	}
}
