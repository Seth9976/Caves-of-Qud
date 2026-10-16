using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ConsoleLib.Console;
using Genkit;
using HistoryKit;
using Qud.API;
using XRL.Language;
using XRL.Rules;
using XRL.World.Conversations;
using XRL.World.Parts;
using XRL.World.WorldBuilders;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A30 RID: 2608
	[Serializable]
	public class FindASpecificSiteDynamicQuestTemplate_FabricateQuestGiver : ZoneBuilderSandbox
	{
		// Token: 0x06006D90 RID: 28048 RVA: 0x0024C94C File Offset: 0x0024AB4C
		public void addQuestConversationToGiver(GameObject go, Quest quest)
		{
			go.SetStringProperty("GivesDynamicQuest", quest.ID, false);
			go.RequirePart<Interesting>(false);
			ConversationXMLBlueprint conversationXMLBlueprint = go.RequirePart<ConversationScript>(false).Blueprint;
			if (conversationXMLBlueprint == null)
			{
				conversationXMLBlueprint = ConversationsAPI.AddConversation(go, null, null, null, true, true);
			}
			go.SetIntProperty("QuestGiver", 1, false);
			if (go.Brain != null)
			{
				go.Brain.Wanders = false;
				go.Brain.WandersRandomly = false;
			}
			conversationXMLBlueprint.ID = Guid.NewGuid().ToString();
			string text = ColorUtility.StripFormatting(this.targetLocation.Text);
			if (text.StartsWith("the snapjaw who wields") || text.StartsWith("a secluded merchant"))
			{
				text = "the location of " + text;
			}
			string text2 = HistoricStringExpander.ExpandString("<spice.quests.FindaSite.travelers.cameToOurVillage.!random.capitalize>", null);
			string text3 = HistoricStringExpander.ExpandString("<spice.quests.FindaSite.records.intro.!random.capitalize>", null).Replace("*siteInitLower*", Grammar.InitLowerIfArticle(text));
			Regex regex = new Regex("\\*.*\\*");
			Match match = Regex.Match(text2, "(?<=\\*)(.*?)(?=\\*)");
			if (match.Success)
			{
				text2 = regex.Replace(text2, Grammar.Pluralize(match.Value));
			}
			match = Regex.Match(text3, "(?<=\\*)(.*?)(?=\\*)");
			if (match.Success)
			{
				text3 = regex.Replace(text3, Grammar.Pluralize(match.Value));
			}
			QuestStoryType_FindASite qst = this.QST;
			string text4;
			if (qst != QuestStoryType_FindASite.Travelers)
			{
				if (qst != QuestStoryType_FindASite.Records)
				{
					text4 = "ERROR: Failed to generate quest text from quest story type.";
				}
				else
				{
					text4 = string.Format("{0}. {1}? {2}? {3}? {4}? {5}. {6}. {7} {8}.", new object[]
					{
						text3,
						HistoricStringExpander.ExpandString("<spice.quests.FindaSite.records.whatTreasures.!random.capitalize>", null),
						Grammar.InitCap(go.GetxTag_CommaDelimited("TextFragments", "ValuedOre", "precious metals", null)),
						Grammar.InitialCap(go.GetxTag_CommaDelimited("TextFragments", "ArableLand", "arable land", null)),
						HistoricStringExpander.ExpandString("A <spice.commonPhrases.shrine.!random> to ", null) + this.questContext.getSacredThings().GetRandomElement<string>(null),
						"We must know",
						HistoricStringExpander.ExpandString("<spice.quests.FindaSite.records.ifYouFindIt.!random.capitalize>", null),
						HistoricStringExpander.ExpandString("<spice.quests.FindaSpecificItem.personal.willYouDoIt.!random.capitalize>", null),
						HistoricStringExpander.ExpandString("<spice.quests.FindaSite.typeOfDirections." + this.typeOfDirections + ".intro.!random.capitalize>", null)
					});
				}
			}
			else
			{
				text4 = string.Format("{0} {1}. {2}. But they wouldn't reveal the location. {3} {4}. {5}.\n\n{6}.", new object[]
				{
					HistoricStringExpander.ExpandString("<spice.quests.FindaSite.travelers.intro.!random.capitalize>", null),
					text2,
					Grammar.ConvertAtoAn(HistoricStringExpander.ExpandString("<spice.quests.FindaSite.travelers.spokeOfPlace.!random.capitalize>", null)).Replace("*GuestActivity*", go.GetxTag_CommaDelimited("TextFragments", "GuestActivity", "breaking bread", null)),
					HistoricStringExpander.ExpandString("<spice.quests.FindaSite.travelers.wouldYouFindIt.!random.capitalize>", null),
					HistoricStringExpander.ExpandString("<spice.quests.FindaSite.travelers.greatBoon.!random.capitalize>", null),
					HistoricStringExpander.ExpandString("<spice.quests.FindaSite.travelers.rewardYou.!random.capitalize>", null),
					HistoricStringExpander.ExpandString("<spice.quests.FindaSite.typeOfDirections." + this.typeOfDirections + ".intro.!random.capitalize>", null)
				});
			}
			text4 = Grammar.ConvertAtoAn(text4.Replace("*site*", text).Replace("*landmark*", Grammar.InitLowerIfArticle(this.landmarkLocation.Text)).Replace("*direction*", this.direction)
				.Replace("*min*", Math.Max(1, this.min / 3).ToString())
				.Replace("*max*", (this.max / 3).ToString())
				.Replace("*path*", this.path));
			ConversationXMLBlueprint conversationXMLBlueprint2 = ConversationsAPI.AddNode(conversationXMLBlueprint, null, text4);
			ConversationXMLBlueprint conversationXMLBlueprint3 = DynamicQuestConversationHelper.fabricateIntroAcceptChoice("Yes. I will locate " + this.targetLocation.Text + "&G as you ask.", conversationXMLBlueprint2, quest);
			conversationXMLBlueprint3["IfNotHaveMapNote"] = this.targetLocation.ID;
			conversationXMLBlueprint3["RevealMapNote"] = this.landmarkLocation.ID;
			DynamicQuestConversationHelper.fabricateIntroRejectChoice("No, I will not.", conversationXMLBlueprint2);
			ConversationXMLBlueprint conversationXMLBlueprint4 = DynamicQuestConversationHelper.fabricateIntroAdditionalChoice("I already know where " + this.targetLocation.Text + " is.", conversationXMLBlueprint2);
			conversationXMLBlueprint4["StartQuest"] = quest.ID;
			conversationXMLBlueprint4["IfNotHaveQuest"] = quest.ID;
			conversationXMLBlueprint4["IfHaveMapNote"] = this.targetLocation.ID;
			conversationXMLBlueprint4["RevealMapNote"] = this.landmarkLocation.ID;
			ConversationXMLBlueprint conversationXMLBlueprint5 = ConversationsAPI.AddPart(conversationXMLBlueprint4, "QuestHandler");
			conversationXMLBlueprint5["QuestID"] = quest.ID;
			conversationXMLBlueprint5["StepID"] = "a_locate~b_return";
			conversationXMLBlueprint5["Action"] = "Step";
			DynamicQuestConversationHelper.appendQuestCompletionSequence(conversationXMLBlueprint, quest, conversationXMLBlueprint2, "I've located " + this.targetLocation.Text + ".", "I haven't located " + this.targetLocation.Text + " yet.", delegate(ConversationXMLBlueprint questIntroChoiceFinalizer)
			{
			}, delegate(ConversationXMLBlueprint gotoAcceptNodeFinalizer)
			{
				gotoAcceptNodeFinalizer["IfNotFinishedQuest"] = quest.ID;
				gotoAcceptNodeFinalizer["IfFinishedQuestStep"] = quest.ID + "~a_locate";
			}, delegate(ConversationXMLBlueprint incompleteNodeFinalizer)
			{
				incompleteNodeFinalizer["IfNotFinishedQuest"] = quest.ID;
				incompleteNodeFinalizer["IfNotFinishedQuestStep"] = quest.ID + "~a_locate";
			}, delegate(ConversationXMLBlueprint completeNodeFinalizer)
			{
				completeNodeFinalizer["CompleteQuestStep"] = quest.ID + "~b_return";
			}, delegate(ConversationXMLBlueprint startNodeFinalizer)
			{
			});
		}

		// Token: 0x06006D91 RID: 28049 RVA: 0x0024CE94 File Offset: 0x0024B094
		public Quest fabricateFindASpecificSiteQuest(GameObject giver)
		{
			this.typeOfDirections = "path";
			Quest quest = QuestsAPI.fabricateEmptyQuest();
			int num = Stat.Random(0, 12);
			this.min = 12;
			this.max = 18;
			this.targetLocation = null;
			int broaden = 0;
			while (this.targetLocation == null && broaden <= 240)
			{
				this.targetLocation = JournalAPI.GetUnrevealedMapNotesWithinZoneRadiusN(this.zone, this.min - broaden, this.max + broaden, new Predicate<Location2D>(this.questContext.IsValidQuestDestination)).GetRandomElement<JournalMapNote>(null);
				int num2 = broaden;
				broaden = num2 + 1;
			}
			if (this.targetLocation == null)
			{
				this.targetLocation = JournalAPI.GetMapNotesWithinRadiusN(this.zone, this.min + broaden).GetRandomElement<JournalMapNote>(null);
			}
			Predicate<JournalMapNote> <>9__0;
			string text2;
			GeneratedLocationInfo generatedLocationInfo;
			for (;;)
			{
				broaden = 0;
				if (this.typeOfDirections == "radius")
				{
					this.min = 0;
					this.max = 1;
					this.landmarkLocation = null;
					this.landmarkLocation = JournalAPI.GetUnrevealedMapNotesWithinZoneRadiusN(this.targetLocation.ZoneID, this.min - broaden, this.max + broaden, new Predicate<Location2D>(this.questContext.IsValidQuestDestination)).GetRandomElement<JournalMapNote>(null);
					if (this.landmarkLocation == null)
					{
						this.typeOfDirections = "direction";
						continue;
					}
				}
				if (this.typeOfDirections == "radius_Failsafe")
				{
					break;
				}
				if (this.typeOfDirections == "direction")
				{
					List<JournalMapNote> mapNotesInCardinalDirections = JournalAPI.GetMapNotesInCardinalDirections(this.targetLocation.ZoneID);
					this.min = 12;
					this.max = 18;
					for (broaden = 0; broaden <= 240; broaden += 3)
					{
						List<JournalMapNote> list = mapNotesInCardinalDirections;
						Predicate<JournalMapNote> predicate;
						if ((predicate = <>9__0) == null)
						{
							predicate = (<>9__0 = (JournalMapNote l) => l.ResolvedLocation.Distance(this.targetLocation.ResolvedLocation) >= this.min - broaden && l.ResolvedLocation.Distance(this.targetLocation.ResolvedLocation) <= this.max + broaden && this.questContext.IsValidQuestDestination(this.targetLocation.ResolvedLocation) && Math.Abs(l.ZoneZ - this.targetLocation.ZoneZ) == 0);
						}
						List<JournalMapNote> list2 = list.FindAll(predicate);
						if (list2.Count > 0)
						{
							this.landmarkLocation = list2.GetRandomElement<JournalMapNote>(null);
							break;
						}
					}
					if (this.landmarkLocation != null)
					{
						goto IL_02C9;
					}
					this.typeOfDirections = "radius_Failsafe";
				}
				else
				{
					if (!(this.typeOfDirections == "path"))
					{
						goto IL_046A;
					}
					if (this.questContext == null)
					{
						goto Block_17;
					}
					if (this.questContext.worldInfo == null)
					{
						goto Block_18;
					}
					if (this.targetLocation == null)
					{
						goto Block_19;
					}
					string text;
					generatedLocationInfo = this.questContext.worldInfo.FindLocationAlongPathFromLandmark(this.targetLocation.ResolvedLocation, out this.path, out text, out text2, new Predicate<Location2D>(this.questContext.IsValidQuestDestination));
					if (generatedLocationInfo != null)
					{
						goto IL_0450;
					}
					this.typeOfDirections = new List<string> { "radius", "direction" }.GetRandomElement<string>(null);
				}
			}
			this.min = 0;
			this.max = 2;
			this.landmarkLocation = null;
			broaden = 0;
			while (this.landmarkLocation == null)
			{
				if (broaden > 240)
				{
					break;
				}
				this.landmarkLocation = JournalAPI.GetUnrevealedMapNotesWithinZoneRadiusN(this.targetLocation.ZoneID, this.min - broaden, this.max + broaden, new Predicate<Location2D>(this.questContext.IsValidQuestDestination)).GetRandomElement<JournalMapNote>(null);
				int num2 = broaden;
				broaden = num2 + 1;
			}
			goto IL_046A;
			IL_02C9:
			this.min = Math.Max(1, this.landmarkLocation.ResolvedLocation.Distance(this.targetLocation.ResolvedLocation) - num);
			this.max = this.min + 12;
			if (this.landmarkLocation.ResolvedX > this.targetLocation.ResolvedX)
			{
				this.direction = "west";
			}
			if (this.landmarkLocation.ResolvedX < this.targetLocation.ResolvedX)
			{
				this.direction = "east";
			}
			if (this.landmarkLocation.ResolvedY < this.targetLocation.ResolvedY)
			{
				this.direction = "south";
			}
			if (this.landmarkLocation.ResolvedY > this.targetLocation.ResolvedY)
			{
				this.direction = "north";
				goto IL_046A;
			}
			goto IL_046A;
			Block_17:
			throw new Exception("questContext missimg");
			Block_18:
			throw new Exception("worldInfo missing");
			Block_19:
			throw new Exception("targetLocation missimg");
			IL_0450:
			this.direction = text2;
			this.landmarkLocation = JournalAPI.GetMapNote(generatedLocationInfo.secretID);
			IL_046A:
			if (this.landmarkLocation == null)
			{
				MetricsManager.LogError("fabricateFindASpecificSiteQuest", "Couldn't find a site!");
				return quest;
			}
			quest.Name = Grammar.MakeTitleCase(ColorUtility.StripFormatting(this.targetLocation.Text));
			quest.Manager = new FindASiteDynamicQuestManager(this.targetLocation.ZoneID, this.targetLocation.ID, quest.ID, "a_locate");
			quest.StepsByID = new Dictionary<string, QuestStep>();
			QuestStep questStep = new QuestStep();
			questStep.ID = "a_locate";
			questStep.Name = Grammar.MakeTitleCase("Find " + ColorUtility.StripFormatting(this.targetLocation.Text));
			string text3 = Grammar.InitLowerIfArticle(this.landmarkLocation.Text);
			if (this.typeOfDirections == "radius_Failsafe")
			{
				questStep.Text = string.Concat(new string[]
				{
					"Locate ",
					this.targetLocation.Text,
					", located within ",
					this.max.ToString(),
					" parasangs of ",
					text3,
					"."
				});
			}
			if (this.typeOfDirections == "radius")
			{
				questStep.Text = string.Concat(new string[]
				{
					"Locate ",
					this.targetLocation.Text,
					", located next to ",
					text3,
					"."
				});
			}
			if (this.typeOfDirections == "direction")
			{
				questStep.Text = string.Concat(new string[]
				{
					"Locate ",
					this.targetLocation.Text,
					", located ",
					Math.Max(this.min / 3, 1).ToString(),
					"-",
					(this.max / 3).ToString(),
					" parasangs ",
					this.direction,
					" of ",
					text3,
					"."
				});
			}
			if (this.typeOfDirections == "path")
			{
				questStep.Text = string.Concat(new string[]
				{
					"Locate ",
					this.targetLocation.Text,
					", located ",
					this.direction,
					" along the ",
					this.path,
					" that runs through ",
					text3,
					"."
				});
			}
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
			this.addQuestConversationToGiver(giver, quest);
			return quest;
		}

		// Token: 0x06006D92 RID: 28050 RVA: 0x0024D65F File Offset: 0x0024B85F
		public FindASpecificSiteDynamicQuestTemplate_FabricateQuestGiver()
		{
		}

		// Token: 0x06006D93 RID: 28051 RVA: 0x0024D68D File Offset: 0x0024B88D
		public FindASpecificSiteDynamicQuestTemplate_FabricateQuestGiver(Func<GameObject, bool> questGiverFilter)
		{
			this.questGiverFilter = questGiverFilter;
		}

		// Token: 0x06006D94 RID: 28052 RVA: 0x0024D6C4 File Offset: 0x0024B8C4
		public bool BuildZone(Zone zone)
		{
			this.zone = zone;
			this.questGiverFilter = this.questContext.getQuestGiverFilter();
			foreach (GameObject gameObject in zone.GetObjects().ShuffleInPlace<GameObject>(null))
			{
				if (this.questGiverFilter(gameObject))
				{
					this.fabricateFindASpecificSiteQuest(gameObject);
					return true;
				}
			}
			return true;
		}

		// Token: 0x04003208 RID: 12808
		private string typeOfDirections;

		// Token: 0x04003209 RID: 12809
		private JournalMapNote targetLocation;

		// Token: 0x0400320A RID: 12810
		private JournalMapNote landmarkLocation;

		// Token: 0x0400320B RID: 12811
		private string direction = "";

		// Token: 0x0400320C RID: 12812
		private string path = "";

		// Token: 0x0400320D RID: 12813
		private int min = 12;

		// Token: 0x0400320E RID: 12814
		private int max = 18;

		// Token: 0x0400320F RID: 12815
		public string deliveryItemID;

		// Token: 0x04003210 RID: 12816
		public Func<GameObject, bool> questGiverFilter;

		// Token: 0x04003211 RID: 12817
		public VillageDynamicQuestContext questContext;

		// Token: 0x04003212 RID: 12818
		public string sanctityOfSacredThing;

		// Token: 0x04003213 RID: 12819
		public QuestStoryType_FindASite QST;
	}
}
