using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000023 RID: 35
	internal abstract class RuntimeClipBase : RuntimeElement
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000210 RID: 528
		public abstract double start { get; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000211 RID: 529
		public abstract double duration { get; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00008214 File Offset: 0x00006414
		public override long intervalStart
		{
			get
			{
				return DiscreteTime.GetNearestTick(this.start);
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00008221 File Offset: 0x00006421
		public override long intervalEnd
		{
			get
			{
				return DiscreteTime.GetNearestTick(this.start + this.duration);
			}
		}
	}
}
