using System;

namespace XRL.World
{
	// Token: 0x0200087F RID: 2175
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Singleton)]
	public class ZoneDeactivatedEvent : IZoneEvent
	{
		// Token: 0x0600564C RID: 22092 RVA: 0x0019AC5F File Offset: 0x00198E5F
		public ZoneDeactivatedEvent()
		{
			this.ID = ZoneDeactivatedEvent.ID;
		}

		// Token: 0x0600564D RID: 22093 RVA: 0x0019AC72 File Offset: 0x00198E72
		public override int GetCascadeLevel()
		{
			return ZoneDeactivatedEvent.CascadeLevel;
		}

		// Token: 0x0600564E RID: 22094 RVA: 0x0019AC79 File Offset: 0x00198E79
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x0600564F RID: 22095 RVA: 0x0019AC8D File Offset: 0x00198E8D
		public static void Send(Zone Zone)
		{
			ZoneDeactivatedEvent.Instance.Zone = Zone;
			The.Game.HandleEvent(ZoneDeactivatedEvent.Instance);
			Zone.HandleEvent(ZoneDeactivatedEvent.Instance);
			ZoneDeactivatedEvent.Instance.Reset();
		}

		// Token: 0x04002B7C RID: 11132
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(ZoneDeactivatedEvent), null, null, null);

		// Token: 0x04002B7D RID: 11133
		public new static readonly int CascadeLevel = 15;

		// Token: 0x04002B7E RID: 11134
		public static readonly ZoneDeactivatedEvent Instance = new ZoneDeactivatedEvent();
	}
}
