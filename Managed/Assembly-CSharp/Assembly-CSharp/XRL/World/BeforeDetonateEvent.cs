using System;

namespace XRL.World
{
	// Token: 0x0200068F RID: 1679
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class BeforeDetonateEvent : PooledEvent<BeforeDetonateEvent>
	{
		// Token: 0x060047DB RID: 18395 RVA: 0x0016AB4F File Offset: 0x00168D4F
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060047DC RID: 18396 RVA: 0x0016AB58 File Offset: 0x00168D58
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Actor = null;
			this.ApparentTarget = null;
			this.Indirect = false;
		}

		// Token: 0x060047DD RID: 18397 RVA: 0x0016AB7C File Offset: 0x00168D7C
		public static bool Check(GameObject Object, GameObject Actor = null, GameObject ApparentTarget = null, bool Indirect = false)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("BeforeDetonate"))
			{
				Event @event = Event.New("BeforeDetonate", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("ApparentTarget", ApparentTarget);
				@event.SetFlag("Indirect", Indirect);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<BeforeDetonateEvent>.ID, MinEvent.CascadeLevel))
			{
				BeforeDetonateEvent beforeDetonateEvent = PooledEvent<BeforeDetonateEvent>.FromPool();
				beforeDetonateEvent.Object = Object;
				beforeDetonateEvent.Actor = Actor;
				beforeDetonateEvent.ApparentTarget = ApparentTarget;
				beforeDetonateEvent.Indirect = Indirect;
				flag = Object.HandleEvent(beforeDetonateEvent);
			}
			return flag;
		}

		// Token: 0x0400223E RID: 8766
		public GameObject Object;

		// Token: 0x0400223F RID: 8767
		public GameObject Actor;

		// Token: 0x04002240 RID: 8768
		public GameObject ApparentTarget;

		// Token: 0x04002241 RID: 8769
		public bool Indirect;
	}
}
