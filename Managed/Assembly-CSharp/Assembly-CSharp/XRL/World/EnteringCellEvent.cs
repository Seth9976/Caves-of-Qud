using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000709 RID: 1801
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class EnteringCellEvent : IObjectCellInteractionEvent
	{
		// Token: 0x06004B2C RID: 19244 RVA: 0x001761C5 File Offset: 0x001743C5
		public EnteringCellEvent()
		{
			this.ID = EnteringCellEvent.ID;
		}

		// Token: 0x06004B2D RID: 19245 RVA: 0x001761D8 File Offset: 0x001743D8
		public static int CountPool()
		{
			if (EnteringCellEvent.Pool != null)
			{
				return EnteringCellEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004B2E RID: 19246 RVA: 0x001761ED File Offset: 0x001743ED
		public static void ResetPool()
		{
			while (EnteringCellEvent.PoolCounter > 0)
			{
				EnteringCellEvent.Pool[--EnteringCellEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004B2F RID: 19247 RVA: 0x00176215 File Offset: 0x00174415
		public static void ResetTo(ref EnteringCellEvent E)
		{
			MinEvent.ResetTo<EnteringCellEvent>(E, EnteringCellEvent.Pool, ref EnteringCellEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004B30 RID: 19248 RVA: 0x0017622B File Offset: 0x0017442B
		public static EnteringCellEvent FromPool()
		{
			return MinEvent.FromPool<EnteringCellEvent>(ref EnteringCellEvent.Pool, ref EnteringCellEvent.PoolCounter, 8192);
		}

		// Token: 0x06004B31 RID: 19249 RVA: 0x00176241 File Offset: 0x00174441
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004B32 RID: 19250 RVA: 0x00176258 File Offset: 0x00174458
		public static EnteringCellEvent FromPool(GameObject Object, Cell Cell, bool Forced = false, bool System = false, bool IgnoreGravity = false, bool NoStack = false, string Direction = null, string Type = null, GameObject Dragging = null, GameObject Actor = null, GameObject ForceSwap = null, GameObject Ignore = null)
		{
			EnteringCellEvent enteringCellEvent = EnteringCellEvent.FromPool();
			enteringCellEvent.Object = Object;
			enteringCellEvent.Cell = Cell;
			enteringCellEvent.Forced = Forced;
			enteringCellEvent.System = System;
			enteringCellEvent.IgnoreGravity = IgnoreGravity;
			enteringCellEvent.NoStack = NoStack;
			enteringCellEvent.Direction = Direction;
			enteringCellEvent.Type = Type;
			enteringCellEvent.Dragging = Dragging;
			enteringCellEvent.Actor = Actor;
			enteringCellEvent.ForceSwap = ForceSwap;
			enteringCellEvent.Ignore = Ignore;
			return enteringCellEvent;
		}

		// Token: 0x04002448 RID: 9288
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(EnteringCellEvent), null, new MinEvent.EventPoolCount(EnteringCellEvent.CountPool), new MinEvent.EventPoolReset(EnteringCellEvent.ResetPool));

		// Token: 0x04002449 RID: 9289
		private static List<EnteringCellEvent> Pool;

		// Token: 0x0400244A RID: 9290
		private static int PoolCounter;
	}
}
