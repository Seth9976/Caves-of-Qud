using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020006B4 RID: 1716
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class BootSequenceInitializedEvent : IBootSequenceEvent
	{
		// Token: 0x060048ED RID: 18669 RVA: 0x00170485 File Offset: 0x0016E685
		public BootSequenceInitializedEvent()
		{
			this.ID = BootSequenceInitializedEvent.ID;
		}

		// Token: 0x060048EE RID: 18670 RVA: 0x00170498 File Offset: 0x0016E698
		public static int CountPool()
		{
			if (BootSequenceInitializedEvent.Pool != null)
			{
				return BootSequenceInitializedEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060048EF RID: 18671 RVA: 0x001704AD File Offset: 0x0016E6AD
		public static void ResetPool()
		{
			while (BootSequenceInitializedEvent.PoolCounter > 0)
			{
				BootSequenceInitializedEvent.Pool[--BootSequenceInitializedEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060048F0 RID: 18672 RVA: 0x001704D5 File Offset: 0x0016E6D5
		public static void ResetTo(ref BootSequenceInitializedEvent E)
		{
			MinEvent.ResetTo<BootSequenceInitializedEvent>(E, BootSequenceInitializedEvent.Pool, ref BootSequenceInitializedEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060048F1 RID: 18673 RVA: 0x001704EB File Offset: 0x0016E6EB
		public static BootSequenceInitializedEvent FromPool()
		{
			return MinEvent.FromPool<BootSequenceInitializedEvent>(ref BootSequenceInitializedEvent.Pool, ref BootSequenceInitializedEvent.PoolCounter, 8192);
		}

		// Token: 0x060048F2 RID: 18674 RVA: 0x00170501 File Offset: 0x0016E701
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060048F3 RID: 18675 RVA: 0x00170515 File Offset: 0x0016E715
		public static BootSequenceInitializedEvent FromPool(GameObject Object)
		{
			BootSequenceInitializedEvent bootSequenceInitializedEvent = BootSequenceInitializedEvent.FromPool();
			bootSequenceInitializedEvent.Object = Object;
			return bootSequenceInitializedEvent;
		}

		// Token: 0x060048F4 RID: 18676 RVA: 0x00170523 File Offset: 0x0016E723
		public static void Send(GameObject Object)
		{
			if (!Object.FireEvent("BootSequenceInitialized"))
			{
				return;
			}
			if (Object.WantEvent(BootSequenceInitializedEvent.ID, MinEvent.CascadeLevel))
			{
				Object.HandleEvent(BootSequenceInitializedEvent.FromPool(Object));
			}
		}

		// Token: 0x040022FE RID: 8958
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(BootSequenceInitializedEvent), null, new MinEvent.EventPoolCount(BootSequenceInitializedEvent.CountPool), new MinEvent.EventPoolReset(BootSequenceInitializedEvent.ResetPool));

		// Token: 0x040022FF RID: 8959
		private static List<BootSequenceInitializedEvent> Pool;

		// Token: 0x04002300 RID: 8960
		private static int PoolCounter;
	}
}
