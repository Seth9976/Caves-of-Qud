using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000696 RID: 1686
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class BeforeMentalAttackEvent : IMentalAttackEvent
	{
		// Token: 0x06004807 RID: 18439 RVA: 0x0016B2D1 File Offset: 0x001694D1
		public BeforeMentalAttackEvent()
		{
			this.ID = BeforeMentalAttackEvent.ID;
		}

		// Token: 0x06004808 RID: 18440 RVA: 0x0016B2E4 File Offset: 0x001694E4
		public static int CountPool()
		{
			if (BeforeMentalAttackEvent.Pool != null)
			{
				return BeforeMentalAttackEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004809 RID: 18441 RVA: 0x0016B2F9 File Offset: 0x001694F9
		public static void ResetPool()
		{
			while (BeforeMentalAttackEvent.PoolCounter > 0)
			{
				BeforeMentalAttackEvent.Pool[--BeforeMentalAttackEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x0600480A RID: 18442 RVA: 0x0016B321 File Offset: 0x00169521
		public static void ResetTo(ref BeforeMentalAttackEvent E)
		{
			MinEvent.ResetTo<BeforeMentalAttackEvent>(E, BeforeMentalAttackEvent.Pool, ref BeforeMentalAttackEvent.PoolCounter);
			E = null;
		}

		// Token: 0x0600480B RID: 18443 RVA: 0x0016B337 File Offset: 0x00169537
		public static BeforeMentalAttackEvent FromPool()
		{
			return MinEvent.FromPool<BeforeMentalAttackEvent>(ref BeforeMentalAttackEvent.Pool, ref BeforeMentalAttackEvent.PoolCounter, 8192);
		}

		// Token: 0x0600480C RID: 18444 RVA: 0x0016B34D File Offset: 0x0016954D
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x0600480D RID: 18445 RVA: 0x0016B364 File Offset: 0x00169564
		public static BeforeMentalAttackEvent FromPool(IMentalAttackEvent PE)
		{
			BeforeMentalAttackEvent beforeMentalAttackEvent = BeforeMentalAttackEvent.FromPool();
			PE.ApplyTo(beforeMentalAttackEvent);
			return beforeMentalAttackEvent;
		}

		// Token: 0x0600480E RID: 18446 RVA: 0x0016B380 File Offset: 0x00169580
		public static bool Check(IMentalAttackEvent PE)
		{
			bool flag = true;
			if (PE.Attacker.WantEvent(BeforeMentalAttackEvent.ID, IMentalAttackEvent.CascadeLevel))
			{
				BeforeMentalAttackEvent beforeMentalAttackEvent = BeforeMentalAttackEvent.FromPool(PE);
				flag = PE.Attacker.HandleEvent<BeforeMentalAttackEvent>(beforeMentalAttackEvent, PE);
				PE.SetFrom(beforeMentalAttackEvent);
			}
			if (flag && PE.Attacker.HasRegisteredEvent("BeforeMentalAttack"))
			{
				Event @event = Event.New("BeforeMentalAttack", 0, 0, 0);
				PE.ApplyTo(@event);
				flag = PE.Attacker.FireEvent(@event);
				PE.SetFrom(@event);
			}
			return flag;
		}

		// Token: 0x04002263 RID: 8803
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(BeforeMentalAttackEvent), null, new MinEvent.EventPoolCount(BeforeMentalAttackEvent.CountPool), new MinEvent.EventPoolReset(BeforeMentalAttackEvent.ResetPool));

		// Token: 0x04002264 RID: 8804
		private static List<BeforeMentalAttackEvent> Pool;

		// Token: 0x04002265 RID: 8805
		private static int PoolCounter;
	}
}
