using System;

namespace XRL.World
{
	// Token: 0x02000712 RID: 1810
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Pool)]
	public class ExtremitiesMovedEvent : PooledEvent<ExtremitiesMovedEvent>
	{
		// Token: 0x06004B72 RID: 19314 RVA: 0x00176EF5 File Offset: 0x001750F5
		public override int GetCascadeLevel()
		{
			return ExtremitiesMovedEvent.CascadeLevel;
		}

		// Token: 0x06004B73 RID: 19315 RVA: 0x00176EFC File Offset: 0x001750FC
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004B74 RID: 19316 RVA: 0x00176F05 File Offset: 0x00175105
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.To = null;
			this.Involuntary = false;
		}

		// Token: 0x06004B75 RID: 19317 RVA: 0x00176F22 File Offset: 0x00175122
		public static ExtremitiesMovedEvent FromPool(GameObject Object, string To = null, bool Involuntary = false)
		{
			ExtremitiesMovedEvent extremitiesMovedEvent = PooledEvent<ExtremitiesMovedEvent>.FromPool();
			extremitiesMovedEvent.Object = Object;
			extremitiesMovedEvent.To = To;
			extremitiesMovedEvent.Involuntary = Involuntary;
			return extremitiesMovedEvent;
		}

		// Token: 0x06004B76 RID: 19318 RVA: 0x00176F40 File Offset: 0x00175140
		public static void Send(GameObject Object, string To = null, bool Involuntary = false)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("ExtremitiesMoved"))
			{
				Event @event = Event.New("ExtremitiesMoved", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("To", To);
				@event.SetFlag("Involuntary", Involuntary);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<ExtremitiesMovedEvent>.ID, ExtremitiesMovedEvent.CascadeLevel))
			{
				flag = Object.HandleEvent(ExtremitiesMovedEvent.FromPool(Object, To, Involuntary));
			}
		}

		// Token: 0x0400246A RID: 9322
		public new static readonly int CascadeLevel = 15;

		// Token: 0x0400246B RID: 9323
		public GameObject Object;

		// Token: 0x0400246C RID: 9324
		public string To;

		// Token: 0x0400246D RID: 9325
		public bool Involuntary;
	}
}
