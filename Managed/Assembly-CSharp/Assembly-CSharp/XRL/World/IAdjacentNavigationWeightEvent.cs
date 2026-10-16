using System;

namespace XRL.World
{
	// Token: 0x020007BC RID: 1980
	[GameEvent(Base = true)]
	public abstract class IAdjacentNavigationWeightEvent : INavigationWeightEvent
	{
		// Token: 0x06005042 RID: 20546 RVA: 0x001863B5 File Offset: 0x001845B5
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06005043 RID: 20547 RVA: 0x001863C9 File Offset: 0x001845C9
		public override void Reset()
		{
			base.Reset();
			this.AdjacentCell = null;
		}

		// Token: 0x06005044 RID: 20548 RVA: 0x001863D8 File Offset: 0x001845D8
		public override void ApplyTo(INavigationWeightEvent E)
		{
			base.ApplyTo(E);
			IAdjacentNavigationWeightEvent adjacentNavigationWeightEvent = E as IAdjacentNavigationWeightEvent;
			if (adjacentNavigationWeightEvent != null)
			{
				adjacentNavigationWeightEvent.AdjacentCell = this.AdjacentCell;
			}
		}

		// Token: 0x040027C0 RID: 10176
		public Cell AdjacentCell;
	}
}
