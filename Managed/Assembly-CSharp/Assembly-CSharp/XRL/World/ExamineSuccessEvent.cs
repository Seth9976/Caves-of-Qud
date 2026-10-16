using System;

namespace XRL.World
{
	// Token: 0x02000710 RID: 1808
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Pool)]
	public class ExamineSuccessEvent : PooledEvent<ExamineSuccessEvent>
	{
		// Token: 0x06004B66 RID: 19302 RVA: 0x00176C14 File Offset: 0x00174E14
		public override int GetCascadeLevel()
		{
			return ExamineSuccessEvent.CascadeLevel;
		}

		// Token: 0x06004B67 RID: 19303 RVA: 0x00176C1B File Offset: 0x00174E1B
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004B68 RID: 19304 RVA: 0x00176C24 File Offset: 0x00174E24
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Actor = null;
			this.Complete = false;
		}

		// Token: 0x06004B69 RID: 19305 RVA: 0x00176C44 File Offset: 0x00174E44
		public static void Send(GameObject Object, GameObject Actor, bool Complete = false)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("ExamineSuccess"))
			{
				Event @event = Event.New("ExamineSuccess", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Actor", Actor);
				@event.SetFlag("Complete", Complete);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("ExamineSuccess"))
			{
				Event event2 = Event.New("ExamineSuccess", 0, 0, 0);
				event2.SetParameter("Object", Object);
				event2.SetParameter("Actor", Actor);
				event2.SetFlag("Complete", Complete);
				flag = Actor.FireEvent(event2);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<ExamineSuccessEvent>.ID, ExamineSuccessEvent.CascadeLevel))
			{
				ExamineSuccessEvent examineSuccessEvent = PooledEvent<ExamineSuccessEvent>.FromPool();
				examineSuccessEvent.Object = Object;
				examineSuccessEvent.Actor = Actor;
				examineSuccessEvent.Complete = Complete;
				flag = Object.HandleEvent(examineSuccessEvent);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<ExamineSuccessEvent>.ID, ExamineSuccessEvent.CascadeLevel))
			{
				ExamineSuccessEvent examineSuccessEvent2 = PooledEvent<ExamineSuccessEvent>.FromPool();
				examineSuccessEvent2.Object = Object;
				examineSuccessEvent2.Actor = Actor;
				examineSuccessEvent2.Complete = Complete;
				flag = Actor.HandleEvent(examineSuccessEvent2);
			}
		}

		// Token: 0x04002461 RID: 9313
		public new static readonly int CascadeLevel = 15;

		// Token: 0x04002462 RID: 9314
		public GameObject Object;

		// Token: 0x04002463 RID: 9315
		public GameObject Actor;

		// Token: 0x04002464 RID: 9316
		public bool Complete;
	}
}
