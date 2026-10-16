using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200025E RID: 606
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class FixedStatementSyntax : StatementSyntax
	{
		// Token: 0x060039A4 RID: 14756 RVA: 0x000FDF22 File Offset: 0x000FC122
		public FixedStatementSyntax Update(SyntaxToken fixedKeyword, SyntaxToken openParenToken, VariableDeclarationSyntax declaration, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			return this.Update(this.AttributeLists, fixedKeyword, openParenToken, declaration, closeParenToken, statement);
		}

		// Token: 0x060039A5 RID: 14757 RVA: 0x000FDF37 File Offset: 0x000FC137
		internal FixedStatementSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x060039A6 RID: 14758 RVA: 0x000FDF42 File Offset: 0x000FC142
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x060039A7 RID: 14759 RVA: 0x000FDF56 File Offset: 0x000FC156
		public SyntaxToken FixedKeyword
		{
			get
			{
				return new SyntaxToken(this, ((FixedStatementSyntax)base.Green).fixedKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x060039A8 RID: 14760 RVA: 0x000FDF7C File Offset: 0x000FC17C
		public SyntaxToken OpenParenToken
		{
			get
			{
				return new SyntaxToken(this, ((FixedStatementSyntax)base.Green).openParenToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x060039A9 RID: 14761 RVA: 0x000FDFA2 File Offset: 0x000FC1A2
		public VariableDeclarationSyntax Declaration
		{
			get
			{
				return base.GetRed<VariableDeclarationSyntax>(ref this.declaration, 3);
			}
		}

		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x060039AA RID: 14762 RVA: 0x000FDFB1 File Offset: 0x000FC1B1
		public SyntaxToken CloseParenToken
		{
			get
			{
				return new SyntaxToken(this, ((FixedStatementSyntax)base.Green).closeParenToken, this.GetChildPosition(4), base.GetChildIndex(4));
			}
		}

		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x060039AB RID: 14763 RVA: 0x000FDFD7 File Offset: 0x000FC1D7
		public StatementSyntax Statement
		{
			get
			{
				return base.GetRed<StatementSyntax>(ref this.statement, 5);
			}
		}

		// Token: 0x060039AC RID: 14764 RVA: 0x000FDFE8 File Offset: 0x000FC1E8
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
					syntaxNode = base.GetRed<VariableDeclarationSyntax>(ref this.declaration, 3);
				}
			}
			else
			{
				syntaxNode = base.GetRedAtZero(ref this.attributeLists);
			}
			return syntaxNode;
		}

		// Token: 0x060039AD RID: 14765 RVA: 0x000FE034 File Offset: 0x000FC234
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
					syntaxNode = this.declaration;
				}
			}
			else
			{
				syntaxNode = this.attributeLists;
			}
			return syntaxNode;
		}

		// Token: 0x060039AE RID: 14766 RVA: 0x000FE06C File Offset: 0x000FC26C
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitFixedStatement(this);
		}

		// Token: 0x060039AF RID: 14767 RVA: 0x000FE075 File Offset: 0x000FC275
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitFixedStatement(this);
		}

		// Token: 0x060039B0 RID: 14768 RVA: 0x000FE080 File Offset: 0x000FC280
		public FixedStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken fixedKeyword, SyntaxToken openParenToken, VariableDeclarationSyntax declaration, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			if (!(attributeLists != this.AttributeLists) && !(fixedKeyword != this.FixedKeyword) && !(openParenToken != this.OpenParenToken) && declaration == this.Declaration && !(closeParenToken != this.CloseParenToken) && statement == this.Statement)
			{
				return this;
			}
			FixedStatementSyntax fixedStatementSyntax = SyntaxFactory.FixedStatement(attributeLists, fixedKeyword, openParenToken, declaration, closeParenToken, statement);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return fixedStatementSyntax;
			}
			return fixedStatementSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060039B1 RID: 14769 RVA: 0x000FE102 File Offset: 0x000FC302
		internal override StatementSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x060039B2 RID: 14770 RVA: 0x000FE10B File Offset: 0x000FC30B
		public new FixedStatementSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.FixedKeyword, this.OpenParenToken, this.Declaration, this.CloseParenToken, this.Statement);
		}

		// Token: 0x060039B3 RID: 14771 RVA: 0x000FE132 File Offset: 0x000FC332
		public FixedStatementSyntax WithFixedKeyword(SyntaxToken fixedKeyword)
		{
			return this.Update(this.AttributeLists, fixedKeyword, this.OpenParenToken, this.Declaration, this.CloseParenToken, this.Statement);
		}

		// Token: 0x060039B4 RID: 14772 RVA: 0x000FE159 File Offset: 0x000FC359
		public FixedStatementSyntax WithOpenParenToken(SyntaxToken openParenToken)
		{
			return this.Update(this.AttributeLists, this.FixedKeyword, openParenToken, this.Declaration, this.CloseParenToken, this.Statement);
		}

		// Token: 0x060039B5 RID: 14773 RVA: 0x000FE180 File Offset: 0x000FC380
		public FixedStatementSyntax WithDeclaration(VariableDeclarationSyntax declaration)
		{
			return this.Update(this.AttributeLists, this.FixedKeyword, this.OpenParenToken, declaration, this.CloseParenToken, this.Statement);
		}

		// Token: 0x060039B6 RID: 14774 RVA: 0x000FE1A7 File Offset: 0x000FC3A7
		public FixedStatementSyntax WithCloseParenToken(SyntaxToken closeParenToken)
		{
			return this.Update(this.AttributeLists, this.FixedKeyword, this.OpenParenToken, this.Declaration, closeParenToken, this.Statement);
		}

		// Token: 0x060039B7 RID: 14775 RVA: 0x000FE1CE File Offset: 0x000FC3CE
		public FixedStatementSyntax WithStatement(StatementSyntax statement)
		{
			return this.Update(this.AttributeLists, this.FixedKeyword, this.OpenParenToken, this.Declaration, this.CloseParenToken, statement);
		}

		// Token: 0x060039B8 RID: 14776 RVA: 0x000FE1F5 File Offset: 0x000FC3F5
		internal override StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x060039B9 RID: 14777 RVA: 0x000FE200 File Offset: 0x000FC400
		public new FixedStatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x060039BA RID: 14778 RVA: 0x000FE224 File Offset: 0x000FC424
		public FixedStatementSyntax AddDeclarationVariables(params VariableDeclaratorSyntax[] items)
		{
			return this.WithDeclaration(this.Declaration.WithVariables(this.Declaration.Variables.AddRange(items)));
		}

		// Token: 0x040013C5 RID: 5061
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x040013C6 RID: 5062
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private VariableDeclarationSyntax declaration;

		// Token: 0x040013C7 RID: 5063
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private StatementSyntax statement;
	}
}
