using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002B3 RID: 691
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class CheckedExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06003F40 RID: 16192 RVA: 0x0010BA34 File Offset: 0x00109C34
		internal CheckedExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700106D RID: 4205
		// (get) Token: 0x06003F41 RID: 16193 RVA: 0x0010BA3F File Offset: 0x00109C3F
		public SyntaxToken Keyword
		{
			get
			{
				return new SyntaxToken(this, ((CheckedExpressionSyntax)base.Green).keyword, base.Position, 0);
			}
		}

		// Token: 0x1700106E RID: 4206
		// (get) Token: 0x06003F42 RID: 16194 RVA: 0x0010BA5E File Offset: 0x00109C5E
		public SyntaxToken OpenParenToken
		{
			get
			{
				return new SyntaxToken(this, ((CheckedExpressionSyntax)base.Green).openParenToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x1700106F RID: 4207
		// (get) Token: 0x06003F43 RID: 16195 RVA: 0x0010BA84 File Offset: 0x00109C84
		public ExpressionSyntax Expression
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.expression, 2);
			}
		}

		// Token: 0x17001070 RID: 4208
		// (get) Token: 0x06003F44 RID: 16196 RVA: 0x0010BA93 File Offset: 0x00109C93
		public SyntaxToken CloseParenToken
		{
			get
			{
				return new SyntaxToken(this, ((CheckedExpressionSyntax)base.Green).closeParenToken, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x06003F45 RID: 16197 RVA: 0x0010BAB9 File Offset: 0x00109CB9
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 2)
			{
				return null;
			}
			return base.GetRed<ExpressionSyntax>(ref this.expression, 2);
		}

		// Token: 0x06003F46 RID: 16198 RVA: 0x0010BACE File Offset: 0x00109CCE
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 2)
			{
				return null;
			}
			return this.expression;
		}

		// Token: 0x06003F47 RID: 16199 RVA: 0x0010BADC File Offset: 0x00109CDC
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitCheckedExpression(this);
		}

		// Token: 0x06003F48 RID: 16200 RVA: 0x0010BAE5 File Offset: 0x00109CE5
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitCheckedExpression(this);
		}

		// Token: 0x06003F49 RID: 16201 RVA: 0x0010BAF0 File Offset: 0x00109CF0
		public CheckedExpressionSyntax Update(SyntaxToken keyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken)
		{
			if (!(keyword != this.Keyword) && !(openParenToken != this.OpenParenToken) && expression == this.Expression && !(closeParenToken != this.CloseParenToken))
			{
				return this;
			}
			CheckedExpressionSyntax checkedExpressionSyntax = SyntaxFactory.CheckedExpression(base.Kind(), keyword, openParenToken, expression, closeParenToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return checkedExpressionSyntax;
			}
			return checkedExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003F4A RID: 16202 RVA: 0x0010BB5B File Offset: 0x00109D5B
		public CheckedExpressionSyntax WithKeyword(SyntaxToken keyword)
		{
			return this.Update(keyword, this.OpenParenToken, this.Expression, this.CloseParenToken);
		}

		// Token: 0x06003F4B RID: 16203 RVA: 0x0010BB76 File Offset: 0x00109D76
		public CheckedExpressionSyntax WithOpenParenToken(SyntaxToken openParenToken)
		{
			return this.Update(this.Keyword, openParenToken, this.Expression, this.CloseParenToken);
		}

		// Token: 0x06003F4C RID: 16204 RVA: 0x0010BB91 File Offset: 0x00109D91
		public CheckedExpressionSyntax WithExpression(ExpressionSyntax expression)
		{
			return this.Update(this.Keyword, this.OpenParenToken, expression, this.CloseParenToken);
		}

		// Token: 0x06003F4D RID: 16205 RVA: 0x0010BBAC File Offset: 0x00109DAC
		public CheckedExpressionSyntax WithCloseParenToken(SyntaxToken closeParenToken)
		{
			return this.Update(this.Keyword, this.OpenParenToken, this.Expression, closeParenToken);
		}

		// Token: 0x04001471 RID: 5233
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expression;
	}
}
