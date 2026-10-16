using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000296 RID: 662
	internal class EnumInfo
	{
		// Token: 0x060020A3 RID: 8355 RVA: 0x0003F634 File Offset: 0x0003D834
		[UsedByNativeCode]
		internal static EnumInfo CreateEnumInfoFromNativeEnum(string[] names, int[] values, string[] annotations, bool isFlags)
		{
			return new EnumInfo
			{
				names = names,
				values = values,
				annotations = annotations,
				isFlags = isFlags
			};
		}

		// Token: 0x040009B6 RID: 2486
		public string[] names;

		// Token: 0x040009B7 RID: 2487
		public int[] values;

		// Token: 0x040009B8 RID: 2488
		public string[] annotations;

		// Token: 0x040009B9 RID: 2489
		public bool isFlags;
	}
}
