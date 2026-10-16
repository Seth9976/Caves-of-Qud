using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000316 RID: 790
	[NullableContext(2)]
	[Nullable(0)]
	public sealed class CompilationWithAnalyzersOptions
	{
		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x060022BC RID: 8892 RVA: 0x0006ECFB File Offset: 0x0006CEFB
		public AnalyzerOptions Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x060022BD RID: 8893 RVA: 0x0006ED03 File Offset: 0x0006CF03
		[Nullable(new byte[] { 2, 1, 1, 1 })]
		public Action<Exception, DiagnosticAnalyzer, Diagnostic> OnAnalyzerException
		{
			[return: Nullable(new byte[] { 2, 1, 1, 1 })]
			get
			{
				return this._onAnalyzerException;
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x060022BE RID: 8894 RVA: 0x0006ED0B File Offset: 0x0006CF0B
		[Nullable(new byte[] { 2, 1 })]
		public Func<Exception, bool> AnalyzerExceptionFilter
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return this._analyzerExceptionFilter;
			}
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x060022BF RID: 8895 RVA: 0x0006ED13 File Offset: 0x0006CF13
		public bool ConcurrentAnalysis
		{
			get
			{
				return this._concurrentAnalysis;
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x060022C0 RID: 8896 RVA: 0x0006ED1B File Offset: 0x0006CF1B
		public bool LogAnalyzerExecutionTime
		{
			get
			{
				return this._logAnalyzerExecutionTime;
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x060022C1 RID: 8897 RVA: 0x0006ED23 File Offset: 0x0006CF23
		public bool ReportSuppressedDiagnostics
		{
			get
			{
				return this._reportSuppressedDiagnostics;
			}
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x0006ED2B File Offset: 0x0006CF2B
		[NullableContext(1)]
		public CompilationWithAnalyzersOptions(AnalyzerOptions options, [Nullable(new byte[] { 2, 1, 1, 1 })] Action<Exception, DiagnosticAnalyzer, Diagnostic> onAnalyzerException, bool concurrentAnalysis, bool logAnalyzerExecutionTime)
			: this(options, onAnalyzerException, concurrentAnalysis, logAnalyzerExecutionTime, false)
		{
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x0006ED39 File Offset: 0x0006CF39
		[NullableContext(1)]
		public CompilationWithAnalyzersOptions(AnalyzerOptions options, [Nullable(new byte[] { 2, 1, 1, 1 })] Action<Exception, DiagnosticAnalyzer, Diagnostic> onAnalyzerException, bool concurrentAnalysis, bool logAnalyzerExecutionTime, bool reportSuppressedDiagnostics)
			: this(options, onAnalyzerException, concurrentAnalysis, logAnalyzerExecutionTime, reportSuppressedDiagnostics, null)
		{
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x0006ED49 File Offset: 0x0006CF49
		public CompilationWithAnalyzersOptions(AnalyzerOptions options, [Nullable(new byte[] { 2, 1, 1, 1 })] Action<Exception, DiagnosticAnalyzer, Diagnostic> onAnalyzerException, bool concurrentAnalysis, bool logAnalyzerExecutionTime, bool reportSuppressedDiagnostics, [Nullable(new byte[] { 2, 1 })] Func<Exception, bool> analyzerExceptionFilter)
		{
			this._options = options;
			this._onAnalyzerException = onAnalyzerException;
			this._analyzerExceptionFilter = analyzerExceptionFilter;
			this._concurrentAnalysis = concurrentAnalysis;
			this._logAnalyzerExecutionTime = logAnalyzerExecutionTime;
			this._reportSuppressedDiagnostics = reportSuppressedDiagnostics;
		}

		// Token: 0x04000EDA RID: 3802
		private readonly AnalyzerOptions _options;

		// Token: 0x04000EDB RID: 3803
		[Nullable(new byte[] { 2, 1, 1, 1 })]
		private readonly Action<Exception, DiagnosticAnalyzer, Diagnostic> _onAnalyzerException;

		// Token: 0x04000EDC RID: 3804
		[Nullable(new byte[] { 2, 1 })]
		private readonly Func<Exception, bool> _analyzerExceptionFilter;

		// Token: 0x04000EDD RID: 3805
		private readonly bool _concurrentAnalysis;

		// Token: 0x04000EDE RID: 3806
		private readonly bool _logAnalyzerExecutionTime;

		// Token: 0x04000EDF RID: 3807
		private readonly bool _reportSuppressedDiagnostics;
	}
}
