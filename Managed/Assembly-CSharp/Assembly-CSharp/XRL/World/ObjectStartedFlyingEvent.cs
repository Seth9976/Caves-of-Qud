using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000816 RID: 2070
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class ObjectStartedFlyingEvent : IObjectCellInteractionEvent
	{
		// Token: 0x0600528F RID: 21135 RVA: 0x0018D2B1 File Offset: 0x0018B4B1
		public ObjectStartedFlyingEvent()
		{
			this.ID = ObjectStartedFlyingEvent.ID;
		}

		// Token: 0x06005290 RID: 21136 RVA: 0x0018D2C4 File Offset: 0x0018B4C4
		public static int CountPool()
		{
			if (ObjectStartedFlyingEvent.Pool != null)
			{
				return ObjectStartedFlyingEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06005291 RID: 21137 RVA: 0x0018D2D9 File Offset: 0x0018B4D9
		public static void ResetPool()
		{
			while (ObjectStartedFlyingEvent.PoolCounter > 0)
			{
				ObjectStartedFlyingEvent.Pool[--ObjectStartedFlyingEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06005292 RID: 21138 RVA: 0x0018D301 File Offset: 0x0018B501
		public static void ResetTo(ref ObjectStartedFlyingEvent E)
		{
			MinEvent.ResetTo<ObjectStartedFlyingEvent>(E, ObjectStartedFlyingEvent.Pool, ref ObjectStartedFlyingEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06005293 RID: 21139 RVA: 0x0018D317 File Offset: 0x0018B517
		public static ObjectStartedFlyingEvent FromPool()
		{
			return MinEvent.FromPool<ObjectStartedFlyingEvent>(ref ObjectStartedFlyingEvent.Pool, ref ObjectStartedFlyingEvent.PoolCounter, 8192);
		}

		// Token: 0x06005294 RID: 21140 RVA: 0x0018D32D File Offset: 0x0018B52D
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06005295 RID: 21141 RVA: 0x0018D344 File Offset: 0x0018B544
		public static bool SendFor(GameObject Object, GameObject Actor = null)
		{
			ObjectStartedFlyingEvent objectStartedFlyingEvent = ObjectStartedFlyingEvent.FromPool();
			objectStartedFlyingEvent.Object = Object;
			objectStartedFlyingEvent.Cell = Object.CurrentCell;
			objectStartedFlyingEvent.Forced = false;
			objectStartedFlyingEvent.System = false;
			objectStartedFlyingEvent.IgnoreGravity = false;
			objectStartedFlyingEvent.NoStack = false;
			objectStartedFlyingEvent.Direction = null;
			objectStartedFlyingEvent.Type = null;
			objectStartedFlyingEvent.Dragging = null;
			objectStartedFlyingEvent.Actor = Actor ?? Object;
			objectStartedFlyingEvent.ForceSwap = null;
			objectStartedFlyingEvent.Ignore = null;
			return (!Object.WantEvent(ObjectStartedFlyingEvent.ID, MinEvent.CascadeLevel) || Object.HandleEvent(objectStartedFlyingEvent)) && (objectStartedFlyingEvent.Cell == null || !objectStartedFlyingEvent.Cell.WantEvent(ObjectStartedFlyingEvent.ID, MinEvent.CascadeLevel) || objectStartedFlyingEvent.Cell.HandleEvent(objectStartedFlyingEvent));
		}

		// Token: 0x04002980 RID: 10624
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(ObjectStartedFlyingEvent), null, new MinEvent.EventPoolCount(ObjectStartedFlyingEvent.CountPool), new MinEvent.EventPoolReset(ObjectStartedFlyingEvent.ResetPool));

		// Token: 0x04002981 RID: 10625
		private static List<ObjectStartedFlyingEvent> Pool;

		// Token: 0x04002982 RID: 10626
		private static int PoolCounter;
	}
}
