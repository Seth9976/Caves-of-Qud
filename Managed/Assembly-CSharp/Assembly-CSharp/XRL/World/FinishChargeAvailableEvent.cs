using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000715 RID: 1813
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class FinishChargeAvailableEvent : IFinalChargeProductionEvent
	{
		// Token: 0x06004B8A RID: 19338 RVA: 0x00177280 File Offset: 0x00175480
		public FinishChargeAvailableEvent()
		{
			this.ID = FinishChargeAvailableEvent.ID;
		}

		// Token: 0x06004B8B RID: 19339 RVA: 0x00177293 File Offset: 0x00175493
		public static int CountPool()
		{
			if (FinishChargeAvailableEvent.Pool != null)
			{
				return FinishChargeAvailableEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004B8C RID: 19340 RVA: 0x001772A8 File Offset: 0x001754A8
		public static void ResetPool()
		{
			while (FinishChargeAvailableEvent.PoolCounter > 0)
			{
				FinishChargeAvailableEvent.Pool[--FinishChargeAvailableEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004B8D RID: 19341 RVA: 0x001772D0 File Offset: 0x001754D0
		public static void ResetTo(ref FinishChargeAvailableEvent E)
		{
			MinEvent.ResetTo<FinishChargeAvailableEvent>(E, FinishChargeAvailableEvent.Pool, ref FinishChargeAvailableEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004B8E RID: 19342 RVA: 0x001772E6 File Offset: 0x001754E6
		public static FinishChargeAvailableEvent FromPool()
		{
			return MinEvent.FromPool<FinishChargeAvailableEvent>(ref FinishChargeAvailableEvent.Pool, ref FinishChargeAvailableEvent.PoolCounter, 8192);
		}

		// Token: 0x06004B8F RID: 19343 RVA: 0x001772FC File Offset: 0x001754FC
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004B90 RID: 19344 RVA: 0x00177310 File Offset: 0x00175510
		public static FinishChargeAvailableEvent FromPool(IChargeEvent From)
		{
			FinishChargeAvailableEvent finishChargeAvailableEvent = FinishChargeAvailableEvent.FromPool();
			finishChargeAvailableEvent.Source = From.Source;
			finishChargeAvailableEvent.Amount = From.Amount;
			finishChargeAvailableEvent.StartingAmount = From.StartingAmount;
			finishChargeAvailableEvent.Multiple = From.Multiple;
			finishChargeAvailableEvent.GridMask = From.GridMask;
			finishChargeAvailableEvent.Forced = From.Forced;
			finishChargeAvailableEvent.LiveOnly = From.LiveOnly;
			finishChargeAvailableEvent.IncludeTransient = From.IncludeTransient;
			finishChargeAvailableEvent.IncludeBiological = From.IncludeBiological;
			return finishChargeAvailableEvent;
		}

		// Token: 0x06004B91 RID: 19345 RVA: 0x00177390 File Offset: 0x00175590
		public static int Send(GameObject Object, GameObject Source, int Amount, int Multiple = 1, long GridMask = 0L, bool Forced = false, bool IncludeTransient = true, bool IncludeBiological = true)
		{
			if (FinishChargeAvailableEvent.Wanted(Object))
			{
				FinishChargeAvailableEvent finishChargeAvailableEvent = FinishChargeAvailableEvent.FromPool();
				finishChargeAvailableEvent.Source = Source;
				finishChargeAvailableEvent.Amount = Amount;
				finishChargeAvailableEvent.StartingAmount = Amount;
				finishChargeAvailableEvent.Multiple = Multiple;
				finishChargeAvailableEvent.GridMask = GridMask;
				finishChargeAvailableEvent.Forced = Forced;
				finishChargeAvailableEvent.LiveOnly = false;
				finishChargeAvailableEvent.IncludeTransient = IncludeTransient;
				finishChargeAvailableEvent.IncludeBiological = IncludeBiological;
				FinishChargeAvailableEvent.Process(Object, finishChargeAvailableEvent);
				int used = finishChargeAvailableEvent.Used;
				FinishChargeAvailableEvent.ResetTo(ref finishChargeAvailableEvent);
				return used;
			}
			return 0;
		}

		// Token: 0x06004B92 RID: 19346 RVA: 0x00177408 File Offset: 0x00175608
		public static int Send(GameObject Object, IChargeEvent From)
		{
			if (FinishChargeAvailableEvent.Wanted(Object))
			{
				FinishChargeAvailableEvent finishChargeAvailableEvent = FinishChargeAvailableEvent.FromPool(From);
				FinishChargeAvailableEvent.Process(Object, finishChargeAvailableEvent);
				return finishChargeAvailableEvent.Used;
			}
			return From.Used;
		}

		// Token: 0x06004B93 RID: 19347 RVA: 0x00177439 File Offset: 0x00175639
		public static bool Wanted(GameObject Object)
		{
			return Object.WantEvent(FinishChargeAvailableEvent.ID, IChargeEvent.CascadeLevel) || Object.HasRegisteredEvent("FinishChargeAvailable");
		}

		// Token: 0x06004B94 RID: 19348 RVA: 0x0017745A File Offset: 0x0017565A
		public static bool Process(GameObject Object, FinishChargeAvailableEvent E)
		{
			return E.CheckRegisteredEvent(Object, "FinishChargeAvailable") && Object.HandleEvent(E);
		}

		// Token: 0x04002477 RID: 9335
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(FinishChargeAvailableEvent), null, new MinEvent.EventPoolCount(FinishChargeAvailableEvent.CountPool), new MinEvent.EventPoolReset(FinishChargeAvailableEvent.ResetPool));

		// Token: 0x04002478 RID: 9336
		private static List<FinishChargeAvailableEvent> Pool;

		// Token: 0x04002479 RID: 9337
		private static int PoolCounter;
	}
}
