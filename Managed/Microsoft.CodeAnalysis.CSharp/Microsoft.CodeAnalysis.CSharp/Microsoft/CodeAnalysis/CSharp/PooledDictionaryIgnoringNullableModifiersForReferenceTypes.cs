using System;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000068 RID: 104
	internal static class PooledDictionaryIgnoringNullableModifiersForReferenceTypes
	{
		// Token: 0x0600095B RID: 2395 RVA: 0x00049A53 File Offset: 0x00047C53
		internal static PooledDictionary<NamedTypeSymbol, NamedTypeSymbol> GetInstance()
		{
			return PooledDictionaryIgnoringNullableModifiersForReferenceTypes.s_poolInstance.Allocate();
		}

		// Token: 0x04000446 RID: 1094
		private static readonly ObjectPool<PooledDictionary<NamedTypeSymbol, NamedTypeSymbol>> s_poolInstance = PooledDictionary<NamedTypeSymbol, NamedTypeSymbol>.CreatePool(SymbolEqualityComparer.IgnoringNullable);
	}
}
