using System;

namespace XRL.World
{
	// Token: 0x020007B7 RID: 1975
	[GameEvent(Base = true, Cascade = 17)]
	public abstract class IAIItemCommandListEvent : IAICommandListEvent
	{
		// Token: 0x0600502F RID: 20527 RVA: 0x001861AF File Offset: 0x001843AF
		public override int GetCascadeLevel()
		{
			return IAIItemCommandListEvent.CascadeLevel;
		}

		// Token: 0x06005030 RID: 20528 RVA: 0x001861B6 File Offset: 0x001843B6
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x040027B1 RID: 10161
		public new static readonly int CascadeLevel = 17;
	}
}
