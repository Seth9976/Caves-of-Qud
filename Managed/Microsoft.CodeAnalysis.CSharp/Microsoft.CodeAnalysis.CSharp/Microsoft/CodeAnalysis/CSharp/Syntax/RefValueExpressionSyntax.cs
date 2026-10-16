using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002B2 RID: 690
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class RefValueExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06003F2E RID: 16174 RVA: 0x0010B791 File Offset: 0x00109991
		internal RefValueExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001067 RID: 4199
		// (get) Token: 0x06003F2F RID: 16175 RVA: 0x0010B79C File Offset: 0x0010999C
		public SyntaxToken Keyword
		{
			get
			{
				return new SyntaxToken(this, ((RefValueExpressionSyntax)base.Green).keyword, base.Position, 0);
			}
		}

		// Token: 0x17001068 RID: 4200
		// (get) Token: 0x06003F30 RID: 16176 RVA: 0x0010B7BB File Offset: 0x001099BB
		public SyntaxToken OpenParenToken
		{
			get
			{
				return new SyntaxToken(this, ((RefValueExpressionSyntax)base.Green).openParenToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17001069 RID: 4201
		// (get) Token: 0x06003F31 RID: 16177 RVA: 0x0010B7E1 File Offset: 0x001099E1
		public ExpressionSyntax Expression
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.expression, 2);
			}
		}

		// Token: 0x1700106A RID: 4202
		// (get) Token: 0x06003F32 RID: 16178 RVA: 0x0010B7F0 File Offset: 0x001099F0
		public SyntaxToken Comma
		{
			get
			{
				return new SyntaxToken(this, ((RefValueExpressionSyntax)base.Green).comma, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x1700106B RID: 4203
		// (get) Token: 0x06003F33 RID: 16179 RVA: 0x0010B816 File Offset: 0x00109A16
		public TypeSyntax Type
		{
			get
			{
				return base.GetRed<TypeSyntax>(ref this.type, 4);
			}
		}

		// Token: 0x1700106C RID: 4204
		// (get) Token: 0x06003F34 RID: 16180 RVA: 0x0010B825 File Offset: 0x00109A25
		public SyntaxToken CloseParenToken
		{
			get
			{
				return new SyntaxToken(this, ((RefValueExpressionSyntax)base.Green).closeParenToken, this.GetChildPosition(5), base.GetChildIndex(5));
			}
		}

		// Token: 0x06003F35 RID: 16181 RVA: 0x0010B84C File Offset: 0x00109A4C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			ExpressionSyntax expressionSyntax;
			if (index != 2)
			{
				if (index != 4)
				{
					expressionSyntax = null;
				}
				else
				{
					expressionSyntax = base.GetRed<TypeSyntax>(ref this.type, 4);
				}
			}
			else
			{
				expressionSyntax = base.GetRed<ExpressionSyntax>(ref this.expression, 2);
			}
			return expressionSyntax;
		}

		// Token: 0x06003F36 RID: 16182 RVA: 0x0010B888 File Offset: 0x00109A88
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			ExpressionSyntax expressionSyntax;
			if (index != 2)
			{
				if (index != 4)
				{
					expressionSyntax = null;
				}
				else
				{
					expressionSyntax = this.type;
				}
			}
			else
			{
				expressionSyntax = this.expression;
			}
			return expressionSyntax;
		}

		// Token: 0x06003F37 RID: 16183 RVA: 0x0010B8B4 File Offset: 0x00109AB4
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitRefValueExpression(this);
		}

		// Token: 0x06003F38 RID: 16184 RVA: 0x0010B8BD File Offset: 0x00109ABD
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitRefValueExpression(this);
		}

		// Token: 0x06003F39 RID: 16185 RVA: 0x0010B8C8 File Offset: 0x00109AC8
		public RefValueExpressionSyntax Update(SyntaxToken keyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken comma, TypeSyntax type, SyntaxToken closeParenToken)
		{
			if (!(keyword != this.Keyword) && !(openParenToken != this.OpenParenToken) && expression == this.Expression && !(comma != this.Comma) && type == this.Type && !(closeParenToken != this.CloseParenToken))
			{
				return this;
			}
			RefValueExpressionSyntax refValueExpressionSyntax = SyntaxFactory.RefValueExpression(keyword, openParenToken, expression, comma, type, closeParenToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return refValueExpressionSyntax;
			}
			return refValueExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003F3A RID: 16186 RVA: 0x0010B94A File Offset: 0x00109B4A
		public RefValueExpressionSyntax WithKeyword(SyntaxToken keyword)
		{
			return this.Update(keyword, this.OpenParenToken, this.Expression, this.Comma, this.Type, this.CloseParenToken);
		}

		// Token: 0x06003F3B RID: 16187 RVA: 0x0010B971 File Offset: 0x00109B71
		public RefValueExpressionSyntax WithOpenParenToken(SyntaxToken openParenToken)
		{
			return this.Update(this.Keyword, openParenToken, this.Expression, this.Comma, this.Type, this.CloseParenToken);
		}

		// Token: 0x06003F3C RID: 16188 RVA: 0x0010B998 File Offset: 0x00109B98
		public RefValueExpressionSyntax WithExpression(ExpressionSyntax expression)
		{
			return this.Update(this.Keyword, this.OpenParenToken, expression, this.Comma, this.Type, this.CloseParenToken);
		}

		// Token: 0x06003F3D RID: 16189 RVA: 0x0010B9BF File Offset: 0x00109BBF
		public RefValueExpressionSyntax WithComma(SyntaxToken comma)
		{
			return this.Update(this.Keyword, this.OpenParenToken, this.Expression, comma, this.Type, this.CloseParenToken);
		}

		// Token: 0x06003F3E RID: 16190 RVA: 0x0010B9E6 File Offset: 0x00109BE6
		public RefValueExpressionSyntax WithType(TypeSyntax type)
		{
			return this.Update(this.Keyword, this.OpenParenToken, this.Expression, this.Comma, type, this.CloseParenToken);
		}

		// Token: 0x06003F3F RID: 16191 RVA: 0x0010BA0D File Offset: 0x00109C0D
		public RefValueExpressionSyntax WithCloseParenToken(SyntaxToken closeParenToken)
		{
			return this.Update(this.Keyword, this.OpenParenToken, this.Expression, this.Comma, this.Type, closeParenToken);
		}

		// Token: 0x0400146F RID: 5231
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expression;

		// Token: 0x04001470 RID: 5232
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax type;
	}
}
