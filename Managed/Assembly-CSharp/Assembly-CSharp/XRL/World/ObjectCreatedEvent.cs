using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000811 RID: 2065
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class ObjectCreatedEvent : IObjectCreationEvent
	{
		// Token: 0x06005266 RID: 21094 RVA: 0x0018CBE5 File Offset: 0x0018ADE5
		public ObjectCreatedEvent()
		{
			this.ID = ObjectCreatedEvent.ID;
		}

		// Token: 0x06005267 RID: 21095 RVA: 0x0018CBF8 File Offset: 0x0018ADF8
		public static int CountPool()
		{
			if (ObjectCreatedEvent.Pool != null)
			{
				return ObjectCreatedEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06005268 RID: 21096 RVA: 0x0018CC0D File Offset: 0x0018AE0D
		public static void ResetPool()
		{
			while (ObjectCreatedEvent.PoolCounter > 0)
			{
				ObjectCreatedEvent.Pool[--ObjectCreatedEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06005269 RID: 21097 RVA: 0x0018CC35 File Offset: 0x0018AE35
		public static void ResetTo(ref ObjectCreatedEvent E)
		{
			MinEvent.ResetTo<ObjectCreatedEvent>(E, ObjectCreatedEvent.Pool, ref ObjectCreatedEvent.PoolCounter);
			E = null;
		}

		// Token: 0x0600526A RID: 21098 RVA: 0x0018CC4B File Offset: 0x0018AE4B
		public static ObjectCreatedEvent FromPool()
		{
			return MinEvent.FromPool<ObjectCreatedEvent>(ref ObjectCreatedEvent.Pool, ref ObjectCreatedEvent.PoolCounter, 8192);
		}

		// Token: 0x0600526B RID: 21099 RVA: 0x0018CC61 File Offset: 0x0018AE61
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x0600526C RID: 21100 RVA: 0x0018CC78 File Offset: 0x0018AE78
		public static void Process(GameObject Object, string Context, ref GameObject ReplacementObject)
		{
			if (true && Object.HasRegisteredEvent("ObjectCreated"))
			{
				Event @event = Event.New("ObjectCreated", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Context", Context);
				@event.SetParameter("ReplacementObject", ReplacementObject);
				Object.FireEvent(@event);
				ReplacementObject = @event.GetGameObjectParameter("ReplacementObject");
			}
			if (Object.WantEvent(ObjectCreatedEvent.ID, MinEvent.CascadeLevel))
			{
				ObjectCreatedEvent objectCreatedEvent = ObjectCreatedEvent.FromPool();
				objectCreatedEvent.Object = Object;
				objectCreatedEvent.Context = Context;
				objectCreatedEvent.ReplacementObject = ReplacementObject;
				Object.HandleEvent(objectCreatedEvent);
				ReplacementObject = objectCreatedEvent.ReplacementObject;
			}
		}

		// Token: 0x0400296F RID: 10607
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(ObjectCreatedEvent), null, new MinEvent.EventPoolCount(ObjectCreatedEvent.CountPool), new MinEvent.EventPoolReset(ObjectCreatedEvent.ResetPool));

		// Token: 0x04002970 RID: 10608
		private static List<ObjectCreatedEvent> Pool;

		// Token: 0x04002971 RID: 10609
		private static int PoolCounter;
	}
}
