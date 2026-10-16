using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001DC RID: 476
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct GeneratorExecutionContext
	{
		// Token: 0x060013C4 RID: 5060 RVA: 0x0003DE4C File Offset: 0x0003C04C
		internal GeneratorExecutionContext(Compilation compilation, ParseOptions parseOptions, [Nullable(new byte[] { 0, 1 })] ImmutableArray<AdditionalText> additionalTexts, AnalyzerConfigOptionsProvider optionsProvider, [Nullable(2)] ISyntaxContextReceiver syntaxReceiver, AdditionalSourcesCollection additionalSources, CancellationToken cancellationToken = default(CancellationToken))
		{
			this.Compilation = compilation;
			this.ParseOptions = parseOptions;
			this.AdditionalFiles = additionalTexts;
			this.AnalyzerConfigOptions = optionsProvider;
			SyntaxContextReceiverAdaptor syntaxContextReceiverAdaptor = syntaxReceiver as SyntaxContextReceiverAdaptor;
			this.SyntaxReceiver = ((syntaxContextReceiverAdaptor != null) ? syntaxContextReceiverAdaptor.Receiver : null);
			this.SyntaxContextReceiver = ((syntaxReceiver is SyntaxContextReceiverAdaptor) ? null : syntaxReceiver);
			this.CancellationToken = cancellationToken;
			this._additionalSources = additionalSources;
			this._diagnostics = new DiagnosticBag();
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x060013C5 RID: 5061 RVA: 0x0003DEBE File Offset: 0x0003C0BE
		public Compilation Compilation { get; }

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x060013C6 RID: 5062 RVA: 0x0003DEC6 File Offset: 0x0003C0C6
		public ParseOptions ParseOptions { get; }

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x060013C7 RID: 5063 RVA: 0x0003DECE File Offset: 0x0003C0CE
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<AdditionalText> AdditionalFiles
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x060013C8 RID: 5064 RVA: 0x0003DED6 File Offset: 0x0003C0D6
		public AnalyzerConfigOptionsProvider AnalyzerConfigOptions { get; }

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x060013C9 RID: 5065 RVA: 0x0003DEDE File Offset: 0x0003C0DE
		[Nullable(2)]
		public ISyntaxReceiver SyntaxReceiver
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x060013CA RID: 5066 RVA: 0x0003DEE6 File Offset: 0x0003C0E6
		[Nullable(2)]
		public ISyntaxContextReceiver SyntaxContextReceiver
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x060013CB RID: 5067 RVA: 0x0003DEEE File Offset: 0x0003C0EE
		public CancellationToken CancellationToken { get; }

		// Token: 0x060013CC RID: 5068 RVA: 0x0003DEF6 File Offset: 0x0003C0F6
		public void AddSource(string hintName, string source)
		{
			this.AddSource(hintName, SourceText.From(source, Encoding.UTF8, SourceHashAlgorithm.Sha1));
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x0003DF0B File Offset: 0x0003C10B
		public void AddSource(string hintName, SourceText sourceText)
		{
			this._additionalSources.Add(hintName, sourceText);
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x0003DF1A File Offset: 0x0003C11A
		public void ReportDiagnostic(Diagnostic diagnostic)
		{
			this._diagnostics.Add(diagnostic);
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x0003DF28 File Offset: 0x0003C128
		[return: TupleElementNames(new string[] { "sources", "diagnostics" })]
		[return: Nullable(new byte[] { 0, 0, 0, 1 })]
		internal ValueTuple<ImmutableArray<GeneratedSourceText>, ImmutableArray<Diagnostic>> ToImmutableAndFree()
		{
			return new ValueTuple<ImmutableArray<GeneratedSourceText>, ImmutableArray<Diagnostic>>(this._additionalSources.ToImmutableAndFree(), this._diagnostics.ToReadOnlyAndFree());
		}

		// Token: 0x04000600 RID: 1536
		private readonly DiagnosticBag _diagnostics;

		// Token: 0x04000601 RID: 1537
		private readonly AdditionalSourcesCollection _additionalSources;
	}
}
