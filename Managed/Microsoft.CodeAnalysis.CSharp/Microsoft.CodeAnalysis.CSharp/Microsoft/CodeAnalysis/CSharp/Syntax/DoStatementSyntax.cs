using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000259 RID: 601
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class DoStatementSyntax : StatementSyntax
	{
		// Token: 0x06003933 RID: 14643 RVA: 0x000FCF94 File Offset: 0x000FB194
		public DoStatementSyntax Update(SyntaxToken doKeyword, StatementSyntax statement, SyntaxToken whileKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, doKeyword, statement, whileKeyword, openParenToken, condition, closeParenToken, semicolonToken);
		}

		// Token: 0x06003934 RID: 14644 RVA: 0x000FCFB8 File Offset: 0x000FB1B8
		internal DoStatementSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x06003935 RID: 14645 RVA: 0x000FCFC3 File Offset: 0x000FB1C3
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x06003936 RID: 14646 RVA: 0x000FCFD7 File Offset: 0x000FB1D7
		public SyntaxToken DoKeyword
		{
			get
			{
				return new SyntaxToken(this, ((DoStatementSyntax)base.Green).doKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x06003937 RID: 14647 RVA: 0x000FCFFD File Offset: 0x000FB1FD
		public StatementSyntax Statement
		{
			get
			{
				return base.GetRed<StatementSyntax>(ref this.statement, 2);
			}
		}

		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x06003938 RID: 14648 RVA: 0x000FD00C File Offset: 0x000FB20C
		public SyntaxToken WhileKeyword
		{
			get
			{
				return new SyntaxToken(this, ((DoStatementSyntax)base.Green).whileKeyword, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x06003939 RID: 14649 RVA: 0x000FD032 File Offset: 0x000FB232
		public SyntaxToken OpenParenToken
		{
			get
			{
				return new SyntaxToken(this, ((DoStatementSyntax)base.Green).openParenToken, this.GetChildPosition(4), base.GetChildIndex(4));
			}
		}

		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x0600393A RID: 14650 RVA: 0x000FD058 File Offset: 0x000FB258
		public ExpressionSyntax Condition
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.condition, 5);
			}
		}

		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x0600393B RID: 14651 RVA: 0x000FD067 File Offset: 0x000FB267
		public SyntaxToken CloseParenToken
		{
			get
			{
				return new SyntaxToken(this, ((DoStatementSyntax)base.Green).closeParenToken, this.GetChildPosition(6), base.GetChildIndex(6));
			}
		}

		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x0600393C RID: 14652 RVA: 0x000FD08D File Offset: 0x000FB28D
		public SyntaxToken SemicolonToken
		{
			get
			{
				return new SyntaxToken(this, ((DoStatementSyntax)base.Green).semicolonToken, this.GetChildPosition(7), base.GetChildIndex(7));
			}
		}

		// Token: 0x0600393D RID: 14653 RVA: 0x000FD0B4 File Offset: 0x000FB2B4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 2)
				{
					if (index != 5)
					{
						syntaxNode = null;
					}
					else
					{
						syntaxNode = base.GetRed<ExpressionSyntax>(ref this.condition, 5);
					}
				}
				else
				{
					syntaxNode = base.GetRed<StatementSyntax>(ref this.statement, 2);
				}
			}
			else
			{
				syntaxNode = base.GetRedAtZero(ref this.attributeLists);
			}
			return syntaxNode;
		}

		// Token: 0x0600393E RID: 14654 RVA: 0x000FD100 File Offset: 0x000FB300
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 2)
				{
					if (index != 5)
					{
						syntaxNode = null;
					}
					else
					{
						syntaxNode = this.condition;
					}
				}
				else
				{
					syntaxNode = this.statement;
				}
			}
			else
			{
				syntaxNode = this.attributeLists;
			}
			return syntaxNode;
		}

		// Token: 0x0600393F RID: 14655 RVA: 0x000FD138 File Offset: 0x000FB338
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitDoStatement(this);
		}

		// Token: 0x06003940 RID: 14656 RVA: 0x000FD141 File Offset: 0x000FB341
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitDoStatement(this);
		}

		// Token: 0x06003941 RID: 14657 RVA: 0x000FD14C File Offset: 0x000FB34C
		public DoStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken doKeyword, StatementSyntax statement, SyntaxToken whileKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, SyntaxToken semicolonToken)
		{
			if (!(attributeLists != this.AttributeLists) && !(doKeyword != this.DoKeyword) && statement == this.Statement && !(whileKeyword != this.WhileKeyword) && !(openParenToken != this.OpenParenToken) && condition == this.Condition && !(closeParenToken != this.CloseParenToken) && !(semicolonToken != this.SemicolonToken))
			{
				return this;
			}
			DoStatementSyntax doStatementSyntax = SyntaxFactory.DoStatement(attributeLists, doKeyword, statement, whileKeyword, openParenToken, condition, closeParenToken, semicolonToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return doStatementSyntax;
			}
			return doStatementSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003942 RID: 14658 RVA: 0x000FD1F0 File Offset: 0x000FB3F0
		internal override StatementSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x06003943 RID: 14659 RVA: 0x000FD1FC File Offset: 0x000FB3FC
		public new DoStatementSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.DoKeyword, this.Statement, this.WhileKeyword, this.OpenParenToken, this.Condition, this.CloseParenToken, this.SemicolonToken);
		}

		// Token: 0x06003944 RID: 14660 RVA: 0x000FD23C File Offset: 0x000FB43C
		public DoStatementSyntax WithDoKeyword(SyntaxToken doKeyword)
		{
			return this.Update(this.AttributeLists, doKeyword, this.Statement, this.WhileKeyword, this.OpenParenToken, this.Condition, this.CloseParenToken, this.SemicolonToken);
		}

		// Token: 0x06003945 RID: 14661 RVA: 0x000FD27C File Offset: 0x000FB47C
		public DoStatementSyntax WithStatement(StatementSyntax statement)
		{
			return this.Update(this.AttributeLists, this.DoKeyword, statement, this.WhileKeyword, this.OpenParenToken, this.Condition, this.CloseParenToken, this.SemicolonToken);
		}

		// Token: 0x06003946 RID: 14662 RVA: 0x000FD2BC File Offset: 0x000FB4BC
		public DoStatementSyntax WithWhileKeyword(SyntaxToken whileKeyword)
		{
			return this.Update(this.AttributeLists, this.DoKeyword, this.Statement, whileKeyword, this.OpenParenToken, this.Condition, this.CloseParenToken, this.SemicolonToken);
		}

		// Token: 0x06003947 RID: 14663 RVA: 0x000FD2FC File Offset: 0x000FB4FC
		public DoStatementSyntax WithOpenParenToken(SyntaxToken openParenToken)
		{
			return this.Update(this.AttributeLists, this.DoKeyword, this.Statement, this.WhileKeyword, openParenToken, this.Condition, this.CloseParenToken, this.SemicolonToken);
		}

		// Token: 0x06003948 RID: 14664 RVA: 0x000FD33C File Offset: 0x000FB53C
		public DoStatementSyntax WithCondition(ExpressionSyntax condition)
		{
			return this.Update(this.AttributeLists, this.DoKeyword, this.Statement, this.WhileKeyword, this.OpenParenToken, condition, this.CloseParenToken, this.SemicolonToken);
		}

		// Token: 0x06003949 RID: 14665 RVA: 0x000FD37C File Offset: 0x000FB57C
		public DoStatementSyntax WithCloseParenToken(SyntaxToken closeParenToken)
		{
			return this.Update(this.AttributeLists, this.DoKeyword, this.Statement, this.WhileKeyword, this.OpenParenToken, this.Condition, closeParenToken, this.SemicolonToken);
		}

		// Token: 0x0600394A RID: 14666 RVA: 0x000FD3BC File Offset: 0x000FB5BC
		public DoStatementSyntax WithSemicolonToken(SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, this.DoKeyword, this.Statement, this.WhileKeyword, this.OpenParenToken, this.Condition, this.CloseParenToken, semicolonToken);
		}

		// Token: 0x0600394B RID: 14667 RVA: 0x000FD3FA File Offset: 0x000FB5FA
		internal override StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x0600394C RID: 14668 RVA: 0x000FD404 File Offset: 0x000FB604
		public new DoStatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x040013B9 RID: 5049
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x040013BA RID: 5050
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private StatementSyntax statement;

		// Token: 0x040013BB RID: 5051
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax condition;
	}
}
