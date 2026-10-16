using System;

namespace XRL.World
{
	// Token: 0x020006E2 RID: 1762
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class CheckRealityDistortionUsabilityEvent : PooledEvent<CheckRealityDistortionUsabilityEvent>
	{
		// Token: 0x06004A19 RID: 18969 RVA: 0x0017369E File Offset: 0x0017189E
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004A1A RID: 18970 RVA: 0x001736A7 File Offset: 0x001718A7
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Cell = null;
			this.Actor = null;
			this.Device = null;
			this.Mutation = null;
			this.Threshold = null;
			this.Penetration = 0;
		}

		// Token: 0x06004A1B RID: 18971 RVA: 0x001736E8 File Offset: 0x001718E8
		public static bool Check(GameObject Object = null, Cell Cell = null, GameObject Actor = null, GameObject Device = null, IPart Mutation = null, int? Threshold = null, int Penetration = 0)
		{
			bool flag = true;
			if (flag)
			{
				bool flag2 = GameObject.Validate(ref Object) && Object.HasRegisteredEvent("CheckRealityDistortionUsability");
				bool flag3 = Cell != null && Cell.HasObjectWithRegisteredEvent("CheckRealityDistortionUsability");
				if (flag2 || flag3)
				{
					Event @event = Event.New("CheckRealityDistortionUsability", 0, 0, 0);
					@event.SetParameter("Object", Object);
					@event.SetParameter("Cell", Cell);
					@event.SetParameter("Actor", Actor);
					@event.SetParameter("Mutation", Mutation);
					@event.SetParameter("Device", Device);
					if (Threshold != null)
					{
						@event.SetParameter("Threshold", Threshold);
					}
					@event.SetParameter("Penetration", Penetration);
					flag = (!flag2 || Object.FireEvent(@event)) && (!flag3 || Cell.FireEvent(@event));
				}
			}
			if (flag)
			{
				bool flag4 = GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<CheckRealityDistortionUsabilityEvent>.ID, MinEvent.CascadeLevel);
				bool flag5 = Cell != null && Cell.WantEvent(PooledEvent<CheckRealityDistortionUsabilityEvent>.ID, MinEvent.CascadeLevel);
				if (flag4 || flag5)
				{
					CheckRealityDistortionUsabilityEvent checkRealityDistortionUsabilityEvent = PooledEvent<CheckRealityDistortionUsabilityEvent>.FromPool();
					checkRealityDistortionUsabilityEvent.Object = Object;
					checkRealityDistortionUsabilityEvent.Cell = Cell;
					checkRealityDistortionUsabilityEvent.Actor = Actor;
					checkRealityDistortionUsabilityEvent.Mutation = Mutation;
					checkRealityDistortionUsabilityEvent.Device = Device;
					checkRealityDistortionUsabilityEvent.Threshold = Threshold;
					checkRealityDistortionUsabilityEvent.Penetration = Penetration;
					flag = (!flag4 || Object.HandleEvent(checkRealityDistortionUsabilityEvent)) && (!flag5 || Cell.HandleEvent(checkRealityDistortionUsabilityEvent));
				}
			}
			return flag;
		}

		// Token: 0x040023A1 RID: 9121
		public GameObject Object;

		// Token: 0x040023A2 RID: 9122
		public Cell Cell;

		// Token: 0x040023A3 RID: 9123
		public GameObject Actor;

		// Token: 0x040023A4 RID: 9124
		public GameObject Device;

		// Token: 0x040023A5 RID: 9125
		public IPart Mutation;

		// Token: 0x040023A6 RID: 9126
		public int? Threshold;

		// Token: 0x040023A7 RID: 9127
		public int Penetration;
	}
}
