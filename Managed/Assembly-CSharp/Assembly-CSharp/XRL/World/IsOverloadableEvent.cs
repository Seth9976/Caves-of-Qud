using System;

namespace XRL.World
{
	// Token: 0x020007F1 RID: 2033
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class IsOverloadableEvent : PooledEvent<IsOverloadableEvent>
	{
		// Token: 0x06005191 RID: 20881 RVA: 0x0018A112 File Offset: 0x00188312
		public override int GetCascadeLevel()
		{
			return IsOverloadableEvent.CascadeLevel;
		}

		// Token: 0x06005192 RID: 20882 RVA: 0x0018A119 File Offset: 0x00188319
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005193 RID: 20883 RVA: 0x0018A122 File Offset: 0x00188322
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
		}

		// Token: 0x06005194 RID: 20884 RVA: 0x0018A134 File Offset: 0x00188334
		public static bool Check(GameObject Object)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("IsOverloadable"))
			{
				Event @event = Event.New("IsOverloadable", 0, 0, 0);
				@event.SetParameter("Object", Object);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<IsOverloadableEvent>.ID, IsOverloadableEvent.CascadeLevel))
			{
				IsOverloadableEvent isOverloadableEvent = PooledEvent<IsOverloadableEvent>.FromPool();
				isOverloadableEvent.Object = Object;
				flag = Object.HandleEvent(isOverloadableEvent);
			}
			return !flag;
		}

		// Token: 0x040028E7 RID: 10471
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040028E8 RID: 10472
		public GameObject Object;
	}
}
