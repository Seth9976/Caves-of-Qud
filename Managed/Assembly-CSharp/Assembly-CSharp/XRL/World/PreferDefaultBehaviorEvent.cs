using System;
using XRL.World.Anatomy;

namespace XRL.World
{
	// Token: 0x02000829 RID: 2089
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class PreferDefaultBehaviorEvent : PooledEvent<PreferDefaultBehaviorEvent>
	{
		// Token: 0x06005319 RID: 21273 RVA: 0x0018E958 File Offset: 0x0018CB58
		public override int GetCascadeLevel()
		{
			return PreferDefaultBehaviorEvent.CascadeLevel;
		}

		// Token: 0x0600531A RID: 21274 RVA: 0x0018E95F File Offset: 0x0018CB5F
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600531B RID: 21275 RVA: 0x0018E968 File Offset: 0x0018CB68
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Target = null;
			this.Part = null;
		}

		// Token: 0x0600531C RID: 21276 RVA: 0x0018E988 File Offset: 0x0018CB88
		public static bool Check(GameObject Actor, GameObject Target, BodyPart Part)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("PreferDefaultBehavior"))
			{
				Event @event = Event.New("PreferDefaultBehavior", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Target", Target);
				@event.SetParameter("Part", Part);
				flag = Actor.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<PreferDefaultBehaviorEvent>.ID, PreferDefaultBehaviorEvent.CascadeLevel))
			{
				PreferDefaultBehaviorEvent preferDefaultBehaviorEvent = PooledEvent<PreferDefaultBehaviorEvent>.FromPool();
				preferDefaultBehaviorEvent.Actor = Actor;
				preferDefaultBehaviorEvent.Target = Target;
				preferDefaultBehaviorEvent.Part = Part;
				flag = Actor.HandleEvent(preferDefaultBehaviorEvent);
			}
			return !flag;
		}

		// Token: 0x040029CF RID: 10703
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040029D0 RID: 10704
		public GameObject Actor;

		// Token: 0x040029D1 RID: 10705
		public GameObject Target;

		// Token: 0x040029D2 RID: 10706
		public BodyPart Part;
	}
}
