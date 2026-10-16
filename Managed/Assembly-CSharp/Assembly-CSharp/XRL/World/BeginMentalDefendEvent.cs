using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020006AB RID: 1707
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class BeginMentalDefendEvent : IMentalAttackEvent
	{
		// Token: 0x060048A5 RID: 18597 RVA: 0x0016CFC0 File Offset: 0x0016B1C0
		public BeginMentalDefendEvent()
		{
			this.ID = BeginMentalDefendEvent.ID;
		}

		// Token: 0x060048A6 RID: 18598 RVA: 0x0016CFD3 File Offset: 0x0016B1D3
		public static int CountPool()
		{
			if (BeginMentalDefendEvent.Pool != null)
			{
				return BeginMentalDefendEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060048A7 RID: 18599 RVA: 0x0016CFE8 File Offset: 0x0016B1E8
		public static void ResetPool()
		{
			while (BeginMentalDefendEvent.PoolCounter > 0)
			{
				BeginMentalDefendEvent.Pool[--BeginMentalDefendEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060048A8 RID: 18600 RVA: 0x0016D010 File Offset: 0x0016B210
		public static void ResetTo(ref BeginMentalDefendEvent E)
		{
			MinEvent.ResetTo<BeginMentalDefendEvent>(E, BeginMentalDefendEvent.Pool, ref BeginMentalDefendEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060048A9 RID: 18601 RVA: 0x0016D026 File Offset: 0x0016B226
		public static BeginMentalDefendEvent FromPool()
		{
			return MinEvent.FromPool<BeginMentalDefendEvent>(ref BeginMentalDefendEvent.Pool, ref BeginMentalDefendEvent.PoolCounter, 8192);
		}

		// Token: 0x060048AA RID: 18602 RVA: 0x0016D03C File Offset: 0x0016B23C
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060048AB RID: 18603 RVA: 0x0016D050 File Offset: 0x0016B250
		public static BeginMentalDefendEvent FromPool(IMentalAttackEvent PE)
		{
			BeginMentalDefendEvent beginMentalDefendEvent = BeginMentalDefendEvent.FromPool();
			PE.ApplyTo(beginMentalDefendEvent);
			return beginMentalDefendEvent;
		}

		// Token: 0x060048AC RID: 18604 RVA: 0x0016D06C File Offset: 0x0016B26C
		public static bool Check(IMentalAttackEvent PE)
		{
			bool flag = true;
			if (PE.Defender.WantEvent(BeginMentalDefendEvent.ID, IMentalAttackEvent.CascadeLevel))
			{
				BeginMentalDefendEvent beginMentalDefendEvent = BeginMentalDefendEvent.FromPool(PE);
				flag = PE.Defender.HandleEvent<BeginMentalDefendEvent>(beginMentalDefendEvent, PE);
				PE.SetFrom(beginMentalDefendEvent);
			}
			if (flag && PE.Defender.HasRegisteredEvent("BeginMentalDefend"))
			{
				Event @event = Event.New("BeginMentalDefend", 0, 0, 0);
				PE.ApplyTo(@event);
				flag = PE.Defender.FireEvent(@event);
				PE.SetFrom(@event);
			}
			return flag;
		}

		// Token: 0x040022D0 RID: 8912
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(BeginMentalDefendEvent), null, new MinEvent.EventPoolCount(BeginMentalDefendEvent.CountPool), new MinEvent.EventPoolReset(BeginMentalDefendEvent.ResetPool));

		// Token: 0x040022D1 RID: 8913
		private static List<BeginMentalDefendEvent> Pool;

		// Token: 0x040022D2 RID: 8914
		private static int PoolCounter;
	}
}
