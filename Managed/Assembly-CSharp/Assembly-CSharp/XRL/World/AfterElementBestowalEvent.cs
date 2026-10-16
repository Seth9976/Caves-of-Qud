using System;

namespace XRL.World
{
	// Token: 0x0200065D RID: 1629
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AfterElementBestowalEvent : PooledEvent<AfterElementBestowalEvent>
	{
		// Token: 0x06004667 RID: 18023 RVA: 0x001667D5 File Offset: 0x001649D5
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004668 RID: 18024 RVA: 0x001667DE File Offset: 0x001649DE
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Element = null;
			this.Type = null;
			this.Subtype = null;
			this.Tier = 0;
		}

		// Token: 0x06004669 RID: 18025 RVA: 0x0016680C File Offset: 0x00164A0C
		public static void Send(GameObject Object, string Element, string Type, string Subtype, int Tier)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("AfterElementBestowal"))
			{
				Event @event = Event.New("AfterElementBestowal", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Element", Element);
				@event.SetParameter("Type", Type);
				@event.SetParameter("Subtype", Subtype);
				@event.SetParameter("Tier", Tier);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<AfterElementBestowalEvent>.ID, MinEvent.CascadeLevel))
			{
				AfterElementBestowalEvent afterElementBestowalEvent = PooledEvent<AfterElementBestowalEvent>.FromPool();
				afterElementBestowalEvent.Object = Object;
				afterElementBestowalEvent.Element = Element;
				afterElementBestowalEvent.Type = Type;
				afterElementBestowalEvent.Subtype = Subtype;
				afterElementBestowalEvent.Tier = Tier;
				flag = Object.HandleEvent(afterElementBestowalEvent);
			}
		}

		// Token: 0x04002171 RID: 8561
		public GameObject Object;

		// Token: 0x04002172 RID: 8562
		public string Element;

		// Token: 0x04002173 RID: 8563
		public string Type;

		// Token: 0x04002174 RID: 8564
		public string Subtype;

		// Token: 0x04002175 RID: 8565
		public int Tier;
	}
}
