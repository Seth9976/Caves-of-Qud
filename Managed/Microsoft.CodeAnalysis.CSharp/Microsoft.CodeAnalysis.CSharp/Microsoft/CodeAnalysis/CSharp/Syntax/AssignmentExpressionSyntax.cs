using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002AA RID: 682
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class AssignmentExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06003EDD RID: 16093 RVA: 0x0010AF28 File Offset: 0x00109128
		internal AssignmentExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001054 RID: 4180
		// (get) Token: 0x06003EDE RID: 16094 RVA: 0x0010AF33 File Offset: 0x00109133
		public ExpressionSyntax Left
		{
			get
			{
				return base.GetRedAtZero<ExpressionSyntax>(ref this.left);
			}
		}

		// Token: 0x17001055 RID: 4181
		// (get) Token: 0x06003EDF RID: 16095 RVA: 0x0010AF41 File Offset: 0x00109141
		public SyntaxToken OperatorToken
		{
			get
			{
				return new SyntaxToken(this, ((AssignmentExpressionSyntax)base.Green).operatorToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17001056 RID: 4182
		// (get) Token: 0x06003EE0 RID: 16096 RVA: 0x0010AF67 File Offset: 0x00109167
		public ExpressionSyntax Right
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.right, 2);
			}
		}

		// Token: 0x06003EE1 RID: 16097 RVA: 0x0010AF78 File Offset: 0x00109178
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

		// Token: 0x06003EE2 RID: 16098 RVA: 0x0010AFB0 File Offset: 0x001091B0
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

		// Token: 0x06003EE3 RID: 16099 RVA: 0x0010AFDB File Offset: 0x001091DB
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitAssignmentExpression(this);
		}

		// Token: 0x06003EE4 RID: 16100 RVA: 0x0010AFE4 File Offset: 0x001091E4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitAssignmentExpression(this);
		}

		// Token: 0x06003EE5 RID: 16101 RVA: 0x0010AFF0 File Offset: 0x001091F0
		public AssignmentExpressionSyntax Update(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right)
		{
			if (left == this.Left && !(operatorToken != this.OperatorToken) && right == this.Right)
			{
				return this;
			}
			AssignmentExpressionSyntax assignmentExpressionSyntax = SyntaxFactory.AssignmentExpression(base.Kind(), left, operatorToken, right);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return assignmentExpressionSyntax;
			}
			return assignmentExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003EE6 RID: 16102 RVA: 0x0010B045 File Offset: 0x00109245
		public AssignmentExpressionSyntax WithLeft(ExpressionSyntax left)
		{
			return this.Update(left, this.OperatorToken, this.Right);
		}

		// Token: 0x06003EE7 RID: 16103 RVA: 0x0010B05A File Offset: 0x0010925A
		public AssignmentExpressionSyntax WithOperatorToken(SyntaxToken operatorToken)
		{
			return this.Update(this.Left, operatorToken, this.Right);
		}

		// Token: 0x06003EE8 RID: 16104 RVA: 0x0010B06F File Offset: 0x0010926F
		public AssignmentExpressionSyntax WithRight(ExpressionSyntax right)
		{
			return this.Update(this.Left, this.OperatorToken, right);
		}

		// Token: 0x04001468 RID: 5224
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax left;

		// Token: 0x04001469 RID: 5225
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax right;
	}
}
