using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002B8 RID: 696
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ElementAccessExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06003F83 RID: 16259 RVA: 0x0010C1A2 File Offset: 0x0010A3A2
		internal ElementAccessExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700107F RID: 4223
		// (get) Token: 0x06003F84 RID: 16260 RVA: 0x0010C1AD File Offset: 0x0010A3AD
		public ExpressionSyntax Expression
		{
			get
			{
				return base.GetRedAtZero<ExpressionSyntax>(ref this.expression);
			}
		}

		// Token: 0x17001080 RID: 4224
		// (get) Token: 0x06003F85 RID: 16261 RVA: 0x0010C1BB File Offset: 0x0010A3BB
		public BracketedArgumentListSyntax ArgumentList
		{
			get
			{
				return base.GetRed<BracketedArgumentListSyntax>(ref this.argumentList, 1);
			}
		}

		// Token: 0x06003F86 RID: 16262 RVA: 0x0010C1CC File Offset: 0x0010A3CC
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
					syntaxNode = base.GetRed<BracketedArgumentListSyntax>(ref this.argumentList, 1);
				}
			}
			else
			{
				syntaxNode = base.GetRedAtZero<ExpressionSyntax>(ref this.expression);
			}
			return syntaxNode;
		}

		// Token: 0x06003F87 RID: 16263 RVA: 0x0010C204 File Offset: 0x0010A404
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

		// Token: 0x06003F88 RID: 16264 RVA: 0x0010C22F File Offset: 0x0010A42F
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitElementAccessExpression(this);
		}

		// Token: 0x06003F89 RID: 16265 RVA: 0x0010C238 File Offset: 0x0010A438
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitElementAccessExpression(this);
		}

		// Token: 0x06003F8A RID: 16266 RVA: 0x0010C244 File Offset: 0x0010A444
		public ElementAccessExpressionSyntax Update(ExpressionSyntax expression, BracketedArgumentListSyntax argumentList)
		{
			if (expression == this.Expression && argumentList == this.ArgumentList)
			{
				return this;
			}
			ElementAccessExpressionSyntax elementAccessExpressionSyntax = SyntaxFactory.ElementAccessExpression(expression, argumentList);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return elementAccessExpressionSyntax;
			}
			return elementAccessExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003F8B RID: 16267 RVA: 0x0010C284 File Offset: 0x0010A484
		public ElementAccessExpressionSyntax WithExpression(ExpressionSyntax expression)
		{
			return this.Update(expression, this.ArgumentList);
		}

		// Token: 0x06003F8C RID: 16268 RVA: 0x0010C293 File Offset: 0x0010A493
		public ElementAccessExpressionSyntax WithArgumentList(BracketedArgumentListSyntax argumentList)
		{
			return this.Update(this.Expression, argumentList);
		}

		// Token: 0x06003F8D RID: 16269 RVA: 0x0010C2A4 File Offset: 0x0010A4A4
		public ElementAccessExpressionSyntax AddArgumentListArguments(params ArgumentSyntax[] items)
		{
			return this.WithArgumentList(this.ArgumentList.WithArguments(this.ArgumentList.Arguments.AddRange(items)));
		}

		// Token: 0x04001477 RID: 5239
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expression;

		// Token: 0x04001478 RID: 5240
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private BracketedArgumentListSyntax argumentList;
	}
}
