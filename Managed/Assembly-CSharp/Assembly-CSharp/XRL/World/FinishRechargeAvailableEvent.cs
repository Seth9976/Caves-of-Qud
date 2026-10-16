using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000716 RID: 1814
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class FinishRechargeAvailableEvent : IFinalChargeProductionEvent
	{
		// Token: 0x06004B96 RID: 19350 RVA: 0x001774A2 File Offset: 0x001756A2
		public FinishRechargeAvailableEvent()
		{
			this.ID = FinishRechargeAvailableEvent.ID;
		}

		// Token: 0x06004B97 RID: 19351 RVA: 0x001774B5 File Offset: 0x001756B5
		public static int CountPool()
		{
			if (FinishRechargeAvailableEvent.Pool != null)
			{
				return FinishRechargeAvailableEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004B98 RID: 19352 RVA: 0x001774CA File Offset: 0x001756CA
		public static void ResetPool()
		{
			while (FinishRechargeAvailableEvent.PoolCounter > 0)
			{
				FinishRechargeAvailableEvent.Pool[--FinishRechargeAvailableEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004B99 RID: 19353 RVA: 0x001774F2 File Offset: 0x001756F2
		public static void ResetTo(ref FinishRechargeAvailableEvent E)
		{
			MinEvent.ResetTo<FinishRechargeAvailableEvent>(E, FinishRechargeAvailableEvent.Pool, ref FinishRechargeAvailableEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004B9A RID: 19354 RVA: 0x00177508 File Offset: 0x00175708
		public static FinishRechargeAvailableEvent FromPool()
		{
			return MinEvent.FromPool<FinishRechargeAvailableEvent>(ref FinishRechargeAvailableEvent.Pool, ref FinishRechargeAvailableEvent.PoolCounter, 8192);
		}

		// Token: 0x06004B9B RID: 19355 RVA: 0x0017751E File Offset: 0x0017571E
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004B9C RID: 19356 RVA: 0x00177534 File Offset: 0x00175734
		public static FinishRechargeAvailableEvent FromPool(IChargeEvent From)
		{
			FinishRechargeAvailableEvent finishRechargeAvailableEvent = FinishRechargeAvailableEvent.FromPool();
			finishRechargeAvailableEvent.Source = From.Source;
			finishRechargeAvailableEvent.Amount = From.Amount;
			finishRechargeAvailableEvent.StartingAmount = From.StartingAmount;
			finishRechargeAvailableEvent.Multiple = From.Multiple;
			finishRechargeAvailableEvent.GridMask = From.GridMask;
			finishRechargeAvailableEvent.Forced = From.Forced;
			finishRechargeAvailableEvent.LiveOnly = From.LiveOnly;
			finishRechargeAvailableEvent.IncludeTransient = From.IncludeTransient;
			finishRechargeAvailableEvent.IncludeBiological = From.IncludeBiological;
			return finishRechargeAvailableEvent;
		}

		// Token: 0x06004B9D RID: 19357 RVA: 0x001775B4 File Offset: 0x001757B4
		public static int Send(GameObject Object, GameObject Source, int Amount, int Multiple = 1, long GridMask = 0L, bool Forced = false, bool IncludeTransient = false, bool IncludeBiological = true)
		{
			if (FinishRechargeAvailableEvent.Wanted(Object))
			{
				FinishRechargeAvailableEvent finishRechargeAvailableEvent = FinishRechargeAvailableEvent.FromPool();
				finishRechargeAvailableEvent.Source = Source;
				finishRechargeAvailableEvent.Amount = Amount;
				finishRechargeAvailableEvent.StartingAmount = Amount;
				finishRechargeAvailableEvent.Multiple = Multiple;
				finishRechargeAvailableEvent.GridMask = GridMask;
				finishRechargeAvailableEvent.Forced = Forced;
				finishRechargeAvailableEvent.LiveOnly = false;
				finishRechargeAvailableEvent.IncludeTransient = IncludeTransient;
				finishRechargeAvailableEvent.IncludeBiological = IncludeBiological;
				FinishRechargeAvailableEvent.Process(Object, finishRechargeAvailableEvent);
				return finishRechargeAvailableEvent.Used;
			}
			return 0;
		}

		// Token: 0x06004B9E RID: 19358 RVA: 0x00177624 File Offset: 0x00175824
		public static int Send(GameObject Object, IChargeEvent From)
		{
			if (FinishRechargeAvailableEvent.Wanted(Object))
			{
				FinishRechargeAvailableEvent finishRechargeAvailableEvent = FinishRechargeAvailableEvent.FromPool(From);
				FinishRechargeAvailableEvent.Process(Object, finishRechargeAvailableEvent);
				return finishRechargeAvailableEvent.Used;
			}
			return From.Used;
		}

		// Token: 0x06004B9F RID: 19359 RVA: 0x00177655 File Offset: 0x00175855
		public static bool Wanted(GameObject Object)
		{
			return Object.WantEvent(FinishRechargeAvailableEvent.ID, IChargeEvent.CascadeLevel) || Object.HasRegisteredEvent("FinishRechargeAvailable");
		}

		// Token: 0x06004BA0 RID: 19360 RVA: 0x00177676 File Offset: 0x00175876
		public static bool Process(GameObject Object, FinishRechargeAvailableEvent E)
		{
			return E.CheckRegisteredEvent(Object, "FinishRechargeAvailable") && Object.HandleEvent(E);
		}

		// Token: 0x0400247A RID: 9338
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(FinishRechargeAvailableEvent), null, new MinEvent.EventPoolCount(FinishRechargeAvailableEvent.CountPool), new MinEvent.EventPoolReset(FinishRechargeAvailableEvent.ResetPool));

		// Token: 0x0400247B RID: 9339
		private static List<FinishRechargeAvailableEvent> Pool;

		// Token: 0x0400247C RID: 9340
		private static int PoolCounter;
	}
}
