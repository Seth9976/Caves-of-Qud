using System;

namespace XRL.World
{
	// Token: 0x020007D5 RID: 2005
	[GameEvent(Base = true)]
	public abstract class IObjectCreationEvent : MinEvent
	{
		// Token: 0x060050F3 RID: 20723 RVA: 0x001880E2 File Offset: 0x001862E2
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060050F4 RID: 20724 RVA: 0x001880EB File Offset: 0x001862EB
		public override void Reset()
		{
			base.Reset();
			this.ReplacementObject = null;
			this.Object = null;
			this.Context = null;
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x060050F5 RID: 20725 RVA: 0x00188108 File Offset: 0x00186308
		public GameObject ActiveObject
		{
			get
			{
				return this.ReplacementObject ?? this.Object;
			}
		}

		// Token: 0x0400285C RID: 10332
		public GameObject ReplacementObject;

		// Token: 0x0400285D RID: 10333
		public GameObject Object;

		// Token: 0x0400285E RID: 10334
		public string Context;
	}
}
