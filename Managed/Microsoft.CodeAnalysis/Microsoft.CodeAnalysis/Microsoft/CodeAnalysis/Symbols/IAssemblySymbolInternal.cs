using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Symbols
{
	// Token: 0x020002B9 RID: 697
	[NullableContext(1)]
	internal interface IAssemblySymbolInternal : ISymbolInternal
	{
		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06001D8B RID: 7563
		[Nullable(2)]
		Version AssemblyVersionPattern
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06001D8C RID: 7564
		AssemblyIdentity Identity { get; }

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06001D8D RID: 7565
		IAssemblySymbolInternal CorLibrary { get; }
	}
}
