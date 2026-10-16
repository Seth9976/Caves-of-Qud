using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000872 RID: 2162
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class UseChargeEvent : IChargeConsumptionEvent
	{
		// Token: 0x060055E5 RID: 21989 RVA: 0x00199C17 File Offset: 0x00197E17
		public UseChargeEvent()
		{
			this.ID = UseChargeEvent.ID;
		}

		// Token: 0x060055E6 RID: 21990 RVA: 0x00199C2A File Offset: 0x00197E2A
		public static int CountPool()
		{
			if (UseChargeEvent.Pool != null)
			{
				return UseChargeEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060055E7 RID: 21991 RVA: 0x00199C3F File Offset: 0x00197E3F
		public static void ResetPool()
		{
			while (UseChargeEvent.PoolCounter > 0)
			{
				UseChargeEvent.Pool[--UseChargeEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060055E8 RID: 21992 RVA: 0x00199C67 File Offset: 0x00197E67
		public static void ResetTo(ref UseChargeEvent E)
		{
			MinEvent.ResetTo<UseChargeEvent>(E, UseChargeEvent.Pool, ref UseChargeEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060055E9 RID: 21993 RVA: 0x00199C7D File Offset: 0x00197E7D
		public static UseChargeEvent FromPool()
		{
			return MinEvent.FromPool<UseChargeEvent>(ref UseChargeEvent.Pool, ref UseChargeEvent.PoolCounter, 8192);
		}

		// Token: 0x060055EA RID: 21994 RVA: 0x00199C93 File Offset: 0x00197E93
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060055EB RID: 21995 RVA: 0x00199CA8 File Offset: 0x00197EA8
		public static bool Check(GameObject Object, GameObject Source, int Amount, int Multiple = 1, long GridMask = 0L, bool Forced = false, bool LiveOnly = false, bool IncludeTransient = true, bool IncludeBiological = true, int PowerLoadLevel = 100)
		{
			if (UseChargeEvent.Wanted(Object))
			{
				UseChargeEvent useChargeEvent = UseChargeEvent.FromPool();
				useChargeEvent.Source = Source;
				useChargeEvent.Amount = Amount;
				useChargeEvent.Multiple = Multiple;
				useChargeEvent.GridMask = GridMask;
				useChargeEvent.Forced = Forced;
				useChargeEvent.LiveOnly = LiveOnly;
				useChargeEvent.IncludeTransient = IncludeTransient;
				useChargeEvent.IncludeBiological = IncludeBiological;
				useChargeEvent.PowerLoadLevel = new int?(PowerLoadLevel);
				if (!UseChargeEvent.Process(Object, useChargeEvent))
				{
					ChargeUsedEvent.Send(Object, useChargeEvent.Source, Amount - useChargeEvent.Amount, Amount, useChargeEvent.Multiple, useChargeEvent.GridMask, useChargeEvent.Forced, useChargeEvent.LiveOnly, useChargeEvent.IncludeTransient, useChargeEvent.IncludeBiological, PowerLoadLevel);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060055EC RID: 21996 RVA: 0x00199D5A File Offset: 0x00197F5A
		public static bool Wanted(GameObject Object)
		{
			return Object.WantEvent(UseChargeEvent.ID, IChargeEvent.CascadeLevel) || Object.HasRegisteredEvent("UseCharge");
		}

		// Token: 0x060055ED RID: 21997 RVA: 0x00199D7C File Offset: 0x00197F7C
		public static bool Process(GameObject Object, UseChargeEvent E)
		{
			if (!E.CheckRegisteredEvent(Object, "UseCharge"))
			{
				return false;
			}
			for (int i = 1; i <= 2; i++)
			{
				E.Pass = i;
				if (!Object.HandleEvent(E))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04002B45 RID: 11077
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(UseChargeEvent), null, new MinEvent.EventPoolCount(UseChargeEvent.CountPool), new MinEvent.EventPoolReset(UseChargeEvent.ResetPool));

		// Token: 0x04002B46 RID: 11078
		private static List<UseChargeEvent> Pool;

		// Token: 0x04002B47 RID: 11079
		private static int PoolCounter;

		// Token: 0x04002B48 RID: 11080
		public const int PASSES = 2;
	}
}
