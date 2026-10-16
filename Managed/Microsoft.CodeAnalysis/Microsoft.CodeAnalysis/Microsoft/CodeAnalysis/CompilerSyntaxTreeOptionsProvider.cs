using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000158 RID: 344
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class CompilerSyntaxTreeOptionsProvider : SyntaxTreeOptionsProvider
	{
		// Token: 0x06000DCF RID: 3535 RVA: 0x000279E8 File Offset: 0x00025BE8
		[NullableContext(0)]
		public CompilerSyntaxTreeOptionsProvider([Nullable(new byte[] { 1, 2 })] SyntaxTree[] trees, ImmutableArray<AnalyzerConfigOptionsResult> results, AnalyzerConfigOptionsResult globalResults)
		{
			ImmutableDictionary<SyntaxTree, CompilerSyntaxTreeOptionsProvider.Options>.Builder builder = ImmutableDictionary.CreateBuilder<SyntaxTree, CompilerSyntaxTreeOptionsProvider.Options>();
			for (int i = 0; i < trees.Length; i++)
			{
				if (trees[i] != null)
				{
					builder.Add(trees[i], new CompilerSyntaxTreeOptionsProvider.Options(results.IsDefault ? default(AnalyzerConfigOptionsResult?) : new AnalyzerConfigOptionsResult?(results[i])));
				}
			}
			this._options = builder.ToImmutableDictionary<SyntaxTree, CompilerSyntaxTreeOptionsProvider.Options>();
			this._globalOptions = globalResults;
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x00027A58 File Offset: 0x00025C58
		public override GeneratedKind IsGenerated(SyntaxTree tree, CancellationToken _)
		{
			CompilerSyntaxTreeOptionsProvider.Options options;
			if (!this._options.TryGetValue(tree, out options))
			{
				return GeneratedKind.Unknown;
			}
			return options.IsGenerated;
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x00027A80 File Offset: 0x00025C80
		public override bool TryGetDiagnosticValue(SyntaxTree tree, string diagnosticId, CancellationToken _, out ReportDiagnostic severity)
		{
			CompilerSyntaxTreeOptionsProvider.Options options;
			if (this._options.TryGetValue(tree, out options))
			{
				return options.DiagnosticOptions.TryGetValue(diagnosticId, out severity);
			}
			severity = ReportDiagnostic.Default;
			return false;
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x00027AB1 File Offset: 0x00025CB1
		public override bool TryGetGlobalDiagnosticValue(string diagnosticId, CancellationToken _, out ReportDiagnostic severity)
		{
			if (this._globalOptions.TreeOptions != null)
			{
				return this._globalOptions.TreeOptions.TryGetValue(diagnosticId, out severity);
			}
			severity = ReportDiagnostic.Default;
			return false;
		}

		// Token: 0x040003C8 RID: 968
		private readonly ImmutableDictionary<SyntaxTree, CompilerSyntaxTreeOptionsProvider.Options> _options;

		// Token: 0x040003C9 RID: 969
		private readonly AnalyzerConfigOptionsResult _globalOptions;

		// Token: 0x02000588 RID: 1416
		[NullableContext(0)]
		private readonly struct Options
		{
			// Token: 0x060034E8 RID: 13544 RVA: 0x00095700 File Offset: 0x00093900
			public Options(AnalyzerConfigOptionsResult? result)
			{
				if (result != null)
				{
					AnalyzerConfigOptionsResult valueOrDefault = result.GetValueOrDefault();
					this.DiagnosticOptions = valueOrDefault.TreeOptions;
					this.IsGenerated = GeneratedCodeUtilities.GetIsGeneratedCodeFromOptions(valueOrDefault.AnalyzerOptions);
					return;
				}
				this.DiagnosticOptions = SyntaxTree.EmptyDiagnosticOptions;
				this.IsGenerated = GeneratedKind.Unknown;
			}

			// Token: 0x040015EE RID: 5614
			public readonly GeneratedKind IsGenerated;

			// Token: 0x040015EF RID: 5615
			[Nullable(1)]
			public readonly ImmutableDictionary<string, ReportDiagnostic> DiagnosticOptions;
		}
	}
}
