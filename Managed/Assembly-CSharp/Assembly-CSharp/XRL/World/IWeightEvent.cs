using System;

namespace XRL.World
{
	// Token: 0x020007DF RID: 2015
	[GameEvent(Base = true)]
	public abstract class IWeightEvent : MinEvent
	{
		// Token: 0x0600511D RID: 20765 RVA: 0x00188989 File Offset: 0x00186B89
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600511E RID: 20766 RVA: 0x00188992 File Offset: 0x00186B92
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.BaseWeight = 0.0;
			this.Weight = 0.0;
		}

		// Token: 0x0400288F RID: 10383
		public GameObject Object;

		// Token: 0x04002890 RID: 10384
		public double BaseWeight;

		// Token: 0x04002891 RID: 10385
		public double Weight;
	}
}
