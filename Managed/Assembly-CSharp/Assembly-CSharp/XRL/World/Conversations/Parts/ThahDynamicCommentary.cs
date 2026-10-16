using System;
using HistoryKit;
using Qud.API;
using XRL.World.Quests;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C1C RID: 3100
	public class ThahDynamicCommentary : IConversationPart
	{
		// Token: 0x06007C13 RID: 31763 RVA: 0x002E9C53 File Offset: 0x002E7E53
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == EnterElementEvent.ID || ID == IsElementVisibleEvent.ID;
		}

		// Token: 0x06007C14 RID: 31764 RVA: 0x002E9C74 File Offset: 0x002E7E74
		public override bool LoadChild(ConversationXMLBlueprint Blueprint)
		{
			if (Blueprint.Name == "Choice")
			{
				this.ChoiceBlueprint = Blueprint;
				this.ChoiceTextBlueprint = Blueprint.GetChild("Text");
			}
			else if (Blueprint.Name == "Text")
			{
				this.TextBlueprint = Blueprint;
			}
			return base.LoadChild(Blueprint);
		}

		// Token: 0x06007C15 RID: 31765 RVA: 0x002E9CD0 File Offset: 0x002E7ED0
		public override void Awake()
		{
			LandingPadsSystem system = The.Game.GetSystem<LandingPadsSystem>();
			if (system == null)
			{
				return;
			}
			foreach (string text in system.candidateFactions)
			{
				if (this.ParentElement.GetElementByID(text) == null)
				{
					HistoricEntitySnapshot villageSnapshot = HistoryAPI.GetVillageSnapshot(text);
					if (villageSnapshot != null)
					{
						string text2 = this.TextBlueprint.Text;
						this.TextBlueprint.ID = text;
						this.TextBlueprint.Text = HistoryAPI.ExpandVillageText(text2, null, villageSnapshot);
						this.TextBlueprint.Attributes["IfLastChoice"] = text;
						this.ParentElement.LoadChild(this.TextBlueprint);
						this.TextBlueprint.Text = text2;
						text2 = this.ChoiceTextBlueprint.Text;
						this.ChoiceBlueprint.ID = text;
						this.ChoiceTextBlueprint.Text = HistoryAPI.ExpandVillageText(text2, null, villageSnapshot);
						this.ParentElement.LoadChild(this.ChoiceBlueprint);
						this.ChoiceTextBlueprint.Text = text2;
					}
				}
			}
		}

		// Token: 0x0400386E RID: 14446
		public ConversationXMLBlueprint ChoiceBlueprint;

		// Token: 0x0400386F RID: 14447
		public ConversationXMLBlueprint ChoiceTextBlueprint;

		// Token: 0x04003870 RID: 14448
		public ConversationXMLBlueprint TextBlueprint;
	}
}
