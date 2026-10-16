using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001F0 RID: 496
	public readonly struct GeneratorRunResult
	{
		// Token: 0x0600142F RID: 5167 RVA: 0x0003F2E0 File Offset: 0x0003D4E0
		internal GeneratorRunResult([Nullable(1)] ISourceGenerator generator, ImmutableArray<GeneratedSourceResult> generatedSources, [Nullable(new byte[] { 0, 1 })] ImmutableArray<Diagnostic> diagnostics, [Nullable(2)] Exception exception)
		{
			this.Generator = generator;
			this.GeneratedSources = generatedSources;
			this.Diagnostics = diagnostics;
			this.Exception = exception;
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001430 RID: 5168 RVA: 0x0003F2FF File Offset: 0x0003D4FF
		[Nullable(1)]
		public ISourceGenerator Generator
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001431 RID: 5169 RVA: 0x0003F307 File Offset: 0x0003D507
		public ImmutableArray<GeneratedSourceResult> GeneratedSources { get; }

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001432 RID: 5170 RVA: 0x0003F30F File Offset: 0x0003D50F
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<Diagnostic> Diagnostics
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001433 RID: 5171 RVA: 0x0003F317 File Offset: 0x0003D517
		[Nullable(2)]
		public Exception Exception
		{
			[NullableContext(2)]
			get;
		}
	}
}
