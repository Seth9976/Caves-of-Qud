using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200030D RID: 781
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public abstract class BaseTypeDeclarationSyntax : MemberDeclarationSyntax
	{
		// Token: 0x0600435F RID: 17247 RVA: 0x0011279E File Offset: 0x0011099E
		internal BaseTypeDeclarationSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001161 RID: 4449
		// (get) Token: 0x06004360 RID: 17248
		public abstract SyntaxToken Identifier { get; }

		// Token: 0x06004361 RID: 17249 RVA: 0x001127A9 File Offset: 0x001109A9
		public BaseTypeDeclarationSyntax WithIdentifier(SyntaxToken identifier)
		{
			return this.WithIdentifierCore(identifier);
		}

		// Token: 0x06004362 RID: 17250
		internal abstract BaseTypeDeclarationSyntax WithIdentifierCore(SyntaxToken identifier);

		// Token: 0x17001162 RID: 4450
		// (get) Token: 0x06004363 RID: 17251
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public abstract BaseListSyntax BaseList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x06004364 RID: 17252 RVA: 0x001127B2 File Offset: 0x001109B2
		public BaseTypeDeclarationSyntax WithBaseList([global::System.Runtime.CompilerServices.Nullable(2)] BaseListSyntax baseList)
		{
			return this.WithBaseListCore(baseList);
		}

		// Token: 0x06004365 RID: 17253
		internal abstract BaseTypeDeclarationSyntax WithBaseListCore([global::System.Runtime.CompilerServices.Nullable(2)] BaseListSyntax baseList);

		// Token: 0x06004366 RID: 17254 RVA: 0x001127BB File Offset: 0x001109BB
		public BaseTypeDeclarationSyntax AddBaseListTypes(params BaseTypeSyntax[] items)
		{
			return this.AddBaseListTypesCore(items);
		}

		// Token: 0x06004367 RID: 17255
		internal abstract BaseTypeDeclarationSyntax AddBaseListTypesCore(params BaseTypeSyntax[] items);

		// Token: 0x17001163 RID: 4451
		// (get) Token: 0x06004368 RID: 17256
		public abstract SyntaxToken OpenBraceToken { get; }

		// Token: 0x06004369 RID: 17257 RVA: 0x001127C4 File Offset: 0x001109C4
		public BaseTypeDeclarationSyntax WithOpenBraceToken(SyntaxToken openBraceToken)
		{
			return this.WithOpenBraceTokenCore(openBraceToken);
		}

		// Token: 0x0600436A RID: 17258
		internal abstract BaseTypeDeclarationSyntax WithOpenBraceTokenCore(SyntaxToken openBraceToken);

		// Token: 0x17001164 RID: 4452
		// (get) Token: 0x0600436B RID: 17259
		public abstract SyntaxToken CloseBraceToken { get; }

		// Token: 0x0600436C RID: 17260 RVA: 0x001127CD File Offset: 0x001109CD
		public BaseTypeDeclarationSyntax WithCloseBraceToken(SyntaxToken closeBraceToken)
		{
			return this.WithCloseBraceTokenCore(closeBraceToken);
		}

		// Token: 0x0600436D RID: 17261
		internal abstract BaseTypeDeclarationSyntax WithCloseBraceTokenCore(SyntaxToken closeBraceToken);

		// Token: 0x17001165 RID: 4453
		// (get) Token: 0x0600436E RID: 17262
		public abstract SyntaxToken SemicolonToken { get; }

		// Token: 0x0600436F RID: 17263 RVA: 0x001127D6 File Offset: 0x001109D6
		public BaseTypeDeclarationSyntax WithSemicolonToken(SyntaxToken semicolonToken)
		{
			return this.WithSemicolonTokenCore(semicolonToken);
		}

		// Token: 0x06004370 RID: 17264
		internal abstract BaseTypeDeclarationSyntax WithSemicolonTokenCore(SyntaxToken semicolonToken);

		// Token: 0x06004371 RID: 17265 RVA: 0x001127DF File Offset: 0x001109DF
		public new BaseTypeDeclarationSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return (BaseTypeDeclarationSyntax)this.WithAttributeListsCore(attributeLists);
		}

		// Token: 0x06004372 RID: 17266 RVA: 0x001127ED File Offset: 0x001109ED
		public new BaseTypeDeclarationSyntax WithModifiers(SyntaxTokenList modifiers)
		{
			return (BaseTypeDeclarationSyntax)this.WithModifiersCore(modifiers);
		}

		// Token: 0x06004373 RID: 17267 RVA: 0x001127FB File Offset: 0x001109FB
		public new BaseTypeDeclarationSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return (BaseTypeDeclarationSyntax)this.AddAttributeListsCore(items);
		}

		// Token: 0x06004374 RID: 17268 RVA: 0x00112809 File Offset: 0x00110A09
		public new BaseTypeDeclarationSyntax AddModifiers(params SyntaxToken[] items)
		{
			return (BaseTypeDeclarationSyntax)this.AddModifiersCore(items);
		}
	}
}
