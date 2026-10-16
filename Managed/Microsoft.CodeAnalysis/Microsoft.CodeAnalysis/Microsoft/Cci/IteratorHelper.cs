using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Microsoft.Cci
{
	// Token: 0x020000B0 RID: 176
	[NullableContext(2)]
	[Nullable(0)]
	internal static class IteratorHelper
	{
		// Token: 0x060005D3 RID: 1491 RVA: 0x00015F6C File Offset: 0x0001416C
		public static bool EnumerableIsNotEmpty<T>([Nullable(new byte[] { 2, 1 })] [NotNullWhen(true)] IEnumerable<T> enumerable)
		{
			if (enumerable == null)
			{
				return false;
			}
			IList<T> list = enumerable as IList<T>;
			if (list != null)
			{
				return list.Count != 0;
			}
			IList list2 = enumerable as IList;
			if (list2 != null)
			{
				return list2.Count != 0;
			}
			return enumerable.GetEnumerator().MoveNext();
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00015FB1 File Offset: 0x000141B1
		public static bool EnumerableIsEmpty<T>([Nullable(new byte[] { 2, 1 })] [NotNullWhen(false)] IEnumerable<T> enumerable)
		{
			return !IteratorHelper.EnumerableIsNotEmpty<T>(enumerable);
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00015FBC File Offset: 0x000141BC
		public static uint EnumerableCount<T>([Nullable(new byte[] { 2, 1 })] IEnumerable<T> enumerable)
		{
			if (enumerable == null)
			{
				return 0U;
			}
			IList<T> list = enumerable as IList<T>;
			if (list != null)
			{
				return (uint)list.Count;
			}
			IList list2 = enumerable as IList;
			if (list2 != null)
			{
				return (uint)list2.Count;
			}
			uint num = 0U;
			IEnumerator<T> enumerator = enumerable.GetEnumerator();
			while (enumerator.MoveNext())
			{
				num += 1U;
			}
			return num & 2147483647U;
		}
	}
}
