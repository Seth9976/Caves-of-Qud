using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020006AA RID: 1706
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class BeginMentalAttackEvent : IMentalAttackEvent
	{
		// Token: 0x0600489C RID: 18588 RVA: 0x0016CE63 File Offset: 0x0016B063
		public BeginMentalAttackEvent()
		{
			this.ID = BeginMentalAttackEvent.ID;
		}

		// Token: 0x0600489D RID: 18589 RVA: 0x0016CE76 File Offset: 0x0016B076
		public static int CountPool()
		{
			if (BeginMentalAttackEvent.Pool != null)
			{
				return BeginMentalAttackEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x0600489E RID: 18590 RVA: 0x0016CE8B File Offset: 0x0016B08B
		public static void ResetPool()
		{
			while (BeginMentalAttackEvent.PoolCounter > 0)
			{
				BeginMentalAttackEvent.Pool[--BeginMentalAttackEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x0600489F RID: 18591 RVA: 0x0016CEB3 File Offset: 0x0016B0B3
		public static void ResetTo(ref BeginMentalAttackEvent E)
		{
			MinEvent.ResetTo<BeginMentalAttackEvent>(E, BeginMentalAttackEvent.Pool, ref BeginMentalAttackEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060048A0 RID: 18592 RVA: 0x0016CEC9 File Offset: 0x0016B0C9
		public static BeginMentalAttackEvent FromPool()
		{
			return MinEvent.FromPool<BeginMentalAttackEvent>(ref BeginMentalAttackEvent.Pool, ref BeginMentalAttackEvent.PoolCounter, 8192);
		}

		// Token: 0x060048A1 RID: 18593 RVA: 0x0016CEDF File Offset: 0x0016B0DF
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060048A2 RID: 18594 RVA: 0x0016CEF4 File Offset: 0x0016B0F4
		public static BeginMentalAttackEvent FromPool(IMentalAttackEvent PE)
		{
			BeginMentalAttackEvent beginMentalAttackEvent = BeginMentalAttackEvent.FromPool();
			PE.ApplyTo(beginMentalAttackEvent);
			return beginMentalAttackEvent;
		}

		// Token: 0x060048A3 RID: 18595 RVA: 0x0016CF10 File Offset: 0x0016B110
		public static bool Check(IMentalAttackEvent PE)
		{
			bool flag = true;
			if (PE.Attacker.WantEvent(BeginMentalAttackEvent.ID, IMentalAttackEvent.CascadeLevel))
			{
				BeginMentalAttackEvent beginMentalAttackEvent = BeginMentalAttackEvent.FromPool(PE);
				flag = PE.Attacker.HandleEvent<BeginMentalAttackEvent>(beginMentalAttackEvent, PE);
				PE.SetFrom(beginMentalAttackEvent);
			}
			if (flag && PE.Attacker.HasRegisteredEvent("BeginMentalAttack"))
			{
				Event @event = Event.New("BeginMentalAttack", 0, 0, 0);
				PE.ApplyTo(@event);
				flag = PE.Attacker.FireEvent(@event);
				PE.SetFrom(@event);
			}
			return flag;
		}

		// Token: 0x040022CD RID: 8909
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(BeginMentalAttackEvent), null, new MinEvent.EventPoolCount(BeginMentalAttackEvent.CountPool), new MinEvent.EventPoolReset(BeginMentalAttackEvent.ResetPool));

		// Token: 0x040022CE RID: 8910
		private static List<BeginMentalAttackEvent> Pool;

		// Token: 0x040022CF RID: 8911
		private static int PoolCounter;
	}
}
