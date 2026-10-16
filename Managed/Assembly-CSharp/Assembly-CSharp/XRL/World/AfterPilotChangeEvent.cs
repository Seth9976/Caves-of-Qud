using System;

namespace XRL.World
{
	// Token: 0x02000668 RID: 1640
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Pool)]
	public class AfterPilotChangeEvent : PooledEvent<AfterPilotChangeEvent>
	{
		// Token: 0x060046BA RID: 18106 RVA: 0x00167957 File Offset: 0x00165B57
		public override int GetCascadeLevel()
		{
			return AfterPilotChangeEvent.CascadeLevel;
		}

		// Token: 0x060046BB RID: 18107 RVA: 0x0016795E File Offset: 0x00165B5E
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060046BC RID: 18108 RVA: 0x00167967 File Offset: 0x00165B67
		public override void Reset()
		{
			base.Reset();
			this.Vehicle = null;
			this.NewPilot = null;
			this.OldPilot = null;
		}

		// Token: 0x060046BD RID: 18109 RVA: 0x00167984 File Offset: 0x00165B84
		public static void Send(GameObject Vehicle, GameObject NewPilot, GameObject OldPilot)
		{
			AfterPilotChangeEvent afterPilotChangeEvent = PooledEvent<AfterPilotChangeEvent>.FromPool();
			afterPilotChangeEvent.Vehicle = Vehicle;
			afterPilotChangeEvent.NewPilot = NewPilot;
			afterPilotChangeEvent.OldPilot = OldPilot;
			Vehicle.HandleEvent(afterPilotChangeEvent);
			if (NewPilot != null)
			{
				NewPilot.HandleEvent(afterPilotChangeEvent);
			}
			if (OldPilot != null)
			{
				OldPilot.HandleEvent(afterPilotChangeEvent);
			}
			PooledEvent<AfterPilotChangeEvent>.ResetTo(ref afterPilotChangeEvent);
		}

		// Token: 0x040021A3 RID: 8611
		public new static readonly int CascadeLevel = 15;

		// Token: 0x040021A4 RID: 8612
		public GameObject Vehicle;

		// Token: 0x040021A5 RID: 8613
		public GameObject NewPilot;

		// Token: 0x040021A6 RID: 8614
		public GameObject OldPilot;
	}
}
