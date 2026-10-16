using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000812 RID: 2066
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class ObjectEnteredCellEvent : IObjectCellInteractionEvent
	{
		// Token: 0x0600526E RID: 21102 RVA: 0x0018CD4D File Offset: 0x0018AF4D
		public ObjectEnteredCellEvent()
		{
			this.ID = ObjectEnteredCellEvent.ID;
		}

		// Token: 0x0600526F RID: 21103 RVA: 0x0018CD60 File Offset: 0x0018AF60
		public static int CountPool()
		{
			if (ObjectEnteredCellEvent.Pool != null)
			{
				return ObjectEnteredCellEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06005270 RID: 21104 RVA: 0x0018CD75 File Offset: 0x0018AF75
		public static void ResetPool()
		{
			while (ObjectEnteredCellEvent.PoolCounter > 0)
			{
				ObjectEnteredCellEvent.Pool[--ObjectEnteredCellEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06005271 RID: 21105 RVA: 0x0018CD9D File Offset: 0x0018AF9D
		public static void ResetTo(ref ObjectEnteredCellEvent E)
		{
			MinEvent.ResetTo<ObjectEnteredCellEvent>(E, ObjectEnteredCellEvent.Pool, ref ObjectEnteredCellEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06005272 RID: 21106 RVA: 0x0018CDB3 File Offset: 0x0018AFB3
		public static ObjectEnteredCellEvent FromPool()
		{
			return MinEvent.FromPool<ObjectEnteredCellEvent>(ref ObjectEnteredCellEvent.Pool, ref ObjectEnteredCellEvent.PoolCounter, 8192);
		}

		// Token: 0x06005273 RID: 21107 RVA: 0x0018CDC9 File Offset: 0x0018AFC9
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06005274 RID: 21108 RVA: 0x0018CDE0 File Offset: 0x0018AFE0
		public static ObjectEnteredCellEvent FromPool(GameObject Object, Cell Cell, bool Forced = false, bool System = false, bool IgnoreGravity = false, bool NoStack = false, string Direction = null, string Type = null, GameObject Dragging = null, GameObject Actor = null, GameObject ForceSwap = null, GameObject Ignore = null)
		{
			ObjectEnteredCellEvent objectEnteredCellEvent = ObjectEnteredCellEvent.FromPool();
			objectEnteredCellEvent.Object = Object;
			objectEnteredCellEvent.Cell = Cell;
			objectEnteredCellEvent.Forced = Forced;
			objectEnteredCellEvent.System = System;
			objectEnteredCellEvent.IgnoreGravity = IgnoreGravity;
			objectEnteredCellEvent.NoStack = NoStack;
			objectEnteredCellEvent.Direction = Direction;
			objectEnteredCellEvent.Type = Type;
			objectEnteredCellEvent.Dragging = Dragging;
			objectEnteredCellEvent.Actor = Actor;
			objectEnteredCellEvent.ForceSwap = ForceSwap;
			objectEnteredCellEvent.Ignore = Ignore;
			return objectEnteredCellEvent;
		}

		// Token: 0x04002972 RID: 10610
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(ObjectEnteredCellEvent), null, new MinEvent.EventPoolCount(ObjectEnteredCellEvent.CountPool), new MinEvent.EventPoolReset(ObjectEnteredCellEvent.ResetPool));

		// Token: 0x04002973 RID: 10611
		private static List<ObjectEnteredCellEvent> Pool;

		// Token: 0x04002974 RID: 10612
		private static int PoolCounter;
	}
}
