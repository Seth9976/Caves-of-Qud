using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000136 RID: 310
	internal static class InputArrayExtensions
	{
		// Token: 0x0600114C RID: 4428 RVA: 0x00052D04 File Offset: 0x00050F04
		public static int IndexOfReference<TValue>(this InlinedArray<TValue> array, TValue value) where TValue : class
		{
			for (int i = 0; i < array.length; i++)
			{
				if (array[i] == value)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x00052D3C File Offset: 0x00050F3C
		public static bool Contains<TValue>(this InlinedArray<TValue> array, TValue value)
		{
			for (int i = 0; i < array.length; i++)
			{
				TValue tvalue = array[i];
				if (tvalue.Equals(value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x00052D7B File Offset: 0x00050F7B
		public static bool ContainsReference<TValue>(this InlinedArray<TValue> array, TValue value) where TValue : class
		{
			return array.IndexOfReference(value) != -1;
		}
	}
}
