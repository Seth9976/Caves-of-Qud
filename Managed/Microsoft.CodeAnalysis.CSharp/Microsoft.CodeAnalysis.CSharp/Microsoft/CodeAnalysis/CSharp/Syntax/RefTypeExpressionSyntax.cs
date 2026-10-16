using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002B1 RID: 689
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class RefTypeExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06003F20 RID: 16160 RVA: 0x0010B605 File Offset: 0x00109805
		internal RefTypeExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001063 RID: 4195
		// (get) Token: 0x06003F21 RID: 16161 RVA: 0x0010B610 File Offset: 0x00109810
		public SyntaxToken Keyword
		{
			get
			{
				return new SyntaxToken(this, ((RefTypeExpressionSyntax)base.Green).keyword, base.Position, 0);
			}
		}

		// Token: 0x17001064 RID: 4196
		// (get) Token: 0x06003F22 RID: 16162 RVA: 0x0010B62F File Offset: 0x0010982F
		public SyntaxToken OpenParenToken
		{
			get
			{
				return new SyntaxToken(this, ((RefTypeExpressionSyntax)base.Green).openParenToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17001065 RID: 4197
		// (get) Token: 0x06003F23 RID: 16163 RVA: 0x0010B655 File Offset: 0x00109855
		public ExpressionSyntax Expression
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.expression, 2);
			}
		}

		// Token: 0x17001066 RID: 4198
		// (get) Token: 0x06003F24 RID: 16164 RVA: 0x0010B664 File Offset: 0x00109864
		public SyntaxToken CloseParenToken
		{
			get
			{
				return new SyntaxToken(this, ((RefTypeExpressionSyntax)base.Green).closeParenToken, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x06003F25 RID: 16165 RVA: 0x0010B68A File Offset: 0x0010988A
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 2)
			{
				return null;
			}
			return base.GetRed<ExpressionSyntax>(ref this.expression, 2);
		}

		// Token: 0x06003F26 RID: 16166 RVA: 0x0010B69F File Offset: 0x0010989F
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 2)
			{
				return null;
			}
			return this.expression;
		}

		// Token: 0x06003F27 RID: 16167 RVA: 0x0010B6AD File Offset: 0x001098AD
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitRefTypeExpression(this);
		}

		// Token: 0x06003F28 RID: 16168 RVA: 0x0010B6B6 File Offset: 0x001098B6
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitRefTypeExpression(this);
		}

		// Token: 0x06003F29 RID: 16169 RVA: 0x0010B6C0 File Offset: 0x001098C0
		public RefTypeExpressionSyntax Update(SyntaxToken keyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken)
		{
			if (!(keyword != this.Keyword) && !(openParenToken != this.OpenParenToken) && expression == this.Expression && !(closeParenToken != this.CloseParenToken))
			{
				return this;
			}
			RefTypeExpressionSyntax refTypeExpressionSyntax = SyntaxFactory.RefTypeExpression(keyword, openParenToken, expression, closeParenToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return refTypeExpressionSyntax;
			}
			return refTypeExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003F2A RID: 16170 RVA: 0x0010B725 File Offset: 0x00109925
		public RefTypeExpressionSyntax WithKeyword(SyntaxToken keyword)
		{
			return this.Update(keyword, this.OpenParenToken, this.Expression, this.CloseParenToken);
		}

		// Token: 0x06003F2B RID: 16171 RVA: 0x0010B740 File Offset: 0x00109940
		public RefTypeExpressionSyntax WithOpenParenToken(SyntaxToken openParenToken)
		{
			return this.Update(this.Keyword, openParenToken, this.Expression, this.CloseParenToken);
		}

		// Token: 0x06003F2C RID: 16172 RVA: 0x0010B75B File Offset: 0x0010995B
		public RefTypeExpressionSyntax WithExpression(ExpressionSyntax expression)
		{
			return this.Update(this.Keyword, this.OpenParenToken, expression, this.CloseParenToken);
		}

		// Token: 0x06003F2D RID: 16173 RVA: 0x0010B776 File Offset: 0x00109976
		public RefTypeExpressionSyntax WithCloseParenToken(SyntaxToken closeParenToken)
		{
			return this.Update(this.Keyword, this.OpenParenToken, this.Expression, closeParenToken);
		}

		// Token: 0x0400146E RID: 5230
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expression;
	}
}
