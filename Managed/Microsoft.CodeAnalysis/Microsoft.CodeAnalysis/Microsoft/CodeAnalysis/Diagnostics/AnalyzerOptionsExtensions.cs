using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x0200030B RID: 779
	[NullableContext(1)]
	[Nullable(0)]
	internal static class AnalyzerOptionsExtensions
	{
		// Token: 0x06002243 RID: 8771 RVA: 0x0006D120 File Offset: 0x0006B320
		private static string GetCategoryBasedDotnetAnalyzerDiagnosticSeverityKey(string category)
		{
			return "dotnet_analyzer_diagnostic.category-" + category + ".severity";
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x0006D134 File Offset: 0x0006B334
		public static bool TryGetSeverityFromBulkConfiguration([Nullable(2)] this AnalyzerOptions analyzerOptions, SyntaxTree tree, Compilation compilation, DiagnosticDescriptor descriptor, CancellationToken cancellationToken, out ReportDiagnostic severity)
		{
			if (analyzerOptions != null && descriptor.IsEnabledByDefault)
			{
				if (!descriptor.CustomTags.Contains<string>((string tag) => tag == "Compiler" || tag == "NotConfigurable"))
				{
					if (!compilation.Options.SpecificDiagnosticOptions.ContainsKey(descriptor.Id))
					{
						SyntaxTreeOptionsProvider syntaxTreeOptionsProvider = compilation.Options.SyntaxTreeOptionsProvider;
						ReportDiagnostic reportDiagnostic;
						if (syntaxTreeOptionsProvider == null || !syntaxTreeOptionsProvider.TryGetDiagnosticValue(tree, descriptor.Id, cancellationToken, out reportDiagnostic))
						{
							SyntaxTreeOptionsProvider syntaxTreeOptionsProvider2 = compilation.Options.SyntaxTreeOptionsProvider;
							if (syntaxTreeOptionsProvider2 == null || !syntaxTreeOptionsProvider2.TryGetGlobalDiagnosticValue(descriptor.Id, cancellationToken, out reportDiagnostic))
							{
								AnalyzerConfigOptions options = analyzerOptions.AnalyzerConfigOptionsProvider.GetOptions(tree);
								string categoryBasedDotnetAnalyzerDiagnosticSeverityKey = AnalyzerOptionsExtensions.GetCategoryBasedDotnetAnalyzerDiagnosticSeverityKey(descriptor.Category);
								string text;
								if (options.TryGetValue(categoryBasedDotnetAnalyzerDiagnosticSeverityKey, out text) && AnalyzerConfigSet.TryParseSeverity(text, out severity))
								{
									return true;
								}
								if (options.TryGetValue("dotnet_analyzer_diagnostic.severity", out text) && AnalyzerConfigSet.TryParseSeverity(text, out severity))
								{
									return true;
								}
								severity = ReportDiagnostic.Default;
								return false;
							}
						}
					}
					severity = ReportDiagnostic.Default;
					return false;
				}
			}
			severity = ReportDiagnostic.Default;
			return false;
		}

		// Token: 0x04000EB8 RID: 3768
		private const string DotnetAnalyzerDiagnosticPrefix = "dotnet_analyzer_diagnostic";

		// Token: 0x04000EB9 RID: 3769
		private const string CategoryPrefix = "category";

		// Token: 0x04000EBA RID: 3770
		private const string SeveritySuffix = "severity";

		// Token: 0x04000EBB RID: 3771
		private const string DotnetAnalyzerDiagnosticSeverityKey = "dotnet_analyzer_diagnostic.severity";
	}
}
