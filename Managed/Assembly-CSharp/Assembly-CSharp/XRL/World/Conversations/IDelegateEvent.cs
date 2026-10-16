using System;

namespace XRL.World.Conversations
{
	// Token: 0x02000BD0 RID: 3024
	[ConversationEvent(Base = true)]
	public abstract class IDelegateEvent : ConversationEvent
	{
		// Token: 0x06007986 RID: 31110 RVA: 0x002DCA2E File Offset: 0x002DAC2E
		public IDelegateEvent(int ID)
			: base(ID)
		{
		}

		// Token: 0x06007987 RID: 31111 RVA: 0x002DCA37 File Offset: 0x002DAC37
		public override void Reset()
		{
			base.Reset();
			this.Delegate = null;
			this.Command = null;
		}

		// Token: 0x0400378E RID: 14222
		[ConversationEvent.ParameterAttribute(Required = true)]
		public string Delegate;

		// Token: 0x0400378F RID: 14223
		[ConversationEvent.ParameterAttribute(Required = true)]
		public string Command;
	}
}
