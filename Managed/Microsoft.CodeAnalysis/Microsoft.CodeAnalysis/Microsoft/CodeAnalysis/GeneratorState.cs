using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001E4 RID: 484
	internal readonly struct GeneratorState
	{
		// Token: 0x06001402 RID: 5122 RVA: 0x0003F049 File Offset: 0x0003D249
		public GeneratorState(GeneratorInfo info)
		{
			this = new GeneratorState(info, ImmutableArray<GeneratedSyntaxTree>.Empty, ImmutableArray<GeneratedSyntaxTree>.Empty, ImmutableArray<Diagnostic>.Empty, null, null);
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x0003F063 File Offset: 0x0003D263
		public GeneratorState(GeneratorInfo info, ImmutableArray<GeneratedSyntaxTree> postInitTrees)
		{
			this = new GeneratorState(info, postInitTrees, ImmutableArray<GeneratedSyntaxTree>.Empty, ImmutableArray<Diagnostic>.Empty, null, null);
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x0003F079 File Offset: 0x0003D279
		[NullableContext(1)]
		public GeneratorState(GeneratorInfo info, Exception e, Diagnostic error)
		{
			this = new GeneratorState(info, ImmutableArray<GeneratedSyntaxTree>.Empty, ImmutableArray<GeneratedSyntaxTree>.Empty, ImmutableArray.Create<Diagnostic>(error), null, e);
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x0003F094 File Offset: 0x0003D294
		public GeneratorState(GeneratorInfo info, ImmutableArray<GeneratedSyntaxTree> postInitTrees, ImmutableArray<GeneratedSyntaxTree> generatedTrees, [Nullable(new byte[] { 0, 1 })] ImmutableArray<Diagnostic> diagnostics)
		{
			this = new GeneratorState(info, postInitTrees, generatedTrees, diagnostics, null, null);
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x0003F0A3 File Offset: 0x0003D2A3
		private GeneratorState(GeneratorInfo info, ImmutableArray<GeneratedSyntaxTree> postInitTrees, ImmutableArray<GeneratedSyntaxTree> generatedTrees, [Nullable(new byte[] { 0, 1 })] ImmutableArray<Diagnostic> diagnostics, [Nullable(2)] ISyntaxContextReceiver syntaxReceiver, [Nullable(2)] Exception exception)
		{
			this.PostInitTrees = postInitTrees;
			this.GeneratedTrees = generatedTrees;
			this.Info = info;
			this.Diagnostics = diagnostics;
			this.SyntaxReceiver = syntaxReceiver;
			this.Exception = exception;
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001407 RID: 5127 RVA: 0x0003F0D2 File Offset: 0x0003D2D2
		internal ImmutableArray<GeneratedSyntaxTree> PostInitTrees { get; }

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001408 RID: 5128 RVA: 0x0003F0DA File Offset: 0x0003D2DA
		internal ImmutableArray<GeneratedSyntaxTree> GeneratedTrees { get; }

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001409 RID: 5129 RVA: 0x0003F0E2 File Offset: 0x0003D2E2
		internal GeneratorInfo Info { get; }

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x0600140A RID: 5130 RVA: 0x0003F0EA File Offset: 0x0003D2EA
		[Nullable(2)]
		internal ISyntaxContextReceiver SyntaxReceiver
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x0600140B RID: 5131 RVA: 0x0003F0F2 File Offset: 0x0003D2F2
		[Nullable(2)]
		internal Exception Exception
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x0600140C RID: 5132 RVA: 0x0003F0FA File Offset: 0x0003D2FA
		[Nullable(new byte[] { 0, 1 })]
		internal ImmutableArray<Diagnostic> Diagnostics
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x0003F102 File Offset: 0x0003D302
		[NullableContext(1)]
		internal GeneratorState WithReceiver(ISyntaxContextReceiver syntaxReceiver)
		{
			return new GeneratorState(this.Info, this.PostInitTrees, this.GeneratedTrees, this.Diagnostics, syntaxReceiver, null);
		}

		// Token: 0x0400061D RID: 1565
		internal static GeneratorState Uninitialized;
	}
}
