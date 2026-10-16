using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000024 RID: 36
	internal abstract class RuntimeElement : IInterval
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000215 RID: 533
		public abstract long intervalStart { get; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000216 RID: 534
		public abstract long intervalEnd { get; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000823D File Offset: 0x0000643D
		// (set) Token: 0x06000218 RID: 536 RVA: 0x00008245 File Offset: 0x00006445
		public int intervalBit { get; set; }

		// Token: 0x1700009F RID: 159
		// (set) Token: 0x06000219 RID: 537
		public abstract bool enable { set; }

		// Token: 0x0600021A RID: 538
		public abstract void EvaluateAt(double localTime, FrameData frameData);

		// Token: 0x0600021B RID: 539
		public abstract void DisableAt(double localTime, double rootDuration, FrameData frameData);
	}
}
