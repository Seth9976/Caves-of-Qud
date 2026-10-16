using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Symbols
{
	// Token: 0x020002C2 RID: 706
	internal interface ISourceAssemblySymbolInternal : IAssemblySymbolInternal, ISymbolInternal
	{
		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06001D98 RID: 7576
		AssemblyFlags AssemblyFlags { get; }

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06001D99 RID: 7577
		[Nullable(2)]
		string SignatureKey
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06001D9A RID: 7578
		AssemblyHashAlgorithm HashAlgorithm { get; }

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06001D9B RID: 7579
		bool InternalsAreVisible { get; }
	}
}
