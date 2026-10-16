using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000815 RID: 2069
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class ObjectLeavingCellEvent : IObjectCellInteractionEvent
	{
		// Token: 0x06005287 RID: 21127 RVA: 0x0018D184 File Offset: 0x0018B384
		public ObjectLeavingCellEvent()
		{
			this.ID = ObjectLeavingCellEvent.ID;
		}

		// Token: 0x06005288 RID: 21128 RVA: 0x0018D197 File Offset: 0x0018B397
		public static int CountPool()
		{
			if (ObjectLeavingCellEvent.Pool != null)
			{
				return ObjectLeavingCellEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06005289 RID: 21129 RVA: 0x0018D1AC File Offset: 0x0018B3AC
		public static void ResetPool()
		{
			while (ObjectLeavingCellEvent.PoolCounter > 0)
			{
				ObjectLeavingCellEvent.Pool[--ObjectLeavingCellEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x0600528A RID: 21130 RVA: 0x0018D1D4 File Offset: 0x0018B3D4
		public static void ResetTo(ref ObjectLeavingCellEvent E)
		{
			MinEvent.ResetTo<ObjectLeavingCellEvent>(E, ObjectLeavingCellEvent.Pool, ref ObjectLeavingCellEvent.PoolCounter);
			E = null;
		}

		// Token: 0x0600528B RID: 21131 RVA: 0x0018D1EA File Offset: 0x0018B3EA
		public static ObjectLeavingCellEvent FromPool()
		{
			return MinEvent.FromPool<ObjectLeavingCellEvent>(ref ObjectLeavingCellEvent.Pool, ref ObjectLeavingCellEvent.PoolCounter, 8192);
		}

		// Token: 0x0600528C RID: 21132 RVA: 0x0018D200 File Offset: 0x0018B400
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x0600528D RID: 21133 RVA: 0x0018D214 File Offset: 0x0018B414
		public static ObjectLeavingCellEvent FromPool(GameObject Object, Cell Cell, bool Forced = false, bool System = false, bool IgnoreGravity = false, bool NoStack = false, string Direction = null, string Type = null, GameObject Dragging = null, GameObject Actor = null, GameObject ForceSwap = null, GameObject Ignore = null)
		{
			ObjectLeavingCellEvent objectLeavingCellEvent = ObjectLeavingCellEvent.FromPool();
			objectLeavingCellEvent.Object = Object;
			objectLeavingCellEvent.Cell = Cell;
			objectLeavingCellEvent.Forced = Forced;
			objectLeavingCellEvent.System = System;
			objectLeavingCellEvent.IgnoreGravity = IgnoreGravity;
			objectLeavingCellEvent.NoStack = NoStack;
			objectLeavingCellEvent.Direction = Direction;
			objectLeavingCellEvent.Type = Type;
			objectLeavingCellEvent.Dragging = Dragging;
			objectLeavingCellEvent.Actor = Actor;
			objectLeavingCellEvent.ForceSwap = ForceSwap;
			objectLeavingCellEvent.Ignore = Ignore;
			return objectLeavingCellEvent;
		}

		// Token: 0x0400297D RID: 10621
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(ObjectLeavingCellEvent), null, new MinEvent.EventPoolCount(ObjectLeavingCellEvent.CountPool), new MinEvent.EventPoolReset(ObjectLeavingCellEvent.ResetPool));

		// Token: 0x0400297E RID: 10622
		private static List<ObjectLeavingCellEvent> Pool;

		// Token: 0x0400297F RID: 10623
		private static int PoolCounter;
	}
}
