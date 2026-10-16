using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200083E RID: 2110
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class RechargeAvailableEvent : IInitialChargeProductionEvent
	{
		// Token: 0x060053D5 RID: 21461 RVA: 0x00190706 File Offset: 0x0018E906
		public RechargeAvailableEvent()
		{
			this.ID = RechargeAvailableEvent.ID;
		}

		// Token: 0x060053D6 RID: 21462 RVA: 0x00190719 File Offset: 0x0018E919
		public static int CountPool()
		{
			if (RechargeAvailableEvent.Pool != null)
			{
				return RechargeAvailableEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060053D7 RID: 21463 RVA: 0x0019072E File Offset: 0x0018E92E
		public static void ResetPool()
		{
			while (RechargeAvailableEvent.PoolCounter > 0)
			{
				RechargeAvailableEvent.Pool[--RechargeAvailableEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060053D8 RID: 21464 RVA: 0x00190756 File Offset: 0x0018E956
		public static void ResetTo(ref RechargeAvailableEvent E)
		{
			MinEvent.ResetTo<RechargeAvailableEvent>(E, RechargeAvailableEvent.Pool, ref RechargeAvailableEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060053D9 RID: 21465 RVA: 0x0019076C File Offset: 0x0018E96C
		public static RechargeAvailableEvent FromPool()
		{
			return MinEvent.FromPool<RechargeAvailableEvent>(ref RechargeAvailableEvent.Pool, ref RechargeAvailableEvent.PoolCounter, 8192);
		}

		// Token: 0x060053DA RID: 21466 RVA: 0x00190782 File Offset: 0x0018E982
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060053DB RID: 21467 RVA: 0x00190798 File Offset: 0x0018E998
		public static RechargeAvailableEvent FromPool(IChargeEvent From)
		{
			RechargeAvailableEvent rechargeAvailableEvent = RechargeAvailableEvent.FromPool();
			rechargeAvailableEvent.Source = From.Source;
			rechargeAvailableEvent.Amount = From.Amount;
			rechargeAvailableEvent.StartingAmount = From.Amount;
			rechargeAvailableEvent.Multiple = From.Multiple;
			rechargeAvailableEvent.GridMask = From.GridMask;
			rechargeAvailableEvent.Forced = From.Forced;
			rechargeAvailableEvent.LiveOnly = From.LiveOnly;
			rechargeAvailableEvent.IncludeTransient = From.IncludeTransient;
			rechargeAvailableEvent.IncludeBiological = From.IncludeBiological;
			return rechargeAvailableEvent;
		}

		// Token: 0x060053DC RID: 21468 RVA: 0x00190818 File Offset: 0x0018EA18
		public static int Send(GameObject Object, GameObject Source, int Amount, int Multiple = 1, long GridMask = 0L, bool Forced = false, bool IncludeTransient = false, bool IncludeBiological = true)
		{
			if (RechargeAvailableEvent.Wanted(Object))
			{
				RechargeAvailableEvent rechargeAvailableEvent = RechargeAvailableEvent.FromPool();
				rechargeAvailableEvent.Source = Source;
				rechargeAvailableEvent.Amount = Amount;
				rechargeAvailableEvent.StartingAmount = Amount;
				rechargeAvailableEvent.Multiple = Multiple;
				rechargeAvailableEvent.GridMask = GridMask;
				rechargeAvailableEvent.Forced = Forced;
				rechargeAvailableEvent.LiveOnly = false;
				rechargeAvailableEvent.IncludeTransient = IncludeTransient;
				rechargeAvailableEvent.IncludeBiological = IncludeBiological;
				RechargeAvailableEvent.Process(Object, rechargeAvailableEvent);
				return rechargeAvailableEvent.Used;
			}
			return 0;
		}

		// Token: 0x060053DD RID: 21469 RVA: 0x00190888 File Offset: 0x0018EA88
		public static int Send(out RechargeAvailableEvent E, GameObject Object, GameObject Source, int Amount, int Multiple = 1, long GridMask = 0L, bool Forced = false, bool IncludeTransient = false, bool IncludeBiological = true)
		{
			if (RechargeAvailableEvent.Wanted(Object))
			{
				E = RechargeAvailableEvent.FromPool();
				E.Source = Source;
				E.Amount = Amount;
				E.StartingAmount = Amount;
				E.Multiple = Multiple;
				E.GridMask = GridMask;
				E.Forced = Forced;
				E.LiveOnly = false;
				E.IncludeTransient = IncludeTransient;
				E.IncludeBiological = IncludeBiological;
				RechargeAvailableEvent.Process(Object, E);
				return E.Used;
			}
			E = null;
			return 0;
		}

		// Token: 0x060053DE RID: 21470 RVA: 0x00190908 File Offset: 0x0018EB08
		public static int Send(GameObject Object, IChargeEvent From)
		{
			if (RechargeAvailableEvent.Wanted(Object))
			{
				RechargeAvailableEvent rechargeAvailableEvent = RechargeAvailableEvent.FromPool(From);
				RechargeAvailableEvent.Process(Object, rechargeAvailableEvent);
				return rechargeAvailableEvent.Used;
			}
			return 0;
		}

		// Token: 0x060053DF RID: 21471 RVA: 0x00190934 File Offset: 0x0018EB34
		public static int Send(GameObject Object, IChargeEvent From, int Amount)
		{
			if (RechargeAvailableEvent.Wanted(Object))
			{
				RechargeAvailableEvent rechargeAvailableEvent = RechargeAvailableEvent.FromPool(From);
				rechargeAvailableEvent.Amount = Amount;
				RechargeAvailableEvent.Process(Object, rechargeAvailableEvent);
				return rechargeAvailableEvent.Used;
			}
			return 0;
		}

		// Token: 0x060053E0 RID: 21472 RVA: 0x00190967 File Offset: 0x0018EB67
		public static bool Wanted(GameObject Object)
		{
			return Object.WantEvent(RechargeAvailableEvent.ID, IChargeEvent.CascadeLevel) || Object.HasRegisteredEvent("RechargeAvailable");
		}

		// Token: 0x060053E1 RID: 21473 RVA: 0x00190988 File Offset: 0x0018EB88
		public static bool Process(GameObject Object, RechargeAvailableEvent E)
		{
			return E.CheckRegisteredEvent(Object, "RechargeAvailable") && Object.HandleEvent(E);
		}

		// Token: 0x04002A27 RID: 10791
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(RechargeAvailableEvent), null, new MinEvent.EventPoolCount(RechargeAvailableEvent.CountPool), new MinEvent.EventPoolReset(RechargeAvailableEvent.ResetPool));

		// Token: 0x04002A28 RID: 10792
		private static List<RechargeAvailableEvent> Pool;

		// Token: 0x04002A29 RID: 10793
		private static int PoolCounter;
	}
}
