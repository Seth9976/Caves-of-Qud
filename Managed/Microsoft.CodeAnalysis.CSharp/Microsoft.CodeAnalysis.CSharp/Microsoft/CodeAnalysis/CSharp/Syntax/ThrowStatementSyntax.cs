using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000285 RID: 645
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ThrowStatementSyntax : StatementSyntax
	{
		// Token: 0x06003D12 RID: 15634 RVA: 0x00107E15 File Offset: 0x00106015
		public ThrowStatementSyntax Update(SyntaxToken throwKeyword, ExpressionSyntax expression, SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, throwKeyword, expression, semicolonToken);
		}

		// Token: 0x06003D13 RID: 15635 RVA: 0x00107E26 File Offset: 0x00106026
		internal ThrowStatementSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000FEC RID: 4076
		// (get) Token: 0x06003D14 RID: 15636 RVA: 0x00107E31 File Offset: 0x00106031
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17000FED RID: 4077
		// (get) Token: 0x06003D15 RID: 15637 RVA: 0x00107E45 File Offset: 0x00106045
		public SyntaxToken ThrowKeyword
		{
			get
			{
				return new SyntaxToken(this, ((ThrowStatementSyntax)base.Green).throwKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17000FEE RID: 4078
		// (get) Token: 0x06003D16 RID: 15638 RVA: 0x00107E6B File Offset: 0x0010606B
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public ExpressionSyntax Expression
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.expression, 2);
			}
		}

		// Token: 0x17000FEF RID: 4079
		// (get) Token: 0x06003D17 RID: 15639 RVA: 0x00107E7A File Offset: 0x0010607A
		public SyntaxToken SemicolonToken
		{
			get
			{
				return new SyntaxToken(this, ((ThrowStatementSyntax)base.Green).semicolonToken, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x06003D18 RID: 15640 RVA: 0x00107EA0 File Offset: 0x001060A0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 2)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = base.GetRed<ExpressionSyntax>(ref this.expression, 2);
				}
			}
			else
			{
				syntaxNode = base.GetRedAtZero(ref this.attributeLists);
			}
			return syntaxNode;
		}

		// Token: 0x06003D19 RID: 15641 RVA: 0x00107ED8 File Offset: 0x001060D8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 2)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = this.expression;
				}
			}
			else
			{
				syntaxNode = this.attributeLists;
			}
			return syntaxNode;
		}

		// Token: 0x06003D1A RID: 15642 RVA: 0x00107F03 File Offset: 0x00106103
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitThrowStatement(this);
		}

		// Token: 0x06003D1B RID: 15643 RVA: 0x00107F0C File Offset: 0x0010610C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitThrowStatement(this);
		}

		// Token: 0x06003D1C RID: 15644 RVA: 0x00107F18 File Offset: 0x00106118
		public ThrowStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken throwKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expression, SyntaxToken semicolonToken)
		{
			if (!(attributeLists != this.AttributeLists) && !(throwKeyword != this.ThrowKeyword) && expression == this.Expression && !(semicolonToken != this.SemicolonToken))
			{
				return this;
			}
			ThrowStatementSyntax throwStatementSyntax = SyntaxFactory.ThrowStatement(attributeLists, throwKeyword, expression, semicolonToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return throwStatementSyntax;
			}
			return throwStatementSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003D1D RID: 15645 RVA: 0x00107F7D File Offset: 0x0010617D
		internal override StatementSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x06003D1E RID: 15646 RVA: 0x00107F86 File Offset: 0x00106186
		public new ThrowStatementSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.ThrowKeyword, this.Expression, this.SemicolonToken);
		}

		// Token: 0x06003D1F RID: 15647 RVA: 0x00107FA1 File Offset: 0x001061A1
		public ThrowStatementSyntax WithThrowKeyword(SyntaxToken throwKeyword)
		{
			return this.Update(this.AttributeLists, throwKeyword, this.Expression, this.SemicolonToken);
		}

		// Token: 0x06003D20 RID: 15648 RVA: 0x00107FBC File Offset: 0x001061BC
		public ThrowStatementSyntax WithExpression([global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expression)
		{
			return this.Update(this.AttributeLists, this.ThrowKeyword, expression, this.SemicolonToken);
		}

		// Token: 0x06003D21 RID: 15649 RVA: 0x00107FD7 File Offset: 0x001061D7
		public ThrowStatementSyntax WithSemicolonToken(SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, this.ThrowKeyword, this.Expression, semicolonToken);
		}

		// Token: 0x06003D22 RID: 15650 RVA: 0x00107FF2 File Offset: 0x001061F2
		internal override StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x06003D23 RID: 15651 RVA: 0x00107FFC File Offset: 0x001061FC
		public new ThrowStatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x04001434 RID: 5172
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x04001435 RID: 5173
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expression;
	}
}
