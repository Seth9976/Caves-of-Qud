using System;

namespace XRL.World
{
	// Token: 0x02000751 RID: 1873
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetGenderEvent : PooledEvent<GetGenderEvent>
	{
		// Token: 0x06004D68 RID: 19816 RVA: 0x0017CE5A File Offset: 0x0017B05A
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004D69 RID: 19817 RVA: 0x0017CE63 File Offset: 0x0017B063
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Name = null;
			this.AsIfKnown = false;
		}

		// Token: 0x06004D6A RID: 19818 RVA: 0x0017CE80 File Offset: 0x0017B080
		public static string GetFor(GameObject Object, string Name, bool AsIfKnown = false)
		{
			if (Object.HasRegisteredEvent("GetGender"))
			{
				Event @event = Event.New("GetGender", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Name", Name);
				@event.SetFlag("AsIfKnown", AsIfKnown);
				Object.FireEvent(@event);
				Name = @event.GetStringParameter("Name", null);
			}
			if (Object.WantEvent(PooledEvent<GetGenderEvent>.ID, MinEvent.CascadeLevel))
			{
				GetGenderEvent getGenderEvent = PooledEvent<GetGenderEvent>.FromPool();
				getGenderEvent.Object = Object;
				getGenderEvent.Name = Name;
				getGenderEvent.AsIfKnown = AsIfKnown;
				Object.HandleEvent(getGenderEvent);
				Name = getGenderEvent.Name;
			}
			return Name;
		}

		// Token: 0x04002594 RID: 9620
		public GameObject Object;

		// Token: 0x04002595 RID: 9621
		public string Name;

		// Token: 0x04002596 RID: 9622
		public bool AsIfKnown;
	}
}
