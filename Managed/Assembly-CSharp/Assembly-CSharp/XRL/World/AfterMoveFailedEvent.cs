using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000665 RID: 1637
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AfterMoveFailedEvent : IObjectCellInteractionEvent
	{
		// Token: 0x060046A3 RID: 18083 RVA: 0x0016753F File Offset: 0x0016573F
		public AfterMoveFailedEvent()
		{
			this.ID = AfterMoveFailedEvent.ID;
		}

		// Token: 0x060046A4 RID: 18084 RVA: 0x00167552 File Offset: 0x00165752
		public static int CountPool()
		{
			if (AfterMoveFailedEvent.Pool != null)
			{
				return AfterMoveFailedEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060046A5 RID: 18085 RVA: 0x00167567 File Offset: 0x00165767
		public static void ResetPool()
		{
			while (AfterMoveFailedEvent.PoolCounter > 0)
			{
				AfterMoveFailedEvent.Pool[--AfterMoveFailedEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060046A6 RID: 18086 RVA: 0x0016758F File Offset: 0x0016578F
		public static void ResetTo(ref AfterMoveFailedEvent E)
		{
			MinEvent.ResetTo<AfterMoveFailedEvent>(E, AfterMoveFailedEvent.Pool, ref AfterMoveFailedEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060046A7 RID: 18087 RVA: 0x001675A5 File Offset: 0x001657A5
		public static AfterMoveFailedEvent FromPool()
		{
			return MinEvent.FromPool<AfterMoveFailedEvent>(ref AfterMoveFailedEvent.Pool, ref AfterMoveFailedEvent.PoolCounter, 8192);
		}

		// Token: 0x060046A8 RID: 18088 RVA: 0x001675BB File Offset: 0x001657BB
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060046A9 RID: 18089 RVA: 0x001675CF File Offset: 0x001657CF
		public override void Reset()
		{
			base.Reset();
			this.Origin = null;
		}

		// Token: 0x060046AA RID: 18090 RVA: 0x001675E0 File Offset: 0x001657E0
		public static void Send(GameObject Object, Cell Origin, Cell Destination, bool Forced = false, bool System = false, bool IgnoreGravity = false, bool NoStack = false, string Direction = null, string Type = null, GameObject Dragging = null, GameObject Actor = null, GameObject ForceSwap = null, GameObject Ignore = null)
		{
			Object.FireRegisteredEvent(AfterMoveFailedEvent.RegisteredInstance);
			AfterMoveFailedEvent afterMoveFailedEvent = AfterMoveFailedEvent.FromPool();
			afterMoveFailedEvent.Object = Object;
			afterMoveFailedEvent.Origin = Origin;
			afterMoveFailedEvent.Cell = Destination;
			afterMoveFailedEvent.Forced = Forced;
			afterMoveFailedEvent.System = System;
			afterMoveFailedEvent.IgnoreGravity = IgnoreGravity;
			afterMoveFailedEvent.NoStack = NoStack;
			afterMoveFailedEvent.Direction = Direction;
			afterMoveFailedEvent.Type = Type;
			afterMoveFailedEvent.Dragging = Dragging;
			afterMoveFailedEvent.Actor = Actor;
			afterMoveFailedEvent.ForceSwap = ForceSwap;
			afterMoveFailedEvent.Ignore = Ignore;
			Object.HandleEvent(afterMoveFailedEvent);
			AfterMoveFailedEvent.ResetTo(ref afterMoveFailedEvent);
		}

		// Token: 0x04002198 RID: 8600
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AfterMoveFailedEvent), null, new MinEvent.EventPoolCount(AfterMoveFailedEvent.CountPool), new MinEvent.EventPoolReset(AfterMoveFailedEvent.ResetPool));

		// Token: 0x04002199 RID: 8601
		private static List<AfterMoveFailedEvent> Pool;

		// Token: 0x0400219A RID: 8602
		private static int PoolCounter;

		// Token: 0x0400219B RID: 8603
		private static ImmutableEvent RegisteredInstance = new ImmutableEvent("MoveFailed");

		// Token: 0x0400219C RID: 8604
		public Cell Origin;
	}
}
