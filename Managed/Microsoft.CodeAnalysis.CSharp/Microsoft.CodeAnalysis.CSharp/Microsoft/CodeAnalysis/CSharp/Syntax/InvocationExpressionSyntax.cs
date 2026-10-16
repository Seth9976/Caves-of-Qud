using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002B7 RID: 695
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class InvocationExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06003F78 RID: 16248 RVA: 0x0010C06D File Offset: 0x0010A26D
		internal InvocationExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700107D RID: 4221
		// (get) Token: 0x06003F79 RID: 16249 RVA: 0x0010C078 File Offset: 0x0010A278
		public ExpressionSyntax Expression
		{
			get
			{
				return base.GetRedAtZero<ExpressionSyntax>(ref this.expression);
			}
		}

		// Token: 0x1700107E RID: 4222
		// (get) Token: 0x06003F7A RID: 16250 RVA: 0x0010C086 File Offset: 0x0010A286
		public ArgumentListSyntax ArgumentList
		{
			get
			{
				return base.GetRed<ArgumentListSyntax>(ref this.argumentList, 1);
			}
		}

		// Token: 0x06003F7B RID: 16251 RVA: 0x0010C098 File Offset: 0x0010A298
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 1)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = base.GetRed<ArgumentListSyntax>(ref this.argumentList, 1);
				}
			}
			else
			{
				syntaxNode = base.GetRedAtZero<ExpressionSyntax>(ref this.expression);
			}
			return syntaxNode;
		}

		// Token: 0x06003F7C RID: 16252 RVA: 0x0010C0D0 File Offset: 0x0010A2D0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 1)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = this.argumentList;
				}
			}
			else
			{
				syntaxNode = this.expression;
			}
			return syntaxNode;
		}

		// Token: 0x06003F7D RID: 16253 RVA: 0x0010C0FB File Offset: 0x0010A2FB
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitInvocationExpression(this);
		}

		// Token: 0x06003F7E RID: 16254 RVA: 0x0010C104 File Offset: 0x0010A304
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitInvocationExpression(this);
		}

		// Token: 0x06003F7F RID: 16255 RVA: 0x0010C110 File Offset: 0x0010A310
		public InvocationExpressionSyntax Update(ExpressionSyntax expression, ArgumentListSyntax argumentList)
		{
			if (expression == this.Expression && argumentList == this.ArgumentList)
			{
				return this;
			}
			InvocationExpressionSyntax invocationExpressionSyntax = SyntaxFactory.InvocationExpression(expression, argumentList);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return invocationExpressionSyntax;
			}
			return invocationExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003F80 RID: 16256 RVA: 0x0010C150 File Offset: 0x0010A350
		public InvocationExpressionSyntax WithExpression(ExpressionSyntax expression)
		{
			return this.Update(expression, this.ArgumentList);
		}

		// Token: 0x06003F81 RID: 16257 RVA: 0x0010C15F File Offset: 0x0010A35F
		public InvocationExpressionSyntax WithArgumentList(ArgumentListSyntax argumentList)
		{
			return this.Update(this.Expression, argumentList);
		}

		// Token: 0x06003F82 RID: 16258 RVA: 0x0010C170 File Offset: 0x0010A370
		public InvocationExpressionSyntax AddArgumentListArguments(params ArgumentSyntax[] items)
		{
			return this.WithArgumentList(this.ArgumentList.WithArguments(this.ArgumentList.Arguments.AddRange(items)));
		}

		// Token: 0x04001475 RID: 5237
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expression;

		// Token: 0x04001476 RID: 5238
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ArgumentListSyntax argumentList;
	}
}
