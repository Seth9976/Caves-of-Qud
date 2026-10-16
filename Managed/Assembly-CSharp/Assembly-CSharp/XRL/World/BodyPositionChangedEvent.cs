using System;

namespace XRL.World
{
	// Token: 0x020006B0 RID: 1712
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Pool)]
	public class BodyPositionChangedEvent : PooledEvent<BodyPositionChangedEvent>
	{
		// Token: 0x060048CD RID: 18637 RVA: 0x0016D624 File Offset: 0x0016B824
		public override int GetCascadeLevel()
		{
			return BodyPositionChangedEvent.CascadeLevel;
		}

		// Token: 0x060048CE RID: 18638 RVA: 0x0016D62B File Offset: 0x0016B82B
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060048CF RID: 18639 RVA: 0x0016D634 File Offset: 0x0016B834
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.To = null;
			this.Involuntary = false;
		}

		// Token: 0x060048D0 RID: 18640 RVA: 0x0016D651 File Offset: 0x0016B851
		public static BodyPositionChangedEvent FromPool(GameObject Object, string To = null, bool Involuntary = false)
		{
			BodyPositionChangedEvent bodyPositionChangedEvent = PooledEvent<BodyPositionChangedEvent>.FromPool();
			bodyPositionChangedEvent.Object = Object;
			bodyPositionChangedEvent.To = To;
			bodyPositionChangedEvent.Involuntary = Involuntary;
			return bodyPositionChangedEvent;
		}

		// Token: 0x060048D1 RID: 18641 RVA: 0x0016D670 File Offset: 0x0016B870
		public static void Send(GameObject Object, string To = null, bool Involuntary = false)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("BodyPositionChanged"))
			{
				Event @event = Event.New("BodyPositionChanged", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("To", To);
				@event.SetFlag("Involuntary", Involuntary);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<BodyPositionChangedEvent>.ID, BodyPositionChangedEvent.CascadeLevel))
			{
				flag = Object.HandleEvent(BodyPositionChangedEvent.FromPool(Object, To, Involuntary));
			}
		}

		// Token: 0x040022E5 RID: 8933
		public new static readonly int CascadeLevel = 15;

		// Token: 0x040022E6 RID: 8934
		public GameObject Object;

		// Token: 0x040022E7 RID: 8935
		public string To;

		// Token: 0x040022E8 RID: 8936
		public bool Involuntary;
	}
}
