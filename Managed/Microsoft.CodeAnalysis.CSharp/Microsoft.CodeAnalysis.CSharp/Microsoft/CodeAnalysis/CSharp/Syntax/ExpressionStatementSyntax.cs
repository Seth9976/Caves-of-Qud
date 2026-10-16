using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200025D RID: 605
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ExpressionStatementSyntax : StatementSyntax
	{
		// Token: 0x17000F28 RID: 3880
		// (get) Token: 0x06003993 RID: 14739 RVA: 0x000FDD50 File Offset: 0x000FBF50
		public bool AllowsAnyExpression
		{
			get
			{
				SyntaxToken semicolonToken = this.SemicolonToken;
				return semicolonToken.IsMissing && !semicolonToken.ContainsDiagnostics;
			}
		}

		// Token: 0x06003994 RID: 14740 RVA: 0x000FDD79 File Offset: 0x000FBF79
		public ExpressionStatementSyntax Update(ExpressionSyntax expression, SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, expression, semicolonToken);
		}

		// Token: 0x06003995 RID: 14741 RVA: 0x000FDD89 File Offset: 0x000FBF89
		internal ExpressionStatementSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x06003996 RID: 14742 RVA: 0x000FDD94 File Offset: 0x000FBF94
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x06003997 RID: 14743 RVA: 0x000FDDA8 File Offset: 0x000FBFA8
		public ExpressionSyntax Expression
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.expression, 1);
			}
		}

		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x06003998 RID: 14744 RVA: 0x000FDDB7 File Offset: 0x000FBFB7
		public SyntaxToken SemicolonToken
		{
			get
			{
				return new SyntaxToken(this, ((ExpressionStatementSyntax)base.Green).semicolonToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x06003999 RID: 14745 RVA: 0x000FDDE0 File Offset: 0x000FBFE0
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
					syntaxNode = base.GetRed<ExpressionSyntax>(ref this.expression, 1);
				}
			}
			else
			{
				syntaxNode = base.GetRedAtZero(ref this.attributeLists);
			}
			return syntaxNode;
		}

		// Token: 0x0600399A RID: 14746 RVA: 0x000FDE18 File Offset: 0x000FC018
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
					syntaxNode = this.expression;
				}
			}
			else
			{
				syntaxNode = this.attributeLists;
			}
			return syntaxNode;
		}

		// Token: 0x0600399B RID: 14747 RVA: 0x000FDE43 File Offset: 0x000FC043
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitExpressionStatement(this);
		}

		// Token: 0x0600399C RID: 14748 RVA: 0x000FDE4C File Offset: 0x000FC04C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitExpressionStatement(this);
		}

		// Token: 0x0600399D RID: 14749 RVA: 0x000FDE58 File Offset: 0x000FC058
		public ExpressionStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, ExpressionSyntax expression, SyntaxToken semicolonToken)
		{
			if (!(attributeLists != this.AttributeLists) && expression == this.Expression && !(semicolonToken != this.SemicolonToken))
			{
				return this;
			}
			ExpressionStatementSyntax expressionStatementSyntax = SyntaxFactory.ExpressionStatement(attributeLists, expression, semicolonToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return expressionStatementSyntax;
			}
			return expressionStatementSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600399E RID: 14750 RVA: 0x000FDEAC File Offset: 0x000FC0AC
		internal override StatementSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x0600399F RID: 14751 RVA: 0x000FDEB5 File Offset: 0x000FC0B5
		public new ExpressionStatementSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.Expression, this.SemicolonToken);
		}

		// Token: 0x060039A0 RID: 14752 RVA: 0x000FDECA File Offset: 0x000FC0CA
		public ExpressionStatementSyntax WithExpression(ExpressionSyntax expression)
		{
			return this.Update(this.AttributeLists, expression, this.SemicolonToken);
		}

		// Token: 0x060039A1 RID: 14753 RVA: 0x000FDEDF File Offset: 0x000FC0DF
		public ExpressionStatementSyntax WithSemicolonToken(SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, this.Expression, semicolonToken);
		}

		// Token: 0x060039A2 RID: 14754 RVA: 0x000FDEF4 File Offset: 0x000FC0F4
		internal override StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x060039A3 RID: 14755 RVA: 0x000FDF00 File Offset: 0x000FC100
		public new ExpressionStatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x040013C3 RID: 5059
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x040013C4 RID: 5060
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expression;
	}
}
