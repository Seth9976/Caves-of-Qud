using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000321 RID: 801
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ArrowExpressionClauseSyntax : CSharpSyntaxNode
	{
		// Token: 0x0600451D RID: 17693 RVA: 0x001164BA File Offset: 0x001146BA
		internal ArrowExpressionClauseSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170011BF RID: 4543
		// (get) Token: 0x0600451E RID: 17694 RVA: 0x001164C5 File Offset: 0x001146C5
		public SyntaxToken ArrowToken
		{
			get
			{
				return new SyntaxToken(this, ((ArrowExpressionClauseSyntax)base.Green).arrowToken, base.Position, 0);
			}
		}

		// Token: 0x170011C0 RID: 4544
		// (get) Token: 0x0600451F RID: 17695 RVA: 0x001164E4 File Offset: 0x001146E4
		public ExpressionSyntax Expression
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.expression, 1);
			}
		}

		// Token: 0x06004520 RID: 17696 RVA: 0x001164F3 File Offset: 0x001146F3
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed<ExpressionSyntax>(ref this.expression, 1);
		}

		// Token: 0x06004521 RID: 17697 RVA: 0x00116508 File Offset: 0x00114708
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.expression;
		}

		// Token: 0x06004522 RID: 17698 RVA: 0x00116516 File Offset: 0x00114716
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitArrowExpressionClause(this);
		}

		// Token: 0x06004523 RID: 17699 RVA: 0x0011651F File Offset: 0x0011471F
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitArrowExpressionClause(this);
		}

		// Token: 0x06004524 RID: 17700 RVA: 0x00116528 File Offset: 0x00114728
		public ArrowExpressionClauseSyntax Update(SyntaxToken arrowToken, ExpressionSyntax expression)
		{
			if (!(arrowToken != this.ArrowToken) && expression == this.Expression)
			{
				return this;
			}
			ArrowExpressionClauseSyntax arrowExpressionClauseSyntax = SyntaxFactory.ArrowExpressionClause(arrowToken, expression);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return arrowExpressionClauseSyntax;
			}
			return arrowExpressionClauseSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004525 RID: 17701 RVA: 0x0011656D File Offset: 0x0011476D
		public ArrowExpressionClauseSyntax WithArrowToken(SyntaxToken arrowToken)
		{
			return this.Update(arrowToken, this.Expression);
		}

		// Token: 0x06004526 RID: 17702 RVA: 0x0011657C File Offset: 0x0011477C
		public ArrowExpressionClauseSyntax WithExpression(ExpressionSyntax expression)
		{
			return this.Update(this.ArrowToken, expression);
		}

		// Token: 0x04001508 RID: 5384
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expression;
	}
}
