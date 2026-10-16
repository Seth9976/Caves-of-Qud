using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000320 RID: 800
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class CodeBlockStartAnalysisContext<[Nullable(0)] TLanguageKindEnum> where TLanguageKindEnum : struct
	{
		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06002322 RID: 8994 RVA: 0x0006F2F7 File Offset: 0x0006D4F7
		public SyntaxNode CodeBlock
		{
			get
			{
				return this._codeBlock;
			}
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06002323 RID: 8995 RVA: 0x0006F2FF File Offset: 0x0006D4FF
		public ISymbol OwningSymbol
		{
			get
			{
				return this._owningSymbol;
			}
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06002324 RID: 8996 RVA: 0x0006F307 File Offset: 0x0006D507
		public SemanticModel SemanticModel
		{
			get
			{
				return this._semanticModel;
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06002325 RID: 8997 RVA: 0x0006F30F File Offset: 0x0006D50F
		public AnalyzerOptions Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06002326 RID: 8998 RVA: 0x0006F317 File Offset: 0x0006D517
		public CancellationToken CancellationToken
		{
			get
			{
				return this._cancellationToken;
			}
		}

		// Token: 0x06002327 RID: 8999 RVA: 0x0006F31F File Offset: 0x0006D51F
		protected CodeBlockStartAnalysisContext(SyntaxNode codeBlock, ISymbol owningSymbol, SemanticModel semanticModel, AnalyzerOptions options, CancellationToken cancellationToken)
		{
			this._codeBlock = codeBlock;
			this._owningSymbol = owningSymbol;
			this._semanticModel = semanticModel;
			this._options = options;
			this._cancellationToken = cancellationToken;
		}

		// Token: 0x06002328 RID: 9000
		public abstract void RegisterCodeBlockEndAction(Action<CodeBlockAnalysisContext> action);

		// Token: 0x06002329 RID: 9001 RVA: 0x0006F34C File Offset: 0x0006D54C
		public void RegisterSyntaxNodeAction(Action<SyntaxNodeAnalysisContext> action, [Nullable(new byte[] { 1, 0 })] params TLanguageKindEnum[] syntaxKinds)
		{
			this.RegisterSyntaxNodeAction(action, syntaxKinds.AsImmutableOrEmpty<TLanguageKindEnum>());
		}

		// Token: 0x0600232A RID: 9002
		[NullableContext(0)]
		public abstract void RegisterSyntaxNodeAction([Nullable(1)] Action<SyntaxNodeAnalysisContext> action, ImmutableArray<TLanguageKindEnum> syntaxKinds);

		// Token: 0x04000F04 RID: 3844
		private readonly SyntaxNode _codeBlock;

		// Token: 0x04000F05 RID: 3845
		private readonly ISymbol _owningSymbol;

		// Token: 0x04000F06 RID: 3846
		private readonly SemanticModel _semanticModel;

		// Token: 0x04000F07 RID: 3847
		private readonly AnalyzerOptions _options;

		// Token: 0x04000F08 RID: 3848
		private readonly CancellationToken _cancellationToken;
	}
}
