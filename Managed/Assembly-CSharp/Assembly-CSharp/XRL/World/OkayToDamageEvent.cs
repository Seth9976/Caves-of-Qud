using System;

namespace XRL.World
{
	// Token: 0x02000818 RID: 2072
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class OkayToDamageEvent : PooledEvent<OkayToDamageEvent>
	{
		// Token: 0x0600529F RID: 21151 RVA: 0x0018D5B1 File Offset: 0x0018B7B1
		public override int GetCascadeLevel()
		{
			return OkayToDamageEvent.CascadeLevel;
		}

		// Token: 0x060052A0 RID: 21152 RVA: 0x0018D5B8 File Offset: 0x0018B7B8
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060052A1 RID: 21153 RVA: 0x0018D5C1 File Offset: 0x0018B7C1
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Object = null;
		}

		// Token: 0x060052A2 RID: 21154 RVA: 0x0018D5D8 File Offset: 0x0018B7D8
		public static bool Check(GameObject Object, GameObject Actor, out bool WasWanted)
		{
			WasWanted = false;
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("OkayToDamage"))
			{
				WasWanted = true;
				Event @event = Event.New("OkayToDamage", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Object", Object);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<OkayToDamageEvent>.ID, OkayToDamageEvent.CascadeLevel))
			{
				WasWanted = true;
				OkayToDamageEvent okayToDamageEvent = PooledEvent<OkayToDamageEvent>.FromPool();
				okayToDamageEvent.Actor = Actor;
				okayToDamageEvent.Object = Object;
				flag = Object.HandleEvent(okayToDamageEvent);
			}
			return flag;
		}

		// Token: 0x060052A3 RID: 21155 RVA: 0x0018D674 File Offset: 0x0018B874
		public static bool Check(GameObject Object, GameObject Actor)
		{
			bool flag;
			return OkayToDamageEvent.Check(Object, Actor, out flag);
		}

		// Token: 0x04002986 RID: 10630
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002987 RID: 10631
		public GameObject Actor;

		// Token: 0x04002988 RID: 10632
		public GameObject Object;
	}
}
