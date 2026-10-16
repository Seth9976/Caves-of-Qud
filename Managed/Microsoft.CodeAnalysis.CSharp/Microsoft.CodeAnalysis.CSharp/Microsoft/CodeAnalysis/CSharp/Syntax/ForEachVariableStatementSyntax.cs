using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000260 RID: 608
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ForEachVariableStatementSyntax : CommonForEachStatementSyntax
	{
		// Token: 0x060039E1 RID: 14817 RVA: 0x000FE8E4 File Offset: 0x000FCAE4
		public ForEachVariableStatementSyntax Update(SyntaxToken forEachKeyword, SyntaxToken openParenToken, ExpressionSyntax variable, SyntaxToken inKeyword, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			return this.Update(this.AwaitKeyword, forEachKeyword, openParenToken, variable, inKeyword, expression, closeParenToken, statement);
		}

		// Token: 0x060039E2 RID: 14818 RVA: 0x000FE908 File Offset: 0x000FCB08
		public ForEachVariableStatementSyntax Update(SyntaxToken awaitKeyword, SyntaxToken forEachKeyword, SyntaxToken openParenToken, ExpressionSyntax variable, SyntaxToken inKeyword, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			return this.Update(this.AttributeLists, awaitKeyword, forEachKeyword, openParenToken, variable, inKeyword, expression, closeParenToken, statement);
		}

		// Token: 0x060039E3 RID: 14819 RVA: 0x000FE92E File Offset: 0x000FCB2E
		internal ForEachVariableStatementSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x060039E4 RID: 14820 RVA: 0x000FE939 File Offset: 0x000FCB39
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x060039E5 RID: 14821 RVA: 0x000FE950 File Offset: 0x000FCB50
		public override SyntaxToken AwaitKeyword
		{
			get
			{
				SyntaxToken awaitKeyword = ((ForEachVariableStatementSyntax)base.Green).awaitKeyword;
				if (awaitKeyword == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, awaitKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x060039E6 RID: 14822 RVA: 0x000FE990 File Offset: 0x000FCB90
		public override SyntaxToken ForEachKeyword
		{
			get
			{
				return new SyntaxToken(this, ((ForEachVariableStatementSyntax)base.Green).forEachKeyword, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x060039E7 RID: 14823 RVA: 0x000FE9B6 File Offset: 0x000FCBB6
		public override SyntaxToken OpenParenToken
		{
			get
			{
				return new SyntaxToken(this, ((ForEachVariableStatementSyntax)base.Green).openParenToken, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x060039E8 RID: 14824 RVA: 0x000FE9DC File Offset: 0x000FCBDC
		public ExpressionSyntax Variable
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.variable, 4);
			}
		}

		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x060039E9 RID: 14825 RVA: 0x000FE9EB File Offset: 0x000FCBEB
		public override SyntaxToken InKeyword
		{
			get
			{
				return new SyntaxToken(this, ((ForEachVariableStatementSyntax)base.Green).inKeyword, this.GetChildPosition(5), base.GetChildIndex(5));
			}
		}

		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x060039EA RID: 14826 RVA: 0x000FEA11 File Offset: 0x000FCC11
		public override ExpressionSyntax Expression
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.expression, 6);
			}
		}

		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x060039EB RID: 14827 RVA: 0x000FEA20 File Offset: 0x000FCC20
		public override SyntaxToken CloseParenToken
		{
			get
			{
				return new SyntaxToken(this, ((ForEachVariableStatementSyntax)base.Green).closeParenToken, this.GetChildPosition(7), base.GetChildIndex(7));
			}
		}

		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x060039EC RID: 14828 RVA: 0x000FEA46 File Offset: 0x000FCC46
		public override StatementSyntax Statement
		{
			get
			{
				return base.GetRed<StatementSyntax>(ref this.statement, 8);
			}
		}

		// Token: 0x060039ED RID: 14829 RVA: 0x000FEA58 File Offset: 0x000FCC58
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				switch (index)
				{
				case 4:
					return base.GetRed<ExpressionSyntax>(ref this.variable, 4);
				case 6:
					return base.GetRed<ExpressionSyntax>(ref this.expression, 6);
				case 8:
					return base.GetRed<StatementSyntax>(ref this.statement, 8);
				}
				syntaxNode = null;
			}
			else
			{
				syntaxNode = base.GetRedAtZero(ref this.attributeLists);
			}
			return syntaxNode;
		}

		// Token: 0x060039EE RID: 14830 RVA: 0x000FEAC8 File Offset: 0x000FCCC8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				switch (index)
				{
				case 4:
					return this.variable;
				case 6:
					return this.expression;
				case 8:
					return this.statement;
				}
				syntaxNode = null;
			}
			else
			{
				syntaxNode = this.attributeLists;
			}
			return syntaxNode;
		}

		// Token: 0x060039EF RID: 14831 RVA: 0x000FEB1D File Offset: 0x000FCD1D
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitForEachVariableStatement(this);
		}

		// Token: 0x060039F0 RID: 14832 RVA: 0x000FEB26 File Offset: 0x000FCD26
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitForEachVariableStatement(this);
		}

		// Token: 0x060039F1 RID: 14833 RVA: 0x000FEB30 File Offset: 0x000FCD30
		public ForEachVariableStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken awaitKeyword, SyntaxToken forEachKeyword, SyntaxToken openParenToken, ExpressionSyntax variable, SyntaxToken inKeyword, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			if (!(attributeLists != this.AttributeLists) && !(awaitKeyword != this.AwaitKeyword) && !(forEachKeyword != this.ForEachKeyword) && !(openParenToken != this.OpenParenToken) && variable == this.Variable && !(inKeyword != this.InKeyword) && expression == this.Expression && !(closeParenToken != this.CloseParenToken) && statement == this.Statement)
			{
				return this;
			}
			ForEachVariableStatementSyntax forEachVariableStatementSyntax = SyntaxFactory.ForEachVariableStatement(attributeLists, awaitKeyword, forEachKeyword, openParenToken, variable, inKeyword, expression, closeParenToken, statement);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return forEachVariableStatementSyntax;
			}
			return forEachVariableStatementSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060039F2 RID: 14834 RVA: 0x000FEBE0 File Offset: 0x000FCDE0
		internal override StatementSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x060039F3 RID: 14835 RVA: 0x000FEBEC File Offset: 0x000FCDEC
		public new ForEachVariableStatementSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.AwaitKeyword, this.ForEachKeyword, this.OpenParenToken, this.Variable, this.InKeyword, this.Expression, this.CloseParenToken, this.Statement);
		}

		// Token: 0x060039F4 RID: 14836 RVA: 0x000FEC30 File Offset: 0x000FCE30
		internal override CommonForEachStatementSyntax WithAwaitKeywordCore(SyntaxToken awaitKeyword)
		{
			return this.WithAwaitKeyword(awaitKeyword);
		}

		// Token: 0x060039F5 RID: 14837 RVA: 0x000FEC3C File Offset: 0x000FCE3C
		public new ForEachVariableStatementSyntax WithAwaitKeyword(SyntaxToken awaitKeyword)
		{
			return this.Update(this.AttributeLists, awaitKeyword, this.ForEachKeyword, this.OpenParenToken, this.Variable, this.InKeyword, this.Expression, this.CloseParenToken, this.Statement);
		}

		// Token: 0x060039F6 RID: 14838 RVA: 0x000FEC80 File Offset: 0x000FCE80
		internal override CommonForEachStatementSyntax WithForEachKeywordCore(SyntaxToken forEachKeyword)
		{
			return this.WithForEachKeyword(forEachKeyword);
		}

		// Token: 0x060039F7 RID: 14839 RVA: 0x000FEC8C File Offset: 0x000FCE8C
		public new ForEachVariableStatementSyntax WithForEachKeyword(SyntaxToken forEachKeyword)
		{
			return this.Update(this.AttributeLists, this.AwaitKeyword, forEachKeyword, this.OpenParenToken, this.Variable, this.InKeyword, this.Expression, this.CloseParenToken, this.Statement);
		}

		// Token: 0x060039F8 RID: 14840 RVA: 0x000FECD0 File Offset: 0x000FCED0
		internal override CommonForEachStatementSyntax WithOpenParenTokenCore(SyntaxToken openParenToken)
		{
			return this.WithOpenParenToken(openParenToken);
		}

		// Token: 0x060039F9 RID: 14841 RVA: 0x000FECDC File Offset: 0x000FCEDC
		public new ForEachVariableStatementSyntax WithOpenParenToken(SyntaxToken openParenToken)
		{
			return this.Update(this.AttributeLists, this.AwaitKeyword, this.ForEachKeyword, openParenToken, this.Variable, this.InKeyword, this.Expression, this.CloseParenToken, this.Statement);
		}

		// Token: 0x060039FA RID: 14842 RVA: 0x000FED20 File Offset: 0x000FCF20
		public ForEachVariableStatementSyntax WithVariable(ExpressionSyntax variable)
		{
			return this.Update(this.AttributeLists, this.AwaitKeyword, this.ForEachKeyword, this.OpenParenToken, variable, this.InKeyword, this.Expression, this.CloseParenToken, this.Statement);
		}

		// Token: 0x060039FB RID: 14843 RVA: 0x000FED64 File Offset: 0x000FCF64
		internal override CommonForEachStatementSyntax WithInKeywordCore(SyntaxToken inKeyword)
		{
			return this.WithInKeyword(inKeyword);
		}

		// Token: 0x060039FC RID: 14844 RVA: 0x000FED70 File Offset: 0x000FCF70
		public new ForEachVariableStatementSyntax WithInKeyword(SyntaxToken inKeyword)
		{
			return this.Update(this.AttributeLists, this.AwaitKeyword, this.ForEachKeyword, this.OpenParenToken, this.Variable, inKeyword, this.Expression, this.CloseParenToken, this.Statement);
		}

		// Token: 0x060039FD RID: 14845 RVA: 0x000FEDB4 File Offset: 0x000FCFB4
		internal override CommonForEachStatementSyntax WithExpressionCore(ExpressionSyntax expression)
		{
			return this.WithExpression(expression);
		}

		// Token: 0x060039FE RID: 14846 RVA: 0x000FEDC0 File Offset: 0x000FCFC0
		public new ForEachVariableStatementSyntax WithExpression(ExpressionSyntax expression)
		{
			return this.Update(this.AttributeLists, this.AwaitKeyword, this.ForEachKeyword, this.OpenParenToken, this.Variable, this.InKeyword, expression, this.CloseParenToken, this.Statement);
		}

		// Token: 0x060039FF RID: 14847 RVA: 0x000FEE04 File Offset: 0x000FD004
		internal override CommonForEachStatementSyntax WithCloseParenTokenCore(SyntaxToken closeParenToken)
		{
			return this.WithCloseParenToken(closeParenToken);
		}

		// Token: 0x06003A00 RID: 14848 RVA: 0x000FEE10 File Offset: 0x000FD010
		public new ForEachVariableStatementSyntax WithCloseParenToken(SyntaxToken closeParenToken)
		{
			return this.Update(this.AttributeLists, this.AwaitKeyword, this.ForEachKeyword, this.OpenParenToken, this.Variable, this.InKeyword, this.Expression, closeParenToken, this.Statement);
		}

		// Token: 0x06003A01 RID: 14849 RVA: 0x000FEE54 File Offset: 0x000FD054
		internal override CommonForEachStatementSyntax WithStatementCore(StatementSyntax statement)
		{
			return this.WithStatement(statement);
		}

		// Token: 0x06003A02 RID: 14850 RVA: 0x000FEE60 File Offset: 0x000FD060
		public new ForEachVariableStatementSyntax WithStatement(StatementSyntax statement)
		{
			return this.Update(this.AttributeLists, this.AwaitKeyword, this.ForEachKeyword, this.OpenParenToken, this.Variable, this.InKeyword, this.Expression, this.CloseParenToken, statement);
		}

		// Token: 0x06003A03 RID: 14851 RVA: 0x000FEEA4 File Offset: 0x000FD0A4
		internal override StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x06003A04 RID: 14852 RVA: 0x000FEEB0 File Offset: 0x000FD0B0
		public new ForEachVariableStatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x040013CC RID: 5068
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x040013CD RID: 5069
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax variable;

		// Token: 0x040013CE RID: 5070
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expression;

		// Token: 0x040013CF RID: 5071
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private StatementSyntax statement;
	}
}
