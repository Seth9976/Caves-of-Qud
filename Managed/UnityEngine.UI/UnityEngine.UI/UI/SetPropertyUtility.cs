using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x02000036 RID: 54
	internal static class SetPropertyUtility
	{
		// Token: 0x060003FD RID: 1021 RVA: 0x00014020 File Offset: 0x00012220
		public static bool SetColor(ref Color currentValue, Color newValue)
		{
			if (currentValue.r == newValue.r && currentValue.g == newValue.g && currentValue.b == newValue.b && currentValue.a == newValue.a)
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0001406F File Offset: 0x0001226F
		public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
		{
			if (EqualityComparer<T>.Default.Equals(currentValue, newValue))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00014090 File Offset: 0x00012290
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
