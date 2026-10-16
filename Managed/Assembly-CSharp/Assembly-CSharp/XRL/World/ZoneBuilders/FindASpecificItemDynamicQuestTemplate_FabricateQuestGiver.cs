using System;
using System.Collections.Generic;
using System.Linq;
using HistoryKit;
using Qud.API;
using XRL.Language;
using XRL.Rules;
using XRL.World.Conversations;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A32 RID: 2610
	[Serializable]
	public class FindASpecificItemDynamicQuestTemplate_FabricateQuestGiver : ZoneBuilderSandbox
	{
		// Token: 0x06006D9D RID: 28061 RVA: 0x0024D8FC File Offset: 0x0024BAFC
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
			}
			go.Brain.WandersRandomly = false;
			string displayName = fetchItem.GetDisplayName(int.MaxValue, null, null, false, true, false, false, true, false, true, false, false, true, false, false, false, null, false, false, false, false, null, false, null, false, false);
			List<GameObject> list = (from o in this.zone.GetObjects(new Predicate<GameObject>(this.questContext.getQuestActorFilter().Invoke))
				where o != go
				select o).ToList<GameObject>();
			GameObject gameObject = ((list != null) ? list.GetRandomElement<GameObject>(null) : null) ?? GameObject.Create("Mehmet");
			QuestStoryType_FindASpecificItem qst = this.QST;
			string text;
			if (qst != QuestStoryType_FindASpecificItem.SacredItem)
			{
				if (qst != QuestStoryType_FindASpecificItem.PersonalFavor)
				{
					text = "ERROR: Failed to generate quest text from quest story type.";
				}
				else
				{
					text = string.Format("{0} {1}. {2}. {3}. {4}. {5}", new object[]
					{
						HistoricStringExpander.ExpandString("<spice.quests.FindaSpecificItem.personal.intro.!random.capitalize>", null),
						Grammar.ConvertAtoAn(HistoricStringExpander.ExpandString("<spice.quests.FindaSpecificItem.personal.iHaveATask.!random.capitalize>", null)),
						Grammar.ConvertAtoAn(HistoricStringExpander.ExpandString("<spice.quests.FindaSpecificItem.personal.rumor.!random.capitalize>", null).Replace("*itemLocation*", "{{|" + this.deliveryTarget.displayName + "}}").Replace("*itemName*", displayName)
							.Replace("*itemName.a*", fetchItem.a)
							.Replace("*itemName.an*", fetchItem.an(int.MaxValue, null, null, false, true, false, false, true, false, true, false, false, true, false, null, false, null, false))
							.Replace("*itemName.are*", fetchItem.GetVerb("are", false, false, false, true, false))
							.Replace("*itemName.have*", fetchItem.GetVerb("have", false, false, false, true, false))
							.Replace("*villagerName*", gameObject.GetDisplayName(int.MaxValue, null, null, false, false, false, false, true, false, true, true, false, false, true, false, false, null, false, false, false, false, null, false, null, false, true))),
						Grammar.InitCap(HistoricStringExpander.ExpandString("<spice.quests.FindaSpecificItem.personal.loveToHave.!random.capitalize>", null).Replace("*it*", fetchItem.them).Replace("*NeedsItemFor*", go.GetxTag_CommaDelimited("TextFragments", "NeedsItemFor", null, null))),
						HistoricStringExpander.ExpandString("<spice.quests.FindaSpecificItem.personal.ifYouRetrieveIt.!random.capitalize>", null).Replace("*it*", fetchItem.them),
						HistoricStringExpander.ExpandString("<spice.quests.FindaSpecificItem.personal.willYouDoIt.!random.capitalize>", null)
					});
				}
			}
			else
			{
				text = string.Format("{0}. {1}. {2}, {3}. {4} {5}. {6}.", new object[]
				{
					HistoricStringExpander.ExpandString("<spice.quests.FindaSpecificItem.sacred.intro.!random.capitalize>", null).Replace("*Activity*", go.GetxTag_CommaDelimited("TextFragments", "Activity", null, null)),
					HistoricStringExpander.ExpandString("<spice.quests.FindaSpecificItem.sacred.afterLearning.!random.capitalize>", null).Replace("*sanctityOfSacredThing*", this.sanctityOfSacredThing),
					HistoricStringExpander.ExpandString("<spice.instancesOf.unfortunately.!random.capitalize>", null),
					HistoricStringExpander.ExpandString("<spice.quests.FindaSpecificItem.sacred.lostOur.!random>", null).Replace("*itemName*", displayName).Replace("*itemName.a*", fetchItem.a)
						.Replace("*itemName.an*", fetchItem.an(int.MaxValue, null, null, false, true, false, false, true, false, true, false, false, true, false, null, false, null, false))
						.Replace("*itemName.are*", fetchItem.GetVerb("are", false, false, false, true, false))
						.Replace("*itemName.have*", fetchItem.GetVerb("have", false, false, false, true, false))
						.Replace("*name*", gameObject.GetDisplayName(int.MaxValue, null, null, false, false, false, false, true, false, true, true, false, false, true, false, false, null, false, false, false, false, null, false, null, false, true))
						.Replace("*were*", fetchItem.GetVerb("were", false, false, false, true, false)),
					HistoricStringExpander.ExpandString("<spice.quests.FindaSpecificItem.sacred.willingToRecover.!random.capitalize>", null).Replace("*itemTheAndName*", fetchItem.the + displayName).Replace("*it*", fetchItem.them),
					HistoricStringExpander.ExpandString("<spice.quests.FindaSpecificItem.sacred.takenTo.!random.capitalize>", null).Replace("*deliveryTarget*", Grammar.LowerArticles(Grammar.TrimTrailingPunctuation(this.deliveryTarget.displayName))).Replace("*it*", fetchItem.it)
						.Replace("*has*", fetchItem.GetVerb("have", false, false, false, true, false)),
					HistoricStringExpander.ExpandString("<spice.quests.FindaSpecificItem.sacred.rewardYou.!random.capitalize>", null)
				});
			}
			ConversationXMLBlueprint conversationXMLBlueprint2 = ConversationsAPI.AddNode(conversationXMLBlueprint, null, text);
			DynamicQuestConversationHelper.fabricateIntroAcceptChoice("Yes. I will find " + fetchItem.the + displayName + " as you ask.", conversationXMLBlueprint2, quest)["RevealMapNote"] = this.deliveryTarget.secretId;
			DynamicQuestConversationHelper.fabricateIntroRejectChoice("No, I will not.", conversationXMLBlueprint2);
			DynamicQuestConversationHelper.appendQuestCompletionSequence(conversationXMLBlueprint, quest, conversationXMLBlueprint2, "I've found " + fetchItem.the + displayName + ".", "I don't have " + fetchItem.the + displayName + " yet.", delegate(ConversationXMLBlueprint questIntroChoiceFinalizer)
			{
			}, delegate(ConversationXMLBlueprint gotoAcceptNodeFinalizer)
			{
				gotoAcceptNodeFinalizer["IfNotFinishedQuest"] = quest.ID;
				gotoAcceptNodeFinalizer["IfHaveItemWithID"] = this.deliveryItemID;
			}, delegate(ConversationXMLBlueprint incompleteNodeFinalizer)
			{
				incompleteNodeFinalizer["IfNotFinishedQuest"] = quest.ID;
				incompleteNodeFinalizer["IfHaveItemWithID"] = "!" + this.deliveryItemID;
			}, delegate(ConversationXMLBlueprint completeNodeFinalizer)
			{
				ConversationXMLBlueprint conversationXMLBlueprint3 = ConversationsAPI.AddPart(completeNodeFinalizer, "TakeItem");
				conversationXMLBlueprint3.SetAttribute("IDs", this.deliveryItemID);
				conversationXMLBlueprint3.SetAttribute("ClearQuest", "true");
				ConversationXMLBlueprint conversationXMLBlueprint4 = ConversationsAPI.AddPart(completeNodeFinalizer, "QuestHandler");
				conversationXMLBlueprint4.SetAttribute("Action", "Step");
				conversationXMLBlueprint4.SetAttribute("QuestID", quest.ID);
				conversationXMLBlueprint4.SetAttribute("StepID", "a_locate~b_return");
			}, delegate(ConversationXMLBlueprint startNodeFinalizer)
			{
			});
		}

		// Token: 0x06006D9E RID: 28062 RVA: 0x0024DED8 File Offset: 0x0024C0D8
		public Quest fabricateFindASpecificItemQuest(GameObject giver, string objectToFetchCacheID)
		{
			Quest quest = QuestsAPI.fabricateEmptyQuest();
			GameObject gameObject = ZoneManager.instance.peekCachedObject(objectToFetchCacheID);
			this.sanctityOfSacredThing = HistoricStringExpander.ExpandString("<spice.commonPhrases.sanctity.!random> of *sacredThing*", null).Replace("*sacredThing*", this.questContext.getSacredThings().GetRandomElement<string>(null));
			QuestStoryType_FindASpecificItem qst = this.QST;
			if (qst != QuestStoryType_FindASpecificItem.SacredItem)
			{
				if (qst != QuestStoryType_FindASpecificItem.PersonalFavor)
				{
					quest.Name = Grammar.MakeTitleCase(gameObject.t(int.MaxValue, null, null, false, true, false, false, true, true, true, false, null, false, true, false, new bool?(true), true, null, false));
				}
				else
				{
					int num = Stat.Random(0, 2);
					if (num != 0)
					{
						if (num != 1)
						{
							quest.Name = Grammar.MakeTitleCase(HistoricStringExpander.ExpandString("<spice.commonPhrases.helping.!random> ", null) + giver.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, true, true, false, true, false, null, false));
						}
						else
						{
							quest.Name = Grammar.MakeTitleCase(HistoricStringExpander.ExpandString("<spice.commonPhrases.helping.!random> ", null) + giver.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, true, true, false, true, false, null, false) + " to find " + gameObject.t(int.MaxValue, null, null, false, true, false, false, true, true, true, false, null, false, true, false, new bool?(true), true, null, false));
						}
					}
					else
					{
						quest.Name = Grammar.MakeTitleCase(gameObject.t(int.MaxValue, null, null, false, true, false, false, true, true, true, false, null, false, true, false, new bool?(true), true, null, false));
					}
				}
			}
			else if (Stat.Random(0, 1) == 0)
			{
				quest.Name = Grammar.MakeTitleCase(gameObject.t(int.MaxValue, null, null, false, true, false, false, true, true, true, false, null, false, true, false, new bool?(true), true, null, false));
			}
			else
			{
				quest.Name = Grammar.MakeTitleCase("The " + this.sanctityOfSacredThing);
			}
			quest.Manager = new FindASpecificItemDynamicQuestManager(objectToFetchCacheID, quest.ID, "a_locate");
			quest.StepsByID = new Dictionary<string, QuestStep>();
			QuestStep questStep = new QuestStep();
			questStep.ID = "a_locate";
			questStep.Name = Grammar.MakeTitleCase("Find " + gameObject.t(int.MaxValue, null, null, false, true, false, false, true, true, true, false, null, false, true, false, new bool?(true), true, null, false));
			questStep.Text = string.Concat(new string[]
			{
				"Locate ",
				gameObject.t(int.MaxValue, null, null, false, true, false, false, true, true, true, false, null, false, true, false, new bool?(true), true, null, false),
				" at ",
				this.deliveryTarget.displayName,
				"."
			});
			questStep.XP = 100;
			questStep.Finished = false;
			quest.StepsByID.Add(questStep.ID, questStep);
			QuestStep questStep2 = new QuestStep();
			questStep2.ID = "b_return";
			questStep2.Name = Grammar.MakeTitleCase("Return " + gameObject.t(int.MaxValue, null, null, false, true, false, false, true, true, true, false, null, false, true, false, new bool?(true), true, null, false) + " to " + this.questContext.getQuestOriginZone());
			questStep2.Text = string.Concat(new string[]
			{
				"Return ",
				gameObject.t(int.MaxValue, null, null, false, true, false, false, true, true, true, false, null, false, true, false, new bool?(true), true, null, false),
				" to ",
				this.questContext.getQuestOriginZone(),
				" and speak with ",
				giver.t(int.MaxValue, null, null, false, true, false, false, true, true, true, false, null, false, true, false, null, true, null, false),
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

		// Token: 0x06006D9F RID: 28063 RVA: 0x0024E2AE File Offset: 0x0024C4AE
		public FindASpecificItemDynamicQuestTemplate_FabricateQuestGiver()
		{
		}

		// Token: 0x06006DA0 RID: 28064 RVA: 0x0024E2B6 File Offset: 0x0024C4B6
		public FindASpecificItemDynamicQuestTemplate_FabricateQuestGiver(Func<GameObject, bool> questGiverFilter)
		{
			this.questGiverFilter = questGiverFilter;
		}

		// Token: 0x06006DA1 RID: 28065 RVA: 0x0024E2C8 File Offset: 0x0024C4C8
		public bool BuildZone(Zone zone)
		{
			List<GameObject> list = new List<GameObject>();
			this.zone = zone;
			this.questGiverFilter = this.questContext.getQuestGiverFilter();
			foreach (GameObject gameObject in zone.GetObjects().ShuffleInPlace<GameObject>(null))
			{
				if (this.questGiverFilter(gameObject))
				{
					this.fabricateFindASpecificItemQuest(gameObject, this.deliveryItemID);
					list.Add(gameObject);
					return true;
				}
			}
			return true;
		}

		// Token: 0x04003217 RID: 12823
		public DynamicQuestDeliveryTarget deliveryTarget;

		// Token: 0x04003218 RID: 12824
		public string deliveryItemID;

		// Token: 0x04003219 RID: 12825
		public Func<GameObject, bool> questGiverFilter;

		// Token: 0x0400321A RID: 12826
		public DynamicQuestContext questContext;

		// Token: 0x0400321B RID: 12827
		public string sanctityOfSacredThing;

		// Token: 0x0400321C RID: 12828
		public QuestStoryType_FindASpecificItem QST;
	}
}
