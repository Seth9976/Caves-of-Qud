using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000813 RID: 2067
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class ObjectEnteringCellEvent : IObjectCellInteractionEvent
	{
		// Token: 0x06005276 RID: 21110 RVA: 0x0018CE7D File Offset: 0x0018B07D
		public ObjectEnteringCellEvent()
		{
			this.ID = ObjectEnteringCellEvent.ID;
		}

		// Token: 0x06005277 RID: 21111 RVA: 0x0018CE90 File Offset: 0x0018B090
		public static int CountPool()
		{
			if (ObjectEnteringCellEvent.Pool != null)
			{
				return ObjectEnteringCellEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06005278 RID: 21112 RVA: 0x0018CEA5 File Offset: 0x0018B0A5
		public static void ResetPool()
		{
			while (ObjectEnteringCellEvent.PoolCounter > 0)
			{
				ObjectEnteringCellEvent.Pool[--ObjectEnteringCellEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06005279 RID: 21113 RVA: 0x0018CECD File Offset: 0x0018B0CD
		public static void ResetTo(ref ObjectEnteringCellEvent E)
		{
			MinEvent.ResetTo<ObjectEnteringCellEvent>(E, ObjectEnteringCellEvent.Pool, ref ObjectEnteringCellEvent.PoolCounter);
			E = null;
		}

		// Token: 0x0600527A RID: 21114 RVA: 0x0018CEE3 File Offset: 0x0018B0E3
		public static ObjectEnteringCellEvent FromPool()
		{
			return MinEvent.FromPool<ObjectEnteringCellEvent>(ref ObjectEnteringCellEvent.Pool, ref ObjectEnteringCellEvent.PoolCounter, 8192);
		}

		// Token: 0x0600527B RID: 21115 RVA: 0x0018CEF9 File Offset: 0x0018B0F9
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x0600527C RID: 21116 RVA: 0x0018CF10 File Offset: 0x0018B110
		public static ObjectEnteringCellEvent FromPool(GameObject Object, Cell Cell, bool Forced = false, bool System = false, bool IgnoreGravity = false, bool NoStack = false, string Direction = null, string Type = null, GameObject Dragging = null, GameObject Actor = null, GameObject ForceSwap = null, GameObject Ignore = null)
		{
			ObjectEnteringCellEvent objectEnteringCellEvent = ObjectEnteringCellEvent.FromPool();
			objectEnteringCellEvent.Object = Object;
			objectEnteringCellEvent.Cell = Cell;
			objectEnteringCellEvent.Forced = Forced;
			objectEnteringCellEvent.System = System;
			objectEnteringCellEvent.IgnoreGravity = IgnoreGravity;
			objectEnteringCellEvent.NoStack = NoStack;
			objectEnteringCellEvent.Direction = Direction;
			objectEnteringCellEvent.Type = Type;
			objectEnteringCellEvent.Dragging = Dragging;
			objectEnteringCellEvent.Actor = Actor;
			objectEnteringCellEvent.ForceSwap = ForceSwap;
			objectEnteringCellEvent.Ignore = Ignore;
			return objectEnteringCellEvent;
		}

		// Token: 0x04002975 RID: 10613
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(ObjectEnteringCellEvent), null, new MinEvent.EventPoolCount(ObjectEnteringCellEvent.CountPool), new MinEvent.EventPoolReset(ObjectEnteringCellEvent.ResetPool));

		// Token: 0x04002976 RID: 10614
		private static List<ObjectEnteringCellEvent> Pool;

		// Token: 0x04002977 RID: 10615
		private static int PoolCounter;
	}
}
