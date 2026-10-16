using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000211 RID: 529
	[UsedByNativeCode]
	public struct GradientAlphaKey
	{
		// Token: 0x06001BC9 RID: 7113 RVA: 0x000323E6 File Offset: 0x000305E6
		public GradientAlphaKey(float alpha, float time)
		{
			this.alpha = alpha;
			this.time = time;
		}

		// Token: 0x04000871 RID: 2161
		public float alpha;

		// Token: 0x04000872 RID: 2162
		public float time;
	}
}
