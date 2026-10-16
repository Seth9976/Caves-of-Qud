using System;
using System.Collections.Generic;
using XRL.World.Conversations;

namespace XRL.World
{
	// Token: 0x0200071A RID: 1818
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GaveDirectionsEvent : IConversationMinEvent
	{
		// Token: 0x06004BB2 RID: 19378 RVA: 0x0017790B File Offset: 0x00175B0B
		public GaveDirectionsEvent()
		{
			this.ID = GaveDirectionsEvent.ID;
		}

		// Token: 0x06004BB3 RID: 19379 RVA: 0x0017791E File Offset: 0x00175B1E
		public static int CountPool()
		{
			if (GaveDirectionsEvent.Pool != null)
			{
				return GaveDirectionsEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004BB4 RID: 19380 RVA: 0x00177933 File Offset: 0x00175B33
		public static void ResetPool()
		{
			while (GaveDirectionsEvent.PoolCounter > 0)
			{
				GaveDirectionsEvent.Pool[--GaveDirectionsEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004BB5 RID: 19381 RVA: 0x0017795B File Offset: 0x00175B5B
		public static void ResetTo(ref GaveDirectionsEvent E)
		{
			MinEvent.ResetTo<GaveDirectionsEvent>(E, GaveDirectionsEvent.Pool, ref GaveDirectionsEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004BB6 RID: 19382 RVA: 0x00177971 File Offset: 0x00175B71
		public static GaveDirectionsEvent FromPool()
		{
			return MinEvent.FromPool<GaveDirectionsEvent>(ref GaveDirectionsEvent.Pool, ref GaveDirectionsEvent.PoolCounter, 8192);
		}

		// Token: 0x06004BB7 RID: 19383 RVA: 0x00177987 File Offset: 0x00175B87
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004BB8 RID: 19384 RVA: 0x0017799C File Offset: 0x00175B9C
		public static void Send(GameObject Actor, GameObject SpeakingWith, GameObject Transmitter, GameObject Receiver, Conversation Conversation, bool CanTrade = false, bool Physical = false, bool Mental = false)
		{
			GaveDirectionsEvent gaveDirectionsEvent = GaveDirectionsEvent.FromPool();
			gaveDirectionsEvent.Actor = Actor;
			gaveDirectionsEvent.SpeakingWith = SpeakingWith;
			gaveDirectionsEvent.Transmitter = Transmitter;
			gaveDirectionsEvent.Receiver = Receiver;
			gaveDirectionsEvent.Conversation = Conversation;
			gaveDirectionsEvent.CanTrade = CanTrade;
			gaveDirectionsEvent.Physical = Physical;
			gaveDirectionsEvent.Mental = Mental;
			IConversationMinEvent.DispatchAll<GaveDirectionsEvent>(gaveDirectionsEvent);
		}

		// Token: 0x04002482 RID: 9346
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GaveDirectionsEvent), null, new MinEvent.EventPoolCount(GaveDirectionsEvent.CountPool), new MinEvent.EventPoolReset(GaveDirectionsEvent.ResetPool));

		// Token: 0x04002483 RID: 9347
		private static List<GaveDirectionsEvent> Pool;

		// Token: 0x04002484 RID: 9348
		private static int PoolCounter;
	}
}
