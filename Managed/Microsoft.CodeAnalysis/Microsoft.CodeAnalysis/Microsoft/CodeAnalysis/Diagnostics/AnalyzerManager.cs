using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000309 RID: 777
	[NullableContext(1)]
	[Nullable(0)]
	internal class AnalyzerManager
	{
		// Token: 0x06002220 RID: 8736 RVA: 0x0006C871 File Offset: 0x0006AA71
		public AnalyzerManager([Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers)
		{
			this._analyzerExecutionContextMap = this.CreateAnalyzerExecutionContextMap(analyzers);
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x0006C88B File Offset: 0x0006AA8B
		public AnalyzerManager(DiagnosticAnalyzer analyzer)
		{
			this._analyzerExecutionContextMap = this.CreateAnalyzerExecutionContextMap(SpecializedCollections.SingletonEnumerable<DiagnosticAnalyzer>(analyzer));
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x0006C8A8 File Offset: 0x0006AAA8
		private ImmutableDictionary<DiagnosticAnalyzer, AnalyzerManager.AnalyzerExecutionContext> CreateAnalyzerExecutionContextMap(IEnumerable<DiagnosticAnalyzer> analyzers)
		{
			ImmutableDictionary<DiagnosticAnalyzer, AnalyzerManager.AnalyzerExecutionContext>.Builder builder = ImmutableDictionary.CreateBuilder<DiagnosticAnalyzer, AnalyzerManager.AnalyzerExecutionContext>();
			foreach (DiagnosticAnalyzer diagnosticAnalyzer in analyzers)
			{
				builder.Add(diagnosticAnalyzer, new AnalyzerManager.AnalyzerExecutionContext(diagnosticAnalyzer));
			}
			return builder.ToImmutable();
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x0006C904 File Offset: 0x0006AB04
		private AnalyzerManager.AnalyzerExecutionContext GetAnalyzerExecutionContext(DiagnosticAnalyzer analyzer)
		{
			return this._analyzerExecutionContextMap[analyzer];
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x0006C914 File Offset: 0x0006AB14
		[return: Nullable(new byte[] { 0, 1 })]
		private ValueTask<HostCompilationStartAnalysisScope> GetCompilationAnalysisScopeAsync(DiagnosticAnalyzer analyzer, HostSessionStartAnalysisScope sessionScope, AnalyzerExecutor analyzerExecutor)
		{
			AnalyzerManager.<GetCompilationAnalysisScopeAsync>d__6 <GetCompilationAnalysisScopeAsync>d__;
			<GetCompilationAnalysisScopeAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<HostCompilationStartAnalysisScope>.Create();
			<GetCompilationAnalysisScopeAsync>d__.<>4__this = this;
			<GetCompilationAnalysisScopeAsync>d__.analyzer = analyzer;
			<GetCompilationAnalysisScopeAsync>d__.sessionScope = sessionScope;
			<GetCompilationAnalysisScopeAsync>d__.analyzerExecutor = analyzerExecutor;
			<GetCompilationAnalysisScopeAsync>d__.<>1__state = -1;
			<GetCompilationAnalysisScopeAsync>d__.<>t__builder.Start<AnalyzerManager.<GetCompilationAnalysisScopeAsync>d__6>(ref <GetCompilationAnalysisScopeAsync>d__);
			return <GetCompilationAnalysisScopeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x0006C970 File Offset: 0x0006AB70
		[return: Nullable(new byte[] { 0, 1 })]
		private ValueTask<HostCompilationStartAnalysisScope> GetCompilationAnalysisScopeCoreAsync(HostSessionStartAnalysisScope sessionScope, AnalyzerExecutor analyzerExecutor, AnalyzerManager.AnalyzerExecutionContext analyzerExecutionContext)
		{
			AnalyzerManager.<GetCompilationAnalysisScopeCoreAsync>d__7 <GetCompilationAnalysisScopeCoreAsync>d__;
			<GetCompilationAnalysisScopeCoreAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<HostCompilationStartAnalysisScope>.Create();
			<GetCompilationAnalysisScopeCoreAsync>d__.<>4__this = this;
			<GetCompilationAnalysisScopeCoreAsync>d__.sessionScope = sessionScope;
			<GetCompilationAnalysisScopeCoreAsync>d__.analyzerExecutor = analyzerExecutor;
			<GetCompilationAnalysisScopeCoreAsync>d__.analyzerExecutionContext = analyzerExecutionContext;
			<GetCompilationAnalysisScopeCoreAsync>d__.<>1__state = -1;
			<GetCompilationAnalysisScopeCoreAsync>d__.<>t__builder.Start<AnalyzerManager.<GetCompilationAnalysisScopeCoreAsync>d__7>(ref <GetCompilationAnalysisScopeCoreAsync>d__);
			return <GetCompilationAnalysisScopeCoreAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x0006C9CC File Offset: 0x0006ABCC
		private Task<HostSymbolStartAnalysisScope> GetSymbolAnalysisScopeAsync(ISymbol symbol, DiagnosticAnalyzer analyzer, [Nullable(new byte[] { 0, 1 })] ImmutableArray<SymbolStartAnalyzerAction> symbolStartActions, AnalyzerExecutor analyzerExecutor)
		{
			AnalyzerManager.<GetSymbolAnalysisScopeAsync>d__8 <GetSymbolAnalysisScopeAsync>d__;
			<GetSymbolAnalysisScopeAsync>d__.<>t__builder = AsyncTaskMethodBuilder<HostSymbolStartAnalysisScope>.Create();
			<GetSymbolAnalysisScopeAsync>d__.<>4__this = this;
			<GetSymbolAnalysisScopeAsync>d__.symbol = symbol;
			<GetSymbolAnalysisScopeAsync>d__.analyzer = analyzer;
			<GetSymbolAnalysisScopeAsync>d__.symbolStartActions = symbolStartActions;
			<GetSymbolAnalysisScopeAsync>d__.analyzerExecutor = analyzerExecutor;
			<GetSymbolAnalysisScopeAsync>d__.<>1__state = -1;
			<GetSymbolAnalysisScopeAsync>d__.<>t__builder.Start<AnalyzerManager.<GetSymbolAnalysisScopeAsync>d__8>(ref <GetSymbolAnalysisScopeAsync>d__);
			return <GetSymbolAnalysisScopeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x0006CA30 File Offset: 0x0006AC30
		private Task<HostSymbolStartAnalysisScope> GetSymbolAnalysisScopeCoreAsync(ISymbol symbol, [Nullable(new byte[] { 0, 1 })] ImmutableArray<SymbolStartAnalyzerAction> symbolStartActions, AnalyzerExecutor analyzerExecutor, AnalyzerManager.AnalyzerExecutionContext analyzerExecutionContext)
		{
			AnalyzerManager.<GetSymbolAnalysisScopeCoreAsync>d__9 <GetSymbolAnalysisScopeCoreAsync>d__;
			<GetSymbolAnalysisScopeCoreAsync>d__.<>t__builder = AsyncTaskMethodBuilder<HostSymbolStartAnalysisScope>.Create();
			<GetSymbolAnalysisScopeCoreAsync>d__.<>4__this = this;
			<GetSymbolAnalysisScopeCoreAsync>d__.symbol = symbol;
			<GetSymbolAnalysisScopeCoreAsync>d__.symbolStartActions = symbolStartActions;
			<GetSymbolAnalysisScopeCoreAsync>d__.analyzerExecutor = analyzerExecutor;
			<GetSymbolAnalysisScopeCoreAsync>d__.analyzerExecutionContext = analyzerExecutionContext;
			<GetSymbolAnalysisScopeCoreAsync>d__.<>1__state = -1;
			<GetSymbolAnalysisScopeCoreAsync>d__.<>t__builder.Start<AnalyzerManager.<GetSymbolAnalysisScopeCoreAsync>d__9>(ref <GetSymbolAnalysisScopeCoreAsync>d__);
			return <GetSymbolAnalysisScopeCoreAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x0006CA94 File Offset: 0x0006AC94
		[return: Nullable(new byte[] { 0, 1 })]
		private ValueTask<HostSessionStartAnalysisScope> GetSessionAnalysisScopeAsync(DiagnosticAnalyzer analyzer, AnalyzerExecutor analyzerExecutor)
		{
			AnalyzerManager.<GetSessionAnalysisScopeAsync>d__10 <GetSessionAnalysisScopeAsync>d__;
			<GetSessionAnalysisScopeAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<HostSessionStartAnalysisScope>.Create();
			<GetSessionAnalysisScopeAsync>d__.<>4__this = this;
			<GetSessionAnalysisScopeAsync>d__.analyzer = analyzer;
			<GetSessionAnalysisScopeAsync>d__.analyzerExecutor = analyzerExecutor;
			<GetSessionAnalysisScopeAsync>d__.<>1__state = -1;
			<GetSessionAnalysisScopeAsync>d__.<>t__builder.Start<AnalyzerManager.<GetSessionAnalysisScopeAsync>d__10>(ref <GetSessionAnalysisScopeAsync>d__);
			return <GetSessionAnalysisScopeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06002229 RID: 8745 RVA: 0x0006CAE8 File Offset: 0x0006ACE8
		[return: Nullable(new byte[] { 0, 1 })]
		private ValueTask<HostSessionStartAnalysisScope> GetSessionAnalysisScopeCoreAsync(AnalyzerExecutor analyzerExecutor, AnalyzerManager.AnalyzerExecutionContext analyzerExecutionContext)
		{
			AnalyzerManager.<GetSessionAnalysisScopeCoreAsync>d__11 <GetSessionAnalysisScopeCoreAsync>d__;
			<GetSessionAnalysisScopeCoreAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<HostSessionStartAnalysisScope>.Create();
			<GetSessionAnalysisScopeCoreAsync>d__.<>4__this = this;
			<GetSessionAnalysisScopeCoreAsync>d__.analyzerExecutor = analyzerExecutor;
			<GetSessionAnalysisScopeCoreAsync>d__.analyzerExecutionContext = analyzerExecutionContext;
			<GetSessionAnalysisScopeCoreAsync>d__.<>1__state = -1;
			<GetSessionAnalysisScopeCoreAsync>d__.<>t__builder.Start<AnalyzerManager.<GetSessionAnalysisScopeCoreAsync>d__11>(ref <GetSessionAnalysisScopeCoreAsync>d__);
			return <GetSessionAnalysisScopeCoreAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x0006CB3C File Offset: 0x0006AD3C
		[return: Nullable(0)]
		public ValueTask<AnalyzerActions> GetAnalyzerActionsAsync(DiagnosticAnalyzer analyzer, AnalyzerExecutor analyzerExecutor)
		{
			AnalyzerManager.<GetAnalyzerActionsAsync>d__12 <GetAnalyzerActionsAsync>d__;
			<GetAnalyzerActionsAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<AnalyzerActions>.Create();
			<GetAnalyzerActionsAsync>d__.<>4__this = this;
			<GetAnalyzerActionsAsync>d__.analyzer = analyzer;
			<GetAnalyzerActionsAsync>d__.analyzerExecutor = analyzerExecutor;
			<GetAnalyzerActionsAsync>d__.<>1__state = -1;
			<GetAnalyzerActionsAsync>d__.<>t__builder.Start<AnalyzerManager.<GetAnalyzerActionsAsync>d__12>(ref <GetAnalyzerActionsAsync>d__);
			return <GetAnalyzerActionsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600222B RID: 8747 RVA: 0x0006CB90 File Offset: 0x0006AD90
		[return: Nullable(0)]
		public ValueTask<AnalyzerActions> GetPerSymbolAnalyzerActionsAsync(ISymbol symbol, DiagnosticAnalyzer analyzer, AnalyzerExecutor analyzerExecutor)
		{
			AnalyzerManager.<GetPerSymbolAnalyzerActionsAsync>d__13 <GetPerSymbolAnalyzerActionsAsync>d__;
			<GetPerSymbolAnalyzerActionsAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<AnalyzerActions>.Create();
			<GetPerSymbolAnalyzerActionsAsync>d__.<>4__this = this;
			<GetPerSymbolAnalyzerActionsAsync>d__.symbol = symbol;
			<GetPerSymbolAnalyzerActionsAsync>d__.analyzer = analyzer;
			<GetPerSymbolAnalyzerActionsAsync>d__.analyzerExecutor = analyzerExecutor;
			<GetPerSymbolAnalyzerActionsAsync>d__.<>1__state = -1;
			<GetPerSymbolAnalyzerActionsAsync>d__.<>t__builder.Start<AnalyzerManager.<GetPerSymbolAnalyzerActionsAsync>d__13>(ref <GetPerSymbolAnalyzerActionsAsync>d__);
			return <GetPerSymbolAnalyzerActionsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600222C RID: 8748 RVA: 0x0006CBEC File Offset: 0x0006ADEC
		public Task<bool> IsConcurrentAnalyzerAsync(DiagnosticAnalyzer analyzer, AnalyzerExecutor analyzerExecutor)
		{
			AnalyzerManager.<IsConcurrentAnalyzerAsync>d__14 <IsConcurrentAnalyzerAsync>d__;
			<IsConcurrentAnalyzerAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<IsConcurrentAnalyzerAsync>d__.<>4__this = this;
			<IsConcurrentAnalyzerAsync>d__.analyzer = analyzer;
			<IsConcurrentAnalyzerAsync>d__.analyzerExecutor = analyzerExecutor;
			<IsConcurrentAnalyzerAsync>d__.<>1__state = -1;
			<IsConcurrentAnalyzerAsync>d__.<>t__builder.Start<AnalyzerManager.<IsConcurrentAnalyzerAsync>d__14>(ref <IsConcurrentAnalyzerAsync>d__);
			return <IsConcurrentAnalyzerAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x0006CC40 File Offset: 0x0006AE40
		public Task<GeneratedCodeAnalysisFlags> GetGeneratedCodeAnalysisFlagsAsync(DiagnosticAnalyzer analyzer, AnalyzerExecutor analyzerExecutor)
		{
			AnalyzerManager.<GetGeneratedCodeAnalysisFlagsAsync>d__15 <GetGeneratedCodeAnalysisFlagsAsync>d__;
			<GetGeneratedCodeAnalysisFlagsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<GeneratedCodeAnalysisFlags>.Create();
			<GetGeneratedCodeAnalysisFlagsAsync>d__.<>4__this = this;
			<GetGeneratedCodeAnalysisFlagsAsync>d__.analyzer = analyzer;
			<GetGeneratedCodeAnalysisFlagsAsync>d__.analyzerExecutor = analyzerExecutor;
			<GetGeneratedCodeAnalysisFlagsAsync>d__.<>1__state = -1;
			<GetGeneratedCodeAnalysisFlagsAsync>d__.<>t__builder.Start<AnalyzerManager.<GetGeneratedCodeAnalysisFlagsAsync>d__15>(ref <GetGeneratedCodeAnalysisFlagsAsync>d__);
			return <GetGeneratedCodeAnalysisFlagsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x0006CC93 File Offset: 0x0006AE93
		private static void ForceLocalizableStringExceptions(LocalizableString localizableString, EventHandler<Exception> handler)
		{
			if (localizableString.CanThrowExceptions)
			{
				localizableString.OnException += handler;
				localizableString.ToString();
				localizableString.OnException -= handler;
			}
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x0006CCB2 File Offset: 0x0006AEB2
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<DiagnosticDescriptor> GetSupportedDiagnosticDescriptors(DiagnosticAnalyzer analyzer, AnalyzerExecutor analyzerExecutor)
		{
			return this.GetAnalyzerExecutionContext(analyzer).GetOrComputeDiagnosticDescriptors(analyzer, analyzerExecutor);
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x0006CCC2 File Offset: 0x0006AEC2
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<SuppressionDescriptor> GetSupportedSuppressionDescriptors(DiagnosticSuppressor suppressor, AnalyzerExecutor analyzerExecutor)
		{
			return this.GetAnalyzerExecutionContext(suppressor).GetOrComputeSuppressionDescriptors(suppressor, analyzerExecutor);
		}

		// Token: 0x06002231 RID: 8753 RVA: 0x0006CCD4 File Offset: 0x0006AED4
		internal bool IsSupportedDiagnostic(DiagnosticAnalyzer analyzer, Diagnostic diagnostic, Func<DiagnosticAnalyzer, bool> isCompilerAnalyzer, AnalyzerExecutor analyzerExecutor)
		{
			if (isCompilerAnalyzer.Invoke(analyzer))
			{
				return true;
			}
			ImmutableArray<DiagnosticDescriptor>.Enumerator enumerator = this.GetSupportedDiagnosticDescriptors(analyzer, analyzerExecutor).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Id.Equals(diagnostic.Id, 5))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002232 RID: 8754 RVA: 0x0006CD28 File Offset: 0x0006AF28
		internal bool IsDiagnosticAnalyzerSuppressed(DiagnosticAnalyzer analyzer, CompilationOptions options, Func<DiagnosticAnalyzer, bool> isCompilerAnalyzer, AnalyzerExecutor analyzerExecutor, SeverityFilter severityFilter)
		{
			if (isCompilerAnalyzer.Invoke(analyzer))
			{
				return false;
			}
			ImmutableArray<DiagnosticDescriptor> supportedDiagnosticDescriptors = this.GetSupportedDiagnosticDescriptors(analyzer, analyzerExecutor);
			ImmutableDictionary<string, ReportDiagnostic> specificDiagnosticOptions = options.SpecificDiagnosticOptions;
			Compilation compilation;
			AnalyzerOptions analyzerOptions;
			analyzerExecutor.TryGetCompilationAndAnalyzerOptions(out compilation, out analyzerOptions);
			foreach (DiagnosticDescriptor diagnosticDescriptor in supportedDiagnosticDescriptors)
			{
				if (AnalyzerManager.HasNotConfigurableTag(diagnosticDescriptor.CustomTags))
				{
					if (diagnosticDescriptor.IsEnabledByDefault)
					{
						return false;
					}
				}
				else
				{
					bool flag = !diagnosticDescriptor.IsEnabledByDefault;
					ReportDiagnostic reportDiagnostic;
					if ((specificDiagnosticOptions.TryGetValue(diagnosticDescriptor.Id, out reportDiagnostic) || (options.SyntaxTreeOptionsProvider != null && options.SyntaxTreeOptionsProvider.TryGetGlobalDiagnosticValue(diagnosticDescriptor.Id, analyzerExecutor.CancellationToken, out reportDiagnostic))) && reportDiagnostic != ReportDiagnostic.Default)
					{
						flag = reportDiagnostic == ReportDiagnostic.Suppress;
					}
					else
					{
						reportDiagnostic = (flag ? ReportDiagnostic.Suppress : DiagnosticDescriptor.MapSeverityToReport(diagnosticDescriptor.DefaultSeverity));
					}
					if (severityFilter.Contains(reportDiagnostic))
					{
						flag = true;
					}
					if (flag && AnalyzerManager.<IsDiagnosticAnalyzerSuppressed>g__isEnabledWithAnalyzerConfigOptions|20_0(diagnosticDescriptor, severityFilter, compilation, analyzerOptions, analyzerExecutor.CancellationToken))
					{
						flag = false;
					}
					if (!flag)
					{
						return false;
					}
				}
			}
			DiagnosticSuppressor diagnosticSuppressor = analyzer as DiagnosticSuppressor;
			if (diagnosticSuppressor != null)
			{
				ImmutableArray<SuppressionDescriptor>.Enumerator enumerator2 = this.GetSupportedSuppressionDescriptors(diagnosticSuppressor, analyzerExecutor).GetEnumerator();
				while (enumerator2.MoveNext())
				{
					if (!enumerator2.Current.IsDisabled(options))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06002233 RID: 8755 RVA: 0x0006CE70 File Offset: 0x0006B070
		internal static bool HasNotConfigurableTag(IEnumerable<string> customTags)
		{
			using (IEnumerator<string> enumerator = customTags.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == "NotConfigurable")
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002234 RID: 8756 RVA: 0x0006CEC4 File Offset: 0x0006B0C4
		public bool TryProcessCompletedMemberAndGetPendingSymbolEndActionsForContainer(ISymbol containingSymbol, ISymbol processedMemberSymbol, DiagnosticAnalyzer analyzer, [TupleElementNames(new string[] { "symbolEndActions", "symbolDeclaredEvent" })] [Nullable(new byte[] { 0, 0, 1, 1 })] out ValueTuple<ImmutableArray<SymbolEndAnalyzerAction>, SymbolDeclaredCompilationEvent> containerEndActionsAndEvent)
		{
			return this.GetAnalyzerExecutionContext(analyzer).TryProcessCompletedMemberAndGetPendingSymbolEndActionsForContainer(containingSymbol, processedMemberSymbol, out containerEndActionsAndEvent);
		}

		// Token: 0x06002235 RID: 8757 RVA: 0x0006CED6 File Offset: 0x0006B0D6
		public bool TryStartExecuteSymbolEndActions([Nullable(new byte[] { 0, 1 })] ImmutableArray<SymbolEndAnalyzerAction> symbolEndActions, DiagnosticAnalyzer analyzer, SymbolDeclaredCompilationEvent symbolDeclaredEvent)
		{
			return this.GetAnalyzerExecutionContext(analyzer).TryStartExecuteSymbolEndActions(symbolEndActions, symbolDeclaredEvent);
		}

		// Token: 0x06002236 RID: 8758 RVA: 0x0006CEE6 File Offset: 0x0006B0E6
		public void MarkSymbolEndAnalysisPending(ISymbol symbol, DiagnosticAnalyzer analyzer, [Nullable(new byte[] { 0, 1 })] ImmutableArray<SymbolEndAnalyzerAction> symbolEndActions, SymbolDeclaredCompilationEvent symbolDeclaredEvent)
		{
			this.GetAnalyzerExecutionContext(analyzer).MarkSymbolEndAnalysisPending(symbol, symbolEndActions, symbolDeclaredEvent);
		}

		// Token: 0x06002237 RID: 8759 RVA: 0x0006CEF8 File Offset: 0x0006B0F8
		public void MarkSymbolEndAnalysisComplete(ISymbol symbol, DiagnosticAnalyzer analyzer)
		{
			this.GetAnalyzerExecutionContext(analyzer).MarkSymbolEndAnalysisComplete(symbol);
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x0006CF08 File Offset: 0x0006B108
		[Conditional("DEBUG")]
		public void VerifyAllSymbolEndActionsExecuted()
		{
			foreach (AnalyzerManager.AnalyzerExecutionContext analyzerExecutionContext in this._analyzerExecutionContextMap.Values)
			{
			}
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x0006CF54 File Offset: 0x0006B154
		[CompilerGenerated]
		[return: Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<SymbolStartAnalyzerAction> <GetPerSymbolAnalyzerActionsAsync>g__getFilteredActionsByKind|13_0([Nullable(new byte[] { 0, 1 })] ImmutableArray<SymbolStartAnalyzerAction> symbolStartActions, ref AnalyzerManager.<>c__DisplayClass13_0 A_1)
		{
			ArrayBuilder<SymbolStartAnalyzerAction> arrayBuilder = null;
			for (int i = 0; i < symbolStartActions.Length; i++)
			{
				SymbolStartAnalyzerAction symbolStartAnalyzerAction = symbolStartActions[i];
				if (symbolStartAnalyzerAction.Kind != A_1.symbol.Kind)
				{
					if (arrayBuilder == null)
					{
						arrayBuilder = ArrayBuilder<SymbolStartAnalyzerAction>.GetInstance();
						arrayBuilder.AddRange(symbolStartActions, i);
					}
				}
				else if (arrayBuilder != null)
				{
					arrayBuilder.Add(symbolStartAnalyzerAction);
				}
			}
			if (arrayBuilder == null)
			{
				return symbolStartActions;
			}
			return arrayBuilder.ToImmutableAndFree();
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x0006CFBC File Offset: 0x0006B1BC
		[NullableContext(2)]
		[CompilerGenerated]
		internal static bool <IsDiagnosticAnalyzerSuppressed>g__isEnabledWithAnalyzerConfigOptions|20_0([Nullable(1)] DiagnosticDescriptor descriptor, SeverityFilter severityFilter, Compilation compilation, AnalyzerOptions analyzerOptions, CancellationToken cancellationToken)
		{
			if (compilation != null)
			{
				SyntaxTreeOptionsProvider syntaxTreeOptionsProvider = compilation.Options.SyntaxTreeOptionsProvider;
				if (syntaxTreeOptionsProvider != null)
				{
					foreach (SyntaxTree syntaxTree in compilation.SyntaxTrees)
					{
						ReportDiagnostic reportDiagnostic;
						if ((syntaxTreeOptionsProvider.TryGetDiagnosticValue(syntaxTree, descriptor.Id, cancellationToken, out reportDiagnostic) || analyzerOptions.TryGetSeverityFromBulkConfiguration(syntaxTree, compilation, descriptor, cancellationToken, out reportDiagnostic)) && reportDiagnostic != ReportDiagnostic.Suppress && !severityFilter.Contains(reportDiagnostic))
						{
							return true;
						}
					}
					return false;
				}
			}
			return false;
		}

		// Token: 0x04000EB4 RID: 3764
		private readonly ImmutableDictionary<DiagnosticAnalyzer, AnalyzerManager.AnalyzerExecutionContext> _analyzerExecutionContextMap;

		// Token: 0x020006D3 RID: 1747
		[Nullable(0)]
		private sealed class AnalyzerExecutionContext
		{
			// Token: 0x06003A96 RID: 14998 RVA: 0x000A49A4 File Offset: 0x000A2BA4
			public AnalyzerExecutionContext(DiagnosticAnalyzer analyzer)
			{
				this._analyzer = analyzer;
				this._gate = new object();
			}

			// Token: 0x06003A97 RID: 14999 RVA: 0x000A49C0 File Offset: 0x000A2BC0
			public Task<HostSessionStartAnalysisScope> GetSessionAnalysisScopeAsync(AnalyzerExecutor analyzerExecutor)
			{
				object gate = this._gate;
				Task<HostSessionStartAnalysisScope> task;
				lock (gate)
				{
					if (this._lazySessionScopeTask != null)
					{
						task = this._lazySessionScopeTask;
					}
					else
					{
						Task<HostSessionStartAnalysisScope> task2 = AnalyzerManager.AnalyzerExecutionContext.<GetSessionAnalysisScopeAsync>g__getSessionAnalysisScopeTaskSlow|10_0(this, analyzerExecutor);
						this._lazySessionScopeTask = task2;
						task = task2;
					}
				}
				return task;
			}

			// Token: 0x06003A98 RID: 15000 RVA: 0x000A4A20 File Offset: 0x000A2C20
			public void ClearSessionScopeTask()
			{
				object gate = this._gate;
				lock (gate)
				{
					this._lazySessionScopeTask = null;
				}
			}

			// Token: 0x06003A99 RID: 15001 RVA: 0x000A4A64 File Offset: 0x000A2C64
			public Task<HostCompilationStartAnalysisScope> GetCompilationAnalysisScopeAsync(HostSessionStartAnalysisScope sessionScope, AnalyzerExecutor analyzerExecutor)
			{
				object gate = this._gate;
				Task<HostCompilationStartAnalysisScope> lazyCompilationScopeTask;
				lock (gate)
				{
					if (this._lazyCompilationScopeTask == null)
					{
						this._lazyCompilationScopeTask = Task.Run<HostCompilationStartAnalysisScope>(delegate
						{
							HostCompilationStartAnalysisScope hostCompilationStartAnalysisScope = new HostCompilationStartAnalysisScope(sessionScope);
							analyzerExecutor.ExecuteCompilationStartActions(sessionScope.GetAnalyzerActions(this._analyzer).CompilationStartActions, hostCompilationStartAnalysisScope);
							return hostCompilationStartAnalysisScope;
						}, analyzerExecutor.CancellationToken);
					}
					lazyCompilationScopeTask = this._lazyCompilationScopeTask;
				}
				return lazyCompilationScopeTask;
			}

			// Token: 0x06003A9A RID: 15002 RVA: 0x000A4AEC File Offset: 0x000A2CEC
			public void ClearCompilationScopeTask()
			{
				object gate = this._gate;
				lock (gate)
				{
					this._lazyCompilationScopeTask = null;
				}
			}

			// Token: 0x06003A9B RID: 15003 RVA: 0x000A4B30 File Offset: 0x000A2D30
			public Task<HostSymbolStartAnalysisScope> GetSymbolAnalysisScopeAsync(ISymbol symbol, [Nullable(new byte[] { 0, 1 })] ImmutableArray<SymbolStartAnalyzerAction> symbolStartActions, AnalyzerExecutor analyzerExecutor)
			{
				object gate = this._gate;
				Task<HostSymbolStartAnalysisScope> task2;
				lock (gate)
				{
					if (this._lazySymbolScopeTasks == null)
					{
						this._lazySymbolScopeTasks = new Dictionary<ISymbol, Task<HostSymbolStartAnalysisScope>>();
					}
					Task<HostSymbolStartAnalysisScope> task;
					if (!this._lazySymbolScopeTasks.TryGetValue(symbol, ref task))
					{
						task = Task.Run<HostSymbolStartAnalysisScope>(() => base.<GetSymbolAnalysisScopeAsync>g__getSymbolAnalysisScopeCore|2(), analyzerExecutor.CancellationToken);
						this._lazySymbolScopeTasks.Add(symbol, task);
					}
					task2 = task;
				}
				return task2;
			}

			// Token: 0x06003A9C RID: 15004 RVA: 0x000A4BE8 File Offset: 0x000A2DE8
			[Conditional("DEBUG")]
			private void VerifyNewEntryForPendingMemberSymbolsMap(ISymbol symbol, [Nullable(new byte[] { 2, 1 })] HashSet<ISymbol> dependentSymbols)
			{
				HashSet<ISymbol> hashSet;
				if (this._lazyPendingMemberSymbolsMap.TryGetValue(symbol, ref hashSet))
				{
				}
			}

			// Token: 0x06003A9D RID: 15005 RVA: 0x000A4C08 File Offset: 0x000A2E08
			public void ClearSymbolScopeTask(ISymbol symbol)
			{
				object gate = this._gate;
				lock (gate)
				{
					Dictionary<ISymbol, Task<HostSymbolStartAnalysisScope>> lazySymbolScopeTasks = this._lazySymbolScopeTasks;
					if (lazySymbolScopeTasks != null)
					{
						lazySymbolScopeTasks.Remove(symbol);
					}
				}
			}

			// Token: 0x06003A9E RID: 15006 RVA: 0x000A4C58 File Offset: 0x000A2E58
			[return: Nullable(new byte[] { 0, 1 })]
			public ImmutableArray<DiagnosticDescriptor> GetOrComputeDiagnosticDescriptors(DiagnosticAnalyzer analyzer, AnalyzerExecutor analyzerExecutor)
			{
				return AnalyzerManager.AnalyzerExecutionContext.GetOrComputeDescriptors<DiagnosticDescriptor>(ref this._lazyDiagnosticDescriptors, new Func<DiagnosticAnalyzer, AnalyzerExecutor, ImmutableArray<DiagnosticDescriptor>>(AnalyzerManager.AnalyzerExecutionContext.ComputeDiagnosticDescriptors), analyzer, analyzerExecutor);
			}

			// Token: 0x06003A9F RID: 15007 RVA: 0x000A4C73 File Offset: 0x000A2E73
			[return: Nullable(new byte[] { 0, 1 })]
			public ImmutableArray<SuppressionDescriptor> GetOrComputeSuppressionDescriptors(DiagnosticSuppressor suppressor, AnalyzerExecutor analyzerExecutor)
			{
				return AnalyzerManager.AnalyzerExecutionContext.GetOrComputeDescriptors<SuppressionDescriptor>(ref this._lazySuppressionDescriptors, new Func<DiagnosticAnalyzer, AnalyzerExecutor, ImmutableArray<SuppressionDescriptor>>(AnalyzerManager.AnalyzerExecutionContext.ComputeSuppressionDescriptors), suppressor, analyzerExecutor);
			}

			// Token: 0x06003AA0 RID: 15008 RVA: 0x000A4C90 File Offset: 0x000A2E90
			[return: Nullable(new byte[] { 0, 1 })]
			private static ImmutableArray<TDescriptor> GetOrComputeDescriptors<[Nullable(2)] TDescriptor>([Nullable(new byte[] { 0, 1 })] ref ImmutableArray<TDescriptor> lazyDescriptors, [Nullable(new byte[] { 1, 1, 1, 0, 1 })] Func<DiagnosticAnalyzer, AnalyzerExecutor, ImmutableArray<TDescriptor>> computeDescriptors, DiagnosticAnalyzer analyzer, AnalyzerExecutor analyzerExecutor)
			{
				if (!lazyDescriptors.IsDefault)
				{
					return lazyDescriptors;
				}
				ImmutableArray<TDescriptor> immutableArray = computeDescriptors.Invoke(analyzer, analyzerExecutor);
				ImmutableInterlocked.InterlockedInitialize<TDescriptor>(ref lazyDescriptors, immutableArray);
				return lazyDescriptors;
			}

			// Token: 0x06003AA1 RID: 15009 RVA: 0x000A4CC4 File Offset: 0x000A2EC4
			[return: Nullable(new byte[] { 0, 1 })]
			private static ImmutableArray<DiagnosticDescriptor> ComputeDiagnosticDescriptors(DiagnosticAnalyzer analyzer, AnalyzerExecutor analyzerExecutor)
			{
				ImmutableArray<DiagnosticDescriptor> supportedDiagnostics = ImmutableArray<DiagnosticDescriptor>.Empty;
				analyzerExecutor.ExecuteAndCatchIfThrows<object>(analyzer, delegate(object _)
				{
					ImmutableArray<DiagnosticDescriptor> supportedDiagnostics2 = analyzer.SupportedDiagnostics;
					if (!supportedDiagnostics2.IsDefaultOrEmpty)
					{
						ImmutableArray<DiagnosticDescriptor>.Enumerator enumerator2 = supportedDiagnostics2.GetEnumerator();
						while (enumerator2.MoveNext())
						{
							if (enumerator2.Current == null)
							{
								throw new ArgumentException(string.Format(CodeAnalysisResources.SupportedDiagnosticsHasNullDescriptor, analyzer.ToString()), "SupportedDiagnostics");
							}
						}
						supportedDiagnostics = supportedDiagnostics2;
					}
				}, null, default(AnalysisContextInfo?));
				Action<Exception, DiagnosticAnalyzer, Diagnostic> onAnalyzerException = analyzerExecutor.OnAnalyzerException;
				if (onAnalyzerException != null)
				{
					EventHandler<Exception> eventHandler = delegate(object sender, [Nullable(1)] Exception ex)
					{
						Diagnostic diagnostic = AnalyzerExecutor.CreateAnalyzerExceptionDiagnostic(analyzer, ex, default(AnalysisContextInfo?));
						onAnalyzerException.Invoke(ex, analyzer, diagnostic);
					};
					foreach (DiagnosticDescriptor diagnosticDescriptor in supportedDiagnostics)
					{
						AnalyzerManager.ForceLocalizableStringExceptions(diagnosticDescriptor.Title, eventHandler);
						AnalyzerManager.ForceLocalizableStringExceptions(diagnosticDescriptor.MessageFormat, eventHandler);
						AnalyzerManager.ForceLocalizableStringExceptions(diagnosticDescriptor.Description, eventHandler);
					}
				}
				return supportedDiagnostics;
			}

			// Token: 0x06003AA2 RID: 15010 RVA: 0x000A4D74 File Offset: 0x000A2F74
			[return: Nullable(new byte[] { 0, 1 })]
			private static ImmutableArray<SuppressionDescriptor> ComputeSuppressionDescriptors(DiagnosticAnalyzer analyzer, AnalyzerExecutor analyzerExecutor)
			{
				ImmutableArray<SuppressionDescriptor> descriptors = ImmutableArray<SuppressionDescriptor>.Empty;
				DiagnosticSuppressor suppressor = analyzer as DiagnosticSuppressor;
				if (suppressor != null)
				{
					analyzerExecutor.ExecuteAndCatchIfThrows<object>(analyzer, delegate(object _)
					{
						ImmutableArray<SuppressionDescriptor> supportedSuppressions = suppressor.SupportedSuppressions;
						if (!supportedSuppressions.IsDefaultOrEmpty)
						{
							ImmutableArray<SuppressionDescriptor>.Enumerator enumerator = supportedSuppressions.GetEnumerator();
							while (enumerator.MoveNext())
							{
								if (enumerator.Current == null)
								{
									throw new ArgumentException(string.Format(CodeAnalysisResources.SupportedSuppressionsHasNullDescriptor, analyzer.ToString()), "SupportedSuppressions");
								}
							}
							descriptors = supportedSuppressions;
						}
					}, null, default(AnalysisContextInfo?));
				}
				return descriptors;
			}

			// Token: 0x06003AA3 RID: 15011 RVA: 0x000A4DDC File Offset: 0x000A2FDC
			public bool TryProcessCompletedMemberAndGetPendingSymbolEndActionsForContainer(ISymbol containingSymbol, ISymbol processedMemberSymbol, [TupleElementNames(new string[] { "symbolEndActions", "symbolDeclaredEvent" })] [Nullable(new byte[] { 0, 0, 1, 1 })] out ValueTuple<ImmutableArray<SymbolEndAnalyzerAction>, SymbolDeclaredCompilationEvent> containerEndActionsAndEvent)
			{
				containerEndActionsAndEvent = default(ValueTuple<ImmutableArray<SymbolEndAnalyzerAction>, SymbolDeclaredCompilationEvent>);
				object gate = this._gate;
				bool flag2;
				lock (gate)
				{
					HashSet<ISymbol> hashSet;
					if (this._lazyPendingMemberSymbolsMap == null || !this._lazyPendingMemberSymbolsMap.TryGetValue(containingSymbol, ref hashSet))
					{
						flag2 = false;
					}
					else
					{
						hashSet.Remove(processedMemberSymbol);
						if (hashSet.Count > 0 || this._lazyPendingSymbolEndActionsMap == null || !this._lazyPendingSymbolEndActionsMap.TryGetValue(containingSymbol, ref containerEndActionsAndEvent))
						{
							flag2 = false;
						}
						else
						{
							this._lazyPendingSymbolEndActionsMap.Remove(containingSymbol);
							flag2 = true;
						}
					}
				}
				return flag2;
			}

			// Token: 0x06003AA4 RID: 15012 RVA: 0x000A4E78 File Offset: 0x000A3078
			public bool TryStartExecuteSymbolEndActions([Nullable(new byte[] { 0, 1 })] ImmutableArray<SymbolEndAnalyzerAction> symbolEndActions, SymbolDeclaredCompilationEvent symbolDeclaredEvent)
			{
				ISymbol symbol = symbolDeclaredEvent.Symbol;
				object gate = this._gate;
				bool flag2;
				lock (gate)
				{
					HashSet<ISymbol> hashSet;
					if (this._lazyPendingMemberSymbolsMap.TryGetValue(symbol, ref hashSet) && hashSet != null && hashSet.Count > 0)
					{
						this.MarkSymbolEndAnalysisPending_NoLock(symbol, symbolEndActions, symbolDeclaredEvent);
						flag2 = false;
					}
					else
					{
						Dictionary<ISymbol, ValueTuple<ImmutableArray<SymbolEndAnalyzerAction>, SymbolDeclaredCompilationEvent>> lazyPendingSymbolEndActionsMap = this._lazyPendingSymbolEndActionsMap;
						if (lazyPendingSymbolEndActionsMap != null)
						{
							lazyPendingSymbolEndActionsMap.Remove(symbol);
						}
						flag2 = true;
					}
				}
				return flag2;
			}

			// Token: 0x06003AA5 RID: 15013 RVA: 0x000A4EFC File Offset: 0x000A30FC
			public void MarkSymbolEndAnalysisComplete(ISymbol symbol)
			{
				object gate = this._gate;
				lock (gate)
				{
					Dictionary<ISymbol, HashSet<ISymbol>> lazyPendingMemberSymbolsMap = this._lazyPendingMemberSymbolsMap;
					if (lazyPendingMemberSymbolsMap != null)
					{
						lazyPendingMemberSymbolsMap.Remove(symbol);
					}
				}
			}

			// Token: 0x06003AA6 RID: 15014 RVA: 0x000A4F4C File Offset: 0x000A314C
			public void MarkSymbolEndAnalysisPending(ISymbol symbol, [Nullable(new byte[] { 0, 1 })] ImmutableArray<SymbolEndAnalyzerAction> symbolEndActions, SymbolDeclaredCompilationEvent symbolDeclaredEvent)
			{
				object gate = this._gate;
				lock (gate)
				{
					this.MarkSymbolEndAnalysisPending_NoLock(symbol, symbolEndActions, symbolDeclaredEvent);
				}
			}

			// Token: 0x06003AA7 RID: 15015 RVA: 0x000A4F90 File Offset: 0x000A3190
			private void MarkSymbolEndAnalysisPending_NoLock(ISymbol symbol, [Nullable(new byte[] { 0, 1 })] ImmutableArray<SymbolEndAnalyzerAction> symbolEndActions, SymbolDeclaredCompilationEvent symbolDeclaredEvent)
			{
				if (this._lazyPendingSymbolEndActionsMap == null)
				{
					this._lazyPendingSymbolEndActionsMap = new Dictionary<ISymbol, ValueTuple<ImmutableArray<SymbolEndAnalyzerAction>, SymbolDeclaredCompilationEvent>>();
				}
				this._lazyPendingSymbolEndActionsMap[symbol] = new ValueTuple<ImmutableArray<SymbolEndAnalyzerAction>, SymbolDeclaredCompilationEvent>(symbolEndActions, symbolDeclaredEvent);
			}

			// Token: 0x06003AA8 RID: 15016 RVA: 0x000A4FB8 File Offset: 0x000A31B8
			[Conditional("DEBUG")]
			public void VerifyAllSymbolEndActionsExecuted()
			{
				object gate = this._gate;
				lock (gate)
				{
				}
			}

			// Token: 0x06003AA9 RID: 15017 RVA: 0x000A4FF4 File Offset: 0x000A31F4
			[CompilerGenerated]
			internal static Task<HostSessionStartAnalysisScope> <GetSessionAnalysisScopeAsync>g__getSessionAnalysisScopeTaskSlow|10_0(AnalyzerManager.AnalyzerExecutionContext context, AnalyzerExecutor executor)
			{
				return Task.Run<HostSessionStartAnalysisScope>(delegate
				{
					HostSessionStartAnalysisScope hostSessionStartAnalysisScope = new HostSessionStartAnalysisScope();
					executor.ExecuteInitializeMethod(context._analyzer, hostSessionStartAnalysisScope);
					return hostSessionStartAnalysisScope;
				}, executor.CancellationToken);
			}

			// Token: 0x04001AB6 RID: 6838
			private readonly DiagnosticAnalyzer _analyzer;

			// Token: 0x04001AB7 RID: 6839
			private readonly object _gate;

			// Token: 0x04001AB8 RID: 6840
			[Nullable(new byte[] { 2, 1, 2, 1 })]
			private Dictionary<ISymbol, HashSet<ISymbol>> _lazyPendingMemberSymbolsMap;

			// Token: 0x04001AB9 RID: 6841
			[Nullable(new byte[] { 2, 1, 0, 0, 1, 1 })]
			private Dictionary<ISymbol, ValueTuple<ImmutableArray<SymbolEndAnalyzerAction>, SymbolDeclaredCompilationEvent>> _lazyPendingSymbolEndActionsMap;

			// Token: 0x04001ABA RID: 6842
			[Nullable(new byte[] { 2, 1 })]
			private Task<HostSessionStartAnalysisScope> _lazySessionScopeTask;

			// Token: 0x04001ABB RID: 6843
			[Nullable(new byte[] { 2, 1 })]
			private Task<HostCompilationStartAnalysisScope> _lazyCompilationScopeTask;

			// Token: 0x04001ABC RID: 6844
			[Nullable(new byte[] { 2, 1, 1, 1 })]
			private Dictionary<ISymbol, Task<HostSymbolStartAnalysisScope>> _lazySymbolScopeTasks;

			// Token: 0x04001ABD RID: 6845
			[Nullable(new byte[] { 0, 1 })]
			private ImmutableArray<DiagnosticDescriptor> _lazyDiagnosticDescriptors;

			// Token: 0x04001ABE RID: 6846
			[Nullable(new byte[] { 0, 1 })]
			private ImmutableArray<SuppressionDescriptor> _lazySuppressionDescriptors;
		}
	}
}
