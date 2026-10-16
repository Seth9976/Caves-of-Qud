using System;
using XRL.Core;

namespace QupKit
{
	// Token: 0x020003AB RID: 939
	public class ThreadTaskQueueEntry
	{
		// Token: 0x06002309 RID: 8969 RVA: 0x000B20EA File Offset: 0x000B02EA
		public ThreadTaskQueueEntry(Action a)
		{
			this.actionToTake = a;
		}

		// Token: 0x0600230A RID: 8970 RVA: 0x000B20F9 File Offset: 0x000B02F9
		public void SetAction(Action a)
		{
			this.actionToTake = a;
		}

		// Token: 0x0600230B RID: 8971 RVA: 0x000B2102 File Offset: 0x000B0302
		public void Clear()
		{
			this.actionToTake = null;
			this.TaskID = null;
		}

		// Token: 0x0600230C RID: 8972 RVA: 0x000B2114 File Offset: 0x000B0314
		public void Execute(ThreadTaskQueue parentQueue)
		{
			if (this.actionToTake != null)
			{
				try
				{
					this.actionToTake();
				}
				catch (Exception ex)
				{
					XRLCore.LogError(ex);
				}
			}
			this.Clear();
			parentQueue.poolEntry(this);
		}

		// Token: 0x040011A9 RID: 4521
		public int Delay;

		// Token: 0x040011AA RID: 4522
		public string TaskID;

		// Token: 0x040011AB RID: 4523
		private Action actionToTake;
	}
}
