using System;

namespace XRL.World
{
	// Token: 0x0200082F RID: 2095
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Pool)]
	public class QueryBroadcastDrawEvent : PooledEvent<QueryBroadcastDrawEvent>
	{
		// Token: 0x06005340 RID: 21312 RVA: 0x0018EE65 File Offset: 0x0018D065
		public override int GetCascadeLevel()
		{
			return QueryBroadcastDrawEvent.CascadeLevel;
		}

		// Token: 0x06005341 RID: 21313 RVA: 0x0018EE6C File Offset: 0x0018D06C
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005342 RID: 21314 RVA: 0x0018EE75 File Offset: 0x0018D075
		public override void Reset()
		{
			base.Reset();
			this.Zone = null;
			this.Draw = 0;
		}

		// Token: 0x06005343 RID: 21315 RVA: 0x0018EE8B File Offset: 0x0018D08B
		public static QueryBroadcastDrawEvent FromPool(Zone Zone)
		{
			QueryBroadcastDrawEvent queryBroadcastDrawEvent = PooledEvent<QueryBroadcastDrawEvent>.FromPool();
			queryBroadcastDrawEvent.Zone = Zone;
			queryBroadcastDrawEvent.Draw = 0;
			return queryBroadcastDrawEvent;
		}

		// Token: 0x06005344 RID: 21316 RVA: 0x0018EEA0 File Offset: 0x0018D0A0
		public static int GetFor(Zone Z)
		{
			if (Z != null && Z.WantEvent(PooledEvent<QueryBroadcastDrawEvent>.ID, QueryBroadcastDrawEvent.CascadeLevel))
			{
				QueryBroadcastDrawEvent queryBroadcastDrawEvent = QueryBroadcastDrawEvent.FromPool(Z);
				Z.HandleEvent(queryBroadcastDrawEvent);
				return queryBroadcastDrawEvent.Draw;
			}
			return 0;
		}

		// Token: 0x040029F1 RID: 10737
		public new static readonly int CascadeLevel = 15;

		// Token: 0x040029F2 RID: 10738
		public Zone Zone;

		// Token: 0x040029F3 RID: 10739
		public int Draw;
	}
}
