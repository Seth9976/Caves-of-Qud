using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000275 RID: 629
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class PropertyDeclarationSyntax : BasePropertyDeclarationSyntax
	{
		// Token: 0x17000FAF RID: 4015
		// (get) Token: 0x06003BD1 RID: 15313 RVA: 0x00103B91 File Offset: 0x00101D91
		[EditorBrowsable(1)]
		[Obsolete("This member is obsolete.", true)]
		public SyntaxToken Semicolon
		{
			get
			{
				return this.SemicolonToken;
			}
		}

		// Token: 0x06003BD2 RID: 15314 RVA: 0x00103B99 File Offset: 0x00101D99
		[EditorBrowsable(1)]
		[Obsolete("This member is obsolete.", true)]
		public PropertyDeclarationSyntax WithSemicolon(SyntaxToken semicolon)
		{
			return this.WithSemicolonToken(semicolon);
		}

		// Token: 0x06003BD3 RID: 15315 RVA: 0x00103BA2 File Offset: 0x00101DA2
		internal PropertyDeclarationSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000FB0 RID: 4016
		// (get) Token: 0x06003BD4 RID: 15316 RVA: 0x00103BAD File Offset: 0x00101DAD
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17000FB1 RID: 4017
		// (get) Token: 0x06003BD5 RID: 15317 RVA: 0x00103BC4 File Offset: 0x00101DC4
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

		// Token: 0x17000FB2 RID: 4018
		// (get) Token: 0x06003BD6 RID: 15318 RVA: 0x00103C00 File Offset: 0x00101E00
		public override TypeSyntax Type
		{
			get
			{
				return base.GetRed<TypeSyntax>(ref this.type, 2);
			}
		}

		// Token: 0x17000FB3 RID: 4019
		// (get) Token: 0x06003BD7 RID: 15319 RVA: 0x00103C0F File Offset: 0x00101E0F
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override ExplicitInterfaceSpecifierSyntax ExplicitInterfaceSpecifier
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<ExplicitInterfaceSpecifierSyntax>(ref this.explicitInterfaceSpecifier, 3);
			}
		}

		// Token: 0x17000FB4 RID: 4020
		// (get) Token: 0x06003BD8 RID: 15320 RVA: 0x00103C1E File Offset: 0x00101E1E
		public SyntaxToken Identifier
		{
			get
			{
				return new SyntaxToken(this, ((PropertyDeclarationSyntax)base.Green).identifier, this.GetChildPosition(4), base.GetChildIndex(4));
			}
		}

		// Token: 0x17000FB5 RID: 4021
		// (get) Token: 0x06003BD9 RID: 15321 RVA: 0x00103C44 File Offset: 0x00101E44
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override AccessorListSyntax AccessorList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<AccessorListSyntax>(ref this.accessorList, 5);
			}
		}

		// Token: 0x17000FB6 RID: 4022
		// (get) Token: 0x06003BDA RID: 15322 RVA: 0x00103C53 File Offset: 0x00101E53
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public ArrowExpressionClauseSyntax ExpressionBody
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<ArrowExpressionClauseSyntax>(ref this.expressionBody, 6);
			}
		}

		// Token: 0x17000FB7 RID: 4023
		// (get) Token: 0x06003BDB RID: 15323 RVA: 0x00103C62 File Offset: 0x00101E62
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public EqualsValueClauseSyntax Initializer
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<EqualsValueClauseSyntax>(ref this.initializer, 7);
			}
		}

		// Token: 0x17000FB8 RID: 4024
		// (get) Token: 0x06003BDC RID: 15324 RVA: 0x00103C74 File Offset: 0x00101E74
		public SyntaxToken SemicolonToken
		{
			get
			{
				SyntaxToken semicolonToken = ((PropertyDeclarationSyntax)base.Green).semicolonToken;
				if (semicolonToken == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, semicolonToken, this.GetChildPosition(8), base.GetChildIndex(8));
			}
		}

		// Token: 0x06003BDD RID: 15325 RVA: 0x00103CB4 File Offset: 0x00101EB4
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
				return base.GetRed<AccessorListSyntax>(ref this.accessorList, 5);
			case 6:
				return base.GetRed<ArrowExpressionClauseSyntax>(ref this.expressionBody, 6);
			case 7:
				return base.GetRed<EqualsValueClauseSyntax>(ref this.initializer, 7);
			}
			return null;
		}

		// Token: 0x06003BDE RID: 15326 RVA: 0x00103D4C File Offset: 0x00101F4C
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
				return this.accessorList;
			case 6:
				return this.expressionBody;
			case 7:
				return this.initializer;
			}
			return null;
		}

		// Token: 0x06003BDF RID: 15327 RVA: 0x00103DBA File Offset: 0x00101FBA
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitPropertyDeclaration(this);
		}

		// Token: 0x06003BE0 RID: 15328 RVA: 0x00103DC3 File Offset: 0x00101FC3
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitPropertyDeclaration(this);
		}

		// Token: 0x06003BE1 RID: 15329 RVA: 0x00103DCC File Offset: 0x00101FCC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public PropertyDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, AccessorListSyntax accessorList, ArrowExpressionClauseSyntax expressionBody, EqualsValueClauseSyntax initializer, SyntaxToken semicolonToken)
		{
			if (!(attributeLists != this.AttributeLists) && !(modifiers != this.Modifiers) && type == this.Type && explicitInterfaceSpecifier == this.ExplicitInterfaceSpecifier && !(identifier != this.Identifier) && accessorList == this.AccessorList && expressionBody == this.ExpressionBody && initializer == this.Initializer && !(semicolonToken != this.SemicolonToken))
			{
				return this;
			}
			PropertyDeclarationSyntax propertyDeclarationSyntax = SyntaxFactory.PropertyDeclaration(attributeLists, modifiers, type, explicitInterfaceSpecifier, identifier, accessorList, expressionBody, initializer, semicolonToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return propertyDeclarationSyntax;
			}
			return propertyDeclarationSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003BE2 RID: 15330 RVA: 0x00103E72 File Offset: 0x00102072
		internal override MemberDeclarationSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x06003BE3 RID: 15331 RVA: 0x00103E7C File Offset: 0x0010207C
		public new PropertyDeclarationSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.Modifiers, this.Type, this.ExplicitInterfaceSpecifier, this.Identifier, this.AccessorList, this.ExpressionBody, this.Initializer, this.SemicolonToken);
		}

		// Token: 0x06003BE4 RID: 15332 RVA: 0x00103EC0 File Offset: 0x001020C0
		internal override MemberDeclarationSyntax WithModifiersCore(SyntaxTokenList modifiers)
		{
			return this.WithModifiers(modifiers);
		}

		// Token: 0x06003BE5 RID: 15333 RVA: 0x00103ECC File Offset: 0x001020CC
		public new PropertyDeclarationSyntax WithModifiers(SyntaxTokenList modifiers)
		{
			return this.Update(this.AttributeLists, modifiers, this.Type, this.ExplicitInterfaceSpecifier, this.Identifier, this.AccessorList, this.ExpressionBody, this.Initializer, this.SemicolonToken);
		}

		// Token: 0x06003BE6 RID: 15334 RVA: 0x00103F10 File Offset: 0x00102110
		internal override BasePropertyDeclarationSyntax WithTypeCore(TypeSyntax type)
		{
			return this.WithType(type);
		}

		// Token: 0x06003BE7 RID: 15335 RVA: 0x00103F1C File Offset: 0x0010211C
		public new PropertyDeclarationSyntax WithType(TypeSyntax type)
		{
			return this.Update(this.AttributeLists, this.Modifiers, type, this.ExplicitInterfaceSpecifier, this.Identifier, this.AccessorList, this.ExpressionBody, this.Initializer, this.SemicolonToken);
		}

		// Token: 0x06003BE8 RID: 15336 RVA: 0x00103F60 File Offset: 0x00102160
		internal override BasePropertyDeclarationSyntax WithExplicitInterfaceSpecifierCore([global::System.Runtime.CompilerServices.Nullable(2)] ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier)
		{
			return this.WithExplicitInterfaceSpecifier(explicitInterfaceSpecifier);
		}

		// Token: 0x06003BE9 RID: 15337 RVA: 0x00103F6C File Offset: 0x0010216C
		public new PropertyDeclarationSyntax WithExplicitInterfaceSpecifier([global::System.Runtime.CompilerServices.Nullable(2)] ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Type, explicitInterfaceSpecifier, this.Identifier, this.AccessorList, this.ExpressionBody, this.Initializer, this.SemicolonToken);
		}

		// Token: 0x06003BEA RID: 15338 RVA: 0x00103FB0 File Offset: 0x001021B0
		public PropertyDeclarationSyntax WithIdentifier(SyntaxToken identifier)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Type, this.ExplicitInterfaceSpecifier, identifier, this.AccessorList, this.ExpressionBody, this.Initializer, this.SemicolonToken);
		}

		// Token: 0x06003BEB RID: 15339 RVA: 0x00103FF4 File Offset: 0x001021F4
		internal override BasePropertyDeclarationSyntax WithAccessorListCore([global::System.Runtime.CompilerServices.Nullable(2)] AccessorListSyntax accessorList)
		{
			return this.WithAccessorList(accessorList);
		}

		// Token: 0x06003BEC RID: 15340 RVA: 0x00104000 File Offset: 0x00102200
		public new PropertyDeclarationSyntax WithAccessorList([global::System.Runtime.CompilerServices.Nullable(2)] AccessorListSyntax accessorList)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Type, this.ExplicitInterfaceSpecifier, this.Identifier, accessorList, this.ExpressionBody, this.Initializer, this.SemicolonToken);
		}

		// Token: 0x06003BED RID: 15341 RVA: 0x00104044 File Offset: 0x00102244
		public PropertyDeclarationSyntax WithExpressionBody([global::System.Runtime.CompilerServices.Nullable(2)] ArrowExpressionClauseSyntax expressionBody)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Type, this.ExplicitInterfaceSpecifier, this.Identifier, this.AccessorList, expressionBody, this.Initializer, this.SemicolonToken);
		}

		// Token: 0x06003BEE RID: 15342 RVA: 0x00104088 File Offset: 0x00102288
		public PropertyDeclarationSyntax WithInitializer([global::System.Runtime.CompilerServices.Nullable(2)] EqualsValueClauseSyntax initializer)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Type, this.ExplicitInterfaceSpecifier, this.Identifier, this.AccessorList, this.ExpressionBody, initializer, this.SemicolonToken);
		}

		// Token: 0x06003BEF RID: 15343 RVA: 0x001040CC File Offset: 0x001022CC
		public PropertyDeclarationSyntax WithSemicolonToken(SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Type, this.ExplicitInterfaceSpecifier, this.Identifier, this.AccessorList, this.ExpressionBody, this.Initializer, semicolonToken);
		}

		// Token: 0x06003BF0 RID: 15344 RVA: 0x00104110 File Offset: 0x00102310
		internal override MemberDeclarationSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x06003BF1 RID: 15345 RVA: 0x0010411C File Offset: 0x0010231C
		public new PropertyDeclarationSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x06003BF2 RID: 15346 RVA: 0x0010413E File Offset: 0x0010233E
		internal override MemberDeclarationSyntax AddModifiersCore(params SyntaxToken[] items)
		{
			return this.AddModifiers(items);
		}

		// Token: 0x06003BF3 RID: 15347 RVA: 0x00104148 File Offset: 0x00102348
		public new PropertyDeclarationSyntax AddModifiers(params SyntaxToken[] items)
		{
			return this.WithModifiers(this.Modifiers.AddRange(items));
		}

		// Token: 0x06003BF4 RID: 15348 RVA: 0x0010416A File Offset: 0x0010236A
		internal override BasePropertyDeclarationSyntax AddAccessorListAccessorsCore(params AccessorDeclarationSyntax[] items)
		{
			return this.AddAccessorListAccessors(items);
		}

		// Token: 0x06003BF5 RID: 15349 RVA: 0x00104174 File Offset: 0x00102374
		public new PropertyDeclarationSyntax AddAccessorListAccessors(params AccessorDeclarationSyntax[] items)
		{
			AccessorListSyntax accessorListSyntax;
			if ((accessorListSyntax = this.AccessorList) == null)
			{
				accessorListSyntax = SyntaxFactory.AccessorList(default(SyntaxList<AccessorDeclarationSyntax>));
			}
			AccessorListSyntax accessorListSyntax2 = accessorListSyntax;
			return this.WithAccessorList(accessorListSyntax2.WithAccessors(accessorListSyntax2.Accessors.AddRange(items)));
		}

		// Token: 0x0400140A RID: 5130
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x0400140B RID: 5131
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax type;

		// Token: 0x0400140C RID: 5132
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier;

		// Token: 0x0400140D RID: 5133
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private AccessorListSyntax accessorList;

		// Token: 0x0400140E RID: 5134
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ArrowExpressionClauseSyntax expressionBody;

		// Token: 0x0400140F RID: 5135
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private EqualsValueClauseSyntax initializer;
	}
}
