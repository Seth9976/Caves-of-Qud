using System;

namespace QupKit
{
	// Token: 0x020003A8 RID: 936
	public class ScheduleEntry
	{
		// Token: 0x06002302 RID: 8962 RVA: 0x000B1F42 File Offset: 0x000B0142
		public ScheduleEntry(float T, Action A, float ScheduledTime)
		{
			this.Time = T;
			this.OnExecute = A;
		}

		// Token: 0x06002303 RID: 8963 RVA: 0x000B1F58 File Offset: 0x000B0158
		public virtual void Execute()
		{
			if (this.OnExecute != null)
			{
				this.OnExecute();
				this.OnExecute = null;
			}
		}

		// Token: 0x040011A4 RID: 4516
		public float ScheduledTime;

		// Token: 0x040011A5 RID: 4517
		public float Time;

		// Token: 0x040011A6 RID: 4518
		public Action OnExecute;
	}
}
