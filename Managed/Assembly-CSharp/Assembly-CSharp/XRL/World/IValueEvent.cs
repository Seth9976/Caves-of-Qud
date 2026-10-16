using System;

namespace XRL.World
{
	// Token: 0x020007DE RID: 2014
	[GameEvent(Base = true)]
	public abstract class IValueEvent : MinEvent
	{
		// Token: 0x0600511A RID: 20762 RVA: 0x0018894B File Offset: 0x00186B4B
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600511B RID: 20763 RVA: 0x00188954 File Offset: 0x00186B54
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Value = 0.01;
		}

		// Token: 0x0400288D RID: 10381
		public GameObject Object;

		// Token: 0x0400288E RID: 10382
		public double Value = 0.01;
	}
}
