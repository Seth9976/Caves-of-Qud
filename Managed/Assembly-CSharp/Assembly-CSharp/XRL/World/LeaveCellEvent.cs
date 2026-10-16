using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020007FD RID: 2045
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class LeaveCellEvent : IObjectCellInteractionEvent
	{
		// Token: 0x060051DF RID: 20959 RVA: 0x0018B177 File Offset: 0x00189377
		public LeaveCellEvent()
		{
			this.ID = LeaveCellEvent.ID;
		}

		// Token: 0x060051E0 RID: 20960 RVA: 0x0018B18A File Offset: 0x0018938A
		public static int CountPool()
		{
			if (LeaveCellEvent.Pool != null)
			{
				return LeaveCellEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060051E1 RID: 20961 RVA: 0x0018B19F File Offset: 0x0018939F
		public static void ResetPool()
		{
			while (LeaveCellEvent.PoolCounter > 0)
			{
				LeaveCellEvent.Pool[--LeaveCellEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060051E2 RID: 20962 RVA: 0x0018B1C7 File Offset: 0x001893C7
		public static void ResetTo(ref LeaveCellEvent E)
		{
			MinEvent.ResetTo<LeaveCellEvent>(E, LeaveCellEvent.Pool, ref LeaveCellEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060051E3 RID: 20963 RVA: 0x0018B1DD File Offset: 0x001893DD
		public static LeaveCellEvent FromPool()
		{
			return MinEvent.FromPool<LeaveCellEvent>(ref LeaveCellEvent.Pool, ref LeaveCellEvent.PoolCounter, 8192);
		}

		// Token: 0x060051E4 RID: 20964 RVA: 0x0018B1F3 File Offset: 0x001893F3
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060051E5 RID: 20965 RVA: 0x0018B208 File Offset: 0x00189408
		public static LeaveCellEvent FromPool(GameObject Object, Cell Cell, bool Forced = false, bool System = false, bool IgnoreGravity = false, bool NoStack = false, string Direction = null, string Type = null, GameObject Dragging = null, GameObject Actor = null, GameObject ForceSwap = null, GameObject Ignore = null)
		{
			LeaveCellEvent leaveCellEvent = LeaveCellEvent.FromPool();
			leaveCellEvent.Object = Object;
			leaveCellEvent.Cell = Cell;
			leaveCellEvent.Forced = Forced;
			leaveCellEvent.System = System;
			leaveCellEvent.IgnoreGravity = IgnoreGravity;
			leaveCellEvent.NoStack = NoStack;
			leaveCellEvent.Direction = Direction;
			leaveCellEvent.Type = Type;
			leaveCellEvent.Dragging = Dragging;
			leaveCellEvent.Actor = Actor;
			leaveCellEvent.ForceSwap = ForceSwap;
			leaveCellEvent.Ignore = Ignore;
			return leaveCellEvent;
		}

		// Token: 0x04002919 RID: 10521
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(LeaveCellEvent), null, new MinEvent.EventPoolCount(LeaveCellEvent.CountPool), new MinEvent.EventPoolReset(LeaveCellEvent.ResetPool));

		// Token: 0x0400291A RID: 10522
		private static List<LeaveCellEvent> Pool;

		// Token: 0x0400291B RID: 10523
		private static int PoolCounter;
	}
}
