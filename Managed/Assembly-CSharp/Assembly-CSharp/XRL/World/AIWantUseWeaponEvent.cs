using System;

namespace XRL.World
{
	// Token: 0x0200064B RID: 1611
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AIWantUseWeaponEvent : PooledEvent<AIWantUseWeaponEvent>
	{
		// Token: 0x060045D9 RID: 17881 RVA: 0x00165242 File Offset: 0x00163442
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060045DA RID: 17882 RVA: 0x0016524B File Offset: 0x0016344B
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Object = null;
			this.Target = null;
		}

		// Token: 0x060045DB RID: 17883 RVA: 0x00165268 File Offset: 0x00163468
		public static bool Check(GameObject Object, GameObject Actor, GameObject Target)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("AIWantUseWeapon"))
			{
				Event @event = Event.New("AIWantUseWeapon", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Target", Target);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<AIWantUseWeaponEvent>.ID, MinEvent.CascadeLevel))
			{
				AIWantUseWeaponEvent aiwantUseWeaponEvent = PooledEvent<AIWantUseWeaponEvent>.FromPool();
				aiwantUseWeaponEvent.Object = Object;
				aiwantUseWeaponEvent.Actor = Actor;
				aiwantUseWeaponEvent.Target = Target;
				flag = Object.HandleEvent(aiwantUseWeaponEvent);
			}
			return flag;
		}

		// Token: 0x04002129 RID: 8489
		public GameObject Actor;

		// Token: 0x0400212A RID: 8490
		public GameObject Object;

		// Token: 0x0400212B RID: 8491
		public GameObject Target;
	}
}
