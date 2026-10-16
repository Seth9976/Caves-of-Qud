using System;

namespace XRL.World
{
	// Token: 0x0200066F RID: 1647
	[GameEvent(Cascade = 1, Cache = MinEvent.Cache.Pool)]
	public class AfterReputationChangeEvent : PooledEvent<AfterReputationChangeEvent>
	{
		// Token: 0x060046E0 RID: 18144 RVA: 0x00167E97 File Offset: 0x00166097
		public override int GetCascadeLevel()
		{
			return AfterReputationChangeEvent.CascadeLevel;
		}

		// Token: 0x060046E1 RID: 18145 RVA: 0x00167E9E File Offset: 0x0016609E
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060046E2 RID: 18146 RVA: 0x00167EA7 File Offset: 0x001660A7
		public override void Reset()
		{
			base.Reset();
			this.Faction = null;
			this.From = 0;
			this.To = 0;
			this.Type = null;
			this.Silent = false;
			this.Transient = false;
		}

		// Token: 0x060046E3 RID: 18147 RVA: 0x00167EDC File Offset: 0x001660DC
		public static void Send(Faction Faction, int From, int To, string Type = null, bool Silent = false, bool Transient = false)
		{
			AfterReputationChangeEvent afterReputationChangeEvent = PooledEvent<AfterReputationChangeEvent>.FromPool();
			afterReputationChangeEvent.Faction = Faction;
			afterReputationChangeEvent.From = From;
			afterReputationChangeEvent.To = To;
			afterReputationChangeEvent.Type = Type;
			afterReputationChangeEvent.Silent = Silent;
			afterReputationChangeEvent.Transient = Transient;
			The.Game.HandleEvent(afterReputationChangeEvent, true);
			PooledEvent<AfterReputationChangeEvent>.ResetTo(ref afterReputationChangeEvent);
		}

		// Token: 0x040021BE RID: 8638
		public new static readonly int CascadeLevel = 1;

		// Token: 0x040021BF RID: 8639
		public Faction Faction;

		// Token: 0x040021C0 RID: 8640
		public int From;

		// Token: 0x040021C1 RID: 8641
		public int To;

		// Token: 0x040021C2 RID: 8642
		public string Type;

		// Token: 0x040021C3 RID: 8643
		public bool Silent;

		// Token: 0x040021C4 RID: 8644
		public bool Transient;
	}
}
