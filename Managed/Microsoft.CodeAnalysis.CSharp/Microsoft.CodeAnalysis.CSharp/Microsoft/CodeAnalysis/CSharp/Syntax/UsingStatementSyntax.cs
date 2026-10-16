using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200028B RID: 651
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class UsingStatementSyntax : StatementSyntax
	{
		// Token: 0x06003D84 RID: 15748 RVA: 0x001089AC File Offset: 0x00106BAC
		public UsingStatementSyntax Update(SyntaxToken usingKeyword, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] VariableDeclarationSyntax declaration, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			return this.Update(this.AwaitKeyword, usingKeyword, openParenToken, declaration, expression, closeParenToken, statement);
		}

		// Token: 0x06003D85 RID: 15749 RVA: 0x001089C4 File Offset: 0x00106BC4
		public UsingStatementSyntax Update(SyntaxToken awaitKeyword, SyntaxToken usingKeyword, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] VariableDeclarationSyntax declaration, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			return this.Update(this.AttributeLists, awaitKeyword, usingKeyword, openParenToken, declaration, expression, closeParenToken, statement);
		}

		// Token: 0x06003D86 RID: 15750 RVA: 0x001089E8 File Offset: 0x00106BE8
		internal UsingStatementSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001008 RID: 4104
		// (get) Token: 0x06003D87 RID: 15751 RVA: 0x001089F3 File Offset: 0x00106BF3
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17001009 RID: 4105
		// (get) Token: 0x06003D88 RID: 15752 RVA: 0x00108A08 File Offset: 0x00106C08
		public SyntaxToken AwaitKeyword
		{
			get
			{
				SyntaxToken awaitKeyword = ((UsingStatementSyntax)base.Green).awaitKeyword;
				if (awaitKeyword == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, awaitKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x1700100A RID: 4106
		// (get) Token: 0x06003D89 RID: 15753 RVA: 0x00108A48 File Offset: 0x00106C48
		public SyntaxToken UsingKeyword
		{
			get
			{
				return new SyntaxToken(this, ((UsingStatementSyntax)base.Green).usingKeyword, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x1700100B RID: 4107
		// (get) Token: 0x06003D8A RID: 15754 RVA: 0x00108A6E File Offset: 0x00106C6E
		public SyntaxToken OpenParenToken
		{
			get
			{
				return new SyntaxToken(this, ((UsingStatementSyntax)base.Green).openParenToken, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x1700100C RID: 4108
		// (get) Token: 0x06003D8B RID: 15755 RVA: 0x00108A94 File Offset: 0x00106C94
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public VariableDeclarationSyntax Declaration
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<VariableDeclarationSyntax>(ref this.declaration, 4);
			}
		}

		// Token: 0x1700100D RID: 4109
		// (get) Token: 0x06003D8C RID: 15756 RVA: 0x00108AA3 File Offset: 0x00106CA3
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public ExpressionSyntax Expression
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.expression, 5);
			}
		}

		// Token: 0x1700100E RID: 4110
		// (get) Token: 0x06003D8D RID: 15757 RVA: 0x00108AB2 File Offset: 0x00106CB2
		public SyntaxToken CloseParenToken
		{
			get
			{
				return new SyntaxToken(this, ((UsingStatementSyntax)base.Green).closeParenToken, this.GetChildPosition(6), base.GetChildIndex(6));
			}
		}

		// Token: 0x1700100F RID: 4111
		// (get) Token: 0x06003D8E RID: 15758 RVA: 0x00108AD8 File Offset: 0x00106CD8
		public StatementSyntax Statement
		{
			get
			{
				return base.GetRed<StatementSyntax>(ref this.statement, 7);
			}
		}

		// Token: 0x06003D8F RID: 15759 RVA: 0x00108AE8 File Offset: 0x00106CE8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				switch (index)
				{
				case 4:
					return base.GetRed<VariableDeclarationSyntax>(ref this.declaration, 4);
				case 5:
					return base.GetRed<ExpressionSyntax>(ref this.expression, 5);
				case 7:
					return base.GetRed<StatementSyntax>(ref this.statement, 7);
				}
				syntaxNode = null;
			}
			else
			{
				syntaxNode = base.GetRedAtZero(ref this.attributeLists);
			}
			return syntaxNode;
		}

		// Token: 0x06003D90 RID: 15760 RVA: 0x00108B54 File Offset: 0x00106D54
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				switch (index)
				{
				case 4:
					return this.declaration;
				case 5:
					return this.expression;
				case 7:
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

		// Token: 0x06003D91 RID: 15761 RVA: 0x00108BA5 File Offset: 0x00106DA5
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitUsingStatement(this);
		}

		// Token: 0x06003D92 RID: 15762 RVA: 0x00108BAE File Offset: 0x00106DAE
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitUsingStatement(this);
		}

		// Token: 0x06003D93 RID: 15763 RVA: 0x00108BB8 File Offset: 0x00106DB8
		public UsingStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken awaitKeyword, SyntaxToken usingKeyword, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] VariableDeclarationSyntax declaration, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			if (!(attributeLists != this.AttributeLists) && !(awaitKeyword != this.AwaitKeyword) && !(usingKeyword != this.UsingKeyword) && !(openParenToken != this.OpenParenToken) && declaration == this.Declaration && expression == this.Expression && !(closeParenToken != this.CloseParenToken) && statement == this.Statement)
			{
				return this;
			}
			UsingStatementSyntax usingStatementSyntax = SyntaxFactory.UsingStatement(attributeLists, awaitKeyword, usingKeyword, openParenToken, declaration, expression, closeParenToken, statement);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return usingStatementSyntax;
			}
			return usingStatementSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003D94 RID: 15764 RVA: 0x00108C57 File Offset: 0x00106E57
		internal override StatementSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x06003D95 RID: 15765 RVA: 0x00108C60 File Offset: 0x00106E60
		public new UsingStatementSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.AwaitKeyword, this.UsingKeyword, this.OpenParenToken, this.Declaration, this.Expression, this.CloseParenToken, this.Statement);
		}

		// Token: 0x06003D96 RID: 15766 RVA: 0x00108CA0 File Offset: 0x00106EA0
		public UsingStatementSyntax WithAwaitKeyword(SyntaxToken awaitKeyword)
		{
			return this.Update(this.AttributeLists, awaitKeyword, this.UsingKeyword, this.OpenParenToken, this.Declaration, this.Expression, this.CloseParenToken, this.Statement);
		}

		// Token: 0x06003D97 RID: 15767 RVA: 0x00108CE0 File Offset: 0x00106EE0
		public UsingStatementSyntax WithUsingKeyword(SyntaxToken usingKeyword)
		{
			return this.Update(this.AttributeLists, this.AwaitKeyword, usingKeyword, this.OpenParenToken, this.Declaration, this.Expression, this.CloseParenToken, this.Statement);
		}

		// Token: 0x06003D98 RID: 15768 RVA: 0x00108D20 File Offset: 0x00106F20
		public UsingStatementSyntax WithOpenParenToken(SyntaxToken openParenToken)
		{
			return this.Update(this.AttributeLists, this.AwaitKeyword, this.UsingKeyword, openParenToken, this.Declaration, this.Expression, this.CloseParenToken, this.Statement);
		}

		// Token: 0x06003D99 RID: 15769 RVA: 0x00108D60 File Offset: 0x00106F60
		public UsingStatementSyntax WithDeclaration([global::System.Runtime.CompilerServices.Nullable(2)] VariableDeclarationSyntax declaration)
		{
			return this.Update(this.AttributeLists, this.AwaitKeyword, this.UsingKeyword, this.OpenParenToken, declaration, this.Expression, this.CloseParenToken, this.Statement);
		}

		// Token: 0x06003D9A RID: 15770 RVA: 0x00108DA0 File Offset: 0x00106FA0
		public UsingStatementSyntax WithExpression([global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expression)
		{
			return this.Update(this.AttributeLists, this.AwaitKeyword, this.UsingKeyword, this.OpenParenToken, this.Declaration, expression, this.CloseParenToken, this.Statement);
		}

		// Token: 0x06003D9B RID: 15771 RVA: 0x00108DE0 File Offset: 0x00106FE0
		public UsingStatementSyntax WithCloseParenToken(SyntaxToken closeParenToken)
		{
			return this.Update(this.AttributeLists, this.AwaitKeyword, this.UsingKeyword, this.OpenParenToken, this.Declaration, this.Expression, closeParenToken, this.Statement);
		}

		// Token: 0x06003D9C RID: 15772 RVA: 0x00108E20 File Offset: 0x00107020
		public UsingStatementSyntax WithStatement(StatementSyntax statement)
		{
			return this.Update(this.AttributeLists, this.AwaitKeyword, this.UsingKeyword, this.OpenParenToken, this.Declaration, this.Expression, this.CloseParenToken, statement);
		}

		// Token: 0x06003D9D RID: 15773 RVA: 0x00108E5E File Offset: 0x0010705E
		internal override StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x06003D9E RID: 15774 RVA: 0x00108E68 File Offset: 0x00107068
		public new UsingStatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x0400143E RID: 5182
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x0400143F RID: 5183
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private VariableDeclarationSyntax declaration;

		// Token: 0x04001440 RID: 5184
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expression;

		// Token: 0x04001441 RID: 5185
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private StatementSyntax statement;
	}
}
