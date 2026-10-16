using System;

namespace XRL.World
{
	// Token: 0x020006E1 RID: 1761
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class CheckRealityDistortionAdvisabilityEvent : PooledEvent<CheckRealityDistortionAdvisabilityEvent>
	{
		// Token: 0x06004A15 RID: 18965 RVA: 0x001734AF File Offset: 0x001716AF
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004A16 RID: 18966 RVA: 0x001734B8 File Offset: 0x001716B8
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Cell = null;
			this.Actor = null;
			this.Device = null;
			this.Mutation = null;
			this.Threshold = 0;
			this.Penetration = 0;
		}

		// Token: 0x06004A17 RID: 18967 RVA: 0x001734F4 File Offset: 0x001716F4
		public static bool Check(GameObject Object = null, Cell Cell = null, GameObject Actor = null, GameObject Device = null, IPart Mutation = null, int? Threshold = null, int Penetration = 0)
		{
			bool flag = true;
			int num = Threshold ?? ((Mutation != null) ? 80 : 30);
			if (flag)
			{
				bool flag2 = GameObject.Validate(ref Object) && Object.HasRegisteredEvent("CheckRealityDistortionAdvisability");
				bool flag3 = Cell != null && Cell.HasObjectWithRegisteredEvent("CheckRealityDistortionAdvisability");
				if (flag2 || flag3)
				{
					Event @event = Event.New("CheckRealityDistortionAdvisability", 0, 0, 0);
					@event.SetParameter("Object", Object);
					@event.SetParameter("Cell", Cell);
					@event.SetParameter("Actor", Actor);
					@event.SetParameter("Mutation", Mutation);
					@event.SetParameter("Device", Device);
					@event.SetParameter("Threshold", num);
					@event.SetParameter("Penetration", Penetration);
					flag = (!flag2 || Object.FireEvent(@event)) && (!flag3 || Cell.FireEvent(@event));
				}
			}
			if (flag)
			{
				bool flag4 = GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<CheckRealityDistortionAdvisabilityEvent>.ID, MinEvent.CascadeLevel);
				bool flag5 = Cell != null && Cell.WantEvent(PooledEvent<CheckRealityDistortionAdvisabilityEvent>.ID, MinEvent.CascadeLevel);
				if (flag4 || flag5)
				{
					CheckRealityDistortionAdvisabilityEvent checkRealityDistortionAdvisabilityEvent = PooledEvent<CheckRealityDistortionAdvisabilityEvent>.FromPool();
					checkRealityDistortionAdvisabilityEvent.Object = Object;
					checkRealityDistortionAdvisabilityEvent.Cell = Cell;
					checkRealityDistortionAdvisabilityEvent.Actor = Actor;
					checkRealityDistortionAdvisabilityEvent.Mutation = Mutation;
					checkRealityDistortionAdvisabilityEvent.Device = Device;
					checkRealityDistortionAdvisabilityEvent.Threshold = num;
					checkRealityDistortionAdvisabilityEvent.Penetration = Penetration;
					flag = (!flag4 || Object.HandleEvent(checkRealityDistortionAdvisabilityEvent)) && (!flag5 || Cell.HandleEvent(checkRealityDistortionAdvisabilityEvent));
				}
			}
			return flag;
		}

		// Token: 0x0400239A RID: 9114
		public GameObject Object;

		// Token: 0x0400239B RID: 9115
		public Cell Cell;

		// Token: 0x0400239C RID: 9116
		public GameObject Actor;

		// Token: 0x0400239D RID: 9117
		public GameObject Device;

		// Token: 0x0400239E RID: 9118
		public IPart Mutation;

		// Token: 0x0400239F RID: 9119
		public int Threshold;

		// Token: 0x040023A0 RID: 9120
		public int Penetration;
	}
}
