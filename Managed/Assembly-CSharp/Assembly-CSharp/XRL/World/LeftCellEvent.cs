using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020007FF RID: 2047
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class LeftCellEvent : IObjectCellInteractionEvent
	{
		// Token: 0x060051EF RID: 20975 RVA: 0x0018B3D5 File Offset: 0x001895D5
		public LeftCellEvent()
		{
			this.ID = LeftCellEvent.ID;
		}

		// Token: 0x060051F0 RID: 20976 RVA: 0x0018B3E8 File Offset: 0x001895E8
		public static int CountPool()
		{
			if (LeftCellEvent.Pool != null)
			{
				return LeftCellEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060051F1 RID: 20977 RVA: 0x0018B3FD File Offset: 0x001895FD
		public static void ResetPool()
		{
			while (LeftCellEvent.PoolCounter > 0)
			{
				LeftCellEvent.Pool[--LeftCellEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060051F2 RID: 20978 RVA: 0x0018B425 File Offset: 0x00189625
		public static void ResetTo(ref LeftCellEvent E)
		{
			MinEvent.ResetTo<LeftCellEvent>(E, LeftCellEvent.Pool, ref LeftCellEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060051F3 RID: 20979 RVA: 0x0018B43B File Offset: 0x0018963B
		public static LeftCellEvent FromPool()
		{
			return MinEvent.FromPool<LeftCellEvent>(ref LeftCellEvent.Pool, ref LeftCellEvent.PoolCounter, 8192);
		}

		// Token: 0x060051F4 RID: 20980 RVA: 0x0018B451 File Offset: 0x00189651
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060051F5 RID: 20981 RVA: 0x0018B468 File Offset: 0x00189668
		public static LeftCellEvent FromPool(GameObject Object, Cell Cell, bool Forced = false, bool System = false, bool IgnoreGravity = false, bool NoStack = false, string Direction = null, string Type = null, GameObject Dragging = null, GameObject Actor = null, GameObject ForceSwap = null, GameObject Ignore = null)
		{
			LeftCellEvent leftCellEvent = LeftCellEvent.FromPool();
			leftCellEvent.Object = Object;
			leftCellEvent.Cell = Cell;
			leftCellEvent.Forced = Forced;
			leftCellEvent.System = System;
			leftCellEvent.IgnoreGravity = IgnoreGravity;
			leftCellEvent.NoStack = NoStack;
			leftCellEvent.Direction = Direction;
			leftCellEvent.Type = Type;
			leftCellEvent.Dragging = Dragging;
			leftCellEvent.Actor = Actor;
			leftCellEvent.ForceSwap = ForceSwap;
			leftCellEvent.Ignore = Ignore;
			return leftCellEvent;
		}

		// Token: 0x0400291F RID: 10527
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(LeftCellEvent), null, new MinEvent.EventPoolCount(LeftCellEvent.CountPool), new MinEvent.EventPoolReset(LeftCellEvent.ResetPool));

		// Token: 0x04002920 RID: 10528
		private static List<LeftCellEvent> Pool;

		// Token: 0x04002921 RID: 10529
		private static int PoolCounter;
	}
}
