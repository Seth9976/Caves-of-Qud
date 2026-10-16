using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200082B RID: 2091
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class PreventSmartUseEvent : IActOnItemEvent
	{
		// Token: 0x06005325 RID: 21285 RVA: 0x0018EB46 File Offset: 0x0018CD46
		public PreventSmartUseEvent()
		{
			this.ID = PreventSmartUseEvent.ID;
		}

		// Token: 0x06005326 RID: 21286 RVA: 0x0018EB59 File Offset: 0x0018CD59
		public static int CountPool()
		{
			if (PreventSmartUseEvent.Pool != null)
			{
				return PreventSmartUseEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06005327 RID: 21287 RVA: 0x0018EB6E File Offset: 0x0018CD6E
		public static void ResetPool()
		{
			while (PreventSmartUseEvent.PoolCounter > 0)
			{
				PreventSmartUseEvent.Pool[--PreventSmartUseEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06005328 RID: 21288 RVA: 0x0018EB96 File Offset: 0x0018CD96
		public static void ResetTo(ref PreventSmartUseEvent E)
		{
			MinEvent.ResetTo<PreventSmartUseEvent>(E, PreventSmartUseEvent.Pool, ref PreventSmartUseEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06005329 RID: 21289 RVA: 0x0018EBAC File Offset: 0x0018CDAC
		public static PreventSmartUseEvent FromPool()
		{
			return MinEvent.FromPool<PreventSmartUseEvent>(ref PreventSmartUseEvent.Pool, ref PreventSmartUseEvent.PoolCounter, 8192);
		}

		// Token: 0x0600532A RID: 21290 RVA: 0x0018EBC2 File Offset: 0x0018CDC2
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x0600532B RID: 21291 RVA: 0x0018EBD6 File Offset: 0x0018CDD6
		public static PreventSmartUseEvent FromPool(GameObject Actor, GameObject Item)
		{
			PreventSmartUseEvent preventSmartUseEvent = PreventSmartUseEvent.FromPool();
			preventSmartUseEvent.Actor = Actor;
			preventSmartUseEvent.Item = Item;
			return preventSmartUseEvent;
		}

		// Token: 0x040029D8 RID: 10712
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(PreventSmartUseEvent), null, new MinEvent.EventPoolCount(PreventSmartUseEvent.CountPool), new MinEvent.EventPoolReset(PreventSmartUseEvent.ResetPool));

		// Token: 0x040029D9 RID: 10713
		private static List<PreventSmartUseEvent> Pool;

		// Token: 0x040029DA RID: 10714
		private static int PoolCounter;
	}
}
