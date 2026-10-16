using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using XRL.World.Conversations;

namespace XRL.World
{
	// Token: 0x020006A9 RID: 1705
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class BeginConversationEvent : IConversationMinEvent
	{
		// Token: 0x06004893 RID: 18579 RVA: 0x0016CC36 File Offset: 0x0016AE36
		public BeginConversationEvent()
		{
			this.ID = BeginConversationEvent.ID;
		}

		// Token: 0x06004894 RID: 18580 RVA: 0x0016CC49 File Offset: 0x0016AE49
		public static int CountPool()
		{
			if (BeginConversationEvent.Pool != null)
			{
				return BeginConversationEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004895 RID: 18581 RVA: 0x0016CC5E File Offset: 0x0016AE5E
		public static void ResetPool()
		{
			while (BeginConversationEvent.PoolCounter > 0)
			{
				BeginConversationEvent.Pool[--BeginConversationEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004896 RID: 18582 RVA: 0x0016CC86 File Offset: 0x0016AE86
		public static void ResetTo(ref BeginConversationEvent E)
		{
			MinEvent.ResetTo<BeginConversationEvent>(E, BeginConversationEvent.Pool, ref BeginConversationEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004897 RID: 18583 RVA: 0x0016CC9C File Offset: 0x0016AE9C
		public static BeginConversationEvent FromPool()
		{
			return MinEvent.FromPool<BeginConversationEvent>(ref BeginConversationEvent.Pool, ref BeginConversationEvent.PoolCounter, 8192);
		}

		// Token: 0x06004898 RID: 18584 RVA: 0x0016CCB2 File Offset: 0x0016AEB2
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004899 RID: 18585 RVA: 0x0016CCC6 File Offset: 0x0016AEC6
		public override void Reset()
		{
			base.Reset();
			this.StartNode = null;
			this.Icon = null;
		}

		// Token: 0x0600489A RID: 18586 RVA: 0x0016CCDC File Offset: 0x0016AEDC
		public static bool Check(GameObject Actor, GameObject SpeakingWith, GameObject Transmitter, GameObject Receiver, Conversation Conversation, Node StartNode, ref IRenderable Icon, bool CanTrade = false, bool Physical = false, bool Mental = false)
		{
			bool flag = true;
			if (flag)
			{
				bool flag2 = GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("BeginConversation");
				bool flag3 = GameObject.Validate(ref SpeakingWith) && SpeakingWith.HasRegisteredEvent("BeginConversation");
				if (flag2 || flag3)
				{
					Event @event = Event.New("BeginConversation", 0, 0, 0);
					@event.SetParameter("Actor", Actor);
					@event.SetParameter("SpeakingWith", SpeakingWith);
					@event.SetParameter("Conversation", Conversation);
					@event.SetParameter("Icon", Icon);
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
					Icon = @event.GetParameter<IRenderable>("Icon");
				}
			}
			if (flag)
			{
				BeginConversationEvent beginConversationEvent = BeginConversationEvent.FromPool();
				beginConversationEvent.Actor = Actor;
				beginConversationEvent.SpeakingWith = SpeakingWith;
				beginConversationEvent.Transmitter = Transmitter;
				beginConversationEvent.Receiver = Receiver;
				beginConversationEvent.Conversation = Conversation;
				beginConversationEvent.StartNode = StartNode;
				beginConversationEvent.Icon = Icon;
				beginConversationEvent.CanTrade = CanTrade;
				beginConversationEvent.Physical = Physical;
				beginConversationEvent.Mental = Mental;
				flag = IConversationMinEvent.DispatchAll<BeginConversationEvent>(beginConversationEvent);
				Icon = beginConversationEvent.Icon;
			}
			return flag;
		}

		// Token: 0x040022C8 RID: 8904
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(BeginConversationEvent), null, new MinEvent.EventPoolCount(BeginConversationEvent.CountPool), new MinEvent.EventPoolReset(BeginConversationEvent.ResetPool));

		// Token: 0x040022C9 RID: 8905
		private static List<BeginConversationEvent> Pool;

		// Token: 0x040022CA RID: 8906
		private static int PoolCounter;

		// Token: 0x040022CB RID: 8907
		public Node StartNode;

		// Token: 0x040022CC RID: 8908
		public IRenderable Icon;
	}
}
