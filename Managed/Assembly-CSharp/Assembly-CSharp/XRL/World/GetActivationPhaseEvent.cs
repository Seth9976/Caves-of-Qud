using System;

namespace XRL.World
{
	// Token: 0x02000723 RID: 1827
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class GetActivationPhaseEvent : PooledEvent<GetActivationPhaseEvent>
	{
		// Token: 0x06004BE6 RID: 19430 RVA: 0x0017821F File Offset: 0x0017641F
		public override int GetCascadeLevel()
		{
			return GetActivationPhaseEvent.CascadeLevel;
		}

		// Token: 0x06004BE7 RID: 19431 RVA: 0x00178226 File Offset: 0x00176426
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004BE8 RID: 19432 RVA: 0x0017822F File Offset: 0x0017642F
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Phase = 0;
		}

		// Token: 0x06004BE9 RID: 19433 RVA: 0x00178248 File Offset: 0x00176448
		public static int GetFor(GameObject Object, int Phase = 0)
		{
			if (Phase == 0 && GameObject.Validate(ref Object))
			{
				Phase = Object.GetPhase();
			}
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GetActivationPhase"))
			{
				Event @event = Event.New("GetActivationPhase", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Phase", Phase);
				flag = Object.FireEvent(@event);
				Phase = @event.GetIntParameter("Phase", 0);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetActivationPhaseEvent>.ID, GetActivationPhaseEvent.CascadeLevel))
			{
				GetActivationPhaseEvent getActivationPhaseEvent = PooledEvent<GetActivationPhaseEvent>.FromPool();
				getActivationPhaseEvent.Object = Object;
				getActivationPhaseEvent.Phase = Phase;
				flag = Object.HandleEvent(getActivationPhaseEvent);
				Phase = getActivationPhaseEvent.Phase;
			}
			return Phase;
		}

		// Token: 0x040024B6 RID: 9398
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040024B7 RID: 9399
		public GameObject Object;

		// Token: 0x040024B8 RID: 9400
		public int Phase;
	}
}
