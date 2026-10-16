using System;

namespace XRL.World
{
	// Token: 0x020007DB RID: 2011
	[GameEvent(Base = true, Cascade = 0)]
	public abstract class IShallowZoneEvent : IZoneEvent
	{
		// Token: 0x06005109 RID: 20745 RVA: 0x0018845B File Offset: 0x0018665B
		public override int GetCascadeLevel()
		{
			return IShallowZoneEvent.CascadeLevel;
		}

		// Token: 0x0600510A RID: 20746 RVA: 0x00188462 File Offset: 0x00186662
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x0400287F RID: 10367
		public new static readonly int CascadeLevel;
	}
}
