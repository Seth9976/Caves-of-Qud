using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002A2 RID: 674
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class PostfixUnaryExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06003E87 RID: 16007 RVA: 0x0010A69F File Offset: 0x0010889F
		internal PostfixUnaryExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001042 RID: 4162
		// (get) Token: 0x06003E88 RID: 16008 RVA: 0x0010A6AA File Offset: 0x001088AA
		public ExpressionSyntax Operand
		{
			get
			{
				return base.GetRedAtZero<ExpressionSyntax>(ref this.operand);
			}
		}

		// Token: 0x17001043 RID: 4163
		// (get) Token: 0x06003E89 RID: 16009 RVA: 0x0010A6B8 File Offset: 0x001088B8
		public SyntaxToken OperatorToken
		{
			get
			{
				return new SyntaxToken(this, ((PostfixUnaryExpressionSyntax)base.Green).operatorToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x06003E8A RID: 16010 RVA: 0x0010A6DE File Offset: 0x001088DE
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return base.GetRedAtZero<ExpressionSyntax>(ref this.operand);
		}

		// Token: 0x06003E8B RID: 16011 RVA: 0x0010A6F1 File Offset: 0x001088F1
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.operand;
		}

		// Token: 0x06003E8C RID: 16012 RVA: 0x0010A6FE File Offset: 0x001088FE
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitPostfixUnaryExpression(this);
		}

		// Token: 0x06003E8D RID: 16013 RVA: 0x0010A707 File Offset: 0x00108907
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitPostfixUnaryExpression(this);
		}

		// Token: 0x06003E8E RID: 16014 RVA: 0x0010A710 File Offset: 0x00108910
		public PostfixUnaryExpressionSyntax Update(ExpressionSyntax operand, SyntaxToken operatorToken)
		{
			if (operand == this.Operand && !(operatorToken != this.OperatorToken))
			{
				return this;
			}
			PostfixUnaryExpressionSyntax postfixUnaryExpressionSyntax = SyntaxFactory.PostfixUnaryExpression(base.Kind(), operand, operatorToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return postfixUnaryExpressionSyntax;
			}
			return postfixUnaryExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003E8F RID: 16015 RVA: 0x0010A75B File Offset: 0x0010895B
		public PostfixUnaryExpressionSyntax WithOperand(ExpressionSyntax operand)
		{
			return this.Update(operand, this.OperatorToken);
		}

		// Token: 0x06003E90 RID: 16016 RVA: 0x0010A76A File Offset: 0x0010896A
		public PostfixUnaryExpressionSyntax WithOperatorToken(SyntaxToken operatorToken)
		{
			return this.Update(this.Operand, operatorToken);
		}

		// Token: 0x0400145C RID: 5212
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax operand;
	}
}
