using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000264 RID: 612
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class GotoStatementSyntax : StatementSyntax
	{
		// Token: 0x06003A48 RID: 14920 RVA: 0x000FF9E2 File Offset: 0x000FDBE2
		public GotoStatementSyntax Update(SyntaxToken gotoKeyword, SyntaxToken caseOrDefaultKeyword, ExpressionSyntax expression, SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, gotoKeyword, caseOrDefaultKeyword, expression, semicolonToken);
		}

		// Token: 0x06003A49 RID: 14921 RVA: 0x000FF9F5 File Offset: 0x000FDBF5
		internal GotoStatementSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x06003A4A RID: 14922 RVA: 0x000FFA00 File Offset: 0x000FDC00
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x06003A4B RID: 14923 RVA: 0x000FFA14 File Offset: 0x000FDC14
		public SyntaxToken GotoKeyword
		{
			get
			{
				return new SyntaxToken(this, ((GotoStatementSyntax)base.Green).gotoKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x06003A4C RID: 14924 RVA: 0x000FFA3C File Offset: 0x000FDC3C
		public SyntaxToken CaseOrDefaultKeyword
		{
			get
			{
				SyntaxToken caseOrDefaultKeyword = ((GotoStatementSyntax)base.Green).caseOrDefaultKeyword;
				if (caseOrDefaultKeyword == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, caseOrDefaultKeyword, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x06003A4D RID: 14925 RVA: 0x000FFA7C File Offset: 0x000FDC7C
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public ExpressionSyntax Expression
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.expression, 3);
			}
		}

		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x06003A4E RID: 14926 RVA: 0x000FFA8B File Offset: 0x000FDC8B
		public SyntaxToken SemicolonToken
		{
			get
			{
				return new SyntaxToken(this, ((GotoStatementSyntax)base.Green).semicolonToken, this.GetChildPosition(4), base.GetChildIndex(4));
			}
		}

		// Token: 0x06003A4F RID: 14927 RVA: 0x000FFAB4 File Offset: 0x000FDCB4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 3)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = base.GetRed<ExpressionSyntax>(ref this.expression, 3);
				}
			}
			else
			{
				syntaxNode = base.GetRedAtZero(ref this.attributeLists);
			}
			return syntaxNode;
		}

		// Token: 0x06003A50 RID: 14928 RVA: 0x000FFAEC File Offset: 0x000FDCEC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 3)
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

		// Token: 0x06003A51 RID: 14929 RVA: 0x000FFB17 File Offset: 0x000FDD17
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitGotoStatement(this);
		}

		// Token: 0x06003A52 RID: 14930 RVA: 0x000FFB20 File Offset: 0x000FDD20
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitGotoStatement(this);
		}

		// Token: 0x06003A53 RID: 14931 RVA: 0x000FFB2C File Offset: 0x000FDD2C
		public GotoStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken gotoKeyword, SyntaxToken caseOrDefaultKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expression, SyntaxToken semicolonToken)
		{
			if (!(attributeLists != this.AttributeLists) && !(gotoKeyword != this.GotoKeyword) && !(caseOrDefaultKeyword != this.CaseOrDefaultKeyword) && expression == this.Expression && !(semicolonToken != this.SemicolonToken))
			{
				return this;
			}
			GotoStatementSyntax gotoStatementSyntax = SyntaxFactory.GotoStatement(base.Kind(), attributeLists, gotoKeyword, caseOrDefaultKeyword, expression, semicolonToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return gotoStatementSyntax;
			}
			return gotoStatementSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003A54 RID: 14932 RVA: 0x000FFBA8 File Offset: 0x000FDDA8
		internal override StatementSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x06003A55 RID: 14933 RVA: 0x000FFBB1 File Offset: 0x000FDDB1
		public new GotoStatementSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.GotoKeyword, this.CaseOrDefaultKeyword, this.Expression, this.SemicolonToken);
		}

		// Token: 0x06003A56 RID: 14934 RVA: 0x000FFBD2 File Offset: 0x000FDDD2
		public GotoStatementSyntax WithGotoKeyword(SyntaxToken gotoKeyword)
		{
			return this.Update(this.AttributeLists, gotoKeyword, this.CaseOrDefaultKeyword, this.Expression, this.SemicolonToken);
		}

		// Token: 0x06003A57 RID: 14935 RVA: 0x000FFBF3 File Offset: 0x000FDDF3
		public GotoStatementSyntax WithCaseOrDefaultKeyword(SyntaxToken caseOrDefaultKeyword)
		{
			return this.Update(this.AttributeLists, this.GotoKeyword, caseOrDefaultKeyword, this.Expression, this.SemicolonToken);
		}

		// Token: 0x06003A58 RID: 14936 RVA: 0x000FFC14 File Offset: 0x000FDE14
		public GotoStatementSyntax WithExpression([global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expression)
		{
			return this.Update(this.AttributeLists, this.GotoKeyword, this.CaseOrDefaultKeyword, expression, this.SemicolonToken);
		}

		// Token: 0x06003A59 RID: 14937 RVA: 0x000FFC35 File Offset: 0x000FDE35
		public GotoStatementSyntax WithSemicolonToken(SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, this.GotoKeyword, this.CaseOrDefaultKeyword, this.Expression, semicolonToken);
		}

		// Token: 0x06003A5A RID: 14938 RVA: 0x000FFC56 File Offset: 0x000FDE56
		internal override StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x06003A5B RID: 14939 RVA: 0x000FFC60 File Offset: 0x000FDE60
		public new GotoStatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x040013D9 RID: 5081
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x040013DA RID: 5082
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expression;
	}
}
