using System;

namespace XRL.World
{
	// Token: 0x02000861 RID: 2145
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Singleton)]
	public class SuspendingEvent : IZoneEvent
	{
		// Token: 0x06005573 RID: 21875 RVA: 0x001989A1 File Offset: 0x00196BA1
		public SuspendingEvent()
		{
			this.ID = SuspendingEvent.ID;
		}

		// Token: 0x06005574 RID: 21876 RVA: 0x001989B4 File Offset: 0x00196BB4
		public override int GetCascadeLevel()
		{
			return SuspendingEvent.CascadeLevel;
		}

		// Token: 0x06005575 RID: 21877 RVA: 0x001989BB File Offset: 0x00196BBB
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06005576 RID: 21878 RVA: 0x001989CF File Offset: 0x00196BCF
		public static void Send(Zone Zone)
		{
			SuspendingEvent.Instance.Zone = Zone;
			The.Game.HandleEvent(SuspendingEvent.Instance);
			Zone.HandleEvent(SuspendingEvent.Instance);
			SuspendingEvent.Instance.Reset();
		}

		// Token: 0x04002B13 RID: 11027
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(SuspendingEvent), null, null, null);

		// Token: 0x04002B14 RID: 11028
		public new static readonly int CascadeLevel = 15;

		// Token: 0x04002B15 RID: 11029
		public static readonly SuspendingEvent Instance = new SuspendingEvent();
	}
}
