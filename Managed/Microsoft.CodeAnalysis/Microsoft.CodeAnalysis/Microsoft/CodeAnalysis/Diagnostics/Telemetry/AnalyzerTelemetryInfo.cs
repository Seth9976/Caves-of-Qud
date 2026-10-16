using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.CodeAnalysis.Diagnostics.Telemetry
{
	// Token: 0x02000358 RID: 856
	[DataContract]
	public sealed class AnalyzerTelemetryInfo
	{
		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x0600249E RID: 9374 RVA: 0x00072005 File Offset: 0x00070205
		// (set) Token: 0x0600249F RID: 9375 RVA: 0x0007200D File Offset: 0x0007020D
		[DataMember(Order = 0)]
		public int CompilationStartActionsCount { get; set; }

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x060024A0 RID: 9376 RVA: 0x00072016 File Offset: 0x00070216
		// (set) Token: 0x060024A1 RID: 9377 RVA: 0x0007201E File Offset: 0x0007021E
		[DataMember(Order = 1)]
		public int CompilationEndActionsCount { get; set; }

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x060024A2 RID: 9378 RVA: 0x00072027 File Offset: 0x00070227
		// (set) Token: 0x060024A3 RID: 9379 RVA: 0x0007202F File Offset: 0x0007022F
		[DataMember(Order = 2)]
		public int CompilationActionsCount { get; set; }

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x060024A4 RID: 9380 RVA: 0x00072038 File Offset: 0x00070238
		// (set) Token: 0x060024A5 RID: 9381 RVA: 0x00072040 File Offset: 0x00070240
		[DataMember(Order = 3)]
		public int SyntaxTreeActionsCount { get; set; }

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x060024A6 RID: 9382 RVA: 0x00072049 File Offset: 0x00070249
		// (set) Token: 0x060024A7 RID: 9383 RVA: 0x00072051 File Offset: 0x00070251
		[DataMember(Order = 4)]
		public int AdditionalFileActionsCount { get; set; }

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x060024A8 RID: 9384 RVA: 0x0007205A File Offset: 0x0007025A
		// (set) Token: 0x060024A9 RID: 9385 RVA: 0x00072062 File Offset: 0x00070262
		[DataMember(Order = 5)]
		public int SemanticModelActionsCount { get; set; }

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x060024AA RID: 9386 RVA: 0x0007206B File Offset: 0x0007026B
		// (set) Token: 0x060024AB RID: 9387 RVA: 0x00072073 File Offset: 0x00070273
		[DataMember(Order = 6)]
		public int SymbolActionsCount { get; set; }

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x060024AC RID: 9388 RVA: 0x0007207C File Offset: 0x0007027C
		// (set) Token: 0x060024AD RID: 9389 RVA: 0x00072084 File Offset: 0x00070284
		[DataMember(Order = 7)]
		public int SymbolStartActionsCount { get; set; }

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x060024AE RID: 9390 RVA: 0x0007208D File Offset: 0x0007028D
		// (set) Token: 0x060024AF RID: 9391 RVA: 0x00072095 File Offset: 0x00070295
		[DataMember(Order = 8)]
		public int SymbolEndActionsCount { get; set; }

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x060024B0 RID: 9392 RVA: 0x0007209E File Offset: 0x0007029E
		// (set) Token: 0x060024B1 RID: 9393 RVA: 0x000720A6 File Offset: 0x000702A6
		[DataMember(Order = 9)]
		public int SyntaxNodeActionsCount { get; set; }

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x060024B2 RID: 9394 RVA: 0x000720AF File Offset: 0x000702AF
		// (set) Token: 0x060024B3 RID: 9395 RVA: 0x000720B7 File Offset: 0x000702B7
		[DataMember(Order = 10)]
		public int CodeBlockStartActionsCount { get; set; }

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x060024B4 RID: 9396 RVA: 0x000720C0 File Offset: 0x000702C0
		// (set) Token: 0x060024B5 RID: 9397 RVA: 0x000720C8 File Offset: 0x000702C8
		[DataMember(Order = 11)]
		public int CodeBlockEndActionsCount { get; set; }

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x060024B6 RID: 9398 RVA: 0x000720D1 File Offset: 0x000702D1
		// (set) Token: 0x060024B7 RID: 9399 RVA: 0x000720D9 File Offset: 0x000702D9
		[DataMember(Order = 12)]
		public int CodeBlockActionsCount { get; set; }

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x060024B8 RID: 9400 RVA: 0x000720E2 File Offset: 0x000702E2
		// (set) Token: 0x060024B9 RID: 9401 RVA: 0x000720EA File Offset: 0x000702EA
		[DataMember(Order = 13)]
		public int OperationActionsCount { get; set; }

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x060024BA RID: 9402 RVA: 0x000720F3 File Offset: 0x000702F3
		// (set) Token: 0x060024BB RID: 9403 RVA: 0x000720FB File Offset: 0x000702FB
		[DataMember(Order = 14)]
		public int OperationBlockStartActionsCount { get; set; }

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x060024BC RID: 9404 RVA: 0x00072104 File Offset: 0x00070304
		// (set) Token: 0x060024BD RID: 9405 RVA: 0x0007210C File Offset: 0x0007030C
		[DataMember(Order = 15)]
		public int OperationBlockEndActionsCount { get; set; }

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x060024BE RID: 9406 RVA: 0x00072115 File Offset: 0x00070315
		// (set) Token: 0x060024BF RID: 9407 RVA: 0x0007211D File Offset: 0x0007031D
		[DataMember(Order = 16)]
		public int OperationBlockActionsCount { get; set; }

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x060024C0 RID: 9408 RVA: 0x00072126 File Offset: 0x00070326
		// (set) Token: 0x060024C1 RID: 9409 RVA: 0x0007212E File Offset: 0x0007032E
		[DataMember(Order = 17)]
		public int SuppressionActionsCount { get; set; }

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x060024C2 RID: 9410 RVA: 0x00072137 File Offset: 0x00070337
		// (set) Token: 0x060024C3 RID: 9411 RVA: 0x0007213F File Offset: 0x0007033F
		[DataMember(Order = 18)]
		public TimeSpan ExecutionTime { get; set; } = TimeSpan.Zero;

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x060024C4 RID: 9412 RVA: 0x00072148 File Offset: 0x00070348
		// (set) Token: 0x060024C5 RID: 9413 RVA: 0x00072150 File Offset: 0x00070350
		[DataMember(Order = 19)]
		public bool Concurrent { get; set; }

		// Token: 0x060024C6 RID: 9414 RVA: 0x0007215C File Offset: 0x0007035C
		[NullableContext(1)]
		internal AnalyzerTelemetryInfo(AnalyzerActionCounts actionCounts, int suppressionActionCounts, TimeSpan executionTime)
		{
			this.CompilationStartActionsCount = actionCounts.CompilationStartActionsCount;
			this.CompilationEndActionsCount = actionCounts.CompilationEndActionsCount;
			this.CompilationActionsCount = actionCounts.CompilationActionsCount;
			this.SyntaxTreeActionsCount = actionCounts.SyntaxTreeActionsCount;
			this.AdditionalFileActionsCount = actionCounts.AdditionalFileActionsCount;
			this.SemanticModelActionsCount = actionCounts.SemanticModelActionsCount;
			this.SymbolActionsCount = actionCounts.SymbolActionsCount;
			this.SymbolStartActionsCount = actionCounts.SymbolStartActionsCount;
			this.SymbolEndActionsCount = actionCounts.SymbolEndActionsCount;
			this.SyntaxNodeActionsCount = actionCounts.SyntaxNodeActionsCount;
			this.CodeBlockStartActionsCount = actionCounts.CodeBlockStartActionsCount;
			this.CodeBlockEndActionsCount = actionCounts.CodeBlockEndActionsCount;
			this.CodeBlockActionsCount = actionCounts.CodeBlockActionsCount;
			this.OperationActionsCount = actionCounts.OperationActionsCount;
			this.OperationBlockStartActionsCount = actionCounts.OperationBlockStartActionsCount;
			this.OperationBlockEndActionsCount = actionCounts.OperationBlockEndActionsCount;
			this.OperationBlockActionsCount = actionCounts.OperationBlockActionsCount;
			this.SuppressionActionsCount = suppressionActionCounts;
			this.ExecutionTime = executionTime;
			this.Concurrent = actionCounts.Concurrent;
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x00072260 File Offset: 0x00070460
		public AnalyzerTelemetryInfo()
		{
		}
	}
}
