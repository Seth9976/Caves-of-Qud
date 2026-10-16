using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000832 RID: 2098
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class QueryChargeStorageEvent : IChargeStorageEvent
	{
		// Token: 0x06005363 RID: 21347 RVA: 0x0018F45C File Offset: 0x0018D65C
		public QueryChargeStorageEvent()
		{
			this.ID = QueryChargeStorageEvent.ID;
		}

		// Token: 0x06005364 RID: 21348 RVA: 0x0018F46F File Offset: 0x0018D66F
		public static int CountPool()
		{
			if (QueryChargeStorageEvent.Pool != null)
			{
				return QueryChargeStorageEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06005365 RID: 21349 RVA: 0x0018F484 File Offset: 0x0018D684
		public static void ResetPool()
		{
			while (QueryChargeStorageEvent.PoolCounter > 0)
			{
				QueryChargeStorageEvent.Pool[--QueryChargeStorageEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06005366 RID: 21350 RVA: 0x0018F4AC File Offset: 0x0018D6AC
		public static void ResetTo(ref QueryChargeStorageEvent E)
		{
			MinEvent.ResetTo<QueryChargeStorageEvent>(E, QueryChargeStorageEvent.Pool, ref QueryChargeStorageEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06005367 RID: 21351 RVA: 0x0018F4C2 File Offset: 0x0018D6C2
		public static QueryChargeStorageEvent FromPool()
		{
			return MinEvent.FromPool<QueryChargeStorageEvent>(ref QueryChargeStorageEvent.Pool, ref QueryChargeStorageEvent.PoolCounter, 8192);
		}

		// Token: 0x06005368 RID: 21352 RVA: 0x0018F4D8 File Offset: 0x0018D6D8
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06005369 RID: 21353 RVA: 0x0018F4EC File Offset: 0x0018D6EC
		public static QueryChargeStorageEvent FromPool(IChargeStorageEvent From)
		{
			QueryChargeStorageEvent queryChargeStorageEvent = QueryChargeStorageEvent.FromPool();
			queryChargeStorageEvent.Source = From.Source;
			queryChargeStorageEvent.Amount = From.Amount;
			queryChargeStorageEvent.StartingAmount = From.StartingAmount;
			queryChargeStorageEvent.Multiple = From.Multiple;
			queryChargeStorageEvent.GridMask = From.GridMask;
			queryChargeStorageEvent.Forced = From.Forced;
			queryChargeStorageEvent.LiveOnly = From.LiveOnly;
			queryChargeStorageEvent.IncludeTransient = From.IncludeTransient;
			queryChargeStorageEvent.IncludeBiological = From.IncludeBiological;
			queryChargeStorageEvent.Transient = From.Transient;
			queryChargeStorageEvent.UnlimitedTransient = From.UnlimitedTransient;
			return queryChargeStorageEvent;
		}

		// Token: 0x0600536A RID: 21354 RVA: 0x0018F584 File Offset: 0x0018D784
		public static int Retrieve(GameObject Object, GameObject Source, bool IncludeTransient = true, long GridMask = 0L, bool Forced = false, bool LiveOnly = false, bool IncludeBiological = true)
		{
			if (QueryChargeStorageEvent.Wanted(Object))
			{
				QueryChargeStorageEvent queryChargeStorageEvent = QueryChargeStorageEvent.FromPool();
				queryChargeStorageEvent.Source = Source;
				queryChargeStorageEvent.Amount = 0;
				queryChargeStorageEvent.StartingAmount = 0;
				queryChargeStorageEvent.Multiple = 1;
				queryChargeStorageEvent.GridMask = GridMask;
				queryChargeStorageEvent.Forced = Forced;
				queryChargeStorageEvent.LiveOnly = LiveOnly;
				queryChargeStorageEvent.IncludeTransient = IncludeTransient;
				queryChargeStorageEvent.IncludeBiological = IncludeBiological;
				QueryChargeStorageEvent.Process(Object, queryChargeStorageEvent);
				return QueryChargeStorageEvent.Result(queryChargeStorageEvent);
			}
			return 0;
		}

		// Token: 0x0600536B RID: 21355 RVA: 0x0018F5F4 File Offset: 0x0018D7F4
		public static int Retrieve(out QueryChargeStorageEvent E, GameObject Object, GameObject Source, bool IncludeTransient = true, long GridMask = 0L, bool Forced = false, bool LiveOnly = false, bool IncludeBiological = true)
		{
			if (QueryChargeStorageEvent.Wanted(Object))
			{
				E = QueryChargeStorageEvent.FromPool();
				E.Source = Source;
				E.Amount = 0;
				E.StartingAmount = 0;
				E.Multiple = 1;
				E.GridMask = GridMask;
				E.Forced = Forced;
				E.LiveOnly = LiveOnly;
				E.IncludeTransient = IncludeTransient;
				E.IncludeBiological = IncludeBiological;
				QueryChargeStorageEvent.Process(Object, E);
				return QueryChargeStorageEvent.Result(E);
			}
			E = null;
			return 0;
		}

		// Token: 0x0600536C RID: 21356 RVA: 0x0018F674 File Offset: 0x0018D874
		public static int Retrieve(GameObject Object, IChargeStorageEvent From)
		{
			if (QueryChargeStorageEvent.Wanted(Object))
			{
				QueryChargeStorageEvent queryChargeStorageEvent = QueryChargeStorageEvent.FromPool(From);
				QueryChargeStorageEvent.Process(Object, queryChargeStorageEvent);
				return QueryChargeStorageEvent.Result(queryChargeStorageEvent);
			}
			return 0;
		}

		// Token: 0x0600536D RID: 21357 RVA: 0x0018F6A0 File Offset: 0x0018D8A0
		public static int Retrieve(out QueryChargeStorageEvent E, GameObject Object, IChargeStorageEvent From)
		{
			if (QueryChargeStorageEvent.Wanted(Object))
			{
				E = QueryChargeStorageEvent.FromPool(From);
				QueryChargeStorageEvent.Process(Object, E);
				return QueryChargeStorageEvent.Result(E);
			}
			E = null;
			return 0;
		}

		// Token: 0x0600536E RID: 21358 RVA: 0x0018F6C7 File Offset: 0x0018D8C7
		public static bool Wanted(GameObject Object)
		{
			return Object.WantEvent(QueryChargeStorageEvent.ID, IChargeEvent.CascadeLevel) || Object.HasRegisteredEvent("QueryChargeStorage");
		}

		// Token: 0x0600536F RID: 21359 RVA: 0x0018F6E8 File Offset: 0x0018D8E8
		public static bool Process(GameObject Object, QueryChargeStorageEvent E)
		{
			return E.CheckRegisteredEvent(Object, "QueryChargeStorage") && Object.HandleEvent(E);
		}

		// Token: 0x06005370 RID: 21360 RVA: 0x0018F701 File Offset: 0x0018D901
		public static int Result(QueryChargeStorageEvent E)
		{
			if (!E.IncludeTransient)
			{
				return E.Amount - E.Transient;
			}
			if (E.UnlimitedTransient)
			{
				return int.MaxValue;
			}
			return E.Amount;
		}

		// Token: 0x040029FA RID: 10746
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(QueryChargeStorageEvent), null, new MinEvent.EventPoolCount(QueryChargeStorageEvent.CountPool), new MinEvent.EventPoolReset(QueryChargeStorageEvent.ResetPool));

		// Token: 0x040029FB RID: 10747
		private static List<QueryChargeStorageEvent> Pool;

		// Token: 0x040029FC RID: 10748
		private static int PoolCounter;
	}
}
