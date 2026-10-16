using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000328 RID: 808
	[NullableContext(1)]
	[Nullable(0)]
	public struct SuppressionAnalysisContext
	{
		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06002365 RID: 9061 RVA: 0x0006F9DD File Offset: 0x0006DBDD
		[Nullable(new byte[] { 0, 1 })]
		public readonly ImmutableArray<Diagnostic> ReportedDiagnostics
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06002366 RID: 9062 RVA: 0x0006F9E5 File Offset: 0x0006DBE5
		public readonly Compilation Compilation { get; }

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06002367 RID: 9063 RVA: 0x0006F9ED File Offset: 0x0006DBED
		public readonly AnalyzerOptions Options { get; }

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06002368 RID: 9064 RVA: 0x0006F9F5 File Offset: 0x0006DBF5
		public readonly CancellationToken CancellationToken { get; }

		// Token: 0x06002369 RID: 9065 RVA: 0x0006F9FD File Offset: 0x0006DBFD
		internal SuppressionAnalysisContext(Compilation compilation, AnalyzerOptions options, [Nullable(new byte[] { 0, 1 })] ImmutableArray<Diagnostic> reportedDiagnostics, Action<Suppression> suppressDiagnostic, Func<SuppressionDescriptor, bool> isSupportedSuppressionDescriptor, Func<SyntaxTree, SemanticModel> getSemanticModel, CancellationToken cancellationToken)
		{
			this.Compilation = compilation;
			this.Options = options;
			this.ReportedDiagnostics = reportedDiagnostics;
			this._addSuppression = suppressDiagnostic;
			this._isSupportedSuppressionDescriptor = isSupportedSuppressionDescriptor;
			this._getSemanticModel = getSemanticModel;
			this.CancellationToken = cancellationToken;
		}

		// Token: 0x0600236A RID: 9066 RVA: 0x0006FA34 File Offset: 0x0006DC34
		public void ReportSuppression(Suppression suppression)
		{
			if (!this.ReportedDiagnostics.Contains(suppression.SuppressedDiagnostic))
			{
				throw new ArgumentException(string.Format(CodeAnalysisResources.NonReportedDiagnosticCannotBeSuppressed, suppression.SuppressedDiagnostic.Id));
			}
			if (!this._isSupportedSuppressionDescriptor.Invoke(suppression.Descriptor))
			{
				throw new ArgumentException(string.Format(CodeAnalysisResources.UnsupportedSuppressionReported, suppression.Descriptor.Id));
			}
			if (suppression.Descriptor.IsDisabled(this.Compilation.Options))
			{
				return;
			}
			this._addSuppression.Invoke(suppression);
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x0006FACA File Offset: 0x0006DCCA
		public SemanticModel GetSemanticModel(SyntaxTree syntaxTree)
		{
			return this._getSemanticModel.Invoke(syntaxTree);
		}

		// Token: 0x04000F39 RID: 3897
		private readonly Action<Suppression> _addSuppression;

		// Token: 0x04000F3A RID: 3898
		private readonly Func<SuppressionDescriptor, bool> _isSupportedSuppressionDescriptor;

		// Token: 0x04000F3B RID: 3899
		private readonly Func<SyntaxTree, SemanticModel> _getSemanticModel;
	}
}
