using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200027A RID: 634
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ReturnStatementSyntax : StatementSyntax
	{
		// Token: 0x06003C62 RID: 15458 RVA: 0x001053D7 File Offset: 0x001035D7
		public ReturnStatementSyntax Update(SyntaxToken returnKeyword, ExpressionSyntax expression, SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, returnKeyword, expression, semicolonToken);
		}

		// Token: 0x06003C63 RID: 15459 RVA: 0x001053E8 File Offset: 0x001035E8
		internal ReturnStatementSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000FD3 RID: 4051
		// (get) Token: 0x06003C64 RID: 15460 RVA: 0x001053F3 File Offset: 0x001035F3
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17000FD4 RID: 4052
		// (get) Token: 0x06003C65 RID: 15461 RVA: 0x00105407 File Offset: 0x00103607
		public SyntaxToken ReturnKeyword
		{
			get
			{
				return new SyntaxToken(this, ((ReturnStatementSyntax)base.Green).returnKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17000FD5 RID: 4053
		// (get) Token: 0x06003C66 RID: 15462 RVA: 0x0010542D File Offset: 0x0010362D
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public ExpressionSyntax Expression
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.expression, 2);
			}
		}

		// Token: 0x17000FD6 RID: 4054
		// (get) Token: 0x06003C67 RID: 15463 RVA: 0x0010543C File Offset: 0x0010363C
		public SyntaxToken SemicolonToken
		{
			get
			{
				return new SyntaxToken(this, ((ReturnStatementSyntax)base.Green).semicolonToken, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x06003C68 RID: 15464 RVA: 0x00105464 File Offset: 0x00103664
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

		// Token: 0x06003C69 RID: 15465 RVA: 0x0010549C File Offset: 0x0010369C
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

		// Token: 0x06003C6A RID: 15466 RVA: 0x001054C7 File Offset: 0x001036C7
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitReturnStatement(this);
		}

		// Token: 0x06003C6B RID: 15467 RVA: 0x001054D0 File Offset: 0x001036D0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitReturnStatement(this);
		}

		// Token: 0x06003C6C RID: 15468 RVA: 0x001054DC File Offset: 0x001036DC
		public ReturnStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken returnKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expression, SyntaxToken semicolonToken)
		{
			if (!(attributeLists != this.AttributeLists) && !(returnKeyword != this.ReturnKeyword) && expression == this.Expression && !(semicolonToken != this.SemicolonToken))
			{
				return this;
			}
			ReturnStatementSyntax returnStatementSyntax = SyntaxFactory.ReturnStatement(attributeLists, returnKeyword, expression, semicolonToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return returnStatementSyntax;
			}
			return returnStatementSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003C6D RID: 15469 RVA: 0x00105541 File Offset: 0x00103741
		internal override StatementSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x06003C6E RID: 15470 RVA: 0x0010554A File Offset: 0x0010374A
		public new ReturnStatementSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.ReturnKeyword, this.Expression, this.SemicolonToken);
		}

		// Token: 0x06003C6F RID: 15471 RVA: 0x00105565 File Offset: 0x00103765
		public ReturnStatementSyntax WithReturnKeyword(SyntaxToken returnKeyword)
		{
			return this.Update(this.AttributeLists, returnKeyword, this.Expression, this.SemicolonToken);
		}

		// Token: 0x06003C70 RID: 15472 RVA: 0x00105580 File Offset: 0x00103780
		public ReturnStatementSyntax WithExpression([global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expression)
		{
			return this.Update(this.AttributeLists, this.ReturnKeyword, expression, this.SemicolonToken);
		}

		// Token: 0x06003C71 RID: 15473 RVA: 0x0010559B File Offset: 0x0010379B
		public ReturnStatementSyntax WithSemicolonToken(SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, this.ReturnKeyword, this.Expression, semicolonToken);
		}

		// Token: 0x06003C72 RID: 15474 RVA: 0x001055B6 File Offset: 0x001037B6
		internal override StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x06003C73 RID: 15475 RVA: 0x001055C0 File Offset: 0x001037C0
		public new ReturnStatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x0400141C RID: 5148
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x0400141D RID: 5149
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expression;
	}
}
