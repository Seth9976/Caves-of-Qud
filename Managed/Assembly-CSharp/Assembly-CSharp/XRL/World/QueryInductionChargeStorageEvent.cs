using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000835 RID: 2101
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class QueryInductionChargeStorageEvent : IChargeStorageEvent
	{
		// Token: 0x06005383 RID: 21379 RVA: 0x0018F993 File Offset: 0x0018DB93
		public QueryInductionChargeStorageEvent()
		{
			this.ID = QueryInductionChargeStorageEvent.ID;
		}

		// Token: 0x06005384 RID: 21380 RVA: 0x0018F9A6 File Offset: 0x0018DBA6
		public static int CountPool()
		{
			if (QueryInductionChargeStorageEvent.Pool != null)
			{
				return QueryInductionChargeStorageEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06005385 RID: 21381 RVA: 0x0018F9BB File Offset: 0x0018DBBB
		public static void ResetPool()
		{
			while (QueryInductionChargeStorageEvent.PoolCounter > 0)
			{
				QueryInductionChargeStorageEvent.Pool[--QueryInductionChargeStorageEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06005386 RID: 21382 RVA: 0x0018F9E3 File Offset: 0x0018DBE3
		public static void ResetTo(ref QueryInductionChargeStorageEvent E)
		{
			MinEvent.ResetTo<QueryInductionChargeStorageEvent>(E, QueryInductionChargeStorageEvent.Pool, ref QueryInductionChargeStorageEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06005387 RID: 21383 RVA: 0x0018F9F9 File Offset: 0x0018DBF9
		public static QueryInductionChargeStorageEvent FromPool()
		{
			return MinEvent.FromPool<QueryInductionChargeStorageEvent>(ref QueryInductionChargeStorageEvent.Pool, ref QueryInductionChargeStorageEvent.PoolCounter, 8192);
		}

		// Token: 0x06005388 RID: 21384 RVA: 0x0018FA0F File Offset: 0x0018DC0F
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06005389 RID: 21385 RVA: 0x0018FA24 File Offset: 0x0018DC24
		public static QueryInductionChargeStorageEvent FromPool(IChargeStorageEvent From)
		{
			QueryInductionChargeStorageEvent queryInductionChargeStorageEvent = QueryInductionChargeStorageEvent.FromPool();
			queryInductionChargeStorageEvent.Source = From.Source;
			queryInductionChargeStorageEvent.Amount = From.Amount;
			queryInductionChargeStorageEvent.StartingAmount = From.StartingAmount;
			queryInductionChargeStorageEvent.Multiple = From.Multiple;
			queryInductionChargeStorageEvent.GridMask = From.GridMask;
			queryInductionChargeStorageEvent.Forced = From.Forced;
			queryInductionChargeStorageEvent.LiveOnly = From.LiveOnly;
			queryInductionChargeStorageEvent.IncludeTransient = From.IncludeTransient;
			queryInductionChargeStorageEvent.IncludeBiological = From.IncludeBiological;
			queryInductionChargeStorageEvent.Transient = From.Transient;
			queryInductionChargeStorageEvent.UnlimitedTransient = From.UnlimitedTransient;
			return queryInductionChargeStorageEvent;
		}

		// Token: 0x0600538A RID: 21386 RVA: 0x0018FABC File Offset: 0x0018DCBC
		public static int Retrieve(GameObject Object, GameObject Source, bool IncludeTransient = true, long GridMask = 0L, bool Forced = false, bool LiveOnly = false, bool IncludeBiological = true)
		{
			if (QueryInductionChargeStorageEvent.Wanted(Object))
			{
				QueryInductionChargeStorageEvent queryInductionChargeStorageEvent = QueryInductionChargeStorageEvent.FromPool();
				queryInductionChargeStorageEvent.Source = Source;
				queryInductionChargeStorageEvent.Amount = 0;
				queryInductionChargeStorageEvent.StartingAmount = 0;
				queryInductionChargeStorageEvent.Multiple = 1;
				queryInductionChargeStorageEvent.GridMask = GridMask;
				queryInductionChargeStorageEvent.Forced = Forced;
				queryInductionChargeStorageEvent.LiveOnly = LiveOnly;
				queryInductionChargeStorageEvent.IncludeTransient = IncludeTransient;
				queryInductionChargeStorageEvent.IncludeBiological = IncludeBiological;
				QueryInductionChargeStorageEvent.Process(Object, queryInductionChargeStorageEvent);
				return QueryInductionChargeStorageEvent.Result(queryInductionChargeStorageEvent);
			}
			return 0;
		}

		// Token: 0x0600538B RID: 21387 RVA: 0x0018FB2C File Offset: 0x0018DD2C
		public static int Retrieve(out QueryInductionChargeStorageEvent E, GameObject Object, GameObject Source, bool IncludeTransient = true, long GridMask = 0L, bool Forced = false, bool LiveOnly = false, bool IncludeBiological = true)
		{
			if (QueryInductionChargeStorageEvent.Wanted(Object))
			{
				E = QueryInductionChargeStorageEvent.FromPool();
				E.Source = Source;
				E.Amount = 0;
				E.StartingAmount = 0;
				E.Multiple = 1;
				E.GridMask = GridMask;
				E.Forced = Forced;
				E.LiveOnly = LiveOnly;
				E.IncludeTransient = IncludeTransient;
				E.IncludeBiological = IncludeBiological;
				QueryInductionChargeStorageEvent.Process(Object, E);
				return QueryInductionChargeStorageEvent.Result(E);
			}
			E = null;
			return 0;
		}

		// Token: 0x0600538C RID: 21388 RVA: 0x0018FBAC File Offset: 0x0018DDAC
		public static int Retrieve(GameObject Object, IChargeStorageEvent From)
		{
			if (QueryInductionChargeStorageEvent.Wanted(Object))
			{
				QueryInductionChargeStorageEvent queryInductionChargeStorageEvent = QueryInductionChargeStorageEvent.FromPool(From);
				QueryInductionChargeStorageEvent.Process(Object, queryInductionChargeStorageEvent);
				return QueryInductionChargeStorageEvent.Result(queryInductionChargeStorageEvent);
			}
			return 0;
		}

		// Token: 0x0600538D RID: 21389 RVA: 0x0018FBD8 File Offset: 0x0018DDD8
		public static int Retrieve(out QueryInductionChargeStorageEvent E, GameObject Object, IChargeStorageEvent From)
		{
			if (QueryInductionChargeStorageEvent.Wanted(Object))
			{
				E = QueryInductionChargeStorageEvent.FromPool(From);
				QueryInductionChargeStorageEvent.Process(Object, E);
				return QueryInductionChargeStorageEvent.Result(E);
			}
			E = null;
			return 0;
		}

		// Token: 0x0600538E RID: 21390 RVA: 0x0018FC00 File Offset: 0x0018DE00
		public static void Subprocess(GameObject Object, IChargeStorageEvent From)
		{
			if (QueryInductionChargeStorageEvent.Wanted(Object))
			{
				QueryInductionChargeStorageEvent queryInductionChargeStorageEvent = QueryInductionChargeStorageEvent.FromPool(From);
				QueryInductionChargeStorageEvent.Process(Object, queryInductionChargeStorageEvent);
				From.Amount = queryInductionChargeStorageEvent.Amount;
				From.Transient = queryInductionChargeStorageEvent.Transient;
				From.UnlimitedTransient = queryInductionChargeStorageEvent.UnlimitedTransient;
			}
		}

		// Token: 0x0600538F RID: 21391 RVA: 0x0018FC48 File Offset: 0x0018DE48
		public static void Subprocess(GameObject Object, IChargeStorageEvent From, ref int Available)
		{
			if (Available > 0 && QueryInductionChargeStorageEvent.Wanted(Object))
			{
				QueryInductionChargeStorageEvent queryInductionChargeStorageEvent = QueryInductionChargeStorageEvent.FromPool(From);
				QueryInductionChargeStorageEvent.Process(Object, queryInductionChargeStorageEvent);
				if (From.Amount != queryInductionChargeStorageEvent.Amount)
				{
					int num = queryInductionChargeStorageEvent.Amount - From.Amount;
					int num2 = 1;
					if (num < 0)
					{
						num = -num;
						num2 = -1;
					}
					if (num > Available)
					{
						num = Available;
					}
					From.Amount += num * num2;
					int num3 = queryInductionChargeStorageEvent.Transient - From.Transient;
					int num4 = 1;
					if (num3 < 0)
					{
						num3 = -num3;
						num4 = -1;
					}
					if (num3 > Available)
					{
						num3 = Available;
					}
					From.Transient += num3 * num4;
					Available -= num;
				}
			}
		}

		// Token: 0x06005390 RID: 21392 RVA: 0x0018FCF0 File Offset: 0x0018DEF0
		public static bool Wanted(GameObject Object)
		{
			return Object.WantEvent(QueryInductionChargeStorageEvent.ID, IChargeEvent.CascadeLevel) || Object.HasRegisteredEvent("QueryInductionChargeStorage");
		}

		// Token: 0x06005391 RID: 21393 RVA: 0x0018FD11 File Offset: 0x0018DF11
		public static bool Process(GameObject Object, QueryInductionChargeStorageEvent E)
		{
			return E.CheckRegisteredEvent(Object, "QueryInductionChargeStorage") && Object.HandleEvent(E);
		}

		// Token: 0x06005392 RID: 21394 RVA: 0x0018FD2A File Offset: 0x0018DF2A
		public static int Result(QueryInductionChargeStorageEvent E)
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

		// Token: 0x04002A0A RID: 10762
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(QueryInductionChargeStorageEvent), null, new MinEvent.EventPoolCount(QueryInductionChargeStorageEvent.CountPool), new MinEvent.EventPoolReset(QueryInductionChargeStorageEvent.ResetPool));

		// Token: 0x04002A0B RID: 10763
		private static List<QueryInductionChargeStorageEvent> Pool;

		// Token: 0x04002A0C RID: 10764
		private static int PoolCounter;
	}
}
