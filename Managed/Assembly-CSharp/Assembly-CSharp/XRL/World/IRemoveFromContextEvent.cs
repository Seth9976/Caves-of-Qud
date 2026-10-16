using System;

namespace XRL.World
{
	// Token: 0x020007D8 RID: 2008
	[GameEvent(Base = true)]
	public abstract class IRemoveFromContextEvent : MinEvent
	{
		// Token: 0x060050FD RID: 20733 RVA: 0x001881BB File Offset: 0x001863BB
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060050FE RID: 20734 RVA: 0x001881C4 File Offset: 0x001863C4
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
		}

		// Token: 0x0400286C RID: 10348
		public GameObject Object;
	}
}
