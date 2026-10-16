using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020006D4 RID: 1748
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class ChargeAvailableEvent : IInitialChargeProductionEvent
	{
		// Token: 0x060049C4 RID: 18884 RVA: 0x00172885 File Offset: 0x00170A85
		public ChargeAvailableEvent()
		{
			this.ID = ChargeAvailableEvent.ID;
		}

		// Token: 0x060049C5 RID: 18885 RVA: 0x00172898 File Offset: 0x00170A98
		public static int CountPool()
		{
			if (ChargeAvailableEvent.Pool != null)
			{
				return ChargeAvailableEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060049C6 RID: 18886 RVA: 0x001728AD File Offset: 0x00170AAD
		public static void ResetPool()
		{
			while (ChargeAvailableEvent.PoolCounter > 0)
			{
				ChargeAvailableEvent.Pool[--ChargeAvailableEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060049C7 RID: 18887 RVA: 0x001728D5 File Offset: 0x00170AD5
		public static void ResetTo(ref ChargeAvailableEvent E)
		{
			MinEvent.ResetTo<ChargeAvailableEvent>(E, ChargeAvailableEvent.Pool, ref ChargeAvailableEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060049C8 RID: 18888 RVA: 0x001728EB File Offset: 0x00170AEB
		public static ChargeAvailableEvent FromPool()
		{
			return MinEvent.FromPool<ChargeAvailableEvent>(ref ChargeAvailableEvent.Pool, ref ChargeAvailableEvent.PoolCounter, 8192);
		}

		// Token: 0x060049C9 RID: 18889 RVA: 0x00172901 File Offset: 0x00170B01
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060049CA RID: 18890 RVA: 0x00172918 File Offset: 0x00170B18
		public static int Send(GameObject Object, GameObject Source, int Amount, int Multiple = 1, long GridMask = 0L, bool Forced = false)
		{
			if (ChargeAvailableEvent.Wanted(Object))
			{
				ChargeAvailableEvent chargeAvailableEvent = ChargeAvailableEvent.FromPool();
				chargeAvailableEvent.Source = Source;
				chargeAvailableEvent.Amount = Amount;
				chargeAvailableEvent.StartingAmount = Amount;
				chargeAvailableEvent.Multiple = Multiple;
				chargeAvailableEvent.GridMask = GridMask;
				chargeAvailableEvent.Forced = Forced;
				chargeAvailableEvent.LiveOnly = false;
				ChargeAvailableEvent.Process(Object, chargeAvailableEvent);
				return chargeAvailableEvent.Used;
			}
			return 0;
		}

		// Token: 0x060049CB RID: 18891 RVA: 0x00172978 File Offset: 0x00170B78
		public static int Send(out ChargeAvailableEvent E, GameObject Object, GameObject Source, int Amount, int Multiple = 1, long GridMask = 0L, bool Forced = false)
		{
			if (ChargeAvailableEvent.Wanted(Object))
			{
				E = ChargeAvailableEvent.FromPool();
				E.Source = Source;
				E.Amount = Amount;
				E.StartingAmount = Amount;
				E.Multiple = Multiple;
				E.GridMask = GridMask;
				E.Forced = Forced;
				E.LiveOnly = false;
				ChargeAvailableEvent.Process(Object, E);
				int used = E.Used;
				ChargeAvailableEvent.ResetTo(ref E);
				return used;
			}
			E = null;
			return 0;
		}

		// Token: 0x060049CC RID: 18892 RVA: 0x001729EA File Offset: 0x00170BEA
		public static bool Wanted(GameObject Object)
		{
			return Object.WantEvent(ChargeAvailableEvent.ID, IChargeEvent.CascadeLevel) || Object.HasRegisteredEvent("ChargeAvailable");
		}

		// Token: 0x060049CD RID: 18893 RVA: 0x00172A0B File Offset: 0x00170C0B
		public static bool Process(GameObject Object, ChargeAvailableEvent E)
		{
			return E.CheckRegisteredEvent(Object, "ChargeAvailable") && Object.HandleEvent(E);
		}

		// Token: 0x04002374 RID: 9076
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(ChargeAvailableEvent), null, new MinEvent.EventPoolCount(ChargeAvailableEvent.CountPool), new MinEvent.EventPoolReset(ChargeAvailableEvent.ResetPool));

		// Token: 0x04002375 RID: 9077
		private static List<ChargeAvailableEvent> Pool;

		// Token: 0x04002376 RID: 9078
		private static int PoolCounter;
	}
}
