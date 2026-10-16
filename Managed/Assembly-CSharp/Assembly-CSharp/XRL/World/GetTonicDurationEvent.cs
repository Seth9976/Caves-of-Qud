using System;

namespace XRL.World
{
	// Token: 0x0200079D RID: 1949
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class GetTonicDurationEvent : PooledEvent<GetTonicDurationEvent>
	{
		// Token: 0x06004F7C RID: 20348 RVA: 0x00184136 File Offset: 0x00182336
		public override int GetCascadeLevel()
		{
			return GetTonicDurationEvent.CascadeLevel;
		}

		// Token: 0x06004F7D RID: 20349 RVA: 0x0018413D File Offset: 0x0018233D
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004F7E RID: 20350 RVA: 0x00184148 File Offset: 0x00182348
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Actor = null;
			this.Subject = null;
			this.Type = null;
			this.Checking = null;
			this.BaseDuration = 0;
			this.Duration = 0;
			this.Dosage = 0;
			this.Pass = 0;
			this.Healing = false;
		}

		// Token: 0x06004F7F RID: 20351 RVA: 0x001841A4 File Offset: 0x001823A4
		public static int GetFor(GameObject Object, GameObject Actor, GameObject Subject, string Type, int BaseDuration, int Dosage = 1, bool Healing = false)
		{
			int num = BaseDuration;
			bool flag = true;
			Event @event = null;
			GetTonicDurationEvent getTonicDurationEvent = null;
			bool flag2 = GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GetTonicDuration");
			bool flag3 = GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("GetTonicDuration");
			bool flag4 = GameObject.Validate(ref Subject) && Subject.HasRegisteredEvent("GetTonicDuration");
			bool flag5 = GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetTonicDurationEvent>.ID, GetTonicDurationEvent.CascadeLevel);
			bool flag6 = GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<GetTonicDurationEvent>.ID, GetTonicDurationEvent.CascadeLevel);
			bool flag7 = GameObject.Validate(ref Subject) && Subject.WantEvent(PooledEvent<GetTonicDurationEvent>.ID, GetTonicDurationEvent.CascadeLevel);
			if (flag2 || flag3 || flag4 || flag5 || flag6 || flag7)
			{
				bool flag8 = true;
				int num2 = 1;
				while (flag && flag8 && num2 <= GetTonicDurationEvent.PASSES)
				{
					flag8 = false;
					if (flag && (flag2 || flag3 || flag4))
					{
						if (@event == null)
						{
							@event = Event.New("GetTonicDuration", 0, 0, 0);
							@event.SetParameter("Object", Object);
							@event.SetParameter("Actor", Actor);
							@event.SetParameter("Subject", Subject);
							@event.SetParameter("Type", Type);
							@event.SetParameter("BaseDuration", BaseDuration);
							@event.SetParameter("Dosage", Dosage);
							@event.SetParameter("Pass", num2);
							@event.SetFlag("Healing", Healing);
						}
						@event.SetParameter("Duration", num);
						if (flag && flag2 && GameObject.Validate(ref Object))
						{
							@event.SetParameter("Checking", "Object");
							flag = Object.FireEvent(@event);
							flag8 = true;
						}
						if (flag && flag3 && GameObject.Validate(ref Actor))
						{
							@event.SetParameter("Checking", "Actor");
							flag = Actor.FireEvent(@event);
							flag8 = true;
						}
						if (flag && flag4 && GameObject.Validate(ref Subject))
						{
							@event.SetParameter("Checking", "Subject");
							flag = Subject.FireEvent(@event);
							flag8 = true;
						}
						num = @event.GetIntParameter("Duration", 0);
					}
					if (flag && (flag5 || flag6 || flag7))
					{
						if (getTonicDurationEvent == null)
						{
							getTonicDurationEvent = PooledEvent<GetTonicDurationEvent>.FromPool();
						}
						getTonicDurationEvent.Object = Object;
						getTonicDurationEvent.Actor = Actor;
						getTonicDurationEvent.Subject = Subject;
						getTonicDurationEvent.Type = Type;
						getTonicDurationEvent.BaseDuration = BaseDuration;
						getTonicDurationEvent.Duration = num;
						getTonicDurationEvent.Dosage = Dosage;
						getTonicDurationEvent.Pass = num2;
						getTonicDurationEvent.Healing = Healing;
						if (flag && flag5 && GameObject.Validate(ref Object))
						{
							getTonicDurationEvent.Checking = "Object";
							flag = Object.HandleEvent(getTonicDurationEvent);
							flag8 = true;
						}
						if (flag && flag6 && GameObject.Validate(ref Actor))
						{
							getTonicDurationEvent.Checking = "Actor";
							flag = Actor.HandleEvent(getTonicDurationEvent);
							flag8 = true;
						}
						if (flag && flag7 && GameObject.Validate(ref Subject))
						{
							getTonicDurationEvent.Checking = "Subject";
							flag = Subject.HandleEvent(getTonicDurationEvent);
							flag8 = true;
						}
						num = getTonicDurationEvent.Duration;
					}
					num2++;
				}
			}
			return num;
		}

		// Token: 0x04002735 RID: 10037
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002736 RID: 10038
		public static readonly int PASSES = 3;

		// Token: 0x04002737 RID: 10039
		public GameObject Object;

		// Token: 0x04002738 RID: 10040
		public GameObject Actor;

		// Token: 0x04002739 RID: 10041
		public GameObject Subject;

		// Token: 0x0400273A RID: 10042
		public string Type;

		// Token: 0x0400273B RID: 10043
		public string Checking;

		// Token: 0x0400273C RID: 10044
		public int BaseDuration;

		// Token: 0x0400273D RID: 10045
		public int Duration;

		// Token: 0x0400273E RID: 10046
		public int Dosage;

		// Token: 0x0400273F RID: 10047
		public int Pass;

		// Token: 0x04002740 RID: 10048
		public bool Healing;
	}
}
