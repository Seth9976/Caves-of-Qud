using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000836 RID: 2102
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class QueryRechargeStorageEvent : IChargeStorageEvent
	{
		// Token: 0x06005394 RID: 21396 RVA: 0x0018FD85 File Offset: 0x0018DF85
		public QueryRechargeStorageEvent()
		{
			this.ID = QueryRechargeStorageEvent.ID;
		}

		// Token: 0x06005395 RID: 21397 RVA: 0x0018FD98 File Offset: 0x0018DF98
		public static int CountPool()
		{
			if (QueryRechargeStorageEvent.Pool != null)
			{
				return QueryRechargeStorageEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06005396 RID: 21398 RVA: 0x0018FDAD File Offset: 0x0018DFAD
		public static void ResetPool()
		{
			while (QueryRechargeStorageEvent.PoolCounter > 0)
			{
				QueryRechargeStorageEvent.Pool[--QueryRechargeStorageEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06005397 RID: 21399 RVA: 0x0018FDD5 File Offset: 0x0018DFD5
		public static void ResetTo(ref QueryRechargeStorageEvent E)
		{
			MinEvent.ResetTo<QueryRechargeStorageEvent>(E, QueryRechargeStorageEvent.Pool, ref QueryRechargeStorageEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06005398 RID: 21400 RVA: 0x0018FDEB File Offset: 0x0018DFEB
		public static QueryRechargeStorageEvent FromPool()
		{
			return MinEvent.FromPool<QueryRechargeStorageEvent>(ref QueryRechargeStorageEvent.Pool, ref QueryRechargeStorageEvent.PoolCounter, 8192);
		}

		// Token: 0x06005399 RID: 21401 RVA: 0x0018FE01 File Offset: 0x0018E001
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x0600539A RID: 21402 RVA: 0x0018FE18 File Offset: 0x0018E018
		public static QueryRechargeStorageEvent FromPool(IChargeStorageEvent From)
		{
			QueryRechargeStorageEvent queryRechargeStorageEvent = QueryRechargeStorageEvent.FromPool();
			queryRechargeStorageEvent.Source = From.Source;
			queryRechargeStorageEvent.Amount = From.Amount;
			queryRechargeStorageEvent.StartingAmount = From.StartingAmount;
			queryRechargeStorageEvent.Multiple = From.Multiple;
			queryRechargeStorageEvent.GridMask = From.GridMask;
			queryRechargeStorageEvent.Forced = From.Forced;
			queryRechargeStorageEvent.LiveOnly = From.LiveOnly;
			queryRechargeStorageEvent.IncludeTransient = From.IncludeTransient;
			queryRechargeStorageEvent.IncludeBiological = From.IncludeBiological;
			queryRechargeStorageEvent.Transient = From.Transient;
			queryRechargeStorageEvent.UnlimitedTransient = From.UnlimitedTransient;
			return queryRechargeStorageEvent;
		}

		// Token: 0x0600539B RID: 21403 RVA: 0x0018FEB0 File Offset: 0x0018E0B0
		public static int Retrieve(GameObject Object, GameObject Source, bool IncludeTransient = false, long GridMask = 0L, bool Forced = false, bool LiveOnly = false, bool IncludeBiological = true)
		{
			if (QueryRechargeStorageEvent.Wanted(Object))
			{
				QueryRechargeStorageEvent queryRechargeStorageEvent = QueryRechargeStorageEvent.FromPool();
				queryRechargeStorageEvent.Source = Source;
				queryRechargeStorageEvent.Amount = 0;
				queryRechargeStorageEvent.StartingAmount = 0;
				queryRechargeStorageEvent.Multiple = 1;
				queryRechargeStorageEvent.GridMask = GridMask;
				queryRechargeStorageEvent.Forced = Forced;
				queryRechargeStorageEvent.LiveOnly = LiveOnly;
				queryRechargeStorageEvent.IncludeTransient = IncludeTransient;
				queryRechargeStorageEvent.IncludeBiological = IncludeBiological;
				QueryRechargeStorageEvent.Process(Object, queryRechargeStorageEvent);
				return QueryRechargeStorageEvent.Result(queryRechargeStorageEvent);
			}
			return 0;
		}

		// Token: 0x0600539C RID: 21404 RVA: 0x0018FF20 File Offset: 0x0018E120
		public static int Retrieve(out QueryRechargeStorageEvent E, GameObject Object, GameObject Source, bool IncludeTransient = false, long GridMask = 0L, bool Forced = false, bool LiveOnly = false, bool IncludeBiological = true)
		{
			if (QueryRechargeStorageEvent.Wanted(Object))
			{
				E = QueryRechargeStorageEvent.FromPool();
				E.Source = Source;
				E.Amount = 0;
				E.StartingAmount = 0;
				E.Multiple = 1;
				E.GridMask = GridMask;
				E.Forced = Forced;
				E.LiveOnly = LiveOnly;
				E.IncludeTransient = IncludeTransient;
				E.IncludeBiological = IncludeBiological;
				QueryRechargeStorageEvent.Process(Object, E);
				return QueryRechargeStorageEvent.Result(E);
			}
			E = null;
			return 0;
		}

		// Token: 0x0600539D RID: 21405 RVA: 0x0018FFA0 File Offset: 0x0018E1A0
		public static int Retrieve(GameObject Object, IChargeStorageEvent From)
		{
			if (QueryRechargeStorageEvent.Wanted(Object))
			{
				QueryRechargeStorageEvent queryRechargeStorageEvent = QueryRechargeStorageEvent.FromPool(From);
				QueryRechargeStorageEvent.Process(Object, queryRechargeStorageEvent);
				return QueryRechargeStorageEvent.Result(queryRechargeStorageEvent);
			}
			return 0;
		}

		// Token: 0x0600539E RID: 21406 RVA: 0x0018FFCC File Offset: 0x0018E1CC
		public static int Retrieve(out QueryRechargeStorageEvent E, GameObject Object, IChargeStorageEvent From)
		{
			if (QueryRechargeStorageEvent.Wanted(Object))
			{
				E = QueryRechargeStorageEvent.FromPool(From);
				QueryRechargeStorageEvent.Process(Object, E);
				return QueryRechargeStorageEvent.Result(E);
			}
			E = null;
			return 0;
		}

		// Token: 0x0600539F RID: 21407 RVA: 0x0018FFF3 File Offset: 0x0018E1F3
		public static bool Wanted(GameObject Object)
		{
			return Object.WantEvent(QueryRechargeStorageEvent.ID, IChargeEvent.CascadeLevel) || Object.HasRegisteredEvent("QueryRechargeStorage");
		}

		// Token: 0x060053A0 RID: 21408 RVA: 0x00190014 File Offset: 0x0018E214
		public static bool Process(GameObject Object, QueryRechargeStorageEvent E)
		{
			return E.CheckRegisteredEvent(Object, "QueryRechargeStorage") && Object.HandleEvent(E);
		}

		// Token: 0x060053A1 RID: 21409 RVA: 0x0019002D File Offset: 0x0018E22D
		public static int Result(QueryRechargeStorageEvent E)
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

		// Token: 0x04002A0D RID: 10765
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(QueryRechargeStorageEvent), null, new MinEvent.EventPoolCount(QueryRechargeStorageEvent.CountPool), new MinEvent.EventPoolReset(QueryRechargeStorageEvent.ResetPool));

		// Token: 0x04002A0E RID: 10766
		private static List<QueryRechargeStorageEvent> Pool;

		// Token: 0x04002A0F RID: 10767
		private static int PoolCounter;
	}
}
