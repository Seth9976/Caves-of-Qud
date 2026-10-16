using System;

namespace XRL.World
{
	// Token: 0x02000641 RID: 1601
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Pool)]
	public class AIBoredEvent : PooledEvent<AIBoredEvent>
	{
		// Token: 0x0600458C RID: 17804 RVA: 0x00163F6D File Offset: 0x0016216D
		public override int GetCascadeLevel()
		{
			return AIBoredEvent.CascadeLevel;
		}

		// Token: 0x0600458D RID: 17805 RVA: 0x00163F74 File Offset: 0x00162174
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600458E RID: 17806 RVA: 0x00163F7D File Offset: 0x0016217D
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
		}

		// Token: 0x0600458F RID: 17807 RVA: 0x00163F8C File Offset: 0x0016218C
		public static bool Check(GameObject Actor)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("AIBored"))
			{
				Event @event = Event.New("AIBored", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				flag = Actor.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<AIBoredEvent>.ID, AIBoredEvent.CascadeLevel))
			{
				AIBoredEvent aiboredEvent = PooledEvent<AIBoredEvent>.FromPool();
				aiboredEvent.Actor = Actor;
				flag = Actor.HandleEvent(aiboredEvent);
			}
			return flag;
		}

		// Token: 0x04002106 RID: 8454
		public new static readonly int CascadeLevel = 15;

		// Token: 0x04002107 RID: 8455
		public GameObject Actor;
	}
}
