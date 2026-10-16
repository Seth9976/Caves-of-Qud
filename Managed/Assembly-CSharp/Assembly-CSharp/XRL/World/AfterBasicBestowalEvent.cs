using System;

namespace XRL.World
{
	// Token: 0x02000656 RID: 1622
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AfterBasicBestowalEvent : PooledEvent<AfterBasicBestowalEvent>
	{
		// Token: 0x06004633 RID: 17971 RVA: 0x00165E46 File Offset: 0x00164046
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004634 RID: 17972 RVA: 0x00165E4F File Offset: 0x0016404F
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Type = null;
			this.Subtype = null;
			this.Tier = 0;
			this.Standard = false;
		}

		// Token: 0x06004635 RID: 17973 RVA: 0x00165E7C File Offset: 0x0016407C
		public static void Send(GameObject Object, string Type, string Subtype, int Tier, bool Standard)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("AfterBasicBestowal"))
			{
				Event @event = Event.New("AfterBasicBestowal", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Type", Type);
				@event.SetParameter("Subtype", Subtype);
				@event.SetParameter("Tier", Tier);
				@event.SetFlag("Standard", Standard);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<AfterBasicBestowalEvent>.ID, MinEvent.CascadeLevel))
			{
				AfterBasicBestowalEvent afterBasicBestowalEvent = PooledEvent<AfterBasicBestowalEvent>.FromPool();
				afterBasicBestowalEvent.Object = Object;
				afterBasicBestowalEvent.Type = Type;
				afterBasicBestowalEvent.Subtype = Subtype;
				afterBasicBestowalEvent.Tier = Tier;
				afterBasicBestowalEvent.Standard = Standard;
				flag = Object.HandleEvent(afterBasicBestowalEvent);
			}
		}

		// Token: 0x04002151 RID: 8529
		public GameObject Object;

		// Token: 0x04002152 RID: 8530
		public string Type;

		// Token: 0x04002153 RID: 8531
		public string Subtype;

		// Token: 0x04002154 RID: 8532
		public int Tier;

		// Token: 0x04002155 RID: 8533
		public bool Standard;
	}
}
