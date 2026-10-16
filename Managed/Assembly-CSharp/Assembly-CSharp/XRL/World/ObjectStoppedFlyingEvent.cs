using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000817 RID: 2071
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class ObjectStoppedFlyingEvent : IObjectCellInteractionEvent
	{
		// Token: 0x06005297 RID: 21143 RVA: 0x0018D431 File Offset: 0x0018B631
		public ObjectStoppedFlyingEvent()
		{
			this.ID = ObjectStoppedFlyingEvent.ID;
		}

		// Token: 0x06005298 RID: 21144 RVA: 0x0018D444 File Offset: 0x0018B644
		public static int CountPool()
		{
			if (ObjectStoppedFlyingEvent.Pool != null)
			{
				return ObjectStoppedFlyingEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06005299 RID: 21145 RVA: 0x0018D459 File Offset: 0x0018B659
		public static void ResetPool()
		{
			while (ObjectStoppedFlyingEvent.PoolCounter > 0)
			{
				ObjectStoppedFlyingEvent.Pool[--ObjectStoppedFlyingEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x0600529A RID: 21146 RVA: 0x0018D481 File Offset: 0x0018B681
		public static void ResetTo(ref ObjectStoppedFlyingEvent E)
		{
			MinEvent.ResetTo<ObjectStoppedFlyingEvent>(E, ObjectStoppedFlyingEvent.Pool, ref ObjectStoppedFlyingEvent.PoolCounter);
			E = null;
		}

		// Token: 0x0600529B RID: 21147 RVA: 0x0018D497 File Offset: 0x0018B697
		public static ObjectStoppedFlyingEvent FromPool()
		{
			return MinEvent.FromPool<ObjectStoppedFlyingEvent>(ref ObjectStoppedFlyingEvent.Pool, ref ObjectStoppedFlyingEvent.PoolCounter, 8192);
		}

		// Token: 0x0600529C RID: 21148 RVA: 0x0018D4AD File Offset: 0x0018B6AD
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x0600529D RID: 21149 RVA: 0x0018D4C4 File Offset: 0x0018B6C4
		public static bool SendFor(GameObject Object, GameObject Actor = null)
		{
			ObjectStoppedFlyingEvent objectStoppedFlyingEvent = ObjectStoppedFlyingEvent.FromPool();
			objectStoppedFlyingEvent.Object = Object;
			objectStoppedFlyingEvent.Cell = Object.CurrentCell;
			objectStoppedFlyingEvent.Forced = false;
			objectStoppedFlyingEvent.System = false;
			objectStoppedFlyingEvent.IgnoreGravity = false;
			objectStoppedFlyingEvent.NoStack = false;
			objectStoppedFlyingEvent.Direction = null;
			objectStoppedFlyingEvent.Type = null;
			objectStoppedFlyingEvent.Dragging = null;
			objectStoppedFlyingEvent.Actor = Actor ?? Object;
			objectStoppedFlyingEvent.ForceSwap = null;
			objectStoppedFlyingEvent.Ignore = null;
			return (!Object.WantEvent(ObjectStoppedFlyingEvent.ID, MinEvent.CascadeLevel) || Object.HandleEvent(objectStoppedFlyingEvent)) && (objectStoppedFlyingEvent.Cell == null || !objectStoppedFlyingEvent.Cell.WantEvent(ObjectStoppedFlyingEvent.ID, MinEvent.CascadeLevel) || objectStoppedFlyingEvent.Cell.HandleEvent(objectStoppedFlyingEvent));
		}

		// Token: 0x04002983 RID: 10627
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(ObjectStoppedFlyingEvent), null, new MinEvent.EventPoolCount(ObjectStoppedFlyingEvent.CountPool), new MinEvent.EventPoolReset(ObjectStoppedFlyingEvent.ResetPool));

		// Token: 0x04002984 RID: 10628
		private static List<ObjectStoppedFlyingEvent> Pool;

		// Token: 0x04002985 RID: 10629
		private static int PoolCounter;
	}
}
