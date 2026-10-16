using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200055F RID: 1375
	internal static class SpecializedSymbolCollections
	{
		// Token: 0x06007440 RID: 29760 RVA: 0x001B2610 File Offset: 0x001B0810
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public static PooledHashSet<TSymbol> GetPooledSymbolHashSetInstance<[global::System.Runtime.CompilerServices.Nullable(0)] TSymbol>() where TSymbol : Symbol
		{
			return SpecializedSymbolCollections.PooledSymbolHashSet<TSymbol>.s_poolInstance.Allocate();
		}

		// Token: 0x06007441 RID: 29761 RVA: 0x001B261C File Offset: 0x001B081C
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public static PooledDictionary<KSymbol, V> GetPooledSymbolDictionaryInstance<[global::System.Runtime.CompilerServices.Nullable(0)] KSymbol, [global::System.Runtime.CompilerServices.Nullable(2)] V>() where KSymbol : Symbol
		{
			return SpecializedSymbolCollections.PooledSymbolDictionary<KSymbol, V>.s_poolInstance.Allocate();
		}

		// Token: 0x02000942 RID: 2370
		private static class PooledSymbolHashSet<TSymbol> where TSymbol : Symbol
		{
			// Token: 0x04002828 RID: 10280
			[global::System.Runtime.CompilerServices.Nullable(1)]
			internal static readonly ObjectPool<PooledHashSet<TSymbol>> s_poolInstance = PooledHashSet<TSymbol>.CreatePool(SymbolEqualityComparer.ConsiderEverything);
		}

		// Token: 0x02000943 RID: 2371
		private static class PooledSymbolDictionary<TSymbol, [global::System.Runtime.CompilerServices.Nullable(2)] V> where TSymbol : Symbol
		{
			// Token: 0x04002829 RID: 10281
			[global::System.Runtime.CompilerServices.Nullable(1)]
			internal static readonly ObjectPool<PooledDictionary<TSymbol, V>> s_poolInstance = PooledDictionary<TSymbol, V>.CreatePool(SymbolEqualityComparer.ConsiderEverything);
		}
	}
}
