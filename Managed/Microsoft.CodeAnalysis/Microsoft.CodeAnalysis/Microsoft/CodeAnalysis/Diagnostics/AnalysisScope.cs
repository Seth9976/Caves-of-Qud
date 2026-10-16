using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x020002FD RID: 765
	[NullableContext(1)]
	[Nullable(0)]
	internal class AnalysisScope
	{
		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x060020C9 RID: 8393 RVA: 0x000656CF File Offset: 0x000638CF
		public SourceOrAdditionalFile? FilterFileOpt { get; }

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x060020CA RID: 8394 RVA: 0x000656D7 File Offset: 0x000638D7
		public TextSpan? FilterSpanOpt { get; }

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x060020CB RID: 8395 RVA: 0x000656DF File Offset: 0x000638DF
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<DiagnosticAnalyzer> Analyzers
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x060020CC RID: 8396 RVA: 0x000656E7 File Offset: 0x000638E7
		public IEnumerable<SyntaxTree> SyntaxTrees { get; }

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x060020CD RID: 8397 RVA: 0x000656EF File Offset: 0x000638EF
		public IEnumerable<AdditionalText> AdditionalFiles { get; }

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x060020CE RID: 8398 RVA: 0x000656F7 File Offset: 0x000638F7
		public bool ConcurrentAnalysis { get; }

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x060020CF RID: 8399 RVA: 0x000656FF File Offset: 0x000638FF
		public bool CategorizeDiagnostics { get; }

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x060020D0 RID: 8400 RVA: 0x00065707 File Offset: 0x00063907
		public bool IsSyntacticSingleFileAnalysis { get; }

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x060020D1 RID: 8401 RVA: 0x00065710 File Offset: 0x00063910
		public bool IsSingleFileAnalysis
		{
			get
			{
				return this.FilterFileOpt != null;
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x060020D2 RID: 8402 RVA: 0x0006572B File Offset: 0x0006392B
		public bool IsPartialAnalysis { get; }

		// Token: 0x060020D3 RID: 8403 RVA: 0x00065734 File Offset: 0x00063934
		public AnalysisScope(Compilation compilation, [Nullable(2)] AnalyzerOptions analyzerOptions, [Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, bool hasAllAnalyzers, bool concurrentAnalysis, bool categorizeDiagnostics)
			: this(compilation.SyntaxTrees, (analyzerOptions != null) ? analyzerOptions.AdditionalFiles : ImmutableArray<AdditionalText>.Empty, analyzers, !hasAllAnalyzers, default(SourceOrAdditionalFile?), default(TextSpan?), false, concurrentAnalysis, categorizeDiagnostics)
		{
		}

		// Token: 0x060020D4 RID: 8404 RVA: 0x00065780 File Offset: 0x00063980
		public AnalysisScope([Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, SourceOrAdditionalFile filterFile, TextSpan? filterSpan, bool isSyntacticSingleFileAnalysis, bool concurrentAnalysis, bool categorizeDiagnostics)
		{
			IEnumerable<SyntaxTree> enumerable2;
			if (filterFile.SourceTree == null)
			{
				IEnumerable<SyntaxTree> enumerable = SpecializedCollections.EmptyEnumerable<SyntaxTree>();
				enumerable2 = enumerable;
			}
			else
			{
				enumerable2 = SpecializedCollections.SingletonEnumerable<SyntaxTree>(filterFile.SourceTree);
			}
			IEnumerable<AdditionalText> enumerable4;
			if (filterFile.AdditionalFile == null)
			{
				IEnumerable<AdditionalText> enumerable3 = SpecializedCollections.EmptyEnumerable<AdditionalText>();
				enumerable4 = enumerable3;
			}
			else
			{
				enumerable4 = SpecializedCollections.SingletonEnumerable<AdditionalText>(filterFile.AdditionalFile);
			}
			this..ctor(enumerable2, enumerable4, analyzers, true, new SourceOrAdditionalFile?(filterFile), filterSpan, isSyntacticSingleFileAnalysis, concurrentAnalysis, categorizeDiagnostics);
		}

		// Token: 0x060020D5 RID: 8405 RVA: 0x000657E0 File Offset: 0x000639E0
		private AnalysisScope(IEnumerable<SyntaxTree> trees, IEnumerable<AdditionalText> additionalFiles, [Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, bool isPartialAnalysis, SourceOrAdditionalFile? filterFile, TextSpan? filterSpanOpt, bool isSyntacticSingleFileAnalysis, bool concurrentAnalysis, bool categorizeDiagnostics)
		{
			this.SyntaxTrees = trees;
			this.AdditionalFiles = additionalFiles;
			this.Analyzers = analyzers;
			this.IsPartialAnalysis = isPartialAnalysis;
			this.FilterFileOpt = filterFile;
			this.FilterSpanOpt = filterSpanOpt;
			this.IsSyntacticSingleFileAnalysis = isSyntacticSingleFileAnalysis;
			this.ConcurrentAnalysis = concurrentAnalysis;
			this.CategorizeDiagnostics = categorizeDiagnostics;
			this._lazyAnalyzersSet = new Lazy<ImmutableHashSet<DiagnosticAnalyzer>>(new Func<ImmutableHashSet<DiagnosticAnalyzer>>(this.CreateAnalyzersSet));
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x0006584F File Offset: 0x00063A4F
		private ImmutableHashSet<DiagnosticAnalyzer> CreateAnalyzersSet()
		{
			return this.Analyzers.ToImmutableHashSet<DiagnosticAnalyzer>();
		}

		// Token: 0x060020D7 RID: 8407 RVA: 0x00065861 File Offset: 0x00063A61
		public bool Contains(DiagnosticAnalyzer analyzer)
		{
			return !this.IsPartialAnalysis || this._lazyAnalyzersSet.Value.Contains(analyzer);
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x00065880 File Offset: 0x00063A80
		public AnalysisScope WithAnalyzers([Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, bool hasAllAnalyzers)
		{
			bool flag = this.IsSingleFileAnalysis || !hasAllAnalyzers;
			return new AnalysisScope(this.SyntaxTrees, this.AdditionalFiles, analyzers, flag, this.FilterFileOpt, this.FilterSpanOpt, this.IsSyntacticSingleFileAnalysis, this.ConcurrentAnalysis, this.CategorizeDiagnostics);
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x000658CE File Offset: 0x00063ACE
		public static bool ShouldSkipSymbolAnalysis(SymbolDeclaredCompilationEvent symbolEvent)
		{
			if (!symbolEvent.Symbol.IsImplicitlyDeclared)
			{
				return symbolEvent.DeclaringSyntaxReferences.All<SyntaxReference>((SyntaxReference s) => s.SyntaxTree == null);
			}
			return true;
		}

		// Token: 0x060020DA RID: 8410 RVA: 0x00065909 File Offset: 0x00063B09
		public static bool ShouldSkipDeclarationAnalysis(ISymbol symbol)
		{
			return symbol.IsImplicitlyDeclared && (symbol.Kind != SymbolKind.Namespace || !((INamespaceSymbol)symbol).IsGlobalNamespace);
		}

		// Token: 0x060020DB RID: 8411 RVA: 0x00065930 File Offset: 0x00063B30
		public bool ShouldAnalyze(SyntaxTree tree)
		{
			return this.FilterFileOpt == null || this.FilterFileOpt.Value.SourceTree == tree;
		}

		// Token: 0x060020DC RID: 8412 RVA: 0x00065968 File Offset: 0x00063B68
		public bool ShouldAnalyze(AdditionalText file)
		{
			return this.FilterFileOpt == null || this.FilterFileOpt.Value.AdditionalFile == file;
		}

		// Token: 0x060020DD RID: 8413 RVA: 0x000659A0 File Offset: 0x00063BA0
		public bool ShouldAnalyze(ISymbol symbol)
		{
			if (this.FilterFileOpt == null)
			{
				return true;
			}
			if (this.FilterFileOpt.Value.SourceTree == null)
			{
				return false;
			}
			foreach (Location location in symbol.Locations)
			{
				if (this.FilterFileOpt.Value.SourceTree == location.SourceTree && this.ShouldInclude(location.SourceSpan))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x00065A30 File Offset: 0x00063C30
		public bool ShouldAnalyze(SyntaxNode node)
		{
			return this.FilterFileOpt == null || (this.FilterFileOpt.Value.SourceTree != null && this.ShouldInclude(node.FullSpan));
		}

		// Token: 0x060020DF RID: 8415 RVA: 0x00065A78 File Offset: 0x00063C78
		private bool ShouldInclude(TextSpan filterSpan)
		{
			return this.FilterSpanOpt == null || this.FilterSpanOpt.Value.IntersectsWith(filterSpan);
		}

		// Token: 0x060020E0 RID: 8416 RVA: 0x00065AB0 File Offset: 0x00063CB0
		public bool ContainsSpan(TextSpan filterSpan)
		{
			return this.FilterSpanOpt == null || this.FilterSpanOpt.Value.Contains(filterSpan);
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x00065AE8 File Offset: 0x00063CE8
		public bool ShouldInclude(Diagnostic diagnostic)
		{
			if (this.FilterFileOpt == null)
			{
				return true;
			}
			if (diagnostic.Location.IsInSource)
			{
				if (diagnostic.Location.SourceTree != this.FilterFileOpt.Value.SourceTree)
				{
					return false;
				}
			}
			else
			{
				ExternalFileLocation externalFileLocation = diagnostic.Location as ExternalFileLocation;
				if (externalFileLocation != null && (this.FilterFileOpt.Value.AdditionalFile == null || !PathUtilities.Comparer.Equals(externalFileLocation.FilePath, this.FilterFileOpt.Value.AdditionalFile.Path)))
				{
					return false;
				}
			}
			return this.ShouldInclude(diagnostic.Location.SourceSpan);
		}

		// Token: 0x04000E43 RID: 3651
		private readonly Lazy<ImmutableHashSet<DiagnosticAnalyzer>> _lazyAnalyzersSet;
	}
}
