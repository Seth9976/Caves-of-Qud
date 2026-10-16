using System;

namespace XRL.World
{
	// Token: 0x02000667 RID: 1639
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class AfterPetEvent : PooledEvent<AfterPetEvent>
	{
		// Token: 0x060046B4 RID: 18100 RVA: 0x00167815 File Offset: 0x00165A15
		public override int GetCascadeLevel()
		{
			return AfterPetEvent.CascadeLevel;
		}

		// Token: 0x060046B5 RID: 18101 RVA: 0x0016781C File Offset: 0x00165A1C
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060046B6 RID: 18102 RVA: 0x00167825 File Offset: 0x00165A25
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Actor = null;
		}

		// Token: 0x060046B7 RID: 18103 RVA: 0x0016783C File Offset: 0x00165A3C
		public static void Send(GameObject Object, GameObject Actor)
		{
			bool flag = true;
			if (flag)
			{
				bool flag2 = GameObject.Validate(ref Object) && Object.HasRegisteredEvent("AfterPet");
				bool flag3 = GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("AfterPet");
				if (flag2 || flag3)
				{
					Event @event = Event.New("AfterPet", 0, 0, 0);
					@event.SetParameter("Object", Object);
					@event.SetParameter("Actor", Actor);
					flag = (!flag2 || Object.FireEvent(@event)) && (!flag3 || Actor.FireEvent(@event));
				}
			}
			if (flag)
			{
				bool flag4 = GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<AfterPetEvent>.ID, AfterPetEvent.CascadeLevel);
				bool flag5 = GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<AfterPetEvent>.ID, AfterPetEvent.CascadeLevel);
				if (flag4 || flag5)
				{
					AfterPetEvent afterPetEvent = PooledEvent<AfterPetEvent>.FromPool();
					afterPetEvent.Object = Object;
					afterPetEvent.Actor = Actor;
					bool flag6 = (!flag4 || Object.HandleEvent(afterPetEvent)) && (!flag5 || Actor.HandleEvent(afterPetEvent));
				}
			}
		}

		// Token: 0x040021A0 RID: 8608
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040021A1 RID: 8609
		public GameObject Object;

		// Token: 0x040021A2 RID: 8610
		public GameObject Actor;
	}
}
