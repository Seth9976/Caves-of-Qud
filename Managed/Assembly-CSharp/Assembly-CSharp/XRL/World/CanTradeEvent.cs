using System;
using System.Collections.Generic;
using XRL.World.Conversations;

namespace XRL.World
{
	// Token: 0x020006CF RID: 1743
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class CanTradeEvent : IConversationMinEvent
	{
		// Token: 0x060049A2 RID: 18850 RVA: 0x001722C0 File Offset: 0x001704C0
		public CanTradeEvent()
		{
			this.ID = CanTradeEvent.ID;
		}

		// Token: 0x060049A3 RID: 18851 RVA: 0x001722D3 File Offset: 0x001704D3
		public static int CountPool()
		{
			if (CanTradeEvent.Pool != null)
			{
				return CanTradeEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060049A4 RID: 18852 RVA: 0x001722E8 File Offset: 0x001704E8
		public static void ResetPool()
		{
			while (CanTradeEvent.PoolCounter > 0)
			{
				CanTradeEvent.Pool[--CanTradeEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060049A5 RID: 18853 RVA: 0x00172310 File Offset: 0x00170510
		public static void ResetTo(ref CanTradeEvent E)
		{
			MinEvent.ResetTo<CanTradeEvent>(E, CanTradeEvent.Pool, ref CanTradeEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060049A6 RID: 18854 RVA: 0x00172326 File Offset: 0x00170526
		public static CanTradeEvent FromPool()
		{
			return MinEvent.FromPool<CanTradeEvent>(ref CanTradeEvent.Pool, ref CanTradeEvent.PoolCounter, 8192);
		}

		// Token: 0x060049A7 RID: 18855 RVA: 0x0017233C File Offset: 0x0017053C
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060049A8 RID: 18856 RVA: 0x00172350 File Offset: 0x00170550
		public override void Reset()
		{
			base.Reset();
			this.Trader = null;
			this.Base = false;
		}

		// Token: 0x060049A9 RID: 18857 RVA: 0x00172368 File Offset: 0x00170568
		public static bool Check(GameObject Actor, GameObject SpeakingWith, GameObject Transmitter, GameObject Receiver, ref GameObject Trader, Conversation Conversation, bool Base, bool Physical = false, bool Mental = false)
		{
			bool flag = true;
			bool flag2 = Base;
			if (flag)
			{
				bool flag3 = GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("CanTrade");
				bool flag4 = GameObject.Validate(ref SpeakingWith) && SpeakingWith.HasRegisteredEvent("CanTrade");
				if (flag3 || flag4)
				{
					Event @event = Event.New("CanTrade", 0, 0, 0);
					@event.SetParameter("Actor", Actor);
					@event.SetParameter("SpeakingWith", SpeakingWith);
					@event.SetParameter("Conversation", Conversation);
					@event.SetFlag("Base", Base);
					@event.SetFlag("Physical", Physical);
					@event.SetFlag("Mental", Mental);
					@event.SetFlag("CanTrade", flag2);
					if (flag && flag3)
					{
						flag = Actor.FireEvent(@event);
					}
					if (flag && flag4)
					{
						flag = SpeakingWith.FireEvent(@event);
					}
					flag2 = @event.HasFlag("CanTrade");
				}
			}
			if (flag)
			{
				CanTradeEvent canTradeEvent = CanTradeEvent.FromPool();
				canTradeEvent.Actor = Actor;
				canTradeEvent.SpeakingWith = SpeakingWith;
				canTradeEvent.Transmitter = Transmitter;
				canTradeEvent.Receiver = Receiver;
				canTradeEvent.Trader = Trader;
				canTradeEvent.Conversation = Conversation;
				canTradeEvent.Base = Base;
				canTradeEvent.Physical = Physical;
				canTradeEvent.Mental = Mental;
				canTradeEvent.CanTrade = flag2;
				flag = IConversationMinEvent.DispatchAll<CanTradeEvent>(canTradeEvent);
				Trader = canTradeEvent.Trader;
				flag2 = canTradeEvent.CanTrade;
			}
			return flag2;
		}

		// Token: 0x04002365 RID: 9061
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(CanTradeEvent), null, new MinEvent.EventPoolCount(CanTradeEvent.CountPool), new MinEvent.EventPoolReset(CanTradeEvent.ResetPool));

		// Token: 0x04002366 RID: 9062
		private static List<CanTradeEvent> Pool;

		// Token: 0x04002367 RID: 9063
		private static int PoolCounter;

		// Token: 0x04002368 RID: 9064
		public GameObject Trader;

		// Token: 0x04002369 RID: 9065
		public bool Base;
	}
}
