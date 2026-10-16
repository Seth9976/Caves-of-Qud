using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002BF RID: 703
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class RefExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06003FD4 RID: 16340 RVA: 0x0010C990 File Offset: 0x0010AB90
		internal RefExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001090 RID: 4240
		// (get) Token: 0x06003FD5 RID: 16341 RVA: 0x0010C99B File Offset: 0x0010AB9B
		public SyntaxToken RefKeyword
		{
			get
			{
				return new SyntaxToken(this, ((RefExpressionSyntax)base.Green).refKeyword, base.Position, 0);
			}
		}

		// Token: 0x17001091 RID: 4241
		// (get) Token: 0x06003FD6 RID: 16342 RVA: 0x0010C9BA File Offset: 0x0010ABBA
		public ExpressionSyntax Expression
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.expression, 1);
			}
		}

		// Token: 0x06003FD7 RID: 16343 RVA: 0x0010C9C9 File Offset: 0x0010ABC9
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed<ExpressionSyntax>(ref this.expression, 1);
		}

		// Token: 0x06003FD8 RID: 16344 RVA: 0x0010C9DE File Offset: 0x0010ABDE
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.expression;
		}

		// Token: 0x06003FD9 RID: 16345 RVA: 0x0010C9EC File Offset: 0x0010ABEC
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitRefExpression(this);
		}

		// Token: 0x06003FDA RID: 16346 RVA: 0x0010C9F5 File Offset: 0x0010ABF5
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitRefExpression(this);
		}

		// Token: 0x06003FDB RID: 16347 RVA: 0x0010CA00 File Offset: 0x0010AC00
		public RefExpressionSyntax Update(SyntaxToken refKeyword, ExpressionSyntax expression)
		{
			if (!(refKeyword != this.RefKeyword) && expression == this.Expression)
			{
				return this;
			}
			RefExpressionSyntax refExpressionSyntax = SyntaxFactory.RefExpression(refKeyword, expression);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return refExpressionSyntax;
			}
			return refExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003FDC RID: 16348 RVA: 0x0010CA45 File Offset: 0x0010AC45
		public RefExpressionSyntax WithRefKeyword(SyntaxToken refKeyword)
		{
			return this.Update(refKeyword, this.Expression);
		}

		// Token: 0x06003FDD RID: 16349 RVA: 0x0010CA54 File Offset: 0x0010AC54
		public RefExpressionSyntax WithExpression(ExpressionSyntax expression)
		{
			return this.Update(this.RefKeyword, expression);
		}

		// Token: 0x04001480 RID: 5248
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expression;
	}
}
