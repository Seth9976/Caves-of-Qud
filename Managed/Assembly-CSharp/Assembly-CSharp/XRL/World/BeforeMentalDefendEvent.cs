using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000697 RID: 1687
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class BeforeMentalDefendEvent : IMentalAttackEvent
	{
		// Token: 0x06004810 RID: 18448 RVA: 0x0016B430 File Offset: 0x00169630
		public BeforeMentalDefendEvent()
		{
			this.ID = BeforeMentalDefendEvent.ID;
		}

		// Token: 0x06004811 RID: 18449 RVA: 0x0016B443 File Offset: 0x00169643
		public static int CountPool()
		{
			if (BeforeMentalDefendEvent.Pool != null)
			{
				return BeforeMentalDefendEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004812 RID: 18450 RVA: 0x0016B458 File Offset: 0x00169658
		public static void ResetPool()
		{
			while (BeforeMentalDefendEvent.PoolCounter > 0)
			{
				BeforeMentalDefendEvent.Pool[--BeforeMentalDefendEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004813 RID: 18451 RVA: 0x0016B480 File Offset: 0x00169680
		public static void ResetTo(ref BeforeMentalDefendEvent E)
		{
			MinEvent.ResetTo<BeforeMentalDefendEvent>(E, BeforeMentalDefendEvent.Pool, ref BeforeMentalDefendEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004814 RID: 18452 RVA: 0x0016B496 File Offset: 0x00169696
		public static BeforeMentalDefendEvent FromPool()
		{
			return MinEvent.FromPool<BeforeMentalDefendEvent>(ref BeforeMentalDefendEvent.Pool, ref BeforeMentalDefendEvent.PoolCounter, 8192);
		}

		// Token: 0x06004815 RID: 18453 RVA: 0x0016B4AC File Offset: 0x001696AC
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004816 RID: 18454 RVA: 0x0016B4C0 File Offset: 0x001696C0
		public static BeforeMentalDefendEvent FromPool(IMentalAttackEvent PE)
		{
			BeforeMentalDefendEvent beforeMentalDefendEvent = BeforeMentalDefendEvent.FromPool();
			PE.ApplyTo(beforeMentalDefendEvent);
			return beforeMentalDefendEvent;
		}

		// Token: 0x06004817 RID: 18455 RVA: 0x0016B4DC File Offset: 0x001696DC
		public static bool Check(IMentalAttackEvent PE)
		{
			bool flag = true;
			if (PE.Defender.WantEvent(BeforeMentalDefendEvent.ID, IMentalAttackEvent.CascadeLevel))
			{
				BeforeMentalDefendEvent beforeMentalDefendEvent = BeforeMentalDefendEvent.FromPool(PE);
				flag = PE.Defender.HandleEvent<BeforeMentalDefendEvent>(beforeMentalDefendEvent, PE);
				PE.SetFrom(beforeMentalDefendEvent);
			}
			if (flag && PE.Defender.HasRegisteredEvent("BeforeMentalDefend"))
			{
				Event @event = Event.New("BeforeMentalDefend", 0, 0, 0);
				PE.ApplyTo(@event);
				flag = PE.Defender.FireEvent(@event);
				PE.SetFrom(@event);
			}
			return flag;
		}

		// Token: 0x04002266 RID: 8806
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(BeforeMentalDefendEvent), null, new MinEvent.EventPoolCount(BeforeMentalDefendEvent.CountPool), new MinEvent.EventPoolReset(BeforeMentalDefendEvent.ResetPool));

		// Token: 0x04002267 RID: 8807
		private static List<BeforeMentalDefendEvent> Pool;

		// Token: 0x04002268 RID: 8808
		private static int PoolCounter;
	}
}
