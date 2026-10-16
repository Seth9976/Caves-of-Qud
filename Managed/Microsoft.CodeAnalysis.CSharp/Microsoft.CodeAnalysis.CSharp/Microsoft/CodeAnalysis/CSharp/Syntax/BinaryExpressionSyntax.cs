using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002A9 RID: 681
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class BinaryExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06003ED1 RID: 16081 RVA: 0x0010ADCE File Offset: 0x00108FCE
		internal BinaryExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001051 RID: 4177
		// (get) Token: 0x06003ED2 RID: 16082 RVA: 0x0010ADD9 File Offset: 0x00108FD9
		public ExpressionSyntax Left
		{
			get
			{
				return base.GetRedAtZero<ExpressionSyntax>(ref this.left);
			}
		}

		// Token: 0x17001052 RID: 4178
		// (get) Token: 0x06003ED3 RID: 16083 RVA: 0x0010ADE7 File Offset: 0x00108FE7
		public SyntaxToken OperatorToken
		{
			get
			{
				return new SyntaxToken(this, ((BinaryExpressionSyntax)base.Green).operatorToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17001053 RID: 4179
		// (get) Token: 0x06003ED4 RID: 16084 RVA: 0x0010AE0D File Offset: 0x0010900D
		public ExpressionSyntax Right
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.right, 2);
			}
		}

		// Token: 0x06003ED5 RID: 16085 RVA: 0x0010AE1C File Offset: 0x0010901C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
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
					expressionSyntax = base.GetRed<ExpressionSyntax>(ref this.right, 2);
				}
			}
			else
			{
				expressionSyntax = base.GetRedAtZero<ExpressionSyntax>(ref this.left);
			}
			return expressionSyntax;
		}

		// Token: 0x06003ED6 RID: 16086 RVA: 0x0010AE54 File Offset: 0x00109054
		[global::System.Runtime.CompilerServices.NullableContext(2)]
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
					expressionSyntax = this.right;
				}
			}
			else
			{
				expressionSyntax = this.left;
			}
			return expressionSyntax;
		}

		// Token: 0x06003ED7 RID: 16087 RVA: 0x0010AE7F File Offset: 0x0010907F
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitBinaryExpression(this);
		}

		// Token: 0x06003ED8 RID: 16088 RVA: 0x0010AE88 File Offset: 0x00109088
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitBinaryExpression(this);
		}

		// Token: 0x06003ED9 RID: 16089 RVA: 0x0010AE94 File Offset: 0x00109094
		public BinaryExpressionSyntax Update(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right)
		{
			if (left == this.Left && !(operatorToken != this.OperatorToken) && right == this.Right)
			{
				return this;
			}
			BinaryExpressionSyntax binaryExpressionSyntax = SyntaxFactory.BinaryExpression(base.Kind(), left, operatorToken, right);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return binaryExpressionSyntax;
			}
			return binaryExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003EDA RID: 16090 RVA: 0x0010AEE9 File Offset: 0x001090E9
		public BinaryExpressionSyntax WithLeft(ExpressionSyntax left)
		{
			return this.Update(left, this.OperatorToken, this.Right);
		}

		// Token: 0x06003EDB RID: 16091 RVA: 0x0010AEFE File Offset: 0x001090FE
		public BinaryExpressionSyntax WithOperatorToken(SyntaxToken operatorToken)
		{
			return this.Update(this.Left, operatorToken, this.Right);
		}

		// Token: 0x06003EDC RID: 16092 RVA: 0x0010AF13 File Offset: 0x00109113
		public BinaryExpressionSyntax WithRight(ExpressionSyntax right)
		{
			return this.Update(this.Left, this.OperatorToken, right);
		}

		// Token: 0x04001466 RID: 5222
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax left;

		// Token: 0x04001467 RID: 5223
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax right;
	}
}
