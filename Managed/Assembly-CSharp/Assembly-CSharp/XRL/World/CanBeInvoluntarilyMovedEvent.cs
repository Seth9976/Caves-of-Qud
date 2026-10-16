using System;

namespace XRL.World
{
	// Token: 0x020006B8 RID: 1720
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class CanBeInvoluntarilyMovedEvent : PooledEvent<CanBeInvoluntarilyMovedEvent>
	{
		// Token: 0x0600490B RID: 18699 RVA: 0x00170888 File Offset: 0x0016EA88
		public override int GetCascadeLevel()
		{
			return CanBeInvoluntarilyMovedEvent.CascadeLevel;
		}

		// Token: 0x0600490C RID: 18700 RVA: 0x0017088F File Offset: 0x0016EA8F
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600490D RID: 18701 RVA: 0x00170898 File Offset: 0x0016EA98
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
		}

		// Token: 0x0600490E RID: 18702 RVA: 0x001708A7 File Offset: 0x0016EAA7
		public static CanBeInvoluntarilyMovedEvent FromPool(GameObject Object)
		{
			CanBeInvoluntarilyMovedEvent canBeInvoluntarilyMovedEvent = PooledEvent<CanBeInvoluntarilyMovedEvent>.FromPool();
			canBeInvoluntarilyMovedEvent.Object = Object;
			return canBeInvoluntarilyMovedEvent;
		}

		// Token: 0x0600490F RID: 18703 RVA: 0x001708B8 File Offset: 0x0016EAB8
		public static bool Check(GameObject Object)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("CanBeInvoluntarilyMoved"))
			{
				Event @event = Event.New("CanBeInvoluntarilyMoved", 0, 0, 0);
				@event.SetParameter("Object", Object);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<CanBeInvoluntarilyMovedEvent>.ID, CanBeInvoluntarilyMovedEvent.CascadeLevel))
			{
				flag = Object.HandleEvent(CanBeInvoluntarilyMovedEvent.FromPool(Object));
			}
			return flag;
		}

		// Token: 0x0400230B RID: 8971
		public new static readonly int CascadeLevel = 17;

		// Token: 0x0400230C RID: 8972
		public GameObject Object;
	}
}
