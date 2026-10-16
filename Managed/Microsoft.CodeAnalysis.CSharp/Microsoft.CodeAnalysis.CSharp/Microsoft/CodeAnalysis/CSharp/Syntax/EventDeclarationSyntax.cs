using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200025C RID: 604
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class EventDeclarationSyntax : BasePropertyDeclarationSyntax
	{
		// Token: 0x06003970 RID: 14704 RVA: 0x000FD7A4 File Offset: 0x000FB9A4
		public EventDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken eventKeyword, TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, AccessorListSyntax accessorList)
		{
			return this.Update(attributeLists, modifiers, eventKeyword, type, explicitInterfaceSpecifier, identifier, accessorList, this.SemicolonToken);
		}

		// Token: 0x06003971 RID: 14705 RVA: 0x000FD7C8 File Offset: 0x000FB9C8
		public EventDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken eventKeyword, TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, SyntaxToken semicolonToken)
		{
			return this.Update(attributeLists, modifiers, eventKeyword, type, explicitInterfaceSpecifier, identifier, this.AccessorList, semicolonToken);
		}

		// Token: 0x06003972 RID: 14706 RVA: 0x000FD7EC File Offset: 0x000FB9EC
		internal EventDeclarationSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x06003973 RID: 14707 RVA: 0x000FD7F7 File Offset: 0x000FB9F7
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x06003974 RID: 14708 RVA: 0x000FD80C File Offset: 0x000FBA0C
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

		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x06003975 RID: 14709 RVA: 0x000FD848 File Offset: 0x000FBA48
		public SyntaxToken EventKeyword
		{
			get
			{
				return new SyntaxToken(this, ((EventDeclarationSyntax)base.Green).eventKeyword, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x06003976 RID: 14710 RVA: 0x000FD86E File Offset: 0x000FBA6E
		public override TypeSyntax Type
		{
			get
			{
				return base.GetRed<TypeSyntax>(ref this.type, 3);
			}
		}

		// Token: 0x17000F24 RID: 3876
		// (get) Token: 0x06003977 RID: 14711 RVA: 0x000FD87D File Offset: 0x000FBA7D
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override ExplicitInterfaceSpecifierSyntax ExplicitInterfaceSpecifier
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<ExplicitInterfaceSpecifierSyntax>(ref this.explicitInterfaceSpecifier, 4);
			}
		}

		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x06003978 RID: 14712 RVA: 0x000FD88C File Offset: 0x000FBA8C
		public SyntaxToken Identifier
		{
			get
			{
				return new SyntaxToken(this, ((EventDeclarationSyntax)base.Green).identifier, this.GetChildPosition(5), base.GetChildIndex(5));
			}
		}

		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x06003979 RID: 14713 RVA: 0x000FD8B2 File Offset: 0x000FBAB2
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override AccessorListSyntax AccessorList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<AccessorListSyntax>(ref this.accessorList, 6);
			}
		}

		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x0600397A RID: 14714 RVA: 0x000FD8C4 File Offset: 0x000FBAC4
		public SyntaxToken SemicolonToken
		{
			get
			{
				SyntaxToken semicolonToken = ((EventDeclarationSyntax)base.Green).semicolonToken;
				if (semicolonToken == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, semicolonToken, this.GetChildPosition(7), base.GetChildIndex(7));
			}
		}

		// Token: 0x0600397B RID: 14715 RVA: 0x000FD904 File Offset: 0x000FBB04
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			switch (index)
			{
			case 0:
				return base.GetRedAtZero(ref this.attributeLists);
			case 3:
				return base.GetRed<TypeSyntax>(ref this.type, 3);
			case 4:
				return base.GetRed<ExplicitInterfaceSpecifierSyntax>(ref this.explicitInterfaceSpecifier, 4);
			case 6:
				return base.GetRed<AccessorListSyntax>(ref this.accessorList, 6);
			}
			return null;
		}

		// Token: 0x0600397C RID: 14716 RVA: 0x000FD978 File Offset: 0x000FBB78
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			switch (index)
			{
			case 0:
				return this.attributeLists;
			case 3:
				return this.type;
			case 4:
				return this.explicitInterfaceSpecifier;
			case 6:
				return this.accessorList;
			}
			return null;
		}

		// Token: 0x0600397D RID: 14717 RVA: 0x000FD9D0 File Offset: 0x000FBBD0
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitEventDeclaration(this);
		}

		// Token: 0x0600397E RID: 14718 RVA: 0x000FD9D9 File Offset: 0x000FBBD9
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitEventDeclaration(this);
		}

		// Token: 0x0600397F RID: 14719 RVA: 0x000FD9E4 File Offset: 0x000FBBE4
		public EventDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken eventKeyword, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] AccessorListSyntax accessorList, SyntaxToken semicolonToken)
		{
			if (!(attributeLists != this.AttributeLists) && !(modifiers != this.Modifiers) && !(eventKeyword != this.EventKeyword) && type == this.Type && explicitInterfaceSpecifier == this.ExplicitInterfaceSpecifier && !(identifier != this.Identifier) && accessorList == this.AccessorList && !(semicolonToken != this.SemicolonToken))
			{
				return this;
			}
			EventDeclarationSyntax eventDeclarationSyntax = SyntaxFactory.EventDeclaration(attributeLists, modifiers, eventKeyword, type, explicitInterfaceSpecifier, identifier, accessorList, semicolonToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return eventDeclarationSyntax;
			}
			return eventDeclarationSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003980 RID: 14720 RVA: 0x000FDA83 File Offset: 0x000FBC83
		internal override MemberDeclarationSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x06003981 RID: 14721 RVA: 0x000FDA8C File Offset: 0x000FBC8C
		public new EventDeclarationSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.Modifiers, this.EventKeyword, this.Type, this.ExplicitInterfaceSpecifier, this.Identifier, this.AccessorList, this.SemicolonToken);
		}

		// Token: 0x06003982 RID: 14722 RVA: 0x000FDACA File Offset: 0x000FBCCA
		internal override MemberDeclarationSyntax WithModifiersCore(SyntaxTokenList modifiers)
		{
			return this.WithModifiers(modifiers);
		}

		// Token: 0x06003983 RID: 14723 RVA: 0x000FDAD4 File Offset: 0x000FBCD4
		public new EventDeclarationSyntax WithModifiers(SyntaxTokenList modifiers)
		{
			return this.Update(this.AttributeLists, modifiers, this.EventKeyword, this.Type, this.ExplicitInterfaceSpecifier, this.Identifier, this.AccessorList, this.SemicolonToken);
		}

		// Token: 0x06003984 RID: 14724 RVA: 0x000FDB14 File Offset: 0x000FBD14
		public EventDeclarationSyntax WithEventKeyword(SyntaxToken eventKeyword)
		{
			return this.Update(this.AttributeLists, this.Modifiers, eventKeyword, this.Type, this.ExplicitInterfaceSpecifier, this.Identifier, this.AccessorList, this.SemicolonToken);
		}

		// Token: 0x06003985 RID: 14725 RVA: 0x000FDB52 File Offset: 0x000FBD52
		internal override BasePropertyDeclarationSyntax WithTypeCore(TypeSyntax type)
		{
			return this.WithType(type);
		}

		// Token: 0x06003986 RID: 14726 RVA: 0x000FDB5C File Offset: 0x000FBD5C
		public new EventDeclarationSyntax WithType(TypeSyntax type)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.EventKeyword, type, this.ExplicitInterfaceSpecifier, this.Identifier, this.AccessorList, this.SemicolonToken);
		}

		// Token: 0x06003987 RID: 14727 RVA: 0x000FDB9A File Offset: 0x000FBD9A
		internal override BasePropertyDeclarationSyntax WithExplicitInterfaceSpecifierCore([global::System.Runtime.CompilerServices.Nullable(2)] ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier)
		{
			return this.WithExplicitInterfaceSpecifier(explicitInterfaceSpecifier);
		}

		// Token: 0x06003988 RID: 14728 RVA: 0x000FDBA4 File Offset: 0x000FBDA4
		public new EventDeclarationSyntax WithExplicitInterfaceSpecifier([global::System.Runtime.CompilerServices.Nullable(2)] ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.EventKeyword, this.Type, explicitInterfaceSpecifier, this.Identifier, this.AccessorList, this.SemicolonToken);
		}

		// Token: 0x06003989 RID: 14729 RVA: 0x000FDBE4 File Offset: 0x000FBDE4
		public EventDeclarationSyntax WithIdentifier(SyntaxToken identifier)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.EventKeyword, this.Type, this.ExplicitInterfaceSpecifier, identifier, this.AccessorList, this.SemicolonToken);
		}

		// Token: 0x0600398A RID: 14730 RVA: 0x000FDC22 File Offset: 0x000FBE22
		internal override BasePropertyDeclarationSyntax WithAccessorListCore([global::System.Runtime.CompilerServices.Nullable(2)] AccessorListSyntax accessorList)
		{
			return this.WithAccessorList(accessorList);
		}

		// Token: 0x0600398B RID: 14731 RVA: 0x000FDC2C File Offset: 0x000FBE2C
		public new EventDeclarationSyntax WithAccessorList([global::System.Runtime.CompilerServices.Nullable(2)] AccessorListSyntax accessorList)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.EventKeyword, this.Type, this.ExplicitInterfaceSpecifier, this.Identifier, accessorList, this.SemicolonToken);
		}

		// Token: 0x0600398C RID: 14732 RVA: 0x000FDC6C File Offset: 0x000FBE6C
		public EventDeclarationSyntax WithSemicolonToken(SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.EventKeyword, this.Type, this.ExplicitInterfaceSpecifier, this.Identifier, this.AccessorList, semicolonToken);
		}

		// Token: 0x0600398D RID: 14733 RVA: 0x000FDCAA File Offset: 0x000FBEAA
		internal override MemberDeclarationSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x0600398E RID: 14734 RVA: 0x000FDCB4 File Offset: 0x000FBEB4
		public new EventDeclarationSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x0600398F RID: 14735 RVA: 0x000FDCD6 File Offset: 0x000FBED6
		internal override MemberDeclarationSyntax AddModifiersCore(params SyntaxToken[] items)
		{
			return this.AddModifiers(items);
		}

		// Token: 0x06003990 RID: 14736 RVA: 0x000FDCE0 File Offset: 0x000FBEE0
		public new EventDeclarationSyntax AddModifiers(params SyntaxToken[] items)
		{
			return this.WithModifiers(this.Modifiers.AddRange(items));
		}

		// Token: 0x06003991 RID: 14737 RVA: 0x000FDD02 File Offset: 0x000FBF02
		internal override BasePropertyDeclarationSyntax AddAccessorListAccessorsCore(params AccessorDeclarationSyntax[] items)
		{
			return this.AddAccessorListAccessors(items);
		}

		// Token: 0x06003992 RID: 14738 RVA: 0x000FDD0C File Offset: 0x000FBF0C
		public new EventDeclarationSyntax AddAccessorListAccessors(params AccessorDeclarationSyntax[] items)
		{
			AccessorListSyntax accessorListSyntax;
			if ((accessorListSyntax = this.AccessorList) == null)
			{
				accessorListSyntax = SyntaxFactory.AccessorList(default(SyntaxList<AccessorDeclarationSyntax>));
			}
			AccessorListSyntax accessorListSyntax2 = accessorListSyntax;
			return this.WithAccessorList(accessorListSyntax2.WithAccessors(accessorListSyntax2.Accessors.AddRange(items)));
		}

		// Token: 0x040013BF RID: 5055
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x040013C0 RID: 5056
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax type;

		// Token: 0x040013C1 RID: 5057
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier;

		// Token: 0x040013C2 RID: 5058
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private AccessorListSyntax accessorList;
	}
}
