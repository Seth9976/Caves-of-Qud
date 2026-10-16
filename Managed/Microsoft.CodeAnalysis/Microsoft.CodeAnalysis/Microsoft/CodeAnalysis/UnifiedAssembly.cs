using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Symbols;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001CE RID: 462
	[NullableContext(1)]
	[Nullable(0)]
	internal struct UnifiedAssembly<TAssemblySymbol> where TAssemblySymbol : class, IAssemblySymbolInternal
	{
		// Token: 0x06001372 RID: 4978 RVA: 0x0003CC17 File Offset: 0x0003AE17
		public UnifiedAssembly(TAssemblySymbol targetAssembly, AssemblyIdentity originalReference)
		{
			this.OriginalReference = originalReference;
			this.TargetAssembly = targetAssembly;
		}

		// Token: 0x040005CE RID: 1486
		internal readonly AssemblyIdentity OriginalReference;

		// Token: 0x040005CF RID: 1487
		internal readonly TAssemblySymbol TargetAssembly;
	}
}
