using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000848 RID: 2120
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class ReplicaCreatedEvent : IReplicationEvent
	{
		// Token: 0x0600541B RID: 21531 RVA: 0x00191228 File Offset: 0x0018F428
		public ReplicaCreatedEvent()
		{
			this.ID = ReplicaCreatedEvent.ID;
		}

		// Token: 0x0600541C RID: 21532 RVA: 0x0019123B File Offset: 0x0018F43B
		public override int GetCascadeLevel()
		{
			return ReplicaCreatedEvent.CascadeLevel;
		}

		// Token: 0x0600541D RID: 21533 RVA: 0x00191242 File Offset: 0x0018F442
		public static int CountPool()
		{
			if (ReplicaCreatedEvent.Pool != null)
			{
				return ReplicaCreatedEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x0600541E RID: 21534 RVA: 0x00191257 File Offset: 0x0018F457
		public static void ResetPool()
		{
			while (ReplicaCreatedEvent.PoolCounter > 0)
			{
				ReplicaCreatedEvent.Pool[--ReplicaCreatedEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x0600541F RID: 21535 RVA: 0x0019127F File Offset: 0x0018F47F
		public static void ResetTo(ref ReplicaCreatedEvent E)
		{
			MinEvent.ResetTo<ReplicaCreatedEvent>(E, ReplicaCreatedEvent.Pool, ref ReplicaCreatedEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06005420 RID: 21536 RVA: 0x00191295 File Offset: 0x0018F495
		public static ReplicaCreatedEvent FromPool()
		{
			return MinEvent.FromPool<ReplicaCreatedEvent>(ref ReplicaCreatedEvent.Pool, ref ReplicaCreatedEvent.PoolCounter, 8192);
		}

		// Token: 0x06005421 RID: 21537 RVA: 0x001912AB File Offset: 0x0018F4AB
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06005422 RID: 21538 RVA: 0x001912BF File Offset: 0x0018F4BF
		public override void Reset()
		{
			base.Reset();
			this.Original = null;
			this.PartsToRemove = null;
		}

		// Token: 0x06005423 RID: 21539 RVA: 0x001912D5 File Offset: 0x0018F4D5
		public void WantToRemove(IPart Part)
		{
			if (this.PartsToRemove != null)
			{
				if (!this.PartsToRemove.Contains(Part))
				{
					this.PartsToRemove.Add(Part);
					return;
				}
			}
			else
			{
				this.Object.RemovePart(Part);
			}
		}

		// Token: 0x06005424 RID: 21540 RVA: 0x00191308 File Offset: 0x0018F508
		public static void Send(GameObject Object, GameObject Actor, GameObject Original, string Context = null, bool Temporary = false)
		{
			List<IPart> list = null;
			try
			{
				if (GameObject.Validate(ref Object) && Object.HasRegisteredEvent("ReplicaCreated"))
				{
					if (list == null)
					{
						list = new List<IPart>();
					}
					Event @event = Event.New("ReplicaCreated", 0, 0, 0);
					@event.SetParameter("Object", Object);
					@event.SetParameter("Actor", Actor);
					@event.SetParameter("Context", Context);
					@event.SetParameter("Temporary", Temporary ? 1 : 0);
					@event.SetParameter("Original", Original);
					@event.SetParameter("PartsToRemove", list);
					Object.FireEvent(@event);
				}
				if (GameObject.Validate(ref Object) && Object.WantEvent(ReplicaCreatedEvent.ID, ReplicaCreatedEvent.CascadeLevel))
				{
					if (list == null)
					{
						list = new List<IPart>();
					}
					ReplicaCreatedEvent replicaCreatedEvent = ReplicaCreatedEvent.FromPool();
					replicaCreatedEvent.Object = Object;
					replicaCreatedEvent.Actor = Actor;
					replicaCreatedEvent.Original = Original;
					replicaCreatedEvent.Context = Context;
					replicaCreatedEvent.Temporary = Temporary;
					replicaCreatedEvent.PartsToRemove = list;
					Object.HandleEvent(replicaCreatedEvent);
				}
			}
			finally
			{
				if (list != null)
				{
					foreach (IPart part in list)
					{
						try
						{
							Object.RemovePart(part);
						}
						catch (Exception ex)
						{
							MetricsManager.LogError(ex);
						}
					}
				}
			}
		}

		// Token: 0x04002A42 RID: 10818
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(ReplicaCreatedEvent), null, new MinEvent.EventPoolCount(ReplicaCreatedEvent.CountPool), new MinEvent.EventPoolReset(ReplicaCreatedEvent.ResetPool));

		// Token: 0x04002A43 RID: 10819
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002A44 RID: 10820
		private static List<ReplicaCreatedEvent> Pool;

		// Token: 0x04002A45 RID: 10821
		private static int PoolCounter;

		// Token: 0x04002A46 RID: 10822
		public GameObject Original;

		// Token: 0x04002A47 RID: 10823
		public List<IPart> PartsToRemove;
	}
}
