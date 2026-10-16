using System;

namespace XRL.World
{
	// Token: 0x0200087D RID: 2173
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Singleton)]
	public class ZoneActivatedEvent : IZoneEvent
	{
		// Token: 0x0600563E RID: 22078 RVA: 0x0019AAD0 File Offset: 0x00198CD0
		public ZoneActivatedEvent()
		{
			this.ID = ZoneActivatedEvent.ID;
		}

		// Token: 0x0600563F RID: 22079 RVA: 0x0019AAE3 File Offset: 0x00198CE3
		public override int GetCascadeLevel()
		{
			return ZoneActivatedEvent.CascadeLevel;
		}

		// Token: 0x06005640 RID: 22080 RVA: 0x0019AAEA File Offset: 0x00198CEA
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06005641 RID: 22081 RVA: 0x0019AAFE File Offset: 0x00198CFE
		public static void Send(Zone Zone)
		{
			ZoneActivatedEvent.Instance.Zone = Zone;
			The.Game.HandleEvent(ZoneActivatedEvent.Instance);
			Zone.HandleEvent(ZoneActivatedEvent.Instance);
			ZoneActivatedEvent.Instance.Reset();
		}

		// Token: 0x04002B75 RID: 11125
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(ZoneActivatedEvent), null, null, null);

		// Token: 0x04002B76 RID: 11126
		public new static readonly int CascadeLevel = 15;

		// Token: 0x04002B77 RID: 11127
		public static readonly ZoneActivatedEvent Instance = new ZoneActivatedEvent();
	}
}
