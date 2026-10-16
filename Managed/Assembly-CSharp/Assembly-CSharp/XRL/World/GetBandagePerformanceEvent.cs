using System;

namespace XRL.World
{
	// Token: 0x0200072D RID: 1837
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class GetBandagePerformanceEvent : PooledEvent<GetBandagePerformanceEvent>
	{
		// Token: 0x06004C38 RID: 19512 RVA: 0x00179194 File Offset: 0x00177394
		public override int GetCascadeLevel()
		{
			return GetBandagePerformanceEvent.CascadeLevel;
		}

		// Token: 0x06004C39 RID: 19513 RVA: 0x0017919B File Offset: 0x0017739B
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004C3A RID: 19514 RVA: 0x001791A4 File Offset: 0x001773A4
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Actor = null;
			this.Subject = null;
			this.Checking = null;
			this.BasePerformance = 0;
			this.Performance = 0;
			this.Pass = 0;
		}

		// Token: 0x06004C3B RID: 19515 RVA: 0x001791E0 File Offset: 0x001773E0
		public static int GetFor(GameObject Object, GameObject Actor, GameObject Subject, int BasePerformance)
		{
			int num = BasePerformance;
			bool flag = true;
			Event @event = null;
			GetBandagePerformanceEvent getBandagePerformanceEvent = null;
			bool flag2 = GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GetBandagePerformance");
			bool flag3 = GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("GetBandagePerformance");
			bool flag4 = GameObject.Validate(ref Subject) && Subject.HasRegisteredEvent("GetBandagePerformance");
			bool flag5 = GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetBandagePerformanceEvent>.ID, GetBandagePerformanceEvent.CascadeLevel);
			bool flag6 = GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<GetBandagePerformanceEvent>.ID, GetBandagePerformanceEvent.CascadeLevel);
			bool flag7 = GameObject.Validate(ref Subject) && Subject.WantEvent(PooledEvent<GetBandagePerformanceEvent>.ID, GetBandagePerformanceEvent.CascadeLevel);
			if (flag2 || flag3 || flag4 || flag5 || flag6 || flag7)
			{
				bool flag8 = true;
				int num2 = 1;
				while (flag && flag8 && num2 <= GetBandagePerformanceEvent.PASSES)
				{
					flag8 = false;
					if (flag && (flag2 || flag3 || flag4))
					{
						if (@event == null)
						{
							@event = Event.New("GetBandagePerformance", 0, 0, 0);
							@event.SetParameter("Object", Object);
							@event.SetParameter("Actor", Actor);
							@event.SetParameter("Subject", Subject);
							@event.SetParameter("BasePerformance", BasePerformance);
							@event.SetParameter("Pass", num2);
						}
						@event.SetParameter("Performance", num);
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
						num = @event.GetIntParameter("Performance", 0);
					}
					if (flag && (flag5 || flag6 || flag7))
					{
						if (getBandagePerformanceEvent == null)
						{
							getBandagePerformanceEvent = PooledEvent<GetBandagePerformanceEvent>.FromPool();
						}
						getBandagePerformanceEvent.Object = Object;
						getBandagePerformanceEvent.Actor = Actor;
						getBandagePerformanceEvent.Subject = Subject;
						getBandagePerformanceEvent.BasePerformance = BasePerformance;
						getBandagePerformanceEvent.Performance = num;
						getBandagePerformanceEvent.Pass = num2;
						if (flag && flag5 && GameObject.Validate(ref Object))
						{
							getBandagePerformanceEvent.Checking = "Object";
							flag = Object.HandleEvent(getBandagePerformanceEvent);
							flag8 = true;
						}
						if (flag && flag6 && GameObject.Validate(ref Actor))
						{
							getBandagePerformanceEvent.Checking = "Actor";
							flag = Actor.HandleEvent(getBandagePerformanceEvent);
							flag8 = true;
						}
						if (flag && flag7 && GameObject.Validate(ref Subject))
						{
							getBandagePerformanceEvent.Checking = "Subject";
							flag = Subject.HandleEvent(getBandagePerformanceEvent);
							flag8 = true;
						}
						num = getBandagePerformanceEvent.Performance;
					}
					num2++;
				}
			}
			return num;
		}

		// Token: 0x040024D8 RID: 9432
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040024D9 RID: 9433
		public static readonly int PASSES = 3;

		// Token: 0x040024DA RID: 9434
		public GameObject Object;

		// Token: 0x040024DB RID: 9435
		public GameObject Actor;

		// Token: 0x040024DC RID: 9436
		public GameObject Subject;

		// Token: 0x040024DD RID: 9437
		public string Checking;

		// Token: 0x040024DE RID: 9438
		public int BasePerformance;

		// Token: 0x040024DF RID: 9439
		public int Performance;

		// Token: 0x040024E0 RID: 9440
		public int Pass;
	}
}
