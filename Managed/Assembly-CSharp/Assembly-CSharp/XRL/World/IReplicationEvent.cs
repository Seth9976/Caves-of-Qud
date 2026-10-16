using System;

namespace XRL.World
{
	// Token: 0x020007D9 RID: 2009
	[GameEvent(Base = true)]
	public abstract class IReplicationEvent : MinEvent
	{
		// Token: 0x06005100 RID: 20736 RVA: 0x001881DB File Offset: 0x001863DB
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005101 RID: 20737 RVA: 0x001881E4 File Offset: 0x001863E4
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Actor = null;
			this.Context = null;
			this.Temporary = false;
		}

		// Token: 0x0400286D RID: 10349
		public GameObject Object;

		// Token: 0x0400286E RID: 10350
		public GameObject Actor;

		// Token: 0x0400286F RID: 10351
		public string Context;

		// Token: 0x04002870 RID: 10352
		public bool Temporary;
	}
}
