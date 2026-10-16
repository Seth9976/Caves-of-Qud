using System;

namespace XRL.World
{
	// Token: 0x02000826 RID: 2086
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class PollForHealingLocationEvent : PooledEvent<PollForHealingLocationEvent>
	{
		// Token: 0x06005302 RID: 21250 RVA: 0x0018E65F File Offset: 0x0018C85F
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005303 RID: 21251 RVA: 0x0018E668 File Offset: 0x0018C868
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Cell = null;
			this.Value = 0;
			this.First = false;
		}

		// Token: 0x06005304 RID: 21252 RVA: 0x0018E68C File Offset: 0x0018C88C
		public static PollForHealingLocationEvent FromPool(GameObject Actor, Cell Cell, bool First = false)
		{
			PollForHealingLocationEvent pollForHealingLocationEvent = PooledEvent<PollForHealingLocationEvent>.FromPool();
			pollForHealingLocationEvent.Actor = Actor;
			pollForHealingLocationEvent.Cell = Cell;
			pollForHealingLocationEvent.Value = 0;
			pollForHealingLocationEvent.First = First;
			return pollForHealingLocationEvent;
		}

		// Token: 0x06005305 RID: 21253 RVA: 0x0018E6B0 File Offset: 0x0018C8B0
		public static int GetFor(GameObject Actor, Cell Cell, bool First = false)
		{
			if (Cell.WantEvent(PooledEvent<PollForHealingLocationEvent>.ID, MinEvent.CascadeLevel))
			{
				PollForHealingLocationEvent pollForHealingLocationEvent = PollForHealingLocationEvent.FromPool(Actor, Cell, First);
				Cell.HandleEvent(pollForHealingLocationEvent);
				return pollForHealingLocationEvent.Value;
			}
			return 0;
		}

		// Token: 0x040029C2 RID: 10690
		public GameObject Actor;

		// Token: 0x040029C3 RID: 10691
		public Cell Cell;

		// Token: 0x040029C4 RID: 10692
		public int Value;

		// Token: 0x040029C5 RID: 10693
		public bool First;
	}
}
