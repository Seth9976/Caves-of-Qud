using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000261 RID: 609
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ForStatementSyntax : StatementSyntax
	{
		// Token: 0x06003A05 RID: 14853 RVA: 0x000FEED4 File Offset: 0x000FD0D4
		public ForStatementSyntax Update(SyntaxToken forKeyword, SyntaxToken openParenToken, VariableDeclarationSyntax declaration, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ExpressionSyntax> initializers, SyntaxToken firstSemicolonToken, ExpressionSyntax condition, SyntaxToken secondSemicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ExpressionSyntax> incrementors, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			return this.Update(this.AttributeLists, forKeyword, openParenToken, declaration, initializers, firstSemicolonToken, condition, secondSemicolonToken, incrementors, closeParenToken, statement);
		}

		// Token: 0x06003A06 RID: 14854 RVA: 0x000FEEFE File Offset: 0x000FD0FE
		internal ForStatementSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x06003A07 RID: 14855 RVA: 0x000FEF09 File Offset: 0x000FD109
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x06003A08 RID: 14856 RVA: 0x000FEF1D File Offset: 0x000FD11D
		public SyntaxToken ForKeyword
		{
			get
			{
				return new SyntaxToken(this, ((ForStatementSyntax)base.Green).forKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x06003A09 RID: 14857 RVA: 0x000FEF43 File Offset: 0x000FD143
		public SyntaxToken OpenParenToken
		{
			get
			{
				return new SyntaxToken(this, ((ForStatementSyntax)base.Green).openParenToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x06003A0A RID: 14858 RVA: 0x000FEF69 File Offset: 0x000FD169
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public VariableDeclarationSyntax Declaration
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<VariableDeclarationSyntax>(ref this.declaration, 3);
			}
		}

		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x06003A0B RID: 14859 RVA: 0x000FEF78 File Offset: 0x000FD178
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<ExpressionSyntax> Initializers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				SyntaxNode red = base.GetRed(ref this.initializers, 4);
				if (red == null)
				{
					return default(SeparatedSyntaxList<ExpressionSyntax>);
				}
				return new SeparatedSyntaxList<ExpressionSyntax>(red, base.GetChildIndex(4));
			}
		}

		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x06003A0C RID: 14860 RVA: 0x000FEFAD File Offset: 0x000FD1AD
		public SyntaxToken FirstSemicolonToken
		{
			get
			{
				return new SyntaxToken(this, ((ForStatementSyntax)base.Green).firstSemicolonToken, this.GetChildPosition(5), base.GetChildIndex(5));
			}
		}

		// Token: 0x17000F4B RID: 3915
		// (get) Token: 0x06003A0D RID: 14861 RVA: 0x000FEFD3 File Offset: 0x000FD1D3
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public ExpressionSyntax Condition
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.condition, 6);
			}
		}

		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x06003A0E RID: 14862 RVA: 0x000FEFE2 File Offset: 0x000FD1E2
		public SyntaxToken SecondSemicolonToken
		{
			get
			{
				return new SyntaxToken(this, ((ForStatementSyntax)base.Green).secondSemicolonToken, this.GetChildPosition(7), base.GetChildIndex(7));
			}
		}

		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x06003A0F RID: 14863 RVA: 0x000FF008 File Offset: 0x000FD208
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<ExpressionSyntax> Incrementors
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				SyntaxNode red = base.GetRed(ref this.incrementors, 8);
				if (red == null)
				{
					return default(SeparatedSyntaxList<ExpressionSyntax>);
				}
				return new SeparatedSyntaxList<ExpressionSyntax>(red, base.GetChildIndex(8));
			}
		}

		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x06003A10 RID: 14864 RVA: 0x000FF03D File Offset: 0x000FD23D
		public SyntaxToken CloseParenToken
		{
			get
			{
				return new SyntaxToken(this, ((ForStatementSyntax)base.Green).closeParenToken, this.GetChildPosition(9), base.GetChildIndex(9));
			}
		}

		// Token: 0x17000F4F RID: 3919
		// (get) Token: 0x06003A11 RID: 14865 RVA: 0x000FF065 File Offset: 0x000FD265
		public StatementSyntax Statement
		{
			get
			{
				return base.GetRed<StatementSyntax>(ref this.statement, 10);
			}
		}

		// Token: 0x06003A12 RID: 14866 RVA: 0x000FF078 File Offset: 0x000FD278
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			switch (index)
			{
			case 0:
				return base.GetRedAtZero(ref this.attributeLists);
			case 3:
				return base.GetRed<VariableDeclarationSyntax>(ref this.declaration, 3);
			case 4:
				return base.GetRed(ref this.initializers, 4);
			case 6:
				return base.GetRed<ExpressionSyntax>(ref this.condition, 6);
			case 8:
				return base.GetRed(ref this.incrementors, 8);
			case 10:
				return base.GetRed<StatementSyntax>(ref this.statement, 10);
			}
			return null;
		}

		// Token: 0x06003A13 RID: 14867 RVA: 0x000FF11C File Offset: 0x000FD31C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			switch (index)
			{
			case 0:
				return this.attributeLists;
			case 3:
				return this.declaration;
			case 4:
				return this.initializers;
			case 6:
				return this.condition;
			case 8:
				return this.incrementors;
			case 10:
				return this.statement;
			}
			return null;
		}

		// Token: 0x06003A14 RID: 14868 RVA: 0x000FF196 File Offset: 0x000FD396
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitForStatement(this);
		}

		// Token: 0x06003A15 RID: 14869 RVA: 0x000FF19F File Offset: 0x000FD39F
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitForStatement(this);
		}

		// Token: 0x06003A16 RID: 14870 RVA: 0x000FF1A8 File Offset: 0x000FD3A8
		public ForStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken forKeyword, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] VariableDeclarationSyntax declaration, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ExpressionSyntax> initializers, SyntaxToken firstSemicolonToken, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax condition, SyntaxToken secondSemicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ExpressionSyntax> incrementors, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			if (!(attributeLists != this.AttributeLists) && !(forKeyword != this.ForKeyword) && !(openParenToken != this.OpenParenToken) && declaration == this.Declaration && !(initializers != this.Initializers) && !(firstSemicolonToken != this.FirstSemicolonToken) && condition == this.Condition && !(secondSemicolonToken != this.SecondSemicolonToken) && !(incrementors != this.Incrementors) && !(closeParenToken != this.CloseParenToken) && statement == this.Statement)
			{
				return this;
			}
			ForStatementSyntax forStatementSyntax = SyntaxFactory.ForStatement(attributeLists, forKeyword, openParenToken, declaration, initializers, firstSemicolonToken, condition, secondSemicolonToken, incrementors, closeParenToken, statement);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return forStatementSyntax;
			}
			return forStatementSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003A17 RID: 14871 RVA: 0x000FF27D File Offset: 0x000FD47D
		internal override StatementSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x06003A18 RID: 14872 RVA: 0x000FF288 File Offset: 0x000FD488
		public new ForStatementSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.ForKeyword, this.OpenParenToken, this.Declaration, this.Initializers, this.FirstSemicolonToken, this.Condition, this.SecondSemicolonToken, this.Incrementors, this.CloseParenToken, this.Statement);
		}

		// Token: 0x06003A19 RID: 14873 RVA: 0x000FF2D8 File Offset: 0x000FD4D8
		public ForStatementSyntax WithForKeyword(SyntaxToken forKeyword)
		{
			return this.Update(this.AttributeLists, forKeyword, this.OpenParenToken, this.Declaration, this.Initializers, this.FirstSemicolonToken, this.Condition, this.SecondSemicolonToken, this.Incrementors, this.CloseParenToken, this.Statement);
		}

		// Token: 0x06003A1A RID: 14874 RVA: 0x000FF328 File Offset: 0x000FD528
		public ForStatementSyntax WithOpenParenToken(SyntaxToken openParenToken)
		{
			return this.Update(this.AttributeLists, this.ForKeyword, openParenToken, this.Declaration, this.Initializers, this.FirstSemicolonToken, this.Condition, this.SecondSemicolonToken, this.Incrementors, this.CloseParenToken, this.Statement);
		}

		// Token: 0x06003A1B RID: 14875 RVA: 0x000FF378 File Offset: 0x000FD578
		public ForStatementSyntax WithDeclaration([global::System.Runtime.CompilerServices.Nullable(2)] VariableDeclarationSyntax declaration)
		{
			return this.Update(this.AttributeLists, this.ForKeyword, this.OpenParenToken, declaration, this.Initializers, this.FirstSemicolonToken, this.Condition, this.SecondSemicolonToken, this.Incrementors, this.CloseParenToken, this.Statement);
		}

		// Token: 0x06003A1C RID: 14876 RVA: 0x000FF3C8 File Offset: 0x000FD5C8
		public ForStatementSyntax WithInitializers([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ExpressionSyntax> initializers)
		{
			return this.Update(this.AttributeLists, this.ForKeyword, this.OpenParenToken, this.Declaration, initializers, this.FirstSemicolonToken, this.Condition, this.SecondSemicolonToken, this.Incrementors, this.CloseParenToken, this.Statement);
		}

		// Token: 0x06003A1D RID: 14877 RVA: 0x000FF418 File Offset: 0x000FD618
		public ForStatementSyntax WithFirstSemicolonToken(SyntaxToken firstSemicolonToken)
		{
			return this.Update(this.AttributeLists, this.ForKeyword, this.OpenParenToken, this.Declaration, this.Initializers, firstSemicolonToken, this.Condition, this.SecondSemicolonToken, this.Incrementors, this.CloseParenToken, this.Statement);
		}

		// Token: 0x06003A1E RID: 14878 RVA: 0x000FF468 File Offset: 0x000FD668
		public ForStatementSyntax WithCondition([global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax condition)
		{
			return this.Update(this.AttributeLists, this.ForKeyword, this.OpenParenToken, this.Declaration, this.Initializers, this.FirstSemicolonToken, condition, this.SecondSemicolonToken, this.Incrementors, this.CloseParenToken, this.Statement);
		}

		// Token: 0x06003A1F RID: 14879 RVA: 0x000FF4B8 File Offset: 0x000FD6B8
		public ForStatementSyntax WithSecondSemicolonToken(SyntaxToken secondSemicolonToken)
		{
			return this.Update(this.AttributeLists, this.ForKeyword, this.OpenParenToken, this.Declaration, this.Initializers, this.FirstSemicolonToken, this.Condition, secondSemicolonToken, this.Incrementors, this.CloseParenToken, this.Statement);
		}

		// Token: 0x06003A20 RID: 14880 RVA: 0x000FF508 File Offset: 0x000FD708
		public ForStatementSyntax WithIncrementors([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ExpressionSyntax> incrementors)
		{
			return this.Update(this.AttributeLists, this.ForKeyword, this.OpenParenToken, this.Declaration, this.Initializers, this.FirstSemicolonToken, this.Condition, this.SecondSemicolonToken, incrementors, this.CloseParenToken, this.Statement);
		}

		// Token: 0x06003A21 RID: 14881 RVA: 0x000FF558 File Offset: 0x000FD758
		public ForStatementSyntax WithCloseParenToken(SyntaxToken closeParenToken)
		{
			return this.Update(this.AttributeLists, this.ForKeyword, this.OpenParenToken, this.Declaration, this.Initializers, this.FirstSemicolonToken, this.Condition, this.SecondSemicolonToken, this.Incrementors, closeParenToken, this.Statement);
		}

		// Token: 0x06003A22 RID: 14882 RVA: 0x000FF5A8 File Offset: 0x000FD7A8
		public ForStatementSyntax WithStatement(StatementSyntax statement)
		{
			return this.Update(this.AttributeLists, this.ForKeyword, this.OpenParenToken, this.Declaration, this.Initializers, this.FirstSemicolonToken, this.Condition, this.SecondSemicolonToken, this.Incrementors, this.CloseParenToken, statement);
		}

		// Token: 0x06003A23 RID: 14883 RVA: 0x000FF5F8 File Offset: 0x000FD7F8
		internal override StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x06003A24 RID: 14884 RVA: 0x000FF604 File Offset: 0x000FD804
		public new ForStatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x06003A25 RID: 14885 RVA: 0x000FF628 File Offset: 0x000FD828
		public ForStatementSyntax AddInitializers(params ExpressionSyntax[] items)
		{
			return this.WithInitializers(this.Initializers.AddRange(items));
		}

		// Token: 0x06003A26 RID: 14886 RVA: 0x000FF64C File Offset: 0x000FD84C
		public ForStatementSyntax AddIncrementors(params ExpressionSyntax[] items)
		{
			return this.WithIncrementors(this.Incrementors.AddRange(items));
		}

		// Token: 0x040013D0 RID: 5072
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x040013D1 RID: 5073
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private VariableDeclarationSyntax declaration;

		// Token: 0x040013D2 RID: 5074
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode initializers;

		// Token: 0x040013D3 RID: 5075
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax condition;

		// Token: 0x040013D4 RID: 5076
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode incrementors;

		// Token: 0x040013D5 RID: 5077
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private StatementSyntax statement;
	}
}
