using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020007FE RID: 2046
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class LeavingCellEvent : IObjectCellInteractionEvent
	{
		// Token: 0x060051E7 RID: 20967 RVA: 0x0018B2A5 File Offset: 0x001894A5
		public LeavingCellEvent()
		{
			this.ID = LeavingCellEvent.ID;
		}

		// Token: 0x060051E8 RID: 20968 RVA: 0x0018B2B8 File Offset: 0x001894B8
		public static int CountPool()
		{
			if (LeavingCellEvent.Pool != null)
			{
				return LeavingCellEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060051E9 RID: 20969 RVA: 0x0018B2CD File Offset: 0x001894CD
		public static void ResetPool()
		{
			while (LeavingCellEvent.PoolCounter > 0)
			{
				LeavingCellEvent.Pool[--LeavingCellEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060051EA RID: 20970 RVA: 0x0018B2F5 File Offset: 0x001894F5
		public static void ResetTo(ref LeavingCellEvent E)
		{
			MinEvent.ResetTo<LeavingCellEvent>(E, LeavingCellEvent.Pool, ref LeavingCellEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060051EB RID: 20971 RVA: 0x0018B30B File Offset: 0x0018950B
		public static LeavingCellEvent FromPool()
		{
			return MinEvent.FromPool<LeavingCellEvent>(ref LeavingCellEvent.Pool, ref LeavingCellEvent.PoolCounter, 8192);
		}

		// Token: 0x060051EC RID: 20972 RVA: 0x0018B321 File Offset: 0x00189521
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060051ED RID: 20973 RVA: 0x0018B338 File Offset: 0x00189538
		public static LeavingCellEvent FromPool(GameObject Object, Cell Cell, bool Forced = false, bool System = false, bool IgnoreGravity = false, bool NoStack = false, string Direction = null, string Type = null, GameObject Dragging = null, GameObject Actor = null, GameObject ForceSwap = null, GameObject Ignore = null)
		{
			LeavingCellEvent leavingCellEvent = LeavingCellEvent.FromPool();
			leavingCellEvent.Object = Object;
			leavingCellEvent.Cell = Cell;
			leavingCellEvent.Forced = Forced;
			leavingCellEvent.System = System;
			leavingCellEvent.IgnoreGravity = IgnoreGravity;
			leavingCellEvent.NoStack = NoStack;
			leavingCellEvent.Direction = Direction;
			leavingCellEvent.Type = Type;
			leavingCellEvent.Dragging = Dragging;
			leavingCellEvent.Actor = Actor;
			leavingCellEvent.ForceSwap = ForceSwap;
			leavingCellEvent.Ignore = Ignore;
			return leavingCellEvent;
		}

		// Token: 0x0400291C RID: 10524
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(LeavingCellEvent), null, new MinEvent.EventPoolCount(LeavingCellEvent.CountPool), new MinEvent.EventPoolReset(LeavingCellEvent.ResetPool));

		// Token: 0x0400291D RID: 10525
		private static List<LeavingCellEvent> Pool;

		// Token: 0x0400291E RID: 10526
		private static int PoolCounter;
	}
}
