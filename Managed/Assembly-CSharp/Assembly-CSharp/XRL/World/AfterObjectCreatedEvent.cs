using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000666 RID: 1638
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AfterObjectCreatedEvent : IObjectCreationEvent
	{
		// Token: 0x060046AC RID: 18092 RVA: 0x001676B0 File Offset: 0x001658B0
		public AfterObjectCreatedEvent()
		{
			this.ID = AfterObjectCreatedEvent.ID;
		}

		// Token: 0x060046AD RID: 18093 RVA: 0x001676C3 File Offset: 0x001658C3
		public static int CountPool()
		{
			if (AfterObjectCreatedEvent.Pool != null)
			{
				return AfterObjectCreatedEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060046AE RID: 18094 RVA: 0x001676D8 File Offset: 0x001658D8
		public static void ResetPool()
		{
			while (AfterObjectCreatedEvent.PoolCounter > 0)
			{
				AfterObjectCreatedEvent.Pool[--AfterObjectCreatedEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060046AF RID: 18095 RVA: 0x00167700 File Offset: 0x00165900
		public static void ResetTo(ref AfterObjectCreatedEvent E)
		{
			MinEvent.ResetTo<AfterObjectCreatedEvent>(E, AfterObjectCreatedEvent.Pool, ref AfterObjectCreatedEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060046B0 RID: 18096 RVA: 0x00167716 File Offset: 0x00165916
		public static AfterObjectCreatedEvent FromPool()
		{
			return MinEvent.FromPool<AfterObjectCreatedEvent>(ref AfterObjectCreatedEvent.Pool, ref AfterObjectCreatedEvent.PoolCounter, 8192);
		}

		// Token: 0x060046B1 RID: 18097 RVA: 0x0016772C File Offset: 0x0016592C
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060046B2 RID: 18098 RVA: 0x00167740 File Offset: 0x00165940
		public static void Process(GameObject Object, string Context, ref GameObject ReplacementObject)
		{
			if (true && Object.HasRegisteredEvent("AfterObjectCreated"))
			{
				Event @event = Event.New("AfterObjectCreated", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Context", Context);
				@event.SetParameter("ReplacementObject", ReplacementObject);
				Object.FireEvent(@event);
				ReplacementObject = @event.GetGameObjectParameter("ReplacementObject");
			}
			if (Object.WantEvent(AfterObjectCreatedEvent.ID, MinEvent.CascadeLevel))
			{
				AfterObjectCreatedEvent afterObjectCreatedEvent = AfterObjectCreatedEvent.FromPool();
				afterObjectCreatedEvent.Object = Object;
				afterObjectCreatedEvent.Context = Context;
				afterObjectCreatedEvent.ReplacementObject = ReplacementObject;
				Object.HandleEvent(afterObjectCreatedEvent);
				ReplacementObject = afterObjectCreatedEvent.ReplacementObject;
			}
		}

		// Token: 0x0400219D RID: 8605
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AfterObjectCreatedEvent), null, new MinEvent.EventPoolCount(AfterObjectCreatedEvent.CountPool), new MinEvent.EventPoolReset(AfterObjectCreatedEvent.ResetPool));

		// Token: 0x0400219E RID: 8606
		private static List<AfterObjectCreatedEvent> Pool;

		// Token: 0x0400219F RID: 8607
		private static int PoolCounter;
	}
}
