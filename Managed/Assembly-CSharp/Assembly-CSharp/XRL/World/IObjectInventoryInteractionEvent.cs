using System;

namespace XRL.World
{
	// Token: 0x020007D6 RID: 2006
	[GameEvent(Base = true)]
	public abstract class IObjectInventoryInteractionEvent : MinEvent
	{
		// Token: 0x060050F7 RID: 20727 RVA: 0x00188122 File Offset: 0x00186322
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060050F8 RID: 20728 RVA: 0x0018812C File Offset: 0x0018632C
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Inventory = null;
			this.Forced = false;
			this.System = false;
			this.IgnoreGravity = false;
			this.NoStack = false;
			this.Direction = null;
			this.Type = null;
			this.Dragging = null;
			this.Actor = null;
			this.ForceSwap = null;
			this.Ignore = null;
		}

		// Token: 0x0400285F RID: 10335
		public GameObject Object;

		// Token: 0x04002860 RID: 10336
		public IInventory Inventory;

		// Token: 0x04002861 RID: 10337
		public bool Forced;

		// Token: 0x04002862 RID: 10338
		public bool System;

		// Token: 0x04002863 RID: 10339
		public bool IgnoreGravity;

		// Token: 0x04002864 RID: 10340
		public bool NoStack;

		// Token: 0x04002865 RID: 10341
		public string Direction;

		// Token: 0x04002866 RID: 10342
		public string Type;

		// Token: 0x04002867 RID: 10343
		public GameObject Dragging;

		// Token: 0x04002868 RID: 10344
		public GameObject Actor;

		// Token: 0x04002869 RID: 10345
		public GameObject ForceSwap;

		// Token: 0x0400286A RID: 10346
		public GameObject Ignore;
	}
}
