using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000290 RID: 656
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal struct EnumData
	{
		// Token: 0x0400099F RID: 2463
		public Enum[] values;

		// Token: 0x040009A0 RID: 2464
		public int[] flagValues;

		// Token: 0x040009A1 RID: 2465
		public string[] displayNames;

		// Token: 0x040009A2 RID: 2466
		public string[] names;

		// Token: 0x040009A3 RID: 2467
		public string[] tooltip;

		// Token: 0x040009A4 RID: 2468
		public bool flags;

		// Token: 0x040009A5 RID: 2469
		public Type underlyingType;

		// Token: 0x040009A6 RID: 2470
		public bool unsigned;

		// Token: 0x040009A7 RID: 2471
		public bool serializable;
	}
}
