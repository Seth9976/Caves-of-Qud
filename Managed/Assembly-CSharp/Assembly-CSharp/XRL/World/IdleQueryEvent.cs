using System;

namespace XRL.World
{
	// Token: 0x020007E3 RID: 2019
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class IdleQueryEvent : PooledEvent<IdleQueryEvent>
	{
		// Token: 0x0600512B RID: 20779 RVA: 0x00188B44 File Offset: 0x00186D44
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600512C RID: 20780 RVA: 0x00188B4D File Offset: 0x00186D4D
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
		}

		// Token: 0x0600512D RID: 20781 RVA: 0x00188B5C File Offset: 0x00186D5C
		public static IdleQueryEvent FromPool(GameObject Actor)
		{
			IdleQueryEvent idleQueryEvent = PooledEvent<IdleQueryEvent>.FromPool();
			idleQueryEvent.Actor = Actor;
			return idleQueryEvent;
		}

		// Token: 0x040028A1 RID: 10401
		public GameObject Actor;
	}
}
