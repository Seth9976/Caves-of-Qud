using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000117 RID: 279
	[NullableContext(2)]
	[Nullable(0)]
	internal static class CollectionsExtensions
	{
		// Token: 0x060009C1 RID: 2497 RVA: 0x0001BEB1 File Offset: 0x0001A0B1
		internal static bool IsNullOrEmpty<T>([Nullable(new byte[] { 2, 1 })] [NotNullWhen(false)] this ICollection<T> collection)
		{
			return collection == null || collection.Count == 0;
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0001BEC1 File Offset: 0x0001A0C1
		internal static bool IsNullOrEmpty<T>([Nullable(new byte[] { 2, 1 })] [NotNullWhen(false)] this IReadOnlyCollection<T> collection)
		{
			return collection == null || collection.Count == 0;
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0001BED1 File Offset: 0x0001A0D1
		internal static bool IsNullOrEmpty<T>([Nullable(new byte[] { 2, 1 })] [NotNullWhen(false)] this ImmutableHashSet<T> hashSet)
		{
			return hashSet == null || hashSet.Count == 0;
		}
	}
}
