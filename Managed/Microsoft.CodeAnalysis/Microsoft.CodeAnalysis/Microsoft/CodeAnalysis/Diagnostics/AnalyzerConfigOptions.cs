using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000300 RID: 768
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class AnalyzerConfigOptions
	{
		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x0600211A RID: 8474 RVA: 0x00066ADC File Offset: 0x00064CDC
		public static StringComparer KeyComparer { get; } = AnalyzerConfig.Section.PropertiesKeyComparer;

		// Token: 0x0600211B RID: 8475
		public abstract bool TryGetValue(string key, [Nullable(2)] [NotNullWhen(true)] out string value);

		// Token: 0x04000E5D RID: 3677
		internal static ImmutableDictionary<string, string> EmptyDictionary = ImmutableDictionary.Create<string, string>(AnalyzerConfigOptions.KeyComparer);
	}
}
