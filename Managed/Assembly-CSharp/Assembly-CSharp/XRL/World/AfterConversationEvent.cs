using System;
using System.Collections.Generic;
using XRL.World.Conversations;

namespace XRL.World
{
	// Token: 0x0200065A RID: 1626
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AfterConversationEvent : IConversationMinEvent
	{
		// Token: 0x0600464E RID: 17998 RVA: 0x0016626A File Offset: 0x0016446A
		public AfterConversationEvent()
		{
			this.ID = AfterConversationEvent.ID;
		}

		// Token: 0x0600464F RID: 17999 RVA: 0x0016627D File Offset: 0x0016447D
		public static int CountPool()
		{
			if (AfterConversationEvent.Pool != null)
			{
				return AfterConversationEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004650 RID: 18000 RVA: 0x00166292 File Offset: 0x00164492
		public static void ResetPool()
		{
			while (AfterConversationEvent.PoolCounter > 0)
			{
				AfterConversationEvent.Pool[--AfterConversationEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004651 RID: 18001 RVA: 0x001662BA File Offset: 0x001644BA
		public static void ResetTo(ref AfterConversationEvent E)
		{
			MinEvent.ResetTo<AfterConversationEvent>(E, AfterConversationEvent.Pool, ref AfterConversationEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004652 RID: 18002 RVA: 0x001662D0 File Offset: 0x001644D0
		public static AfterConversationEvent FromPool()
		{
			return MinEvent.FromPool<AfterConversationEvent>(ref AfterConversationEvent.Pool, ref AfterConversationEvent.PoolCounter, 8192);
		}

		// Token: 0x06004653 RID: 18003 RVA: 0x001662E6 File Offset: 0x001644E6
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004654 RID: 18004 RVA: 0x001662FC File Offset: 0x001644FC
		public static void Send(GameObject Actor, GameObject SpeakingWith, GameObject Transmitter, GameObject Receiver, Conversation Conversation, bool CanTrade = false, bool Physical = false, bool Mental = false)
		{
			bool flag = true;
			if (flag)
			{
				bool flag2 = GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("AfterConversation");
				bool flag3 = GameObject.Validate(ref SpeakingWith) && SpeakingWith.HasRegisteredEvent("AfterConversation");
				if (flag2 || flag3)
				{
					Event @event = Event.New("AfterConversation", 0, 0, 0);
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
				AfterConversationEvent afterConversationEvent = AfterConversationEvent.FromPool();
				afterConversationEvent.Actor = Actor;
				afterConversationEvent.SpeakingWith = SpeakingWith;
				afterConversationEvent.Transmitter = Transmitter;
				afterConversationEvent.Receiver = Receiver;
				afterConversationEvent.Conversation = Conversation;
				afterConversationEvent.CanTrade = CanTrade;
				afterConversationEvent.Physical = Physical;
				afterConversationEvent.Mental = Mental;
				flag = IConversationMinEvent.DispatchAll<AfterConversationEvent>(afterConversationEvent);
			}
		}

		// Token: 0x04002162 RID: 8546
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AfterConversationEvent), null, new MinEvent.EventPoolCount(AfterConversationEvent.CountPool), new MinEvent.EventPoolReset(AfterConversationEvent.ResetPool));

		// Token: 0x04002163 RID: 8547
		private static List<AfterConversationEvent> Pool;

		// Token: 0x04002164 RID: 8548
		private static int PoolCounter;
	}
}
