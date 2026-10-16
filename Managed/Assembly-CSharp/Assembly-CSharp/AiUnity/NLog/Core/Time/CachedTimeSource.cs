using System;

namespace AiUnity.NLog.Core.Time
{
	// Token: 0x02000213 RID: 531
	public abstract class CachedTimeSource : TimeSource
	{
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06001374 RID: 4980
		protected abstract DateTime FreshTime { get; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06001375 RID: 4981 RVA: 0x00070638 File Offset: 0x0006E838
		public override DateTime Time
		{
			get
			{
				int tickCount = Environment.TickCount;
				if (tickCount == this.lastTicks)
				{
					return this.lastTime;
				}
				DateTime freshTime = this.FreshTime;
				this.lastTicks = tickCount;
				this.lastTime = freshTime;
				return freshTime;
			}
		}

		// Token: 0x04000CBF RID: 3263
		private int lastTicks = -1;

		// Token: 0x04000CC0 RID: 3264
		private DateTime lastTime = DateTime.MinValue;
	}
}
