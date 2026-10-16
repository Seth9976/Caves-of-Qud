using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200011C RID: 284
	internal static class HashSetExtensions
	{
		// Token: 0x060009D0 RID: 2512 RVA: 0x0001C06C File Offset: 0x0001A26C
		[NullableContext(2)]
		internal static bool IsNullOrEmpty<T>([Nullable(new byte[] { 2, 1 })] [NotNullWhen(false)] this HashSet<T> hashSet)
		{
			return hashSet == null || hashSet.Count == 0;
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0001C07C File Offset: 0x0001A27C
		[NullableContext(1)]
		internal static bool InitializeAndAdd<T>([Nullable(new byte[] { 2, 1 })] [NotNullIfNotNull("item")] [NotNullWhen(true)] ref HashSet<T> hashSet, [Nullable(2)] [NotNullWhen(true)] T item) where T : class
		{
			if (item == null)
			{
				return false;
			}
			if (hashSet == null)
			{
				hashSet = new HashSet<T>();
			}
			return hashSet.Add(item);
		}
	}
}
