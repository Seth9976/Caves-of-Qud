using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000267 RID: 615
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class IndexerDeclarationSyntax : BasePropertyDeclarationSyntax
	{
		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x06003A7E RID: 14974 RVA: 0x001000E6 File Offset: 0x000FE2E6
		[EditorBrowsable(1)]
		[Obsolete("This member is obsolete.", true)]
		public SyntaxToken Semicolon
		{
			get
			{
				return this.SemicolonToken;
			}
		}

		// Token: 0x06003A7F RID: 14975 RVA: 0x001000EE File Offset: 0x000FE2EE
		[EditorBrowsable(1)]
		[Obsolete("This member is obsolete.", true)]
		public IndexerDeclarationSyntax WithSemicolon(SyntaxToken semicolon)
		{
			return this.WithSemicolonToken(semicolon);
		}

		// Token: 0x06003A80 RID: 14976 RVA: 0x001000F7 File Offset: 0x000FE2F7
		internal IndexerDeclarationSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x06003A81 RID: 14977 RVA: 0x00100102 File Offset: 0x000FE302
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x06003A82 RID: 14978 RVA: 0x00100118 File Offset: 0x000FE318
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

		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x06003A83 RID: 14979 RVA: 0x00100154 File Offset: 0x000FE354
		public override TypeSyntax Type
		{
			get
			{
				return base.GetRed<TypeSyntax>(ref this.type, 2);
			}
		}

		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x06003A84 RID: 14980 RVA: 0x00100163 File Offset: 0x000FE363
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override ExplicitInterfaceSpecifierSyntax ExplicitInterfaceSpecifier
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<ExplicitInterfaceSpecifierSyntax>(ref this.explicitInterfaceSpecifier, 3);
			}
		}

		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x06003A85 RID: 14981 RVA: 0x00100172 File Offset: 0x000FE372
		public SyntaxToken ThisKeyword
		{
			get
			{
				return new SyntaxToken(this, ((IndexerDeclarationSyntax)base.Green).thisKeyword, this.GetChildPosition(4), base.GetChildIndex(4));
			}
		}

		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x06003A86 RID: 14982 RVA: 0x00100198 File Offset: 0x000FE398
		public BracketedParameterListSyntax ParameterList
		{
			get
			{
				return base.GetRed<BracketedParameterListSyntax>(ref this.parameterList, 5);
			}
		}

		// Token: 0x17000F6A RID: 3946
		// (get) Token: 0x06003A87 RID: 14983 RVA: 0x001001A7 File Offset: 0x000FE3A7
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override AccessorListSyntax AccessorList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<AccessorListSyntax>(ref this.accessorList, 6);
			}
		}

		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x06003A88 RID: 14984 RVA: 0x001001B6 File Offset: 0x000FE3B6
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public ArrowExpressionClauseSyntax ExpressionBody
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<ArrowExpressionClauseSyntax>(ref this.expressionBody, 7);
			}
		}

		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x06003A89 RID: 14985 RVA: 0x001001C8 File Offset: 0x000FE3C8
		public SyntaxToken SemicolonToken
		{
			get
			{
				SyntaxToken semicolonToken = ((IndexerDeclarationSyntax)base.Green).semicolonToken;
				if (semicolonToken == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, semicolonToken, this.GetChildPosition(8), base.GetChildIndex(8));
			}
		}

		// Token: 0x06003A8A RID: 14986 RVA: 0x00100208 File Offset: 0x000FE408
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			switch (index)
			{
			case 0:
				return base.GetRedAtZero(ref this.attributeLists);
			case 2:
				return base.GetRed<TypeSyntax>(ref this.type, 2);
			case 3:
				return base.GetRed<ExplicitInterfaceSpecifierSyntax>(ref this.explicitInterfaceSpecifier, 3);
			case 5:
				return base.GetRed<BracketedParameterListSyntax>(ref this.parameterList, 5);
			case 6:
				return base.GetRed<AccessorListSyntax>(ref this.accessorList, 6);
			case 7:
				return base.GetRed<ArrowExpressionClauseSyntax>(ref this.expressionBody, 7);
			}
			return null;
		}

		// Token: 0x06003A8B RID: 14987 RVA: 0x001002A0 File Offset: 0x000FE4A0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			switch (index)
			{
			case 0:
				return this.attributeLists;
			case 2:
				return this.type;
			case 3:
				return this.explicitInterfaceSpecifier;
			case 5:
				return this.parameterList;
			case 6:
				return this.accessorList;
			case 7:
				return this.expressionBody;
			}
			return null;
		}

		// Token: 0x06003A8C RID: 14988 RVA: 0x0010030E File Offset: 0x000FE50E
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitIndexerDeclaration(this);
		}

		// Token: 0x06003A8D RID: 14989 RVA: 0x00100317 File Offset: 0x000FE517
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitIndexerDeclaration(this);
		}

		// Token: 0x06003A8E RID: 14990 RVA: 0x00100320 File Offset: 0x000FE520
		public IndexerDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken thisKeyword, BracketedParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(2)] AccessorListSyntax accessorList, [global::System.Runtime.CompilerServices.Nullable(2)] ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
		{
			if (!(attributeLists != this.AttributeLists) && !(modifiers != this.Modifiers) && type == this.Type && explicitInterfaceSpecifier == this.ExplicitInterfaceSpecifier && !(thisKeyword != this.ThisKeyword) && parameterList == this.ParameterList && accessorList == this.AccessorList && expressionBody == this.ExpressionBody && !(semicolonToken != this.SemicolonToken))
			{
				return this;
			}
			IndexerDeclarationSyntax indexerDeclarationSyntax = SyntaxFactory.IndexerDeclaration(attributeLists, modifiers, type, explicitInterfaceSpecifier, thisKeyword, parameterList, accessorList, expressionBody, semicolonToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return indexerDeclarationSyntax;
			}
			return indexerDeclarationSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003A8F RID: 14991 RVA: 0x001003C6 File Offset: 0x000FE5C6
		internal override MemberDeclarationSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x06003A90 RID: 14992 RVA: 0x001003D0 File Offset: 0x000FE5D0
		public new IndexerDeclarationSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.Modifiers, this.Type, this.ExplicitInterfaceSpecifier, this.ThisKeyword, this.ParameterList, this.AccessorList, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x06003A91 RID: 14993 RVA: 0x00100414 File Offset: 0x000FE614
		internal override MemberDeclarationSyntax WithModifiersCore(SyntaxTokenList modifiers)
		{
			return this.WithModifiers(modifiers);
		}

		// Token: 0x06003A92 RID: 14994 RVA: 0x00100420 File Offset: 0x000FE620
		public new IndexerDeclarationSyntax WithModifiers(SyntaxTokenList modifiers)
		{
			return this.Update(this.AttributeLists, modifiers, this.Type, this.ExplicitInterfaceSpecifier, this.ThisKeyword, this.ParameterList, this.AccessorList, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x06003A93 RID: 14995 RVA: 0x00100464 File Offset: 0x000FE664
		internal override BasePropertyDeclarationSyntax WithTypeCore(TypeSyntax type)
		{
			return this.WithType(type);
		}

		// Token: 0x06003A94 RID: 14996 RVA: 0x00100470 File Offset: 0x000FE670
		public new IndexerDeclarationSyntax WithType(TypeSyntax type)
		{
			return this.Update(this.AttributeLists, this.Modifiers, type, this.ExplicitInterfaceSpecifier, this.ThisKeyword, this.ParameterList, this.AccessorList, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x06003A95 RID: 14997 RVA: 0x001004B4 File Offset: 0x000FE6B4
		internal override BasePropertyDeclarationSyntax WithExplicitInterfaceSpecifierCore([global::System.Runtime.CompilerServices.Nullable(2)] ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier)
		{
			return this.WithExplicitInterfaceSpecifier(explicitInterfaceSpecifier);
		}

		// Token: 0x06003A96 RID: 14998 RVA: 0x001004C0 File Offset: 0x000FE6C0
		public new IndexerDeclarationSyntax WithExplicitInterfaceSpecifier([global::System.Runtime.CompilerServices.Nullable(2)] ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Type, explicitInterfaceSpecifier, this.ThisKeyword, this.ParameterList, this.AccessorList, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x06003A97 RID: 14999 RVA: 0x00100504 File Offset: 0x000FE704
		public IndexerDeclarationSyntax WithThisKeyword(SyntaxToken thisKeyword)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Type, this.ExplicitInterfaceSpecifier, thisKeyword, this.ParameterList, this.AccessorList, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x06003A98 RID: 15000 RVA: 0x00100548 File Offset: 0x000FE748
		public IndexerDeclarationSyntax WithParameterList(BracketedParameterListSyntax parameterList)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Type, this.ExplicitInterfaceSpecifier, this.ThisKeyword, parameterList, this.AccessorList, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x06003A99 RID: 15001 RVA: 0x0010058C File Offset: 0x000FE78C
		internal override BasePropertyDeclarationSyntax WithAccessorListCore([global::System.Runtime.CompilerServices.Nullable(2)] AccessorListSyntax accessorList)
		{
			return this.WithAccessorList(accessorList);
		}

		// Token: 0x06003A9A RID: 15002 RVA: 0x00100598 File Offset: 0x000FE798
		public new IndexerDeclarationSyntax WithAccessorList([global::System.Runtime.CompilerServices.Nullable(2)] AccessorListSyntax accessorList)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Type, this.ExplicitInterfaceSpecifier, this.ThisKeyword, this.ParameterList, accessorList, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x06003A9B RID: 15003 RVA: 0x001005DC File Offset: 0x000FE7DC
		public IndexerDeclarationSyntax WithExpressionBody([global::System.Runtime.CompilerServices.Nullable(2)] ArrowExpressionClauseSyntax expressionBody)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Type, this.ExplicitInterfaceSpecifier, this.ThisKeyword, this.ParameterList, this.AccessorList, expressionBody, this.SemicolonToken);
		}

		// Token: 0x06003A9C RID: 15004 RVA: 0x00100620 File Offset: 0x000FE820
		public IndexerDeclarationSyntax WithSemicolonToken(SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Type, this.ExplicitInterfaceSpecifier, this.ThisKeyword, this.ParameterList, this.AccessorList, this.ExpressionBody, semicolonToken);
		}

		// Token: 0x06003A9D RID: 15005 RVA: 0x00100664 File Offset: 0x000FE864
		internal override MemberDeclarationSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x06003A9E RID: 15006 RVA: 0x00100670 File Offset: 0x000FE870
		public new IndexerDeclarationSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x06003A9F RID: 15007 RVA: 0x00100692 File Offset: 0x000FE892
		internal override MemberDeclarationSyntax AddModifiersCore(params SyntaxToken[] items)
		{
			return this.AddModifiers(items);
		}

		// Token: 0x06003AA0 RID: 15008 RVA: 0x0010069C File Offset: 0x000FE89C
		public new IndexerDeclarationSyntax AddModifiers(params SyntaxToken[] items)
		{
			return this.WithModifiers(this.Modifiers.AddRange(items));
		}

		// Token: 0x06003AA1 RID: 15009 RVA: 0x001006C0 File Offset: 0x000FE8C0
		public IndexerDeclarationSyntax AddParameterListParameters(params ParameterSyntax[] items)
		{
			return this.WithParameterList(this.ParameterList.WithParameters(this.ParameterList.Parameters.AddRange(items)));
		}

		// Token: 0x06003AA2 RID: 15010 RVA: 0x001006F2 File Offset: 0x000FE8F2
		internal override BasePropertyDeclarationSyntax AddAccessorListAccessorsCore(params AccessorDeclarationSyntax[] items)
		{
			return this.AddAccessorListAccessors(items);
		}

		// Token: 0x06003AA3 RID: 15011 RVA: 0x001006FC File Offset: 0x000FE8FC
		public new IndexerDeclarationSyntax AddAccessorListAccessors(params AccessorDeclarationSyntax[] items)
		{
			AccessorListSyntax accessorListSyntax;
			if ((accessorListSyntax = this.AccessorList) == null)
			{
				accessorListSyntax = SyntaxFactory.AccessorList(default(SyntaxList<AccessorDeclarationSyntax>));
			}
			AccessorListSyntax accessorListSyntax2 = accessorListSyntax;
			return this.WithAccessorList(accessorListSyntax2.WithAccessors(accessorListSyntax2.Accessors.AddRange(items)));
		}

		// Token: 0x040013DF RID: 5087
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x040013E0 RID: 5088
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax type;

		// Token: 0x040013E1 RID: 5089
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier;

		// Token: 0x040013E2 RID: 5090
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private BracketedParameterListSyntax parameterList;

		// Token: 0x040013E3 RID: 5091
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private AccessorListSyntax accessorList;

		// Token: 0x040013E4 RID: 5092
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ArrowExpressionClauseSyntax expressionBody;
	}
}
