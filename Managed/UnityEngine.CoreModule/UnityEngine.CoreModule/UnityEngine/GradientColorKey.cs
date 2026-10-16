using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000210 RID: 528
	[UsedByNativeCode]
	public struct GradientColorKey
	{
		// Token: 0x06001BC8 RID: 7112 RVA: 0x000323D5 File Offset: 0x000305D5
		public GradientColorKey(Color col, float time)
		{
			this.color = col;
			this.time = time;
		}

		// Token: 0x0400086F RID: 2159
		public Color color;

		// Token: 0x04000870 RID: 2160
		public float time;
	}
}
