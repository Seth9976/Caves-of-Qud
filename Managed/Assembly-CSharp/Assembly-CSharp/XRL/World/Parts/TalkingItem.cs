using System;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000E44 RID: 3652
	[Serializable]
	public class TalkingItem : IPart
	{
		// Token: 0x06009412 RID: 37906 RVA: 0x003550A2 File Offset: 0x003532A2
		public TalkingItem()
		{
		}

		// Token: 0x06009413 RID: 37907 RVA: 0x003550AA File Offset: 0x003532AA
		public TalkingItem(string ID)
			: this()
		{
			this.ConversationID = ID;
		}

		// Token: 0x06009414 RID: 37908 RVA: 0x003550BC File Offset: 0x003532BC
		public override bool SameAs(IPart p)
		{
			TalkingItem talkingItem = p as TalkingItem;
			return !(talkingItem.ConversationID != this.ConversationID) && talkingItem.ClearLost == this.ClearLost && base.SameAs(p);
		}

		// Token: 0x06009415 RID: 37909 RVA: 0x003550FC File Offset: 0x003532FC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == CanGiveDirectionsEvent.ID && this.ClearLost) || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x06009416 RID: 37910 RVA: 0x0035512A File Offset: 0x0035332A
		public override bool HandleEvent(CanGiveDirectionsEvent E)
		{
			if (E.SpeakingWith == this.ParentObject && this.ClearLost && !E.PlayerCompanion && !E.SpeakingWith.HasEffect<Lost>())
			{
				E.CanGiveDirections = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009417 RID: 37911 RVA: 0x00355168 File Offset: 0x00353368
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (E.Actor != this.ParentObject)
			{
				E.AddAction("Chat", "chat", "Chat", null, 'h', false, 10, 0, false, false, false, true, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009418 RID: 37912 RVA: 0x003551AE File Offset: 0x003533AE
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Chat" && E.Actor != this.ParentObject)
			{
				ConversationUI.HaveConversation(this.ConversationID, this.ParentObject, null, null, null, false, false, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003D42 RID: 15682
		public string ConversationID;

		// Token: 0x04003D43 RID: 15683
		public bool ClearLost;
	}
}
