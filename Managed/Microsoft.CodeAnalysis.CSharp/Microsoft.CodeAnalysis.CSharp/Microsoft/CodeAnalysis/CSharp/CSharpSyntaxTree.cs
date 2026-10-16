using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200022C RID: 556
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public abstract class CSharpSyntaxTree : SyntaxTree
	{
		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06002CA8 RID: 11432
		public new abstract CSharpParseOptions Options { get; }

		// Token: 0x06002CA9 RID: 11433 RVA: 0x000EE701 File Offset: 0x000EC901
		protected T CloneNodeAsRoot<[global::System.Runtime.CompilerServices.Nullable(0)] T>(T node) where T : CSharpSyntaxNode
		{
			return SyntaxNode.CloneNodeAsRoot<T>(node, this);
		}

		// Token: 0x06002CAA RID: 11434
		public new abstract CSharpSyntaxNode GetRoot(CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06002CAB RID: 11435
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public abstract bool TryGetRoot([NotNullWhen(true)] out CSharpSyntaxNode root);

		// Token: 0x06002CAC RID: 11436 RVA: 0x000EE70C File Offset: 0x000EC90C
		public new virtual Task<CSharpSyntaxNode> GetRootAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSyntaxNode csharpSyntaxNode;
			return Task.FromResult<CSharpSyntaxNode>(this.TryGetRoot(out csharpSyntaxNode) ? csharpSyntaxNode : this.GetRoot(cancellationToken));
		}

		// Token: 0x06002CAD RID: 11437 RVA: 0x000EE732 File Offset: 0x000EC932
		public Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax GetCompilationUnitRoot(CancellationToken cancellationToken = default(CancellationToken))
		{
			return (Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax)this.GetRoot(cancellationToken);
		}

		// Token: 0x06002CAE RID: 11438 RVA: 0x000EE740 File Offset: 0x000EC940
		public override bool IsEquivalentTo(SyntaxTree tree, bool topLevel = false)
		{
			return SyntaxFactory.AreEquivalent(this, tree, topLevel);
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06002CAF RID: 11439 RVA: 0x000EE74C File Offset: 0x000EC94C
		internal bool HasReferenceDirectives
		{
			get
			{
				return this.Options.Kind == SourceCodeKind.Script && this.GetCompilationUnitRoot(default(CancellationToken)).GetReferenceDirectives().Count > 0;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06002CB0 RID: 11440 RVA: 0x000EE788 File Offset: 0x000EC988
		internal bool HasReferenceOrLoadDirectives
		{
			get
			{
				if (this.Options.Kind == SourceCodeKind.Script)
				{
					Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax compilationUnitRoot = this.GetCompilationUnitRoot(default(CancellationToken));
					return compilationUnitRoot.GetReferenceDirectives().Count > 0 || compilationUnitRoot.GetLoadDirectives().Count > 0;
				}
				return false;
			}
		}

		// Token: 0x06002CB1 RID: 11441 RVA: 0x000EE7D3 File Offset: 0x000EC9D3
		internal void SetDirectiveStack(DirectiveStack directives)
		{
			this._directives = directives;
			this._hasDirectives = true;
		}

		// Token: 0x06002CB2 RID: 11442 RVA: 0x000EE7E4 File Offset: 0x000EC9E4
		private DirectiveStack GetDirectives()
		{
			if (!this._hasDirectives)
			{
				DirectiveStack directiveStack = this.GetRoot(default(CancellationToken)).CsGreen.ApplyDirectives(default(DirectiveStack));
				this.SetDirectiveStack(directiveStack);
			}
			return this._directives;
		}

		// Token: 0x06002CB3 RID: 11443 RVA: 0x000EE82C File Offset: 0x000ECA2C
		internal bool IsAnyPreprocessorSymbolDefined([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> conditionalSymbols)
		{
			foreach (string text in conditionalSymbols)
			{
				if (this.IsPreprocessorSymbolDefined(text))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002CB4 RID: 11444 RVA: 0x000EE860 File Offset: 0x000ECA60
		internal bool IsPreprocessorSymbolDefined(string symbolName)
		{
			return this.IsPreprocessorSymbolDefined(this.GetDirectives(), symbolName);
		}

		// Token: 0x06002CB5 RID: 11445 RVA: 0x000EE870 File Offset: 0x000ECA70
		private bool IsPreprocessorSymbolDefined(DirectiveStack directives, string symbolName)
		{
			DefineState defineState = directives.IsDefined(symbolName);
			return defineState == DefineState.Defined || (defineState != DefineState.Undefined && this.Options.PreprocessorSymbols.Contains(symbolName));
		}

		// Token: 0x06002CB6 RID: 11446 RVA: 0x000EE8A8 File Offset: 0x000ECAA8
		internal bool IsPreprocessorSymbolDefined(string symbolName, int position)
		{
			if (this._preprocessorStateChangePositions.IsDefault)
			{
				this.BuildPreprocessorStateChangeMap();
			}
			int num = this._preprocessorStateChangePositions.BinarySearch(position);
			DirectiveStack directiveStack;
			if (num < 0)
			{
				num = ~num - 1;
				if (num >= 0)
				{
					directiveStack = this._preprocessorStates[num];
				}
				else
				{
					directiveStack = DirectiveStack.Empty;
				}
			}
			else
			{
				directiveStack = this._preprocessorStates[num];
			}
			return this.IsPreprocessorSymbolDefined(directiveStack, symbolName);
		}

		// Token: 0x06002CB7 RID: 11447 RVA: 0x000EE910 File Offset: 0x000ECB10
		private void BuildPreprocessorStateChangeMap()
		{
			DirectiveStack directiveStack = DirectiveStack.Empty;
			ArrayBuilder<int> instance = ArrayBuilder<int>.GetInstance();
			ArrayBuilder<DirectiveStack> instance2 = ArrayBuilder<DirectiveStack>.GetInstance();
			foreach (Microsoft.CodeAnalysis.CSharp.Syntax.DirectiveTriviaSyntax directiveTriviaSyntax in this.GetRoot(default(CancellationToken)).GetDirectives(delegate(Microsoft.CodeAnalysis.CSharp.Syntax.DirectiveTriviaSyntax d)
			{
				SyntaxKind syntaxKind = d.Kind();
				return syntaxKind - SyntaxKind.IfDirectiveTrivia <= 3 || syntaxKind - SyntaxKind.DefineDirectiveTrivia <= 1;
			}))
			{
				directiveStack = directiveTriviaSyntax.ApplyDirectives(directiveStack);
				switch (directiveTriviaSyntax.Kind())
				{
				case SyntaxKind.IfDirectiveTrivia:
					continue;
				case SyntaxKind.ElifDirectiveTrivia:
					instance2.Add(directiveStack);
					instance.Add(((Microsoft.CodeAnalysis.CSharp.Syntax.ElifDirectiveTriviaSyntax)directiveTriviaSyntax).ElifKeyword.SpanStart);
					continue;
				case SyntaxKind.ElseDirectiveTrivia:
					instance2.Add(directiveStack);
					instance.Add(((Microsoft.CodeAnalysis.CSharp.Syntax.ElseDirectiveTriviaSyntax)directiveTriviaSyntax).ElseKeyword.SpanStart);
					continue;
				case SyntaxKind.EndIfDirectiveTrivia:
					instance2.Add(directiveStack);
					instance.Add(((Microsoft.CodeAnalysis.CSharp.Syntax.EndIfDirectiveTriviaSyntax)directiveTriviaSyntax).EndIfKeyword.SpanStart);
					continue;
				case SyntaxKind.DefineDirectiveTrivia:
					instance2.Add(directiveStack);
					instance.Add(((Microsoft.CodeAnalysis.CSharp.Syntax.DefineDirectiveTriviaSyntax)directiveTriviaSyntax).Name.SpanStart);
					continue;
				case SyntaxKind.UndefDirectiveTrivia:
					instance2.Add(directiveStack);
					instance.Add(((Microsoft.CodeAnalysis.CSharp.Syntax.UndefDirectiveTriviaSyntax)directiveTriviaSyntax).Name.SpanStart);
					continue;
				}
				throw ExceptionUtilities.UnexpectedValue(directiveTriviaSyntax.Kind());
			}
			ImmutableInterlocked.InterlockedInitialize<DirectiveStack>(ref this._preprocessorStates, instance2.ToImmutableAndFree());
			ImmutableInterlocked.InterlockedInitialize<int>(ref this._preprocessorStateChangePositions, instance.ToImmutableAndFree());
		}

		// Token: 0x06002CB8 RID: 11448 RVA: 0x000EEADC File Offset: 0x000ECCDC
		public static SyntaxTree Create(CSharpSyntaxNode root, [global::System.Runtime.CompilerServices.Nullable(2)] CSharpParseOptions options = null, string path = "", [global::System.Runtime.CompilerServices.Nullable(2)] Encoding encoding = null)
		{
			return CSharpSyntaxTree.Create(root, options, path, encoding, null);
		}

		// Token: 0x06002CB9 RID: 11449 RVA: 0x000EEAE8 File Offset: 0x000ECCE8
		[EditorBrowsable(1)]
		[Obsolete("The diagnosticOptions and isGeneratedCode parameters are obsolete due to performance problems, if you are using them use CompilationOptions.SyntaxTreeOptionsProvider instead", false)]
		public static SyntaxTree Create(CSharpSyntaxNode root, [global::System.Runtime.CompilerServices.Nullable(2)] CSharpParseOptions options, string path, [global::System.Runtime.CompilerServices.Nullable(2)] Encoding encoding, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ImmutableDictionary<string, ReportDiagnostic> diagnosticOptions, bool? isGeneratedCode)
		{
			if (root == null)
			{
				throw new ArgumentNullException("root");
			}
			DirectiveStack directiveStack = ((root.Kind() == SyntaxKind.CompilationUnit) ? ((Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax)root).GetConditionalDirectivesStack() : DirectiveStack.Empty);
			return new CSharpSyntaxTree.ParsedSyntaxTree(null, encoding, SourceHashAlgorithm.Sha1, path, options ?? CSharpParseOptions.Default, root, directiveStack, diagnosticOptions, true);
		}

		// Token: 0x06002CBA RID: 11450 RVA: 0x000EEB3B File Offset: 0x000ECD3B
		internal static SyntaxTree CreateForDebugger(CSharpSyntaxNode root, SourceText text, CSharpParseOptions options)
		{
			return new CSharpSyntaxTree.DebuggerSyntaxTree(root, text, options);
		}

		// Token: 0x06002CBB RID: 11451 RVA: 0x000EEB48 File Offset: 0x000ECD48
		internal static SyntaxTree CreateWithoutClone(CSharpSyntaxNode root)
		{
			return new CSharpSyntaxTree.ParsedSyntaxTree(null, null, SourceHashAlgorithm.Sha1, "", CSharpParseOptions.Default, root, DirectiveStack.Empty, null, false);
		}

		// Token: 0x06002CBC RID: 11452 RVA: 0x000EEB6F File Offset: 0x000ECD6F
		internal static SyntaxTree ParseTextLazy(SourceText text, [global::System.Runtime.CompilerServices.Nullable(2)] CSharpParseOptions options = null, string path = "")
		{
			return new CSharpSyntaxTree.LazySyntaxTree(text, options ?? CSharpParseOptions.Default, path, null);
		}

		// Token: 0x06002CBD RID: 11453 RVA: 0x000EEB83 File Offset: 0x000ECD83
		public static SyntaxTree ParseText(string text, [global::System.Runtime.CompilerServices.Nullable(2)] CSharpParseOptions options = null, string path = "", [global::System.Runtime.CompilerServices.Nullable(2)] Encoding encoding = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return CSharpSyntaxTree.ParseText(text, options, path, encoding, null, cancellationToken);
		}

		// Token: 0x06002CBE RID: 11454 RVA: 0x000EEB91 File Offset: 0x000ECD91
		[EditorBrowsable(1)]
		[Obsolete("The diagnosticOptions and isGeneratedCode parameters are obsolete due to performance problems, if you are using them use CompilationOptions.SyntaxTreeOptionsProvider instead", false)]
		public static SyntaxTree ParseText(string text, [global::System.Runtime.CompilerServices.Nullable(2)] CSharpParseOptions options, string path, [global::System.Runtime.CompilerServices.Nullable(2)] Encoding encoding, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ImmutableDictionary<string, ReportDiagnostic> diagnosticOptions, bool? isGeneratedCode, CancellationToken cancellationToken)
		{
			return CSharpSyntaxTree.ParseText(SourceText.From(text, encoding, SourceHashAlgorithm.Sha1), options, path, diagnosticOptions, isGeneratedCode, cancellationToken);
		}

		// Token: 0x06002CBF RID: 11455 RVA: 0x000EEBA8 File Offset: 0x000ECDA8
		public static SyntaxTree ParseText(SourceText text, [global::System.Runtime.CompilerServices.Nullable(2)] CSharpParseOptions options = null, string path = "", CancellationToken cancellationToken = default(CancellationToken))
		{
			return CSharpSyntaxTree.ParseText(text, options, path, null, cancellationToken);
		}

		// Token: 0x06002CC0 RID: 11456 RVA: 0x000EEBB4 File Offset: 0x000ECDB4
		[EditorBrowsable(1)]
		[Obsolete("The diagnosticOptions and isGeneratedCode parameters are obsolete due to performance problems, if you are using them use CompilationOptions.SyntaxTreeOptionsProvider instead", false)]
		public static SyntaxTree ParseText(SourceText text, [global::System.Runtime.CompilerServices.Nullable(2)] CSharpParseOptions options, string path, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ImmutableDictionary<string, ReportDiagnostic> diagnosticOptions, bool? isGeneratedCode, CancellationToken cancellationToken)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			options = options ?? CSharpParseOptions.Default;
			SyntaxTree syntaxTree;
			using (Lexer lexer = new Lexer(text, options, true, false))
			{
				using (LanguageParser languageParser = new LanguageParser(lexer, null, null, LexerMode.Syntax, cancellationToken))
				{
					Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax compilationUnitSyntax = (Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax)languageParser.ParseCompilationUnit().CreateRed();
					syntaxTree = new CSharpSyntaxTree.ParsedSyntaxTree(text, text.Encoding, text.ChecksumAlgorithm, path, options, compilationUnitSyntax, languageParser.Directives, diagnosticOptions, true);
				}
			}
			return syntaxTree;
		}

		// Token: 0x06002CC1 RID: 11457 RVA: 0x000EEC54 File Offset: 0x000ECE54
		public override SyntaxTree WithChangedText(SourceText newText)
		{
			SourceText sourceText;
			if (!this.TryGetText(out sourceText))
			{
				return this.WithChanges(newText, new TextChangeRange[]
				{
					new TextChangeRange(new TextSpan(0, this.Length), newText.Length)
				});
			}
			IReadOnlyList<TextChangeRange> changeRanges = newText.GetChangeRanges(sourceText);
			if (changeRanges.Count == 0 && newText == sourceText)
			{
				return this;
			}
			return this.WithChanges(newText, changeRanges);
		}

		// Token: 0x06002CC2 RID: 11458 RVA: 0x000EECB8 File Offset: 0x000ECEB8
		private SyntaxTree WithChanges(SourceText newText, IReadOnlyList<TextChangeRange> changes)
		{
			if (changes == null)
			{
				throw new ArgumentNullException("changes");
			}
			IReadOnlyList<TextChangeRange> readOnlyList = changes;
			CSharpSyntaxTree csharpSyntaxTree = this;
			if (readOnlyList.Count == 1 && readOnlyList[0].Span == new TextSpan(0, this.Length) && readOnlyList[0].NewLength == newText.Length)
			{
				readOnlyList = null;
				csharpSyntaxTree = null;
			}
			SyntaxTree syntaxTree;
			using (Lexer lexer = new Lexer(newText, this.Options, true, false))
			{
				using (LanguageParser languageParser = new LanguageParser(lexer, (csharpSyntaxTree != null) ? csharpSyntaxTree.GetRoot(default(CancellationToken)) : null, readOnlyList, LexerMode.Syntax, default(CancellationToken)))
				{
					Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax compilationUnitSyntax = (Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax)languageParser.ParseCompilationUnit().CreateRed();
					syntaxTree = new CSharpSyntaxTree.ParsedSyntaxTree(newText, newText.Encoding, newText.ChecksumAlgorithm, this.FilePath, this.Options, compilationUnitSyntax, languageParser.Directives, this.DiagnosticOptions, true);
				}
			}
			return syntaxTree;
		}

		// Token: 0x06002CC3 RID: 11459 RVA: 0x000EEDCC File Offset: 0x000ECFCC
		public override IList<TextSpan> GetChangedSpans(SyntaxTree oldTree)
		{
			if (oldTree == null)
			{
				throw new ArgumentNullException("oldTree");
			}
			return SyntaxDiffer.GetPossiblyDifferentTextSpans(oldTree, this);
		}

		// Token: 0x06002CC4 RID: 11460 RVA: 0x000EEDE3 File Offset: 0x000ECFE3
		public override IList<TextChange> GetChanges(SyntaxTree oldTree)
		{
			if (oldTree == null)
			{
				throw new ArgumentNullException("oldTree");
			}
			return SyntaxDiffer.GetTextChanges(oldTree, this);
		}

		// Token: 0x06002CC5 RID: 11461 RVA: 0x000EEDFA File Offset: 0x000ECFFA
		public override FileLinePositionSpan GetLineSpan(TextSpan span, CancellationToken cancellationToken = default(CancellationToken))
		{
			return new FileLinePositionSpan(this.FilePath, this.GetLinePosition(span.Start), this.GetLinePosition(span.End));
		}

		// Token: 0x06002CC6 RID: 11462 RVA: 0x000EEE21 File Offset: 0x000ED021
		public override FileLinePositionSpan GetMappedLineSpan(TextSpan span, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (this._lazyLineDirectiveMap == null)
			{
				Interlocked.CompareExchange<CSharpLineDirectiveMap>(ref this._lazyLineDirectiveMap, new CSharpLineDirectiveMap(this), null);
			}
			return this._lazyLineDirectiveMap.TranslateSpan(this.GetText(cancellationToken), this.FilePath, span);
		}

		// Token: 0x06002CC7 RID: 11463 RVA: 0x000EEE57 File Offset: 0x000ED057
		public override LineVisibility GetLineVisibility(int position, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (this._lazyLineDirectiveMap == null)
			{
				Interlocked.CompareExchange<CSharpLineDirectiveMap>(ref this._lazyLineDirectiveMap, new CSharpLineDirectiveMap(this), null);
			}
			return this._lazyLineDirectiveMap.GetLineVisibility(this.GetText(cancellationToken), position);
		}

		// Token: 0x06002CC8 RID: 11464 RVA: 0x000EEE88 File Offset: 0x000ED088
		internal override FileLinePositionSpan GetMappedLineSpanAndVisibility(TextSpan span, out bool isHiddenPosition)
		{
			if (this._lazyLineDirectiveMap == null)
			{
				Interlocked.CompareExchange<CSharpLineDirectiveMap>(ref this._lazyLineDirectiveMap, new CSharpLineDirectiveMap(this), null);
			}
			return this._lazyLineDirectiveMap.TranslateSpanAndVisibility(this.GetText(default(CancellationToken)), this.FilePath, span, out isHiddenPosition);
		}

		// Token: 0x06002CC9 RID: 11465 RVA: 0x000EEED2 File Offset: 0x000ED0D2
		public override bool HasHiddenRegions()
		{
			if (this._lazyLineDirectiveMap == null)
			{
				Interlocked.CompareExchange<CSharpLineDirectiveMap>(ref this._lazyLineDirectiveMap, new CSharpLineDirectiveMap(this), null);
			}
			return this._lazyLineDirectiveMap.HasAnyHiddenRegions();
		}

		// Token: 0x06002CCA RID: 11466 RVA: 0x000EEEFA File Offset: 0x000ED0FA
		internal PragmaWarningState GetPragmaDirectiveWarningState(string id, int position)
		{
			if (this._lazyPragmaWarningStateMap == null)
			{
				Interlocked.CompareExchange<CSharpPragmaWarningStateMap>(ref this._lazyPragmaWarningStateMap, new CSharpPragmaWarningStateMap(this), null);
			}
			return this._lazyPragmaWarningStateMap.GetWarningState(id, position);
		}

		// Token: 0x06002CCB RID: 11467 RVA: 0x000EEF24 File Offset: 0x000ED124
		private NullableContextStateMap GetNullableContextStateMap()
		{
			if (this._lazyNullableContextStateMap == null)
			{
				Interlocked.CompareExchange<StrongBox<NullableContextStateMap>>(ref this._lazyNullableContextStateMap, new StrongBox<NullableContextStateMap>(NullableContextStateMap.Create(this)), null);
			}
			return this._lazyNullableContextStateMap.Value;
		}

		// Token: 0x06002CCC RID: 11468 RVA: 0x000EEF54 File Offset: 0x000ED154
		internal NullableContextState GetNullableContextState(int position)
		{
			return this.GetNullableContextStateMap().GetContextState(position);
		}

		// Token: 0x06002CCD RID: 11469 RVA: 0x000EEF70 File Offset: 0x000ED170
		internal bool? IsNullableAnalysisEnabled(TextSpan span)
		{
			return this.GetNullableContextStateMap().IsNullableAnalysisEnabled(span);
		}

		// Token: 0x06002CCE RID: 11470 RVA: 0x000EEF8C File Offset: 0x000ED18C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal bool IsGeneratedCode(SyntaxTreeOptionsProvider provider, CancellationToken cancellationToken)
		{
			GeneratedKind? generatedKind = ((provider != null) ? new GeneratedKind?(provider.IsGenerated(this, cancellationToken)) : default(GeneratedKind?));
			if (generatedKind != null)
			{
				GeneratedKind valueOrDefault = generatedKind.GetValueOrDefault();
				if (valueOrDefault != GeneratedKind.Unknown)
				{
					return valueOrDefault != GeneratedKind.NotGenerated;
				}
			}
			return this.<IsGeneratedCode>g__isGeneratedHeuristic|46_0();
		}

		// Token: 0x06002CCF RID: 11471 RVA: 0x000EEFDC File Offset: 0x000ED1DC
		private LinePosition GetLinePosition(int position)
		{
			return this.GetText(default(CancellationToken)).Lines.GetLinePosition(position);
		}

		// Token: 0x06002CD0 RID: 11472 RVA: 0x000EF003 File Offset: 0x000ED203
		public override Location GetLocation(TextSpan span)
		{
			return new SourceLocation(this, span);
		}

		// Token: 0x06002CD1 RID: 11473 RVA: 0x000EF00C File Offset: 0x000ED20C
		public override IEnumerable<Diagnostic> GetDiagnostics(SyntaxNode node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			return this.GetDiagnostics(node.Green, node.Position);
		}

		// Token: 0x06002CD2 RID: 11474 RVA: 0x000EF02E File Offset: 0x000ED22E
		private IEnumerable<Diagnostic> GetDiagnostics(GreenNode greenNode, int position)
		{
			if (greenNode == null)
			{
				throw new InvalidOperationException();
			}
			if (greenNode.ContainsDiagnostics)
			{
				return this.EnumerateDiagnostics(greenNode, position);
			}
			return SpecializedCollections.EmptyEnumerable<Diagnostic>();
		}

		// Token: 0x06002CD3 RID: 11475 RVA: 0x000EF04F File Offset: 0x000ED24F
		private IEnumerable<Diagnostic> EnumerateDiagnostics(GreenNode node, int position)
		{
			SyntaxTreeDiagnosticEnumerator enumerator = new SyntaxTreeDiagnosticEnumerator(this, node, position);
			while (enumerator.MoveNext())
			{
				Diagnostic diagnostic = enumerator.Current;
				yield return diagnostic;
			}
			yield break;
		}

		// Token: 0x06002CD4 RID: 11476 RVA: 0x000EF06D File Offset: 0x000ED26D
		public override IEnumerable<Diagnostic> GetDiagnostics(SyntaxToken token)
		{
			if (token.Node == null)
			{
				throw new InvalidOperationException();
			}
			return this.GetDiagnostics(token.Node, token.Position);
		}

		// Token: 0x06002CD5 RID: 11477 RVA: 0x000EF092 File Offset: 0x000ED292
		public override IEnumerable<Diagnostic> GetDiagnostics(SyntaxTrivia trivia)
		{
			if (trivia.UnderlyingNode == null)
			{
				throw new InvalidOperationException();
			}
			return this.GetDiagnostics(trivia.UnderlyingNode, trivia.Position);
		}

		// Token: 0x06002CD6 RID: 11478 RVA: 0x000EF0B7 File Offset: 0x000ED2B7
		public override IEnumerable<Diagnostic> GetDiagnostics(SyntaxNodeOrToken nodeOrToken)
		{
			if (nodeOrToken.UnderlyingNode == null)
			{
				throw new InvalidOperationException();
			}
			return this.GetDiagnostics(nodeOrToken.UnderlyingNode, nodeOrToken.Position);
		}

		// Token: 0x06002CD7 RID: 11479 RVA: 0x000EF0DC File Offset: 0x000ED2DC
		public override IEnumerable<Diagnostic> GetDiagnostics(CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.GetDiagnostics(this.GetRoot(cancellationToken));
		}

		// Token: 0x06002CD8 RID: 11480 RVA: 0x000EF0EB File Offset: 0x000ED2EB
		protected override SyntaxNode GetRootCore(CancellationToken cancellationToken)
		{
			return this.GetRoot(cancellationToken);
		}

		// Token: 0x06002CD9 RID: 11481 RVA: 0x000EF0F4 File Offset: 0x000ED2F4
		protected override Task<SyntaxNode> GetRootAsyncCore(CancellationToken cancellationToken)
		{
			CSharpSyntaxTree.<GetRootAsyncCore>d__61 <GetRootAsyncCore>d__;
			<GetRootAsyncCore>d__.<>t__builder = AsyncTaskMethodBuilder<SyntaxNode>.Create();
			<GetRootAsyncCore>d__.<>4__this = this;
			<GetRootAsyncCore>d__.cancellationToken = cancellationToken;
			<GetRootAsyncCore>d__.<>1__state = -1;
			<GetRootAsyncCore>d__.<>t__builder.Start<CSharpSyntaxTree.<GetRootAsyncCore>d__61>(ref <GetRootAsyncCore>d__);
			return <GetRootAsyncCore>d__.<>t__builder.Task;
		}

		// Token: 0x06002CDA RID: 11482 RVA: 0x000EF140 File Offset: 0x000ED340
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		protected override bool TryGetRootCore([NotNullWhen(true)] out SyntaxNode root)
		{
			CSharpSyntaxNode csharpSyntaxNode;
			if (this.TryGetRoot(out csharpSyntaxNode))
			{
				root = csharpSyntaxNode;
				return true;
			}
			root = null;
			return false;
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06002CDB RID: 11483 RVA: 0x000EF160 File Offset: 0x000ED360
		protected override ParseOptions OptionsCore
		{
			get
			{
				return this.Options;
			}
		}

		// Token: 0x06002CDC RID: 11484 RVA: 0x000EF168 File Offset: 0x000ED368
		[EditorBrowsable(1)]
		[Obsolete("The diagnosticOptions parameter is obsolete due to performance problems, if you are passing non-null use CompilationOptions.SyntaxTreeOptionsProvider instead", false)]
		public static SyntaxTree ParseText(SourceText text, [global::System.Runtime.CompilerServices.Nullable(2)] CSharpParseOptions options, string path, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ImmutableDictionary<string, ReportDiagnostic> diagnosticOptions, CancellationToken cancellationToken)
		{
			return CSharpSyntaxTree.ParseText(text, options, path, diagnosticOptions, default(bool?), cancellationToken);
		}

		// Token: 0x06002CDD RID: 11485 RVA: 0x000EF18C File Offset: 0x000ED38C
		[EditorBrowsable(1)]
		[Obsolete("The diagnosticOptions parameter is obsolete due to performance problems, if you are passing non-null use CompilationOptions.SyntaxTreeOptionsProvider instead", false)]
		public static SyntaxTree ParseText(string text, [global::System.Runtime.CompilerServices.Nullable(2)] CSharpParseOptions options, string path, [global::System.Runtime.CompilerServices.Nullable(2)] Encoding encoding, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ImmutableDictionary<string, ReportDiagnostic> diagnosticOptions, CancellationToken cancellationToken)
		{
			return CSharpSyntaxTree.ParseText(text, options, path, encoding, diagnosticOptions, default(bool?), cancellationToken);
		}

		// Token: 0x06002CDE RID: 11486 RVA: 0x000EF1B0 File Offset: 0x000ED3B0
		[EditorBrowsable(1)]
		[Obsolete("The diagnosticOptions parameter is obsolete due to performance problems, if you are passing non-null use CompilationOptions.SyntaxTreeOptionsProvider instead", false)]
		public static SyntaxTree Create(CSharpSyntaxNode root, [global::System.Runtime.CompilerServices.Nullable(2)] CSharpParseOptions options, string path, [global::System.Runtime.CompilerServices.Nullable(2)] Encoding encoding, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ImmutableDictionary<string, ReportDiagnostic> diagnosticOptions)
		{
			return CSharpSyntaxTree.Create(root, options, path, encoding, diagnosticOptions, default(bool?));
		}

		// Token: 0x06002CE1 RID: 11489 RVA: 0x000EF1E8 File Offset: 0x000ED3E8
		[CompilerGenerated]
		private bool <IsGeneratedCode>g__isGeneratedHeuristic|46_0()
		{
			if (this._lazyIsGeneratedCode == GeneratedKind.Unknown)
			{
				this._lazyIsGeneratedCode = (GeneratedCodeUtilities.IsGeneratedCode(this, (SyntaxTrivia trivia) => trivia.Kind() == SyntaxKind.SingleLineCommentTrivia || trivia.Kind() == SyntaxKind.MultiLineCommentTrivia, default(CancellationToken)) ? GeneratedKind.MarkedGenerated : GeneratedKind.NotGenerated);
			}
			return this._lazyIsGeneratedCode == GeneratedKind.MarkedGenerated;
		}

		// Token: 0x04001150 RID: 4432
		internal static readonly SyntaxTree Dummy = new CSharpSyntaxTree.DummySyntaxTree();

		// Token: 0x04001151 RID: 4433
		private bool _hasDirectives;

		// Token: 0x04001152 RID: 4434
		private DirectiveStack _directives;

		// Token: 0x04001153 RID: 4435
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private ImmutableArray<int> _preprocessorStateChangePositions;

		// Token: 0x04001154 RID: 4436
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private ImmutableArray<DirectiveStack> _preprocessorStates;

		// Token: 0x04001155 RID: 4437
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private CSharpLineDirectiveMap _lazyLineDirectiveMap;

		// Token: 0x04001156 RID: 4438
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private CSharpPragmaWarningStateMap _lazyPragmaWarningStateMap;

		// Token: 0x04001157 RID: 4439
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private StrongBox<NullableContextStateMap> _lazyNullableContextStateMap;

		// Token: 0x04001158 RID: 4440
		private GeneratedKind _lazyIsGeneratedCode;

		// Token: 0x0200075A RID: 1882
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private class DebuggerSyntaxTree : CSharpSyntaxTree.ParsedSyntaxTree
		{
			// Token: 0x060086D6 RID: 34518 RVA: 0x001E91EC File Offset: 0x001E73EC
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			public DebuggerSyntaxTree(CSharpSyntaxNode root, SourceText text, CSharpParseOptions options)
				: base(text, text.Encoding, text.ChecksumAlgorithm, "", options, root, DirectiveStack.Empty, null, true)
			{
			}

			// Token: 0x170026D7 RID: 9943
			// (get) Token: 0x060086D7 RID: 34519 RVA: 0x001E921A File Offset: 0x001E741A
			internal override bool SupportsLocations
			{
				get
				{
					return true;
				}
			}
		}

		// Token: 0x0200075B RID: 1883
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal sealed class DummySyntaxTree : CSharpSyntaxTree
		{
			// Token: 0x060086D8 RID: 34520 RVA: 0x001E921D File Offset: 0x001E741D
			public DummySyntaxTree()
			{
				this._node = base.CloneNodeAsRoot<Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax>(SyntaxFactory.ParseCompilationUnit(string.Empty, 0, null));
			}

			// Token: 0x060086D9 RID: 34521 RVA: 0x001E923D File Offset: 0x001E743D
			public override string ToString()
			{
				return string.Empty;
			}

			// Token: 0x060086DA RID: 34522 RVA: 0x001E9244 File Offset: 0x001E7444
			public override SourceText GetText(CancellationToken cancellationToken)
			{
				return SourceText.From(string.Empty, Encoding.UTF8, SourceHashAlgorithm.Sha1);
			}

			// Token: 0x060086DB RID: 34523 RVA: 0x001E9256 File Offset: 0x001E7456
			public override bool TryGetText(out SourceText text)
			{
				text = SourceText.From(string.Empty, Encoding.UTF8, SourceHashAlgorithm.Sha1);
				return true;
			}

			// Token: 0x170026D8 RID: 9944
			// (get) Token: 0x060086DC RID: 34524 RVA: 0x001E926B File Offset: 0x001E746B
			public override Encoding Encoding
			{
				get
				{
					return Encoding.UTF8;
				}
			}

			// Token: 0x170026D9 RID: 9945
			// (get) Token: 0x060086DD RID: 34525 RVA: 0x001E9272 File Offset: 0x001E7472
			public override int Length
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x170026DA RID: 9946
			// (get) Token: 0x060086DE RID: 34526 RVA: 0x001E9275 File Offset: 0x001E7475
			public override CSharpParseOptions Options
			{
				get
				{
					return CSharpParseOptions.Default;
				}
			}

			// Token: 0x170026DB RID: 9947
			// (get) Token: 0x060086DF RID: 34527 RVA: 0x001E927C File Offset: 0x001E747C
			[Obsolete("Obsolete due to performance problems, use CompilationOptions.SyntaxTreeOptionsProvider instead", false)]
			public override ImmutableDictionary<string, ReportDiagnostic> DiagnosticOptions
			{
				get
				{
					throw ExceptionUtilities.Unreachable;
				}
			}

			// Token: 0x170026DC RID: 9948
			// (get) Token: 0x060086E0 RID: 34528 RVA: 0x001E9283 File Offset: 0x001E7483
			public override string FilePath
			{
				get
				{
					return string.Empty;
				}
			}

			// Token: 0x060086E1 RID: 34529 RVA: 0x001E928A File Offset: 0x001E748A
			public override SyntaxReference GetReference(SyntaxNode node)
			{
				return new SimpleSyntaxReference(node);
			}

			// Token: 0x060086E2 RID: 34530 RVA: 0x001E9292 File Offset: 0x001E7492
			public override CSharpSyntaxNode GetRoot(CancellationToken cancellationToken)
			{
				return this._node;
			}

			// Token: 0x060086E3 RID: 34531 RVA: 0x001E929A File Offset: 0x001E749A
			public override bool TryGetRoot(out CSharpSyntaxNode root)
			{
				root = this._node;
				return true;
			}

			// Token: 0x170026DD RID: 9949
			// (get) Token: 0x060086E4 RID: 34532 RVA: 0x001E92A5 File Offset: 0x001E74A5
			public override bool HasCompilationUnitRoot
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060086E5 RID: 34533 RVA: 0x001E92A8 File Offset: 0x001E74A8
			public override FileLinePositionSpan GetLineSpan(TextSpan span, CancellationToken cancellationToken = default(CancellationToken))
			{
				return default(FileLinePositionSpan);
			}

			// Token: 0x060086E6 RID: 34534 RVA: 0x001E92BE File Offset: 0x001E74BE
			public override SyntaxTree WithRootAndOptions(SyntaxNode root, ParseOptions options)
			{
				return SyntaxFactory.SyntaxTree(root, options, this.FilePath, null);
			}

			// Token: 0x060086E7 RID: 34535 RVA: 0x001E92CE File Offset: 0x001E74CE
			public override SyntaxTree WithFilePath(string path)
			{
				return SyntaxFactory.SyntaxTree(this._node, this.Options, path, null);
			}

			// Token: 0x04002363 RID: 9059
			private readonly Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax _node;
		}

		// Token: 0x0200075C RID: 1884
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private sealed class LazySyntaxTree : CSharpSyntaxTree
		{
			// Token: 0x060086E8 RID: 34536 RVA: 0x001E92E3 File Offset: 0x001E74E3
			internal LazySyntaxTree(SourceText text, CSharpParseOptions options, string path, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ImmutableDictionary<string, ReportDiagnostic> diagnosticOptions)
			{
				this._text = text;
				this._options = options;
				this._path = path ?? string.Empty;
				this._diagnosticOptions = diagnosticOptions ?? SyntaxTree.EmptyDiagnosticOptions;
			}

			// Token: 0x170026DE RID: 9950
			// (get) Token: 0x060086E9 RID: 34537 RVA: 0x001E931A File Offset: 0x001E751A
			public override string FilePath
			{
				get
				{
					return this._path;
				}
			}

			// Token: 0x060086EA RID: 34538 RVA: 0x001E9322 File Offset: 0x001E7522
			public override SourceText GetText(CancellationToken cancellationToken)
			{
				return this._text;
			}

			// Token: 0x060086EB RID: 34539 RVA: 0x001E932A File Offset: 0x001E752A
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			public override bool TryGetText([NotNullWhen(true)] out SourceText text)
			{
				text = this._text;
				return true;
			}

			// Token: 0x170026DF RID: 9951
			// (get) Token: 0x060086EC RID: 34540 RVA: 0x001E9335 File Offset: 0x001E7535
			[global::System.Runtime.CompilerServices.Nullable(2)]
			public override Encoding Encoding
			{
				[global::System.Runtime.CompilerServices.NullableContext(2)]
				get
				{
					return this._text.Encoding;
				}
			}

			// Token: 0x170026E0 RID: 9952
			// (get) Token: 0x060086ED RID: 34541 RVA: 0x001E9342 File Offset: 0x001E7542
			public override int Length
			{
				get
				{
					return this._text.Length;
				}
			}

			// Token: 0x060086EE RID: 34542 RVA: 0x001E9350 File Offset: 0x001E7550
			public override CSharpSyntaxNode GetRoot(CancellationToken cancellationToken)
			{
				if (this._lazyRoot == null)
				{
					SyntaxTree syntaxTree = SyntaxFactory.ParseSyntaxTree(this._text, this._options, this._path, cancellationToken);
					CSharpSyntaxNode csharpSyntaxNode = base.CloneNodeAsRoot<CSharpSyntaxNode>((CSharpSyntaxNode)syntaxTree.GetRoot(cancellationToken));
					Interlocked.CompareExchange<CSharpSyntaxNode>(ref this._lazyRoot, csharpSyntaxNode, null);
				}
				return this._lazyRoot;
			}

			// Token: 0x060086EF RID: 34543 RVA: 0x001E93A5 File Offset: 0x001E75A5
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			public override bool TryGetRoot([NotNullWhen(true)] out CSharpSyntaxNode root)
			{
				root = this._lazyRoot;
				return root != null;
			}

			// Token: 0x170026E1 RID: 9953
			// (get) Token: 0x060086F0 RID: 34544 RVA: 0x001E93B4 File Offset: 0x001E75B4
			public override bool HasCompilationUnitRoot
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170026E2 RID: 9954
			// (get) Token: 0x060086F1 RID: 34545 RVA: 0x001E93B7 File Offset: 0x001E75B7
			public override CSharpParseOptions Options
			{
				get
				{
					return this._options;
				}
			}

			// Token: 0x170026E3 RID: 9955
			// (get) Token: 0x060086F2 RID: 34546 RVA: 0x001E93BF File Offset: 0x001E75BF
			[Obsolete("Obsolete due to performance problems, use CompilationOptions.SyntaxTreeOptionsProvider instead", false)]
			public override ImmutableDictionary<string, ReportDiagnostic> DiagnosticOptions
			{
				get
				{
					return this._diagnosticOptions;
				}
			}

			// Token: 0x060086F3 RID: 34547 RVA: 0x001E93C7 File Offset: 0x001E75C7
			public override SyntaxReference GetReference(SyntaxNode node)
			{
				return new SimpleSyntaxReference(node);
			}

			// Token: 0x060086F4 RID: 34548 RVA: 0x001E93D0 File Offset: 0x001E75D0
			public override SyntaxTree WithRootAndOptions(SyntaxNode root, ParseOptions options)
			{
				if (this._lazyRoot == root && this._options == options)
				{
					return this;
				}
				return new CSharpSyntaxTree.ParsedSyntaxTree(null, this._text.Encoding, this._text.ChecksumAlgorithm, this._path, (CSharpParseOptions)options, (CSharpSyntaxNode)root, this._directives, this._diagnosticOptions, true);
			}

			// Token: 0x060086F5 RID: 34549 RVA: 0x001E942C File Offset: 0x001E762C
			public override SyntaxTree WithFilePath(string path)
			{
				if (this._path == path)
				{
					return this;
				}
				CSharpSyntaxNode csharpSyntaxNode;
				if (this.TryGetRoot(out csharpSyntaxNode))
				{
					return new CSharpSyntaxTree.ParsedSyntaxTree(this._text, this._text.Encoding, this._text.ChecksumAlgorithm, path, this._options, csharpSyntaxNode, base.GetDirectives(), this._diagnosticOptions, true);
				}
				return new CSharpSyntaxTree.LazySyntaxTree(this._text, this._options, path, this._diagnosticOptions);
			}

			// Token: 0x060086F6 RID: 34550 RVA: 0x001E94A4 File Offset: 0x001E76A4
			[Obsolete("Obsolete due to performance problems, use CompilationOptions.SyntaxTreeOptionsProvider instead", false)]
			public override SyntaxTree WithDiagnosticOptions(ImmutableDictionary<string, ReportDiagnostic> options)
			{
				if (options == null)
				{
					options = SyntaxTree.EmptyDiagnosticOptions;
				}
				if (this._diagnosticOptions == options)
				{
					return this;
				}
				CSharpSyntaxNode csharpSyntaxNode;
				if (this.TryGetRoot(out csharpSyntaxNode))
				{
					return new CSharpSyntaxTree.ParsedSyntaxTree(this._text, this._text.Encoding, this._text.ChecksumAlgorithm, this._path, this._options, csharpSyntaxNode, base.GetDirectives(), options, true);
				}
				return new CSharpSyntaxTree.LazySyntaxTree(this._text, this._options, this._path, options);
			}

			// Token: 0x04002364 RID: 9060
			private readonly SourceText _text;

			// Token: 0x04002365 RID: 9061
			private readonly CSharpParseOptions _options;

			// Token: 0x04002366 RID: 9062
			private readonly string _path;

			// Token: 0x04002367 RID: 9063
			private readonly ImmutableDictionary<string, ReportDiagnostic> _diagnosticOptions;

			// Token: 0x04002368 RID: 9064
			[global::System.Runtime.CompilerServices.Nullable(2)]
			private CSharpSyntaxNode _lazyRoot;
		}

		// Token: 0x0200075D RID: 1885
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private class ParsedSyntaxTree : CSharpSyntaxTree
		{
			// Token: 0x060086F7 RID: 34551 RVA: 0x001E9520 File Offset: 0x001E7720
			internal ParsedSyntaxTree([global::System.Runtime.CompilerServices.Nullable(2)] SourceText textOpt, [global::System.Runtime.CompilerServices.Nullable(2)] Encoding encodingOpt, SourceHashAlgorithm checksumAlgorithm, string path, CSharpParseOptions options, CSharpSyntaxNode root, DirectiveStack directives, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ImmutableDictionary<string, ReportDiagnostic> diagnosticOptions, bool cloneRoot)
			{
				this._lazyText = textOpt;
				this._encodingOpt = encodingOpt ?? ((textOpt != null) ? textOpt.Encoding : null);
				this._checksumAlgorithm = checksumAlgorithm;
				this._options = options;
				this._path = path ?? string.Empty;
				this._root = (cloneRoot ? base.CloneNodeAsRoot<CSharpSyntaxNode>(root) : root);
				this._hasCompilationUnitRoot = root.Kind() == SyntaxKind.CompilationUnit;
				this._diagnosticOptions = diagnosticOptions ?? SyntaxTree.EmptyDiagnosticOptions;
				base.SetDirectiveStack(directives);
			}

			// Token: 0x170026E4 RID: 9956
			// (get) Token: 0x060086F8 RID: 34552 RVA: 0x001E95B4 File Offset: 0x001E77B4
			public override string FilePath
			{
				get
				{
					return this._path;
				}
			}

			// Token: 0x060086F9 RID: 34553 RVA: 0x001E95BC File Offset: 0x001E77BC
			public override SourceText GetText(CancellationToken cancellationToken)
			{
				if (this._lazyText == null)
				{
					Interlocked.CompareExchange<SourceText>(ref this._lazyText, this.GetRoot(cancellationToken).GetText(this._encodingOpt, this._checksumAlgorithm), null);
				}
				return this._lazyText;
			}

			// Token: 0x060086FA RID: 34554 RVA: 0x001E95F1 File Offset: 0x001E77F1
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			public override bool TryGetText([NotNullWhen(true)] out SourceText text)
			{
				text = this._lazyText;
				return text != null;
			}

			// Token: 0x170026E5 RID: 9957
			// (get) Token: 0x060086FB RID: 34555 RVA: 0x001E9600 File Offset: 0x001E7800
			[global::System.Runtime.CompilerServices.Nullable(2)]
			public override Encoding Encoding
			{
				[global::System.Runtime.CompilerServices.NullableContext(2)]
				get
				{
					return this._encodingOpt;
				}
			}

			// Token: 0x170026E6 RID: 9958
			// (get) Token: 0x060086FC RID: 34556 RVA: 0x001E9608 File Offset: 0x001E7808
			public override int Length
			{
				get
				{
					return this._root.FullSpan.Length;
				}
			}

			// Token: 0x060086FD RID: 34557 RVA: 0x001E9628 File Offset: 0x001E7828
			public override CSharpSyntaxNode GetRoot(CancellationToken cancellationToken)
			{
				return this._root;
			}

			// Token: 0x060086FE RID: 34558 RVA: 0x001E9630 File Offset: 0x001E7830
			public override bool TryGetRoot(out CSharpSyntaxNode root)
			{
				root = this._root;
				return true;
			}

			// Token: 0x170026E7 RID: 9959
			// (get) Token: 0x060086FF RID: 34559 RVA: 0x001E963B File Offset: 0x001E783B
			public override bool HasCompilationUnitRoot
			{
				get
				{
					return this._hasCompilationUnitRoot;
				}
			}

			// Token: 0x170026E8 RID: 9960
			// (get) Token: 0x06008700 RID: 34560 RVA: 0x001E9643 File Offset: 0x001E7843
			public override CSharpParseOptions Options
			{
				get
				{
					return this._options;
				}
			}

			// Token: 0x170026E9 RID: 9961
			// (get) Token: 0x06008701 RID: 34561 RVA: 0x001E964B File Offset: 0x001E784B
			[Obsolete("Obsolete due to performance problems, use CompilationOptions.SyntaxTreeOptionsProvider instead", false)]
			public override ImmutableDictionary<string, ReportDiagnostic> DiagnosticOptions
			{
				get
				{
					return this._diagnosticOptions;
				}
			}

			// Token: 0x06008702 RID: 34562 RVA: 0x001E9653 File Offset: 0x001E7853
			public override SyntaxReference GetReference(SyntaxNode node)
			{
				return new SimpleSyntaxReference(node);
			}

			// Token: 0x06008703 RID: 34563 RVA: 0x001E965C File Offset: 0x001E785C
			public override SyntaxTree WithRootAndOptions(SyntaxNode root, ParseOptions options)
			{
				if (this._root == root && this._options == options)
				{
					return this;
				}
				return new CSharpSyntaxTree.ParsedSyntaxTree(null, this._encodingOpt, this._checksumAlgorithm, this._path, (CSharpParseOptions)options, (CSharpSyntaxNode)root, this._directives, this._diagnosticOptions, true);
			}

			// Token: 0x06008704 RID: 34564 RVA: 0x001E96B0 File Offset: 0x001E78B0
			public override SyntaxTree WithFilePath(string path)
			{
				if (this._path == path)
				{
					return this;
				}
				return new CSharpSyntaxTree.ParsedSyntaxTree(this._lazyText, this._encodingOpt, this._checksumAlgorithm, path, this._options, this._root, this._directives, this._diagnosticOptions, true);
			}

			// Token: 0x06008705 RID: 34565 RVA: 0x001E9700 File Offset: 0x001E7900
			[Obsolete("Obsolete due to performance problems, use CompilationOptions.SyntaxTreeOptionsProvider instead", false)]
			public override SyntaxTree WithDiagnosticOptions(ImmutableDictionary<string, ReportDiagnostic> options)
			{
				if (options == null)
				{
					options = SyntaxTree.EmptyDiagnosticOptions;
				}
				if (this._diagnosticOptions == options)
				{
					return this;
				}
				return new CSharpSyntaxTree.ParsedSyntaxTree(this._lazyText, this._encodingOpt, this._checksumAlgorithm, this._path, this._options, this._root, this._directives, options, true);
			}

			// Token: 0x04002369 RID: 9065
			private readonly CSharpParseOptions _options;

			// Token: 0x0400236A RID: 9066
			private readonly string _path;

			// Token: 0x0400236B RID: 9067
			private readonly CSharpSyntaxNode _root;

			// Token: 0x0400236C RID: 9068
			private readonly bool _hasCompilationUnitRoot;

			// Token: 0x0400236D RID: 9069
			[global::System.Runtime.CompilerServices.Nullable(2)]
			private readonly Encoding _encodingOpt;

			// Token: 0x0400236E RID: 9070
			private readonly SourceHashAlgorithm _checksumAlgorithm;

			// Token: 0x0400236F RID: 9071
			private readonly ImmutableDictionary<string, ReportDiagnostic> _diagnosticOptions;

			// Token: 0x04002370 RID: 9072
			[global::System.Runtime.CompilerServices.Nullable(2)]
			private SourceText _lazyText;
		}
	}
}
