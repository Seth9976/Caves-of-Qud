using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000266 RID: 614
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class IfStatementSyntax : StatementSyntax
	{
		// Token: 0x06003A66 RID: 14950 RVA: 0x000FFD31 File Offset: 0x000FDF31
		public IfStatementSyntax Update(SyntaxToken ifKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, StatementSyntax statement, ElseClauseSyntax @else)
		{
			return this.Update(this.AttributeLists, ifKeyword, openParenToken, condition, closeParenToken, statement, @else);
		}

		// Token: 0x06003A67 RID: 14951 RVA: 0x000FFD48 File Offset: 0x000FDF48
		internal IfStatementSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000F5C RID: 3932
		// (get) Token: 0x06003A68 RID: 14952 RVA: 0x000FFD53 File Offset: 0x000FDF53
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x06003A69 RID: 14953 RVA: 0x000FFD67 File Offset: 0x000FDF67
		public SyntaxToken IfKeyword
		{
			get
			{
				return new SyntaxToken(this, ((IfStatementSyntax)base.Green).ifKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17000F5E RID: 3934
		// (get) Token: 0x06003A6A RID: 14954 RVA: 0x000FFD8D File Offset: 0x000FDF8D
		public SyntaxToken OpenParenToken
		{
			get
			{
				return new SyntaxToken(this, ((IfStatementSyntax)base.Green).openParenToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x06003A6B RID: 14955 RVA: 0x000FFDB3 File Offset: 0x000FDFB3
		public ExpressionSyntax Condition
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.condition, 3);
			}
		}

		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x06003A6C RID: 14956 RVA: 0x000FFDC2 File Offset: 0x000FDFC2
		public SyntaxToken CloseParenToken
		{
			get
			{
				return new SyntaxToken(this, ((IfStatementSyntax)base.Green).closeParenToken, this.GetChildPosition(4), base.GetChildIndex(4));
			}
		}

		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x06003A6D RID: 14957 RVA: 0x000FFDE8 File Offset: 0x000FDFE8
		public StatementSyntax Statement
		{
			get
			{
				return base.GetRed<StatementSyntax>(ref this.statement, 5);
			}
		}

		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x06003A6E RID: 14958 RVA: 0x000FFDF7 File Offset: 0x000FDFF7
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public ElseClauseSyntax Else
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<ElseClauseSyntax>(ref this.@else, 6);
			}
		}

		// Token: 0x06003A6F RID: 14959 RVA: 0x000FFE08 File Offset: 0x000FE008
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			switch (index)
			{
			case 0:
				return base.GetRedAtZero(ref this.attributeLists);
			case 3:
				return base.GetRed<ExpressionSyntax>(ref this.condition, 3);
			case 5:
				return base.GetRed<StatementSyntax>(ref this.statement, 5);
			case 6:
				return base.GetRed<ElseClauseSyntax>(ref this.@else, 6);
			}
			return null;
		}

		// Token: 0x06003A70 RID: 14960 RVA: 0x000FFE7C File Offset: 0x000FE07C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			switch (index)
			{
			case 0:
				return this.attributeLists;
			case 3:
				return this.condition;
			case 5:
				return this.statement;
			case 6:
				return this.@else;
			}
			return null;
		}

		// Token: 0x06003A71 RID: 14961 RVA: 0x000FFED4 File Offset: 0x000FE0D4
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitIfStatement(this);
		}

		// Token: 0x06003A72 RID: 14962 RVA: 0x000FFEDD File Offset: 0x000FE0DD
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitIfStatement(this);
		}

		// Token: 0x06003A73 RID: 14963 RVA: 0x000FFEE8 File Offset: 0x000FE0E8
		public IfStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken ifKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, StatementSyntax statement, [global::System.Runtime.CompilerServices.Nullable(2)] ElseClauseSyntax @else)
		{
			if (!(attributeLists != this.AttributeLists) && !(ifKeyword != this.IfKeyword) && !(openParenToken != this.OpenParenToken) && condition == this.Condition && !(closeParenToken != this.CloseParenToken) && statement == this.Statement && @else == this.Else)
			{
				return this;
			}
			IfStatementSyntax ifStatementSyntax = SyntaxFactory.IfStatement(attributeLists, ifKeyword, openParenToken, condition, closeParenToken, statement, @else);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return ifStatementSyntax;
			}
			return ifStatementSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003A74 RID: 14964 RVA: 0x000FFF76 File Offset: 0x000FE176
		internal override StatementSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x06003A75 RID: 14965 RVA: 0x000FFF7F File Offset: 0x000FE17F
		public new IfStatementSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.IfKeyword, this.OpenParenToken, this.Condition, this.CloseParenToken, this.Statement, this.Else);
		}

		// Token: 0x06003A76 RID: 14966 RVA: 0x000FFFAC File Offset: 0x000FE1AC
		public IfStatementSyntax WithIfKeyword(SyntaxToken ifKeyword)
		{
			return this.Update(this.AttributeLists, ifKeyword, this.OpenParenToken, this.Condition, this.CloseParenToken, this.Statement, this.Else);
		}

		// Token: 0x06003A77 RID: 14967 RVA: 0x000FFFD9 File Offset: 0x000FE1D9
		public IfStatementSyntax WithOpenParenToken(SyntaxToken openParenToken)
		{
			return this.Update(this.AttributeLists, this.IfKeyword, openParenToken, this.Condition, this.CloseParenToken, this.Statement, this.Else);
		}

		// Token: 0x06003A78 RID: 14968 RVA: 0x00100006 File Offset: 0x000FE206
		public IfStatementSyntax WithCondition(ExpressionSyntax condition)
		{
			return this.Update(this.AttributeLists, this.IfKeyword, this.OpenParenToken, condition, this.CloseParenToken, this.Statement, this.Else);
		}

		// Token: 0x06003A79 RID: 14969 RVA: 0x00100033 File Offset: 0x000FE233
		public IfStatementSyntax WithCloseParenToken(SyntaxToken closeParenToken)
		{
			return this.Update(this.AttributeLists, this.IfKeyword, this.OpenParenToken, this.Condition, closeParenToken, this.Statement, this.Else);
		}

		// Token: 0x06003A7A RID: 14970 RVA: 0x00100060 File Offset: 0x000FE260
		public IfStatementSyntax WithStatement(StatementSyntax statement)
		{
			return this.Update(this.AttributeLists, this.IfKeyword, this.OpenParenToken, this.Condition, this.CloseParenToken, statement, this.Else);
		}

		// Token: 0x06003A7B RID: 14971 RVA: 0x0010008D File Offset: 0x000FE28D
		public IfStatementSyntax WithElse([global::System.Runtime.CompilerServices.Nullable(2)] ElseClauseSyntax @else)
		{
			return this.Update(this.AttributeLists, this.IfKeyword, this.OpenParenToken, this.Condition, this.CloseParenToken, this.Statement, @else);
		}

		// Token: 0x06003A7C RID: 14972 RVA: 0x001000BA File Offset: 0x000FE2BA
		internal override StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x06003A7D RID: 14973 RVA: 0x001000C4 File Offset: 0x000FE2C4
		public new IfStatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x040013DB RID: 5083
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x040013DC RID: 5084
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax condition;

		// Token: 0x040013DD RID: 5085
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private StatementSyntax statement;

		// Token: 0x040013DE RID: 5086
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ElseClauseSyntax @else;
	}
}
