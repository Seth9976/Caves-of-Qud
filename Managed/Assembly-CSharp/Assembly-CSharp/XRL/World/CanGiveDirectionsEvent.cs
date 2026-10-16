using System;
using System.Collections.Generic;
using XRL.World.Conversations;
using XRL.World.Effects;

namespace XRL.World
{
	// Token: 0x020006C5 RID: 1733
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class CanGiveDirectionsEvent : IConversationMinEvent
	{
		// Token: 0x06004962 RID: 18786 RVA: 0x001717DF File Offset: 0x0016F9DF
		public CanGiveDirectionsEvent()
		{
			this.ID = CanGiveDirectionsEvent.ID;
		}

		// Token: 0x06004963 RID: 18787 RVA: 0x001717F2 File Offset: 0x0016F9F2
		public static int CountPool()
		{
			if (CanGiveDirectionsEvent.Pool != null)
			{
				return CanGiveDirectionsEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004964 RID: 18788 RVA: 0x00171807 File Offset: 0x0016FA07
		public static void ResetPool()
		{
			while (CanGiveDirectionsEvent.PoolCounter > 0)
			{
				CanGiveDirectionsEvent.Pool[--CanGiveDirectionsEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004965 RID: 18789 RVA: 0x0017182F File Offset: 0x0016FA2F
		public static void ResetTo(ref CanGiveDirectionsEvent E)
		{
			MinEvent.ResetTo<CanGiveDirectionsEvent>(E, CanGiveDirectionsEvent.Pool, ref CanGiveDirectionsEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004966 RID: 18790 RVA: 0x00171845 File Offset: 0x0016FA45
		public static CanGiveDirectionsEvent FromPool()
		{
			return MinEvent.FromPool<CanGiveDirectionsEvent>(ref CanGiveDirectionsEvent.Pool, ref CanGiveDirectionsEvent.PoolCounter, 8192);
		}

		// Token: 0x06004967 RID: 18791 RVA: 0x0017185B File Offset: 0x0016FA5B
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004968 RID: 18792 RVA: 0x0017186F File Offset: 0x0016FA6F
		public override void Reset()
		{
			base.Reset();
			this.PlayerCompanion = false;
			this.CanGiveDirections = false;
		}

		// Token: 0x06004969 RID: 18793 RVA: 0x00171888 File Offset: 0x0016FA88
		public static bool Check(GameObject Actor, GameObject SpeakingWith, GameObject Transmitter, GameObject Receiver, Conversation Conversation, bool CanTrade = false, bool Physical = false, bool Mental = false)
		{
			bool flag = !SpeakingWith.IsPlayer() && (SpeakingWith.GetIntProperty("TurnsAsPlayerMinion", 0) > 0 || SpeakingWith.IsPlayerLed());
			bool flag2 = !flag && (SpeakingWith.HasTagOrProperty("ClearLost") || SpeakingWith.IsPlayer()) && !SpeakingWith.HasEffect<Lost>() && !SpeakingWith.HasEffect<Confused>() && !SpeakingWith.HasEffect<FuriouslyConfused>();
			bool flag3 = true;
			if (flag3)
			{
				bool flag4 = GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("CanGiveDirections");
				bool flag5 = GameObject.Validate(ref SpeakingWith) && SpeakingWith.HasRegisteredEvent("CanGiveDirections");
				if (flag4 || flag5)
				{
					Event @event = Event.New("CanGiveDirections", 0, 0, 0);
					@event.SetParameter("Actor", Actor);
					@event.SetParameter("SpeakingWith", SpeakingWith);
					@event.SetParameter("Conversation", Conversation);
					@event.SetFlag("CanTrade", CanTrade);
					@event.SetFlag("Physical", Physical);
					@event.SetFlag("Mental", Mental);
					@event.SetFlag("PlayerCompanion", flag);
					@event.SetFlag("CanGiveDirections", flag2);
					if (flag3 && flag4)
					{
						flag3 = Actor.FireEvent(@event);
					}
					if (flag3 && flag5)
					{
						flag3 = SpeakingWith.FireEvent(@event);
					}
					flag2 = @event.HasFlag("CanGiveDirections");
				}
			}
			if (flag3)
			{
				CanGiveDirectionsEvent canGiveDirectionsEvent = CanGiveDirectionsEvent.FromPool();
				canGiveDirectionsEvent.Actor = Actor;
				canGiveDirectionsEvent.SpeakingWith = SpeakingWith;
				canGiveDirectionsEvent.Transmitter = Transmitter;
				canGiveDirectionsEvent.Receiver = Receiver;
				canGiveDirectionsEvent.Conversation = Conversation;
				canGiveDirectionsEvent.CanTrade = CanTrade;
				canGiveDirectionsEvent.Physical = Physical;
				canGiveDirectionsEvent.Mental = Mental;
				canGiveDirectionsEvent.PlayerCompanion = flag;
				canGiveDirectionsEvent.CanGiveDirections = flag2;
				flag3 = IConversationMinEvent.DispatchAll<CanGiveDirectionsEvent>(canGiveDirectionsEvent);
				flag2 = canGiveDirectionsEvent.CanGiveDirections;
			}
			return flag2;
		}

		// Token: 0x04002340 RID: 9024
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(CanGiveDirectionsEvent), null, new MinEvent.EventPoolCount(CanGiveDirectionsEvent.CountPool), new MinEvent.EventPoolReset(CanGiveDirectionsEvent.ResetPool));

		// Token: 0x04002341 RID: 9025
		private static List<CanGiveDirectionsEvent> Pool;

		// Token: 0x04002342 RID: 9026
		private static int PoolCounter;

		// Token: 0x04002343 RID: 9027
		public bool PlayerCompanion;

		// Token: 0x04002344 RID: 9028
		public bool CanGiveDirections;
	}
}
