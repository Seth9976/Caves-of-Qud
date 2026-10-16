using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Symbols;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001CD RID: 461
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ModuleReferences<TAssemblySymbol> where TAssemblySymbol : class, IAssemblySymbolInternal
	{
		// Token: 0x06001371 RID: 4977 RVA: 0x0003CBFA File Offset: 0x0003ADFA
		public ModuleReferences([Nullable(new byte[] { 0, 1 })] ImmutableArray<AssemblyIdentity> identities, [Nullable(new byte[] { 0, 1 })] ImmutableArray<TAssemblySymbol> symbols, [Nullable(new byte[] { 0, 0, 1 })] ImmutableArray<UnifiedAssembly<TAssemblySymbol>> unifiedAssemblies)
		{
			this.Identities = identities;
			this.Symbols = symbols;
			this.UnifiedAssemblies = unifiedAssemblies;
		}

		// Token: 0x040005CB RID: 1483
		[Nullable(new byte[] { 0, 1 })]
		public readonly ImmutableArray<AssemblyIdentity> Identities;

		// Token: 0x040005CC RID: 1484
		[Nullable(new byte[] { 0, 1 })]
		public readonly ImmutableArray<TAssemblySymbol> Symbols;

		// Token: 0x040005CD RID: 1485
		[Nullable(new byte[] { 0, 0, 1 })]
		public readonly ImmutableArray<UnifiedAssembly<TAssemblySymbol>> UnifiedAssemblies;
	}
}
