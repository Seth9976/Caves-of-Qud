using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000831 RID: 2097
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class QueryChargeProductionEvent : IChargeEvent
	{
		// Token: 0x06005355 RID: 21333 RVA: 0x0018F1A8 File Offset: 0x0018D3A8
		public QueryChargeProductionEvent()
		{
			this.ID = QueryChargeProductionEvent.ID;
		}

		// Token: 0x06005356 RID: 21334 RVA: 0x0018F1BB File Offset: 0x0018D3BB
		public static int CountPool()
		{
			if (QueryChargeProductionEvent.Pool != null)
			{
				return QueryChargeProductionEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06005357 RID: 21335 RVA: 0x0018F1D0 File Offset: 0x0018D3D0
		public static void ResetPool()
		{
			while (QueryChargeProductionEvent.PoolCounter > 0)
			{
				QueryChargeProductionEvent.Pool[--QueryChargeProductionEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06005358 RID: 21336 RVA: 0x0018F1F8 File Offset: 0x0018D3F8
		public static void ResetTo(ref QueryChargeProductionEvent E)
		{
			MinEvent.ResetTo<QueryChargeProductionEvent>(E, QueryChargeProductionEvent.Pool, ref QueryChargeProductionEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06005359 RID: 21337 RVA: 0x0018F20E File Offset: 0x0018D40E
		public static QueryChargeProductionEvent FromPool()
		{
			return MinEvent.FromPool<QueryChargeProductionEvent>(ref QueryChargeProductionEvent.Pool, ref QueryChargeProductionEvent.PoolCounter, 8192);
		}

		// Token: 0x0600535A RID: 21338 RVA: 0x0018F224 File Offset: 0x0018D424
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x0600535B RID: 21339 RVA: 0x0018F238 File Offset: 0x0018D438
		public static QueryChargeProductionEvent FromPool(QueryChargeProductionEvent From)
		{
			QueryChargeProductionEvent queryChargeProductionEvent = QueryChargeProductionEvent.FromPool();
			queryChargeProductionEvent.Source = From.Source;
			queryChargeProductionEvent.Amount = From.Amount;
			queryChargeProductionEvent.StartingAmount = From.StartingAmount;
			queryChargeProductionEvent.Multiple = From.Multiple;
			queryChargeProductionEvent.GridMask = From.GridMask;
			queryChargeProductionEvent.Forced = From.Forced;
			queryChargeProductionEvent.LiveOnly = From.LiveOnly;
			queryChargeProductionEvent.IncludeTransient = From.IncludeTransient;
			queryChargeProductionEvent.IncludeBiological = From.IncludeBiological;
			return queryChargeProductionEvent;
		}

		// Token: 0x0600535C RID: 21340 RVA: 0x0018F2B8 File Offset: 0x0018D4B8
		public static int Retrieve(GameObject Object, GameObject Source, long GridMask = 0L, bool Forced = false, bool LiveOnly = false)
		{
			if (QueryChargeProductionEvent.Wanted(Object))
			{
				QueryChargeProductionEvent queryChargeProductionEvent = QueryChargeProductionEvent.FromPool();
				queryChargeProductionEvent.Source = Source;
				queryChargeProductionEvent.Amount = 0;
				queryChargeProductionEvent.StartingAmount = 0;
				queryChargeProductionEvent.Multiple = 1;
				queryChargeProductionEvent.GridMask = GridMask;
				queryChargeProductionEvent.Forced = Forced;
				queryChargeProductionEvent.LiveOnly = LiveOnly;
				queryChargeProductionEvent.IncludeTransient = true;
				queryChargeProductionEvent.IncludeBiological = true;
				QueryChargeProductionEvent.Process(Object, queryChargeProductionEvent);
				return queryChargeProductionEvent.Amount;
			}
			return 0;
		}

		// Token: 0x0600535D RID: 21341 RVA: 0x0018F324 File Offset: 0x0018D524
		public static int Retrieve(out QueryChargeProductionEvent E, GameObject Object, GameObject Source, long GridMask = 0L, bool Forced = false, bool LiveOnly = false)
		{
			if (QueryChargeProductionEvent.Wanted(Object))
			{
				E = QueryChargeProductionEvent.FromPool();
				E.Source = Source;
				E.Amount = 0;
				E.StartingAmount = 0;
				E.Multiple = 1;
				E.GridMask = GridMask;
				E.Forced = Forced;
				E.LiveOnly = LiveOnly;
				E.IncludeTransient = true;
				E.IncludeBiological = true;
				QueryChargeProductionEvent.Process(Object, E);
				return E.Amount;
			}
			E = null;
			return 0;
		}

		// Token: 0x0600535E RID: 21342 RVA: 0x0018F3A0 File Offset: 0x0018D5A0
		public static int Retrieve(GameObject Object, QueryChargeProductionEvent From)
		{
			if (QueryChargeProductionEvent.Wanted(Object))
			{
				QueryChargeProductionEvent queryChargeProductionEvent = QueryChargeProductionEvent.FromPool(From);
				QueryChargeProductionEvent.Process(Object, queryChargeProductionEvent);
				return queryChargeProductionEvent.Amount;
			}
			return 0;
		}

		// Token: 0x0600535F RID: 21343 RVA: 0x0018F3CC File Offset: 0x0018D5CC
		public static int Retrieve(out QueryChargeProductionEvent E, GameObject Object, QueryChargeProductionEvent From)
		{
			if (QueryChargeProductionEvent.Wanted(Object))
			{
				E = QueryChargeProductionEvent.FromPool(From);
				QueryChargeProductionEvent.Process(Object, E);
				return E.Amount;
			}
			E = null;
			return 0;
		}

		// Token: 0x06005360 RID: 21344 RVA: 0x0018F3F3 File Offset: 0x0018D5F3
		public static bool Wanted(GameObject Object)
		{
			return Object.WantEvent(QueryChargeProductionEvent.ID, IChargeEvent.CascadeLevel) || Object.HasRegisteredEvent("QueryChargeProduction");
		}

		// Token: 0x06005361 RID: 21345 RVA: 0x0018F414 File Offset: 0x0018D614
		public static bool Process(GameObject Object, QueryChargeProductionEvent E)
		{
			return E.CheckRegisteredEvent(Object, "QueryChargeProduction") && Object.HandleEvent(E);
		}

		// Token: 0x040029F7 RID: 10743
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(QueryChargeProductionEvent), null, new MinEvent.EventPoolCount(QueryChargeProductionEvent.CountPool), new MinEvent.EventPoolReset(QueryChargeProductionEvent.ResetPool));

		// Token: 0x040029F8 RID: 10744
		private static List<QueryChargeProductionEvent> Pool;

		// Token: 0x040029F9 RID: 10745
		private static int PoolCounter;
	}
}
