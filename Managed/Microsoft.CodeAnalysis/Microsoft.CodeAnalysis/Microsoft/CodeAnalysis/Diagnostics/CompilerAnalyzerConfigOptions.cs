using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000301 RID: 769
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class CompilerAnalyzerConfigOptions : AnalyzerConfigOptions
	{
		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x0600211E RID: 8478 RVA: 0x00066B06 File Offset: 0x00064D06
		public static CompilerAnalyzerConfigOptions Empty { get; } = new CompilerAnalyzerConfigOptions(AnalyzerConfigOptions.EmptyDictionary);

		// Token: 0x0600211F RID: 8479 RVA: 0x00066B0D File Offset: 0x00064D0D
		public CompilerAnalyzerConfigOptions(ImmutableDictionary<string, string> properties)
		{
			this._backing = properties;
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x00066B1C File Offset: 0x00064D1C
		public override bool TryGetValue(string key, [Nullable(2)] [NotNullWhen(true)] out string value)
		{
			return this._backing.TryGetValue(key, out value);
		}

		// Token: 0x04000E5F RID: 3679
		private readonly ImmutableDictionary<string, string> _backing;
	}
}
