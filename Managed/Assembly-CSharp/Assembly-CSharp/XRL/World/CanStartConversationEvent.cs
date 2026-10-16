using System;

namespace XRL.World
{
	// Token: 0x020006CD RID: 1741
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class CanStartConversationEvent : PooledEvent<CanStartConversationEvent>
	{
		// Token: 0x06004997 RID: 18839 RVA: 0x0017206F File Offset: 0x0017026F
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004998 RID: 18840 RVA: 0x00172078 File Offset: 0x00170278
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Object = null;
			this.Physical = false;
			this.Mental = false;
			this.FailureMessage = null;
		}

		// Token: 0x06004999 RID: 18841 RVA: 0x001720A3 File Offset: 0x001702A3
		public static CanStartConversationEvent FromPool(GameObject Actor, GameObject Object, bool Physical = false, bool Mental = false)
		{
			CanStartConversationEvent canStartConversationEvent = PooledEvent<CanStartConversationEvent>.FromPool();
			canStartConversationEvent.Actor = Actor;
			canStartConversationEvent.Object = Object;
			canStartConversationEvent.Physical = Physical;
			canStartConversationEvent.Mental = Mental;
			canStartConversationEvent.FailureMessage = null;
			return canStartConversationEvent;
		}

		// Token: 0x0600499A RID: 18842 RVA: 0x001720D0 File Offset: 0x001702D0
		public static bool Check(GameObject Actor, GameObject Object, out string FailureMessage, bool Physical = false, bool Mental = false)
		{
			FailureMessage = null;
			if (Actor.HasRegisteredEvent("CanStartConversation") || Object.HasRegisteredEvent("CanStartConversation"))
			{
				Event @event = Event.New("CanStartConversation", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Object", Object);
				@event.SetFlag("Physical", Physical);
				@event.SetFlag("Mental", Mental);
				try
				{
					if (!Actor.FireEvent(@event))
					{
						return false;
					}
					if (!Object.FireEvent(@event))
					{
						return false;
					}
				}
				finally
				{
					FailureMessage = @event.GetStringParameter("FailureMessage", null);
				}
			}
			bool flag = Actor.WantEvent(PooledEvent<CanStartConversationEvent>.ID, MinEvent.CascadeLevel);
			bool flag2 = Object.WantEvent(PooledEvent<CanStartConversationEvent>.ID, MinEvent.CascadeLevel);
			if (flag || flag2)
			{
				CanStartConversationEvent canStartConversationEvent = CanStartConversationEvent.FromPool(Actor, Object, Physical, Mental);
				try
				{
					if (flag && !Actor.HandleEvent(canStartConversationEvent))
					{
						return false;
					}
					if (flag2 && !Object.HandleEvent(canStartConversationEvent))
					{
						return false;
					}
				}
				finally
				{
					FailureMessage = canStartConversationEvent.FailureMessage;
				}
			}
			return true;
		}

		// Token: 0x0400235D RID: 9053
		public GameObject Actor;

		// Token: 0x0400235E RID: 9054
		public GameObject Object;

		// Token: 0x0400235F RID: 9055
		public bool Physical;

		// Token: 0x04002360 RID: 9056
		public bool Mental;

		// Token: 0x04002361 RID: 9057
		public string FailureMessage;
	}
}
