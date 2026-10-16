using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002A7 RID: 679
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class RangeExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06003EBC RID: 16060 RVA: 0x0010ABBA File Offset: 0x00108DBA
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal RangeExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700104D RID: 4173
		// (get) Token: 0x06003EBD RID: 16061 RVA: 0x0010ABC5 File Offset: 0x00108DC5
		public ExpressionSyntax LeftOperand
		{
			get
			{
				return base.GetRedAtZero<ExpressionSyntax>(ref this.leftOperand);
			}
		}

		// Token: 0x1700104E RID: 4174
		// (get) Token: 0x06003EBE RID: 16062 RVA: 0x0010ABD3 File Offset: 0x00108DD3
		public SyntaxToken OperatorToken
		{
			get
			{
				return new SyntaxToken(this, ((RangeExpressionSyntax)base.Green).operatorToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x1700104F RID: 4175
		// (get) Token: 0x06003EBF RID: 16063 RVA: 0x0010ABF9 File Offset: 0x00108DF9
		public ExpressionSyntax RightOperand
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.rightOperand, 2);
			}
		}

		// Token: 0x06003EC0 RID: 16064 RVA: 0x0010AC08 File Offset: 0x00108E08
		internal override SyntaxNode GetNodeSlot(int index)
		{
			ExpressionSyntax expressionSyntax;
			if (index != 0)
			{
				if (index != 2)
				{
					expressionSyntax = null;
				}
				else
				{
					expressionSyntax = base.GetRed<ExpressionSyntax>(ref this.rightOperand, 2);
				}
			}
			else
			{
				expressionSyntax = base.GetRedAtZero<ExpressionSyntax>(ref this.leftOperand);
			}
			return expressionSyntax;
		}

		// Token: 0x06003EC1 RID: 16065 RVA: 0x0010AC40 File Offset: 0x00108E40
		internal override SyntaxNode GetCachedSlot(int index)
		{
			ExpressionSyntax expressionSyntax;
			if (index != 0)
			{
				if (index != 2)
				{
					expressionSyntax = null;
				}
				else
				{
					expressionSyntax = this.rightOperand;
				}
			}
			else
			{
				expressionSyntax = this.leftOperand;
			}
			return expressionSyntax;
		}

		// Token: 0x06003EC2 RID: 16066 RVA: 0x0010AC6B File Offset: 0x00108E6B
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitRangeExpression(this);
		}

		// Token: 0x06003EC3 RID: 16067 RVA: 0x0010AC74 File Offset: 0x00108E74
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitRangeExpression(this);
		}

		// Token: 0x06003EC4 RID: 16068 RVA: 0x0010AC80 File Offset: 0x00108E80
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public RangeExpressionSyntax Update(ExpressionSyntax leftOperand, SyntaxToken operatorToken, ExpressionSyntax rightOperand)
		{
			if (leftOperand == this.LeftOperand && !(operatorToken != this.OperatorToken) && rightOperand == this.RightOperand)
			{
				return this;
			}
			RangeExpressionSyntax rangeExpressionSyntax = SyntaxFactory.RangeExpression(leftOperand, operatorToken, rightOperand);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return rangeExpressionSyntax;
			}
			return rangeExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003EC5 RID: 16069 RVA: 0x0010ACCF File Offset: 0x00108ECF
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public RangeExpressionSyntax WithLeftOperand([global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax leftOperand)
		{
			return this.Update(leftOperand, this.OperatorToken, this.RightOperand);
		}

		// Token: 0x06003EC6 RID: 16070 RVA: 0x0010ACE4 File Offset: 0x00108EE4
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public RangeExpressionSyntax WithOperatorToken(SyntaxToken operatorToken)
		{
			return this.Update(this.LeftOperand, operatorToken, this.RightOperand);
		}

		// Token: 0x06003EC7 RID: 16071 RVA: 0x0010ACF9 File Offset: 0x00108EF9
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public RangeExpressionSyntax WithRightOperand([global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax rightOperand)
		{
			return this.Update(this.LeftOperand, this.OperatorToken, rightOperand);
		}

		// Token: 0x04001463 RID: 5219
		private ExpressionSyntax leftOperand;

		// Token: 0x04001464 RID: 5220
		private ExpressionSyntax rightOperand;
	}
}
