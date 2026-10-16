using System;

namespace XRL.World
{
	// Token: 0x0200077F RID: 1919
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class GetRealityStabilizationPenetrationEvent : PooledEvent<GetRealityStabilizationPenetrationEvent>
	{
		// Token: 0x06004EB8 RID: 20152 RVA: 0x00181419 File Offset: 0x0017F619
		public override int GetCascadeLevel()
		{
			return GetRealityStabilizationPenetrationEvent.CascadeLevel;
		}

		// Token: 0x06004EB9 RID: 20153 RVA: 0x00181420 File Offset: 0x0017F620
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004EBA RID: 20154 RVA: 0x00181429 File Offset: 0x0017F629
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Penetration = 0;
		}

		// Token: 0x06004EBB RID: 20155 RVA: 0x00181440 File Offset: 0x0017F640
		public static int GetFor(GameObject Object)
		{
			int num = Object.GetIntProperty("RealityStabilizationPenetration", 0);
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GetRealityStabilizationPenetration"))
			{
				Event @event = Event.New("GetRealityStabilizationPenetration", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Penetration", num);
				flag = Object.FireEvent(@event);
				num = @event.GetIntParameter("Penetration", 0);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetRealityStabilizationPenetrationEvent>.ID, GetRealityStabilizationPenetrationEvent.CascadeLevel))
			{
				GetRealityStabilizationPenetrationEvent getRealityStabilizationPenetrationEvent = PooledEvent<GetRealityStabilizationPenetrationEvent>.FromPool();
				getRealityStabilizationPenetrationEvent.Object = Object;
				getRealityStabilizationPenetrationEvent.Penetration = num;
				flag = Object.HandleEvent(getRealityStabilizationPenetrationEvent);
				num = getRealityStabilizationPenetrationEvent.Penetration;
			}
			return num;
		}

		// Token: 0x04002679 RID: 9849
		public new static readonly int CascadeLevel = 17;

		// Token: 0x0400267A RID: 9850
		public GameObject Object;

		// Token: 0x0400267B RID: 9851
		public int Penetration;
	}
}
