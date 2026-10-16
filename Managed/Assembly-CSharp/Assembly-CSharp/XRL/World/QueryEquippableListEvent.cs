using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000834 RID: 2100
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class QueryEquippableListEvent : IActOnItemEvent
	{
		// Token: 0x0600537A RID: 21370 RVA: 0x0018F876 File Offset: 0x0018DA76
		public QueryEquippableListEvent()
		{
			this.ID = QueryEquippableListEvent.ID;
		}

		// Token: 0x0600537B RID: 21371 RVA: 0x0018F894 File Offset: 0x0018DA94
		public static int CountPool()
		{
			if (QueryEquippableListEvent.Pool != null)
			{
				return QueryEquippableListEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x0600537C RID: 21372 RVA: 0x0018F8A9 File Offset: 0x0018DAA9
		public static void ResetPool()
		{
			while (QueryEquippableListEvent.PoolCounter > 0)
			{
				QueryEquippableListEvent.Pool[--QueryEquippableListEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x0600537D RID: 21373 RVA: 0x0018F8D1 File Offset: 0x0018DAD1
		public static void ResetTo(ref QueryEquippableListEvent E)
		{
			MinEvent.ResetTo<QueryEquippableListEvent>(E, QueryEquippableListEvent.Pool, ref QueryEquippableListEvent.PoolCounter);
			E = null;
		}

		// Token: 0x0600537E RID: 21374 RVA: 0x0018F8E7 File Offset: 0x0018DAE7
		public static QueryEquippableListEvent FromPool()
		{
			return MinEvent.FromPool<QueryEquippableListEvent>(ref QueryEquippableListEvent.Pool, ref QueryEquippableListEvent.PoolCounter, 8192);
		}

		// Token: 0x0600537F RID: 21375 RVA: 0x0018F8FD File Offset: 0x0018DAFD
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06005380 RID: 21376 RVA: 0x0018F911 File Offset: 0x0018DB11
		public override void Reset()
		{
			base.Reset();
			this.List.Clear();
			this.SlotType = null;
			this.RequireDesirable = false;
			this.RequirePossible = false;
		}

		// Token: 0x06005381 RID: 21377 RVA: 0x0018F939 File Offset: 0x0018DB39
		public static QueryEquippableListEvent FromPool(GameObject Actor, GameObject Item, string SlotType, bool RequireDesirable = false, bool RequirePossible = false)
		{
			QueryEquippableListEvent queryEquippableListEvent = QueryEquippableListEvent.FromPool();
			queryEquippableListEvent.Actor = Actor;
			queryEquippableListEvent.Item = Item;
			queryEquippableListEvent.SlotType = SlotType;
			queryEquippableListEvent.RequireDesirable = RequireDesirable;
			queryEquippableListEvent.RequirePossible = RequirePossible;
			return queryEquippableListEvent;
		}

		// Token: 0x04002A03 RID: 10755
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(QueryEquippableListEvent), null, new MinEvent.EventPoolCount(QueryEquippableListEvent.CountPool), new MinEvent.EventPoolReset(QueryEquippableListEvent.ResetPool));

		// Token: 0x04002A04 RID: 10756
		private static List<QueryEquippableListEvent> Pool;

		// Token: 0x04002A05 RID: 10757
		private static int PoolCounter;

		// Token: 0x04002A06 RID: 10758
		public List<GameObject> List = new List<GameObject>();

		// Token: 0x04002A07 RID: 10759
		public string SlotType;

		// Token: 0x04002A08 RID: 10760
		public bool RequireDesirable;

		// Token: 0x04002A09 RID: 10761
		public bool RequirePossible;
	}
}
