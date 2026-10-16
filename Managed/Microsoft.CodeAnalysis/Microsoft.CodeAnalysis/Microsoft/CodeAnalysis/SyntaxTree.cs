using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000284 RID: 644
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class SyntaxTree
	{
		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001A4E RID: 6734
		public abstract string FilePath { get; }

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001A4F RID: 6735
		public abstract bool HasCompilationUnitRoot { get; }

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001A50 RID: 6736 RVA: 0x0004E668 File Offset: 0x0004C868
		public ParseOptions Options
		{
			get
			{
				return this.OptionsCore;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001A51 RID: 6737
		protected abstract ParseOptions OptionsCore { get; }

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001A52 RID: 6738 RVA: 0x0004E670 File Offset: 0x0004C870
		[Obsolete("Obsolete due to performance problems, use CompilationOptions.SyntaxTreeOptionsProvider instead", false)]
		public virtual ImmutableDictionary<string, ReportDiagnostic> DiagnosticOptions
		{
			get
			{
				return SyntaxTree.EmptyDiagnosticOptions;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001A53 RID: 6739
		public abstract int Length { get; }

		// Token: 0x06001A54 RID: 6740
		[NullableContext(2)]
		public abstract bool TryGetText([NotNullWhen(true)] out SourceText text);

		// Token: 0x06001A55 RID: 6741
		public abstract SourceText GetText(CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001A56 RID: 6742
		[Nullable(2)]
		public abstract Encoding Encoding
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x0004E678 File Offset: 0x0004C878
		public virtual Task<SourceText> GetTextAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			SourceText sourceText;
			return Task.FromResult<SourceText>(this.TryGetText(out sourceText) ? sourceText : this.GetText(cancellationToken));
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x0004E69E File Offset: 0x0004C89E
		[NullableContext(2)]
		public bool TryGetRoot([NotNullWhen(true)] out SyntaxNode root)
		{
			return this.TryGetRootCore(out root);
		}

		// Token: 0x06001A59 RID: 6745
		[NullableContext(2)]
		protected abstract bool TryGetRootCore([NotNullWhen(true)] out SyntaxNode root);

		// Token: 0x06001A5A RID: 6746 RVA: 0x0004E6A7 File Offset: 0x0004C8A7
		public SyntaxNode GetRoot(CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.GetRootCore(cancellationToken);
		}

		// Token: 0x06001A5B RID: 6747
		protected abstract SyntaxNode GetRootCore(CancellationToken cancellationToken);

		// Token: 0x06001A5C RID: 6748 RVA: 0x0004E6B0 File Offset: 0x0004C8B0
		public Task<SyntaxNode> GetRootAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.GetRootAsyncCore(cancellationToken);
		}

		// Token: 0x06001A5D RID: 6749
		protected abstract Task<SyntaxNode> GetRootAsyncCore(CancellationToken cancellationToken);

		// Token: 0x06001A5E RID: 6750
		public abstract SyntaxTree WithChangedText(SourceText newText);

		// Token: 0x06001A5F RID: 6751
		public abstract IEnumerable<Diagnostic> GetDiagnostics(CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06001A60 RID: 6752
		public abstract IEnumerable<Diagnostic> GetDiagnostics(SyntaxNode node);

		// Token: 0x06001A61 RID: 6753
		public abstract IEnumerable<Diagnostic> GetDiagnostics(SyntaxToken token);

		// Token: 0x06001A62 RID: 6754
		public abstract IEnumerable<Diagnostic> GetDiagnostics(SyntaxTrivia trivia);

		// Token: 0x06001A63 RID: 6755
		public abstract IEnumerable<Diagnostic> GetDiagnostics(SyntaxNodeOrToken nodeOrToken);

		// Token: 0x06001A64 RID: 6756
		public abstract FileLinePositionSpan GetLineSpan(TextSpan span, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06001A65 RID: 6757
		public abstract FileLinePositionSpan GetMappedLineSpan(TextSpan span, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06001A66 RID: 6758 RVA: 0x0004E6B9 File Offset: 0x0004C8B9
		public virtual LineVisibility GetLineVisibility(int position, CancellationToken cancellationToken = default(CancellationToken))
		{
			return LineVisibility.Visible;
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x0004E6BC File Offset: 0x0004C8BC
		internal virtual FileLinePositionSpan GetMappedLineSpanAndVisibility(TextSpan span, out bool isHiddenPosition)
		{
			isHiddenPosition = this.GetLineVisibility(span.Start, default(CancellationToken)) == LineVisibility.Hidden;
			return this.GetMappedLineSpan(span, default(CancellationToken));
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x0004E6F4 File Offset: 0x0004C8F4
		internal string GetDisplayPath(TextSpan span, [Nullable(2)] SourceReferenceResolver resolver)
		{
			FileLinePositionSpan mappedLineSpan = this.GetMappedLineSpan(span, default(CancellationToken));
			if (resolver == null || mappedLineSpan.Path.IsEmpty())
			{
				return mappedLineSpan.Path;
			}
			return resolver.NormalizePath(mappedLineSpan.Path, mappedLineSpan.HasMappedPath ? this.FilePath : null) ?? mappedLineSpan.Path;
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x0004E758 File Offset: 0x0004C958
		internal int GetDisplayLineNumber(TextSpan span)
		{
			return this.GetMappedLineSpan(span, default(CancellationToken)).StartLinePosition.Line + 1;
		}

		// Token: 0x06001A6A RID: 6762
		public abstract bool HasHiddenRegions();

		// Token: 0x06001A6B RID: 6763
		public abstract IList<TextSpan> GetChangedSpans(SyntaxTree syntaxTree);

		// Token: 0x06001A6C RID: 6764
		public abstract Location GetLocation(TextSpan span);

		// Token: 0x06001A6D RID: 6765
		public abstract bool IsEquivalentTo(SyntaxTree tree, bool topLevel = false);

		// Token: 0x06001A6E RID: 6766
		public abstract SyntaxReference GetReference(SyntaxNode node);

		// Token: 0x06001A6F RID: 6767
		public abstract IList<TextChange> GetChanges(SyntaxTree oldTree);

		// Token: 0x06001A70 RID: 6768 RVA: 0x0004E788 File Offset: 0x0004C988
		internal DebugSourceInfo GetDebugSourceInfo()
		{
			if (this._lazyChecksum.IsDefault)
			{
				SourceText text = this.GetText(default(CancellationToken));
				this._lazyChecksum = text.GetChecksum();
				this._lazyHashAlgorithm = text.ChecksumAlgorithm;
			}
			return new DebugSourceInfo(this._lazyChecksum, this._lazyHashAlgorithm, default(ImmutableArray<byte>));
		}

		// Token: 0x06001A71 RID: 6769
		public abstract SyntaxTree WithRootAndOptions(SyntaxNode root, ParseOptions options);

		// Token: 0x06001A72 RID: 6770
		public abstract SyntaxTree WithFilePath(string path);

		// Token: 0x06001A73 RID: 6771 RVA: 0x0004E7E4 File Offset: 0x0004C9E4
		[Obsolete("Obsolete due to performance problems, use CompilationOptions.SyntaxTreeOptionsProvider instead", false)]
		public virtual SyntaxTree WithDiagnosticOptions(ImmutableDictionary<string, ReportDiagnostic> options)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x0004E7EB File Offset: 0x0004C9EB
		public override string ToString()
		{
			return this.GetText(CancellationToken.None).ToString();
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001A75 RID: 6773 RVA: 0x0004E7FD File Offset: 0x0004C9FD
		internal virtual bool SupportsLocations
		{
			get
			{
				return this.HasCompilationUnitRoot;
			}
		}

		// Token: 0x04000A36 RID: 2614
		protected internal static readonly ImmutableDictionary<string, ReportDiagnostic> EmptyDiagnosticOptions = ImmutableDictionary.Create<string, ReportDiagnostic>(CaseInsensitiveComparison.Comparer);

		// Token: 0x04000A37 RID: 2615
		[Nullable(0)]
		private ImmutableArray<byte> _lazyChecksum;

		// Token: 0x04000A38 RID: 2616
		private SourceHashAlgorithm _lazyHashAlgorithm;
	}
}
