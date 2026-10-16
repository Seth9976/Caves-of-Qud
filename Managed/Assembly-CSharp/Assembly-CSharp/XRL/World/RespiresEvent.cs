using System;

namespace XRL.World
{
	// Token: 0x0200084A RID: 2122
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class RespiresEvent : PooledEvent<RespiresEvent>
	{
		// Token: 0x0600542C RID: 21548 RVA: 0x00191671 File Offset: 0x0018F871
		public override int GetCascadeLevel()
		{
			return RespiresEvent.CascadeLevel;
		}

		// Token: 0x0600542D RID: 21549 RVA: 0x00191678 File Offset: 0x0018F878
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600542E RID: 21550 RVA: 0x00191681 File Offset: 0x0018F881
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
		}

		// Token: 0x0600542F RID: 21551 RVA: 0x00191690 File Offset: 0x0018F890
		public static bool Check(GameObject Object)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("Respires"))
			{
				Event @event = Event.New("Respires", 0, 0, 0);
				@event.SetParameter("Object", Object);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<RespiresEvent>.ID, RespiresEvent.CascadeLevel))
			{
				RespiresEvent respiresEvent = PooledEvent<RespiresEvent>.FromPool();
				respiresEvent.Object = Object;
				flag = Object.HandleEvent(respiresEvent);
			}
			return flag;
		}

		// Token: 0x04002A50 RID: 10832
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002A51 RID: 10833
		public GameObject Object;
	}
}
