using System;
using System.Collections.Generic;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000148 RID: 328
	public static class ReadOnlyArrayExtensions
	{
		// Token: 0x0600121F RID: 4639 RVA: 0x00056264 File Offset: 0x00054464
		public static bool Contains<TValue>(this ReadOnlyArray<TValue> array, TValue value) where TValue : IComparable<TValue>
		{
			for (int i = 0; i < array.m_Length; i++)
			{
				if (array.m_Array[array.m_StartIndex + i].CompareTo(value) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x000562A8 File Offset: 0x000544A8
		public static bool ContainsReference<TValue>(this ReadOnlyArray<TValue> array, TValue value) where TValue : class
		{
			return array.IndexOfReference(value) != -1;
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x000562B8 File Offset: 0x000544B8
		public static int IndexOfReference<TValue>(this ReadOnlyArray<TValue> array, TValue value) where TValue : class
		{
			for (int i = 0; i < array.m_Length; i++)
			{
				if (array.m_Array[array.m_StartIndex + i] == value)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x000562FC File Offset: 0x000544FC
		internal static bool HaveEqualReferences<TValue>(this ReadOnlyArray<TValue> array1, IReadOnlyList<TValue> array2, int count = 2147483647)
		{
			int num = Math.Min(array1.Count, count);
			int num2 = Math.Min(array2.Count, count);
			if (num != num2)
			{
				return false;
			}
			for (int i = 0; i < num; i++)
			{
				if (array1[i] != array2[i])
				{
					return false;
				}
			}
			return true;
		}
	}
}
