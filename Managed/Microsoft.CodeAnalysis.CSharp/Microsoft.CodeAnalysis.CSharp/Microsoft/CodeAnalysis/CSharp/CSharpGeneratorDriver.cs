using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000219 RID: 537
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class CSharpGeneratorDriver : GeneratorDriver
	{
		// Token: 0x06002743 RID: 10051 RVA: 0x000D6485 File Offset: 0x000D4685
		internal CSharpGeneratorDriver(CSharpParseOptions parseOptions, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<ISourceGenerator> generators, AnalyzerConfigOptionsProvider optionsProvider, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<AdditionalText> additionalTexts)
			: base(parseOptions, generators, optionsProvider, additionalTexts)
		{
		}

		// Token: 0x06002744 RID: 10052 RVA: 0x000D6492 File Offset: 0x000D4692
		private CSharpGeneratorDriver(GeneratorDriverState state)
			: base(state)
		{
		}

		// Token: 0x06002745 RID: 10053 RVA: 0x000D649B File Offset: 0x000D469B
		public static CSharpGeneratorDriver Create(params ISourceGenerator[] generators)
		{
			return CSharpGeneratorDriver.Create(generators, null, null, null);
		}

		// Token: 0x06002746 RID: 10054 RVA: 0x000D64A6 File Offset: 0x000D46A6
		public static CSharpGeneratorDriver Create(IEnumerable<ISourceGenerator> generators, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] IEnumerable<AdditionalText> additionalTexts = null, [global::System.Runtime.CompilerServices.Nullable(2)] CSharpParseOptions parseOptions = null, [global::System.Runtime.CompilerServices.Nullable(2)] AnalyzerConfigOptionsProvider optionsProvider = null)
		{
			return new CSharpGeneratorDriver(parseOptions ?? CSharpParseOptions.Default, generators.ToImmutableArray<ISourceGenerator>(), optionsProvider ?? CompilerAnalyzerConfigOptionsProvider.Empty, additionalTexts.AsImmutableOrEmpty<AdditionalText>());
		}

		// Token: 0x06002747 RID: 10055 RVA: 0x000D64CD File Offset: 0x000D46CD
		internal override SyntaxTree ParseGeneratedSourceText(GeneratedSourceText input, string fileName, CancellationToken cancellationToken)
		{
			return CSharpSyntaxTree.ParseTextLazy(input.Text, (CSharpParseOptions)this._state.ParseOptions, fileName);
		}

		// Token: 0x06002748 RID: 10056 RVA: 0x000D64EC File Offset: 0x000D46EC
		internal override GeneratorDriver FromState(GeneratorDriverState state)
		{
			return new CSharpGeneratorDriver(state);
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06002749 RID: 10057 RVA: 0x000D64F4 File Offset: 0x000D46F4
		internal override CommonMessageProvider MessageProvider
		{
			get
			{
				return Microsoft.CodeAnalysis.CSharp.MessageProvider.Instance;
			}
		}

		// Token: 0x0600274A RID: 10058 RVA: 0x000D64FB File Offset: 0x000D46FB
		internal override AdditionalSourcesCollection CreateSourcesCollection()
		{
			return new AdditionalSourcesCollection(".cs");
		}
	}
}
