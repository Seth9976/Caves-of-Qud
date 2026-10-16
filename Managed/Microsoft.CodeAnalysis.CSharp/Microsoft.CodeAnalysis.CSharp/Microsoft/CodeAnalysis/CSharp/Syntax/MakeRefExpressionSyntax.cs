using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002B0 RID: 688
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class MakeRefExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06003F12 RID: 16146 RVA: 0x0010B47A File Offset: 0x0010967A
		internal MakeRefExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700105F RID: 4191
		// (get) Token: 0x06003F13 RID: 16147 RVA: 0x0010B485 File Offset: 0x00109685
		public SyntaxToken Keyword
		{
			get
			{
				return new SyntaxToken(this, ((MakeRefExpressionSyntax)base.Green).keyword, base.Position, 0);
			}
		}

		// Token: 0x17001060 RID: 4192
		// (get) Token: 0x06003F14 RID: 16148 RVA: 0x0010B4A4 File Offset: 0x001096A4
		public SyntaxToken OpenParenToken
		{
			get
			{
				return new SyntaxToken(this, ((MakeRefExpressionSyntax)base.Green).openParenToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17001061 RID: 4193
		// (get) Token: 0x06003F15 RID: 16149 RVA: 0x0010B4CA File Offset: 0x001096CA
		public ExpressionSyntax Expression
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.expression, 2);
			}
		}

		// Token: 0x17001062 RID: 4194
		// (get) Token: 0x06003F16 RID: 16150 RVA: 0x0010B4D9 File Offset: 0x001096D9
		public SyntaxToken CloseParenToken
		{
			get
			{
				return new SyntaxToken(this, ((MakeRefExpressionSyntax)base.Green).closeParenToken, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x06003F17 RID: 16151 RVA: 0x0010B4FF File Offset: 0x001096FF
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 2)
			{
				return null;
			}
			return base.GetRed<ExpressionSyntax>(ref this.expression, 2);
		}

		// Token: 0x06003F18 RID: 16152 RVA: 0x0010B514 File Offset: 0x00109714
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 2)
			{
				return null;
			}
			return this.expression;
		}

		// Token: 0x06003F19 RID: 16153 RVA: 0x0010B522 File Offset: 0x00109722
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitMakeRefExpression(this);
		}

		// Token: 0x06003F1A RID: 16154 RVA: 0x0010B52B File Offset: 0x0010972B
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitMakeRefExpression(this);
		}

		// Token: 0x06003F1B RID: 16155 RVA: 0x0010B534 File Offset: 0x00109734
		public MakeRefExpressionSyntax Update(SyntaxToken keyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken)
		{
			if (!(keyword != this.Keyword) && !(openParenToken != this.OpenParenToken) && expression == this.Expression && !(closeParenToken != this.CloseParenToken))
			{
				return this;
			}
			MakeRefExpressionSyntax makeRefExpressionSyntax = SyntaxFactory.MakeRefExpression(keyword, openParenToken, expression, closeParenToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return makeRefExpressionSyntax;
			}
			return makeRefExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003F1C RID: 16156 RVA: 0x0010B599 File Offset: 0x00109799
		public MakeRefExpressionSyntax WithKeyword(SyntaxToken keyword)
		{
			return this.Update(keyword, this.OpenParenToken, this.Expression, this.CloseParenToken);
		}

		// Token: 0x06003F1D RID: 16157 RVA: 0x0010B5B4 File Offset: 0x001097B4
		public MakeRefExpressionSyntax WithOpenParenToken(SyntaxToken openParenToken)
		{
			return this.Update(this.Keyword, openParenToken, this.Expression, this.CloseParenToken);
		}

		// Token: 0x06003F1E RID: 16158 RVA: 0x0010B5CF File Offset: 0x001097CF
		public MakeRefExpressionSyntax WithExpression(ExpressionSyntax expression)
		{
			return this.Update(this.Keyword, this.OpenParenToken, expression, this.CloseParenToken);
		}

		// Token: 0x06003F1F RID: 16159 RVA: 0x0010B5EA File Offset: 0x001097EA
		public MakeRefExpressionSyntax WithCloseParenToken(SyntaxToken closeParenToken)
		{
			return this.Update(this.Keyword, this.OpenParenToken, this.Expression, closeParenToken);
		}

		// Token: 0x0400146D RID: 5229
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expression;
	}
}
