using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200026E RID: 622
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class NamespaceDeclarationSyntax : MemberDeclarationSyntax
	{
		// Token: 0x17000F8F RID: 3983
		// (get) Token: 0x06003B4D RID: 15181 RVA: 0x00102725 File Offset: 0x00100925
		internal new NamespaceDeclarationSyntax Green
		{
			get
			{
				return (NamespaceDeclarationSyntax)base.Green;
			}
		}

		// Token: 0x06003B4E RID: 15182 RVA: 0x00102734 File Offset: 0x00100934
		public NamespaceDeclarationSyntax Update(SyntaxToken namespaceKeyword, NameSyntax name, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<ExternAliasDirectiveSyntax> externs, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<UsingDirectiveSyntax> usings, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, this.Modifiers, namespaceKeyword, name, openBraceToken, externs, usings, members, closeBraceToken, semicolonToken);
		}

		// Token: 0x06003B4F RID: 15183 RVA: 0x00102760 File Offset: 0x00100960
		internal NamespaceDeclarationSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000F90 RID: 3984
		// (get) Token: 0x06003B50 RID: 15184 RVA: 0x0010276B File Offset: 0x0010096B
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17000F91 RID: 3985
		// (get) Token: 0x06003B51 RID: 15185 RVA: 0x00102780 File Offset: 0x00100980
		public override SyntaxTokenList Modifiers
		{
			get
			{
				GreenNode slot = this.Green.GetSlot(1);
				if (slot == null)
				{
					return default(SyntaxTokenList);
				}
				return new SyntaxTokenList(this, slot, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x06003B52 RID: 15186 RVA: 0x001027BC File Offset: 0x001009BC
		public SyntaxToken NamespaceKeyword
		{
			get
			{
				return new SyntaxToken(this, this.Green.namespaceKeyword, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17000F93 RID: 3987
		// (get) Token: 0x06003B53 RID: 15187 RVA: 0x001027DD File Offset: 0x001009DD
		public NameSyntax Name
		{
			get
			{
				return base.GetRed<NameSyntax>(ref this.name, 3);
			}
		}

		// Token: 0x17000F94 RID: 3988
		// (get) Token: 0x06003B54 RID: 15188 RVA: 0x001027EC File Offset: 0x001009EC
		public SyntaxToken OpenBraceToken
		{
			get
			{
				return new SyntaxToken(this, this.Green.openBraceToken, this.GetChildPosition(4), base.GetChildIndex(4));
			}
		}

		// Token: 0x17000F95 RID: 3989
		// (get) Token: 0x06003B55 RID: 15189 RVA: 0x0010280D File Offset: 0x00100A0D
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<ExternAliasDirectiveSyntax> Externs
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<ExternAliasDirectiveSyntax>(base.GetRed(ref this.externs, 5));
			}
		}

		// Token: 0x17000F96 RID: 3990
		// (get) Token: 0x06003B56 RID: 15190 RVA: 0x00102821 File Offset: 0x00100A21
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<UsingDirectiveSyntax> Usings
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<UsingDirectiveSyntax>(base.GetRed(ref this.usings, 6));
			}
		}

		// Token: 0x17000F97 RID: 3991
		// (get) Token: 0x06003B57 RID: 15191 RVA: 0x00102835 File Offset: 0x00100A35
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<MemberDeclarationSyntax> Members
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<MemberDeclarationSyntax>(base.GetRed(ref this.members, 7));
			}
		}

		// Token: 0x17000F98 RID: 3992
		// (get) Token: 0x06003B58 RID: 15192 RVA: 0x00102849 File Offset: 0x00100A49
		public SyntaxToken CloseBraceToken
		{
			get
			{
				return new SyntaxToken(this, this.Green.closeBraceToken, this.GetChildPosition(8), base.GetChildIndex(8));
			}
		}

		// Token: 0x17000F99 RID: 3993
		// (get) Token: 0x06003B59 RID: 15193 RVA: 0x0010286C File Offset: 0x00100A6C
		public SyntaxToken SemicolonToken
		{
			get
			{
				SyntaxToken semicolonToken = this.Green.semicolonToken;
				if (semicolonToken == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, semicolonToken, this.GetChildPosition(9), base.GetChildIndex(9));
			}
		}

		// Token: 0x06003B5A RID: 15194 RVA: 0x001028AC File Offset: 0x00100AAC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			switch (index)
			{
			case 0:
				return base.GetRedAtZero(ref this.attributeLists);
			case 3:
				return base.GetRed<NameSyntax>(ref this.name, 3);
			case 5:
				return base.GetRed(ref this.externs, 5);
			case 6:
				return base.GetRed(ref this.usings, 6);
			case 7:
				return base.GetRed(ref this.members, 7);
			}
			return null;
		}

		// Token: 0x06003B5B RID: 15195 RVA: 0x00102934 File Offset: 0x00100B34
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			switch (index)
			{
			case 0:
				return this.attributeLists;
			case 3:
				return this.name;
			case 5:
				return this.externs;
			case 6:
				return this.usings;
			case 7:
				return this.members;
			}
			return null;
		}

		// Token: 0x06003B5C RID: 15196 RVA: 0x00102999 File Offset: 0x00100B99
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitNamespaceDeclaration(this);
		}

		// Token: 0x06003B5D RID: 15197 RVA: 0x001029A2 File Offset: 0x00100BA2
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitNamespaceDeclaration(this);
		}

		// Token: 0x06003B5E RID: 15198 RVA: 0x001029AC File Offset: 0x00100BAC
		public NamespaceDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken namespaceKeyword, NameSyntax name, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<ExternAliasDirectiveSyntax> externs, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<UsingDirectiveSyntax> usings, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
		{
			if (!(attributeLists != this.AttributeLists) && !(modifiers != this.Modifiers) && !(namespaceKeyword != this.NamespaceKeyword) && name == this.Name && !(openBraceToken != this.OpenBraceToken) && !(externs != this.Externs) && !(usings != this.Usings) && !(members != this.Members) && !(closeBraceToken != this.CloseBraceToken) && !(semicolonToken != this.SemicolonToken))
			{
				return this;
			}
			NamespaceDeclarationSyntax namespaceDeclarationSyntax = SyntaxFactory.NamespaceDeclaration(attributeLists, modifiers, namespaceKeyword, name, openBraceToken, externs, usings, members, closeBraceToken, semicolonToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return namespaceDeclarationSyntax;
			}
			return namespaceDeclarationSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003B5F RID: 15199 RVA: 0x00102A7A File Offset: 0x00100C7A
		internal override MemberDeclarationSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x06003B60 RID: 15200 RVA: 0x00102A84 File Offset: 0x00100C84
		public new NamespaceDeclarationSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.Modifiers, this.NamespaceKeyword, this.Name, this.OpenBraceToken, this.Externs, this.Usings, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x06003B61 RID: 15201 RVA: 0x00102ACE File Offset: 0x00100CCE
		internal override MemberDeclarationSyntax WithModifiersCore(SyntaxTokenList modifiers)
		{
			return this.WithModifiers(modifiers);
		}

		// Token: 0x06003B62 RID: 15202 RVA: 0x00102AD8 File Offset: 0x00100CD8
		public new NamespaceDeclarationSyntax WithModifiers(SyntaxTokenList modifiers)
		{
			return this.Update(this.AttributeLists, modifiers, this.NamespaceKeyword, this.Name, this.OpenBraceToken, this.Externs, this.Usings, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x06003B63 RID: 15203 RVA: 0x00102B24 File Offset: 0x00100D24
		public NamespaceDeclarationSyntax WithNamespaceKeyword(SyntaxToken namespaceKeyword)
		{
			return this.Update(this.AttributeLists, this.Modifiers, namespaceKeyword, this.Name, this.OpenBraceToken, this.Externs, this.Usings, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x06003B64 RID: 15204 RVA: 0x00102B70 File Offset: 0x00100D70
		public NamespaceDeclarationSyntax WithName(NameSyntax name)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.NamespaceKeyword, name, this.OpenBraceToken, this.Externs, this.Usings, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x06003B65 RID: 15205 RVA: 0x00102BBC File Offset: 0x00100DBC
		public NamespaceDeclarationSyntax WithOpenBraceToken(SyntaxToken openBraceToken)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.NamespaceKeyword, this.Name, openBraceToken, this.Externs, this.Usings, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x06003B66 RID: 15206 RVA: 0x00102C08 File Offset: 0x00100E08
		public NamespaceDeclarationSyntax WithExterns([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<ExternAliasDirectiveSyntax> externs)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.NamespaceKeyword, this.Name, this.OpenBraceToken, externs, this.Usings, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x06003B67 RID: 15207 RVA: 0x00102C54 File Offset: 0x00100E54
		public NamespaceDeclarationSyntax WithUsings([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<UsingDirectiveSyntax> usings)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.NamespaceKeyword, this.Name, this.OpenBraceToken, this.Externs, usings, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x06003B68 RID: 15208 RVA: 0x00102CA0 File Offset: 0x00100EA0
		public NamespaceDeclarationSyntax WithMembers([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.NamespaceKeyword, this.Name, this.OpenBraceToken, this.Externs, this.Usings, members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x06003B69 RID: 15209 RVA: 0x00102CEC File Offset: 0x00100EEC
		public NamespaceDeclarationSyntax WithCloseBraceToken(SyntaxToken closeBraceToken)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.NamespaceKeyword, this.Name, this.OpenBraceToken, this.Externs, this.Usings, this.Members, closeBraceToken, this.SemicolonToken);
		}

		// Token: 0x06003B6A RID: 15210 RVA: 0x00102D38 File Offset: 0x00100F38
		public NamespaceDeclarationSyntax WithSemicolonToken(SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.NamespaceKeyword, this.Name, this.OpenBraceToken, this.Externs, this.Usings, this.Members, this.CloseBraceToken, semicolonToken);
		}

		// Token: 0x06003B6B RID: 15211 RVA: 0x00102D82 File Offset: 0x00100F82
		internal override MemberDeclarationSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x06003B6C RID: 15212 RVA: 0x00102D8C File Offset: 0x00100F8C
		public new NamespaceDeclarationSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x06003B6D RID: 15213 RVA: 0x00102DAE File Offset: 0x00100FAE
		internal override MemberDeclarationSyntax AddModifiersCore(params SyntaxToken[] items)
		{
			return this.AddModifiers(items);
		}

		// Token: 0x06003B6E RID: 15214 RVA: 0x00102DB8 File Offset: 0x00100FB8
		public new NamespaceDeclarationSyntax AddModifiers(params SyntaxToken[] items)
		{
			return this.WithModifiers(this.Modifiers.AddRange(items));
		}

		// Token: 0x06003B6F RID: 15215 RVA: 0x00102DDC File Offset: 0x00100FDC
		public NamespaceDeclarationSyntax AddExterns(params ExternAliasDirectiveSyntax[] items)
		{
			return this.WithExterns(this.Externs.AddRange(items));
		}

		// Token: 0x06003B70 RID: 15216 RVA: 0x00102E00 File Offset: 0x00101000
		public NamespaceDeclarationSyntax AddUsings(params UsingDirectiveSyntax[] items)
		{
			return this.WithUsings(this.Usings.AddRange(items));
		}

		// Token: 0x06003B71 RID: 15217 RVA: 0x00102E24 File Offset: 0x00101024
		public NamespaceDeclarationSyntax AddMembers(params MemberDeclarationSyntax[] items)
		{
			return this.WithMembers(this.Members.AddRange(items));
		}

		// Token: 0x040013F9 RID: 5113
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x040013FA RID: 5114
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private NameSyntax name;

		// Token: 0x040013FB RID: 5115
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode externs;

		// Token: 0x040013FC RID: 5116
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode usings;

		// Token: 0x040013FD RID: 5117
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode members;
	}
}
