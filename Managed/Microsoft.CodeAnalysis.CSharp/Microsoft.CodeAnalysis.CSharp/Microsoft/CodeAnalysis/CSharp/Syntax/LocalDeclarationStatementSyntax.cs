using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000255 RID: 597
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class LocalDeclarationStatementSyntax : StatementSyntax
	{
		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x060038BE RID: 14526 RVA: 0x000FB9B7 File Offset: 0x000F9BB7
		public bool IsConst
		{
			get
			{
				return this.Modifiers.Any(SyntaxKind.ConstKeyword);
			}
		}

		// Token: 0x060038BF RID: 14527 RVA: 0x000FB9C9 File Offset: 0x000F9BC9
		public LocalDeclarationStatementSyntax Update(SyntaxTokenList modifiers, VariableDeclarationSyntax declaration, SyntaxToken semicolonToken)
		{
			return this.Update(this.AwaitKeyword, this.UsingKeyword, modifiers, declaration, semicolonToken);
		}

		// Token: 0x060038C0 RID: 14528 RVA: 0x000FB9E0 File Offset: 0x000F9BE0
		public LocalDeclarationStatementSyntax Update(SyntaxToken awaitKeyword, SyntaxToken usingKeyword, SyntaxTokenList modifiers, VariableDeclarationSyntax declaration, SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, awaitKeyword, usingKeyword, modifiers, declaration, semicolonToken);
		}

		// Token: 0x060038C1 RID: 14529 RVA: 0x000FB9F5 File Offset: 0x000F9BF5
		internal LocalDeclarationStatementSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x060038C2 RID: 14530 RVA: 0x000FBA00 File Offset: 0x000F9C00
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17000EF7 RID: 3831
		// (get) Token: 0x060038C3 RID: 14531 RVA: 0x000FBA14 File Offset: 0x000F9C14
		public SyntaxToken AwaitKeyword
		{
			get
			{
				SyntaxToken awaitKeyword = ((LocalDeclarationStatementSyntax)base.Green).awaitKeyword;
				if (awaitKeyword == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, awaitKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x060038C4 RID: 14532 RVA: 0x000FBA54 File Offset: 0x000F9C54
		public SyntaxToken UsingKeyword
		{
			get
			{
				SyntaxToken usingKeyword = ((LocalDeclarationStatementSyntax)base.Green).usingKeyword;
				if (usingKeyword == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, usingKeyword, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x060038C5 RID: 14533 RVA: 0x000FBA94 File Offset: 0x000F9C94
		public SyntaxTokenList Modifiers
		{
			get
			{
				GreenNode slot = base.Green.GetSlot(3);
				if (slot == null)
				{
					return default(SyntaxTokenList);
				}
				return new SyntaxTokenList(this, slot, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x060038C6 RID: 14534 RVA: 0x000FBAD0 File Offset: 0x000F9CD0
		public VariableDeclarationSyntax Declaration
		{
			get
			{
				return base.GetRed<VariableDeclarationSyntax>(ref this.declaration, 4);
			}
		}

		// Token: 0x17000EFB RID: 3835
		// (get) Token: 0x060038C7 RID: 14535 RVA: 0x000FBADF File Offset: 0x000F9CDF
		public SyntaxToken SemicolonToken
		{
			get
			{
				return new SyntaxToken(this, ((LocalDeclarationStatementSyntax)base.Green).semicolonToken, this.GetChildPosition(5), base.GetChildIndex(5));
			}
		}

		// Token: 0x060038C8 RID: 14536 RVA: 0x000FBB08 File Offset: 0x000F9D08
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 4)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = base.GetRed<VariableDeclarationSyntax>(ref this.declaration, 4);
				}
			}
			else
			{
				syntaxNode = base.GetRedAtZero(ref this.attributeLists);
			}
			return syntaxNode;
		}

		// Token: 0x060038C9 RID: 14537 RVA: 0x000FBB40 File Offset: 0x000F9D40
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 4)
				{
					syntaxNode = null;
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

		// Token: 0x060038CA RID: 14538 RVA: 0x000FBB6B File Offset: 0x000F9D6B
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitLocalDeclarationStatement(this);
		}

		// Token: 0x060038CB RID: 14539 RVA: 0x000FBB74 File Offset: 0x000F9D74
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitLocalDeclarationStatement(this);
		}

		// Token: 0x060038CC RID: 14540 RVA: 0x000FBB80 File Offset: 0x000F9D80
		public LocalDeclarationStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken awaitKeyword, SyntaxToken usingKeyword, SyntaxTokenList modifiers, VariableDeclarationSyntax declaration, SyntaxToken semicolonToken)
		{
			if (!(attributeLists != this.AttributeLists) && !(awaitKeyword != this.AwaitKeyword) && !(usingKeyword != this.UsingKeyword) && !(modifiers != this.Modifiers) && declaration == this.Declaration && !(semicolonToken != this.SemicolonToken))
			{
				return this;
			}
			LocalDeclarationStatementSyntax localDeclarationStatementSyntax = SyntaxFactory.LocalDeclarationStatement(attributeLists, awaitKeyword, usingKeyword, modifiers, declaration, semicolonToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return localDeclarationStatementSyntax;
			}
			return localDeclarationStatementSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060038CD RID: 14541 RVA: 0x000FBC07 File Offset: 0x000F9E07
		internal override StatementSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x060038CE RID: 14542 RVA: 0x000FBC10 File Offset: 0x000F9E10
		public new LocalDeclarationStatementSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.AwaitKeyword, this.UsingKeyword, this.Modifiers, this.Declaration, this.SemicolonToken);
		}

		// Token: 0x060038CF RID: 14543 RVA: 0x000FBC37 File Offset: 0x000F9E37
		public LocalDeclarationStatementSyntax WithAwaitKeyword(SyntaxToken awaitKeyword)
		{
			return this.Update(this.AttributeLists, awaitKeyword, this.UsingKeyword, this.Modifiers, this.Declaration, this.SemicolonToken);
		}

		// Token: 0x060038D0 RID: 14544 RVA: 0x000FBC5E File Offset: 0x000F9E5E
		public LocalDeclarationStatementSyntax WithUsingKeyword(SyntaxToken usingKeyword)
		{
			return this.Update(this.AttributeLists, this.AwaitKeyword, usingKeyword, this.Modifiers, this.Declaration, this.SemicolonToken);
		}

		// Token: 0x060038D1 RID: 14545 RVA: 0x000FBC85 File Offset: 0x000F9E85
		public LocalDeclarationStatementSyntax WithModifiers(SyntaxTokenList modifiers)
		{
			return this.Update(this.AttributeLists, this.AwaitKeyword, this.UsingKeyword, modifiers, this.Declaration, this.SemicolonToken);
		}

		// Token: 0x060038D2 RID: 14546 RVA: 0x000FBCAC File Offset: 0x000F9EAC
		public LocalDeclarationStatementSyntax WithDeclaration(VariableDeclarationSyntax declaration)
		{
			return this.Update(this.AttributeLists, this.AwaitKeyword, this.UsingKeyword, this.Modifiers, declaration, this.SemicolonToken);
		}

		// Token: 0x060038D3 RID: 14547 RVA: 0x000FBCD3 File Offset: 0x000F9ED3
		public LocalDeclarationStatementSyntax WithSemicolonToken(SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, this.AwaitKeyword, this.UsingKeyword, this.Modifiers, this.Declaration, semicolonToken);
		}

		// Token: 0x060038D4 RID: 14548 RVA: 0x000FBCFA File Offset: 0x000F9EFA
		internal override StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x060038D5 RID: 14549 RVA: 0x000FBD04 File Offset: 0x000F9F04
		public new LocalDeclarationStatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x060038D6 RID: 14550 RVA: 0x000FBD28 File Offset: 0x000F9F28
		public LocalDeclarationStatementSyntax AddModifiers(params SyntaxToken[] items)
		{
			return this.WithModifiers(this.Modifiers.AddRange(items));
		}

		// Token: 0x060038D7 RID: 14551 RVA: 0x000FBD4C File Offset: 0x000F9F4C
		public LocalDeclarationStatementSyntax AddDeclarationVariables(params VariableDeclaratorSyntax[] items)
		{
			return this.WithDeclaration(this.Declaration.WithVariables(this.Declaration.Variables.AddRange(items)));
		}

		// Token: 0x040013AD RID: 5037
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x040013AE RID: 5038
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private VariableDeclarationSyntax declaration;
	}
}
