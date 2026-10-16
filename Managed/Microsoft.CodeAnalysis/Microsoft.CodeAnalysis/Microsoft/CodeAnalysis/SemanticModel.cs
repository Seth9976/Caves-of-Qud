using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000150 RID: 336
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class SemanticModel
	{
		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000D5C RID: 3420
		public abstract string Language { get; }

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000D5D RID: 3421 RVA: 0x00027467 File Offset: 0x00025667
		public Compilation Compilation
		{
			get
			{
				return this.CompilationCore;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000D5E RID: 3422
		protected abstract Compilation CompilationCore { get; }

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x0002746F File Offset: 0x0002566F
		public SyntaxTree SyntaxTree
		{
			get
			{
				return this.SyntaxTreeCore;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000D60 RID: 3424
		protected abstract SyntaxTree SyntaxTreeCore { get; }

		// Token: 0x06000D61 RID: 3425 RVA: 0x00027478 File Offset: 0x00025678
		[return: Nullable(2)]
		public IOperation GetOperation(SyntaxNode node, CancellationToken cancellationToken = default(CancellationToken))
		{
			try
			{
				return this.GetOperationCore(node, cancellationToken);
			}
			catch (Exception ex) when (FatalError.ReportAndCatchUnlessCanceled(ex, cancellationToken))
			{
			}
			return null;
		}

		// Token: 0x06000D62 RID: 3426
		[return: Nullable(2)]
		protected abstract IOperation GetOperationCore(SyntaxNode node, CancellationToken cancellationToken);

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000D63 RID: 3427 RVA: 0x000274C0 File Offset: 0x000256C0
		public virtual bool IgnoresAccessibility
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x000274C3 File Offset: 0x000256C3
		internal SymbolInfo GetSymbolInfo(SyntaxNode node, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.GetSymbolInfoCore(node, cancellationToken);
		}

		// Token: 0x06000D65 RID: 3429
		protected abstract SymbolInfo GetSymbolInfoCore(SyntaxNode node, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06000D66 RID: 3430 RVA: 0x000274CD File Offset: 0x000256CD
		internal SymbolInfo GetSpeculativeSymbolInfo(int position, SyntaxNode expression, SpeculativeBindingOption bindingOption)
		{
			return this.GetSpeculativeSymbolInfoCore(position, expression, bindingOption);
		}

		// Token: 0x06000D67 RID: 3431
		protected abstract SymbolInfo GetSpeculativeSymbolInfoCore(int position, SyntaxNode expression, SpeculativeBindingOption bindingOption);

		// Token: 0x06000D68 RID: 3432 RVA: 0x000274D8 File Offset: 0x000256D8
		internal TypeInfo GetSpeculativeTypeInfo(int position, SyntaxNode expression, SpeculativeBindingOption bindingOption)
		{
			return this.GetSpeculativeTypeInfoCore(position, expression, bindingOption);
		}

		// Token: 0x06000D69 RID: 3433
		protected abstract TypeInfo GetSpeculativeTypeInfoCore(int position, SyntaxNode expression, SpeculativeBindingOption bindingOption);

		// Token: 0x06000D6A RID: 3434 RVA: 0x000274E3 File Offset: 0x000256E3
		internal TypeInfo GetTypeInfo(SyntaxNode node, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.GetTypeInfoCore(node, cancellationToken);
		}

		// Token: 0x06000D6B RID: 3435
		protected abstract TypeInfo GetTypeInfoCore(SyntaxNode node, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06000D6C RID: 3436 RVA: 0x000274ED File Offset: 0x000256ED
		[return: Nullable(2)]
		internal IAliasSymbol GetAliasInfo(SyntaxNode nameSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.GetAliasInfoCore(nameSyntax, cancellationToken);
		}

		// Token: 0x06000D6D RID: 3437
		[return: Nullable(2)]
		protected abstract IAliasSymbol GetAliasInfoCore(SyntaxNode nameSyntax, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000D6E RID: 3438
		[MemberNotNullWhen(true, "ParentModel")]
		public abstract bool IsSpeculativeSemanticModel
		{
			[MemberNotNullWhen(true, "ParentModel")]
			get;
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000D6F RID: 3439
		public abstract int OriginalPositionForSpeculation { get; }

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000D70 RID: 3440 RVA: 0x000274F7 File Offset: 0x000256F7
		[Nullable(2)]
		public SemanticModel ParentModel
		{
			[NullableContext(2)]
			get
			{
				return this.ParentModelCore;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000D71 RID: 3441
		[Nullable(2)]
		protected abstract SemanticModel ParentModelCore
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000D72 RID: 3442
		internal abstract SemanticModel ContainingModelOrSelf { get; }

		// Token: 0x06000D73 RID: 3443 RVA: 0x000274FF File Offset: 0x000256FF
		[return: Nullable(2)]
		internal IAliasSymbol GetSpeculativeAliasInfo(int position, SyntaxNode nameSyntax, SpeculativeBindingOption bindingOption)
		{
			return this.GetSpeculativeAliasInfoCore(position, nameSyntax, bindingOption);
		}

		// Token: 0x06000D74 RID: 3444
		[return: Nullable(2)]
		protected abstract IAliasSymbol GetSpeculativeAliasInfoCore(int position, SyntaxNode nameSyntax, SpeculativeBindingOption bindingOption);

		// Token: 0x06000D75 RID: 3445
		[return: Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<Diagnostic> GetSyntaxDiagnostics(TextSpan? span = null, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06000D76 RID: 3446
		[return: Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<Diagnostic> GetDeclarationDiagnostics(TextSpan? span = null, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06000D77 RID: 3447
		[return: Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<Diagnostic> GetMethodBodyDiagnostics(TextSpan? span = null, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06000D78 RID: 3448
		[return: Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<Diagnostic> GetDiagnostics(TextSpan? span = null, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06000D79 RID: 3449 RVA: 0x0002750A File Offset: 0x0002570A
		[return: Nullable(2)]
		internal ISymbol GetDeclaredSymbolForNode(SyntaxNode declaration, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.GetDeclaredSymbolCore(declaration, cancellationToken);
		}

		// Token: 0x06000D7A RID: 3450
		[return: Nullable(2)]
		protected abstract ISymbol GetDeclaredSymbolCore(SyntaxNode declaration, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06000D7B RID: 3451 RVA: 0x00027514 File Offset: 0x00025714
		[return: Nullable(new byte[] { 0, 1 })]
		internal ImmutableArray<ISymbol> GetDeclaredSymbolsForNode(SyntaxNode declaration, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.GetDeclaredSymbolsCore(declaration, cancellationToken);
		}

		// Token: 0x06000D7C RID: 3452
		[return: Nullable(new byte[] { 0, 1 })]
		protected abstract ImmutableArray<ISymbol> GetDeclaredSymbolsCore(SyntaxNode declaration, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06000D7D RID: 3453 RVA: 0x0002751E File Offset: 0x0002571E
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ISymbol> LookupSymbols(int position, INamespaceOrTypeSymbol container = null, string name = null, bool includeReducedExtensionMethods = false)
		{
			return this.LookupSymbolsCore(position, container, name, includeReducedExtensionMethods);
		}

		// Token: 0x06000D7E RID: 3454
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		protected abstract ImmutableArray<ISymbol> LookupSymbolsCore(int position, INamespaceOrTypeSymbol container, string name, bool includeReducedExtensionMethods);

		// Token: 0x06000D7F RID: 3455 RVA: 0x0002752B File Offset: 0x0002572B
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ISymbol> LookupBaseMembers(int position, string name = null)
		{
			return this.LookupBaseMembersCore(position, name);
		}

		// Token: 0x06000D80 RID: 3456
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		protected abstract ImmutableArray<ISymbol> LookupBaseMembersCore(int position, string name);

		// Token: 0x06000D81 RID: 3457 RVA: 0x00027535 File Offset: 0x00025735
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ISymbol> LookupStaticMembers(int position, INamespaceOrTypeSymbol container = null, string name = null)
		{
			return this.LookupStaticMembersCore(position, container, name);
		}

		// Token: 0x06000D82 RID: 3458
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		protected abstract ImmutableArray<ISymbol> LookupStaticMembersCore(int position, INamespaceOrTypeSymbol container, string name);

		// Token: 0x06000D83 RID: 3459 RVA: 0x00027540 File Offset: 0x00025740
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ISymbol> LookupNamespacesAndTypes(int position, INamespaceOrTypeSymbol container = null, string name = null)
		{
			return this.LookupNamespacesAndTypesCore(position, container, name);
		}

		// Token: 0x06000D84 RID: 3460
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		protected abstract ImmutableArray<ISymbol> LookupNamespacesAndTypesCore(int position, INamespaceOrTypeSymbol container, string name);

		// Token: 0x06000D85 RID: 3461 RVA: 0x0002754B File Offset: 0x0002574B
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ISymbol> LookupLabels(int position, string name = null)
		{
			return this.LookupLabelsCore(position, name);
		}

		// Token: 0x06000D86 RID: 3462
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		protected abstract ImmutableArray<ISymbol> LookupLabelsCore(int position, string name);

		// Token: 0x06000D87 RID: 3463 RVA: 0x00027555 File Offset: 0x00025755
		internal ControlFlowAnalysis AnalyzeControlFlow(SyntaxNode firstStatement, SyntaxNode lastStatement)
		{
			return this.AnalyzeControlFlowCore(firstStatement, lastStatement);
		}

		// Token: 0x06000D88 RID: 3464
		protected abstract ControlFlowAnalysis AnalyzeControlFlowCore(SyntaxNode firstStatement, SyntaxNode lastStatement);

		// Token: 0x06000D89 RID: 3465 RVA: 0x0002755F File Offset: 0x0002575F
		internal ControlFlowAnalysis AnalyzeControlFlow(SyntaxNode statement)
		{
			return this.AnalyzeControlFlowCore(statement);
		}

		// Token: 0x06000D8A RID: 3466
		protected abstract ControlFlowAnalysis AnalyzeControlFlowCore(SyntaxNode statement);

		// Token: 0x06000D8B RID: 3467 RVA: 0x00027568 File Offset: 0x00025768
		internal DataFlowAnalysis AnalyzeDataFlow(SyntaxNode firstStatement, SyntaxNode lastStatement)
		{
			return this.AnalyzeDataFlowCore(firstStatement, lastStatement);
		}

		// Token: 0x06000D8C RID: 3468
		protected abstract DataFlowAnalysis AnalyzeDataFlowCore(SyntaxNode firstStatement, SyntaxNode lastStatement);

		// Token: 0x06000D8D RID: 3469 RVA: 0x00027572 File Offset: 0x00025772
		internal DataFlowAnalysis AnalyzeDataFlow(SyntaxNode statementOrExpression)
		{
			return this.AnalyzeDataFlowCore(statementOrExpression);
		}

		// Token: 0x06000D8E RID: 3470
		protected abstract DataFlowAnalysis AnalyzeDataFlowCore(SyntaxNode statementOrExpression);

		// Token: 0x06000D8F RID: 3471 RVA: 0x0002757B File Offset: 0x0002577B
		[return: Nullable(new byte[] { 0, 2 })]
		public Optional<object> GetConstantValue(SyntaxNode node, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.GetConstantValueCore(node, cancellationToken);
		}

		// Token: 0x06000D90 RID: 3472
		[return: Nullable(new byte[] { 0, 2 })]
		protected abstract Optional<object> GetConstantValueCore(SyntaxNode node, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06000D91 RID: 3473 RVA: 0x00027585 File Offset: 0x00025785
		[return: Nullable(new byte[] { 0, 1 })]
		internal ImmutableArray<ISymbol> GetMemberGroup(SyntaxNode node, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.GetMemberGroupCore(node, cancellationToken);
		}

		// Token: 0x06000D92 RID: 3474
		[return: Nullable(new byte[] { 0, 1 })]
		protected abstract ImmutableArray<ISymbol> GetMemberGroupCore(SyntaxNode node, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06000D93 RID: 3475 RVA: 0x0002758F File Offset: 0x0002578F
		[NullableContext(2)]
		public ISymbol GetEnclosingSymbol(int position, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.GetEnclosingSymbolCore(position, cancellationToken);
		}

		// Token: 0x06000D94 RID: 3476
		[NullableContext(2)]
		protected abstract ISymbol GetEnclosingSymbolCore(int position, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06000D95 RID: 3477 RVA: 0x00027599 File Offset: 0x00025799
		public bool IsAccessible(int position, ISymbol symbol)
		{
			return this.IsAccessibleCore(position, symbol);
		}

		// Token: 0x06000D96 RID: 3478
		protected abstract bool IsAccessibleCore(int position, ISymbol symbol);

		// Token: 0x06000D97 RID: 3479 RVA: 0x000275A3 File Offset: 0x000257A3
		public bool IsEventUsableAsField(int position, IEventSymbol eventSymbol)
		{
			return this.IsEventUsableAsFieldCore(position, eventSymbol);
		}

		// Token: 0x06000D98 RID: 3480
		protected abstract bool IsEventUsableAsFieldCore(int position, IEventSymbol eventSymbol);

		// Token: 0x06000D99 RID: 3481 RVA: 0x000275AD File Offset: 0x000257AD
		public PreprocessingSymbolInfo GetPreprocessingSymbolInfo(SyntaxNode nameSyntax)
		{
			return this.GetPreprocessingSymbolInfoCore(nameSyntax);
		}

		// Token: 0x06000D9A RID: 3482
		protected abstract PreprocessingSymbolInfo GetPreprocessingSymbolInfoCore(SyntaxNode nameSyntax);

		// Token: 0x06000D9B RID: 3483
		internal abstract void ComputeDeclarationsInSpan(TextSpan span, bool getSymbol, ArrayBuilder<DeclarationInfo> builder, CancellationToken cancellationToken);

		// Token: 0x06000D9C RID: 3484
		internal abstract void ComputeDeclarationsInNode(SyntaxNode node, ISymbol associatedSymbol, bool getSymbol, ArrayBuilder<DeclarationInfo> builder, CancellationToken cancellationToken, int? levelsToCompute = null);

		// Token: 0x06000D9D RID: 3485 RVA: 0x000275B6 File Offset: 0x000257B6
		[return: Nullable(new byte[] { 2, 1 })]
		internal virtual Func<SyntaxNode, bool> GetSyntaxNodesToAnalyzeFilter(SyntaxNode declaredNode, ISymbol declaredSymbol)
		{
			return null;
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x000275B9 File Offset: 0x000257B9
		protected internal virtual SyntaxNode GetTopmostNodeForDiagnosticAnalysis(ISymbol symbol, SyntaxNode declaringSyntax)
		{
			return declaringSyntax;
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000D9F RID: 3487 RVA: 0x000275BC File Offset: 0x000257BC
		internal SyntaxNode Root
		{
			get
			{
				return this.RootCore;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000DA0 RID: 3488
		protected abstract SyntaxNode RootCore { get; }

		// Token: 0x06000DA1 RID: 3489
		public abstract NullableContext GetNullableContext(int position);
	}
}
