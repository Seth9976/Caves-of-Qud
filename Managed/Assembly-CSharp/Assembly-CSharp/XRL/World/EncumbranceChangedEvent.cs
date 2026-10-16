using System;

namespace XRL.World
{
	// Token: 0x02000704 RID: 1796
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Pool)]
	public class EncumbranceChangedEvent : PooledEvent<EncumbranceChangedEvent>
	{
		// Token: 0x06004B0A RID: 19210 RVA: 0x00175DB4 File Offset: 0x00173FB4
		public override int GetCascadeLevel()
		{
			return EncumbranceChangedEvent.CascadeLevel;
		}

		// Token: 0x06004B0B RID: 19211 RVA: 0x00175DBB File Offset: 0x00173FBB
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004B0C RID: 19212 RVA: 0x00175DC4 File Offset: 0x00173FC4
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Object = null;
			this.Silent = false;
		}

		// Token: 0x06004B0D RID: 19213 RVA: 0x00175DE4 File Offset: 0x00173FE4
		public static bool Send(GameObject Actor, GameObject Object = null, bool Silent = false)
		{
			bool flag = true;
			if (GameObject.Validate(Actor) && Actor.HasRegisteredEvent("EncumbranceChanged"))
			{
				Event @event = Event.New("EncumbranceChanged", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Object", Object);
				@event.SetSilent(Silent);
				flag = Actor.FireEvent(@event);
			}
			if (flag && GameObject.Validate(Actor) && Actor.WantEvent(PooledEvent<EncumbranceChangedEvent>.ID, EncumbranceChangedEvent.CascadeLevel))
			{
				EncumbranceChangedEvent encumbranceChangedEvent = PooledEvent<EncumbranceChangedEvent>.FromPool();
				encumbranceChangedEvent.Actor = Actor;
				encumbranceChangedEvent.Object = Object;
				encumbranceChangedEvent.Silent = Silent;
				flag = Actor.HandleEvent(encumbranceChangedEvent);
				PooledEvent<EncumbranceChangedEvent>.ResetTo(ref encumbranceChangedEvent);
			}
			return flag;
		}

		// Token: 0x0400243A RID: 9274
		public new static readonly int CascadeLevel = 15;

		// Token: 0x0400243B RID: 9275
		public GameObject Actor;

		// Token: 0x0400243C RID: 9276
		public GameObject Object;

		// Token: 0x0400243D RID: 9277
		public bool Silent;
	}
}
