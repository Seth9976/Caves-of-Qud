using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200087A RID: 2170
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class WasReplicatedEvent : IReplicationEvent
	{
		// Token: 0x06005625 RID: 22053 RVA: 0x0019A647 File Offset: 0x00198847
		public WasReplicatedEvent()
		{
			this.ID = WasReplicatedEvent.ID;
		}

		// Token: 0x06005626 RID: 22054 RVA: 0x0019A65A File Offset: 0x0019885A
		public static int CountPool()
		{
			if (WasReplicatedEvent.Pool != null)
			{
				return WasReplicatedEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06005627 RID: 22055 RVA: 0x0019A66F File Offset: 0x0019886F
		public static void ResetPool()
		{
			while (WasReplicatedEvent.PoolCounter > 0)
			{
				WasReplicatedEvent.Pool[--WasReplicatedEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06005628 RID: 22056 RVA: 0x0019A697 File Offset: 0x00198897
		public static void ResetTo(ref WasReplicatedEvent E)
		{
			MinEvent.ResetTo<WasReplicatedEvent>(E, WasReplicatedEvent.Pool, ref WasReplicatedEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06005629 RID: 22057 RVA: 0x0019A6AD File Offset: 0x001988AD
		public static WasReplicatedEvent FromPool()
		{
			return MinEvent.FromPool<WasReplicatedEvent>(ref WasReplicatedEvent.Pool, ref WasReplicatedEvent.PoolCounter, 8192);
		}

		// Token: 0x0600562A RID: 22058 RVA: 0x0019A6C3 File Offset: 0x001988C3
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x0600562B RID: 22059 RVA: 0x0019A6D7 File Offset: 0x001988D7
		public override void Reset()
		{
			base.Reset();
			this.Replica = null;
		}

		// Token: 0x0600562C RID: 22060 RVA: 0x0019A6E8 File Offset: 0x001988E8
		public static void Send(GameObject Object, GameObject Actor, GameObject Replica, string Context = null, bool Temporary = false)
		{
			if (GameObject.Validate(ref Object) && Object.HasRegisteredEvent("WasReplicated"))
			{
				Event @event = Event.New("WasReplicated", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Context", Context);
				@event.SetParameter("Temporary", Temporary ? 1 : 0);
				@event.SetParameter("Replica", Replica);
				Object.FireEvent(@event);
			}
			if (GameObject.Validate(ref Object) && Object.WantEvent(WasReplicatedEvent.ID, MinEvent.CascadeLevel))
			{
				WasReplicatedEvent wasReplicatedEvent = WasReplicatedEvent.FromPool();
				wasReplicatedEvent.Object = Object;
				wasReplicatedEvent.Actor = Actor;
				wasReplicatedEvent.Replica = Replica;
				wasReplicatedEvent.Context = Context;
				wasReplicatedEvent.Temporary = Temporary;
				Object.HandleEvent(wasReplicatedEvent);
			}
		}

		// Token: 0x04002B65 RID: 11109
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(WasReplicatedEvent), null, new MinEvent.EventPoolCount(WasReplicatedEvent.CountPool), new MinEvent.EventPoolReset(WasReplicatedEvent.ResetPool));

		// Token: 0x04002B66 RID: 11110
		private static List<WasReplicatedEvent> Pool;

		// Token: 0x04002B67 RID: 11111
		private static int PoolCounter;

		// Token: 0x04002B68 RID: 11112
		public GameObject Replica;
	}
}
