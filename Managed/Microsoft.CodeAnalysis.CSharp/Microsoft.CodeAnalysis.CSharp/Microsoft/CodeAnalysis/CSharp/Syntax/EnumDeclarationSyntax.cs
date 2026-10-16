using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000311 RID: 785
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class EnumDeclarationSyntax : BaseTypeDeclarationSyntax
	{
		// Token: 0x0600440E RID: 17422 RVA: 0x001141F6 File Offset: 0x001123F6
		internal EnumDeclarationSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001187 RID: 4487
		// (get) Token: 0x0600440F RID: 17423 RVA: 0x00114201 File Offset: 0x00112401
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17001188 RID: 4488
		// (get) Token: 0x06004410 RID: 17424 RVA: 0x00114218 File Offset: 0x00112418
		public override SyntaxTokenList Modifiers
		{
			get
			{
				GreenNode slot = base.Green.GetSlot(1);
				if (slot == null)
				{
					return default(SyntaxTokenList);
				}
				return new SyntaxTokenList(this, slot, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17001189 RID: 4489
		// (get) Token: 0x06004411 RID: 17425 RVA: 0x00114254 File Offset: 0x00112454
		public SyntaxToken EnumKeyword
		{
			get
			{
				return new SyntaxToken(this, ((EnumDeclarationSyntax)base.Green).enumKeyword, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x1700118A RID: 4490
		// (get) Token: 0x06004412 RID: 17426 RVA: 0x0011427A File Offset: 0x0011247A
		public override SyntaxToken Identifier
		{
			get
			{
				return new SyntaxToken(this, ((EnumDeclarationSyntax)base.Green).identifier, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x1700118B RID: 4491
		// (get) Token: 0x06004413 RID: 17427 RVA: 0x001142A0 File Offset: 0x001124A0
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override BaseListSyntax BaseList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<BaseListSyntax>(ref this.baseList, 4);
			}
		}

		// Token: 0x1700118C RID: 4492
		// (get) Token: 0x06004414 RID: 17428 RVA: 0x001142AF File Offset: 0x001124AF
		public override SyntaxToken OpenBraceToken
		{
			get
			{
				return new SyntaxToken(this, ((EnumDeclarationSyntax)base.Green).openBraceToken, this.GetChildPosition(5), base.GetChildIndex(5));
			}
		}

		// Token: 0x1700118D RID: 4493
		// (get) Token: 0x06004415 RID: 17429 RVA: 0x001142D8 File Offset: 0x001124D8
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<EnumMemberDeclarationSyntax> Members
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				SyntaxNode red = base.GetRed(ref this.members, 6);
				if (red == null)
				{
					return default(SeparatedSyntaxList<EnumMemberDeclarationSyntax>);
				}
				return new SeparatedSyntaxList<EnumMemberDeclarationSyntax>(red, base.GetChildIndex(6));
			}
		}

		// Token: 0x1700118E RID: 4494
		// (get) Token: 0x06004416 RID: 17430 RVA: 0x0011430D File Offset: 0x0011250D
		public override SyntaxToken CloseBraceToken
		{
			get
			{
				return new SyntaxToken(this, ((EnumDeclarationSyntax)base.Green).closeBraceToken, this.GetChildPosition(7), base.GetChildIndex(7));
			}
		}

		// Token: 0x1700118F RID: 4495
		// (get) Token: 0x06004417 RID: 17431 RVA: 0x00114334 File Offset: 0x00112534
		public override SyntaxToken SemicolonToken
		{
			get
			{
				SyntaxToken semicolonToken = ((EnumDeclarationSyntax)base.Green).semicolonToken;
				if (semicolonToken == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, semicolonToken, this.GetChildPosition(8), base.GetChildIndex(8));
			}
		}

		// Token: 0x06004418 RID: 17432 RVA: 0x00114374 File Offset: 0x00112574
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 4)
				{
					if (index != 6)
					{
						syntaxNode = null;
					}
					else
					{
						syntaxNode = base.GetRed(ref this.members, 6);
					}
				}
				else
				{
					syntaxNode = base.GetRed<BaseListSyntax>(ref this.baseList, 4);
				}
			}
			else
			{
				syntaxNode = base.GetRedAtZero(ref this.attributeLists);
			}
			return syntaxNode;
		}

		// Token: 0x06004419 RID: 17433 RVA: 0x001143C0 File Offset: 0x001125C0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 4)
				{
					if (index != 6)
					{
						syntaxNode = null;
					}
					else
					{
						syntaxNode = this.members;
					}
				}
				else
				{
					syntaxNode = this.baseList;
				}
			}
			else
			{
				syntaxNode = this.attributeLists;
			}
			return syntaxNode;
		}

		// Token: 0x0600441A RID: 17434 RVA: 0x001143F8 File Offset: 0x001125F8
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitEnumDeclaration(this);
		}

		// Token: 0x0600441B RID: 17435 RVA: 0x00114401 File Offset: 0x00112601
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitEnumDeclaration(this);
		}

		// Token: 0x0600441C RID: 17436 RVA: 0x0011440C File Offset: 0x0011260C
		public EnumDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken enumKeyword, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] BaseListSyntax baseList, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<EnumMemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
		{
			if (!(attributeLists != this.AttributeLists) && !(modifiers != this.Modifiers) && !(enumKeyword != this.EnumKeyword) && !(identifier != this.Identifier) && baseList == this.BaseList && !(openBraceToken != this.OpenBraceToken) && !(members != this.Members) && !(closeBraceToken != this.CloseBraceToken) && !(semicolonToken != this.SemicolonToken))
			{
				return this;
			}
			EnumDeclarationSyntax enumDeclarationSyntax = SyntaxFactory.EnumDeclaration(attributeLists, modifiers, enumKeyword, identifier, baseList, openBraceToken, members, closeBraceToken, semicolonToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return enumDeclarationSyntax;
			}
			return enumDeclarationSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600441D RID: 17437 RVA: 0x001144C6 File Offset: 0x001126C6
		internal override MemberDeclarationSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x0600441E RID: 17438 RVA: 0x001144D0 File Offset: 0x001126D0
		public new EnumDeclarationSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.Modifiers, this.EnumKeyword, this.Identifier, this.BaseList, this.OpenBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x0600441F RID: 17439 RVA: 0x00114514 File Offset: 0x00112714
		internal override MemberDeclarationSyntax WithModifiersCore(SyntaxTokenList modifiers)
		{
			return this.WithModifiers(modifiers);
		}

		// Token: 0x06004420 RID: 17440 RVA: 0x00114520 File Offset: 0x00112720
		public new EnumDeclarationSyntax WithModifiers(SyntaxTokenList modifiers)
		{
			return this.Update(this.AttributeLists, modifiers, this.EnumKeyword, this.Identifier, this.BaseList, this.OpenBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x06004421 RID: 17441 RVA: 0x00114564 File Offset: 0x00112764
		public EnumDeclarationSyntax WithEnumKeyword(SyntaxToken enumKeyword)
		{
			return this.Update(this.AttributeLists, this.Modifiers, enumKeyword, this.Identifier, this.BaseList, this.OpenBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x06004422 RID: 17442 RVA: 0x001145A8 File Offset: 0x001127A8
		internal override BaseTypeDeclarationSyntax WithIdentifierCore(SyntaxToken identifier)
		{
			return this.WithIdentifier(identifier);
		}

		// Token: 0x06004423 RID: 17443 RVA: 0x001145B4 File Offset: 0x001127B4
		public new EnumDeclarationSyntax WithIdentifier(SyntaxToken identifier)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.EnumKeyword, identifier, this.BaseList, this.OpenBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x06004424 RID: 17444 RVA: 0x001145F8 File Offset: 0x001127F8
		internal override BaseTypeDeclarationSyntax WithBaseListCore([global::System.Runtime.CompilerServices.Nullable(2)] BaseListSyntax baseList)
		{
			return this.WithBaseList(baseList);
		}

		// Token: 0x06004425 RID: 17445 RVA: 0x00114604 File Offset: 0x00112804
		public new EnumDeclarationSyntax WithBaseList([global::System.Runtime.CompilerServices.Nullable(2)] BaseListSyntax baseList)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.EnumKeyword, this.Identifier, baseList, this.OpenBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x06004426 RID: 17446 RVA: 0x00114648 File Offset: 0x00112848
		internal override BaseTypeDeclarationSyntax WithOpenBraceTokenCore(SyntaxToken openBraceToken)
		{
			return this.WithOpenBraceToken(openBraceToken);
		}

		// Token: 0x06004427 RID: 17447 RVA: 0x00114654 File Offset: 0x00112854
		public new EnumDeclarationSyntax WithOpenBraceToken(SyntaxToken openBraceToken)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.EnumKeyword, this.Identifier, this.BaseList, openBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x06004428 RID: 17448 RVA: 0x00114698 File Offset: 0x00112898
		public EnumDeclarationSyntax WithMembers([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<EnumMemberDeclarationSyntax> members)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.EnumKeyword, this.Identifier, this.BaseList, this.OpenBraceToken, members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x06004429 RID: 17449 RVA: 0x001146DC File Offset: 0x001128DC
		internal override BaseTypeDeclarationSyntax WithCloseBraceTokenCore(SyntaxToken closeBraceToken)
		{
			return this.WithCloseBraceToken(closeBraceToken);
		}

		// Token: 0x0600442A RID: 17450 RVA: 0x001146E8 File Offset: 0x001128E8
		public new EnumDeclarationSyntax WithCloseBraceToken(SyntaxToken closeBraceToken)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.EnumKeyword, this.Identifier, this.BaseList, this.OpenBraceToken, this.Members, closeBraceToken, this.SemicolonToken);
		}

		// Token: 0x0600442B RID: 17451 RVA: 0x0011472C File Offset: 0x0011292C
		internal override BaseTypeDeclarationSyntax WithSemicolonTokenCore(SyntaxToken semicolonToken)
		{
			return this.WithSemicolonToken(semicolonToken);
		}

		// Token: 0x0600442C RID: 17452 RVA: 0x00114738 File Offset: 0x00112938
		public new EnumDeclarationSyntax WithSemicolonToken(SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.EnumKeyword, this.Identifier, this.BaseList, this.OpenBraceToken, this.Members, this.CloseBraceToken, semicolonToken);
		}

		// Token: 0x0600442D RID: 17453 RVA: 0x0011477C File Offset: 0x0011297C
		internal override MemberDeclarationSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x0600442E RID: 17454 RVA: 0x00114788 File Offset: 0x00112988
		public new EnumDeclarationSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x0600442F RID: 17455 RVA: 0x001147AA File Offset: 0x001129AA
		internal override MemberDeclarationSyntax AddModifiersCore(params SyntaxToken[] items)
		{
			return this.AddModifiers(items);
		}

		// Token: 0x06004430 RID: 17456 RVA: 0x001147B4 File Offset: 0x001129B4
		public new EnumDeclarationSyntax AddModifiers(params SyntaxToken[] items)
		{
			return this.WithModifiers(this.Modifiers.AddRange(items));
		}

		// Token: 0x06004431 RID: 17457 RVA: 0x001147D6 File Offset: 0x001129D6
		internal override BaseTypeDeclarationSyntax AddBaseListTypesCore(params BaseTypeSyntax[] items)
		{
			return this.AddBaseListTypes(items);
		}

		// Token: 0x06004432 RID: 17458 RVA: 0x001147E0 File Offset: 0x001129E0
		public new EnumDeclarationSyntax AddBaseListTypes(params BaseTypeSyntax[] items)
		{
			BaseListSyntax baseListSyntax;
			if ((baseListSyntax = this.BaseList) == null)
			{
				baseListSyntax = SyntaxFactory.BaseList(default(SeparatedSyntaxList<BaseTypeSyntax>));
			}
			BaseListSyntax baseListSyntax2 = baseListSyntax;
			return this.WithBaseList(baseListSyntax2.WithTypes(baseListSyntax2.Types.AddRange(items)));
		}

		// Token: 0x06004433 RID: 17459 RVA: 0x00114824 File Offset: 0x00112A24
		public EnumDeclarationSyntax AddMembers(params EnumMemberDeclarationSyntax[] items)
		{
			return this.WithMembers(this.Members.AddRange(items));
		}

		// Token: 0x040014EE RID: 5358
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x040014EF RID: 5359
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private BaseListSyntax baseList;

		// Token: 0x040014F0 RID: 5360
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode members;
	}
}
