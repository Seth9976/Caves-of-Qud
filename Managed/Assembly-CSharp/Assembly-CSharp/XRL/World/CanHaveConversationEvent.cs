using System;
using System.Collections.Generic;
using XRL.World.Conversations;

namespace XRL.World
{
	// Token: 0x020006C6 RID: 1734
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class CanHaveConversationEvent : IConversationMinEvent
	{
		// Token: 0x0600496B RID: 18795 RVA: 0x00171A6C File Offset: 0x0016FC6C
		public CanHaveConversationEvent()
		{
			this.ID = CanHaveConversationEvent.ID;
		}

		// Token: 0x0600496C RID: 18796 RVA: 0x00171A7F File Offset: 0x0016FC7F
		public override int GetCascadeLevel()
		{
			return CanHaveConversationEvent.CascadeLevel;
		}

		// Token: 0x0600496D RID: 18797 RVA: 0x00171A86 File Offset: 0x0016FC86
		public static int CountPool()
		{
			if (CanHaveConversationEvent.Pool != null)
			{
				return CanHaveConversationEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x0600496E RID: 18798 RVA: 0x00171A9B File Offset: 0x0016FC9B
		public static void ResetPool()
		{
			while (CanHaveConversationEvent.PoolCounter > 0)
			{
				CanHaveConversationEvent.Pool[--CanHaveConversationEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x0600496F RID: 18799 RVA: 0x00171AC3 File Offset: 0x0016FCC3
		public static void ResetTo(ref CanHaveConversationEvent E)
		{
			MinEvent.ResetTo<CanHaveConversationEvent>(E, CanHaveConversationEvent.Pool, ref CanHaveConversationEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004970 RID: 18800 RVA: 0x00171AD9 File Offset: 0x0016FCD9
		public static CanHaveConversationEvent FromPool()
		{
			return MinEvent.FromPool<CanHaveConversationEvent>(ref CanHaveConversationEvent.Pool, ref CanHaveConversationEvent.PoolCounter, 8192);
		}

		// Token: 0x06004971 RID: 18801 RVA: 0x00171AEF File Offset: 0x0016FCEF
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004972 RID: 18802 RVA: 0x00171B04 File Offset: 0x0016FD04
		public static bool Check(GameObject Actor, GameObject SpeakingWith, GameObject Transmitter, GameObject Receiver, Conversation Conversation, bool CanTrade = false, bool Physical = false, bool Mental = false)
		{
			bool flag = true;
			if (flag)
			{
				bool flag2 = GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("CanHaveConversation");
				bool flag3 = GameObject.Validate(ref SpeakingWith) && SpeakingWith.HasRegisteredEvent("CanHaveConversation");
				if (flag2 || flag3)
				{
					Event @event = Event.New("CanHaveConversation", 0, 0, 0);
					@event.SetParameter("Actor", Actor);
					@event.SetParameter("SpeakingWith", SpeakingWith);
					@event.SetParameter("Conversation", Conversation);
					@event.SetFlag("CanTrade", CanTrade);
					@event.SetFlag("Physical", Physical);
					@event.SetFlag("Mental", Mental);
					if (flag && flag2)
					{
						flag = Actor.FireEvent(@event);
					}
					if (flag && flag3)
					{
						flag = SpeakingWith.FireEvent(@event);
					}
				}
			}
			if (flag)
			{
				CanHaveConversationEvent canHaveConversationEvent = CanHaveConversationEvent.FromPool();
				canHaveConversationEvent.Actor = Actor;
				canHaveConversationEvent.SpeakingWith = SpeakingWith;
				canHaveConversationEvent.Transmitter = Transmitter;
				canHaveConversationEvent.Receiver = Receiver;
				canHaveConversationEvent.Conversation = Conversation;
				canHaveConversationEvent.CanTrade = CanTrade;
				canHaveConversationEvent.Physical = Physical;
				canHaveConversationEvent.Mental = Mental;
				flag = IConversationMinEvent.DispatchAll<CanHaveConversationEvent>(canHaveConversationEvent);
			}
			return flag;
		}

		// Token: 0x04002345 RID: 9029
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(CanHaveConversationEvent), null, new MinEvent.EventPoolCount(CanHaveConversationEvent.CountPool), new MinEvent.EventPoolReset(CanHaveConversationEvent.ResetPool));

		// Token: 0x04002346 RID: 9030
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002347 RID: 9031
		private static List<CanHaveConversationEvent> Pool;

		// Token: 0x04002348 RID: 9032
		private static int PoolCounter;
	}
}
