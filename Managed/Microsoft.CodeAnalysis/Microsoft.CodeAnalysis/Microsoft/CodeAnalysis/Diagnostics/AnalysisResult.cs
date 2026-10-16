using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Diagnostics.Telemetry;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x020002FB RID: 763
	[NullableContext(1)]
	[Nullable(0)]
	public class AnalysisResult
	{
		// Token: 0x060020A7 RID: 8359 RVA: 0x00064A78 File Offset: 0x00062C78
		internal AnalysisResult([Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, [Nullable(new byte[] { 1, 1, 1, 1, 0, 1 })] ImmutableDictionary<SyntaxTree, ImmutableDictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>>> localSyntaxDiagnostics, [Nullable(new byte[] { 1, 1, 1, 1, 0, 1 })] ImmutableDictionary<SyntaxTree, ImmutableDictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>>> localSemanticDiagnostics, [Nullable(new byte[] { 1, 1, 1, 1, 0, 1 })] ImmutableDictionary<AdditionalText, ImmutableDictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>>> localAdditionalFileDiagnostics, [Nullable(new byte[] { 1, 1, 0, 1 })] ImmutableDictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>> nonLocalDiagnostics, ImmutableDictionary<DiagnosticAnalyzer, AnalyzerTelemetryInfo> analyzerTelemetryInfo)
		{
			this.Analyzers = analyzers;
			this.SyntaxDiagnostics = localSyntaxDiagnostics;
			this.SemanticDiagnostics = localSemanticDiagnostics;
			this.AdditionalFileDiagnostics = localAdditionalFileDiagnostics;
			this.CompilationDiagnostics = nonLocalDiagnostics;
			this.AnalyzerTelemetryInfo = analyzerTelemetryInfo;
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x060020A8 RID: 8360 RVA: 0x00064AAD File Offset: 0x00062CAD
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<DiagnosticAnalyzer> Analyzers
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x060020A9 RID: 8361 RVA: 0x00064AB5 File Offset: 0x00062CB5
		[Nullable(new byte[] { 1, 1, 1, 1, 0, 1 })]
		public ImmutableDictionary<SyntaxTree, ImmutableDictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>>> SyntaxDiagnostics
		{
			[return: Nullable(new byte[] { 1, 1, 1, 1, 0, 1 })]
			get;
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x060020AA RID: 8362 RVA: 0x00064ABD File Offset: 0x00062CBD
		[Nullable(new byte[] { 1, 1, 1, 1, 0, 1 })]
		public ImmutableDictionary<SyntaxTree, ImmutableDictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>>> SemanticDiagnostics
		{
			[return: Nullable(new byte[] { 1, 1, 1, 1, 0, 1 })]
			get;
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x060020AB RID: 8363 RVA: 0x00064AC5 File Offset: 0x00062CC5
		[Nullable(new byte[] { 1, 1, 1, 1, 0, 1 })]
		public ImmutableDictionary<AdditionalText, ImmutableDictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>>> AdditionalFileDiagnostics
		{
			[return: Nullable(new byte[] { 1, 1, 1, 1, 0, 1 })]
			get;
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x060020AC RID: 8364 RVA: 0x00064ACD File Offset: 0x00062CCD
		[Nullable(new byte[] { 1, 1, 0, 1 })]
		public ImmutableDictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>> CompilationDiagnostics
		{
			[return: Nullable(new byte[] { 1, 1, 0, 1 })]
			get;
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x060020AD RID: 8365 RVA: 0x00064AD5 File Offset: 0x00062CD5
		public ImmutableDictionary<DiagnosticAnalyzer, AnalyzerTelemetryInfo> AnalyzerTelemetryInfo { get; }

		// Token: 0x060020AE RID: 8366 RVA: 0x00064AE0 File Offset: 0x00062CE0
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<Diagnostic> GetAllDiagnostics(DiagnosticAnalyzer analyzer)
		{
			if (!this.Analyzers.Contains(analyzer))
			{
				throw new ArgumentException(CodeAnalysisResources.UnsupportedAnalyzerInstance, "analyzer");
			}
			return this.GetDiagnostics(SpecializedCollections.SingletonEnumerable<DiagnosticAnalyzer>(analyzer));
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x00064B1A File Offset: 0x00062D1A
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<Diagnostic> GetAllDiagnostics()
		{
			return this.GetDiagnostics(this.Analyzers);
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x00064B30 File Offset: 0x00062D30
		[return: Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<Diagnostic> GetDiagnostics(IEnumerable<DiagnosticAnalyzer> analyzers)
		{
			IEnumerable<DiagnosticAnalyzer> enumerable = Enumerable.Except<DiagnosticAnalyzer>(this.Analyzers, analyzers);
			ImmutableHashSet<DiagnosticAnalyzer> immutableHashSet = (Enumerable.Any<DiagnosticAnalyzer>(enumerable) ? enumerable.ToImmutableHashSet<DiagnosticAnalyzer>() : ImmutableHashSet<DiagnosticAnalyzer>.Empty);
			return this.GetDiagnostics(immutableHashSet);
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x00064B6C File Offset: 0x00062D6C
		[return: Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<Diagnostic> GetDiagnostics(ImmutableHashSet<DiagnosticAnalyzer> excludedAnalyzers)
		{
			if (this.SyntaxDiagnostics.Count > 0 || this.SemanticDiagnostics.Count > 0 || this.AdditionalFileDiagnostics.Count > 0 || this.CompilationDiagnostics.Count > 0)
			{
				ImmutableArray<Diagnostic>.Builder builder = ImmutableArray.CreateBuilder<Diagnostic>();
				AnalysisResult.AddLocalDiagnostics<SyntaxTree>(this.SyntaxDiagnostics, excludedAnalyzers, builder);
				AnalysisResult.AddLocalDiagnostics<SyntaxTree>(this.SemanticDiagnostics, excludedAnalyzers, builder);
				AnalysisResult.AddLocalDiagnostics<AdditionalText>(this.AdditionalFileDiagnostics, excludedAnalyzers, builder);
				AnalysisResult.AddNonLocalDiagnostics(this.CompilationDiagnostics, excludedAnalyzers, builder);
				return builder.ToImmutable();
			}
			return ImmutableArray<Diagnostic>.Empty;
		}

		// Token: 0x060020B2 RID: 8370 RVA: 0x00064BF8 File Offset: 0x00062DF8
		private static void AddLocalDiagnostics<T>([Nullable(new byte[] { 1, 1, 1, 1, 0, 1 })] ImmutableDictionary<T, ImmutableDictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>>> localDiagnostics, ImmutableHashSet<DiagnosticAnalyzer> excludedAnalyzers, ImmutableArray<Diagnostic>.Builder builder)
		{
			foreach (KeyValuePair<T, ImmutableDictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>>> keyValuePair in localDiagnostics)
			{
				foreach (KeyValuePair<DiagnosticAnalyzer, ImmutableArray<Diagnostic>> keyValuePair2 in keyValuePair.Value)
				{
					if (!excludedAnalyzers.Contains(keyValuePair2.Key))
					{
						builder.AddRange(keyValuePair2.Value);
					}
				}
			}
		}

		// Token: 0x060020B3 RID: 8371 RVA: 0x00064C98 File Offset: 0x00062E98
		private static void AddNonLocalDiagnostics([Nullable(new byte[] { 1, 1, 0, 1 })] ImmutableDictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>> nonLocalDiagnostics, ImmutableHashSet<DiagnosticAnalyzer> excludedAnalyzers, ImmutableArray<Diagnostic>.Builder builder)
		{
			foreach (KeyValuePair<DiagnosticAnalyzer, ImmutableArray<Diagnostic>> keyValuePair in nonLocalDiagnostics)
			{
				if (!excludedAnalyzers.Contains(keyValuePair.Key))
				{
					builder.AddRange(keyValuePair.Value);
				}
			}
		}
	}
}
