using System;

namespace XRL.World
{
	// Token: 0x02000742 RID: 1858
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetDiseaseOnsetEvent : PooledEvent<GetDiseaseOnsetEvent>
	{
		// Token: 0x06004CF1 RID: 19697 RVA: 0x0017B6B2 File Offset: 0x001798B2
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004CF2 RID: 19698 RVA: 0x0017B6BB File Offset: 0x001798BB
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Effect = null;
		}

		// Token: 0x06004CF3 RID: 19699 RVA: 0x0017B6D4 File Offset: 0x001798D4
		public static Effect GetFor(GameObject Object)
		{
			bool flag = true;
			Effect effect = null;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GetDiseaseOnset"))
			{
				Event @event = Event.New("GetDiseaseOnset", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Effect", effect);
				flag = Object.FireEvent(@event);
				effect = @event.GetParameter("Effect") as Effect;
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetDiseaseOnsetEvent>.ID, MinEvent.CascadeLevel))
			{
				GetDiseaseOnsetEvent getDiseaseOnsetEvent = PooledEvent<GetDiseaseOnsetEvent>.FromPool();
				getDiseaseOnsetEvent.Object = Object;
				getDiseaseOnsetEvent.Effect = effect;
				flag = Object.HandleEvent(getDiseaseOnsetEvent);
				effect = getDiseaseOnsetEvent.Effect;
			}
			return effect;
		}

		// Token: 0x04002541 RID: 9537
		public GameObject Object;

		// Token: 0x04002542 RID: 9538
		public Effect Effect;
	}
}
