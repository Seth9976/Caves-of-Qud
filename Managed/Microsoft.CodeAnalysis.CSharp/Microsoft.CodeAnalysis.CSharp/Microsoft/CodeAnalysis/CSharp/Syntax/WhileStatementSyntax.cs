using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200028C RID: 652
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class WhileStatementSyntax : StatementSyntax
	{
		// Token: 0x06003D9F RID: 15775 RVA: 0x00108E8A File Offset: 0x0010708A
		public WhileStatementSyntax Update(SyntaxToken whileKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			return this.Update(this.AttributeLists, whileKeyword, openParenToken, condition, closeParenToken, statement);
		}

		// Token: 0x06003DA0 RID: 15776 RVA: 0x00108E9F File Offset: 0x0010709F
		internal WhileStatementSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001010 RID: 4112
		// (get) Token: 0x06003DA1 RID: 15777 RVA: 0x00108EAA File Offset: 0x001070AA
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17001011 RID: 4113
		// (get) Token: 0x06003DA2 RID: 15778 RVA: 0x00108EBE File Offset: 0x001070BE
		public SyntaxToken WhileKeyword
		{
			get
			{
				return new SyntaxToken(this, ((WhileStatementSyntax)base.Green).whileKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17001012 RID: 4114
		// (get) Token: 0x06003DA3 RID: 15779 RVA: 0x00108EE4 File Offset: 0x001070E4
		public SyntaxToken OpenParenToken
		{
			get
			{
				return new SyntaxToken(this, ((WhileStatementSyntax)base.Green).openParenToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17001013 RID: 4115
		// (get) Token: 0x06003DA4 RID: 15780 RVA: 0x00108F0A File Offset: 0x0010710A
		public ExpressionSyntax Condition
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.condition, 3);
			}
		}

		// Token: 0x17001014 RID: 4116
		// (get) Token: 0x06003DA5 RID: 15781 RVA: 0x00108F19 File Offset: 0x00107119
		public SyntaxToken CloseParenToken
		{
			get
			{
				return new SyntaxToken(this, ((WhileStatementSyntax)base.Green).closeParenToken, this.GetChildPosition(4), base.GetChildIndex(4));
			}
		}

		// Token: 0x17001015 RID: 4117
		// (get) Token: 0x06003DA6 RID: 15782 RVA: 0x00108F3F File Offset: 0x0010713F
		public StatementSyntax Statement
		{
			get
			{
				return base.GetRed<StatementSyntax>(ref this.statement, 5);
			}
		}

		// Token: 0x06003DA7 RID: 15783 RVA: 0x00108F50 File Offset: 0x00107150
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 3)
				{
					if (index != 5)
					{
						syntaxNode = null;
					}
					else
					{
						syntaxNode = base.GetRed<StatementSyntax>(ref this.statement, 5);
					}
				}
				else
				{
					syntaxNode = base.GetRed<ExpressionSyntax>(ref this.condition, 3);
				}
			}
			else
			{
				syntaxNode = base.GetRedAtZero(ref this.attributeLists);
			}
			return syntaxNode;
		}

		// Token: 0x06003DA8 RID: 15784 RVA: 0x00108F9C File Offset: 0x0010719C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 3)
				{
					if (index != 5)
					{
						syntaxNode = null;
					}
					else
					{
						syntaxNode = this.statement;
					}
				}
				else
				{
					syntaxNode = this.condition;
				}
			}
			else
			{
				syntaxNode = this.attributeLists;
			}
			return syntaxNode;
		}

		// Token: 0x06003DA9 RID: 15785 RVA: 0x00108FD4 File Offset: 0x001071D4
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitWhileStatement(this);
		}

		// Token: 0x06003DAA RID: 15786 RVA: 0x00108FDD File Offset: 0x001071DD
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitWhileStatement(this);
		}

		// Token: 0x06003DAB RID: 15787 RVA: 0x00108FE8 File Offset: 0x001071E8
		public WhileStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken whileKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			if (!(attributeLists != this.AttributeLists) && !(whileKeyword != this.WhileKeyword) && !(openParenToken != this.OpenParenToken) && condition == this.Condition && !(closeParenToken != this.CloseParenToken) && statement == this.Statement)
			{
				return this;
			}
			WhileStatementSyntax whileStatementSyntax = SyntaxFactory.WhileStatement(attributeLists, whileKeyword, openParenToken, condition, closeParenToken, statement);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return whileStatementSyntax;
			}
			return whileStatementSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003DAC RID: 15788 RVA: 0x0010906A File Offset: 0x0010726A
		internal override StatementSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x06003DAD RID: 15789 RVA: 0x00109073 File Offset: 0x00107273
		public new WhileStatementSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.WhileKeyword, this.OpenParenToken, this.Condition, this.CloseParenToken, this.Statement);
		}

		// Token: 0x06003DAE RID: 15790 RVA: 0x0010909A File Offset: 0x0010729A
		public WhileStatementSyntax WithWhileKeyword(SyntaxToken whileKeyword)
		{
			return this.Update(this.AttributeLists, whileKeyword, this.OpenParenToken, this.Condition, this.CloseParenToken, this.Statement);
		}

		// Token: 0x06003DAF RID: 15791 RVA: 0x001090C1 File Offset: 0x001072C1
		public WhileStatementSyntax WithOpenParenToken(SyntaxToken openParenToken)
		{
			return this.Update(this.AttributeLists, this.WhileKeyword, openParenToken, this.Condition, this.CloseParenToken, this.Statement);
		}

		// Token: 0x06003DB0 RID: 15792 RVA: 0x001090E8 File Offset: 0x001072E8
		public WhileStatementSyntax WithCondition(ExpressionSyntax condition)
		{
			return this.Update(this.AttributeLists, this.WhileKeyword, this.OpenParenToken, condition, this.CloseParenToken, this.Statement);
		}

		// Token: 0x06003DB1 RID: 15793 RVA: 0x0010910F File Offset: 0x0010730F
		public WhileStatementSyntax WithCloseParenToken(SyntaxToken closeParenToken)
		{
			return this.Update(this.AttributeLists, this.WhileKeyword, this.OpenParenToken, this.Condition, closeParenToken, this.Statement);
		}

		// Token: 0x06003DB2 RID: 15794 RVA: 0x00109136 File Offset: 0x00107336
		public WhileStatementSyntax WithStatement(StatementSyntax statement)
		{
			return this.Update(this.AttributeLists, this.WhileKeyword, this.OpenParenToken, this.Condition, this.CloseParenToken, statement);
		}

		// Token: 0x06003DB3 RID: 15795 RVA: 0x0010915D File Offset: 0x0010735D
		internal override StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x06003DB4 RID: 15796 RVA: 0x00109168 File Offset: 0x00107368
		public new WhileStatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x04001442 RID: 5186
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x04001443 RID: 5187
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax condition;

		// Token: 0x04001444 RID: 5188
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private StatementSyntax statement;
	}
}
