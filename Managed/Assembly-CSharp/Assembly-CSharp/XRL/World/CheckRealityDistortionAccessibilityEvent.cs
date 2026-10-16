using System;

namespace XRL.World
{
	// Token: 0x020006E0 RID: 1760
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class CheckRealityDistortionAccessibilityEvent : PooledEvent<CheckRealityDistortionAccessibilityEvent>
	{
		// Token: 0x06004A11 RID: 18961 RVA: 0x001732BB File Offset: 0x001714BB
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004A12 RID: 18962 RVA: 0x001732C4 File Offset: 0x001714C4
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Cell = null;
			this.Actor = null;
			this.Device = null;
			this.Mutation = null;
			this.Penetration = 0;
			this.Allow = false;
		}

		// Token: 0x06004A13 RID: 18963 RVA: 0x00173300 File Offset: 0x00171500
		public static bool Check(GameObject Object = null, Cell Cell = null, GameObject Actor = null, GameObject Device = null, IPart Mutation = null, int Penetration = 0)
		{
			bool flag = true;
			bool flag2 = true;
			if (Cell == null)
			{
				Cell = ((Object != null) ? Object.CurrentCell : null);
			}
			if (flag)
			{
				bool flag3 = GameObject.Validate(ref Object) && Object.HasRegisteredEvent("CheckRealityDistortionAccessibility");
				bool flag4 = Cell != null && Cell.HasObjectWithRegisteredEvent("CheckRealityDistortionAccessibility");
				if (flag3 || flag4)
				{
					Event @event = Event.New("CheckRealityDistortionAccessibility", 0, 0, 0);
					@event.SetParameter("Object", Object);
					@event.SetParameter("Cell", Cell);
					@event.SetParameter("Actor", Actor);
					@event.SetParameter("Mutation", Mutation);
					@event.SetParameter("Device", Device);
					@event.SetParameter("Penetration", Penetration);
					@event.SetFlag("Allow", flag2);
					flag = (!flag3 || Object.FireEvent(@event)) && (!flag4 || Cell.FireEvent(@event));
					flag2 = @event.HasFlag("Allow");
				}
			}
			if (flag)
			{
				bool flag5 = GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<CheckRealityDistortionAccessibilityEvent>.ID, MinEvent.CascadeLevel);
				bool flag6 = Cell != null && Cell.WantEvent(PooledEvent<CheckRealityDistortionAccessibilityEvent>.ID, MinEvent.CascadeLevel);
				if (flag5 || flag6)
				{
					CheckRealityDistortionAccessibilityEvent checkRealityDistortionAccessibilityEvent = PooledEvent<CheckRealityDistortionAccessibilityEvent>.FromPool();
					checkRealityDistortionAccessibilityEvent.Object = Object;
					checkRealityDistortionAccessibilityEvent.Cell = Cell;
					checkRealityDistortionAccessibilityEvent.Actor = Actor;
					checkRealityDistortionAccessibilityEvent.Mutation = Mutation;
					checkRealityDistortionAccessibilityEvent.Device = Device;
					checkRealityDistortionAccessibilityEvent.Penetration = Penetration;
					checkRealityDistortionAccessibilityEvent.Allow = flag2;
					bool flag7 = (!flag5 || Object.HandleEvent(checkRealityDistortionAccessibilityEvent)) && (!flag6 || Cell.HandleEvent(checkRealityDistortionAccessibilityEvent));
					flag2 = checkRealityDistortionAccessibilityEvent.Allow;
				}
			}
			return flag2;
		}

		// Token: 0x04002393 RID: 9107
		public GameObject Object;

		// Token: 0x04002394 RID: 9108
		public Cell Cell;

		// Token: 0x04002395 RID: 9109
		public GameObject Actor;

		// Token: 0x04002396 RID: 9110
		public GameObject Device;

		// Token: 0x04002397 RID: 9111
		public IPart Mutation;

		// Token: 0x04002398 RID: 9112
		public int Penetration;

		// Token: 0x04002399 RID: 9113
		public bool Allow;
	}
}
