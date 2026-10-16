using System;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C0F RID: 3087
	public class QuestHandler : IConversationPart
	{
		// Token: 0x170009DE RID: 2526
		// (set) Token: 0x06007BCB RID: 31691 RVA: 0x002E846C File Offset: 0x002E666C
		public string Action
		{
			set
			{
				string text = value.ToLowerInvariant();
				byte b;
				if (!(text == "start"))
				{
					if (!(text == "step"))
					{
						if (!(text == "finish"))
						{
							if (!(text == "complete"))
							{
								b = 0;
							}
							else
							{
								b = 4;
							}
						}
						else
						{
							b = 3;
						}
					}
					else
					{
						b = 2;
					}
				}
				else
				{
					b = 1;
				}
				this.Type = (int)b;
			}
		}

		// Token: 0x06007BCC RID: 31692 RVA: 0x002E84CF File Offset: 0x002E66CF
		public QuestHandler()
		{
			this.Priority = 1000;
		}

		// Token: 0x06007BCD RID: 31693 RVA: 0x002E84E9 File Offset: 0x002E66E9
		public QuestHandler(string QuestID, string StepID = null, int XP = -1, int Type = 1)
			: this()
		{
			this.QuestID = QuestID;
			this.StepID = StepID;
			this.XP = XP;
			this.Type = Type;
		}

		// Token: 0x06007BCE RID: 31694 RVA: 0x002E850E File Offset: 0x002E670E
		public override void LoadText(string Text)
		{
			this.Text = Text;
		}

		// Token: 0x06007BCF RID: 31695 RVA: 0x002E8517 File Offset: 0x002E6717
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == GetChoiceTagEvent.ID || ID == EnteredElementEvent.ID;
		}

		// Token: 0x06007BD0 RID: 31696 RVA: 0x002E8538 File Offset: 0x002E6738
		public override bool HandleEvent(GetChoiceTagEvent E)
		{
			if (!this.Text.IsNullOrEmpty())
			{
				E.Tag = this.Text;
				return false;
			}
			if (this.Type == 1 && !this.QuestID.IsNullOrEmpty())
			{
				Quest quest;
				if (QuestLoader.Loader.QuestsByID.TryGetValue(this.QuestID, out quest) && !quest.BonusAtLevel.IsNullOrEmpty())
				{
					E.Tag = "{{W|[Accept Quest - level-based reward]}}";
				}
				else
				{
					E.Tag = "{{W|[Accept Quest]}}";
				}
				return false;
			}
			if (this.Type == 2 && !this.QuestID.IsNullOrEmpty() && !this.StepID.IsNullOrEmpty())
			{
				E.Tag = "{{W|[Complete Quest Step]}}";
				return false;
			}
			if (this.Type >= 3 && !this.QuestID.IsNullOrEmpty())
			{
				Quest quest2;
				if (QuestLoader.Loader.QuestsByID.TryGetValue(this.QuestID, out quest2) && !quest2.BonusAtLevel.IsNullOrEmpty())
				{
					E.Tag = "{{W|[Complete Quest - level-based reward]}}";
				}
				else
				{
					E.Tag = "{{W|[Complete Quest]}}";
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007BD1 RID: 31697 RVA: 0x002E8644 File Offset: 0x002E6844
		public override bool HandleEvent(EnteredElementEvent E)
		{
			if (this.Type == 1 && !this.QuestID.IsNullOrEmpty())
			{
				XRLGame game = The.Game;
				string questID = this.QuestID;
				GameObject speaker = The.Speaker;
				game.StartQuest(questID, (speaker != null) ? speaker.DisplayName : null, null, null);
			}
			if (this.Type == 2 && !this.QuestID.IsNullOrEmpty() && !this.StepID.IsNullOrEmpty())
			{
				The.Game.FinishQuestStep(this.QuestID, this.StepID, this.XP, true, null);
			}
			if (this.Type == 3 && !this.QuestID.IsNullOrEmpty())
			{
				The.Game.FinishQuest(this.QuestID);
			}
			if (this.Type == 4 && !this.QuestID.IsNullOrEmpty())
			{
				The.Game.CompleteQuest(this.QuestID);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0400383C RID: 14396
		public const byte ACT_NONE = 0;

		// Token: 0x0400383D RID: 14397
		public const byte ACT_START = 1;

		// Token: 0x0400383E RID: 14398
		public const byte ACT_STEP = 2;

		// Token: 0x0400383F RID: 14399
		public const byte ACT_FINISH = 3;

		// Token: 0x04003840 RID: 14400
		public const byte ACT_COMPLETE = 4;

		// Token: 0x04003841 RID: 14401
		public string QuestID;

		// Token: 0x04003842 RID: 14402
		public string StepID;

		// Token: 0x04003843 RID: 14403
		public string Text;

		// Token: 0x04003844 RID: 14404
		public int XP = -1;

		// Token: 0x04003845 RID: 14405
		public int Type;
	}
}
