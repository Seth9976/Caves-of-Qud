using System;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x0200086E RID: 2158
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class TriggersMakersMarkCreationEvent : PooledEvent<TriggersMakersMarkCreationEvent>
	{
		// Token: 0x060055C8 RID: 21960 RVA: 0x00199708 File Offset: 0x00197908
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060055C9 RID: 21961 RVA: 0x00199711 File Offset: 0x00197911
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Actor = null;
			this.ModAdded = null;
			this.Context = null;
		}

		// Token: 0x060055CA RID: 21962 RVA: 0x00199738 File Offset: 0x00197938
		public static bool Check(GameObject Object, GameObject Actor, IModification ModAdded = null, string Context = null)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("TriggersMakersMarkCreation"))
			{
				Event @event = Event.New("TriggersMakersMarkCreation", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("ModAdded", ModAdded);
				@event.SetParameter("Context", Context);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<TriggersMakersMarkCreationEvent>.ID, MinEvent.CascadeLevel))
			{
				TriggersMakersMarkCreationEvent triggersMakersMarkCreationEvent = PooledEvent<TriggersMakersMarkCreationEvent>.FromPool();
				triggersMakersMarkCreationEvent.Object = Object;
				triggersMakersMarkCreationEvent.Actor = Actor;
				triggersMakersMarkCreationEvent.ModAdded = ModAdded;
				triggersMakersMarkCreationEvent.Context = Context;
				flag = Object.HandleEvent(triggersMakersMarkCreationEvent);
			}
			return !flag;
		}

		// Token: 0x04002B35 RID: 11061
		public GameObject Object;

		// Token: 0x04002B36 RID: 11062
		public GameObject Actor;

		// Token: 0x04002B37 RID: 11063
		public IModification ModAdded;

		// Token: 0x04002B38 RID: 11064
		public string Context;
	}
}
