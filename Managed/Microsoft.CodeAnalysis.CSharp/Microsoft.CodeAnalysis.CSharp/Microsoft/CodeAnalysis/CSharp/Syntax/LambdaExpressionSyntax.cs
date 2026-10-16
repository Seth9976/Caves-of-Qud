using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000269 RID: 617
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public abstract class LambdaExpressionSyntax : AnonymousFunctionExpressionSyntax
	{
		// Token: 0x06003AB6 RID: 15030 RVA: 0x00100948 File Offset: 0x000FEB48
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public new LambdaExpressionSyntax WithBody(CSharpSyntaxNode body)
		{
			BlockSyntax blockSyntax = body as BlockSyntax;
			if (blockSyntax == null)
			{
				return this.WithExpressionBody((ExpressionSyntax)body).WithBlock(null);
			}
			return this.WithBlock(blockSyntax).WithExpressionBody(null);
		}

		// Token: 0x06003AB7 RID: 15031 RVA: 0x0010097F File Offset: 0x000FEB7F
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public new LambdaExpressionSyntax WithAsyncKeyword(SyntaxToken asyncKeyword)
		{
			return (LambdaExpressionSyntax)this.WithAsyncKeywordCore(asyncKeyword);
		}

		// Token: 0x06003AB8 RID: 15032 RVA: 0x0010098D File Offset: 0x000FEB8D
		internal LambdaExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x06003AB9 RID: 15033
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public abstract SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06003ABA RID: 15034 RVA: 0x00100998 File Offset: 0x000FEB98
		public LambdaExpressionSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeListsCore(attributeLists);
		}

		// Token: 0x06003ABB RID: 15035
		internal abstract LambdaExpressionSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists);

		// Token: 0x06003ABC RID: 15036 RVA: 0x001009A1 File Offset: 0x000FEBA1
		public LambdaExpressionSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.AddAttributeListsCore(items);
		}

		// Token: 0x06003ABD RID: 15037
		internal abstract LambdaExpressionSyntax AddAttributeListsCore(params AttributeListSyntax[] items);

		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x06003ABE RID: 15038
		public abstract SyntaxToken ArrowToken { get; }

		// Token: 0x06003ABF RID: 15039 RVA: 0x001009AA File Offset: 0x000FEBAA
		public LambdaExpressionSyntax WithArrowToken(SyntaxToken arrowToken)
		{
			return this.WithArrowTokenCore(arrowToken);
		}

		// Token: 0x06003AC0 RID: 15040
		internal abstract LambdaExpressionSyntax WithArrowTokenCore(SyntaxToken arrowToken);

		// Token: 0x06003AC1 RID: 15041 RVA: 0x001009B3 File Offset: 0x000FEBB3
		public new LambdaExpressionSyntax WithModifiers(SyntaxTokenList modifiers)
		{
			return (LambdaExpressionSyntax)this.WithModifiersCore(modifiers);
		}

		// Token: 0x06003AC2 RID: 15042 RVA: 0x001009C1 File Offset: 0x000FEBC1
		public new LambdaExpressionSyntax WithBlock([global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax block)
		{
			return (LambdaExpressionSyntax)this.WithBlockCore(block);
		}

		// Token: 0x06003AC3 RID: 15043 RVA: 0x001009CF File Offset: 0x000FEBCF
		public new LambdaExpressionSyntax WithExpressionBody([global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expressionBody)
		{
			return (LambdaExpressionSyntax)this.WithExpressionBodyCore(expressionBody);
		}

		// Token: 0x06003AC4 RID: 15044 RVA: 0x001009DD File Offset: 0x000FEBDD
		public new LambdaExpressionSyntax AddModifiers(params SyntaxToken[] items)
		{
			return (LambdaExpressionSyntax)this.AddModifiersCore(items);
		}

		// Token: 0x06003AC5 RID: 15045 RVA: 0x001009EB File Offset: 0x000FEBEB
		public new AnonymousFunctionExpressionSyntax AddBlockAttributeLists(params AttributeListSyntax[] items)
		{
			return this.AddBlockAttributeListsCore(items);
		}

		// Token: 0x06003AC6 RID: 15046 RVA: 0x001009F4 File Offset: 0x000FEBF4
		public new AnonymousFunctionExpressionSyntax AddBlockStatements(params StatementSyntax[] items)
		{
			return this.AddBlockStatementsCore(items);
		}
	}
}
