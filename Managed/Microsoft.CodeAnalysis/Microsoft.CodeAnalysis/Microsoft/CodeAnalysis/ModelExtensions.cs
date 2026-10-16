using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000141 RID: 321
	[NullableContext(1)]
	[Nullable(0)]
	public static class ModelExtensions
	{
		// Token: 0x06000D08 RID: 3336 RVA: 0x00026DD0 File Offset: 0x00024FD0
		public static SymbolInfo GetSymbolInfo(this SemanticModel semanticModel, SyntaxNode node, CancellationToken cancellationToken = default(CancellationToken))
		{
			return semanticModel.GetSymbolInfo(node, cancellationToken);
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x00026DDA File Offset: 0x00024FDA
		public static SymbolInfo GetSpeculativeSymbolInfo(this SemanticModel semanticModel, int position, SyntaxNode expression, SpeculativeBindingOption bindingOption)
		{
			return semanticModel.GetSpeculativeSymbolInfo(position, expression, bindingOption);
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x00026DE5 File Offset: 0x00024FE5
		public static TypeInfo GetTypeInfo(this SemanticModel semanticModel, SyntaxNode node, CancellationToken cancellationToken = default(CancellationToken))
		{
			return semanticModel.GetTypeInfo(node, cancellationToken);
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x00026DEF File Offset: 0x00024FEF
		[return: Nullable(2)]
		public static IAliasSymbol GetAliasInfo(this SemanticModel semanticModel, SyntaxNode nameSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			return semanticModel.GetAliasInfo(nameSyntax, cancellationToken);
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x00026DF9 File Offset: 0x00024FF9
		[return: Nullable(2)]
		public static IAliasSymbol GetSpeculativeAliasInfo(this SemanticModel semanticModel, int position, SyntaxNode nameSyntax, SpeculativeBindingOption bindingOption)
		{
			return semanticModel.GetSpeculativeAliasInfo(position, nameSyntax, bindingOption);
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x00026E04 File Offset: 0x00025004
		public static TypeInfo GetSpeculativeTypeInfo(this SemanticModel semanticModel, int position, SyntaxNode expression, SpeculativeBindingOption bindingOption)
		{
			return semanticModel.GetSpeculativeTypeInfo(position, expression, bindingOption);
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x00026E0F File Offset: 0x0002500F
		[return: Nullable(2)]
		public static ISymbol GetDeclaredSymbol(this SemanticModel semanticModel, SyntaxNode declaration, CancellationToken cancellationToken = default(CancellationToken))
		{
			return semanticModel.GetDeclaredSymbolForNode(declaration, cancellationToken);
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x00026E19 File Offset: 0x00025019
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<ISymbol> GetMemberGroup(this SemanticModel semanticModel, SyntaxNode node, CancellationToken cancellationToken = default(CancellationToken))
		{
			return semanticModel.GetMemberGroup(node, cancellationToken);
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x00026E23 File Offset: 0x00025023
		public static ControlFlowAnalysis AnalyzeControlFlow(this SemanticModel semanticModel, SyntaxNode firstStatement, SyntaxNode lastStatement)
		{
			return semanticModel.AnalyzeControlFlow(firstStatement, lastStatement);
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x00026E2D File Offset: 0x0002502D
		public static ControlFlowAnalysis AnalyzeControlFlow(this SemanticModel semanticModel, SyntaxNode statement)
		{
			return semanticModel.AnalyzeControlFlow(statement);
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x00026E36 File Offset: 0x00025036
		public static DataFlowAnalysis AnalyzeDataFlow(this SemanticModel semanticModel, SyntaxNode firstStatement, SyntaxNode lastStatement)
		{
			return semanticModel.AnalyzeDataFlow(firstStatement, lastStatement);
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x00026E40 File Offset: 0x00025040
		public static DataFlowAnalysis AnalyzeDataFlow(this SemanticModel semanticModel, SyntaxNode statementOrExpression)
		{
			return semanticModel.AnalyzeDataFlow(statementOrExpression);
		}
	}
}
