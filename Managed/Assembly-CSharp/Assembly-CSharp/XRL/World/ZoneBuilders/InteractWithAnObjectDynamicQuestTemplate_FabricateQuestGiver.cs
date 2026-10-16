using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleLib.Console;
using HistoryKit;
using Qud.API;
using XRL.Language;
using XRL.World.Conversations;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A35 RID: 2613
	[Serializable]
	public class InteractWithAnObjectDynamicQuestTemplate_FabricateQuestGiver : ZoneBuilderSandbox
	{
		// Token: 0x06006DAA RID: 28074 RVA: 0x0024E5DC File Offset: 0x0024C7DC
		public void addQuestConversationToGiver(GameObject go, Quest quest, GameObject fetchItem)
		{
			go.SetStringProperty("GivesDynamicQuest", quest.ID, false);
			go.RequirePart<Interesting>(false);
			ConversationXMLBlueprint conversationXMLBlueprint = go.RequirePart<ConversationScript>(false).Blueprint;
			if (conversationXMLBlueprint == null)
			{
				conversationXMLBlueprint = ConversationsAPI.AddConversation(go, null, null, null, true, true);
			}
			conversationXMLBlueprint.ID = Guid.NewGuid().ToString();
			go.SetIntProperty("QuestGiver", 1, false);
			if (go.Brain != null)
			{
				go.Brain.Wanders = false;
				go.Brain.WandersRandomly = false;
			}
			string stringProperty = fetchItem.GetStringProperty("QuestVerb", null);
			QuestStoryType_InteractWithAnObject qst = this.QST;
			string text;
			if (qst != QuestStoryType_InteractWithAnObject.HolyItem)
			{
				if (qst != QuestStoryType_InteractWithAnObject.StrangePlan)
				{
					text = "ERROR: Failed to generate quest text from quest story type.";
				}
				else
				{
					if (If.CoinFlip(null, null))
					{
						quest.Name = Grammar.MakeTitleCase(Grammar.MakePossessive(go.GetDisplayName(int.MaxValue, null, null, false, false, false, false, true, false, true, true, false, false, true, false, false, null, false, false, false, false, null, false, null, false, true)) + " Strange " + this.plan);
					}
					text = string.Format("{0}\n\n{1} {2}. {3}. {4}. {5} {6}.\n\n{7}.", new object[]
					{
						HistoricStringExpander.ExpandString("<spice.quests.InteractWithAnObject.strangeplan.intro.!random>", null),
						HistoricStringExpander.ExpandString("<spice.quests.InteractWithAnObject.strangeplan.comeClose.!random.capitalize>", null),
						HistoricStringExpander.ExpandString("<spice.quests.InteractWithAnObject.strangeplan.myPlan.!random.capitalize>", null).Replace("*plan*", this.plan),
						HistoricStringExpander.ExpandString("<spice.quests.InteractWithAnObject.strangeplan.goTo.!random.capitalize>", null).Replace("*verb*", stringProperty).Replace("*deliveryTarget*", ColorUtility.StripFormatting(this.deliveryTarget.displayName))
							.Replace("*itemName*", fetchItem.DisplayNameOnlyDirectAndStripped),
						"No, I cannot tell you why",
						HistoricStringExpander.ExpandString("<spice.quests.FindaSpecificItem.personal.willYouDoIt.!random.capitalize>", null),
						HistoricStringExpander.ExpandString("<spice.quests.InteractWithAnObject.strangeplan.IRewardYou.!random.capitalize>", null),
						HistoricStringExpander.ExpandString("<spice.quests.InteractWithAnObject.strangeplan.byThe_TellNoOne.!random.capitalize>", null).Replace("*sacredThing*", this.questContext.getSacredThings().GetRandomElement<string>(null))
					});
				}
			}
			else if (stringProperty != "desecrate")
			{
				text = Grammar.ConvertAtoAn(string.Format("{0}? {1}. {2}. {3}. {4}? {5}.", new object[]
				{
					HistoricStringExpander.ExpandString("<spice.quests.InteractWithAnObject.holyitem.intro.!random.capitalize>", null).Replace("*itemName*", fetchItem.DisplayNameOnlyDirectAndStripped).Replace("*deliveryTarget*", ColorUtility.StripFormatting(this.deliveryTarget.displayName)),
					HistoricStringExpander.ExpandString("<spice.quests.InteractWithAnObject.holyitem.itIsHoly.!random.capitalize>", null).Replace("*It*", fetchItem.It),
					HistoricStringExpander.ExpandString("<spice.quests.InteractWithAnObject.holyitem.willInteract.!random.capitalize>", null).Replace("*it*", fetchItem.it).Replace("*verb*", stringProperty)
						.Replace("*sacredThing*", this.questContext.getSacredThings().GetRandomElement<string>(null)),
					HistoricStringExpander.ExpandString("<spice.quests.InteractWithAnObject.holyitem.honorUs.!random.capitalize>", null),
					HistoricStringExpander.ExpandString("<spice.quests.InteractWithAnObject.holyitem.willYou.!random.capitalize>", null),
					HistoricStringExpander.ExpandString("<spice.quests.InteractWithAnObject.holyitem.rewardYou.!random.capitalize>", null)
				}));
			}
			else
			{
				text = Grammar.ConvertAtoAn(string.Format("{0}? {1}. {2}. {3}? {4}.", new object[]
				{
					HistoricStringExpander.ExpandString("<spice.quests.InteractWithAnObject.holyitem.intro.!random.capitalize>", null).Replace("*itemName*", fetchItem.DisplayNameOnlyDirectAndStripped).Replace("*deliveryTarget*", ColorUtility.StripFormatting(this.deliveryTarget.displayName)),
					HistoricStringExpander.ExpandString("<spice.quests.InteractWithAnObject.holyitem.itIsDespicable.!random.capitalize>", null).Replace("*It*", fetchItem.It).Replace("*sacredThing*", this.questContext.getSacredThings().GetRandomElement<string>(null)),
					HistoricStringExpander.ExpandString("<spice.quests.InteractWithAnObject.holyitem.honorUsDesecrate.!random.capitalize>", null).Replace("*it*", fetchItem.it).Replace("*verb*", stringProperty),
					HistoricStringExpander.ExpandString("<spice.quests.InteractWithAnObject.holyitem.willYou.!random.capitalize>", null),
					HistoricStringExpander.ExpandString("<spice.quests.InteractWithAnObject.holyitem.rewardYou.!random.capitalize>", null)
				}));
			}
			ConversationXMLBlueprint conversationXMLBlueprint2 = ConversationsAPI.AddNode(conversationXMLBlueprint, null, Grammar.ConvertAtoAn(text));
			DynamicQuestConversationHelper.fabricateIntroAcceptChoice(string.Concat(new string[]
			{
				"Yes. I will ",
				stringProperty,
				" ",
				fetchItem.t(int.MaxValue, null, null, false, false, false, false, true, true, true, true, null, false, true, false, null, true, null, false),
				" as you ask."
			}), conversationXMLBlueprint2, quest)["RevealMapNote"] = this.deliveryTarget.secretId;
			DynamicQuestConversationHelper.fabricateIntroRejectChoice("No, I will not.", conversationXMLBlueprint2);
			string[] array = stringProperty.Split(' ', StringSplitOptions.None);
			string text2 = ((array.Length > 1) ? (Grammar.PastTenseOf(array[0]) + " " + string.Join(" ", array.Skip<string>(1).ToArray<string>())) : Grammar.PastTenseOf(stringProperty));
			DynamicQuestConversationHelper.appendQuestCompletionSequence(conversationXMLBlueprint, quest, conversationXMLBlueprint2, string.Concat(new string[]
			{
				"I've ",
				text2,
				" ",
				fetchItem.t(int.MaxValue, null, null, false, false, false, false, true, true, true, true, null, false, true, false, null, true, null, false),
				"."
			}), string.Concat(new string[]
			{
				"I haven't ",
				text2,
				" ",
				fetchItem.t(int.MaxValue, null, null, false, false, false, false, true, true, true, true, null, false, true, false, null, true, null, false),
				" yet."
			}), delegate(ConversationXMLBlueprint questIntroChoiceFinalizer)
			{
			}, delegate(ConversationXMLBlueprint gotoAcceptNodeFinalizer)
			{
				gotoAcceptNodeFinalizer["IfNotFinishedQuest"] = quest.ID;
				gotoAcceptNodeFinalizer["IfFinishedQuestStep"] = quest.ID + "~a_use";
			}, delegate(ConversationXMLBlueprint incompleteNodeFinalizer)
			{
				incompleteNodeFinalizer["IfNotFinishedQuest"] = quest.ID;
				incompleteNodeFinalizer["IfNotFinishedQuestStep"] = quest.ID + "~a_use";
			}, delegate(ConversationXMLBlueprint completeNodeFinalizer)
			{
				completeNodeFinalizer["CompleteQuestStep"] = quest.ID + "~b_return";
			}, delegate(ConversationXMLBlueprint startNodeFinalizer)
			{
			});
		}

		// Token: 0x06006DAB RID: 28075 RVA: 0x0024EB88 File Offset: 0x0024CD88
		public Quest fabricateInteractWithAnObjectQuest(GameObject giver, string objectToFetchCacheID)
		{
			Quest quest = QuestsAPI.fabricateEmptyQuest();
			GameObject gameObject = ZoneManager.instance.peekCachedObject(objectToFetchCacheID);
			string stringProperty = gameObject.GetStringProperty("QuestVerb", null);
			this.plan = HistoricStringExpander.ExpandString("<spice.commonPhrases.plan.!random>", null);
			quest.Name = (If.CoinFlip(null, null) ? Grammar.MakeTitleCase(stringProperty + " " + gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false)) : Grammar.MakeTitleCase(gameObject.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false)));
			quest.Manager = new InteractWithAnObjectDynamicQuestManager(objectToFetchCacheID, quest.ID, "a_use");
			quest.StepsByID = new Dictionary<string, QuestStep>();
			QuestStep questStep = new QuestStep();
			questStep.ID = "a_use";
			questStep.Name = Grammar.MakeTitleCase(stringProperty + " " + gameObject.t(int.MaxValue, null, null, false, false, false, false, true, true, true, true, null, false, true, false, null, true, null, false));
			questStep.Text = string.Concat(new string[]
			{
				"Travel to ",
				this.deliveryTarget.displayName,
				" and ",
				stringProperty,
				" ",
				gameObject.t(int.MaxValue, null, null, false, false, false, false, true, true, true, true, null, false, true, false, null, true, null, false),
				"."
			});
			questStep.XP = 100;
			questStep.Finished = false;
			quest.StepsByID.Add(questStep.ID, questStep);
			QuestStep questStep2 = new QuestStep();
			questStep2.ID = "b_return";
			questStep2.Name = Grammar.MakeTitleCase("Return to " + this.questContext.getQuestOriginZone());
			questStep2.Text = string.Concat(new string[]
			{
				"Return to ",
				this.questContext.getQuestOriginZone(),
				" and speak to ",
				giver.DisplayNameOnlyDirectAndStripped,
				"."
			});
			questStep2.XP = 100;
			questStep2.Finished = false;
			quest.StepsByID.Add(questStep2.ID, questStep2);
			quest.dynamicReward = this.questContext.getQuestReward();
			DynamicQuestsGameState.Add(quest);
			this.addQuestConversationToGiver(giver, quest, gameObject);
			return quest;
		}

		// Token: 0x06006DAC RID: 28076 RVA: 0x0024EDEE File Offset: 0x0024CFEE
		public InteractWithAnObjectDynamicQuestTemplate_FabricateQuestGiver()
		{
		}

		// Token: 0x06006DAD RID: 28077 RVA: 0x0024EDF6 File Offset: 0x0024CFF6
		public InteractWithAnObjectDynamicQuestTemplate_FabricateQuestGiver(Func<GameObject, bool> questGiverFilter)
		{
			this.questGiverFilter = questGiverFilter;
		}

		// Token: 0x06006DAE RID: 28078 RVA: 0x0024EE08 File Offset: 0x0024D008
		public bool BuildZone(Zone zone)
		{
			List<GameObject> list = new List<GameObject>();
			this.zone = zone;
			this.questGiverFilter = this.questContext.getQuestGiverFilter();
			foreach (GameObject gameObject in zone.GetObjects().ShuffleInPlace<GameObject>(null))
			{
				if (this.questGiverFilter(gameObject))
				{
					this.fabricateInteractWithAnObjectQuest(gameObject, this.deliveryItemID);
					list.Add(gameObject);
					return true;
				}
			}
			return true;
		}

		// Token: 0x04003222 RID: 12834
		public DynamicQuestDeliveryTarget deliveryTarget;

		// Token: 0x04003223 RID: 12835
		public string deliveryItemID;

		// Token: 0x04003224 RID: 12836
		public Func<GameObject, bool> questGiverFilter;

		// Token: 0x04003225 RID: 12837
		public DynamicQuestContext questContext;

		// Token: 0x04003226 RID: 12838
		public string plan;

		// Token: 0x04003227 RID: 12839
		public QuestStoryType_InteractWithAnObject QST;
	}
}
