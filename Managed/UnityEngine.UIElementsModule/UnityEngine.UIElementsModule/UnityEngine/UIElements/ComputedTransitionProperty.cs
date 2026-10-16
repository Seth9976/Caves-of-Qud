using System;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x0200034E RID: 846
	internal struct ComputedTransitionProperty
	{
		// Token: 0x04000D53 RID: 3411
		public StylePropertyId id;

		// Token: 0x04000D54 RID: 3412
		public int durationMs;

		// Token: 0x04000D55 RID: 3413
		public int delayMs;

		// Token: 0x04000D56 RID: 3414
		public Func<float, float> easingCurve;
	}
}
