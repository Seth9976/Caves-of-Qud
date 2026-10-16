using System;

namespace Microsoft.CodeAnalysis.Diagnostics.Telemetry
{
	// Token: 0x02000357 RID: 855
	internal class AnalyzerActionCounts
	{
		// Token: 0x06002488 RID: 9352 RVA: 0x00071DF4 File Offset: 0x0006FFF4
		internal AnalyzerActionCounts(in AnalyzerActions analyzerActions)
			: this(analyzerActions.CompilationStartActionsCount, analyzerActions.CompilationEndActionsCount, analyzerActions.CompilationActionsCount, analyzerActions.SyntaxTreeActionsCount, analyzerActions.AdditionalFileActionsCount, analyzerActions.SemanticModelActionsCount, analyzerActions.SymbolActionsCount, analyzerActions.SymbolStartActionsCount, analyzerActions.SymbolEndActionsCount, analyzerActions.SyntaxNodeActionsCount, analyzerActions.CodeBlockStartActionsCount, analyzerActions.CodeBlockEndActionsCount, analyzerActions.CodeBlockActionsCount, analyzerActions.OperationActionsCount, analyzerActions.OperationBlockStartActionsCount, analyzerActions.OperationBlockEndActionsCount, analyzerActions.OperationBlockActionsCount, analyzerActions.Concurrent)
		{
		}

		// Token: 0x06002489 RID: 9353 RVA: 0x00071E74 File Offset: 0x00070074
		internal AnalyzerActionCounts(int compilationStartActionsCount, int compilationEndActionsCount, int compilationActionsCount, int syntaxTreeActionsCount, int additionalFileActionsCount, int semanticModelActionsCount, int symbolActionsCount, int symbolStartActionsCount, int symbolEndActionsCount, int syntaxNodeActionsCount, int codeBlockStartActionsCount, int codeBlockEndActionsCount, int codeBlockActionsCount, int operationActionsCount, int operationBlockStartActionsCount, int operationBlockEndActionsCount, int operationBlockActionsCount, bool concurrent)
		{
			this.CompilationStartActionsCount = compilationStartActionsCount;
			this.CompilationEndActionsCount = compilationEndActionsCount;
			this.CompilationActionsCount = compilationActionsCount;
			this.SyntaxTreeActionsCount = syntaxTreeActionsCount;
			this.AdditionalFileActionsCount = additionalFileActionsCount;
			this.SemanticModelActionsCount = semanticModelActionsCount;
			this.SymbolActionsCount = symbolActionsCount;
			this.SymbolStartActionsCount = symbolStartActionsCount;
			this.SymbolEndActionsCount = symbolEndActionsCount;
			this.SyntaxNodeActionsCount = syntaxNodeActionsCount;
			this.CodeBlockStartActionsCount = codeBlockStartActionsCount;
			this.CodeBlockEndActionsCount = codeBlockEndActionsCount;
			this.CodeBlockActionsCount = codeBlockActionsCount;
			this.OperationActionsCount = operationActionsCount;
			this.OperationBlockStartActionsCount = operationBlockStartActionsCount;
			this.OperationBlockEndActionsCount = operationBlockEndActionsCount;
			this.OperationBlockActionsCount = operationBlockActionsCount;
			this.Concurrent = concurrent;
			this.HasAnyExecutableCodeActions = this.CodeBlockActionsCount > 0 || this.CodeBlockStartActionsCount > 0 || this.SyntaxNodeActionsCount > 0 || this.OperationActionsCount > 0 || this.OperationBlockActionsCount > 0 || this.OperationBlockStartActionsCount > 0 || this.SymbolStartActionsCount > 0;
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x0600248A RID: 9354 RVA: 0x00071F5C File Offset: 0x0007015C
		public int CompilationStartActionsCount { get; }

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x0600248B RID: 9355 RVA: 0x00071F64 File Offset: 0x00070164
		public int CompilationEndActionsCount { get; }

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x0600248C RID: 9356 RVA: 0x00071F6C File Offset: 0x0007016C
		public int CompilationActionsCount { get; }

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x0600248D RID: 9357 RVA: 0x00071F74 File Offset: 0x00070174
		public int SyntaxTreeActionsCount { get; }

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x0600248E RID: 9358 RVA: 0x00071F7C File Offset: 0x0007017C
		public int AdditionalFileActionsCount { get; }

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x0600248F RID: 9359 RVA: 0x00071F84 File Offset: 0x00070184
		public int SemanticModelActionsCount { get; }

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06002490 RID: 9360 RVA: 0x00071F8C File Offset: 0x0007018C
		public int SymbolActionsCount { get; }

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06002491 RID: 9361 RVA: 0x00071F94 File Offset: 0x00070194
		public int SymbolStartActionsCount { get; }

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06002492 RID: 9362 RVA: 0x00071F9C File Offset: 0x0007019C
		public int SymbolEndActionsCount { get; }

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06002493 RID: 9363 RVA: 0x00071FA4 File Offset: 0x000701A4
		public int SyntaxNodeActionsCount { get; }

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06002494 RID: 9364 RVA: 0x00071FAC File Offset: 0x000701AC
		public int CodeBlockStartActionsCount { get; }

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06002495 RID: 9365 RVA: 0x00071FB4 File Offset: 0x000701B4
		public int CodeBlockEndActionsCount { get; }

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06002496 RID: 9366 RVA: 0x00071FBC File Offset: 0x000701BC
		public int CodeBlockActionsCount { get; }

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06002497 RID: 9367 RVA: 0x00071FC4 File Offset: 0x000701C4
		public int OperationActionsCount { get; }

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06002498 RID: 9368 RVA: 0x00071FCC File Offset: 0x000701CC
		public int OperationBlockStartActionsCount { get; }

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06002499 RID: 9369 RVA: 0x00071FD4 File Offset: 0x000701D4
		public int OperationBlockEndActionsCount { get; }

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x0600249A RID: 9370 RVA: 0x00071FDC File Offset: 0x000701DC
		public int OperationBlockActionsCount { get; }

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x0600249B RID: 9371 RVA: 0x00071FE4 File Offset: 0x000701E4
		public bool HasAnyExecutableCodeActions { get; }

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x0600249C RID: 9372 RVA: 0x00071FEC File Offset: 0x000701EC
		public bool Concurrent { get; }

		// Token: 0x04000F99 RID: 3993
		internal static readonly AnalyzerActionCounts Empty = new AnalyzerActionCounts(in AnalyzerActions.Empty);
	}
}
