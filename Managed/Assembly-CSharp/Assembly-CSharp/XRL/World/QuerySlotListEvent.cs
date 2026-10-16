using System;
using System.Collections.Generic;
using XRL.World.Anatomy;

namespace XRL.World
{
	// Token: 0x02000837 RID: 2103
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class QuerySlotListEvent : PooledEvent<QuerySlotListEvent>
	{
		// Token: 0x060053A3 RID: 21411 RVA: 0x00190088 File Offset: 0x0018E288
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060053A4 RID: 21412 RVA: 0x00190091 File Offset: 0x0018E291
		public override void Reset()
		{
			base.Reset();
			this.Subject = null;
			this.Object = null;
			this.SlotList.Clear();
			this.FailureMessage = null;
		}

		// Token: 0x060053A5 RID: 21413 RVA: 0x001900BC File Offset: 0x0018E2BC
		public static List<BodyPart> GetFor(GameObject Subject, GameObject Object, ref string FailureMessage)
		{
			bool flag = true;
			QuerySlotListEvent querySlotListEvent = PooledEvent<QuerySlotListEvent>.FromPool();
			querySlotListEvent.Subject = Subject;
			querySlotListEvent.Object = Object;
			querySlotListEvent.SlotList.Clear();
			querySlotListEvent.FailureMessage = FailureMessage;
			if (flag && GameObject.Validate(ref Subject) && Subject.HasRegisteredEvent("QuerySlotList"))
			{
				Event @event = Event.New("QuerySlotList", 0, 0, 0);
				@event.SetParameter("Subject", Subject);
				@event.SetParameter("Object", Object);
				@event.SetParameter("SlotList", querySlotListEvent.SlotList);
				@event.SetParameter("FailureMessage", FailureMessage);
				flag = Subject.FireEvent(@event);
				FailureMessage = @event.GetStringParameter("FailureMessage", null);
				querySlotListEvent.FailureMessage = FailureMessage;
			}
			if (flag && GameObject.Validate(ref Subject) && Subject.WantEvent(PooledEvent<QuerySlotListEvent>.ID, MinEvent.CascadeLevel))
			{
				flag = Subject.HandleEvent(querySlotListEvent);
				FailureMessage = querySlotListEvent.FailureMessage;
			}
			return querySlotListEvent.SlotList;
		}

		// Token: 0x060053A6 RID: 21414 RVA: 0x001901A8 File Offset: 0x0018E3A8
		public static List<BodyPart> GetFor(GameObject Subject, GameObject Object)
		{
			string text = null;
			return QuerySlotListEvent.GetFor(Subject, Object, ref text);
		}

		// Token: 0x04002A10 RID: 10768
		public GameObject Subject;

		// Token: 0x04002A11 RID: 10769
		public GameObject Object;

		// Token: 0x04002A12 RID: 10770
		public List<BodyPart> SlotList = new List<BodyPart>();

		// Token: 0x04002A13 RID: 10771
		public string FailureMessage;
	}
}
