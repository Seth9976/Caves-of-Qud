using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000072 RID: 114
	public class Compute_DT_EventArgs
	{
		// Token: 0x060005A9 RID: 1449 RVA: 0x0002DCA5 File Offset: 0x0002BEA5
		public Compute_DT_EventArgs(Compute_DistanceTransform_EventTypes type, float progress)
		{
			this.EventType = type;
			this.ProgressPercentage = progress;
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0002DCBB File Offset: 0x0002BEBB
		public Compute_DT_EventArgs(Compute_DistanceTransform_EventTypes type, Color[] colors)
		{
			this.EventType = type;
			this.Colors = colors;
		}

		// Token: 0x04000501 RID: 1281
		public Compute_DistanceTransform_EventTypes EventType;

		// Token: 0x04000502 RID: 1282
		public float ProgressPercentage;

		// Token: 0x04000503 RID: 1283
		public Color[] Colors;
	}
}
