using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020006B2 RID: 1714
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class BootSequenceAbortedEvent : IBootSequenceEvent
	{
		// Token: 0x060048DB RID: 18651 RVA: 0x0017028D File Offset: 0x0016E48D
		public BootSequenceAbortedEvent()
		{
			this.ID = BootSequenceAbortedEvent.ID;
		}

		// Token: 0x060048DC RID: 18652 RVA: 0x001702A0 File Offset: 0x0016E4A0
		public static int CountPool()
		{
			if (BootSequenceAbortedEvent.Pool != null)
			{
				return BootSequenceAbortedEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060048DD RID: 18653 RVA: 0x001702B5 File Offset: 0x0016E4B5
		public static void ResetPool()
		{
			while (BootSequenceAbortedEvent.PoolCounter > 0)
			{
				BootSequenceAbortedEvent.Pool[--BootSequenceAbortedEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060048DE RID: 18654 RVA: 0x001702DD File Offset: 0x0016E4DD
		public static void ResetTo(ref BootSequenceAbortedEvent E)
		{
			MinEvent.ResetTo<BootSequenceAbortedEvent>(E, BootSequenceAbortedEvent.Pool, ref BootSequenceAbortedEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060048DF RID: 18655 RVA: 0x001702F3 File Offset: 0x0016E4F3
		public static BootSequenceAbortedEvent FromPool()
		{
			return MinEvent.FromPool<BootSequenceAbortedEvent>(ref BootSequenceAbortedEvent.Pool, ref BootSequenceAbortedEvent.PoolCounter, 8192);
		}

		// Token: 0x060048E0 RID: 18656 RVA: 0x00170309 File Offset: 0x0016E509
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060048E1 RID: 18657 RVA: 0x0017031D File Offset: 0x0016E51D
		public static BootSequenceAbortedEvent FromPool(GameObject Object)
		{
			BootSequenceAbortedEvent bootSequenceAbortedEvent = BootSequenceAbortedEvent.FromPool();
			bootSequenceAbortedEvent.Object = Object;
			return bootSequenceAbortedEvent;
		}

		// Token: 0x060048E2 RID: 18658 RVA: 0x0017032B File Offset: 0x0016E52B
		public static void Send(GameObject Object)
		{
			if (!Object.FireEvent("BootSequenceAborted"))
			{
				return;
			}
			if (Object.WantEvent(BootSequenceAbortedEvent.ID, MinEvent.CascadeLevel))
			{
				Object.HandleEvent(BootSequenceAbortedEvent.FromPool(Object));
			}
		}

		// Token: 0x040022F8 RID: 8952
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(BootSequenceAbortedEvent), null, new MinEvent.EventPoolCount(BootSequenceAbortedEvent.CountPool), new MinEvent.EventPoolReset(BootSequenceAbortedEvent.ResetPool));

		// Token: 0x040022F9 RID: 8953
		private static List<BootSequenceAbortedEvent> Pool;

		// Token: 0x040022FA RID: 8954
		private static int PoolCounter;
	}
}
