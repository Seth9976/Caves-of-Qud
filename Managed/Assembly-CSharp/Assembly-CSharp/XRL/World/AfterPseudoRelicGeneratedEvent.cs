using System;

namespace XRL.World
{
	// Token: 0x0200066A RID: 1642
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AfterPseudoRelicGeneratedEvent : PooledEvent<AfterPseudoRelicGeneratedEvent>
	{
		// Token: 0x060046C6 RID: 18118 RVA: 0x00167AAE File Offset: 0x00165CAE
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060046C7 RID: 18119 RVA: 0x00167AB7 File Offset: 0x00165CB7
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Element = null;
			this.Type = null;
			this.Subtype = null;
			this.Tier = 0;
		}

		// Token: 0x060046C8 RID: 18120 RVA: 0x00167AE4 File Offset: 0x00165CE4
		public static void Send(GameObject Object, string Element, string Type, string Subtype, int Tier)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("AfterPseudoRelicGenerated"))
			{
				Event @event = Event.New("AfterPseudoRelicGenerated", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Element", Element);
				@event.SetParameter("Type", Type);
				@event.SetParameter("Subtype", Subtype);
				@event.SetParameter("Tier", Tier);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<AfterPseudoRelicGeneratedEvent>.ID, MinEvent.CascadeLevel))
			{
				AfterPseudoRelicGeneratedEvent afterPseudoRelicGeneratedEvent = PooledEvent<AfterPseudoRelicGeneratedEvent>.FromPool();
				afterPseudoRelicGeneratedEvent.Object = Object;
				afterPseudoRelicGeneratedEvent.Element = Element;
				afterPseudoRelicGeneratedEvent.Type = Type;
				afterPseudoRelicGeneratedEvent.Subtype = Subtype;
				afterPseudoRelicGeneratedEvent.Tier = Tier;
				flag = Object.HandleEvent(afterPseudoRelicGeneratedEvent);
			}
		}

		// Token: 0x040021AA RID: 8618
		public GameObject Object;

		// Token: 0x040021AB RID: 8619
		public string Element;

		// Token: 0x040021AC RID: 8620
		public string Type;

		// Token: 0x040021AD RID: 8621
		public string Subtype;

		// Token: 0x040021AE RID: 8622
		public int Tier;
	}
}
