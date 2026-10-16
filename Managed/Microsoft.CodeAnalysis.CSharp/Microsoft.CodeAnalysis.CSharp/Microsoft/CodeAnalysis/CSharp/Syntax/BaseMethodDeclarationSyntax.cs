using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000248 RID: 584
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public abstract class BaseMethodDeclarationSyntax : MemberDeclarationSyntax
	{
		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x060037ED RID: 14317
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public abstract override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x060037EE RID: 14318
		public abstract override SyntaxTokenList Modifiers { get; }

		// Token: 0x060037EF RID: 14319 RVA: 0x000FA0F2 File Offset: 0x000F82F2
		internal BaseMethodDeclarationSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x060037F0 RID: 14320
		public abstract ParameterListSyntax ParameterList { get; }

		// Token: 0x060037F1 RID: 14321 RVA: 0x000FA0FD File Offset: 0x000F82FD
		public BaseMethodDeclarationSyntax WithParameterList(ParameterListSyntax parameterList)
		{
			return this.WithParameterListCore(parameterList);
		}

		// Token: 0x060037F2 RID: 14322
		internal abstract BaseMethodDeclarationSyntax WithParameterListCore(ParameterListSyntax parameterList);

		// Token: 0x060037F3 RID: 14323 RVA: 0x000FA106 File Offset: 0x000F8306
		public BaseMethodDeclarationSyntax AddParameterListParameters(params ParameterSyntax[] items)
		{
			return this.AddParameterListParametersCore(items);
		}

		// Token: 0x060037F4 RID: 14324
		internal abstract BaseMethodDeclarationSyntax AddParameterListParametersCore(params ParameterSyntax[] items);

		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x060037F5 RID: 14325
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public abstract BlockSyntax Body
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x060037F6 RID: 14326 RVA: 0x000FA10F File Offset: 0x000F830F
		public BaseMethodDeclarationSyntax WithBody([global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax body)
		{
			return this.WithBodyCore(body);
		}

		// Token: 0x060037F7 RID: 14327
		internal abstract BaseMethodDeclarationSyntax WithBodyCore([global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax body);

		// Token: 0x060037F8 RID: 14328 RVA: 0x000FA118 File Offset: 0x000F8318
		public BaseMethodDeclarationSyntax AddBodyAttributeLists(params AttributeListSyntax[] items)
		{
			return this.AddBodyAttributeListsCore(items);
		}

		// Token: 0x060037F9 RID: 14329
		internal abstract BaseMethodDeclarationSyntax AddBodyAttributeListsCore(params AttributeListSyntax[] items);

		// Token: 0x060037FA RID: 14330 RVA: 0x000FA121 File Offset: 0x000F8321
		public BaseMethodDeclarationSyntax AddBodyStatements(params StatementSyntax[] items)
		{
			return this.AddBodyStatementsCore(items);
		}

		// Token: 0x060037FB RID: 14331
		internal abstract BaseMethodDeclarationSyntax AddBodyStatementsCore(params StatementSyntax[] items);

		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x060037FC RID: 14332
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public abstract ArrowExpressionClauseSyntax ExpressionBody
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x060037FD RID: 14333 RVA: 0x000FA12A File Offset: 0x000F832A
		public BaseMethodDeclarationSyntax WithExpressionBody([global::System.Runtime.CompilerServices.Nullable(2)] ArrowExpressionClauseSyntax expressionBody)
		{
			return this.WithExpressionBodyCore(expressionBody);
		}

		// Token: 0x060037FE RID: 14334
		internal abstract BaseMethodDeclarationSyntax WithExpressionBodyCore([global::System.Runtime.CompilerServices.Nullable(2)] ArrowExpressionClauseSyntax expressionBody);

		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x060037FF RID: 14335
		public abstract SyntaxToken SemicolonToken { get; }

		// Token: 0x06003800 RID: 14336 RVA: 0x000FA133 File Offset: 0x000F8333
		public BaseMethodDeclarationSyntax WithSemicolonToken(SyntaxToken semicolonToken)
		{
			return this.WithSemicolonTokenCore(semicolonToken);
		}

		// Token: 0x06003801 RID: 14337
		internal abstract BaseMethodDeclarationSyntax WithSemicolonTokenCore(SyntaxToken semicolonToken);

		// Token: 0x06003802 RID: 14338 RVA: 0x000FA13C File Offset: 0x000F833C
		public new BaseMethodDeclarationSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return (BaseMethodDeclarationSyntax)this.WithAttributeListsCore(attributeLists);
		}

		// Token: 0x06003803 RID: 14339 RVA: 0x000FA14A File Offset: 0x000F834A
		public new BaseMethodDeclarationSyntax WithModifiers(SyntaxTokenList modifiers)
		{
			return (BaseMethodDeclarationSyntax)this.WithModifiersCore(modifiers);
		}

		// Token: 0x06003804 RID: 14340 RVA: 0x000FA158 File Offset: 0x000F8358
		public new BaseMethodDeclarationSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return (BaseMethodDeclarationSyntax)this.AddAttributeListsCore(items);
		}

		// Token: 0x06003805 RID: 14341 RVA: 0x000FA166 File Offset: 0x000F8366
		public new BaseMethodDeclarationSyntax AddModifiers(params SyntaxToken[] items)
		{
			return (BaseMethodDeclarationSyntax)this.AddModifiersCore(items);
		}
	}
}
