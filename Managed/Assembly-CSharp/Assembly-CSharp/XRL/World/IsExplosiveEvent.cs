using System;

namespace XRL.World
{
	// Token: 0x020007EF RID: 2031
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class IsExplosiveEvent : PooledEvent<IsExplosiveEvent>
	{
		// Token: 0x06005189 RID: 20873 RVA: 0x00189F39 File Offset: 0x00188139
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600518A RID: 20874 RVA: 0x00189F42 File Offset: 0x00188142
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
		}

		// Token: 0x0600518B RID: 20875 RVA: 0x00189F54 File Offset: 0x00188154
		public static bool Check(GameObject Object)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("IsExplosive"))
			{
				Event @event = Event.New("IsExplosive", 0, 0, 0);
				@event.SetParameter("Object", Object);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<IsExplosiveEvent>.ID, MinEvent.CascadeLevel))
			{
				IsExplosiveEvent isExplosiveEvent = PooledEvent<IsExplosiveEvent>.FromPool();
				isExplosiveEvent.Object = Object;
				flag = Object.HandleEvent(isExplosiveEvent);
			}
			return !flag;
		}

		// Token: 0x040028E4 RID: 10468
		public GameObject Object;
	}
}
