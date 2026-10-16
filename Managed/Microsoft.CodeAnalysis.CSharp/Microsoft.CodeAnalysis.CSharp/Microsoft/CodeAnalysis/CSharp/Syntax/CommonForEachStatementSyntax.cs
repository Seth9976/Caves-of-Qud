using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002F8 RID: 760
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public abstract class CommonForEachStatementSyntax : StatementSyntax
	{
		// Token: 0x06004244 RID: 16964 RVA: 0x00110BB2 File Offset: 0x0010EDB2
		internal CommonForEachStatementSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700111C RID: 4380
		// (get) Token: 0x06004245 RID: 16965
		public abstract SyntaxToken AwaitKeyword { get; }

		// Token: 0x06004246 RID: 16966 RVA: 0x00110BBD File Offset: 0x0010EDBD
		public CommonForEachStatementSyntax WithAwaitKeyword(SyntaxToken awaitKeyword)
		{
			return this.WithAwaitKeywordCore(awaitKeyword);
		}

		// Token: 0x06004247 RID: 16967
		internal abstract CommonForEachStatementSyntax WithAwaitKeywordCore(SyntaxToken awaitKeyword);

		// Token: 0x1700111D RID: 4381
		// (get) Token: 0x06004248 RID: 16968
		public abstract SyntaxToken ForEachKeyword { get; }

		// Token: 0x06004249 RID: 16969 RVA: 0x00110BC6 File Offset: 0x0010EDC6
		public CommonForEachStatementSyntax WithForEachKeyword(SyntaxToken forEachKeyword)
		{
			return this.WithForEachKeywordCore(forEachKeyword);
		}

		// Token: 0x0600424A RID: 16970
		internal abstract CommonForEachStatementSyntax WithForEachKeywordCore(SyntaxToken forEachKeyword);

		// Token: 0x1700111E RID: 4382
		// (get) Token: 0x0600424B RID: 16971
		public abstract SyntaxToken OpenParenToken { get; }

		// Token: 0x0600424C RID: 16972 RVA: 0x00110BCF File Offset: 0x0010EDCF
		public CommonForEachStatementSyntax WithOpenParenToken(SyntaxToken openParenToken)
		{
			return this.WithOpenParenTokenCore(openParenToken);
		}

		// Token: 0x0600424D RID: 16973
		internal abstract CommonForEachStatementSyntax WithOpenParenTokenCore(SyntaxToken openParenToken);

		// Token: 0x1700111F RID: 4383
		// (get) Token: 0x0600424E RID: 16974
		public abstract SyntaxToken InKeyword { get; }

		// Token: 0x0600424F RID: 16975 RVA: 0x00110BD8 File Offset: 0x0010EDD8
		public CommonForEachStatementSyntax WithInKeyword(SyntaxToken inKeyword)
		{
			return this.WithInKeywordCore(inKeyword);
		}

		// Token: 0x06004250 RID: 16976
		internal abstract CommonForEachStatementSyntax WithInKeywordCore(SyntaxToken inKeyword);

		// Token: 0x17001120 RID: 4384
		// (get) Token: 0x06004251 RID: 16977
		public abstract ExpressionSyntax Expression { get; }

		// Token: 0x06004252 RID: 16978 RVA: 0x00110BE1 File Offset: 0x0010EDE1
		public CommonForEachStatementSyntax WithExpression(ExpressionSyntax expression)
		{
			return this.WithExpressionCore(expression);
		}

		// Token: 0x06004253 RID: 16979
		internal abstract CommonForEachStatementSyntax WithExpressionCore(ExpressionSyntax expression);

		// Token: 0x17001121 RID: 4385
		// (get) Token: 0x06004254 RID: 16980
		public abstract SyntaxToken CloseParenToken { get; }

		// Token: 0x06004255 RID: 16981 RVA: 0x00110BEA File Offset: 0x0010EDEA
		public CommonForEachStatementSyntax WithCloseParenToken(SyntaxToken closeParenToken)
		{
			return this.WithCloseParenTokenCore(closeParenToken);
		}

		// Token: 0x06004256 RID: 16982
		internal abstract CommonForEachStatementSyntax WithCloseParenTokenCore(SyntaxToken closeParenToken);

		// Token: 0x17001122 RID: 4386
		// (get) Token: 0x06004257 RID: 16983
		public abstract StatementSyntax Statement { get; }

		// Token: 0x06004258 RID: 16984 RVA: 0x00110BF3 File Offset: 0x0010EDF3
		public CommonForEachStatementSyntax WithStatement(StatementSyntax statement)
		{
			return this.WithStatementCore(statement);
		}

		// Token: 0x06004259 RID: 16985
		internal abstract CommonForEachStatementSyntax WithStatementCore(StatementSyntax statement);

		// Token: 0x0600425A RID: 16986 RVA: 0x00110BFC File Offset: 0x0010EDFC
		public new CommonForEachStatementSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return (CommonForEachStatementSyntax)this.WithAttributeListsCore(attributeLists);
		}

		// Token: 0x0600425B RID: 16987 RVA: 0x00110C0A File Offset: 0x0010EE0A
		public new CommonForEachStatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return (CommonForEachStatementSyntax)this.AddAttributeListsCore(items);
		}
	}
}
