using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000037 RID: 55
	internal static class SetPropertyUtility
	{
		// Token: 0x060002B1 RID: 689 RVA: 0x0000FBC0 File Offset: 0x0000DDC0
		public static bool SetColor(ref Color currentValue, Color newValue)
		{
			if (currentValue.r == newValue.r && currentValue.g == newValue.g && currentValue.b == newValue.b && currentValue.a == newValue.a)
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000FC0F File Offset: 0x0000DE0F
		public static bool SetEquatableStruct<T>(ref T currentValue, T newValue) where T : IEquatable<T>
		{
			if (currentValue.Equals(newValue))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000FC2A File Offset: 0x0000DE2A
		public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
		{
			if (currentValue.Equals(newValue))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000FC4C File Offset: 0x0000DE4C
		public static bool SetClass<T>(ref T currentValue, T newValue) where T : class
		{
			if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}
	}
}
