using System;

namespace XRL.World
{
	// Token: 0x020007B8 RID: 1976
	[GameEvent(Base = true)]
	public abstract class IAIMoveCommandListEvent : IAICommandListEvent
	{
		// Token: 0x06005033 RID: 20531 RVA: 0x001861DB File Offset: 0x001843DB
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06005034 RID: 20532 RVA: 0x001861EF File Offset: 0x001843EF
		public override void Reset()
		{
			base.Reset();
			this.TargetCell = null;
			this.StandoffDistance = 0;
		}

		// Token: 0x040027B2 RID: 10162
		public Cell TargetCell;

		// Token: 0x040027B3 RID: 10163
		public int StandoffDistance;
	}
}
