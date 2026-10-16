using System;

namespace XRL.World.Parts
{
	// Token: 0x02000E42 RID: 3650
	[Serializable]
	public class PriorityChat : IPart
	{
		// Token: 0x060093F4 RID: 37876 RVA: 0x003540D9 File Offset: 0x003522D9
		public override bool WantEvent(int ID, int cascade)
		{
			return ID == CanSmartUseEvent.ID || ID == CommandSmartUseEvent.ID;
		}

		// Token: 0x060093F5 RID: 37877 RVA: 0x003540ED File Offset: 0x003522ED
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return (!E.Actor.IsPlayer() || !this.ParentObject.HasPart<ConversationScript>()) && base.HandleEvent(E);
		}

		// Token: 0x060093F6 RID: 37878 RVA: 0x00354114 File Offset: 0x00352314
		public override bool HandleEvent(CommandSmartUseEvent E)
		{
			ConversationScript conversationScript;
			return (!E.Actor.IsPlayer() || !this.ParentObject.TryGetPart<ConversationScript>(out conversationScript) || !conversationScript.AttemptConversation(false, null, null)) && base.HandleEvent(E);
		}
	}
}
