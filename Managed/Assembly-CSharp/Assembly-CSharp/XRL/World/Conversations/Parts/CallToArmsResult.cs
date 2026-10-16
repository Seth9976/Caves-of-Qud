using System;
using XRL.World.Parts;
using XRL.World.ZoneParts;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000BF5 RID: 3061
	public class CallToArmsResult : IConversationPart
	{
		// Token: 0x06007B05 RID: 31493 RVA: 0x002E31BE File Offset: 0x002E13BE
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == PrepareTextEvent.ID || ID == LeftElementEvent.ID;
		}

		// Token: 0x06007B06 RID: 31494 RVA: 0x002E31DC File Offset: 0x002E13DC
		public override bool HandleEvent(PrepareTextEvent E)
		{
			E.Text.Insert(0, "\n\n");
			E.Text.Insert(0, ScriptCallToArms.GenerateResultConversation());
			return base.HandleEvent(E);
		}

		// Token: 0x06007B07 RID: 31495 RVA: 0x002E320C File Offset: 0x002E140C
		public override bool HandleEvent(LeftElementEvent E)
		{
			if (!The.Game.HasFinishedQuestStep("The Assessment", "Speak to Otho"))
			{
				The.Game.FinishQuestStep("The Assessment", "Speak to Otho", -1, true, null);
				if (this.Reward == "Top")
				{
					CallToArmsScore.GiveCallToArmsReward_Top();
				}
				else if (this.Reward == "Mid")
				{
					CallToArmsScore.GiveCallToArmsReward_Mid();
				}
				else
				{
					CallToArmsScore.GiveCallToArmsReward_Bottom();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003813 RID: 14355
		public string Reward;
	}
}
