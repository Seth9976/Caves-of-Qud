using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000348 RID: 840
	[NullableContext(1)]
	[Nullable(0)]
	internal struct AnalyzerActions
	{
		// Token: 0x06002401 RID: 9217 RVA: 0x0007095C File Offset: 0x0006EB5C
		internal AnalyzerActions(bool concurrent)
		{
			this._compilationStartActions = ImmutableArray<CompilationStartAnalyzerAction>.Empty;
			this._compilationEndActions = ImmutableArray<CompilationAnalyzerAction>.Empty;
			this._compilationActions = ImmutableArray<CompilationAnalyzerAction>.Empty;
			this._syntaxTreeActions = ImmutableArray<SyntaxTreeAnalyzerAction>.Empty;
			this._additionalFileActions = ImmutableArray<AdditionalFileAnalyzerAction>.Empty;
			this._semanticModelActions = ImmutableArray<SemanticModelAnalyzerAction>.Empty;
			this._symbolActions = ImmutableArray<SymbolAnalyzerAction>.Empty;
			this._symbolStartActions = ImmutableArray<SymbolStartAnalyzerAction>.Empty;
			this._symbolEndActions = ImmutableArray<SymbolEndAnalyzerAction>.Empty;
			this._codeBlockStartActions = ImmutableArray<AnalyzerAction>.Empty;
			this._codeBlockEndActions = ImmutableArray<CodeBlockAnalyzerAction>.Empty;
			this._codeBlockActions = ImmutableArray<CodeBlockAnalyzerAction>.Empty;
			this._operationBlockStartActions = ImmutableArray<OperationBlockStartAnalyzerAction>.Empty;
			this._operationBlockEndActions = ImmutableArray<OperationBlockAnalyzerAction>.Empty;
			this._operationBlockActions = ImmutableArray<OperationBlockAnalyzerAction>.Empty;
			this._syntaxNodeActions = ImmutableArray<AnalyzerAction>.Empty;
			this._operationActions = ImmutableArray<OperationAnalyzerAction>.Empty;
			this._concurrent = concurrent;
			this.IsEmpty = true;
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x00070A34 File Offset: 0x0006EC34
		public AnalyzerActions([Nullable(new byte[] { 0, 1 })] ImmutableArray<CompilationStartAnalyzerAction> compilationStartActions, [Nullable(new byte[] { 0, 1 })] ImmutableArray<CompilationAnalyzerAction> compilationEndActions, [Nullable(new byte[] { 0, 1 })] ImmutableArray<CompilationAnalyzerAction> compilationActions, [Nullable(new byte[] { 0, 1 })] ImmutableArray<SyntaxTreeAnalyzerAction> syntaxTreeActions, [Nullable(new byte[] { 0, 1 })] ImmutableArray<AdditionalFileAnalyzerAction> additionalFileActions, [Nullable(new byte[] { 0, 1 })] ImmutableArray<SemanticModelAnalyzerAction> semanticModelActions, [Nullable(new byte[] { 0, 1 })] ImmutableArray<SymbolAnalyzerAction> symbolActions, [Nullable(new byte[] { 0, 1 })] ImmutableArray<SymbolStartAnalyzerAction> symbolStartActions, [Nullable(new byte[] { 0, 1 })] ImmutableArray<SymbolEndAnalyzerAction> symbolEndActions, [Nullable(new byte[] { 0, 1 })] ImmutableArray<AnalyzerAction> codeBlockStartActions, [Nullable(new byte[] { 0, 1 })] ImmutableArray<CodeBlockAnalyzerAction> codeBlockEndActions, [Nullable(new byte[] { 0, 1 })] ImmutableArray<CodeBlockAnalyzerAction> codeBlockActions, [Nullable(new byte[] { 0, 1 })] ImmutableArray<OperationBlockStartAnalyzerAction> operationBlockStartActions, [Nullable(new byte[] { 0, 1 })] ImmutableArray<OperationBlockAnalyzerAction> operationBlockEndActions, [Nullable(new byte[] { 0, 1 })] ImmutableArray<OperationBlockAnalyzerAction> operationBlockActions, [Nullable(new byte[] { 0, 1 })] ImmutableArray<AnalyzerAction> syntaxNodeActions, [Nullable(new byte[] { 0, 1 })] ImmutableArray<OperationAnalyzerAction> operationActions, bool concurrent, bool isEmpty)
		{
			this._compilationStartActions = compilationStartActions;
			this._compilationEndActions = compilationEndActions;
			this._compilationActions = compilationActions;
			this._syntaxTreeActions = syntaxTreeActions;
			this._additionalFileActions = additionalFileActions;
			this._semanticModelActions = semanticModelActions;
			this._symbolActions = symbolActions;
			this._symbolStartActions = symbolStartActions;
			this._symbolEndActions = symbolEndActions;
			this._codeBlockStartActions = codeBlockStartActions;
			this._codeBlockEndActions = codeBlockEndActions;
			this._codeBlockActions = codeBlockActions;
			this._operationBlockStartActions = operationBlockStartActions;
			this._operationBlockEndActions = operationBlockEndActions;
			this._operationBlockActions = operationBlockActions;
			this._syntaxNodeActions = syntaxNodeActions;
			this._operationActions = operationActions;
			this._concurrent = concurrent;
			this.IsEmpty = isEmpty;
		}

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06002403 RID: 9219 RVA: 0x00070AD8 File Offset: 0x0006ECD8
		public readonly int CompilationStartActionsCount
		{
			get
			{
				ImmutableArray<CompilationStartAnalyzerAction> compilationStartActions = this._compilationStartActions;
				return compilationStartActions.Length;
			}
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x06002404 RID: 9220 RVA: 0x00070AF4 File Offset: 0x0006ECF4
		public readonly int CompilationEndActionsCount
		{
			get
			{
				ImmutableArray<CompilationAnalyzerAction> compilationEndActions = this._compilationEndActions;
				return compilationEndActions.Length;
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06002405 RID: 9221 RVA: 0x00070B10 File Offset: 0x0006ED10
		public readonly int CompilationActionsCount
		{
			get
			{
				ImmutableArray<CompilationAnalyzerAction> compilationActions = this._compilationActions;
				return compilationActions.Length;
			}
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06002406 RID: 9222 RVA: 0x00070B2C File Offset: 0x0006ED2C
		public readonly int SyntaxTreeActionsCount
		{
			get
			{
				ImmutableArray<SyntaxTreeAnalyzerAction> syntaxTreeActions = this._syntaxTreeActions;
				return syntaxTreeActions.Length;
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06002407 RID: 9223 RVA: 0x00070B48 File Offset: 0x0006ED48
		public readonly int AdditionalFileActionsCount
		{
			get
			{
				ImmutableArray<AdditionalFileAnalyzerAction> additionalFileActions = this._additionalFileActions;
				return additionalFileActions.Length;
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06002408 RID: 9224 RVA: 0x00070B64 File Offset: 0x0006ED64
		public readonly int SemanticModelActionsCount
		{
			get
			{
				ImmutableArray<SemanticModelAnalyzerAction> semanticModelActions = this._semanticModelActions;
				return semanticModelActions.Length;
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06002409 RID: 9225 RVA: 0x00070B80 File Offset: 0x0006ED80
		public readonly int SymbolActionsCount
		{
			get
			{
				ImmutableArray<SymbolAnalyzerAction> symbolActions = this._symbolActions;
				return symbolActions.Length;
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x0600240A RID: 9226 RVA: 0x00070B9C File Offset: 0x0006ED9C
		public readonly int SymbolStartActionsCount
		{
			get
			{
				ImmutableArray<SymbolStartAnalyzerAction> symbolStartActions = this._symbolStartActions;
				return symbolStartActions.Length;
			}
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x0600240B RID: 9227 RVA: 0x00070BB8 File Offset: 0x0006EDB8
		public readonly int SymbolEndActionsCount
		{
			get
			{
				ImmutableArray<SymbolEndAnalyzerAction> symbolEndActions = this._symbolEndActions;
				return symbolEndActions.Length;
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x0600240C RID: 9228 RVA: 0x00070BD4 File Offset: 0x0006EDD4
		public readonly int SyntaxNodeActionsCount
		{
			get
			{
				ImmutableArray<AnalyzerAction> syntaxNodeActions = this._syntaxNodeActions;
				return syntaxNodeActions.Length;
			}
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x0600240D RID: 9229 RVA: 0x00070BF0 File Offset: 0x0006EDF0
		public readonly int OperationActionsCount
		{
			get
			{
				ImmutableArray<OperationAnalyzerAction> operationActions = this._operationActions;
				return operationActions.Length;
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x0600240E RID: 9230 RVA: 0x00070C0C File Offset: 0x0006EE0C
		public readonly int OperationBlockStartActionsCount
		{
			get
			{
				ImmutableArray<OperationBlockStartAnalyzerAction> operationBlockStartActions = this._operationBlockStartActions;
				return operationBlockStartActions.Length;
			}
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x0600240F RID: 9231 RVA: 0x00070C28 File Offset: 0x0006EE28
		public readonly int OperationBlockEndActionsCount
		{
			get
			{
				ImmutableArray<OperationBlockAnalyzerAction> operationBlockEndActions = this._operationBlockEndActions;
				return operationBlockEndActions.Length;
			}
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x06002410 RID: 9232 RVA: 0x00070C44 File Offset: 0x0006EE44
		public readonly int OperationBlockActionsCount
		{
			get
			{
				ImmutableArray<OperationBlockAnalyzerAction> operationBlockActions = this._operationBlockActions;
				return operationBlockActions.Length;
			}
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06002411 RID: 9233 RVA: 0x00070C60 File Offset: 0x0006EE60
		public readonly int CodeBlockStartActionsCount
		{
			get
			{
				ImmutableArray<AnalyzerAction> codeBlockStartActions = this._codeBlockStartActions;
				return codeBlockStartActions.Length;
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06002412 RID: 9234 RVA: 0x00070C7C File Offset: 0x0006EE7C
		public readonly int CodeBlockEndActionsCount
		{
			get
			{
				ImmutableArray<CodeBlockAnalyzerAction> codeBlockEndActions = this._codeBlockEndActions;
				return codeBlockEndActions.Length;
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06002413 RID: 9235 RVA: 0x00070C98 File Offset: 0x0006EE98
		public readonly int CodeBlockActionsCount
		{
			get
			{
				ImmutableArray<CodeBlockAnalyzerAction> codeBlockActions = this._codeBlockActions;
				return codeBlockActions.Length;
			}
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x06002414 RID: 9236 RVA: 0x00070CB3 File Offset: 0x0006EEB3
		public readonly bool Concurrent
		{
			get
			{
				return this._concurrent;
			}
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06002415 RID: 9237 RVA: 0x00070CBB File Offset: 0x0006EEBB
		// (set) Token: 0x06002416 RID: 9238 RVA: 0x00070CC3 File Offset: 0x0006EEC3
		public bool IsEmpty { readonly get; private set; }

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06002417 RID: 9239 RVA: 0x00070CCC File Offset: 0x0006EECC
		public readonly bool IsDefault
		{
			get
			{
				ImmutableArray<CompilationStartAnalyzerAction> compilationStartActions = this._compilationStartActions;
				return compilationStartActions.IsDefault;
			}
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06002418 RID: 9240 RVA: 0x00070CE7 File Offset: 0x0006EEE7
		[Nullable(new byte[] { 0, 1 })]
		internal readonly ImmutableArray<CompilationStartAnalyzerAction> CompilationStartActions
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._compilationStartActions;
			}
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06002419 RID: 9241 RVA: 0x00070CEF File Offset: 0x0006EEEF
		[Nullable(new byte[] { 0, 1 })]
		internal readonly ImmutableArray<CompilationAnalyzerAction> CompilationEndActions
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._compilationEndActions;
			}
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x0600241A RID: 9242 RVA: 0x00070CF7 File Offset: 0x0006EEF7
		[Nullable(new byte[] { 0, 1 })]
		internal readonly ImmutableArray<CompilationAnalyzerAction> CompilationActions
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._compilationActions;
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x0600241B RID: 9243 RVA: 0x00070CFF File Offset: 0x0006EEFF
		[Nullable(new byte[] { 0, 1 })]
		internal readonly ImmutableArray<SyntaxTreeAnalyzerAction> SyntaxTreeActions
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._syntaxTreeActions;
			}
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x0600241C RID: 9244 RVA: 0x00070D07 File Offset: 0x0006EF07
		[Nullable(new byte[] { 0, 1 })]
		internal readonly ImmutableArray<AdditionalFileAnalyzerAction> AdditionalFileActions
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._additionalFileActions;
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x0600241D RID: 9245 RVA: 0x00070D0F File Offset: 0x0006EF0F
		[Nullable(new byte[] { 0, 1 })]
		internal readonly ImmutableArray<SemanticModelAnalyzerAction> SemanticModelActions
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._semanticModelActions;
			}
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x0600241E RID: 9246 RVA: 0x00070D17 File Offset: 0x0006EF17
		[Nullable(new byte[] { 0, 1 })]
		internal readonly ImmutableArray<SymbolAnalyzerAction> SymbolActions
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._symbolActions;
			}
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x0600241F RID: 9247 RVA: 0x00070D1F File Offset: 0x0006EF1F
		[Nullable(new byte[] { 0, 1 })]
		internal readonly ImmutableArray<SymbolStartAnalyzerAction> SymbolStartActions
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._symbolStartActions;
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06002420 RID: 9248 RVA: 0x00070D27 File Offset: 0x0006EF27
		[Nullable(new byte[] { 0, 1 })]
		internal readonly ImmutableArray<SymbolEndAnalyzerAction> SymbolEndActions
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._symbolEndActions;
			}
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06002421 RID: 9249 RVA: 0x00070D2F File Offset: 0x0006EF2F
		[Nullable(new byte[] { 0, 1 })]
		internal readonly ImmutableArray<CodeBlockAnalyzerAction> CodeBlockEndActions
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._codeBlockEndActions;
			}
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06002422 RID: 9250 RVA: 0x00070D37 File Offset: 0x0006EF37
		[Nullable(new byte[] { 0, 1 })]
		internal readonly ImmutableArray<CodeBlockAnalyzerAction> CodeBlockActions
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._codeBlockActions;
			}
		}

		// Token: 0x06002423 RID: 9251 RVA: 0x00070D40 File Offset: 0x0006EF40
		[NullableContext(0)]
		[return: Nullable(new byte[] { 0, 1, 0 })]
		internal readonly ImmutableArray<CodeBlockStartAnalyzerAction<TLanguageKindEnum>> GetCodeBlockStartActions<TLanguageKindEnum>() where TLanguageKindEnum : struct
		{
			ImmutableArray<AnalyzerAction> codeBlockStartActions = this._codeBlockStartActions;
			return codeBlockStartActions.OfType<CodeBlockStartAnalyzerAction<TLanguageKindEnum>>().ToImmutableArray<CodeBlockStartAnalyzerAction<TLanguageKindEnum>>();
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x00070D60 File Offset: 0x0006EF60
		[NullableContext(0)]
		[return: Nullable(new byte[] { 0, 1, 0 })]
		internal readonly ImmutableArray<SyntaxNodeAnalyzerAction<TLanguageKindEnum>> GetSyntaxNodeActions<TLanguageKindEnum>() where TLanguageKindEnum : struct
		{
			ImmutableArray<AnalyzerAction> syntaxNodeActions = this._syntaxNodeActions;
			return syntaxNodeActions.OfType<SyntaxNodeAnalyzerAction<TLanguageKindEnum>>().ToImmutableArray<SyntaxNodeAnalyzerAction<TLanguageKindEnum>>();
		}

		// Token: 0x06002425 RID: 9253 RVA: 0x00070D80 File Offset: 0x0006EF80
		[NullableContext(0)]
		[return: Nullable(new byte[] { 0, 1, 0 })]
		internal readonly ImmutableArray<SyntaxNodeAnalyzerAction<TLanguageKindEnum>> GetSyntaxNodeActions<TLanguageKindEnum>([Nullable(1)] DiagnosticAnalyzer analyzer) where TLanguageKindEnum : struct
		{
			ArrayBuilder<SyntaxNodeAnalyzerAction<TLanguageKindEnum>> instance = ArrayBuilder<SyntaxNodeAnalyzerAction<TLanguageKindEnum>>.GetInstance();
			ImmutableArray<AnalyzerAction> syntaxNodeActions = this._syntaxNodeActions;
			foreach (AnalyzerAction analyzerAction in syntaxNodeActions)
			{
				if (analyzerAction.Analyzer == analyzer)
				{
					SyntaxNodeAnalyzerAction<TLanguageKindEnum> syntaxNodeAnalyzerAction = analyzerAction as SyntaxNodeAnalyzerAction<TLanguageKindEnum>;
					if (syntaxNodeAnalyzerAction != null)
					{
						instance.Add(syntaxNodeAnalyzerAction);
					}
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06002426 RID: 9254 RVA: 0x00070DD8 File Offset: 0x0006EFD8
		[Nullable(new byte[] { 0, 1 })]
		internal readonly ImmutableArray<OperationBlockAnalyzerAction> OperationBlockActions
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._operationBlockActions;
			}
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06002427 RID: 9255 RVA: 0x00070DE0 File Offset: 0x0006EFE0
		[Nullable(new byte[] { 0, 1 })]
		internal readonly ImmutableArray<OperationBlockAnalyzerAction> OperationBlockEndActions
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._operationBlockEndActions;
			}
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06002428 RID: 9256 RVA: 0x00070DE8 File Offset: 0x0006EFE8
		[Nullable(new byte[] { 0, 1 })]
		internal readonly ImmutableArray<OperationBlockStartAnalyzerAction> OperationBlockStartActions
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._operationBlockStartActions;
			}
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06002429 RID: 9257 RVA: 0x00070DF0 File Offset: 0x0006EFF0
		[Nullable(new byte[] { 0, 1 })]
		internal readonly ImmutableArray<OperationAnalyzerAction> OperationActions
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._operationActions;
			}
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x00070DF8 File Offset: 0x0006EFF8
		internal void AddCompilationStartAction(CompilationStartAnalyzerAction action)
		{
			this._compilationStartActions = this._compilationStartActions.Add(action);
			this.IsEmpty = false;
		}

		// Token: 0x0600242B RID: 9259 RVA: 0x00070E13 File Offset: 0x0006F013
		internal void AddCompilationEndAction(CompilationAnalyzerAction action)
		{
			this._compilationEndActions = this._compilationEndActions.Add(action);
			this.IsEmpty = false;
		}

		// Token: 0x0600242C RID: 9260 RVA: 0x00070E2E File Offset: 0x0006F02E
		internal void AddCompilationAction(CompilationAnalyzerAction action)
		{
			this._compilationActions = this._compilationActions.Add(action);
			this.IsEmpty = false;
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x00070E49 File Offset: 0x0006F049
		internal void AddSyntaxTreeAction(SyntaxTreeAnalyzerAction action)
		{
			this._syntaxTreeActions = this._syntaxTreeActions.Add(action);
			this.IsEmpty = false;
		}

		// Token: 0x0600242E RID: 9262 RVA: 0x00070E64 File Offset: 0x0006F064
		internal void AddAdditionalFileAction(AdditionalFileAnalyzerAction action)
		{
			this._additionalFileActions = this._additionalFileActions.Add(action);
			this.IsEmpty = false;
		}

		// Token: 0x0600242F RID: 9263 RVA: 0x00070E7F File Offset: 0x0006F07F
		internal void AddSemanticModelAction(SemanticModelAnalyzerAction action)
		{
			this._semanticModelActions = this._semanticModelActions.Add(action);
			this.IsEmpty = false;
		}

		// Token: 0x06002430 RID: 9264 RVA: 0x00070E9A File Offset: 0x0006F09A
		internal void AddSymbolAction(SymbolAnalyzerAction action)
		{
			this._symbolActions = this._symbolActions.Add(action);
			this.IsEmpty = false;
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x00070EB5 File Offset: 0x0006F0B5
		internal void AddSymbolStartAction(SymbolStartAnalyzerAction action)
		{
			this._symbolStartActions = this._symbolStartActions.Add(action);
			this.IsEmpty = false;
		}

		// Token: 0x06002432 RID: 9266 RVA: 0x00070ED0 File Offset: 0x0006F0D0
		internal void AddSymbolEndAction(SymbolEndAnalyzerAction action)
		{
			this._symbolEndActions = this._symbolEndActions.Add(action);
			this.IsEmpty = false;
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x00070EEB File Offset: 0x0006F0EB
		[NullableContext(0)]
		internal void AddCodeBlockStartAction<TLanguageKindEnum>([Nullable(new byte[] { 1, 0 })] CodeBlockStartAnalyzerAction<TLanguageKindEnum> action) where TLanguageKindEnum : struct
		{
			this._codeBlockStartActions = this._codeBlockStartActions.Add(action);
			this.IsEmpty = false;
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x00070F06 File Offset: 0x0006F106
		internal void AddCodeBlockEndAction(CodeBlockAnalyzerAction action)
		{
			this._codeBlockEndActions = this._codeBlockEndActions.Add(action);
			this.IsEmpty = false;
		}

		// Token: 0x06002435 RID: 9269 RVA: 0x00070F21 File Offset: 0x0006F121
		internal void AddCodeBlockAction(CodeBlockAnalyzerAction action)
		{
			this._codeBlockActions = this._codeBlockActions.Add(action);
			this.IsEmpty = false;
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x00070F3C File Offset: 0x0006F13C
		[NullableContext(0)]
		internal void AddSyntaxNodeAction<TLanguageKindEnum>([Nullable(new byte[] { 1, 0 })] SyntaxNodeAnalyzerAction<TLanguageKindEnum> action) where TLanguageKindEnum : struct
		{
			this._syntaxNodeActions = this._syntaxNodeActions.Add(action);
			this.IsEmpty = false;
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x00070F57 File Offset: 0x0006F157
		internal void AddOperationBlockStartAction(OperationBlockStartAnalyzerAction action)
		{
			this._operationBlockStartActions = this._operationBlockStartActions.Add(action);
			this.IsEmpty = false;
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x00070F72 File Offset: 0x0006F172
		internal void AddOperationBlockAction(OperationBlockAnalyzerAction action)
		{
			this._operationBlockActions = this._operationBlockActions.Add(action);
			this.IsEmpty = false;
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x00070F8D File Offset: 0x0006F18D
		internal void AddOperationBlockEndAction(OperationBlockAnalyzerAction action)
		{
			this._operationBlockEndActions = this._operationBlockEndActions.Add(action);
			this.IsEmpty = false;
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x00070FA8 File Offset: 0x0006F1A8
		internal void AddOperationAction(OperationAnalyzerAction action)
		{
			this._operationActions = this._operationActions.Add(action);
			this.IsEmpty = false;
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x00070FC3 File Offset: 0x0006F1C3
		internal void EnableConcurrentExecution()
		{
			this._concurrent = true;
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x00070FCC File Offset: 0x0006F1CC
		public readonly AnalyzerActions Append(in AnalyzerActions otherActions, bool appendSymbolStartAndSymbolEndActions = true)
		{
			if (otherActions.IsDefault)
			{
				throw new ArgumentNullException("otherActions");
			}
			AnalyzerActions analyzerActions = new AnalyzerActions(this._concurrent || otherActions.Concurrent);
			ImmutableArray<CompilationStartAnalyzerAction> compilationStartActions = this._compilationStartActions;
			analyzerActions._compilationStartActions = compilationStartActions.AddRange(otherActions._compilationStartActions);
			ImmutableArray<CompilationAnalyzerAction> immutableArray = this._compilationEndActions;
			analyzerActions._compilationEndActions = immutableArray.AddRange(otherActions._compilationEndActions);
			immutableArray = this._compilationActions;
			analyzerActions._compilationActions = immutableArray.AddRange(otherActions._compilationActions);
			ImmutableArray<SyntaxTreeAnalyzerAction> syntaxTreeActions = this._syntaxTreeActions;
			analyzerActions._syntaxTreeActions = syntaxTreeActions.AddRange(otherActions._syntaxTreeActions);
			ImmutableArray<AdditionalFileAnalyzerAction> additionalFileActions = this._additionalFileActions;
			analyzerActions._additionalFileActions = additionalFileActions.AddRange(otherActions._additionalFileActions);
			ImmutableArray<SemanticModelAnalyzerAction> semanticModelActions = this._semanticModelActions;
			analyzerActions._semanticModelActions = semanticModelActions.AddRange(otherActions._semanticModelActions);
			ImmutableArray<SymbolAnalyzerAction> symbolActions = this._symbolActions;
			analyzerActions._symbolActions = symbolActions.AddRange(otherActions._symbolActions);
			ImmutableArray<SymbolStartAnalyzerAction> immutableArray2;
			if (!appendSymbolStartAndSymbolEndActions)
			{
				immutableArray2 = this._symbolStartActions;
			}
			else
			{
				ImmutableArray<SymbolStartAnalyzerAction> symbolStartActions = this._symbolStartActions;
				immutableArray2 = symbolStartActions.AddRange(otherActions._symbolStartActions);
			}
			analyzerActions._symbolStartActions = immutableArray2;
			ImmutableArray<SymbolEndAnalyzerAction> immutableArray3;
			if (!appendSymbolStartAndSymbolEndActions)
			{
				immutableArray3 = this._symbolEndActions;
			}
			else
			{
				ImmutableArray<SymbolEndAnalyzerAction> symbolEndActions = this._symbolEndActions;
				immutableArray3 = symbolEndActions.AddRange(otherActions._symbolEndActions);
			}
			analyzerActions._symbolEndActions = immutableArray3;
			ImmutableArray<AnalyzerAction> immutableArray4 = this._codeBlockStartActions;
			analyzerActions._codeBlockStartActions = immutableArray4.AddRange(otherActions._codeBlockStartActions);
			ImmutableArray<CodeBlockAnalyzerAction> immutableArray5 = this._codeBlockEndActions;
			analyzerActions._codeBlockEndActions = immutableArray5.AddRange(otherActions._codeBlockEndActions);
			immutableArray5 = this._codeBlockActions;
			analyzerActions._codeBlockActions = immutableArray5.AddRange(otherActions._codeBlockActions);
			immutableArray4 = this._syntaxNodeActions;
			analyzerActions._syntaxNodeActions = immutableArray4.AddRange(otherActions._syntaxNodeActions);
			ImmutableArray<OperationAnalyzerAction> operationActions = this._operationActions;
			analyzerActions._operationActions = operationActions.AddRange(otherActions._operationActions);
			ImmutableArray<OperationBlockStartAnalyzerAction> operationBlockStartActions = this._operationBlockStartActions;
			analyzerActions._operationBlockStartActions = operationBlockStartActions.AddRange(otherActions._operationBlockStartActions);
			ImmutableArray<OperationBlockAnalyzerAction> immutableArray6 = this._operationBlockEndActions;
			analyzerActions._operationBlockEndActions = immutableArray6.AddRange(otherActions._operationBlockEndActions);
			immutableArray6 = this._operationBlockActions;
			analyzerActions._operationBlockActions = immutableArray6.AddRange(otherActions._operationBlockActions);
			analyzerActions.IsEmpty = this.IsEmpty && otherActions.IsEmpty;
			return analyzerActions;
		}

		// Token: 0x04000F67 RID: 3943
		public static readonly AnalyzerActions Empty = new AnalyzerActions(false);

		// Token: 0x04000F68 RID: 3944
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<CompilationStartAnalyzerAction> _compilationStartActions;

		// Token: 0x04000F69 RID: 3945
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<CompilationAnalyzerAction> _compilationEndActions;

		// Token: 0x04000F6A RID: 3946
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<CompilationAnalyzerAction> _compilationActions;

		// Token: 0x04000F6B RID: 3947
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<SyntaxTreeAnalyzerAction> _syntaxTreeActions;

		// Token: 0x04000F6C RID: 3948
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<AdditionalFileAnalyzerAction> _additionalFileActions;

		// Token: 0x04000F6D RID: 3949
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<SemanticModelAnalyzerAction> _semanticModelActions;

		// Token: 0x04000F6E RID: 3950
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<SymbolAnalyzerAction> _symbolActions;

		// Token: 0x04000F6F RID: 3951
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<SymbolStartAnalyzerAction> _symbolStartActions;

		// Token: 0x04000F70 RID: 3952
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<SymbolEndAnalyzerAction> _symbolEndActions;

		// Token: 0x04000F71 RID: 3953
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<AnalyzerAction> _codeBlockStartActions;

		// Token: 0x04000F72 RID: 3954
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<CodeBlockAnalyzerAction> _codeBlockEndActions;

		// Token: 0x04000F73 RID: 3955
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<CodeBlockAnalyzerAction> _codeBlockActions;

		// Token: 0x04000F74 RID: 3956
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<OperationBlockStartAnalyzerAction> _operationBlockStartActions;

		// Token: 0x04000F75 RID: 3957
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<OperationBlockAnalyzerAction> _operationBlockEndActions;

		// Token: 0x04000F76 RID: 3958
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<OperationBlockAnalyzerAction> _operationBlockActions;

		// Token: 0x04000F77 RID: 3959
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<AnalyzerAction> _syntaxNodeActions;

		// Token: 0x04000F78 RID: 3960
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<OperationAnalyzerAction> _operationActions;

		// Token: 0x04000F79 RID: 3961
		private bool _concurrent;
	}
}
