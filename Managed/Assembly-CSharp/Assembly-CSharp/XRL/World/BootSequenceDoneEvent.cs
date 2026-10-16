using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020006B3 RID: 1715
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class BootSequenceDoneEvent : IBootSequenceEvent
	{
		// Token: 0x060048E4 RID: 18660 RVA: 0x00170389 File Offset: 0x0016E589
		public BootSequenceDoneEvent()
		{
			this.ID = BootSequenceDoneEvent.ID;
		}

		// Token: 0x060048E5 RID: 18661 RVA: 0x0017039C File Offset: 0x0016E59C
		public static int CountPool()
		{
			if (BootSequenceDoneEvent.Pool != null)
			{
				return BootSequenceDoneEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060048E6 RID: 18662 RVA: 0x001703B1 File Offset: 0x0016E5B1
		public static void ResetPool()
		{
			while (BootSequenceDoneEvent.PoolCounter > 0)
			{
				BootSequenceDoneEvent.Pool[--BootSequenceDoneEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060048E7 RID: 18663 RVA: 0x001703D9 File Offset: 0x0016E5D9
		public static void ResetTo(ref BootSequenceDoneEvent E)
		{
			MinEvent.ResetTo<BootSequenceDoneEvent>(E, BootSequenceDoneEvent.Pool, ref BootSequenceDoneEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060048E8 RID: 18664 RVA: 0x001703EF File Offset: 0x0016E5EF
		public static BootSequenceDoneEvent FromPool()
		{
			return MinEvent.FromPool<BootSequenceDoneEvent>(ref BootSequenceDoneEvent.Pool, ref BootSequenceDoneEvent.PoolCounter, 8192);
		}

		// Token: 0x060048E9 RID: 18665 RVA: 0x00170405 File Offset: 0x0016E605
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060048EA RID: 18666 RVA: 0x00170419 File Offset: 0x0016E619
		public static BootSequenceDoneEvent FromPool(GameObject Object)
		{
			BootSequenceDoneEvent bootSequenceDoneEvent = BootSequenceDoneEvent.FromPool();
			bootSequenceDoneEvent.Object = Object;
			return bootSequenceDoneEvent;
		}

		// Token: 0x060048EB RID: 18667 RVA: 0x00170427 File Offset: 0x0016E627
		public static void Send(GameObject Object)
		{
			if (!Object.FireEvent("BootSequenceDone"))
			{
				return;
			}
			if (Object.WantEvent(BootSequenceDoneEvent.ID, MinEvent.CascadeLevel))
			{
				Object.HandleEvent(BootSequenceDoneEvent.FromPool(Object));
			}
		}

		// Token: 0x040022FB RID: 8955
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(BootSequenceDoneEvent), null, new MinEvent.EventPoolCount(BootSequenceDoneEvent.CountPool), new MinEvent.EventPoolReset(BootSequenceDoneEvent.ResetPool));

		// Token: 0x040022FC RID: 8956
		private static List<BootSequenceDoneEvent> Pool;

		// Token: 0x040022FD RID: 8957
		private static int PoolCounter;
	}
}
