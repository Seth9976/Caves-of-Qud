using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000830 RID: 2096
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class QueryChargeEvent : IChargeConsumptionEvent
	{
		// Token: 0x06005347 RID: 21319 RVA: 0x0018EEEA File Offset: 0x0018D0EA
		public QueryChargeEvent()
		{
			this.ID = QueryChargeEvent.ID;
		}

		// Token: 0x06005348 RID: 21320 RVA: 0x0018EEFD File Offset: 0x0018D0FD
		public static int CountPool()
		{
			if (QueryChargeEvent.Pool != null)
			{
				return QueryChargeEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06005349 RID: 21321 RVA: 0x0018EF12 File Offset: 0x0018D112
		public static void ResetPool()
		{
			while (QueryChargeEvent.PoolCounter > 0)
			{
				QueryChargeEvent.Pool[--QueryChargeEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x0600534A RID: 21322 RVA: 0x0018EF3A File Offset: 0x0018D13A
		public static void ResetTo(ref QueryChargeEvent E)
		{
			MinEvent.ResetTo<QueryChargeEvent>(E, QueryChargeEvent.Pool, ref QueryChargeEvent.PoolCounter);
			E = null;
		}

		// Token: 0x0600534B RID: 21323 RVA: 0x0018EF50 File Offset: 0x0018D150
		public static QueryChargeEvent FromPool()
		{
			return MinEvent.FromPool<QueryChargeEvent>(ref QueryChargeEvent.Pool, ref QueryChargeEvent.PoolCounter, 8192);
		}

		// Token: 0x0600534C RID: 21324 RVA: 0x0018EF66 File Offset: 0x0018D166
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x0600534D RID: 21325 RVA: 0x0018EF7C File Offset: 0x0018D17C
		public static QueryChargeEvent FromPool(IChargeEvent From)
		{
			QueryChargeEvent queryChargeEvent = QueryChargeEvent.FromPool();
			queryChargeEvent.Source = From.Source;
			queryChargeEvent.Amount = From.Amount;
			queryChargeEvent.StartingAmount = From.StartingAmount;
			queryChargeEvent.Multiple = From.Multiple;
			queryChargeEvent.GridMask = From.GridMask;
			queryChargeEvent.Forced = From.Forced;
			queryChargeEvent.LiveOnly = From.LiveOnly;
			queryChargeEvent.IncludeTransient = From.IncludeTransient;
			queryChargeEvent.IncludeBiological = From.IncludeBiological;
			return queryChargeEvent;
		}

		// Token: 0x0600534E RID: 21326 RVA: 0x0018EFFC File Offset: 0x0018D1FC
		public static int Retrieve(GameObject Object, GameObject Source, int Multiple = 1, long GridMask = 0L, bool Forced = false, bool LiveOnly = false, bool IncludeTransient = true, bool IncludeBiological = true)
		{
			if (QueryChargeEvent.Wanted(Object))
			{
				QueryChargeEvent queryChargeEvent = QueryChargeEvent.FromPool();
				queryChargeEvent.Source = Source;
				queryChargeEvent.Amount = 0;
				queryChargeEvent.StartingAmount = 0;
				queryChargeEvent.Multiple = Multiple;
				queryChargeEvent.GridMask = GridMask;
				queryChargeEvent.Forced = Forced;
				queryChargeEvent.LiveOnly = LiveOnly;
				queryChargeEvent.IncludeTransient = IncludeTransient;
				queryChargeEvent.IncludeBiological = IncludeBiological;
				QueryChargeEvent.Process(Object, queryChargeEvent);
				return queryChargeEvent.Amount;
			}
			return 0;
		}

		// Token: 0x0600534F RID: 21327 RVA: 0x0018F06C File Offset: 0x0018D26C
		public static int Retrieve(out QueryChargeEvent E, GameObject Object, GameObject Source, int Multiple = 1, long GridMask = 0L, bool Forced = false, bool LiveOnly = false, bool IncludeTransient = true, bool IncludeBiological = true)
		{
			if (QueryChargeEvent.Wanted(Object))
			{
				E = QueryChargeEvent.FromPool();
				E.Source = Source;
				E.Amount = 0;
				E.StartingAmount = 0;
				E.Multiple = Multiple;
				E.GridMask = GridMask;
				E.Forced = Forced;
				E.LiveOnly = LiveOnly;
				E.IncludeTransient = IncludeTransient;
				E.IncludeBiological = IncludeBiological;
				QueryChargeEvent.Process(Object, E);
				return E.Amount;
			}
			E = null;
			return 0;
		}

		// Token: 0x06005350 RID: 21328 RVA: 0x0018F0EC File Offset: 0x0018D2EC
		public static int Retrieve(GameObject Object, IChargeEvent From)
		{
			if (QueryChargeEvent.Wanted(Object))
			{
				QueryChargeEvent queryChargeEvent = QueryChargeEvent.FromPool(From);
				QueryChargeEvent.Process(Object, queryChargeEvent);
				return queryChargeEvent.Amount;
			}
			return 0;
		}

		// Token: 0x06005351 RID: 21329 RVA: 0x0018F118 File Offset: 0x0018D318
		public static int Retrieve(out QueryChargeEvent E, GameObject Object, IChargeEvent From)
		{
			if (QueryChargeEvent.Wanted(Object))
			{
				E = QueryChargeEvent.FromPool(From);
				QueryChargeEvent.Process(Object, E);
				return E.Amount;
			}
			E = null;
			return 0;
		}

		// Token: 0x06005352 RID: 21330 RVA: 0x0018F13F File Offset: 0x0018D33F
		public static bool Wanted(GameObject Object)
		{
			return Object.WantEvent(QueryChargeEvent.ID, IChargeEvent.CascadeLevel) || Object.HasRegisteredEvent("QueryCharge");
		}

		// Token: 0x06005353 RID: 21331 RVA: 0x0018F160 File Offset: 0x0018D360
		public static bool Process(GameObject Object, QueryChargeEvent E)
		{
			return E.CheckRegisteredEvent(Object, "QueryCharge") && Object.HandleEvent(E);
		}

		// Token: 0x040029F4 RID: 10740
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(QueryChargeEvent), null, new MinEvent.EventPoolCount(QueryChargeEvent.CountPool), new MinEvent.EventPoolReset(QueryChargeEvent.ResetPool));

		// Token: 0x040029F5 RID: 10741
		private static List<QueryChargeEvent> Pool;

		// Token: 0x040029F6 RID: 10742
		private static int PoolCounter;
	}
}
