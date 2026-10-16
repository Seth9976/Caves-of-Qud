using System;

namespace XRL.World
{
	// Token: 0x020006BF RID: 1727
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class CanBeUnequippedEvent : PooledEvent<CanBeUnequippedEvent>
	{
		// Token: 0x0600493F RID: 18751 RVA: 0x0017116B File Offset: 0x0016F36B
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004940 RID: 18752 RVA: 0x00171174 File Offset: 0x0016F374
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Equipper = null;
			this.Actor = null;
			this.Forced = false;
			this.SemiForced = false;
		}

		// Token: 0x06004941 RID: 18753 RVA: 0x001711A0 File Offset: 0x0016F3A0
		public static bool Check(GameObject Object, GameObject Equipper = null, GameObject Actor = null, bool Forced = false, bool SemiForced = false)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("CanBeUnequipped"))
			{
				Event @event = Event.New("CanBeUnequipped", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Equipper", Equipper);
				@event.SetParameter("Actor", Actor);
				@event.SetFlag("Forced", Forced);
				@event.SetFlag("SemiForced", SemiForced);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<CanBeUnequippedEvent>.ID, MinEvent.CascadeLevel))
			{
				CanBeUnequippedEvent canBeUnequippedEvent = PooledEvent<CanBeUnequippedEvent>.FromPool();
				canBeUnequippedEvent.Object = Object;
				canBeUnequippedEvent.Equipper = Equipper;
				canBeUnequippedEvent.Actor = Actor;
				canBeUnequippedEvent.Forced = Forced;
				canBeUnequippedEvent.SemiForced = SemiForced;
				flag = Object.HandleEvent(canBeUnequippedEvent);
			}
			return flag;
		}

		// Token: 0x04002322 RID: 8994
		public GameObject Object;

		// Token: 0x04002323 RID: 8995
		public GameObject Equipper;

		// Token: 0x04002324 RID: 8996
		public GameObject Actor;

		// Token: 0x04002325 RID: 8997
		public bool Forced;

		// Token: 0x04002326 RID: 8998
		public bool SemiForced;
	}
}
