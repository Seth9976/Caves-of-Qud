using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000317 RID: 791
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class CompilerAnalyzerConfigOptionsProvider : AnalyzerConfigOptionsProvider
	{
		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x060022C5 RID: 8901 RVA: 0x0006ED7E File Offset: 0x0006CF7E
		public static CompilerAnalyzerConfigOptionsProvider Empty { get; } = new CompilerAnalyzerConfigOptionsProvider(ImmutableDictionary<object, AnalyzerConfigOptions>.Empty, CompilerAnalyzerConfigOptions.Empty);

		// Token: 0x060022C6 RID: 8902 RVA: 0x0006ED85 File Offset: 0x0006CF85
		internal CompilerAnalyzerConfigOptionsProvider(ImmutableDictionary<object, AnalyzerConfigOptions> treeDict, AnalyzerConfigOptions globalOptions)
		{
			this._treeDict = treeDict;
			this.GlobalOptions = globalOptions;
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x060022C7 RID: 8903 RVA: 0x0006ED9B File Offset: 0x0006CF9B
		public override AnalyzerConfigOptions GlobalOptions { get; }

		// Token: 0x060022C8 RID: 8904 RVA: 0x0006EDA4 File Offset: 0x0006CFA4
		public override AnalyzerConfigOptions GetOptions(SyntaxTree tree)
		{
			AnalyzerConfigOptions analyzerConfigOptions;
			if (!this._treeDict.TryGetValue(tree, out analyzerConfigOptions))
			{
				return CompilerAnalyzerConfigOptions.Empty;
			}
			return analyzerConfigOptions;
		}

		// Token: 0x060022C9 RID: 8905 RVA: 0x0006EDC8 File Offset: 0x0006CFC8
		public override AnalyzerConfigOptions GetOptions(AdditionalText textFile)
		{
			AnalyzerConfigOptions analyzerConfigOptions;
			if (!this._treeDict.TryGetValue(textFile, out analyzerConfigOptions))
			{
				return CompilerAnalyzerConfigOptions.Empty;
			}
			return analyzerConfigOptions;
		}

		// Token: 0x060022CA RID: 8906 RVA: 0x0006EDEC File Offset: 0x0006CFEC
		internal CompilerAnalyzerConfigOptionsProvider WithAdditionalTreeOptions(ImmutableDictionary<object, AnalyzerConfigOptions> treeDict)
		{
			return new CompilerAnalyzerConfigOptionsProvider(this._treeDict.AddRange(treeDict), this.GlobalOptions);
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x0006EE05 File Offset: 0x0006D005
		internal CompilerAnalyzerConfigOptionsProvider WithGlobalOptions(AnalyzerConfigOptions globalOptions)
		{
			return new CompilerAnalyzerConfigOptionsProvider(this._treeDict, globalOptions);
		}

		// Token: 0x04000EE0 RID: 3808
		private readonly ImmutableDictionary<object, AnalyzerConfigOptions> _treeDict;
	}
}
