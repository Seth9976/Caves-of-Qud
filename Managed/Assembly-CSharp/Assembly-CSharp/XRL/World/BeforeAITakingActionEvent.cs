using System;

namespace XRL.World
{
	// Token: 0x02000685 RID: 1669
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class BeforeAITakingActionEvent : PooledEvent<BeforeAITakingActionEvent>
	{
		// Token: 0x0600478A RID: 18314 RVA: 0x00169C73 File Offset: 0x00167E73
		public override int GetCascadeLevel()
		{
			return BeforeAITakingActionEvent.CascadeLevel;
		}

		// Token: 0x0600478B RID: 18315 RVA: 0x00169C7A File Offset: 0x00167E7A
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600478C RID: 18316 RVA: 0x00169C83 File Offset: 0x00167E83
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
		}

		// Token: 0x0600478D RID: 18317 RVA: 0x00169C94 File Offset: 0x00167E94
		public static bool Check(GameObject Actor)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("BeforeAITakingAction"))
			{
				Event @event = Event.New("BeforeAITakingAction", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				flag = Actor.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<BeforeAITakingActionEvent>.ID, BeforeAITakingActionEvent.CascadeLevel))
			{
				BeforeAITakingActionEvent beforeAITakingActionEvent = PooledEvent<BeforeAITakingActionEvent>.FromPool();
				beforeAITakingActionEvent.Actor = Actor;
				flag = Actor.HandleEvent(beforeAITakingActionEvent);
			}
			return flag;
		}

		// Token: 0x0400221F RID: 8735
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002220 RID: 8736
		public GameObject Actor;
	}
}
