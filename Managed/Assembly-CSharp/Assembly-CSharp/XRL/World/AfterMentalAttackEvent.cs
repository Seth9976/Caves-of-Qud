using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000663 RID: 1635
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AfterMentalAttackEvent : IMentalAttackEvent
	{
		// Token: 0x06004691 RID: 18065 RVA: 0x00167286 File Offset: 0x00165486
		public AfterMentalAttackEvent()
		{
			this.ID = AfterMentalAttackEvent.ID;
		}

		// Token: 0x06004692 RID: 18066 RVA: 0x00167299 File Offset: 0x00165499
		public static int CountPool()
		{
			if (AfterMentalAttackEvent.Pool != null)
			{
				return AfterMentalAttackEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004693 RID: 18067 RVA: 0x001672AE File Offset: 0x001654AE
		public static void ResetPool()
		{
			while (AfterMentalAttackEvent.PoolCounter > 0)
			{
				AfterMentalAttackEvent.Pool[--AfterMentalAttackEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004694 RID: 18068 RVA: 0x001672D6 File Offset: 0x001654D6
		public static void ResetTo(ref AfterMentalAttackEvent E)
		{
			MinEvent.ResetTo<AfterMentalAttackEvent>(E, AfterMentalAttackEvent.Pool, ref AfterMentalAttackEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004695 RID: 18069 RVA: 0x001672EC File Offset: 0x001654EC
		public static AfterMentalAttackEvent FromPool()
		{
			return MinEvent.FromPool<AfterMentalAttackEvent>(ref AfterMentalAttackEvent.Pool, ref AfterMentalAttackEvent.PoolCounter, 8192);
		}

		// Token: 0x06004696 RID: 18070 RVA: 0x00167302 File Offset: 0x00165502
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004697 RID: 18071 RVA: 0x00167318 File Offset: 0x00165518
		public static AfterMentalAttackEvent FromPool(IMentalAttackEvent PE)
		{
			AfterMentalAttackEvent afterMentalAttackEvent = AfterMentalAttackEvent.FromPool();
			PE.ApplyTo(afterMentalAttackEvent);
			return afterMentalAttackEvent;
		}

		// Token: 0x06004698 RID: 18072 RVA: 0x00167334 File Offset: 0x00165534
		public static void Send(IMentalAttackEvent PE)
		{
			bool flag = true;
			if (PE.Attacker.WantEvent(AfterMentalAttackEvent.ID, IMentalAttackEvent.CascadeLevel))
			{
				AfterMentalAttackEvent afterMentalAttackEvent = AfterMentalAttackEvent.FromPool(PE);
				flag = PE.Attacker.HandleEvent<AfterMentalAttackEvent>(afterMentalAttackEvent, PE);
				PE.SetFrom(afterMentalAttackEvent);
			}
			if (flag && PE.Attacker.HasRegisteredEvent("AfterMentalAttack"))
			{
				Event @event = Event.New("AfterMentalAttack", 0, 0, 0);
				PE.ApplyTo(@event);
				flag = PE.Attacker.FireEvent(@event);
				PE.SetFrom(@event);
			}
		}

		// Token: 0x04002192 RID: 8594
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AfterMentalAttackEvent), null, new MinEvent.EventPoolCount(AfterMentalAttackEvent.CountPool), new MinEvent.EventPoolReset(AfterMentalAttackEvent.ResetPool));

		// Token: 0x04002193 RID: 8595
		private static List<AfterMentalAttackEvent> Pool;

		// Token: 0x04002194 RID: 8596
		private static int PoolCounter;
	}
}
