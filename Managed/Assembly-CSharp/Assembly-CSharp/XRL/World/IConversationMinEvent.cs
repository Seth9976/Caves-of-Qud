using System;
using XRL.World.Conversations;

namespace XRL.World
{
	// Token: 0x020007C3 RID: 1987
	[GameEvent(Base = true, Cascade = 17)]
	public abstract class IConversationMinEvent : MinEvent
	{
		// Token: 0x06005065 RID: 20581 RVA: 0x001868C7 File Offset: 0x00184AC7
		public override int GetCascadeLevel()
		{
			return IConversationMinEvent.CascadeLevel;
		}

		// Token: 0x06005066 RID: 20582 RVA: 0x001868CE File Offset: 0x00184ACE
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005067 RID: 20583 RVA: 0x001868D7 File Offset: 0x00184AD7
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.SpeakingWith = null;
			this.Transmitter = null;
			this.Receiver = null;
			this.Conversation = null;
			this.CanTrade = false;
			this.Physical = false;
			this.Mental = false;
		}

		// Token: 0x06005068 RID: 20584 RVA: 0x00186917 File Offset: 0x00184B17
		public bool IsParticipant(GameObject Object)
		{
			return Object != null && (Object == this.Actor || Object == this.SpeakingWith);
		}

		// Token: 0x06005069 RID: 20585 RVA: 0x00186932 File Offset: 0x00184B32
		public bool IsDevice(GameObject Object)
		{
			return Object != null && (Object == this.Transmitter || Object == this.Receiver);
		}

		// Token: 0x0600506A RID: 20586 RVA: 0x00186950 File Offset: 0x00184B50
		public static bool DispatchAll<T>(T E) where T : IConversationMinEvent
		{
			GameObject actor = E.Actor;
			GameObject speakingWith = E.SpeakingWith;
			GameObject transmitter = E.Transmitter;
			GameObject receiver = E.Receiver;
			XRLGame game = The.Game;
			return (game == null || game.HandleEvent(E)) && (actor == null || actor.HandleEvent(E)) && (speakingWith == null || speakingWith.HandleEvent(E)) && (transmitter == null || transmitter.HandleEvent(E)) && (receiver == null || receiver.HandleEvent(E));
		}

		// Token: 0x040027DA RID: 10202
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040027DB RID: 10203
		public GameObject Actor;

		// Token: 0x040027DC RID: 10204
		public GameObject SpeakingWith;

		// Token: 0x040027DD RID: 10205
		public GameObject Transmitter;

		// Token: 0x040027DE RID: 10206
		public GameObject Receiver;

		// Token: 0x040027DF RID: 10207
		public Conversation Conversation;

		// Token: 0x040027E0 RID: 10208
		public bool CanTrade;

		// Token: 0x040027E1 RID: 10209
		public bool Physical;

		// Token: 0x040027E2 RID: 10210
		public bool Mental;
	}
}
