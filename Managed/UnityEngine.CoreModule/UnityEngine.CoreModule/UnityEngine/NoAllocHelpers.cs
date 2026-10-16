using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine
{
	// Token: 0x020002A2 RID: 674
	internal static class NoAllocHelpers
	{
		// Token: 0x06002180 RID: 8576 RVA: 0x000411C8 File Offset: 0x0003F3C8
		public unsafe static void EnsureListElemCount<T>(List<T> list, int count)
		{
			bool flag = list == null;
			if (flag)
			{
				throw new ArgumentNullException("list");
			}
			bool flag2 = count < 0;
			if (flag2)
			{
				throw new ArgumentException("invalid size to resize.", "list");
			}
			list.Clear();
			bool flag3 = list.Capacity < count;
			if (flag3)
			{
				list.Capacity = count;
			}
			bool flag4 = count != list.Count;
			if (flag4)
			{
				NoAllocHelpers.ListPrivateFieldAccess<T> listPrivateFieldAccess = *UnsafeUtility.As<List<T>, NoAllocHelpers.ListPrivateFieldAccess<T>>(ref list);
				listPrivateFieldAccess._size = count;
				listPrivateFieldAccess._version++;
			}
		}

		// Token: 0x06002181 RID: 8577 RVA: 0x00041250 File Offset: 0x0003F450
		public static int SafeLength(Array values)
		{
			return (values != null) ? values.Length : 0;
		}

		// Token: 0x06002182 RID: 8578 RVA: 0x00041270 File Offset: 0x0003F470
		public static int SafeLength<T>(List<T> values)
		{
			return (values != null) ? values.Count : 0;
		}

		// Token: 0x06002183 RID: 8579 RVA: 0x00041290 File Offset: 0x0003F490
		[Obsolete("Use ExtractArrayFromList", false)]
		public static T[] ExtractArrayFromListT<T>(List<T> list)
		{
			return NoAllocHelpers.ExtractArrayFromList<T>(list);
		}

		// Token: 0x06002184 RID: 8580 RVA: 0x000412A8 File Offset: 0x0003F4A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T[] ExtractArrayFromList<T>(List<T> list)
		{
			bool flag = list == null;
			T[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				NoAllocHelpers.ListPrivateFieldAccess<T> listPrivateFieldAccess = UnsafeUtility.As<NoAllocHelpers.ListPrivateFieldAccess<T>>(list);
				array = listPrivateFieldAccess._items;
			}
			return array;
		}

		// Token: 0x06002185 RID: 8581 RVA: 0x000412D4 File Offset: 0x0003F4D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ResetListContents<T>(List<T> list, ReadOnlySpan<T> span)
		{
			NoAllocHelpers.ListPrivateFieldAccess<T> listPrivateFieldAccess = UnsafeUtility.As<NoAllocHelpers.ListPrivateFieldAccess<T>>(list);
			listPrivateFieldAccess._items = span.ToArray();
			listPrivateFieldAccess._size = span.Length;
			listPrivateFieldAccess._version++;
		}

		// Token: 0x06002186 RID: 8582 RVA: 0x00041314 File Offset: 0x0003F514
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ResetListSize<[IsUnmanaged] T>(List<T> list, int size) where T : struct, ValueType
		{
			Debug.Assert(list.Capacity >= size);
			NoAllocHelpers.ListPrivateFieldAccess<T> listPrivateFieldAccess = UnsafeUtility.As<NoAllocHelpers.ListPrivateFieldAccess<T>>(list);
			listPrivateFieldAccess._size = size;
			listPrivateFieldAccess._version++;
		}

		// Token: 0x020002A3 RID: 675
		private class ListPrivateFieldAccess<T>
		{
			// Token: 0x040009C6 RID: 2502
			internal T[] _items;

			// Token: 0x040009C7 RID: 2503
			internal int _size;

			// Token: 0x040009C8 RID: 2504
			internal int _version;
		}
	}
}
