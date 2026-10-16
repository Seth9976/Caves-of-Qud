using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000249 RID: 585
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public abstract class BasePropertyDeclarationSyntax : MemberDeclarationSyntax
	{
		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x06003806 RID: 14342
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public abstract override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x06003807 RID: 14343
		public abstract override SyntaxTokenList Modifiers { get; }

		// Token: 0x06003808 RID: 14344 RVA: 0x000FA174 File Offset: 0x000F8374
		internal BasePropertyDeclarationSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x06003809 RID: 14345
		public abstract TypeSyntax Type { get; }

		// Token: 0x0600380A RID: 14346 RVA: 0x000FA17F File Offset: 0x000F837F
		public BasePropertyDeclarationSyntax WithType(TypeSyntax type)
		{
			return this.WithTypeCore(type);
		}

		// Token: 0x0600380B RID: 14347
		internal abstract BasePropertyDeclarationSyntax WithTypeCore(TypeSyntax type);

		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x0600380C RID: 14348
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public abstract ExplicitInterfaceSpecifierSyntax ExplicitInterfaceSpecifier
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x0600380D RID: 14349 RVA: 0x000FA188 File Offset: 0x000F8388
		public BasePropertyDeclarationSyntax WithExplicitInterfaceSpecifier([global::System.Runtime.CompilerServices.Nullable(2)] ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier)
		{
			return this.WithExplicitInterfaceSpecifierCore(explicitInterfaceSpecifier);
		}

		// Token: 0x0600380E RID: 14350
		internal abstract BasePropertyDeclarationSyntax WithExplicitInterfaceSpecifierCore([global::System.Runtime.CompilerServices.Nullable(2)] ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier);

		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x0600380F RID: 14351
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public abstract AccessorListSyntax AccessorList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x06003810 RID: 14352 RVA: 0x000FA191 File Offset: 0x000F8391
		public BasePropertyDeclarationSyntax WithAccessorList([global::System.Runtime.CompilerServices.Nullable(2)] AccessorListSyntax accessorList)
		{
			return this.WithAccessorListCore(accessorList);
		}

		// Token: 0x06003811 RID: 14353
		internal abstract BasePropertyDeclarationSyntax WithAccessorListCore([global::System.Runtime.CompilerServices.Nullable(2)] AccessorListSyntax accessorList);

		// Token: 0x06003812 RID: 14354 RVA: 0x000FA19A File Offset: 0x000F839A
		public BasePropertyDeclarationSyntax AddAccessorListAccessors(params AccessorDeclarationSyntax[] items)
		{
			return this.AddAccessorListAccessorsCore(items);
		}

		// Token: 0x06003813 RID: 14355
		internal abstract BasePropertyDeclarationSyntax AddAccessorListAccessorsCore(params AccessorDeclarationSyntax[] items);

		// Token: 0x06003814 RID: 14356 RVA: 0x000FA1A3 File Offset: 0x000F83A3
		public new BasePropertyDeclarationSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return (BasePropertyDeclarationSyntax)this.WithAttributeListsCore(attributeLists);
		}

		// Token: 0x06003815 RID: 14357 RVA: 0x000FA1B1 File Offset: 0x000F83B1
		public new BasePropertyDeclarationSyntax WithModifiers(SyntaxTokenList modifiers)
		{
			return (BasePropertyDeclarationSyntax)this.WithModifiersCore(modifiers);
		}

		// Token: 0x06003816 RID: 14358 RVA: 0x000FA1BF File Offset: 0x000F83BF
		public new BasePropertyDeclarationSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return (BasePropertyDeclarationSyntax)this.AddAttributeListsCore(items);
		}

		// Token: 0x06003817 RID: 14359 RVA: 0x000FA1CD File Offset: 0x000F83CD
		public new BasePropertyDeclarationSyntax AddModifiers(params SyntaxToken[] items)
		{
			return (BasePropertyDeclarationSyntax)this.AddModifiersCore(items);
		}
	}
}
