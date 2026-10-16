using System;

namespace XRL.World
{
	// Token: 0x020007D4 RID: 2004
	[GameEvent(Base = true)]
	public abstract class IObjectCellInteractionEvent : MinEvent
	{
		// Token: 0x060050F0 RID: 20720 RVA: 0x00188063 File Offset: 0x00186263
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060050F1 RID: 20721 RVA: 0x0018806C File Offset: 0x0018626C
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Cell = null;
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
			this.Blocking = null;
		}

		// Token: 0x0400284F RID: 10319
		public GameObject Object;

		// Token: 0x04002850 RID: 10320
		public Cell Cell;

		// Token: 0x04002851 RID: 10321
		public bool Forced;

		// Token: 0x04002852 RID: 10322
		public bool System;

		// Token: 0x04002853 RID: 10323
		public bool IgnoreGravity;

		// Token: 0x04002854 RID: 10324
		public bool NoStack;

		// Token: 0x04002855 RID: 10325
		public string Direction;

		// Token: 0x04002856 RID: 10326
		public string Type;

		// Token: 0x04002857 RID: 10327
		public GameObject Dragging;

		// Token: 0x04002858 RID: 10328
		public GameObject Actor;

		// Token: 0x04002859 RID: 10329
		public GameObject ForceSwap;

		// Token: 0x0400285A RID: 10330
		public GameObject Ignore;

		// Token: 0x0400285B RID: 10331
		public GameObject Blocking;
	}
}
