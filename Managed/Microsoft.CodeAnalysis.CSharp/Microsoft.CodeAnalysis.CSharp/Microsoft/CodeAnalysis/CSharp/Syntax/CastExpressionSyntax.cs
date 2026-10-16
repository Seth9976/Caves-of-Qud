using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002BE RID: 702
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class CastExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06003FC6 RID: 16326 RVA: 0x0010C7DA File Offset: 0x0010A9DA
		internal CastExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700108C RID: 4236
		// (get) Token: 0x06003FC7 RID: 16327 RVA: 0x0010C7E5 File Offset: 0x0010A9E5
		public SyntaxToken OpenParenToken
		{
			get
			{
				return new SyntaxToken(this, ((CastExpressionSyntax)base.Green).openParenToken, base.Position, 0);
			}
		}

		// Token: 0x1700108D RID: 4237
		// (get) Token: 0x06003FC8 RID: 16328 RVA: 0x0010C804 File Offset: 0x0010AA04
		public TypeSyntax Type
		{
			get
			{
				return base.GetRed<TypeSyntax>(ref this.type, 1);
			}
		}

		// Token: 0x1700108E RID: 4238
		// (get) Token: 0x06003FC9 RID: 16329 RVA: 0x0010C813 File Offset: 0x0010AA13
		public SyntaxToken CloseParenToken
		{
			get
			{
				return new SyntaxToken(this, ((CastExpressionSyntax)base.Green).closeParenToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x1700108F RID: 4239
		// (get) Token: 0x06003FCA RID: 16330 RVA: 0x0010C839 File Offset: 0x0010AA39
		public ExpressionSyntax Expression
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.expression, 3);
			}
		}

		// Token: 0x06003FCB RID: 16331 RVA: 0x0010C848 File Offset: 0x0010AA48
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			ExpressionSyntax expressionSyntax;
			if (index != 1)
			{
				if (index != 3)
				{
					expressionSyntax = null;
				}
				else
				{
					expressionSyntax = base.GetRed<ExpressionSyntax>(ref this.expression, 3);
				}
			}
			else
			{
				expressionSyntax = base.GetRed<TypeSyntax>(ref this.type, 1);
			}
			return expressionSyntax;
		}

		// Token: 0x06003FCC RID: 16332 RVA: 0x0010C884 File Offset: 0x0010AA84
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			ExpressionSyntax expressionSyntax;
			if (index != 1)
			{
				if (index != 3)
				{
					expressionSyntax = null;
				}
				else
				{
					expressionSyntax = this.expression;
				}
			}
			else
			{
				expressionSyntax = this.type;
			}
			return expressionSyntax;
		}

		// Token: 0x06003FCD RID: 16333 RVA: 0x0010C8B0 File Offset: 0x0010AAB0
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitCastExpression(this);
		}

		// Token: 0x06003FCE RID: 16334 RVA: 0x0010C8B9 File Offset: 0x0010AAB9
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitCastExpression(this);
		}

		// Token: 0x06003FCF RID: 16335 RVA: 0x0010C8C4 File Offset: 0x0010AAC4
		public CastExpressionSyntax Update(SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken, ExpressionSyntax expression)
		{
			if (!(openParenToken != this.OpenParenToken) && type == this.Type && !(closeParenToken != this.CloseParenToken) && expression == this.Expression)
			{
				return this;
			}
			CastExpressionSyntax castExpressionSyntax = SyntaxFactory.CastExpression(openParenToken, type, closeParenToken, expression);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return castExpressionSyntax;
			}
			return castExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003FD0 RID: 16336 RVA: 0x0010C924 File Offset: 0x0010AB24
		public CastExpressionSyntax WithOpenParenToken(SyntaxToken openParenToken)
		{
			return this.Update(openParenToken, this.Type, this.CloseParenToken, this.Expression);
		}

		// Token: 0x06003FD1 RID: 16337 RVA: 0x0010C93F File Offset: 0x0010AB3F
		public CastExpressionSyntax WithType(TypeSyntax type)
		{
			return this.Update(this.OpenParenToken, type, this.CloseParenToken, this.Expression);
		}

		// Token: 0x06003FD2 RID: 16338 RVA: 0x0010C95A File Offset: 0x0010AB5A
		public CastExpressionSyntax WithCloseParenToken(SyntaxToken closeParenToken)
		{
			return this.Update(this.OpenParenToken, this.Type, closeParenToken, this.Expression);
		}

		// Token: 0x06003FD3 RID: 16339 RVA: 0x0010C975 File Offset: 0x0010AB75
		public CastExpressionSyntax WithExpression(ExpressionSyntax expression)
		{
			return this.Update(this.OpenParenToken, this.Type, this.CloseParenToken, expression);
		}

		// Token: 0x0400147E RID: 5246
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax type;

		// Token: 0x0400147F RID: 5247
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expression;
	}
}
