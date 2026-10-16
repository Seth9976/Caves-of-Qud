using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001E2 RID: 482
	internal readonly struct GeneratorDriverState
	{
		// Token: 0x060013FB RID: 5115 RVA: 0x0003EF2F File Offset: 0x0003D12F
		[NullableContext(1)]
		internal GeneratorDriverState(ParseOptions parseOptions, AnalyzerConfigOptionsProvider optionsProvider, [Nullable(new byte[] { 0, 1 })] ImmutableArray<ISourceGenerator> generators, [Nullable(new byte[] { 0, 1 })] ImmutableArray<AdditionalText> additionalTexts, [Nullable(0)] ImmutableArray<GeneratorState> generatorStates, [Nullable(new byte[] { 0, 1 })] ImmutableArray<PendingEdit> edits, bool editsFailed)
		{
			this.Generators = generators;
			this.GeneratorStates = generatorStates;
			this.AdditionalTexts = additionalTexts;
			this.Edits = edits;
			this.ParseOptions = parseOptions;
			this.OptionsProvider = optionsProvider;
			this.EditsFailed = editsFailed;
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x0003EF68 File Offset: 0x0003D168
		internal GeneratorDriverState With([Nullable(new byte[] { 0, 1 })] ImmutableArray<ISourceGenerator>? generators = null, ImmutableArray<GeneratorState>? generatorStates = null, [Nullable(new byte[] { 0, 1 })] ImmutableArray<AdditionalText>? additionalTexts = null, [Nullable(new byte[] { 0, 1 })] ImmutableArray<PendingEdit>? edits = null, bool? editsFailed = null)
		{
			return new GeneratorDriverState(this.ParseOptions, this.OptionsProvider, generators ?? this.Generators, additionalTexts ?? this.AdditionalTexts, generatorStates ?? this.GeneratorStates, edits ?? this.Edits, editsFailed ?? this.EditsFailed);
		}

		// Token: 0x04000612 RID: 1554
		[Nullable(new byte[] { 0, 1 })]
		internal readonly ImmutableArray<ISourceGenerator> Generators;

		// Token: 0x04000613 RID: 1555
		internal readonly ImmutableArray<GeneratorState> GeneratorStates;

		// Token: 0x04000614 RID: 1556
		[Nullable(new byte[] { 0, 1 })]
		internal readonly ImmutableArray<AdditionalText> AdditionalTexts;

		// Token: 0x04000615 RID: 1557
		[Nullable(1)]
		internal readonly AnalyzerConfigOptionsProvider OptionsProvider;

		// Token: 0x04000616 RID: 1558
		[Nullable(new byte[] { 0, 1 })]
		internal readonly ImmutableArray<PendingEdit> Edits;

		// Token: 0x04000617 RID: 1559
		internal readonly bool EditsFailed;

		// Token: 0x04000618 RID: 1560
		[Nullable(1)]
		internal readonly ParseOptions ParseOptions;
	}
}
