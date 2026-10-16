using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000698 RID: 1688
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class BeforeObjectCreatedEvent : IObjectCreationEvent
	{
		// Token: 0x06004819 RID: 18457 RVA: 0x0016B58C File Offset: 0x0016978C
		public BeforeObjectCreatedEvent()
		{
			this.ID = BeforeObjectCreatedEvent.ID;
		}

		// Token: 0x0600481A RID: 18458 RVA: 0x0016B59F File Offset: 0x0016979F
		public static int CountPool()
		{
			if (BeforeObjectCreatedEvent.Pool != null)
			{
				return BeforeObjectCreatedEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x0600481B RID: 18459 RVA: 0x0016B5B4 File Offset: 0x001697B4
		public static void ResetPool()
		{
			while (BeforeObjectCreatedEvent.PoolCounter > 0)
			{
				BeforeObjectCreatedEvent.Pool[--BeforeObjectCreatedEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x0600481C RID: 18460 RVA: 0x0016B5DC File Offset: 0x001697DC
		public static void ResetTo(ref BeforeObjectCreatedEvent E)
		{
			MinEvent.ResetTo<BeforeObjectCreatedEvent>(E, BeforeObjectCreatedEvent.Pool, ref BeforeObjectCreatedEvent.PoolCounter);
			E = null;
		}

		// Token: 0x0600481D RID: 18461 RVA: 0x0016B5F2 File Offset: 0x001697F2
		public static BeforeObjectCreatedEvent FromPool()
		{
			return MinEvent.FromPool<BeforeObjectCreatedEvent>(ref BeforeObjectCreatedEvent.Pool, ref BeforeObjectCreatedEvent.PoolCounter, 8192);
		}

		// Token: 0x0600481E RID: 18462 RVA: 0x0016B608 File Offset: 0x00169808
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x0600481F RID: 18463 RVA: 0x0016B61C File Offset: 0x0016981C
		public static void Process(GameObject Object, string Context, ref GameObject ReplacementObject)
		{
			if (true && Object.HasRegisteredEvent("BeforeObjectCreated"))
			{
				Event @event = Event.New("BeforeObjectCreated", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Context", Context);
				@event.SetParameter("ReplacementObject", ReplacementObject);
				Object.FireEvent(@event);
				ReplacementObject = @event.GetGameObjectParameter("ReplacementObject");
			}
			if (Object.WantEvent(BeforeObjectCreatedEvent.ID, MinEvent.CascadeLevel))
			{
				BeforeObjectCreatedEvent beforeObjectCreatedEvent = BeforeObjectCreatedEvent.FromPool();
				beforeObjectCreatedEvent.Object = Object;
				beforeObjectCreatedEvent.Context = Context;
				beforeObjectCreatedEvent.ReplacementObject = ReplacementObject;
				Object.HandleEvent(beforeObjectCreatedEvent);
				ReplacementObject = beforeObjectCreatedEvent.ReplacementObject;
			}
		}

		// Token: 0x04002269 RID: 8809
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(BeforeObjectCreatedEvent), null, new MinEvent.EventPoolCount(BeforeObjectCreatedEvent.CountPool), new MinEvent.EventPoolReset(BeforeObjectCreatedEvent.ResetPool));

		// Token: 0x0400226A RID: 8810
		private static List<BeforeObjectCreatedEvent> Pool;

		// Token: 0x0400226B RID: 8811
		private static int PoolCounter;
	}
}
