using System;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C0B RID: 3083
	public class KithAndKinRumor : IConversationPart
	{
		// Token: 0x06007BB6 RID: 31670 RVA: 0x002E79CB File Offset: 0x002E5BCB
		public override bool LoadChild(ConversationXMLBlueprint Blueprint)
		{
			this.Blueprint = Blueprint;
			return base.LoadChild(Blueprint);
		}

		// Token: 0x06007BB7 RID: 31671 RVA: 0x002E79DC File Offset: 0x002E5BDC
		public override void Awake()
		{
			string id = The.Conversation.ID;
			if (id == "HindrenVillager")
			{
				this.Rumor = KithAndKinGameState.Instance.getRumorForVillagerCategory("*villager");
			}
			else if (id == "FaundrenVillager")
			{
				this.Rumor = KithAndKinGameState.Instance.getRumorForVillagerCategory("*faundren");
			}
			else if (id == "HindrenScout")
			{
				this.Rumor = KithAndKinGameState.Instance.getRumorForVillagerCategory("*scout");
			}
			if (this.Rumor != null)
			{
				this.Blueprint.Text = this.Rumor.text;
				this.ParentElement.LoadChild(this.Blueprint);
			}
		}

		// Token: 0x06007BB8 RID: 31672 RVA: 0x002E7A8E File Offset: 0x002E5C8E
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == LeftElementEvent.ID;
		}

		// Token: 0x06007BB9 RID: 31673 RVA: 0x002E7AA4 File Offset: 0x002E5CA4
		public override bool HandleEvent(LeftElementEvent E)
		{
			if (this.Rumor != null)
			{
				this.Rumor.trigger();
				KithAndKinGameState.Instance.foundClue();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003838 RID: 14392
		public ConversationXMLBlueprint Blueprint;

		// Token: 0x04003839 RID: 14393
		public HindrenClueRumor Rumor;
	}
}
