using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200029E RID: 670
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ParenthesizedExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06003E5A RID: 15962 RVA: 0x0010A256 File Offset: 0x00108456
		internal ParenthesizedExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001038 RID: 4152
		// (get) Token: 0x06003E5B RID: 15963 RVA: 0x0010A261 File Offset: 0x00108461
		public SyntaxToken OpenParenToken
		{
			get
			{
				return new SyntaxToken(this, ((ParenthesizedExpressionSyntax)base.Green).openParenToken, base.Position, 0);
			}
		}

		// Token: 0x17001039 RID: 4153
		// (get) Token: 0x06003E5C RID: 15964 RVA: 0x0010A280 File Offset: 0x00108480
		public ExpressionSyntax Expression
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.expression, 1);
			}
		}

		// Token: 0x1700103A RID: 4154
		// (get) Token: 0x06003E5D RID: 15965 RVA: 0x0010A28F File Offset: 0x0010848F
		public SyntaxToken CloseParenToken
		{
			get
			{
				return new SyntaxToken(this, ((ParenthesizedExpressionSyntax)base.Green).closeParenToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x06003E5E RID: 15966 RVA: 0x0010A2B5 File Offset: 0x001084B5
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed<ExpressionSyntax>(ref this.expression, 1);
		}

		// Token: 0x06003E5F RID: 15967 RVA: 0x0010A2CA File Offset: 0x001084CA
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.expression;
		}

		// Token: 0x06003E60 RID: 15968 RVA: 0x0010A2D8 File Offset: 0x001084D8
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitParenthesizedExpression(this);
		}

		// Token: 0x06003E61 RID: 15969 RVA: 0x0010A2E1 File Offset: 0x001084E1
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitParenthesizedExpression(this);
		}

		// Token: 0x06003E62 RID: 15970 RVA: 0x0010A2EC File Offset: 0x001084EC
		public ParenthesizedExpressionSyntax Update(SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken)
		{
			if (!(openParenToken != this.OpenParenToken) && expression == this.Expression && !(closeParenToken != this.CloseParenToken))
			{
				return this;
			}
			ParenthesizedExpressionSyntax parenthesizedExpressionSyntax = SyntaxFactory.ParenthesizedExpression(openParenToken, expression, closeParenToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return parenthesizedExpressionSyntax;
			}
			return parenthesizedExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003E63 RID: 15971 RVA: 0x0010A340 File Offset: 0x00108540
		public ParenthesizedExpressionSyntax WithOpenParenToken(SyntaxToken openParenToken)
		{
			return this.Update(openParenToken, this.Expression, this.CloseParenToken);
		}

		// Token: 0x06003E64 RID: 15972 RVA: 0x0010A355 File Offset: 0x00108555
		public ParenthesizedExpressionSyntax WithExpression(ExpressionSyntax expression)
		{
			return this.Update(this.OpenParenToken, expression, this.CloseParenToken);
		}

		// Token: 0x06003E65 RID: 15973 RVA: 0x0010A36A File Offset: 0x0010856A
		public ParenthesizedExpressionSyntax WithCloseParenToken(SyntaxToken closeParenToken)
		{
			return this.Update(this.OpenParenToken, this.Expression, closeParenToken);
		}

		// Token: 0x04001458 RID: 5208
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expression;
	}
}
