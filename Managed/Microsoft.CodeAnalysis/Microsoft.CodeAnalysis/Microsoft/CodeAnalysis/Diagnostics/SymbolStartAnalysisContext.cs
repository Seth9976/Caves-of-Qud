using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x0200031F RID: 799
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class SymbolStartAnalysisContext
	{
		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06002314 RID: 8980 RVA: 0x0006F294 File Offset: 0x0006D494
		public ISymbol Symbol { get; }

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06002315 RID: 8981 RVA: 0x0006F29C File Offset: 0x0006D49C
		public Compilation Compilation { get; }

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06002316 RID: 8982 RVA: 0x0006F2A4 File Offset: 0x0006D4A4
		public AnalyzerOptions Options { get; }

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06002317 RID: 8983 RVA: 0x0006F2AC File Offset: 0x0006D4AC
		public CancellationToken CancellationToken { get; }

		// Token: 0x06002318 RID: 8984 RVA: 0x0006F2B4 File Offset: 0x0006D4B4
		public SymbolStartAnalysisContext(ISymbol symbol, Compilation compilation, AnalyzerOptions options, CancellationToken cancellationToken)
		{
			this.Symbol = symbol;
			this.Compilation = compilation;
			this.Options = options;
			this.CancellationToken = cancellationToken;
		}

		// Token: 0x06002319 RID: 8985
		public abstract void RegisterSymbolEndAction(Action<SymbolAnalysisContext> action);

		// Token: 0x0600231A RID: 8986
		[NullableContext(0)]
		public abstract void RegisterCodeBlockStartAction<TLanguageKindEnum>([Nullable(new byte[] { 1, 1, 0 })] Action<CodeBlockStartAnalysisContext<TLanguageKindEnum>> action) where TLanguageKindEnum : struct;

		// Token: 0x0600231B RID: 8987
		public abstract void RegisterCodeBlockAction(Action<CodeBlockAnalysisContext> action);

		// Token: 0x0600231C RID: 8988 RVA: 0x0006F2D9 File Offset: 0x0006D4D9
		[NullableContext(0)]
		public void RegisterSyntaxNodeAction<TLanguageKindEnum>([Nullable(1)] Action<SyntaxNodeAnalysisContext> action, [Nullable(new byte[] { 1, 0 })] params TLanguageKindEnum[] syntaxKinds) where TLanguageKindEnum : struct
		{
			this.RegisterSyntaxNodeAction<TLanguageKindEnum>(action, syntaxKinds.AsImmutableOrEmpty<TLanguageKindEnum>());
		}

		// Token: 0x0600231D RID: 8989
		[NullableContext(0)]
		public abstract void RegisterSyntaxNodeAction<TLanguageKindEnum>([Nullable(1)] Action<SyntaxNodeAnalysisContext> action, ImmutableArray<TLanguageKindEnum> syntaxKinds) where TLanguageKindEnum : struct;

		// Token: 0x0600231E RID: 8990
		public abstract void RegisterOperationBlockStartAction(Action<OperationBlockStartAnalysisContext> action);

		// Token: 0x0600231F RID: 8991
		public abstract void RegisterOperationBlockAction(Action<OperationBlockAnalysisContext> action);

		// Token: 0x06002320 RID: 8992 RVA: 0x0006F2E8 File Offset: 0x0006D4E8
		public void RegisterOperationAction(Action<OperationAnalysisContext> action, params OperationKind[] operationKinds)
		{
			this.RegisterOperationAction(action, operationKinds.AsImmutableOrEmpty<OperationKind>());
		}

		// Token: 0x06002321 RID: 8993
		[NullableContext(0)]
		public abstract void RegisterOperationAction([Nullable(1)] Action<OperationAnalysisContext> action, ImmutableArray<OperationKind> operationKinds);
	}
}
