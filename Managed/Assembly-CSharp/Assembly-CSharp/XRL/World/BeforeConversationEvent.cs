using System;
using System.Collections.Generic;
using XRL.World.Conversations;

namespace XRL.World
{
	// Token: 0x0200068C RID: 1676
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class BeforeConversationEvent : IConversationMinEvent
	{
		// Token: 0x060047C2 RID: 18370 RVA: 0x0016A573 File Offset: 0x00168773
		public BeforeConversationEvent()
		{
			this.ID = BeforeConversationEvent.ID;
		}

		// Token: 0x060047C3 RID: 18371 RVA: 0x0016A586 File Offset: 0x00168786
		public static int CountPool()
		{
			if (BeforeConversationEvent.Pool != null)
			{
				return BeforeConversationEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060047C4 RID: 18372 RVA: 0x0016A59B File Offset: 0x0016879B
		public static void ResetPool()
		{
			while (BeforeConversationEvent.PoolCounter > 0)
			{
				BeforeConversationEvent.Pool[--BeforeConversationEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060047C5 RID: 18373 RVA: 0x0016A5C3 File Offset: 0x001687C3
		public static void ResetTo(ref BeforeConversationEvent E)
		{
			MinEvent.ResetTo<BeforeConversationEvent>(E, BeforeConversationEvent.Pool, ref BeforeConversationEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060047C6 RID: 18374 RVA: 0x0016A5D9 File Offset: 0x001687D9
		public static BeforeConversationEvent FromPool()
		{
			return MinEvent.FromPool<BeforeConversationEvent>(ref BeforeConversationEvent.Pool, ref BeforeConversationEvent.PoolCounter, 8192);
		}

		// Token: 0x060047C7 RID: 18375 RVA: 0x0016A5EF File Offset: 0x001687EF
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060047C8 RID: 18376 RVA: 0x0016A604 File Offset: 0x00168804
		public static bool Check(GameObject Actor, GameObject SpeakingWith, GameObject Transmitter, GameObject Receiver, Conversation Conversation, bool CanTrade = false, bool Physical = false, bool Mental = false)
		{
			bool flag = true;
			if (flag)
			{
				bool flag2 = GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("BeforeConversation");
				bool flag3 = GameObject.Validate(ref SpeakingWith) && SpeakingWith.HasRegisteredEvent("BeforeConversation");
				if (flag2 || flag3)
				{
					Event @event = Event.New("BeforeConversation", 0, 0, 0);
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
				BeforeConversationEvent beforeConversationEvent = BeforeConversationEvent.FromPool();
				beforeConversationEvent.Actor = Actor;
				beforeConversationEvent.SpeakingWith = SpeakingWith;
				beforeConversationEvent.Transmitter = Transmitter;
				beforeConversationEvent.Receiver = Receiver;
				beforeConversationEvent.Conversation = Conversation;
				beforeConversationEvent.CanTrade = CanTrade;
				beforeConversationEvent.Physical = Physical;
				beforeConversationEvent.Mental = Mental;
				flag = IConversationMinEvent.DispatchAll<BeforeConversationEvent>(beforeConversationEvent);
			}
			return flag;
		}

		// Token: 0x04002235 RID: 8757
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(BeforeConversationEvent), null, new MinEvent.EventPoolCount(BeforeConversationEvent.CountPool), new MinEvent.EventPoolReset(BeforeConversationEvent.ResetPool));

		// Token: 0x04002236 RID: 8758
		private static List<BeforeConversationEvent> Pool;

		// Token: 0x04002237 RID: 8759
		private static int PoolCounter;
	}
}
