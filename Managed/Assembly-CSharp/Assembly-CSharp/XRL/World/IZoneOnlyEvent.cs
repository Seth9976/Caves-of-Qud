using System;

namespace XRL.World
{
	// Token: 0x020007E2 RID: 2018
	[GameEvent(Base = true, Cascade = 32)]
	public abstract class IZoneOnlyEvent : IZoneEvent
	{
		// Token: 0x06005127 RID: 20775 RVA: 0x00188B18 File Offset: 0x00186D18
		public override int GetCascadeLevel()
		{
			return IZoneOnlyEvent.CascadeLevel;
		}

		// Token: 0x06005128 RID: 20776 RVA: 0x00188B1F File Offset: 0x00186D1F
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x040028A0 RID: 10400
		public new static readonly int CascadeLevel = 32;
	}
}
