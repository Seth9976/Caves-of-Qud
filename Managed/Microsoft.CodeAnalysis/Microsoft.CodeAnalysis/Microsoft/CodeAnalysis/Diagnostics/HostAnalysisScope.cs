using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000347 RID: 839
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class HostAnalysisScope
	{
		// Token: 0x060023EE RID: 9198 RVA: 0x00070650 File Offset: 0x0006E850
		public virtual AnalyzerActions GetAnalyzerActions(DiagnosticAnalyzer analyzer)
		{
			return this.GetOrCreateAnalyzerActions(analyzer).Value;
		}

		// Token: 0x060023EF RID: 9199 RVA: 0x00070660 File Offset: 0x0006E860
		public void RegisterCompilationAction(DiagnosticAnalyzer analyzer, Action<CompilationAnalysisContext> action)
		{
			CompilationAnalyzerAction compilationAnalyzerAction = new CompilationAnalyzerAction(action, analyzer);
			this.GetOrCreateAnalyzerActions(analyzer).Value.AddCompilationAction(compilationAnalyzerAction);
		}

		// Token: 0x060023F0 RID: 9200 RVA: 0x00070688 File Offset: 0x0006E888
		public void RegisterCompilationEndAction(DiagnosticAnalyzer analyzer, Action<CompilationAnalysisContext> action)
		{
			CompilationAnalyzerAction compilationAnalyzerAction = new CompilationAnalyzerAction(action, analyzer);
			this.GetOrCreateAnalyzerActions(analyzer).Value.AddCompilationEndAction(compilationAnalyzerAction);
		}

		// Token: 0x060023F1 RID: 9201 RVA: 0x000706B0 File Offset: 0x0006E8B0
		public void RegisterSemanticModelAction(DiagnosticAnalyzer analyzer, Action<SemanticModelAnalysisContext> action)
		{
			SemanticModelAnalyzerAction semanticModelAnalyzerAction = new SemanticModelAnalyzerAction(action, analyzer);
			this.GetOrCreateAnalyzerActions(analyzer).Value.AddSemanticModelAction(semanticModelAnalyzerAction);
		}

		// Token: 0x060023F2 RID: 9202 RVA: 0x000706D8 File Offset: 0x0006E8D8
		public void RegisterSyntaxTreeAction(DiagnosticAnalyzer analyzer, Action<SyntaxTreeAnalysisContext> action)
		{
			SyntaxTreeAnalyzerAction syntaxTreeAnalyzerAction = new SyntaxTreeAnalyzerAction(action, analyzer);
			this.GetOrCreateAnalyzerActions(analyzer).Value.AddSyntaxTreeAction(syntaxTreeAnalyzerAction);
		}

		// Token: 0x060023F3 RID: 9203 RVA: 0x00070700 File Offset: 0x0006E900
		public void RegisterAdditionalFileAction(DiagnosticAnalyzer analyzer, Action<AdditionalFileAnalysisContext> action)
		{
			AdditionalFileAnalyzerAction additionalFileAnalyzerAction = new AdditionalFileAnalyzerAction(action, analyzer);
			this.GetOrCreateAnalyzerActions(analyzer).Value.AddAdditionalFileAction(additionalFileAnalyzerAction);
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x00070728 File Offset: 0x0006E928
		public void RegisterSymbolAction(DiagnosticAnalyzer analyzer, Action<SymbolAnalysisContext> action, [Nullable(0)] ImmutableArray<SymbolKind> symbolKinds)
		{
			SymbolAnalyzerAction symbolAnalyzerAction = new SymbolAnalyzerAction(action, symbolKinds, analyzer);
			this.GetOrCreateAnalyzerActions(analyzer).Value.AddSymbolAction(symbolAnalyzerAction);
			if (symbolKinds.Contains(SymbolKind.Parameter))
			{
				this.RegisterSymbolAction(analyzer, delegate(SymbolAnalysisContext context)
				{
					SymbolKind kind = context.Symbol.Kind;
					ImmutableArray<IParameterSymbol> immutableArray;
					if (kind != SymbolKind.Method)
					{
						if (kind != SymbolKind.NamedType)
						{
							if (kind != SymbolKind.Property)
							{
								throw new ArgumentException(string.Format("{0} is not supported.", context.Symbol.Kind), "context");
							}
							immutableArray = ((IPropertySymbol)context.Symbol).Parameters;
						}
						else
						{
							IMethodSymbol delegateInvokeMethod = ((INamedTypeSymbol)context.Symbol).DelegateInvokeMethod;
							immutableArray = ((delegateInvokeMethod != null) ? delegateInvokeMethod.Parameters : ImmutableArray.Create<IParameterSymbol>());
						}
					}
					else
					{
						immutableArray = ((IMethodSymbol)context.Symbol).Parameters;
					}
					foreach (IParameterSymbol parameterSymbol in immutableArray)
					{
						if (!parameterSymbol.IsImplicitlyDeclared)
						{
							action.Invoke(new SymbolAnalysisContext(parameterSymbol, context.Compilation, context.Options, new Action<Diagnostic>(context.ReportDiagnostic), context.IsSupportedDiagnostic, context.CancellationToken));
						}
					}
				}, ImmutableArray.Create<SymbolKind>(SymbolKind.Method, SymbolKind.Property, SymbolKind.NamedType));
			}
		}

		// Token: 0x060023F5 RID: 9205 RVA: 0x0007078C File Offset: 0x0006E98C
		public void RegisterSymbolStartAction(DiagnosticAnalyzer analyzer, Action<SymbolStartAnalysisContext> action, SymbolKind symbolKind)
		{
			SymbolStartAnalyzerAction symbolStartAnalyzerAction = new SymbolStartAnalyzerAction(action, symbolKind, analyzer);
			this.GetOrCreateAnalyzerActions(analyzer).Value.AddSymbolStartAction(symbolStartAnalyzerAction);
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x000707B4 File Offset: 0x0006E9B4
		public void RegisterSymbolEndAction(DiagnosticAnalyzer analyzer, Action<SymbolAnalysisContext> action)
		{
			SymbolEndAnalyzerAction symbolEndAnalyzerAction = new SymbolEndAnalyzerAction(action, analyzer);
			this.GetOrCreateAnalyzerActions(analyzer).Value.AddSymbolEndAction(symbolEndAnalyzerAction);
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x000707DC File Offset: 0x0006E9DC
		[NullableContext(0)]
		public void RegisterCodeBlockStartAction<TLanguageKindEnum>([Nullable(1)] DiagnosticAnalyzer analyzer, [Nullable(new byte[] { 1, 1, 0 })] Action<CodeBlockStartAnalysisContext<TLanguageKindEnum>> action) where TLanguageKindEnum : struct
		{
			CodeBlockStartAnalyzerAction<TLanguageKindEnum> codeBlockStartAnalyzerAction = new CodeBlockStartAnalyzerAction<TLanguageKindEnum>(action, analyzer);
			this.GetOrCreateAnalyzerActions(analyzer).Value.AddCodeBlockStartAction<TLanguageKindEnum>(codeBlockStartAnalyzerAction);
		}

		// Token: 0x060023F8 RID: 9208 RVA: 0x00070804 File Offset: 0x0006EA04
		public void RegisterCodeBlockEndAction(DiagnosticAnalyzer analyzer, Action<CodeBlockAnalysisContext> action)
		{
			CodeBlockAnalyzerAction codeBlockAnalyzerAction = new CodeBlockAnalyzerAction(action, analyzer);
			this.GetOrCreateAnalyzerActions(analyzer).Value.AddCodeBlockEndAction(codeBlockAnalyzerAction);
		}

		// Token: 0x060023F9 RID: 9209 RVA: 0x0007082C File Offset: 0x0006EA2C
		public void RegisterCodeBlockAction(DiagnosticAnalyzer analyzer, Action<CodeBlockAnalysisContext> action)
		{
			CodeBlockAnalyzerAction codeBlockAnalyzerAction = new CodeBlockAnalyzerAction(action, analyzer);
			this.GetOrCreateAnalyzerActions(analyzer).Value.AddCodeBlockAction(codeBlockAnalyzerAction);
		}

		// Token: 0x060023FA RID: 9210 RVA: 0x00070854 File Offset: 0x0006EA54
		[NullableContext(0)]
		public void RegisterSyntaxNodeAction<TLanguageKindEnum>([Nullable(1)] DiagnosticAnalyzer analyzer, [Nullable(1)] Action<SyntaxNodeAnalysisContext> action, ImmutableArray<TLanguageKindEnum> syntaxKinds) where TLanguageKindEnum : struct
		{
			SyntaxNodeAnalyzerAction<TLanguageKindEnum> syntaxNodeAnalyzerAction = new SyntaxNodeAnalyzerAction<TLanguageKindEnum>(action, syntaxKinds, analyzer);
			this.GetOrCreateAnalyzerActions(analyzer).Value.AddSyntaxNodeAction<TLanguageKindEnum>(syntaxNodeAnalyzerAction);
		}

		// Token: 0x060023FB RID: 9211 RVA: 0x0007087C File Offset: 0x0006EA7C
		public void RegisterOperationBlockStartAction(DiagnosticAnalyzer analyzer, Action<OperationBlockStartAnalysisContext> action)
		{
			OperationBlockStartAnalyzerAction operationBlockStartAnalyzerAction = new OperationBlockStartAnalyzerAction(action, analyzer);
			this.GetOrCreateAnalyzerActions(analyzer).Value.AddOperationBlockStartAction(operationBlockStartAnalyzerAction);
		}

		// Token: 0x060023FC RID: 9212 RVA: 0x000708A4 File Offset: 0x0006EAA4
		public void RegisterOperationBlockEndAction(DiagnosticAnalyzer analyzer, Action<OperationBlockAnalysisContext> action)
		{
			OperationBlockAnalyzerAction operationBlockAnalyzerAction = new OperationBlockAnalyzerAction(action, analyzer);
			this.GetOrCreateAnalyzerActions(analyzer).Value.AddOperationBlockEndAction(operationBlockAnalyzerAction);
		}

		// Token: 0x060023FD RID: 9213 RVA: 0x000708CC File Offset: 0x0006EACC
		public void RegisterOperationBlockAction(DiagnosticAnalyzer analyzer, Action<OperationBlockAnalysisContext> action)
		{
			OperationBlockAnalyzerAction operationBlockAnalyzerAction = new OperationBlockAnalyzerAction(action, analyzer);
			this.GetOrCreateAnalyzerActions(analyzer).Value.AddOperationBlockAction(operationBlockAnalyzerAction);
		}

		// Token: 0x060023FE RID: 9214 RVA: 0x000708F4 File Offset: 0x0006EAF4
		public void RegisterOperationAction(DiagnosticAnalyzer analyzer, Action<OperationAnalysisContext> action, [Nullable(0)] ImmutableArray<OperationKind> operationKinds)
		{
			OperationAnalyzerAction operationAnalyzerAction = new OperationAnalyzerAction(action, operationKinds, analyzer);
			this.GetOrCreateAnalyzerActions(analyzer).Value.AddOperationAction(operationAnalyzerAction);
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x0007091C File Offset: 0x0006EB1C
		protected StrongBox<AnalyzerActions> GetOrCreateAnalyzerActions(DiagnosticAnalyzer analyzer)
		{
			return this._analyzerActions.GetOrAdd(analyzer, (DiagnosticAnalyzer _) => new StrongBox<AnalyzerActions>(AnalyzerActions.Empty));
		}

		// Token: 0x04000F66 RID: 3942
		private readonly ConcurrentDictionary<DiagnosticAnalyzer, StrongBox<AnalyzerActions>> _analyzerActions = new ConcurrentDictionary<DiagnosticAnalyzer, StrongBox<AnalyzerActions>>();
	}
}
