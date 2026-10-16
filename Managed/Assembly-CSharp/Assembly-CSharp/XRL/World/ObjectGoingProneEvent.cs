using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000814 RID: 2068
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class ObjectGoingProneEvent : IObjectCellInteractionEvent
	{
		// Token: 0x0600527E RID: 21118 RVA: 0x0018CFAD File Offset: 0x0018B1AD
		public ObjectGoingProneEvent()
		{
			this.ID = ObjectGoingProneEvent.ID;
		}

		// Token: 0x0600527F RID: 21119 RVA: 0x0018CFC0 File Offset: 0x0018B1C0
		public static int CountPool()
		{
			if (ObjectGoingProneEvent.Pool != null)
			{
				return ObjectGoingProneEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06005280 RID: 21120 RVA: 0x0018CFD5 File Offset: 0x0018B1D5
		public static void ResetPool()
		{
			while (ObjectGoingProneEvent.PoolCounter > 0)
			{
				ObjectGoingProneEvent.Pool[--ObjectGoingProneEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06005281 RID: 21121 RVA: 0x0018CFFD File Offset: 0x0018B1FD
		public static void ResetTo(ref ObjectGoingProneEvent E)
		{
			MinEvent.ResetTo<ObjectGoingProneEvent>(E, ObjectGoingProneEvent.Pool, ref ObjectGoingProneEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06005282 RID: 21122 RVA: 0x0018D013 File Offset: 0x0018B213
		public static ObjectGoingProneEvent FromPool()
		{
			return MinEvent.FromPool<ObjectGoingProneEvent>(ref ObjectGoingProneEvent.Pool, ref ObjectGoingProneEvent.PoolCounter, 8192);
		}

		// Token: 0x06005283 RID: 21123 RVA: 0x0018D029 File Offset: 0x0018B229
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06005284 RID: 21124 RVA: 0x0018D03D File Offset: 0x0018B23D
		public override void Reset()
		{
			base.Reset();
			this.Voluntary = false;
			this.UsePopups = false;
		}

		// Token: 0x06005285 RID: 21125 RVA: 0x0018D054 File Offset: 0x0018B254
		public static void Send(GameObject Object, Cell Cell, bool Voluntary = false, bool UsePopups = false)
		{
			bool flag = true;
			if (flag && Cell != null && Cell.HasObjectWithRegisteredEvent("ObjectGoingProne"))
			{
				Event @event = Event.New("ObjectGoingProne", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Cell", Cell);
				@event.SetFlag("Voluntary", Voluntary);
				@event.SetFlag("UsePopups", UsePopups);
				flag = Cell.FireEvent(@event);
			}
			if (flag && Cell != null && Cell.WantEvent(ObjectGoingProneEvent.ID, MinEvent.CascadeLevel))
			{
				ObjectGoingProneEvent objectGoingProneEvent = ObjectGoingProneEvent.FromPool();
				objectGoingProneEvent.Object = Object;
				objectGoingProneEvent.Cell = Cell;
				objectGoingProneEvent.Forced = !Voluntary;
				objectGoingProneEvent.System = false;
				objectGoingProneEvent.IgnoreGravity = false;
				objectGoingProneEvent.NoStack = false;
				objectGoingProneEvent.Direction = ".";
				objectGoingProneEvent.Type = null;
				objectGoingProneEvent.Dragging = null;
				objectGoingProneEvent.Actor = null;
				objectGoingProneEvent.ForceSwap = null;
				objectGoingProneEvent.Ignore = null;
				objectGoingProneEvent.Voluntary = Voluntary;
				objectGoingProneEvent.UsePopups = UsePopups;
				flag = Cell.HandleEvent(objectGoingProneEvent);
			}
		}

		// Token: 0x04002978 RID: 10616
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(ObjectGoingProneEvent), null, new MinEvent.EventPoolCount(ObjectGoingProneEvent.CountPool), new MinEvent.EventPoolReset(ObjectGoingProneEvent.ResetPool));

		// Token: 0x04002979 RID: 10617
		private static List<ObjectGoingProneEvent> Pool;

		// Token: 0x0400297A RID: 10618
		private static int PoolCounter;

		// Token: 0x0400297B RID: 10619
		public bool Voluntary;

		// Token: 0x0400297C RID: 10620
		public bool UsePopups;
	}
}
