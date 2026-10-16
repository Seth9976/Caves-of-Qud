using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000345 RID: 837
	internal sealed class HostCodeBlockStartAnalysisScope<TLanguageKindEnum> where TLanguageKindEnum : struct
	{
		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x060023E4 RID: 9188 RVA: 0x0007058A File Offset: 0x0006E78A
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<CodeBlockAnalyzerAction> CodeBlockEndActions
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._codeBlockEndActions;
			}
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x060023E5 RID: 9189 RVA: 0x00070592 File Offset: 0x0006E792
		[Nullable(new byte[] { 0, 1, 0 })]
		public ImmutableArray<SyntaxNodeAnalyzerAction<TLanguageKindEnum>> SyntaxNodeActions
		{
			[return: Nullable(new byte[] { 0, 1, 0 })]
			get
			{
				return this._syntaxNodeActions;
			}
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x0007059A File Offset: 0x0006E79A
		internal HostCodeBlockStartAnalysisScope()
		{
		}

		// Token: 0x060023E7 RID: 9191 RVA: 0x000705B8 File Offset: 0x0006E7B8
		[NullableContext(1)]
		public void RegisterCodeBlockEndAction(DiagnosticAnalyzer analyzer, Action<CodeBlockAnalysisContext> action)
		{
			this._codeBlockEndActions = this._codeBlockEndActions.Add(new CodeBlockAnalyzerAction(action, analyzer));
		}

		// Token: 0x060023E8 RID: 9192 RVA: 0x000705D2 File Offset: 0x0006E7D2
		[NullableContext(1)]
		public void RegisterSyntaxNodeAction(DiagnosticAnalyzer analyzer, Action<SyntaxNodeAnalysisContext> action, [Nullable(0)] ImmutableArray<TLanguageKindEnum> syntaxKinds)
		{
			this._syntaxNodeActions = this._syntaxNodeActions.Add(new SyntaxNodeAnalyzerAction<TLanguageKindEnum>(action, syntaxKinds, analyzer));
		}

		// Token: 0x04000F62 RID: 3938
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<CodeBlockAnalyzerAction> _codeBlockEndActions = ImmutableArray<CodeBlockAnalyzerAction>.Empty;

		// Token: 0x04000F63 RID: 3939
		[Nullable(new byte[] { 0, 1, 0 })]
		private ImmutableArray<SyntaxNodeAnalyzerAction<TLanguageKindEnum>> _syntaxNodeActions = ImmutableArray<SyntaxNodeAnalyzerAction<TLanguageKindEnum>>.Empty;
	}
}
