using System;

namespace XRL.World
{
	// Token: 0x02000880 RID: 2176
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Singleton)]
	public class ZoneThawedEvent : IZoneEvent
	{
		// Token: 0x06005651 RID: 22097 RVA: 0x0019ACEA File Offset: 0x00198EEA
		public ZoneThawedEvent()
		{
			this.ID = ZoneThawedEvent.ID;
		}

		// Token: 0x06005652 RID: 22098 RVA: 0x0019ACFD File Offset: 0x00198EFD
		public override int GetCascadeLevel()
		{
			return ZoneThawedEvent.CascadeLevel;
		}

		// Token: 0x06005653 RID: 22099 RVA: 0x0019AD04 File Offset: 0x00198F04
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06005654 RID: 22100 RVA: 0x0019AD18 File Offset: 0x00198F18
		public override void Reset()
		{
			base.Reset();
			this.TicksFrozen = 0L;
		}

		// Token: 0x06005655 RID: 22101 RVA: 0x0019AD28 File Offset: 0x00198F28
		public static void Send(Zone Zone, long TicksFrozen = 0L)
		{
			ZoneThawedEvent.Instance.Zone = Zone;
			ZoneThawedEvent.Instance.TicksFrozen = TicksFrozen;
			The.Game.HandleEvent(ZoneThawedEvent.Instance);
			Zone.HandleEvent(ZoneThawedEvent.Instance);
			ZoneThawedEvent.Instance.Reset();
		}

		// Token: 0x04002B7F RID: 11135
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(ZoneThawedEvent), null, null, null);

		// Token: 0x04002B80 RID: 11136
		public new static readonly int CascadeLevel = 15;

		// Token: 0x04002B81 RID: 11137
		public static readonly ZoneThawedEvent Instance = new ZoneThawedEvent();

		// Token: 0x04002B82 RID: 11138
		public long TicksFrozen;
	}
}
