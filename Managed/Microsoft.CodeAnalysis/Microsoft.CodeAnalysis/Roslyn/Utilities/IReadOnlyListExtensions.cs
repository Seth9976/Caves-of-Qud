using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Roslyn.Utilities
{
	// Token: 0x0200004B RID: 75
	internal static class IReadOnlyListExtensions
	{
		// Token: 0x06000192 RID: 402 RVA: 0x00006284 File Offset: 0x00004484
		[NullableContext(1)]
		public static bool Contains<[Nullable(2)] T>(this IReadOnlyList<T> list, T item, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> comparer = null)
		{
			if (comparer == null)
			{
				comparer = EqualityComparer<T>.Default;
			}
			for (int i = 0; i < list.Count; i++)
			{
				if (comparer.Equals(item, list[i]))
				{
					return true;
				}
			}
			return false;
		}
	}
}
