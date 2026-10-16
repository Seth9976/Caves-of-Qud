using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000664 RID: 1636
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AfterMentalDefendEvent : IMentalAttackEvent
	{
		// Token: 0x0600469A RID: 18074 RVA: 0x001673E3 File Offset: 0x001655E3
		public AfterMentalDefendEvent()
		{
			this.ID = AfterMentalDefendEvent.ID;
		}

		// Token: 0x0600469B RID: 18075 RVA: 0x001673F6 File Offset: 0x001655F6
		public static int CountPool()
		{
			if (AfterMentalDefendEvent.Pool != null)
			{
				return AfterMentalDefendEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x0600469C RID: 18076 RVA: 0x0016740B File Offset: 0x0016560B
		public static void ResetPool()
		{
			while (AfterMentalDefendEvent.PoolCounter > 0)
			{
				AfterMentalDefendEvent.Pool[--AfterMentalDefendEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x0600469D RID: 18077 RVA: 0x00167433 File Offset: 0x00165633
		public static void ResetTo(ref AfterMentalDefendEvent E)
		{
			MinEvent.ResetTo<AfterMentalDefendEvent>(E, AfterMentalDefendEvent.Pool, ref AfterMentalDefendEvent.PoolCounter);
			E = null;
		}

		// Token: 0x0600469E RID: 18078 RVA: 0x00167449 File Offset: 0x00165649
		public static AfterMentalDefendEvent FromPool()
		{
			return MinEvent.FromPool<AfterMentalDefendEvent>(ref AfterMentalDefendEvent.Pool, ref AfterMentalDefendEvent.PoolCounter, 8192);
		}

		// Token: 0x0600469F RID: 18079 RVA: 0x0016745F File Offset: 0x0016565F
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060046A0 RID: 18080 RVA: 0x00167474 File Offset: 0x00165674
		public static AfterMentalDefendEvent FromPool(IMentalAttackEvent PE)
		{
			AfterMentalDefendEvent afterMentalDefendEvent = AfterMentalDefendEvent.FromPool();
			PE.ApplyTo(afterMentalDefendEvent);
			return afterMentalDefendEvent;
		}

		// Token: 0x060046A1 RID: 18081 RVA: 0x00167490 File Offset: 0x00165690
		public static void Send(IMentalAttackEvent PE)
		{
			bool flag = true;
			if (PE.Defender.WantEvent(AfterMentalDefendEvent.ID, IMentalAttackEvent.CascadeLevel))
			{
				AfterMentalDefendEvent afterMentalDefendEvent = AfterMentalDefendEvent.FromPool(PE);
				flag = PE.Defender.HandleEvent<AfterMentalDefendEvent>(afterMentalDefendEvent, PE);
				PE.SetFrom(afterMentalDefendEvent);
			}
			if (flag && PE.Defender.HasRegisteredEvent("AfterMentalDefend"))
			{
				Event @event = Event.New("AfterMentalDefend", 0, 0, 0);
				PE.ApplyTo(@event);
				flag = PE.Defender.FireEvent(@event);
				PE.SetFrom(@event);
			}
		}

		// Token: 0x04002195 RID: 8597
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AfterMentalDefendEvent), null, new MinEvent.EventPoolCount(AfterMentalDefendEvent.CountPool), new MinEvent.EventPoolReset(AfterMentalDefendEvent.ResetPool));

		// Token: 0x04002196 RID: 8598
		private static List<AfterMentalDefendEvent> Pool;

		// Token: 0x04002197 RID: 8599
		private static int PoolCounter;
	}
}
