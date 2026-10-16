using System;

namespace XRL.World
{
	// Token: 0x020007E1 RID: 2017
	[GameEvent(Base = true)]
	public abstract class IZoneEvent : MinEvent
	{
		// Token: 0x06005124 RID: 20772 RVA: 0x00188AF8 File Offset: 0x00186CF8
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005125 RID: 20773 RVA: 0x00188B01 File Offset: 0x00186D01
		public override void Reset()
		{
			base.Reset();
			this.Zone = null;
		}

		// Token: 0x0400289F RID: 10399
		public Zone Zone;
	}
}
