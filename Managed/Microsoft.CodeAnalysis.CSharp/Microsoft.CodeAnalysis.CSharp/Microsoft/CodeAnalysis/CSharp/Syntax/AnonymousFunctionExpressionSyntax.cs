using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000241 RID: 577
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public abstract class AnonymousFunctionExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x06003775 RID: 14197 RVA: 0x000F962A File Offset: 0x000F782A
		public CSharpSyntaxNode Body
		{
			get
			{
				return this.Block ?? this.ExpressionBody;
			}
		}

		// Token: 0x06003776 RID: 14198 RVA: 0x000F963C File Offset: 0x000F783C
		public AnonymousFunctionExpressionSyntax WithBody(CSharpSyntaxNode body)
		{
			BlockSyntax blockSyntax = body as BlockSyntax;
			if (blockSyntax == null)
			{
				return this.WithExpressionBody((ExpressionSyntax)body).WithBlock(null);
			}
			return this.WithBlock(blockSyntax).WithExpressionBody(null);
		}

		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x06003777 RID: 14199
		public abstract SyntaxToken AsyncKeyword { get; }

		// Token: 0x06003778 RID: 14200 RVA: 0x000F9673 File Offset: 0x000F7873
		public AnonymousFunctionExpressionSyntax WithAsyncKeyword(SyntaxToken asyncKeyword)
		{
			return this.WithAsyncKeywordCore(asyncKeyword);
		}

		// Token: 0x06003779 RID: 14201
		internal abstract AnonymousFunctionExpressionSyntax WithAsyncKeywordCore(SyntaxToken asyncKeyword);

		// Token: 0x0600377A RID: 14202 RVA: 0x000F967C File Offset: 0x000F787C
		internal AnonymousFunctionExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x0600377B RID: 14203
		public abstract SyntaxTokenList Modifiers { get; }

		// Token: 0x0600377C RID: 14204 RVA: 0x000F9687 File Offset: 0x000F7887
		public AnonymousFunctionExpressionSyntax WithModifiers(SyntaxTokenList modifiers)
		{
			return this.WithModifiersCore(modifiers);
		}

		// Token: 0x0600377D RID: 14205
		internal abstract AnonymousFunctionExpressionSyntax WithModifiersCore(SyntaxTokenList modifiers);

		// Token: 0x0600377E RID: 14206 RVA: 0x000F9690 File Offset: 0x000F7890
		public AnonymousFunctionExpressionSyntax AddModifiers(params SyntaxToken[] items)
		{
			return this.AddModifiersCore(items);
		}

		// Token: 0x0600377F RID: 14207
		internal abstract AnonymousFunctionExpressionSyntax AddModifiersCore(params SyntaxToken[] items);

		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x06003780 RID: 14208
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public abstract BlockSyntax Block
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x06003781 RID: 14209 RVA: 0x000F9699 File Offset: 0x000F7899
		public AnonymousFunctionExpressionSyntax WithBlock([global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax block)
		{
			return this.WithBlockCore(block);
		}

		// Token: 0x06003782 RID: 14210
		internal abstract AnonymousFunctionExpressionSyntax WithBlockCore([global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax block);

		// Token: 0x06003783 RID: 14211 RVA: 0x000F96A2 File Offset: 0x000F78A2
		public AnonymousFunctionExpressionSyntax AddBlockAttributeLists(params AttributeListSyntax[] items)
		{
			return this.AddBlockAttributeListsCore(items);
		}

		// Token: 0x06003784 RID: 14212
		internal abstract AnonymousFunctionExpressionSyntax AddBlockAttributeListsCore(params AttributeListSyntax[] items);

		// Token: 0x06003785 RID: 14213 RVA: 0x000F96AB File Offset: 0x000F78AB
		public AnonymousFunctionExpressionSyntax AddBlockStatements(params StatementSyntax[] items)
		{
			return this.AddBlockStatementsCore(items);
		}

		// Token: 0x06003786 RID: 14214
		internal abstract AnonymousFunctionExpressionSyntax AddBlockStatementsCore(params StatementSyntax[] items);

		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x06003787 RID: 14215
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public abstract ExpressionSyntax ExpressionBody
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x06003788 RID: 14216 RVA: 0x000F96B4 File Offset: 0x000F78B4
		public AnonymousFunctionExpressionSyntax WithExpressionBody([global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expressionBody)
		{
			return this.WithExpressionBodyCore(expressionBody);
		}

		// Token: 0x06003789 RID: 14217
		internal abstract AnonymousFunctionExpressionSyntax WithExpressionBodyCore([global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expressionBody);
	}
}
