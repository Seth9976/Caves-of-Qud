using System;

namespace XRL.World
{
	// Token: 0x0200079C RID: 1948
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetTonicDosageEvent : PooledEvent<GetTonicDosageEvent>
	{
		// Token: 0x06004F78 RID: 20344 RVA: 0x00183FB3 File Offset: 0x001821B3
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004F79 RID: 20345 RVA: 0x00183FBC File Offset: 0x001821BC
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Subject = null;
			this.Actor = null;
			this.Dosage = 0;
		}

		// Token: 0x06004F7A RID: 20346 RVA: 0x00183FE0 File Offset: 0x001821E0
		public static int GetFor(GameObject Object, GameObject Subject = null, GameObject Actor = null, int BaseDosage = 1)
		{
			int num = BaseDosage;
			bool flag = true;
			bool flag2 = GameObject.Validate(ref Subject) && Subject.HasRegisteredEvent("GetTonicDosage");
			bool flag3 = GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GetTonicDosage");
			bool flag4 = GameObject.Validate(ref Subject) && Subject.WantEvent(PooledEvent<GetTonicDosageEvent>.ID, MinEvent.CascadeLevel);
			bool flag5 = GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetTonicDosageEvent>.ID, MinEvent.CascadeLevel);
			if (flag && (flag2 || flag3))
			{
				Event @event = Event.New("GetTonicDosage", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Subject", Subject);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Dosage", num);
				if (flag && flag2)
				{
					flag = Subject.FireEvent(@event);
				}
				if (flag && flag3)
				{
					flag = Object.FireEvent(@event);
				}
				num = @event.GetIntParameter("Dosage", 0);
			}
			if (flag && (flag4 || flag5))
			{
				GetTonicDosageEvent getTonicDosageEvent = PooledEvent<GetTonicDosageEvent>.FromPool();
				getTonicDosageEvent.Object = Object;
				getTonicDosageEvent.Subject = Subject;
				getTonicDosageEvent.Actor = Actor;
				getTonicDosageEvent.Dosage = num;
				if (flag && flag4)
				{
					flag = Subject.HandleEvent(getTonicDosageEvent);
				}
				if (flag && flag5)
				{
					flag = Object.HandleEvent(getTonicDosageEvent);
				}
				num = getTonicDosageEvent.Dosage;
			}
			return num;
		}

		// Token: 0x04002731 RID: 10033
		public GameObject Object;

		// Token: 0x04002732 RID: 10034
		public GameObject Subject;

		// Token: 0x04002733 RID: 10035
		public GameObject Actor;

		// Token: 0x04002734 RID: 10036
		public int Dosage;
	}
}
