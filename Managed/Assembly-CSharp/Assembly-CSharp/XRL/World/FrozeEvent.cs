using System;

namespace XRL.World
{
	// Token: 0x02000719 RID: 1817
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class FrozeEvent : PooledEvent<FrozeEvent>
	{
		// Token: 0x06004BAE RID: 19374 RVA: 0x0017784F File Offset: 0x00175A4F
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004BAF RID: 19375 RVA: 0x00177858 File Offset: 0x00175A58
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.By = null;
		}

		// Token: 0x06004BB0 RID: 19376 RVA: 0x00177870 File Offset: 0x00175A70
		public static bool Check(GameObject Object, GameObject By)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("Froze"))
			{
				Event @event = Event.New("Froze", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("By", By);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<FrozeEvent>.ID, MinEvent.CascadeLevel))
			{
				FrozeEvent frozeEvent = PooledEvent<FrozeEvent>.FromPool();
				frozeEvent.Object = Object;
				frozeEvent.By = By;
				flag = Object.HandleEvent(frozeEvent);
			}
			return flag;
		}

		// Token: 0x04002480 RID: 9344
		public GameObject Object;

		// Token: 0x04002481 RID: 9345
		public GameObject By;
	}
}
