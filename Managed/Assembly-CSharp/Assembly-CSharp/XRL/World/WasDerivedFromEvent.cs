using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000879 RID: 2169
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class WasDerivedFromEvent : IDerivationEvent
	{
		// Token: 0x0600561B RID: 22043 RVA: 0x0019A3D5 File Offset: 0x001985D5
		public WasDerivedFromEvent()
		{
			this.ID = WasDerivedFromEvent.ID;
		}

		// Token: 0x0600561C RID: 22044 RVA: 0x0019A3E8 File Offset: 0x001985E8
		public static int CountPool()
		{
			if (WasDerivedFromEvent.Pool != null)
			{
				return WasDerivedFromEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x0600561D RID: 22045 RVA: 0x0019A3FD File Offset: 0x001985FD
		public static void ResetPool()
		{
			while (WasDerivedFromEvent.PoolCounter > 0)
			{
				WasDerivedFromEvent.Pool[--WasDerivedFromEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x0600561E RID: 22046 RVA: 0x0019A425 File Offset: 0x00198625
		public static void ResetTo(ref WasDerivedFromEvent E)
		{
			MinEvent.ResetTo<WasDerivedFromEvent>(E, WasDerivedFromEvent.Pool, ref WasDerivedFromEvent.PoolCounter);
			E = null;
		}

		// Token: 0x0600561F RID: 22047 RVA: 0x0019A43B File Offset: 0x0019863B
		public static WasDerivedFromEvent FromPool()
		{
			return MinEvent.FromPool<WasDerivedFromEvent>(ref WasDerivedFromEvent.Pool, ref WasDerivedFromEvent.PoolCounter, 8192);
		}

		// Token: 0x06005620 RID: 22048 RVA: 0x0019A451 File Offset: 0x00198651
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06005621 RID: 22049 RVA: 0x0019A465 File Offset: 0x00198665
		public override void Reset()
		{
			base.Reset();
			this.Derivation = null;
			this.Additional = null;
			this.All = null;
		}

		// Token: 0x06005622 RID: 22050 RVA: 0x0019A484 File Offset: 0x00198684
		public static void Send(GameObject Object, GameObject Actor, GameObject Derivation, List<GameObject> Additional, List<GameObject> All, string Context = null)
		{
			if (GameObject.Validate(ref Object) && Object.HasRegisteredEvent("WasDerivedFrom"))
			{
				Event @event = Event.New("WasDerivedFrom", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Context", Context);
				@event.SetParameter("Derivation", Derivation);
				@event.SetParameter("Additional", Additional);
				@event.SetParameter("Additional", All);
				Object.FireEvent(@event);
			}
			if (GameObject.Validate(ref Object) && Object.WantEvent(WasDerivedFromEvent.ID, MinEvent.CascadeLevel))
			{
				WasDerivedFromEvent wasDerivedFromEvent = WasDerivedFromEvent.FromPool();
				wasDerivedFromEvent.Object = Object;
				wasDerivedFromEvent.Actor = Actor;
				wasDerivedFromEvent.Derivation = Derivation;
				wasDerivedFromEvent.Additional = Additional;
				wasDerivedFromEvent.All = All;
				wasDerivedFromEvent.Context = Context;
				Object.HandleEvent(wasDerivedFromEvent);
			}
		}

		// Token: 0x06005623 RID: 22051 RVA: 0x0019A564 File Offset: 0x00198764
		public void ApplyToEach(Action<GameObject> Func)
		{
			if (this.All != null)
			{
				using (List<GameObject>.Enumerator enumerator = this.All.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						GameObject gameObject = enumerator.Current;
						Func(gameObject);
					}
					return;
				}
			}
			Func(this.Derivation);
			if (this.Additional != null)
			{
				foreach (GameObject gameObject2 in this.Additional)
				{
					Func(gameObject2);
				}
			}
		}

		// Token: 0x04002B5F RID: 11103
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(WasDerivedFromEvent), null, new MinEvent.EventPoolCount(WasDerivedFromEvent.CountPool), new MinEvent.EventPoolReset(WasDerivedFromEvent.ResetPool));

		// Token: 0x04002B60 RID: 11104
		private static List<WasDerivedFromEvent> Pool;

		// Token: 0x04002B61 RID: 11105
		private static int PoolCounter;

		// Token: 0x04002B62 RID: 11106
		public GameObject Derivation;

		// Token: 0x04002B63 RID: 11107
		public List<GameObject> Additional;

		// Token: 0x04002B64 RID: 11108
		public List<GameObject> All;
	}
}
