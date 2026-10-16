using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002A4 RID: 676
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ConditionalAccessExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06003E9D RID: 16029 RVA: 0x0010A8D4 File Offset: 0x00108AD4
		internal ConditionalAccessExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001047 RID: 4167
		// (get) Token: 0x06003E9E RID: 16030 RVA: 0x0010A8DF File Offset: 0x00108ADF
		public ExpressionSyntax Expression
		{
			get
			{
				return base.GetRedAtZero<ExpressionSyntax>(ref this.expression);
			}
		}

		// Token: 0x17001048 RID: 4168
		// (get) Token: 0x06003E9F RID: 16031 RVA: 0x0010A8ED File Offset: 0x00108AED
		public SyntaxToken OperatorToken
		{
			get
			{
				return new SyntaxToken(this, ((ConditionalAccessExpressionSyntax)base.Green).operatorToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17001049 RID: 4169
		// (get) Token: 0x06003EA0 RID: 16032 RVA: 0x0010A913 File Offset: 0x00108B13
		public ExpressionSyntax WhenNotNull
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.whenNotNull, 2);
			}
		}

		// Token: 0x06003EA1 RID: 16033 RVA: 0x0010A924 File Offset: 0x00108B24
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
					expressionSyntax = base.GetRed<ExpressionSyntax>(ref this.whenNotNull, 2);
				}
			}
			else
			{
				expressionSyntax = base.GetRedAtZero<ExpressionSyntax>(ref this.expression);
			}
			return expressionSyntax;
		}

		// Token: 0x06003EA2 RID: 16034 RVA: 0x0010A95C File Offset: 0x00108B5C
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
					expressionSyntax = this.whenNotNull;
				}
			}
			else
			{
				expressionSyntax = this.expression;
			}
			return expressionSyntax;
		}

		// Token: 0x06003EA3 RID: 16035 RVA: 0x0010A987 File Offset: 0x00108B87
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitConditionalAccessExpression(this);
		}

		// Token: 0x06003EA4 RID: 16036 RVA: 0x0010A990 File Offset: 0x00108B90
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitConditionalAccessExpression(this);
		}

		// Token: 0x06003EA5 RID: 16037 RVA: 0x0010A99C File Offset: 0x00108B9C
		public ConditionalAccessExpressionSyntax Update(ExpressionSyntax expression, SyntaxToken operatorToken, ExpressionSyntax whenNotNull)
		{
			if (expression == this.Expression && !(operatorToken != this.OperatorToken) && whenNotNull == this.WhenNotNull)
			{
				return this;
			}
			ConditionalAccessExpressionSyntax conditionalAccessExpressionSyntax = SyntaxFactory.ConditionalAccessExpression(expression, operatorToken, whenNotNull);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return conditionalAccessExpressionSyntax;
			}
			return conditionalAccessExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003EA6 RID: 16038 RVA: 0x0010A9EB File Offset: 0x00108BEB
		public ConditionalAccessExpressionSyntax WithExpression(ExpressionSyntax expression)
		{
			return this.Update(expression, this.OperatorToken, this.WhenNotNull);
		}

		// Token: 0x06003EA7 RID: 16039 RVA: 0x0010AA00 File Offset: 0x00108C00
		public ConditionalAccessExpressionSyntax WithOperatorToken(SyntaxToken operatorToken)
		{
			return this.Update(this.Expression, operatorToken, this.WhenNotNull);
		}

		// Token: 0x06003EA8 RID: 16040 RVA: 0x0010AA15 File Offset: 0x00108C15
		public ConditionalAccessExpressionSyntax WithWhenNotNull(ExpressionSyntax whenNotNull)
		{
			return this.Update(this.Expression, this.OperatorToken, whenNotNull);
		}

		// Token: 0x0400145F RID: 5215
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expression;

		// Token: 0x04001460 RID: 5216
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax whenNotNull;
	}
}
