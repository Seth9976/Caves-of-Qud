using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002DB RID: 731
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ThrowExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06004130 RID: 16688 RVA: 0x0010F14E File Offset: 0x0010D34E
		internal ThrowExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170010E4 RID: 4324
		// (get) Token: 0x06004131 RID: 16689 RVA: 0x0010F159 File Offset: 0x0010D359
		public SyntaxToken ThrowKeyword
		{
			get
			{
				return new SyntaxToken(this, ((ThrowExpressionSyntax)base.Green).throwKeyword, base.Position, 0);
			}
		}

		// Token: 0x170010E5 RID: 4325
		// (get) Token: 0x06004132 RID: 16690 RVA: 0x0010F178 File Offset: 0x0010D378
		public ExpressionSyntax Expression
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.expression, 1);
			}
		}

		// Token: 0x06004133 RID: 16691 RVA: 0x0010F187 File Offset: 0x0010D387
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed<ExpressionSyntax>(ref this.expression, 1);
		}

		// Token: 0x06004134 RID: 16692 RVA: 0x0010F19C File Offset: 0x0010D39C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.expression;
		}

		// Token: 0x06004135 RID: 16693 RVA: 0x0010F1AA File Offset: 0x0010D3AA
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitThrowExpression(this);
		}

		// Token: 0x06004136 RID: 16694 RVA: 0x0010F1B3 File Offset: 0x0010D3B3
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitThrowExpression(this);
		}

		// Token: 0x06004137 RID: 16695 RVA: 0x0010F1BC File Offset: 0x0010D3BC
		public ThrowExpressionSyntax Update(SyntaxToken throwKeyword, ExpressionSyntax expression)
		{
			if (!(throwKeyword != this.ThrowKeyword) && expression == this.Expression)
			{
				return this;
			}
			ThrowExpressionSyntax throwExpressionSyntax = SyntaxFactory.ThrowExpression(throwKeyword, expression);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return throwExpressionSyntax;
			}
			return throwExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004138 RID: 16696 RVA: 0x0010F201 File Offset: 0x0010D401
		public ThrowExpressionSyntax WithThrowKeyword(SyntaxToken throwKeyword)
		{
			return this.Update(throwKeyword, this.Expression);
		}

		// Token: 0x06004139 RID: 16697 RVA: 0x0010F210 File Offset: 0x0010D410
		public ThrowExpressionSyntax WithExpression(ExpressionSyntax expression)
		{
			return this.Update(this.ThrowKeyword, expression);
		}

		// Token: 0x040014A7 RID: 5287
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expression;
	}
}
