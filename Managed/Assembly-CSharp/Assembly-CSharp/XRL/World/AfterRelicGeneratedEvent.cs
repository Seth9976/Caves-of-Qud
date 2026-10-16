using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200066C RID: 1644
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AfterRelicGeneratedEvent : PooledEvent<AfterRelicGeneratedEvent>
	{
		// Token: 0x060046D0 RID: 18128 RVA: 0x00167C51 File Offset: 0x00165E51
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060046D1 RID: 18129 RVA: 0x00167C5A File Offset: 0x00165E5A
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.NameElement = null;
			this.Elements = null;
			this.Type = null;
			this.Subtype = null;
			this.Tier = 0;
		}

		// Token: 0x060046D2 RID: 18130 RVA: 0x00167C8C File Offset: 0x00165E8C
		public static void Send(GameObject Object, List<string> Elements, string NameElement, string Type, string Subtype, int Tier)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("AfterRelicGenerated"))
			{
				Event @event = Event.New("AfterRelicGenerated", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Elements", Elements);
				@event.SetParameter("NameElement", NameElement);
				@event.SetParameter("Type", Type);
				@event.SetParameter("Subtype", Subtype);
				@event.SetParameter("Tier", Tier);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<AfterRelicGeneratedEvent>.ID, MinEvent.CascadeLevel))
			{
				AfterRelicGeneratedEvent afterRelicGeneratedEvent = PooledEvent<AfterRelicGeneratedEvent>.FromPool();
				afterRelicGeneratedEvent.Object = Object;
				afterRelicGeneratedEvent.Elements = Elements;
				afterRelicGeneratedEvent.NameElement = NameElement;
				afterRelicGeneratedEvent.Type = Type;
				afterRelicGeneratedEvent.Subtype = Subtype;
				afterRelicGeneratedEvent.Tier = Tier;
				flag = Object.HandleEvent(afterRelicGeneratedEvent);
			}
		}

		// Token: 0x040021B3 RID: 8627
		public GameObject Object;

		// Token: 0x040021B4 RID: 8628
		public string NameElement;

		// Token: 0x040021B5 RID: 8629
		public List<string> Elements;

		// Token: 0x040021B6 RID: 8630
		public string Type;

		// Token: 0x040021B7 RID: 8631
		public string Subtype;

		// Token: 0x040021B8 RID: 8632
		public int Tier;
	}
}
