using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000247 RID: 583
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public abstract class BaseFieldDeclarationSyntax : MemberDeclarationSyntax
	{
		// Token: 0x17000EC7 RID: 3783
		// (get) Token: 0x060037DE RID: 14302
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public abstract override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x060037DF RID: 14303
		public abstract override SyntaxTokenList Modifiers { get; }

		// Token: 0x060037E0 RID: 14304 RVA: 0x000FA094 File Offset: 0x000F8294
		internal BaseFieldDeclarationSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x060037E1 RID: 14305
		public abstract VariableDeclarationSyntax Declaration { get; }

		// Token: 0x060037E2 RID: 14306 RVA: 0x000FA09F File Offset: 0x000F829F
		public BaseFieldDeclarationSyntax WithDeclaration(VariableDeclarationSyntax declaration)
		{
			return this.WithDeclarationCore(declaration);
		}

		// Token: 0x060037E3 RID: 14307
		internal abstract BaseFieldDeclarationSyntax WithDeclarationCore(VariableDeclarationSyntax declaration);

		// Token: 0x060037E4 RID: 14308 RVA: 0x000FA0A8 File Offset: 0x000F82A8
		public BaseFieldDeclarationSyntax AddDeclarationVariables(params VariableDeclaratorSyntax[] items)
		{
			return this.AddDeclarationVariablesCore(items);
		}

		// Token: 0x060037E5 RID: 14309
		internal abstract BaseFieldDeclarationSyntax AddDeclarationVariablesCore(params VariableDeclaratorSyntax[] items);

		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x060037E6 RID: 14310
		public abstract SyntaxToken SemicolonToken { get; }

		// Token: 0x060037E7 RID: 14311 RVA: 0x000FA0B1 File Offset: 0x000F82B1
		public BaseFieldDeclarationSyntax WithSemicolonToken(SyntaxToken semicolonToken)
		{
			return this.WithSemicolonTokenCore(semicolonToken);
		}

		// Token: 0x060037E8 RID: 14312
		internal abstract BaseFieldDeclarationSyntax WithSemicolonTokenCore(SyntaxToken semicolonToken);

		// Token: 0x060037E9 RID: 14313 RVA: 0x000FA0BA File Offset: 0x000F82BA
		public new BaseFieldDeclarationSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return (BaseFieldDeclarationSyntax)this.WithAttributeListsCore(attributeLists);
		}

		// Token: 0x060037EA RID: 14314 RVA: 0x000FA0C8 File Offset: 0x000F82C8
		public new BaseFieldDeclarationSyntax WithModifiers(SyntaxTokenList modifiers)
		{
			return (BaseFieldDeclarationSyntax)this.WithModifiersCore(modifiers);
		}

		// Token: 0x060037EB RID: 14315 RVA: 0x000FA0D6 File Offset: 0x000F82D6
		public new BaseFieldDeclarationSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return (BaseFieldDeclarationSyntax)this.AddAttributeListsCore(items);
		}

		// Token: 0x060037EC RID: 14316 RVA: 0x000FA0E4 File Offset: 0x000F82E4
		public new BaseFieldDeclarationSyntax AddModifiers(params SyntaxToken[] items)
		{
			return (BaseFieldDeclarationSyntax)this.AddModifiersCore(items);
		}
	}
}
