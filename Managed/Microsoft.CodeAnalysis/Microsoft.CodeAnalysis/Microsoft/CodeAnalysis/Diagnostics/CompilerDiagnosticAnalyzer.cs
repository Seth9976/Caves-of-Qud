using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000318 RID: 792
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class CompilerDiagnosticAnalyzer : DiagnosticAnalyzer
	{
		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x060022CD RID: 8909
		internal abstract CommonMessageProvider MessageProvider { get; }

		// Token: 0x060022CE RID: 8910
		[NullableContext(0)]
		internal abstract ImmutableArray<int> GetSupportedErrorCodes();

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x060022CF RID: 8911 RVA: 0x0006EE2C File Offset: 0x0006D02C
		[Nullable(new byte[] { 0, 1 })]
		public sealed override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				CommonMessageProvider messageProvider = this.MessageProvider;
				ImmutableArray<int> supportedErrorCodes = this.GetSupportedErrorCodes();
				ImmutableArray<DiagnosticDescriptor>.Builder builder = ImmutableArray.CreateBuilder<DiagnosticDescriptor>(supportedErrorCodes.Length);
				foreach (int num in supportedErrorCodes)
				{
					DiagnosticDescriptor descriptor = DiagnosticInfo.GetDescriptor(num, messageProvider);
					builder.Add(descriptor);
				}
				builder.Add(AnalyzerExecutor.GetAnalyzerExceptionDiagnosticDescriptor(null, null, null, null));
				return builder.ToImmutable();
			}
		}

		// Token: 0x060022D0 RID: 8912 RVA: 0x0006EE93 File Offset: 0x0006D093
		public sealed override void Initialize(AnalysisContext context)
		{
			context.EnableConcurrentExecution();
			context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
			context.RegisterCompilationStartAction(delegate(CompilationStartAnalysisContext c)
			{
				CompilerDiagnosticAnalyzer.CompilationAnalyzer compilationAnalyzer = new CompilerDiagnosticAnalyzer.CompilationAnalyzer(c.Compilation);
				c.RegisterSyntaxTreeAction(new Action<SyntaxTreeAnalysisContext>(compilationAnalyzer.AnalyzeSyntaxTree));
				c.RegisterSemanticModelAction(new Action<SemanticModelAnalysisContext>(CompilerDiagnosticAnalyzer.CompilationAnalyzer.AnalyzeSemanticModel));
			});
		}

		// Token: 0x04000EE3 RID: 3811
		private const string Origin = "Origin";

		// Token: 0x04000EE4 RID: 3812
		private const string Syntactic = "Syntactic";

		// Token: 0x04000EE5 RID: 3813
		private const string Declaration = "Declaration";

		// Token: 0x04000EE6 RID: 3814
		[Nullable(new byte[] { 1, 1, 2 })]
		private static readonly ImmutableDictionary<string, string> s_syntactic = ImmutableDictionary<string, string>.Empty.Add("Origin", "Syntactic");

		// Token: 0x04000EE7 RID: 3815
		[Nullable(new byte[] { 1, 1, 2 })]
		private static readonly ImmutableDictionary<string, string> s_declaration = ImmutableDictionary<string, string>.Empty.Add("Origin", "Declaration");

		// Token: 0x02000711 RID: 1809
		[Nullable(0)]
		private class CompilationAnalyzer
		{
			// Token: 0x06003B34 RID: 15156 RVA: 0x000A9926 File Offset: 0x000A7B26
			public CompilationAnalyzer(Compilation compilation)
			{
				this._compilation = compilation;
			}

			// Token: 0x06003B35 RID: 15157 RVA: 0x000A9938 File Offset: 0x000A7B38
			public void AnalyzeSyntaxTree(SyntaxTreeAnalysisContext context)
			{
				SemanticModel semanticModel = this._compilation.GetSemanticModel(context.Tree, false);
				CancellationToken cancellationToken = context.CancellationToken;
				CompilerDiagnosticAnalyzer.CompilationAnalyzer.ReportDiagnostics(semanticModel.GetSyntaxDiagnostics(default(TextSpan?), cancellationToken), new Action<Diagnostic>(context.ReportDiagnostic), new Func<Location, bool>(CompilerDiagnosticAnalyzer.CompilationAnalyzer.IsSourceLocation), CompilerDiagnosticAnalyzer.s_syntactic);
			}

			// Token: 0x06003B36 RID: 15158 RVA: 0x000A9998 File Offset: 0x000A7B98
			public static void AnalyzeSemanticModel(SemanticModelAnalysisContext context)
			{
				SemanticModel semanticModel = context.SemanticModel;
				CancellationToken cancellationToken = context.CancellationToken;
				ImmutableArray<Diagnostic> declarationDiagnostics = semanticModel.GetDeclarationDiagnostics(default(TextSpan?), cancellationToken);
				SemanticModel semanticModel2 = context.SemanticModel;
				cancellationToken = context.CancellationToken;
				ImmutableArray<Diagnostic> methodBodyDiagnostics = semanticModel2.GetMethodBodyDiagnostics(default(TextSpan?), cancellationToken);
				CompilerDiagnosticAnalyzer.CompilationAnalyzer.ReportDiagnostics(declarationDiagnostics, new Action<Diagnostic>(context.ReportDiagnostic), new Func<Location, bool>(CompilerDiagnosticAnalyzer.CompilationAnalyzer.IsSourceLocation), CompilerDiagnosticAnalyzer.s_declaration);
				CompilerDiagnosticAnalyzer.CompilationAnalyzer.ReportDiagnostics(methodBodyDiagnostics, new Action<Diagnostic>(context.ReportDiagnostic), new Func<Location, bool>(CompilerDiagnosticAnalyzer.CompilationAnalyzer.IsSourceLocation), null);
			}

			// Token: 0x06003B37 RID: 15159 RVA: 0x000A9A30 File Offset: 0x000A7C30
			public static void AnalyzeCompilation(CompilationAnalysisContext context)
			{
				CompilerDiagnosticAnalyzer.CompilationAnalyzer.ReportDiagnostics(context.Compilation.GetDeclarationDiagnostics(context.CancellationToken), new Action<Diagnostic>(context.ReportDiagnostic), (Location location) => !CompilerDiagnosticAnalyzer.CompilationAnalyzer.IsSourceLocation(location), CompilerDiagnosticAnalyzer.s_declaration);
			}

			// Token: 0x06003B38 RID: 15160 RVA: 0x000A9A8A File Offset: 0x000A7C8A
			private static bool IsSourceLocation(Location location)
			{
				return location != null && location.Kind == LocationKind.SourceFile;
			}

			// Token: 0x06003B39 RID: 15161 RVA: 0x000A9AA0 File Offset: 0x000A7CA0
			private static void ReportDiagnostics([Nullable(new byte[] { 0, 1 })] ImmutableArray<Diagnostic> diagnostics, Action<Diagnostic> reportDiagnostic, Func<Location, bool> locationFilter, [Nullable(new byte[] { 2, 1, 2 })] ImmutableDictionary<string, string> properties = null)
			{
				foreach (Diagnostic diagnostic in diagnostics)
				{
					if (locationFilter.Invoke(diagnostic.Location) && diagnostic.Severity != DiagnosticSeverity.Hidden)
					{
						Diagnostic diagnostic2 = ((properties == null) ? diagnostic : new CompilerDiagnosticAnalyzer.CompilationAnalyzer.CompilerDiagnostic(diagnostic, properties));
						reportDiagnostic.Invoke(diagnostic2);
					}
				}
			}

			// Token: 0x04001C38 RID: 7224
			private readonly Compilation _compilation;

			// Token: 0x020007E9 RID: 2025
			[Nullable(0)]
			private class CompilerDiagnostic : Diagnostic
			{
				// Token: 0x06004047 RID: 16455 RVA: 0x000B40A6 File Offset: 0x000B22A6
				public CompilerDiagnostic(Diagnostic original, [Nullable(new byte[] { 1, 1, 2 })] ImmutableDictionary<string, string> properties)
				{
					this._original = original;
					this._properties = properties;
				}

				// Token: 0x17001191 RID: 4497
				// (get) Token: 0x06004048 RID: 16456 RVA: 0x000B40BC File Offset: 0x000B22BC
				public override DiagnosticDescriptor Descriptor
				{
					get
					{
						return this._original.Descriptor;
					}
				}

				// Token: 0x17001192 RID: 4498
				// (get) Token: 0x06004049 RID: 16457 RVA: 0x000B40C9 File Offset: 0x000B22C9
				internal override int Code
				{
					get
					{
						return this._original.Code;
					}
				}

				// Token: 0x17001193 RID: 4499
				// (get) Token: 0x0600404A RID: 16458 RVA: 0x000B40D6 File Offset: 0x000B22D6
				[Nullable(new byte[] { 1, 2 })]
				internal override IReadOnlyList<object> Arguments
				{
					[return: Nullable(new byte[] { 1, 2 })]
					get
					{
						return this._original.Arguments;
					}
				}

				// Token: 0x17001194 RID: 4500
				// (get) Token: 0x0600404B RID: 16459 RVA: 0x000B40E3 File Offset: 0x000B22E3
				public override string Id
				{
					get
					{
						return this._original.Id;
					}
				}

				// Token: 0x17001195 RID: 4501
				// (get) Token: 0x0600404C RID: 16460 RVA: 0x000B40F0 File Offset: 0x000B22F0
				public override DiagnosticSeverity Severity
				{
					get
					{
						return this._original.Severity;
					}
				}

				// Token: 0x17001196 RID: 4502
				// (get) Token: 0x0600404D RID: 16461 RVA: 0x000B40FD File Offset: 0x000B22FD
				public override int WarningLevel
				{
					get
					{
						return this._original.WarningLevel;
					}
				}

				// Token: 0x17001197 RID: 4503
				// (get) Token: 0x0600404E RID: 16462 RVA: 0x000B410A File Offset: 0x000B230A
				public override Location Location
				{
					get
					{
						return this._original.Location;
					}
				}

				// Token: 0x17001198 RID: 4504
				// (get) Token: 0x0600404F RID: 16463 RVA: 0x000B4117 File Offset: 0x000B2317
				public override IReadOnlyList<Location> AdditionalLocations
				{
					get
					{
						return this._original.AdditionalLocations;
					}
				}

				// Token: 0x17001199 RID: 4505
				// (get) Token: 0x06004050 RID: 16464 RVA: 0x000B4124 File Offset: 0x000B2324
				public override bool IsSuppressed
				{
					get
					{
						return this._original.IsSuppressed;
					}
				}

				// Token: 0x1700119A RID: 4506
				// (get) Token: 0x06004051 RID: 16465 RVA: 0x000B4131 File Offset: 0x000B2331
				[Nullable(new byte[] { 1, 1, 2 })]
				public override ImmutableDictionary<string, string> Properties
				{
					[return: Nullable(new byte[] { 1, 1, 2 })]
					get
					{
						return this._properties;
					}
				}

				// Token: 0x06004052 RID: 16466 RVA: 0x000B4139 File Offset: 0x000B2339
				public override string GetMessage([Nullable(2)] IFormatProvider formatProvider = null)
				{
					return this._original.GetMessage(formatProvider);
				}

				// Token: 0x06004053 RID: 16467 RVA: 0x000B4147 File Offset: 0x000B2347
				[NullableContext(2)]
				public override bool Equals(object obj)
				{
					return this._original.Equals(obj);
				}

				// Token: 0x06004054 RID: 16468 RVA: 0x000B4155 File Offset: 0x000B2355
				public override int GetHashCode()
				{
					return this._original.GetHashCode();
				}

				// Token: 0x06004055 RID: 16469 RVA: 0x000B4162 File Offset: 0x000B2362
				[NullableContext(2)]
				public override bool Equals(Diagnostic obj)
				{
					return this._original.Equals(obj);
				}

				// Token: 0x06004056 RID: 16470 RVA: 0x000B4170 File Offset: 0x000B2370
				internal override Diagnostic WithLocation(Location location)
				{
					return new CompilerDiagnosticAnalyzer.CompilationAnalyzer.CompilerDiagnostic(this._original.WithLocation(location), this._properties);
				}

				// Token: 0x06004057 RID: 16471 RVA: 0x000B4189 File Offset: 0x000B2389
				internal override Diagnostic WithSeverity(DiagnosticSeverity severity)
				{
					return new CompilerDiagnosticAnalyzer.CompilationAnalyzer.CompilerDiagnostic(this._original.WithSeverity(severity), this._properties);
				}

				// Token: 0x06004058 RID: 16472 RVA: 0x000B41A2 File Offset: 0x000B23A2
				internal override Diagnostic WithIsSuppressed(bool isSuppressed)
				{
					return new CompilerDiagnosticAnalyzer.CompilationAnalyzer.CompilerDiagnostic(this._original.WithIsSuppressed(isSuppressed), this._properties);
				}

				// Token: 0x04001E4E RID: 7758
				private readonly Diagnostic _original;

				// Token: 0x04001E4F RID: 7759
				[Nullable(new byte[] { 1, 1, 2 })]
				private readonly ImmutableDictionary<string, string> _properties;
			}
		}
	}
}
