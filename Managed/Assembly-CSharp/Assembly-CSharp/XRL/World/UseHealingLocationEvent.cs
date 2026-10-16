using System;

namespace XRL.World
{
	// Token: 0x02000875 RID: 2165
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class UseHealingLocationEvent : PooledEvent<UseHealingLocationEvent>
	{
		// Token: 0x060055FD RID: 22013 RVA: 0x0019A02A File Offset: 0x0019822A
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060055FE RID: 22014 RVA: 0x0019A033 File Offset: 0x00198233
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Cell = null;
		}

		// Token: 0x060055FF RID: 22015 RVA: 0x0019A049 File Offset: 0x00198249
		public static UseHealingLocationEvent FromPool(GameObject Actor, Cell Cell)
		{
			UseHealingLocationEvent useHealingLocationEvent = PooledEvent<UseHealingLocationEvent>.FromPool();
			useHealingLocationEvent.Actor = Actor;
			useHealingLocationEvent.Cell = Cell;
			return useHealingLocationEvent;
		}

		// Token: 0x06005600 RID: 22016 RVA: 0x0019A05E File Offset: 0x0019825E
		public static void Send(GameObject Actor, Cell Cell)
		{
			Cell.HandleEvent(UseHealingLocationEvent.FromPool(Actor, Cell));
		}

		// Token: 0x04002B54 RID: 11092
		public GameObject Actor;

		// Token: 0x04002B55 RID: 11093
		public Cell Cell;
	}
}
