using System;
using System.Collections.Generic;
using System.Linq;

namespace AiUnity.Common.Utilities
{
	// Token: 0x020002BB RID: 699
	public static class EnumUtility
	{
		// Token: 0x0600186A RID: 6250 RVA: 0x0007EF9C File Offset: 0x0007D19C
		public static IEnumerable<T> GetValues<T>(bool allowZero = true, bool allowCombinators = false)
		{
			return ((T[])Enum.GetValues(typeof(T))).Where<T>((T t) => (allowZero || Convert.ToInt32(t) > 0) && (allowCombinators || !typeof(T).IsDefined(typeof(FlagsAttribute), false) || (Convert.ToInt32(t) & (Convert.ToInt32(t) - 1)) == 0));
		}
	}
}
