using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020006BC RID: 1724
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class CanBeReplicatedEvent : IReplicationEvent
	{
		// Token: 0x06004929 RID: 18729 RVA: 0x00170DB0 File Offset: 0x0016EFB0
		public CanBeReplicatedEvent()
		{
			this.ID = CanBeReplicatedEvent.ID;
		}

		// Token: 0x0600492A RID: 18730 RVA: 0x00170DC3 File Offset: 0x0016EFC3
		public static int CountPool()
		{
			if (CanBeReplicatedEvent.Pool != null)
			{
				return CanBeReplicatedEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x0600492B RID: 18731 RVA: 0x00170DD8 File Offset: 0x0016EFD8
		public static void ResetPool()
		{
			while (CanBeReplicatedEvent.PoolCounter > 0)
			{
				CanBeReplicatedEvent.Pool[--CanBeReplicatedEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x0600492C RID: 18732 RVA: 0x00170E00 File Offset: 0x0016F000
		public static void ResetTo(ref CanBeReplicatedEvent E)
		{
			MinEvent.ResetTo<CanBeReplicatedEvent>(E, CanBeReplicatedEvent.Pool, ref CanBeReplicatedEvent.PoolCounter);
			E = null;
		}

		// Token: 0x0600492D RID: 18733 RVA: 0x00170E16 File Offset: 0x0016F016
		public static CanBeReplicatedEvent FromPool()
		{
			return MinEvent.FromPool<CanBeReplicatedEvent>(ref CanBeReplicatedEvent.Pool, ref CanBeReplicatedEvent.PoolCounter, 8192);
		}

		// Token: 0x0600492E RID: 18734 RVA: 0x00170E2C File Offset: 0x0016F02C
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x0600492F RID: 18735 RVA: 0x00170E40 File Offset: 0x0016F040
		public static bool Check(GameObject Object, GameObject Actor, string Context = null, bool Temporary = false)
		{
			bool flag = true;
			if (GameObject.Validate(ref Object) && Object.HasRegisteredEvent("CanBeReplicated"))
			{
				Event @event = Event.New("CanBeReplicated", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Context", Context);
				@event.SetParameter("Temporary", Temporary ? 1 : 0);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(CanBeReplicatedEvent.ID, MinEvent.CascadeLevel))
			{
				CanBeReplicatedEvent canBeReplicatedEvent = CanBeReplicatedEvent.FromPool();
				canBeReplicatedEvent.Object = Object;
				canBeReplicatedEvent.Actor = Actor;
				canBeReplicatedEvent.Context = Context;
				canBeReplicatedEvent.Temporary = Temporary;
				flag = Object.HandleEvent(canBeReplicatedEvent);
			}
			return flag;
		}

		// Token: 0x04002317 RID: 8983
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(CanBeReplicatedEvent), null, new MinEvent.EventPoolCount(CanBeReplicatedEvent.CountPool), new MinEvent.EventPoolReset(CanBeReplicatedEvent.ResetPool));

		// Token: 0x04002318 RID: 8984
		private static List<CanBeReplicatedEvent> Pool;

		// Token: 0x04002319 RID: 8985
		private static int PoolCounter;
	}
}
