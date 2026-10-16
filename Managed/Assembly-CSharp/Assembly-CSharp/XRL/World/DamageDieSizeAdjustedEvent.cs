using System;

namespace XRL.World
{
	// Token: 0x020006F3 RID: 1779
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class DamageDieSizeAdjustedEvent : PooledEvent<DamageDieSizeAdjustedEvent>
	{
		// Token: 0x06004A87 RID: 19079 RVA: 0x001746C2 File Offset: 0x001728C2
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004A88 RID: 19080 RVA: 0x001746CB File Offset: 0x001728CB
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Part = null;
			this.Amount = 0;
		}

		// Token: 0x06004A89 RID: 19081 RVA: 0x001746E8 File Offset: 0x001728E8
		public static void Send(GameObject Object, IPart Part, int Amount)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("DamageDieSizeAdjusted"))
			{
				Event @event = Event.New("DamageDieSizeAdjusted", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Part", Part);
				@event.SetParameter("Amount", Amount);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<DamageDieSizeAdjustedEvent>.ID, MinEvent.CascadeLevel))
			{
				DamageDieSizeAdjustedEvent damageDieSizeAdjustedEvent = PooledEvent<DamageDieSizeAdjustedEvent>.FromPool();
				damageDieSizeAdjustedEvent.Object = Object;
				damageDieSizeAdjustedEvent.Part = Part;
				damageDieSizeAdjustedEvent.Amount = Amount;
				flag = Object.HandleEvent(damageDieSizeAdjustedEvent);
			}
		}

		// Token: 0x040023EF RID: 9199
		public GameObject Object;

		// Token: 0x040023F0 RID: 9200
		public IPart Part;

		// Token: 0x040023F1 RID: 9201
		public int Amount;
	}
}
