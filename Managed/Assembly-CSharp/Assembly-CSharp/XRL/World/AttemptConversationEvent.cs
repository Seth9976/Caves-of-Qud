using System;
using XRL.World.Conversations;

namespace XRL.World
{
	// Token: 0x0200067F RID: 1663
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Singleton)]
	public class AttemptConversationEvent : SingletonEvent<AttemptConversationEvent>
	{
		// Token: 0x0600475A RID: 18266 RVA: 0x00169426 File Offset: 0x00167626
		public override int GetCascadeLevel()
		{
			return AttemptConversationEvent.CascadeLevel;
		}

		// Token: 0x0600475B RID: 18267 RVA: 0x0016942D File Offset: 0x0016762D
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600475C RID: 18268 RVA: 0x00169436 File Offset: 0x00167636
		public override void Reset()
		{
			base.Reset();
			this.Listener = null;
			this.Speaker = null;
			this.Transmitter = null;
			this.Receiver = null;
			this.Blueprint = null;
			this.Silent = false;
		}

		// Token: 0x0600475D RID: 18269 RVA: 0x00169468 File Offset: 0x00167668
		public bool IsParticipant(GameObject Object)
		{
			return Object != null && (Object == this.Listener || Object == this.Speaker);
		}

		// Token: 0x0600475E RID: 18270 RVA: 0x00169483 File Offset: 0x00167683
		public bool IsDevice(GameObject Object)
		{
			return Object != null && (Object == this.Transmitter || Object == this.Receiver);
		}

		// Token: 0x0600475F RID: 18271 RVA: 0x001694A0 File Offset: 0x001676A0
		public static bool Check(ref GameObject Speaker, ref GameObject Listener, ref GameObject Transmitter, ref GameObject Receiver, ref ConversationXMLBlueprint Blueprint, bool Silent)
		{
			SingletonEvent<AttemptConversationEvent>.Instance.Listener = Listener;
			SingletonEvent<AttemptConversationEvent>.Instance.Speaker = Speaker;
			SingletonEvent<AttemptConversationEvent>.Instance.Transmitter = Transmitter;
			SingletonEvent<AttemptConversationEvent>.Instance.Receiver = Receiver;
			SingletonEvent<AttemptConversationEvent>.Instance.Blueprint = Blueprint;
			SingletonEvent<AttemptConversationEvent>.Instance.Silent = Silent;
			bool flag;
			if (Listener.HandleEvent(SingletonEvent<AttemptConversationEvent>.Instance) && Speaker.HandleEvent(SingletonEvent<AttemptConversationEvent>.Instance))
			{
				GameObject gameObject = Transmitter;
				if (gameObject == null || gameObject.HandleEvent(SingletonEvent<AttemptConversationEvent>.Instance))
				{
					GameObject gameObject2 = Receiver;
					flag = gameObject2 == null || gameObject2.HandleEvent(SingletonEvent<AttemptConversationEvent>.Instance);
					goto IL_0090;
				}
			}
			flag = false;
			IL_0090:
			Listener = SingletonEvent<AttemptConversationEvent>.Instance.Listener;
			Speaker = SingletonEvent<AttemptConversationEvent>.Instance.Speaker;
			Transmitter = SingletonEvent<AttemptConversationEvent>.Instance.Transmitter;
			Receiver = SingletonEvent<AttemptConversationEvent>.Instance.Receiver;
			Blueprint = SingletonEvent<AttemptConversationEvent>.Instance.Blueprint;
			SingletonEvent<AttemptConversationEvent>.Instance.Reset();
			return flag;
		}

		// Token: 0x04002205 RID: 8709
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002206 RID: 8710
		public GameObject Listener;

		// Token: 0x04002207 RID: 8711
		public GameObject Speaker;

		// Token: 0x04002208 RID: 8712
		public GameObject Transmitter;

		// Token: 0x04002209 RID: 8713
		public GameObject Receiver;

		// Token: 0x0400220A RID: 8714
		public ConversationXMLBlueprint Blueprint;

		// Token: 0x0400220B RID: 8715
		public bool Silent;
	}
}
