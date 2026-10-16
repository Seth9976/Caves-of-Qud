using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200025F RID: 607
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ForEachStatementSyntax : CommonForEachStatementSyntax
	{
		// Token: 0x060039BB RID: 14779 RVA: 0x000FE258 File Offset: 0x000FC458
		public ForEachStatementSyntax Update(SyntaxToken forEachKeyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			return this.Update(this.AwaitKeyword, forEachKeyword, openParenToken, type, identifier, inKeyword, expression, closeParenToken, statement);
		}

		// Token: 0x060039BC RID: 14780 RVA: 0x000FE280 File Offset: 0x000FC480
		public ForEachStatementSyntax Update(SyntaxToken awaitKeyword, SyntaxToken forEachKeyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			return this.Update(this.AttributeLists, awaitKeyword, forEachKeyword, openParenToken, type, identifier, inKeyword, expression, closeParenToken, statement);
		}

		// Token: 0x060039BD RID: 14781 RVA: 0x000FE2A8 File Offset: 0x000FC4A8
		internal ForEachStatementSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x060039BE RID: 14782 RVA: 0x000FE2B3 File Offset: 0x000FC4B3
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x060039BF RID: 14783 RVA: 0x000FE2C8 File Offset: 0x000FC4C8
		public override SyntaxToken AwaitKeyword
		{
			get
			{
				SyntaxToken awaitKeyword = ((ForEachStatementSyntax)base.Green).awaitKeyword;
				if (awaitKeyword == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, awaitKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x060039C0 RID: 14784 RVA: 0x000FE308 File Offset: 0x000FC508
		public override SyntaxToken ForEachKeyword
		{
			get
			{
				return new SyntaxToken(this, ((ForEachStatementSyntax)base.Green).forEachKeyword, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x060039C1 RID: 14785 RVA: 0x000FE32E File Offset: 0x000FC52E
		public override SyntaxToken OpenParenToken
		{
			get
			{
				return new SyntaxToken(this, ((ForEachStatementSyntax)base.Green).openParenToken, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x060039C2 RID: 14786 RVA: 0x000FE354 File Offset: 0x000FC554
		public TypeSyntax Type
		{
			get
			{
				return base.GetRed<TypeSyntax>(ref this.type, 4);
			}
		}

		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x060039C3 RID: 14787 RVA: 0x000FE363 File Offset: 0x000FC563
		public SyntaxToken Identifier
		{
			get
			{
				return new SyntaxToken(this, ((ForEachStatementSyntax)base.Green).identifier, this.GetChildPosition(5), base.GetChildIndex(5));
			}
		}

		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x060039C4 RID: 14788 RVA: 0x000FE389 File Offset: 0x000FC589
		public override SyntaxToken InKeyword
		{
			get
			{
				return new SyntaxToken(this, ((ForEachStatementSyntax)base.Green).inKeyword, this.GetChildPosition(6), base.GetChildIndex(6));
			}
		}

		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x060039C5 RID: 14789 RVA: 0x000FE3AF File Offset: 0x000FC5AF
		public override ExpressionSyntax Expression
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.expression, 7);
			}
		}

		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x060039C6 RID: 14790 RVA: 0x000FE3BE File Offset: 0x000FC5BE
		public override SyntaxToken CloseParenToken
		{
			get
			{
				return new SyntaxToken(this, ((ForEachStatementSyntax)base.Green).closeParenToken, this.GetChildPosition(8), base.GetChildIndex(8));
			}
		}

		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x060039C7 RID: 14791 RVA: 0x000FE3E4 File Offset: 0x000FC5E4
		public override StatementSyntax Statement
		{
			get
			{
				return base.GetRed<StatementSyntax>(ref this.statement, 9);
			}
		}

		// Token: 0x060039C8 RID: 14792 RVA: 0x000FE3F4 File Offset: 0x000FC5F4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index <= 4)
			{
				if (index == 0)
				{
					return base.GetRedAtZero(ref this.attributeLists);
				}
				if (index == 4)
				{
					return base.GetRed<TypeSyntax>(ref this.type, 4);
				}
			}
			else
			{
				if (index == 7)
				{
					return base.GetRed<ExpressionSyntax>(ref this.expression, 7);
				}
				if (index == 9)
				{
					return base.GetRed<StatementSyntax>(ref this.statement, 9);
				}
			}
			return null;
		}

		// Token: 0x060039C9 RID: 14793 RVA: 0x000FE45C File Offset: 0x000FC65C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index <= 4)
			{
				if (index == 0)
				{
					return this.attributeLists;
				}
				if (index == 4)
				{
					return this.type;
				}
			}
			else
			{
				if (index == 7)
				{
					return this.expression;
				}
				if (index == 9)
				{
					return this.statement;
				}
			}
			return null;
		}

		// Token: 0x060039CA RID: 14794 RVA: 0x000FE4A8 File Offset: 0x000FC6A8
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitForEachStatement(this);
		}

		// Token: 0x060039CB RID: 14795 RVA: 0x000FE4B1 File Offset: 0x000FC6B1
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitForEachStatement(this);
		}

		// Token: 0x060039CC RID: 14796 RVA: 0x000FE4BC File Offset: 0x000FC6BC
		public ForEachStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken awaitKeyword, SyntaxToken forEachKeyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			if (!(attributeLists != this.AttributeLists) && !(awaitKeyword != this.AwaitKeyword) && !(forEachKeyword != this.ForEachKeyword) && !(openParenToken != this.OpenParenToken) && type == this.Type && !(identifier != this.Identifier) && !(inKeyword != this.InKeyword) && expression == this.Expression && !(closeParenToken != this.CloseParenToken) && statement == this.Statement)
			{
				return this;
			}
			ForEachStatementSyntax forEachStatementSyntax = SyntaxFactory.ForEachStatement(attributeLists, awaitKeyword, forEachKeyword, openParenToken, type, identifier, inKeyword, expression, closeParenToken, statement);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return forEachStatementSyntax;
			}
			return forEachStatementSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060039CD RID: 14797 RVA: 0x000FE57D File Offset: 0x000FC77D
		internal override StatementSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x060039CE RID: 14798 RVA: 0x000FE588 File Offset: 0x000FC788
		public new ForEachStatementSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.AwaitKeyword, this.ForEachKeyword, this.OpenParenToken, this.Type, this.Identifier, this.InKeyword, this.Expression, this.CloseParenToken, this.Statement);
		}

		// Token: 0x060039CF RID: 14799 RVA: 0x000FE5D2 File Offset: 0x000FC7D2
		internal override CommonForEachStatementSyntax WithAwaitKeywordCore(SyntaxToken awaitKeyword)
		{
			return this.WithAwaitKeyword(awaitKeyword);
		}

		// Token: 0x060039D0 RID: 14800 RVA: 0x000FE5DC File Offset: 0x000FC7DC
		public new ForEachStatementSyntax WithAwaitKeyword(SyntaxToken awaitKeyword)
		{
			return this.Update(this.AttributeLists, awaitKeyword, this.ForEachKeyword, this.OpenParenToken, this.Type, this.Identifier, this.InKeyword, this.Expression, this.CloseParenToken, this.Statement);
		}

		// Token: 0x060039D1 RID: 14801 RVA: 0x000FE626 File Offset: 0x000FC826
		internal override CommonForEachStatementSyntax WithForEachKeywordCore(SyntaxToken forEachKeyword)
		{
			return this.WithForEachKeyword(forEachKeyword);
		}

		// Token: 0x060039D2 RID: 14802 RVA: 0x000FE630 File Offset: 0x000FC830
		public new ForEachStatementSyntax WithForEachKeyword(SyntaxToken forEachKeyword)
		{
			return this.Update(this.AttributeLists, this.AwaitKeyword, forEachKeyword, this.OpenParenToken, this.Type, this.Identifier, this.InKeyword, this.Expression, this.CloseParenToken, this.Statement);
		}

		// Token: 0x060039D3 RID: 14803 RVA: 0x000FE67A File Offset: 0x000FC87A
		internal override CommonForEachStatementSyntax WithOpenParenTokenCore(SyntaxToken openParenToken)
		{
			return this.WithOpenParenToken(openParenToken);
		}

		// Token: 0x060039D4 RID: 14804 RVA: 0x000FE684 File Offset: 0x000FC884
		public new ForEachStatementSyntax WithOpenParenToken(SyntaxToken openParenToken)
		{
			return this.Update(this.AttributeLists, this.AwaitKeyword, this.ForEachKeyword, openParenToken, this.Type, this.Identifier, this.InKeyword, this.Expression, this.CloseParenToken, this.Statement);
		}

		// Token: 0x060039D5 RID: 14805 RVA: 0x000FE6D0 File Offset: 0x000FC8D0
		public ForEachStatementSyntax WithType(TypeSyntax type)
		{
			return this.Update(this.AttributeLists, this.AwaitKeyword, this.ForEachKeyword, this.OpenParenToken, type, this.Identifier, this.InKeyword, this.Expression, this.CloseParenToken, this.Statement);
		}

		// Token: 0x060039D6 RID: 14806 RVA: 0x000FE71C File Offset: 0x000FC91C
		public ForEachStatementSyntax WithIdentifier(SyntaxToken identifier)
		{
			return this.Update(this.AttributeLists, this.AwaitKeyword, this.ForEachKeyword, this.OpenParenToken, this.Type, identifier, this.InKeyword, this.Expression, this.CloseParenToken, this.Statement);
		}

		// Token: 0x060039D7 RID: 14807 RVA: 0x000FE766 File Offset: 0x000FC966
		internal override CommonForEachStatementSyntax WithInKeywordCore(SyntaxToken inKeyword)
		{
			return this.WithInKeyword(inKeyword);
		}

		// Token: 0x060039D8 RID: 14808 RVA: 0x000FE770 File Offset: 0x000FC970
		public new ForEachStatementSyntax WithInKeyword(SyntaxToken inKeyword)
		{
			return this.Update(this.AttributeLists, this.AwaitKeyword, this.ForEachKeyword, this.OpenParenToken, this.Type, this.Identifier, inKeyword, this.Expression, this.CloseParenToken, this.Statement);
		}

		// Token: 0x060039D9 RID: 14809 RVA: 0x000FE7BA File Offset: 0x000FC9BA
		internal override CommonForEachStatementSyntax WithExpressionCore(ExpressionSyntax expression)
		{
			return this.WithExpression(expression);
		}

		// Token: 0x060039DA RID: 14810 RVA: 0x000FE7C4 File Offset: 0x000FC9C4
		public new ForEachStatementSyntax WithExpression(ExpressionSyntax expression)
		{
			return this.Update(this.AttributeLists, this.AwaitKeyword, this.ForEachKeyword, this.OpenParenToken, this.Type, this.Identifier, this.InKeyword, expression, this.CloseParenToken, this.Statement);
		}

		// Token: 0x060039DB RID: 14811 RVA: 0x000FE80E File Offset: 0x000FCA0E
		internal override CommonForEachStatementSyntax WithCloseParenTokenCore(SyntaxToken closeParenToken)
		{
			return this.WithCloseParenToken(closeParenToken);
		}

		// Token: 0x060039DC RID: 14812 RVA: 0x000FE818 File Offset: 0x000FCA18
		public new ForEachStatementSyntax WithCloseParenToken(SyntaxToken closeParenToken)
		{
			return this.Update(this.AttributeLists, this.AwaitKeyword, this.ForEachKeyword, this.OpenParenToken, this.Type, this.Identifier, this.InKeyword, this.Expression, closeParenToken, this.Statement);
		}

		// Token: 0x060039DD RID: 14813 RVA: 0x000FE862 File Offset: 0x000FCA62
		internal override CommonForEachStatementSyntax WithStatementCore(StatementSyntax statement)
		{
			return this.WithStatement(statement);
		}

		// Token: 0x060039DE RID: 14814 RVA: 0x000FE86C File Offset: 0x000FCA6C
		public new ForEachStatementSyntax WithStatement(StatementSyntax statement)
		{
			return this.Update(this.AttributeLists, this.AwaitKeyword, this.ForEachKeyword, this.OpenParenToken, this.Type, this.Identifier, this.InKeyword, this.Expression, this.CloseParenToken, statement);
		}

		// Token: 0x060039DF RID: 14815 RVA: 0x000FE8B6 File Offset: 0x000FCAB6
		internal override StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x060039E0 RID: 14816 RVA: 0x000FE8C0 File Offset: 0x000FCAC0
		public new ForEachStatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x040013C8 RID: 5064
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x040013C9 RID: 5065
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax type;

		// Token: 0x040013CA RID: 5066
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expression;

		// Token: 0x040013CB RID: 5067
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private StatementSyntax statement;
	}
}
