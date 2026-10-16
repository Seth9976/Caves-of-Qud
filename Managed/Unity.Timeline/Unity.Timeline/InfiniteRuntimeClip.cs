using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x0200001E RID: 30
	internal class InfiniteRuntimeClip : RuntimeElement
	{
		// Token: 0x060001EF RID: 495 RVA: 0x00007902 File Offset: 0x00005B02
		public InfiniteRuntimeClip(Playable playable)
		{
			this.m_Playable = playable;
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00007911 File Offset: 0x00005B11
		public override long intervalStart
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00007915 File Offset: 0x00005B15
		public override long intervalEnd
		{
			get
			{
				return InfiniteRuntimeClip.kIntervalEnd;
			}
		}

		// Token: 0x1700008E RID: 142
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x0000791C File Offset: 0x00005B1C
		public override bool enable
		{
			set
			{
				if (value)
				{
					this.m_Playable.Play<Playable>();
					return;
				}
				this.m_Playable.Pause<Playable>();
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00007938 File Offset: 0x00005B38
		public override void EvaluateAt(double localTime, FrameData frameData)
		{
			this.m_Playable.SetTime(localTime);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00007946 File Offset: 0x00005B46
		public override void DisableAt(double localTime, double rootDuration, FrameData frameData)
		{
			this.m_Playable.SetTime(localTime);
			this.enable = false;
		}

		// Token: 0x040000B1 RID: 177
		private Playable m_Playable;

		// Token: 0x040000B2 RID: 178
		private static readonly long kIntervalEnd = DiscreteTime.GetNearestTick(TimelineClip.kMaxTimeValue);
	}
}
