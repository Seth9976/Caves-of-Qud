using System;
using System.Collections.Generic;
using System.Reflection;
using Qud.API;
using XRL.Rules;
using XRL.UI;
using XRL.World.Conversations.Parts;
using XRL.World.Parts;
using XRL.World.Quests;

namespace XRL.World.Conversations
{
	// Token: 0x02000BE3 RID: 3043
	[HasModSensitiveStaticCache]
	[HasConversationDelegate]
	public static class ConversationDelegates
	{
		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x060079F6 RID: 31222 RVA: 0x002DD70A File Offset: 0x002DB90A
		public static Dictionary<string, PredicateReceiver> Predicates
		{
			get
			{
				if (ConversationDelegates._Predicates == null)
				{
					ConversationDelegates.LoadDelegates();
				}
				return ConversationDelegates._Predicates;
			}
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x060079F7 RID: 31223 RVA: 0x002DD71D File Offset: 0x002DB91D
		public static Dictionary<string, ActionReceiver> Actions
		{
			get
			{
				if (ConversationDelegates._Actions == null)
				{
					ConversationDelegates.LoadDelegates();
				}
				return ConversationDelegates._Actions;
			}
		}

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x060079F8 RID: 31224 RVA: 0x002DD730 File Offset: 0x002DB930
		public static Dictionary<string, PartGeneratorReceiver> PartGenerators
		{
			get
			{
				if (ConversationDelegates._PartGenerators == null)
				{
					ConversationDelegates.LoadDelegates();
				}
				return ConversationDelegates._PartGenerators;
			}
		}

		// Token: 0x060079F9 RID: 31225 RVA: 0x002DD743 File Offset: 0x002DB943
		public static void CreatePredicate(MethodInfo Method, Dictionary<string, PredicateReceiver> Predicates)
		{
			ConversationDelegates.CreatePredicate(Method.GetCustomAttribute<ConversationDelegate>(), Method, Predicates);
		}

		// Token: 0x060079FA RID: 31226 RVA: 0x002DD754 File Offset: 0x002DB954
		public static void CreatePredicate(ConversationDelegate Attribute, MethodInfo Method, Dictionary<string, PredicateReceiver> Predicates)
		{
			string text = Attribute.Key ?? Method.Name;
			int num = text.UpToNthIndex(new Func<char, bool>(char.IsUpper), 2, 0);
			ConversationPredicate predicate = (ConversationPredicate)Method.CreateDelegate(typeof(ConversationPredicate));
			Predicates[text] = (IConversationElement e, string x) => predicate(DelegateContext.Set(e, x, The.Listener));
			if (Attribute.Inverse)
			{
				if (Attribute.InverseKey != null)
				{
					Predicates[Attribute.InverseKey] = (IConversationElement e, string x) => !predicate(DelegateContext.Set(e, x, The.Listener));
				}
				else
				{
					Predicates[text.Insert(num, "Not")] = (IConversationElement e, string x) => !predicate(DelegateContext.Set(e, x, The.Listener));
				}
			}
			if (Attribute.Speaker)
			{
				if (Attribute.SpeakerKey != null)
				{
					Predicates[Attribute.SpeakerKey] = (IConversationElement e, string x) => predicate(DelegateContext.Set(e, x, The.Speaker));
				}
				else
				{
					Predicates[text.Insert(num, "Speaker")] = (IConversationElement e, string x) => predicate(DelegateContext.Set(e, x, The.Speaker));
				}
				if (Attribute.Inverse)
				{
					if (Attribute.SpeakerInverseKey != null)
					{
						Predicates[Attribute.SpeakerInverseKey] = (IConversationElement e, string x) => !predicate(DelegateContext.Set(e, x, The.Speaker));
						return;
					}
					Predicates[text.Insert(num, "SpeakerNot")] = (IConversationElement e, string x) => !predicate(DelegateContext.Set(e, x, The.Speaker));
				}
			}
		}

		// Token: 0x060079FB RID: 31227 RVA: 0x002DD895 File Offset: 0x002DBA95
		public static void CreateAction(MethodInfo Method, Dictionary<string, ActionReceiver> Actions)
		{
			ConversationDelegates.CreateAction(Method.GetCustomAttribute<ConversationDelegate>(), Method, Actions);
		}

		// Token: 0x060079FC RID: 31228 RVA: 0x002DD8A4 File Offset: 0x002DBAA4
		public static void CreateAction(ConversationDelegate Attribute, MethodInfo Method, Dictionary<string, ActionReceiver> Actions)
		{
			string text = Attribute.Key ?? Method.Name;
			int num = text.UpToNthIndex(new Func<char, bool>(char.IsUpper), 2, 0);
			ConversationAction action = Method.CreateDelegate(typeof(ConversationAction)) as ConversationAction;
			Actions[text] = delegate(IConversationElement e, string x)
			{
				action(DelegateContext.Set(e, x, The.Listener));
			};
			if (Attribute.Speaker)
			{
				if (Attribute.SpeakerKey != null)
				{
					Actions[Attribute.SpeakerKey] = delegate(IConversationElement e, string x)
					{
						action(DelegateContext.Set(e, x, The.Speaker));
					};
					return;
				}
				Actions[text.Insert(num, "Speaker")] = delegate(IConversationElement e, string x)
				{
					action(DelegateContext.Set(e, x, The.Speaker));
				};
			}
		}

		// Token: 0x060079FD RID: 31229 RVA: 0x002DD954 File Offset: 0x002DBB54
		public static void CreatePart(ConversationDelegate Attribute, MethodInfo Method, Dictionary<string, PartGeneratorReceiver> PartGenerators)
		{
			string text = Attribute.Key ?? Method.Name;
			int num = text.UpToNthIndex(new Func<char, bool>(char.IsUpper), 2, 0);
			ConversationPartGenerator generator = Method.CreateDelegate(typeof(ConversationPartGenerator)) as ConversationPartGenerator;
			PartGenerators[text] = (IConversationElement e, string x) => generator(DelegateContext.Set(e, x, The.Listener));
			if (Attribute.Speaker)
			{
				if (Attribute.SpeakerKey != null)
				{
					PartGenerators[Attribute.SpeakerKey] = (IConversationElement e, string x) => generator(DelegateContext.Set(e, x, The.Speaker));
					return;
				}
				PartGenerators[text.Insert(num, "Speaker")] = (IConversationElement e, string x) => generator(DelegateContext.Set(e, x, The.Speaker));
			}
		}

		// Token: 0x060079FE RID: 31230 RVA: 0x002DDA04 File Offset: 0x002DBC04
		[PreGameCacheInit]
		public static void LoadDelegates()
		{
			ConversationDelegates._Predicates = new Dictionary<string, PredicateReceiver>();
			ConversationDelegates._Actions = new Dictionary<string, ActionReceiver>();
			ConversationDelegates._PartGenerators = new Dictionary<string, PartGeneratorReceiver>();
			Type typeFromHandle = typeof(ConversationDelegate);
			Type typeFromHandle2 = typeof(HasConversationDelegate);
			foreach (MethodInfo methodInfo in ModManager.GetMethodsWithAttribute(typeFromHandle, typeFromHandle2, false))
			{
				try
				{
					ConversationDelegate customAttribute = methodInfo.GetCustomAttribute<ConversationDelegate>();
					string text = customAttribute.Key ?? methodInfo.Name;
					if (methodInfo.ReturnType == typeof(bool))
					{
						ConversationDelegates.CreatePredicate(customAttribute, methodInfo, ConversationDelegates.Predicates);
					}
					else if (methodInfo.ReturnType == typeof(void))
					{
						ConversationDelegates.CreateAction(customAttribute, methodInfo, ConversationDelegates.Actions);
					}
					else if (methodInfo.ReturnType == typeof(IConversationPart))
					{
						ConversationDelegates.CreatePart(customAttribute, methodInfo, ConversationDelegates.PartGenerators);
					}
					else
					{
						MetricsManager.LogAssemblyError(methodInfo, "Conversation delegate " + text + " does not match signature.");
					}
				}
				catch (Exception ex)
				{
					MetricsManager.LogAssemblyError(methodInfo, ex);
				}
			}
		}

		// Token: 0x060079FF RID: 31231 RVA: 0x002DDB44 File Offset: 0x002DBD44
		[ConversationDelegate]
		public static bool IfHaveQuest(DelegateContext Context)
		{
			return The.Game.HasQuest(Context.Value);
		}

		// Token: 0x06007A00 RID: 31232 RVA: 0x002DDB56 File Offset: 0x002DBD56
		[ConversationDelegate]
		public static bool IfHaveActiveQuest(DelegateContext Context)
		{
			return The.Game.HasUnfinishedQuest(Context.Value);
		}

		// Token: 0x06007A01 RID: 31233 RVA: 0x002DDB68 File Offset: 0x002DBD68
		[ConversationDelegate]
		public static bool IfHaveQuestProperty(DelegateContext Context)
		{
			string text;
			string text2;
			Context.Value.Split('~', out text, out text2);
			return The.Game.HasQuestProperty(text, text2);
		}

		// Token: 0x06007A02 RID: 31234 RVA: 0x002DDB92 File Offset: 0x002DBD92
		[ConversationDelegate]
		public static bool IfFinishedQuest(DelegateContext Context)
		{
			return The.Game.HasFinishedQuest(Context.Value);
		}

		// Token: 0x06007A03 RID: 31235 RVA: 0x002DDBA4 File Offset: 0x002DBDA4
		[ConversationDelegate]
		public static bool IfFinishedQuestStep(DelegateContext Context)
		{
			return The.Game.FinishedQuestStep(Context.Value);
		}

		// Token: 0x06007A04 RID: 31236 RVA: 0x002DDBB6 File Offset: 0x002DBDB6
		[ConversationDelegate]
		public static bool IfHaveObservation(DelegateContext Context)
		{
			return JournalAPI.HasObservation(Context.Value);
		}

		// Token: 0x06007A05 RID: 31237 RVA: 0x002DDBC3 File Offset: 0x002DBDC3
		[ConversationDelegate]
		public static bool IfHaveObservationWithTag(DelegateContext Context)
		{
			return JournalAPI.HasObservationWithTag(Context.Value);
		}

		// Token: 0x06007A06 RID: 31238 RVA: 0x002DDBD0 File Offset: 0x002DBDD0
		[ConversationDelegate]
		public static bool IfHaveSultanNoteWithTag(DelegateContext Context)
		{
			return JournalAPI.HasSultanNoteWithTag(Context.Value);
		}

		// Token: 0x06007A07 RID: 31239 RVA: 0x002DDBDD File Offset: 0x002DBDDD
		[ConversationDelegate]
		public static bool IfHaveVillageNote(DelegateContext Context)
		{
			return JournalAPI.HasVillageNote(Context.Value);
		}

		// Token: 0x06007A08 RID: 31240 RVA: 0x002DDBEA File Offset: 0x002DBDEA
		[ConversationDelegate]
		public static bool IfHaveMapNote(DelegateContext Context)
		{
			return JournalAPI.IsMapOrVillageNoteRevealed(Context.Value);
		}

		// Token: 0x06007A09 RID: 31241 RVA: 0x002DDBF7 File Offset: 0x002DBDF7
		[ConversationDelegate]
		public static bool IfHaveState(DelegateContext Context)
		{
			return The.Game.HasGameState(Context.Value);
		}

		// Token: 0x06007A0A RID: 31242 RVA: 0x002DDC09 File Offset: 0x002DBE09
		[ConversationDelegate]
		public static bool IfHaveConversationState(DelegateContext Context)
		{
			return The.Conversation.HasState(Context.Value);
		}

		// Token: 0x06007A0B RID: 31243 RVA: 0x002DDC1B File Offset: 0x002DBE1B
		[ConversationDelegate]
		public static bool IfTestState(DelegateContext Context)
		{
			return The.Game.TestGameState(Context.Value);
		}

		// Token: 0x06007A0C RID: 31244 RVA: 0x002DDC30 File Offset: 0x002DBE30
		[ConversationDelegate]
		public unsafe static bool IfHaveDelimitedState(DelegateContext Context)
		{
			int num = Context.Value.LastIndexOf(':');
			if (num == -1)
			{
				return false;
			}
			ReadOnlySpan<char> readOnlySpan = Context.Value.AsSpan(0, num);
			ReadOnlySpan<char> readOnlySpan2 = Context.Value.AsSpan(num + 1);
			char c = ',';
			if (*readOnlySpan[0] == 91)
			{
				c = Context.Value[1];
				readOnlySpan = readOnlySpan.Slice(3);
			}
			return The.Game.HasDelimitedGameState(new string(readOnlySpan), c, new string(readOnlySpan2));
		}

		// Token: 0x06007A0D RID: 31245 RVA: 0x002DDCAC File Offset: 0x002DBEAC
		[ConversationDelegate]
		public unsafe static bool IfHaveDelimitedConversationState(DelegateContext Context)
		{
			int num = Context.Value.LastIndexOf(':');
			if (num == -1)
			{
				return false;
			}
			ReadOnlySpan<char> readOnlySpan = Context.Value.AsSpan(0, num);
			ReadOnlySpan<char> readOnlySpan2 = Context.Value.AsSpan(num + 1);
			char c = ',';
			if (*readOnlySpan[0] == 91)
			{
				c = Context.Value[1];
				readOnlySpan = readOnlySpan.Slice(3);
			}
			return The.Conversation.HasDelimitedState(new string(readOnlySpan), c, new string(readOnlySpan2));
		}

		// Token: 0x06007A0E RID: 31246 RVA: 0x002DDD27 File Offset: 0x002DBF27
		[ConversationDelegate]
		public static bool IfLastChoice(DelegateContext Context)
		{
			Choice lastChoice = ConversationUI.LastChoice;
			return ((lastChoice != null) ? lastChoice.ID : null) == Context.Value;
		}

		// Token: 0x06007A0F RID: 31247 RVA: 0x002DDD48 File Offset: 0x002DBF48
		[ConversationDelegate]
		public static bool IfHaveText(DelegateContext Context)
		{
			Node currentNode = ConversationUI.CurrentNode;
			string text = ((currentNode != null) ? currentNode.Text : null);
			return text != null && text.Contains(Context.Value);
		}

		// Token: 0x06007A10 RID: 31248 RVA: 0x002DDD78 File Offset: 0x002DBF78
		[ConversationDelegate]
		public static bool IfTime(DelegateContext Context)
		{
			int num;
			int num2;
			if (!Context.Value.TryParseRange(out num, out num2))
			{
				return false;
			}
			long num3 = Calendar.TotalTimeTicks % 1200L;
			if (num2 < num)
			{
				return num3 >= (long)num || num3 <= (long)num2;
			}
			return num3 >= (long)num && num3 <= (long)num2;
		}

		// Token: 0x06007A11 RID: 31249 RVA: 0x002DDDC8 File Offset: 0x002DBFC8
		[ConversationDelegate]
		public static bool IfLedBy(DelegateContext Context)
		{
			Brain brain = The.Speaker.Brain;
			if (((brain != null) ? brain.PartyLeader : null) == null)
			{
				return false;
			}
			if (Context.Value == "*")
			{
				return true;
			}
			if (Context.Value.EqualsNoCase("Player"))
			{
				return The.Speaker.IsPlayerLed();
			}
			return The.Speaker.Brain.IsLedBy(Context.Value);
		}

		// Token: 0x06007A12 RID: 31250 RVA: 0x002DDE38 File Offset: 0x002DC038
		[ConversationDelegate]
		public static bool IfZoneName(DelegateContext Context)
		{
			if (The.ActiveZone != null)
			{
				string zoneBaseDisplayName = The.ZoneManager.GetZoneBaseDisplayName(The.ActiveZone.ZoneID, false);
				if (zoneBaseDisplayName != null && zoneBaseDisplayName.Contains(Context.Value))
				{
					return true;
				}
				string zoneNameContext = The.ZoneManager.GetZoneNameContext(The.ActiveZone.ZoneID);
				if (zoneNameContext != null && zoneNameContext.Contains(Context.Value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06007A13 RID: 31251 RVA: 0x002DDEA0 File Offset: 0x002DC0A0
		[ConversationDelegate]
		public static bool IfZoneLevel(DelegateContext Context)
		{
			if (The.ActiveZone == null)
			{
				return false;
			}
			int z = The.ActiveZone.Z;
			int num;
			int num2;
			return Context.Value.TryParseRange(out num, out num2) && num <= z && num2 >= z;
		}

		// Token: 0x06007A14 RID: 31252 RVA: 0x002DDEE0 File Offset: 0x002DC0E0
		[ConversationDelegate]
		public static bool IfZoneTier(DelegateContext Context)
		{
			if (The.ActiveZone == null)
			{
				return false;
			}
			int newTier = The.ActiveZone.NewTier;
			int num;
			int num2;
			return Context.Value.TryParseRange(out num, out num2) && num <= newTier && num2 >= newTier;
		}

		// Token: 0x06007A15 RID: 31253 RVA: 0x002DDF1F File Offset: 0x002DC11F
		[ConversationDelegate]
		public static bool IfZoneWorld(DelegateContext Context)
		{
			Zone activeZone = The.ActiveZone;
			return ((activeZone != null) ? activeZone.ZoneWorld : null) == Context.Value;
		}

		// Token: 0x06007A16 RID: 31254 RVA: 0x002DDF3D File Offset: 0x002DC13D
		[ConversationDelegate]
		public static bool IfZoneID(DelegateContext Context)
		{
			Zone activeZone = The.ActiveZone;
			return ((activeZone != null) ? activeZone.ZoneID : null) != null && The.ActiveZone.ZoneID.StartsWith(Context.Value);
		}

		// Token: 0x06007A17 RID: 31255 RVA: 0x002DDF6C File Offset: 0x002DC16C
		[ConversationDelegate]
		public static bool IfZoneVisited(DelegateContext Context)
		{
			ZoneManager zoneManager = The.ZoneManager;
			Dictionary<string, long> dictionary = ((zoneManager != null) ? zoneManager.VisitedTime : null);
			return !dictionary.IsNullOrEmpty<KeyValuePair<string, long>>() && dictionary.ContainsKey(Context.Value);
		}

		// Token: 0x06007A18 RID: 31256 RVA: 0x002DDFA4 File Offset: 0x002DC1A4
		[ConversationDelegate]
		public static bool IfZoneHaveObject(DelegateContext Context)
		{
			GameObject speaker = The.Speaker;
			Zone zone = ((speaker != null) ? speaker.CurrentZone : null);
			if (zone == null)
			{
				return false;
			}
			ReadOnlySpan<char> readOnlySpan;
			ReadOnlySpan<char> readOnlySpan2;
			Context.Value.AsDelimitedSpans(',', out readOnlySpan, out readOnlySpan2);
			if (readOnlySpan.Length == 0)
			{
				return false;
			}
			if (readOnlySpan2.Length == 0)
			{
				return zone.HasObject((readOnlySpan.Length == Context.Value.Length) ? Context.Value : readOnlySpan.ToString());
			}
			int num;
			if (!int.TryParse(readOnlySpan2, out num))
			{
				MetricsManager.LogError("IfZoneBlueprint::Unable to parse " + readOnlySpan2.ToString() + " as distance integer");
				return false;
			}
			GameObject gameObject = zone.FindClosestObject(The.Speaker, readOnlySpan.ToString(), false, true, true);
			return gameObject != null && gameObject.DistanceTo(The.Speaker) <= num;
		}

		// Token: 0x06007A19 RID: 31257 RVA: 0x002DE080 File Offset: 0x002DC280
		[ConversationDelegate]
		public static bool IfZoneHaveQuestGiver(DelegateContext Context)
		{
			GameObject speaker = The.Speaker;
			Zone zone = ((speaker != null) ? speaker.CurrentZone : null);
			if (zone == null || The.Speaker.Brain == null)
			{
				return false;
			}
			string value = Context.Value;
			Predicate<int> predicate;
			string text;
			if (!(value == "Finished"))
			{
				if (!(value == "Active"))
				{
					if (!(value == "Started"))
					{
						if (!(value == "Unstarted"))
						{
							if (!(value == "Invalid"))
							{
								return false;
							}
							predicate = (int x) => x == -1;
							text = "ZoneHaveQuestGiverInvalid";
						}
						else
						{
							predicate = (int x) => x == 0;
							text = "ZoneHaveQuestGiverUnstarted";
						}
					}
					else
					{
						predicate = (int x) => x >= 1;
						text = "ZoneHaveQuestGiverStarted";
					}
				}
				else
				{
					predicate = (int x) => x == 1;
					text = "ZoneHaveQuestGiverActive";
				}
			}
			else
			{
				predicate = (int x) => x == 2;
				text = "ZoneHaveQuestGiverFinished";
			}
			bool flag;
			if (The.Conversation.TryGetState<bool>(text, out flag))
			{
				return flag;
			}
			bool flag2 = false;
			XRLGame game = The.Game;
			Brain brain = The.Speaker.Brain;
			foreach (GameObject gameObject in zone.IterateObjects())
			{
				if (brain.InSameFactionAs(gameObject, Brain.AllegianceLevel.Member))
				{
					int questGiverState = game.GetQuestGiverState(gameObject);
					if (predicate(questGiverState))
					{
						flag2 = true;
						break;
					}
				}
			}
			The.Conversation.State[text] = flag2;
			return flag2;
		}

		// Token: 0x06007A1A RID: 31258 RVA: 0x002DE264 File Offset: 0x002DC464
		[ConversationDelegate(Inverse = false)]
		public static bool IfCheckpoint(DelegateContext Context)
		{
			if (Context.Value.EqualsNoCase("false"))
			{
				GameObject speaker = The.Speaker;
				if (speaker == null)
				{
					return true;
				}
				Zone currentZone = speaker.CurrentZone;
				bool? flag = ((currentZone != null) ? new bool?(currentZone.IsCheckpoint()) : null);
				bool flag2 = true;
				return !((flag.GetValueOrDefault() == flag2) & (flag != null));
			}
			else
			{
				GameObject speaker2 = The.Speaker;
				if (speaker2 == null)
				{
					return false;
				}
				Zone currentZone2 = speaker2.CurrentZone;
				bool? flag = ((currentZone2 != null) ? new bool?(currentZone2.IsCheckpoint()) : null);
				bool flag2 = true;
				return (flag.GetValueOrDefault() == flag2) & (flag != null);
			}
		}

		// Token: 0x06007A1B RID: 31259 RVA: 0x002DE304 File Offset: 0x002DC504
		[ConversationDelegate]
		public static bool IfUnderstood(DelegateContext Context)
		{
			if (Examiner.UnderstandingTable == null)
			{
				Loading.LoadTask("Loading medication names", new Action(Examiner.ResetGlobals), true);
			}
			int num;
			return Examiner.UnderstandingTable.TryGetValue(Context.Value, out num) && num == 2;
		}

		// Token: 0x06007A1C RID: 31260 RVA: 0x002DE349 File Offset: 0x002DC549
		[ConversationDelegate]
		public static bool IfCommand(DelegateContext Context)
		{
			return PredicateEvent.GetFor(Context.Element, "IfCommand", Context.Value, false);
		}

		// Token: 0x06007A1D RID: 31261 RVA: 0x002DE364 File Offset: 0x002DC564
		[ConversationDelegate]
		public static bool IfReputationAtLeast(DelegateContext Context)
		{
			string text = Context.Value.ToUpperInvariant();
			int num;
			if (!(text == "LOVED"))
			{
				if (!(text == "LIKED"))
				{
					if (!(text == "INDIFFERENT"))
					{
						if (!(text == "DISLIKED"))
						{
							if (!(text == "HATED"))
							{
								num = int.MaxValue;
							}
							else
							{
								num = -2;
							}
						}
						else
						{
							num = -1;
						}
					}
					else
					{
						num = 0;
					}
				}
				else
				{
					num = 1;
				}
			}
			else
			{
				num = 2;
			}
			int num2 = num;
			return The.Game.PlayerReputation.GetLevel(The.Speaker.GetPrimaryFaction(false)) >= num2;
		}

		// Token: 0x06007A1E RID: 31262 RVA: 0x002DE400 File Offset: 0x002DC600
		[ConversationDelegate]
		public static bool IfSlynthCandidate(DelegateContext Context)
		{
			LandingPadsSystem system = The.Game.GetSystem<LandingPadsSystem>();
			if (system == null)
			{
				return false;
			}
			if (Context.Value.EqualsNoCase("Mayor"))
			{
				string propertyOrTag = The.Speaker.GetPropertyOrTag("Mayor", null);
				return propertyOrTag != null && system.candidateFactions.Contains(propertyOrTag);
			}
			if (Context.Value.EqualsNoCase("Primary"))
			{
				string primaryFaction = The.Speaker.GetPrimaryFaction(false);
				return primaryFaction != null && system.candidateFactions.Contains(primaryFaction);
			}
			return system.candidateFactions.Contains(Context.Value);
		}

		// Token: 0x06007A1F RID: 31263 RVA: 0x002DE494 File Offset: 0x002DC694
		[ConversationDelegate]
		public static bool IfSlynthChosen(DelegateContext Context)
		{
			string text;
			if (!The.Game.StringGameState.TryGetValue("SlynthSettlementFaction", out text))
			{
				return false;
			}
			if (Context.Value.EqualsNoCase("Mayor"))
			{
				return text == The.Speaker.GetPropertyOrTag("Mayor", null);
			}
			if (Context.Value.EqualsNoCase("Primary"))
			{
				return text == The.Speaker.GetPrimaryFaction(false);
			}
			return text == Context.Value;
		}

		// Token: 0x06007A20 RID: 31264 RVA: 0x002DE514 File Offset: 0x002DC714
		[ConversationDelegate]
		public static bool IfHindriarch(DelegateContext Context)
		{
			string text;
			return The.Game.StringGameState.TryGetValue("HindrenMysteryOutcomeHindriarch", out text) && text == Context.Value;
		}

		// Token: 0x06007A21 RID: 31265 RVA: 0x002DE548 File Offset: 0x002DC748
		[ConversationDelegate(Inverse = false)]
		public static bool IfAllowEscape(DelegateContext Context)
		{
			bool flag = Context.Value.EqualsNoCase("true");
			Node node = Context.Element.GetAncestor((IConversationElement x) => x is Node, true) as Node;
			return (node == null || node.AllowEscape) == flag;
		}

		// Token: 0x06007A22 RID: 31266 RVA: 0x002DE5A8 File Offset: 0x002DC7A8
		[ConversationDelegate(Inverse = false)]
		public static bool IfIn100(DelegateContext Context)
		{
			int num;
			return int.TryParse(Context.Value, out num) && num > 0 && (num >= 100 || Stat.Random(1, 100) <= num);
		}

		// Token: 0x06007A23 RID: 31267 RVA: 0x002DE5E1 File Offset: 0x002DC7E1
		[ConversationDelegate(Speaker = true)]
		public static bool IfGenotype(DelegateContext Context)
		{
			return Context.Value.HasDelimitedSubstring(',', Context.Target.GetGenotype(), StringComparison.Ordinal);
		}

		// Token: 0x06007A24 RID: 31268 RVA: 0x002DE5FC File Offset: 0x002DC7FC
		[ConversationDelegate(Speaker = true)]
		public static bool IfSubtype(DelegateContext Context)
		{
			return Context.Value.HasDelimitedSubstring(',', Context.Target.GetSubtype(), StringComparison.Ordinal);
		}

		// Token: 0x06007A25 RID: 31269 RVA: 0x002DE617 File Offset: 0x002DC817
		[ConversationDelegate(Speaker = true)]
		public static bool IfTrueKin(DelegateContext Context)
		{
			return Context.Target.IsTrueKin();
		}

		// Token: 0x06007A26 RID: 31270 RVA: 0x002DE624 File Offset: 0x002DC824
		[ConversationDelegate(Speaker = true)]
		public static bool IfMutant(DelegateContext Context)
		{
			return Context.Target.IsMutant();
		}

		// Token: 0x06007A27 RID: 31271 RVA: 0x002DE631 File Offset: 0x002DC831
		[ConversationDelegate(Speaker = true)]
		public static bool IfHaveItem(DelegateContext Context)
		{
			return Context.Target.HasObjectInInventory(Context.Value, 1);
		}

		// Token: 0x06007A28 RID: 31272 RVA: 0x002DE648 File Offset: 0x002DC848
		[ConversationDelegate(Speaker = true)]
		public static bool IfHaveItemWithID(DelegateContext Context)
		{
			return Context.Target.HasObjectInInventory((GameObject o) => o.IDMatch(Context.Value), 1);
		}

		// Token: 0x06007A29 RID: 31273 RVA: 0x002DE67F File Offset: 0x002DC87F
		[ConversationDelegate(Speaker = true)]
		public static bool IfHavePart(DelegateContext Context)
		{
			return Context.Target.HasPart(Context.Value);
		}

		// Token: 0x06007A2A RID: 31274 RVA: 0x002DE692 File Offset: 0x002DC892
		[ConversationDelegate(Speaker = true)]
		public static bool IfWearingBlueprint(DelegateContext Context)
		{
			return Context.Target.HasObjectEquipped(Context.Value, 1);
		}

		// Token: 0x06007A2B RID: 31275 RVA: 0x002DE6A8 File Offset: 0x002DC8A8
		[ConversationDelegate(Speaker = true)]
		public static bool IfHaveBlueprint(DelegateContext Context)
		{
			Inventory inventory = Context.Target.Inventory;
			Body body = Context.Target.Body;
			foreach (ReadOnlySpan<char> readOnlySpan in Context.Value.DelimitedBy(','))
			{
				if (inventory != null)
				{
					foreach (GameObject gameObject in inventory.Objects)
					{
						if (readOnlySpan.SequenceEqual<char>(gameObject.Blueprint))
						{
							return true;
						}
					}
				}
				if (body != null)
				{
					foreach (GameObject gameObject2 in body.GetEquippedObjectsReadonly())
					{
						if (!gameObject2.IsNatural() && gameObject2.Physics.IsReal && readOnlySpan.SequenceEqual<char>(gameObject2.Blueprint))
						{
							return true;
						}
					}
					continue;
				}
				continue;
			}
			return false;
		}

		// Token: 0x06007A2C RID: 31276 RVA: 0x002DE7D4 File Offset: 0x002DC9D4
		[ConversationDelegate(Speaker = true)]
		public static bool IfHaveItemDescendsFrom(DelegateContext Context)
		{
			Inventory inventory = Context.Target.Inventory;
			Body body = Context.Target.Body;
			if (inventory != null)
			{
				foreach (GameObject gameObject in inventory.Objects)
				{
					if (gameObject.Physics.IsReal && gameObject.GetBlueprint(true).DescendsFrom(Context.Value))
					{
						return true;
					}
				}
			}
			if (body != null)
			{
				foreach (GameObject gameObject2 in body.GetEquippedObjectsReadonly())
				{
					if (!gameObject2.IsNatural() && gameObject2.Physics.IsReal && gameObject2.GetBlueprint(true).DescendsFrom(Context.Value))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06007A2D RID: 31277 RVA: 0x002DE8D8 File Offset: 0x002DCAD8
		[ConversationDelegate(Speaker = true)]
		public static bool IfHaveTag(DelegateContext Context)
		{
			return Context.Target.HasTag(Context.Value);
		}

		// Token: 0x06007A2E RID: 31278 RVA: 0x002DE8EB File Offset: 0x002DCAEB
		[ConversationDelegate(Speaker = true)]
		public static bool IfHaveProperty(DelegateContext Context)
		{
			return Context.Target.HasProperty(Context.Value);
		}

		// Token: 0x06007A2F RID: 31279 RVA: 0x002DE8FE File Offset: 0x002DCAFE
		[ConversationDelegate(Speaker = true)]
		public static bool IfHaveTagOrProperty(DelegateContext Context)
		{
			return Context.Target.HasTagOrProperty(Context.Value);
		}

		// Token: 0x06007A30 RID: 31280 RVA: 0x002DE911 File Offset: 0x002DCB11
		[ConversationDelegate(Speaker = true)]
		public static bool IfLevelLessOrEqual(DelegateContext Context)
		{
			return Context.Target.Stat("Level", 0) <= Convert.ToInt32(Context.Value);
		}

		// Token: 0x06007A31 RID: 31281 RVA: 0x002DE934 File Offset: 0x002DCB34
		[ConversationDelegate(Speaker = true)]
		public static bool IfHaveLiquid(DelegateContext Context)
		{
			ReadOnlySpan<char> readOnlySpan;
			ReadOnlySpan<char> readOnlySpan2;
			Context.Value.AsDelimitedSpans(':', out readOnlySpan, out readOnlySpan2);
			if (!readOnlySpan.IsEmpty)
			{
				int freeDrams = Context.Target.GetFreeDrams(new string(readOnlySpan), null, null, null, false);
				if (freeDrams > 0)
				{
					if (readOnlySpan2.IsEmpty)
					{
						return true;
					}
					int num;
					if (int.TryParse(readOnlySpan2, out num) && freeDrams >= num)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06007A32 RID: 31282 RVA: 0x002DE991 File Offset: 0x002DCB91
		[ConversationDelegate(Speaker = true)]
		public static bool IfAtLeastRank(DelegateContext Context)
		{
			return Context.Target.IsAtLeastFactionRank(The.Speaker.GetPrimaryFaction(false), Context.Value);
		}

		// Token: 0x06007A33 RID: 31283 RVA: 0x002DE9AF File Offset: 0x002DCBAF
		[ConversationDelegate(Speaker = true)]
		public static bool IfAtLeastStanding(DelegateContext Context)
		{
			return Context.Target.IsAtLeastFactionStanding(The.Speaker.GetPrimaryFaction(false), Convert.ToInt32(Context.Value));
		}

		// Token: 0x06007A34 RID: 31284 RVA: 0x002DE9D2 File Offset: 0x002DCBD2
		[ConversationDelegate(Speaker = true)]
		public static bool IfHasWorshipped(DelegateContext Context)
		{
			return Context.Target.HasWorshippedBySpec(Context.Value, The.Speaker.GetPrimaryFaction(false));
		}

		// Token: 0x06007A35 RID: 31285 RVA: 0x002DE9F0 File Offset: 0x002DCBF0
		[ConversationDelegate(Speaker = true)]
		public static bool IfHasBlasphemed(DelegateContext Context)
		{
			return Context.Target.HasBlasphemedBySpec(Context.Value, The.Speaker.GetPrimaryFaction(false));
		}

		// Token: 0x06007A36 RID: 31286 RVA: 0x002DEA10 File Offset: 0x002DCC10
		[ConversationDelegate]
		public static bool IfQuestGiverState(DelegateContext Context)
		{
			int questGiverState = The.Game.GetQuestGiverState(The.Speaker);
			string value = Context.Value;
			if (value == "Finished")
			{
				return questGiverState == 2;
			}
			if (value == "Active")
			{
				return questGiverState == 1;
			}
			if (value == "Started")
			{
				return questGiverState >= 1;
			}
			if (!(value == "Unstarted"))
			{
				return value == "Invalid" && questGiverState == -1;
			}
			return questGiverState == 0;
		}

		// Token: 0x06007A37 RID: 31287 RVA: 0x002DEA94 File Offset: 0x002DCC94
		[ConversationDelegate]
		public static bool IfQuestSignpost(DelegateContext Context)
		{
			GameObject speaker = The.Speaker;
			Zone zone = ((speaker != null) ? speaker.CurrentZone : null);
			if (zone == null || The.Speaker.HasTagOrIntProperty("VillagePet") || The.Speaker.HasTagOrIntProperty("NoSignpost") || (Context.Value.EqualsNoCase("checkpoint") && !zone.IsCheckpoint()))
			{
				return false;
			}
			Context.Value = "Unstarted";
			return !ConversationDelegates.IfQuestGiverState(Context) && ConversationDelegates.IfZoneHaveQuestGiver(Context);
		}

		// Token: 0x06007A38 RID: 31288 RVA: 0x002DEB10 File Offset: 0x002DCD10
		[ConversationDelegate(Speaker = true)]
		public static void GiveLiquid(DelegateContext Context)
		{
			ReadOnlySpan<char> readOnlySpan;
			ReadOnlySpan<char> readOnlySpan2;
			Context.Value.AsDelimitedSpans(':', out readOnlySpan, out readOnlySpan2);
			if (!readOnlySpan.IsEmpty)
			{
				int num;
				if (readOnlySpan2.IsEmpty || !int.TryParse(readOnlySpan2, out num))
				{
					num = 1;
				}
				Context.Target.GiveDrams(num, new string(readOnlySpan), false, null, null, null, null, true, null);
			}
		}

		// Token: 0x06007A39 RID: 31289 RVA: 0x002DEB68 File Offset: 0x002DCD68
		[ConversationDelegate(Speaker = true)]
		public static void UseLiquid(DelegateContext Context)
		{
			string text;
			string text2;
			Context.Value.Split(':', out text, out text2);
			if (!text.IsNullOrEmpty())
			{
				int num;
				if (text2.IsNullOrEmpty() || !int.TryParse(text2, out num))
				{
					num = 1;
				}
				Context.Target.UseDrams(num, text, null, null, null, null, false);
			}
		}

		// Token: 0x06007A3A RID: 31290 RVA: 0x002DEBB8 File Offset: 0x002DCDB8
		[ConversationDelegate]
		[Obsolete("TODO: Replace usage with parts")]
		public static void Execute(DelegateContext Context)
		{
			try
			{
				string text;
				string text2;
				Context.Value.Split(':', out text, out text2);
				ModManager.ResolveType(text, false, false, true).GetMethod(text2).Invoke(null, null);
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Error invoking command:" + Context.Value, ex, "game_exception");
			}
		}

		// Token: 0x06007A3B RID: 31291 RVA: 0x002DEC20 File Offset: 0x002DCE20
		[ConversationDelegate(Speaker = true)]
		public unsafe static void AwardXP(DelegateContext Context)
		{
			if (Context.Value.IsNullOrEmpty())
			{
				return;
			}
			ReadOnlySpan<char> readOnlySpan = Context.Value.AsSpan();
			bool flag = true;
			if (readOnlySpan.Length > 0 && *readOnlySpan[0] == 33)
			{
				flag = false;
				readOnlySpan = readOnlySpan.Slice(1);
			}
			int num;
			if (int.TryParse(readOnlySpan, out num))
			{
				if (flag && Context.Target.IsPlayerControlled())
				{
					Context.Target.Physics.DidX("gain", "{{rules|" + num + "}} XP", "!", null, null, null, null, false, false, null, null, false, false, false, false, true, null);
				}
				Context.Target.AwardXP(num, -1, 0, int.MaxValue, null, (Context.Target == The.Speaker) ? The.Listener : The.Speaker, null, null, null, null);
			}
		}

		// Token: 0x06007A3C RID: 31292 RVA: 0x002DECF4 File Offset: 0x002DCEF4
		[ConversationDelegate]
		public static void FinishQuest(DelegateContext Context)
		{
			The.Game.FinishQuest(Context.Value);
		}

		// Token: 0x06007A3D RID: 31293 RVA: 0x002DED06 File Offset: 0x002DCF06
		[ConversationDelegate]
		public static void Achievement(DelegateContext Context)
		{
			AchievementManager.SetAchievement(Context.Value);
		}

		// Token: 0x06007A3E RID: 31294 RVA: 0x002DED14 File Offset: 0x002DCF14
		[ConversationDelegate(Speaker = true)]
		public static void FireEvent(DelegateContext Context)
		{
			string[] array = Context.Value.Split(',', StringSplitOptions.None);
			Event @event = new Event(array[0]);
			for (int i = 1; i < array.Length; i++)
			{
				string text;
				string text2;
				array[i].Split(':', out text, out text2);
				if (text2.IsNullOrEmpty())
				{
					@event.SetParameter(text, 1);
				}
				else
				{
					@event.SetParameter(text, text2);
				}
			}
			Context.Target.FireEvent(@event);
		}

		// Token: 0x06007A3F RID: 31295 RVA: 0x002DED80 File Offset: 0x002DCF80
		[ConversationDelegate]
		public static void SetStringState(DelegateContext Context)
		{
			string text;
			string text2;
			Context.Value.Split(',', out text, out text2);
			if (text2.IsNullOrEmpty())
			{
				The.Game.SetStringGameState(text, text2);
				return;
			}
			The.Game.RemoveStringGameState(text);
		}

		// Token: 0x06007A40 RID: 31296 RVA: 0x002DEDC0 File Offset: 0x002DCFC0
		[ConversationDelegate]
		public static void SetIntState(DelegateContext Context)
		{
			ReadOnlySpan<char> readOnlySpan;
			ReadOnlySpan<char> readOnlySpan2;
			Context.Value.AsDelimitedSpans(',', out readOnlySpan, out readOnlySpan2);
			if (readOnlySpan2.IsEmpty)
			{
				The.Game.RemoveIntGameState(new string(readOnlySpan));
				return;
			}
			int num;
			if (int.TryParse(readOnlySpan2, out num))
			{
				The.Game.SetIntGameState(new string(readOnlySpan), num);
			}
		}

		// Token: 0x06007A41 RID: 31297 RVA: 0x002DEE14 File Offset: 0x002DD014
		[ConversationDelegate]
		public static void AddIntState(DelegateContext Context)
		{
			ReadOnlySpan<char> readOnlySpan;
			ReadOnlySpan<char> readOnlySpan2;
			Context.Value.AsDelimitedSpans(',', out readOnlySpan, out readOnlySpan2);
			int num;
			if (!readOnlySpan2.IsEmpty && int.TryParse(readOnlySpan2, out num))
			{
				string text = new string(readOnlySpan);
				int num2;
				The.Game.IntGameState.TryGetValue(text, out num2);
				The.Game.IntGameState[text] = num2 + num;
			}
		}

		// Token: 0x06007A42 RID: 31298 RVA: 0x002DEE74 File Offset: 0x002DD074
		[ConversationDelegate]
		public static void SetBooleanState(DelegateContext Context)
		{
			ReadOnlySpan<char> readOnlySpan;
			ReadOnlySpan<char> readOnlySpan2;
			Context.Value.AsDelimitedSpans(',', out readOnlySpan, out readOnlySpan2);
			if (readOnlySpan2.IsEmpty)
			{
				The.Game.RemoveBooleanGameState(Context.Value);
				return;
			}
			bool flag;
			if (bool.TryParse(readOnlySpan2, out flag))
			{
				The.Game.SetBooleanGameState(new string(readOnlySpan), flag);
			}
		}

		// Token: 0x06007A43 RID: 31299 RVA: 0x002DEEC7 File Offset: 0x002DD0C7
		[ConversationDelegate]
		public static void ToggleBooleanState(DelegateContext Context)
		{
			The.Game.SetBooleanGameState(Context.Value, !The.Game.GetBooleanGameState(Context.Value, false));
		}

		// Token: 0x06007A44 RID: 31300 RVA: 0x002DEEF0 File Offset: 0x002DD0F0
		[ConversationDelegate(Speaker = true)]
		public static void SetStringProperty(DelegateContext Context)
		{
			string text;
			string text2;
			Context.Value.Split(',', out text, out text2);
			if (text2.IsNullOrEmpty())
			{
				Context.Target.RemoveStringProperty(text);
				return;
			}
			Context.Target.SetStringProperty(text, text2, false);
		}

		// Token: 0x06007A45 RID: 31301 RVA: 0x002DEF34 File Offset: 0x002DD134
		[ConversationDelegate(Speaker = true)]
		public static void SetIntProperty(DelegateContext Context)
		{
			ReadOnlySpan<char> readOnlySpan;
			ReadOnlySpan<char> readOnlySpan2;
			Context.Value.AsDelimitedSpans(',', out readOnlySpan, out readOnlySpan2);
			if (readOnlySpan2.IsEmpty)
			{
				Context.Target.RemoveIntProperty(Context.Value);
				return;
			}
			int num;
			if (int.TryParse(readOnlySpan2, out num))
			{
				Context.Target.SetIntProperty(new string(readOnlySpan), num, false);
			}
		}

		// Token: 0x06007A46 RID: 31302 RVA: 0x002DEF8C File Offset: 0x002DD18C
		[ConversationDelegate]
		public static void SetStringConversationState(DelegateContext Context)
		{
			string text;
			string text2;
			Context.Value.Split(',', out text, out text2);
			if (text2.IsNullOrEmpty())
			{
				The.Conversation.RemoveState(Context.Value);
				return;
			}
			The.Conversation[text] = text2;
		}

		// Token: 0x06007A47 RID: 31303 RVA: 0x002DEFD0 File Offset: 0x002DD1D0
		[ConversationDelegate]
		public static void SetIntConversationState(DelegateContext Context)
		{
			ReadOnlySpan<char> readOnlySpan;
			ReadOnlySpan<char> readOnlySpan2;
			Context.Value.AsDelimitedSpans(',', out readOnlySpan, out readOnlySpan2);
			if (readOnlySpan2.IsEmpty)
			{
				The.Conversation.RemoveState(Context.Value);
				return;
			}
			int num;
			if (int.TryParse(readOnlySpan2, out num))
			{
				The.Conversation[new string(readOnlySpan)] = num;
			}
		}

		// Token: 0x06007A48 RID: 31304 RVA: 0x002DF02C File Offset: 0x002DD22C
		[ConversationDelegate]
		public static void SetBooleanConversationState(DelegateContext Context)
		{
			ReadOnlySpan<char> readOnlySpan;
			ReadOnlySpan<char> readOnlySpan2;
			Context.Value.AsDelimitedSpans(',', out readOnlySpan, out readOnlySpan2);
			if (readOnlySpan2.IsEmpty)
			{
				The.Conversation.RemoveState(Context.Value);
				return;
			}
			bool flag;
			if (bool.TryParse(readOnlySpan2, out flag))
			{
				The.Conversation[new string(readOnlySpan)] = flag;
			}
		}

		// Token: 0x06007A49 RID: 31305 RVA: 0x002DF088 File Offset: 0x002DD288
		[ConversationDelegate]
		public static void SetLeader(DelegateContext Context)
		{
			GameObject speaker = The.Speaker;
			Brain brain = ((speaker != null) ? speaker.Brain : null);
			if (brain == null)
			{
				return;
			}
			if (Context.Value == "Listener")
			{
				brain.PartyLeader = The.Listener;
				return;
			}
			if (Context.Value == "Player")
			{
				brain.PartyLeader = The.Player;
				return;
			}
			Zone currentZone = The.Speaker.CurrentZone;
			GameObject gameObject = ((currentZone != null) ? currentZone.FindClosestObject(The.Speaker, (GameObject x) => x.Blueprint == Context.Value, false, true, true) : null);
			if (gameObject != null)
			{
				brain.PartyLeader = gameObject;
			}
		}

		// Token: 0x06007A4A RID: 31306 RVA: 0x002DF134 File Offset: 0x002DD334
		[ConversationDelegate]
		public static void CallScript(DelegateContext Context)
		{
			int num = Context.Value.LastIndexOf('.');
			if (num == -1)
			{
				return;
			}
			MethodInfo method = ModManager.ResolveType(Context.Value.Substring(0, num), false, false, true).GetMethod(Context.Value.Substring(num + 1));
			if (method == null)
			{
				return;
			}
			method.Invoke(null, null);
		}

		// Token: 0x06007A4B RID: 31307 RVA: 0x002DF18C File Offset: 0x002DD38C
		[ConversationDelegate]
		public static void ClearOwner(DelegateContext Context)
		{
			Zone activeZone = The.ActiveZone;
			for (int i = 0; i < activeZone.Height; i++)
			{
				for (int j = 0; j < activeZone.Width; j++)
				{
					int k = 0;
					int count = activeZone.Map[j][i].Objects.Count;
					while (k < count)
					{
						if (activeZone.Map[j][i].Objects[k].HasTagOrProperty(Context.Value))
						{
							activeZone.Map[j][i].Objects[k].Physics.Owner = null;
						}
						k++;
					}
				}
			}
		}

		// Token: 0x06007A4C RID: 31308 RVA: 0x002DF229 File Offset: 0x002DD429
		[ConversationDelegate]
		public static void RevealMapNoteByID(DelegateContext Context)
		{
			ConversationDelegates.RevealMapNote(Context);
		}

		// Token: 0x06007A4D RID: 31309 RVA: 0x002DF234 File Offset: 0x002DD434
		[ConversationDelegate]
		public static void RevealMapNote(DelegateContext Context)
		{
			JournalMapNote mapNote = JournalAPI.GetMapNote(Context.Value);
			GameObject speaker = The.Speaker;
			string text;
			if (speaker == null)
			{
				text = null;
			}
			else
			{
				Brain brain = speaker.Brain;
				text = ((brain != null) ? brain.GetPrimaryFaction(false) : null);
			}
			string text2 = text;
			if (mapNote == null || text2.IsNullOrEmpty())
			{
				return;
			}
			Faction faction = Factions.Get(text2);
			mapNote.Attributes.Add(faction.NoBuySecretString);
			if (faction.Visible)
			{
				mapNote.AppendHistory(" {{K|-learned from " + faction.GetFormattedName() + "}}");
			}
			JournalAPI.RevealMapNote(mapNote, false, null);
		}

		// Token: 0x06007A4E RID: 31310 RVA: 0x002DF2BC File Offset: 0x002DD4BC
		[ConversationDelegate]
		public static void RevealObservation(DelegateContext Context)
		{
			JournalObservation observation = JournalAPI.GetObservation(Context.Value);
			GameObject speaker = The.Speaker;
			string text;
			if (speaker == null)
			{
				text = null;
			}
			else
			{
				Brain brain = speaker.Brain;
				text = ((brain != null) ? brain.GetPrimaryFaction(false) : null);
			}
			string text2 = text;
			if (observation == null || text2.IsNullOrEmpty())
			{
				return;
			}
			Faction faction = Factions.Get(text2);
			observation.Attributes.Add(faction.NoBuySecretString);
			if (faction.Visible)
			{
				observation.AppendHistory(" {{K|-learned from " + faction.GetFormattedName() + "}}");
			}
			JournalAPI.RevealObservation(observation);
		}

		// Token: 0x06007A4F RID: 31311 RVA: 0x002DF341 File Offset: 0x002DD541
		[ConversationDelegate(Speaker = true)]
		public static void Notify(DelegateContext Context)
		{
			GenericNotifyEvent.Send(Context.Target, Context.Value, null, null, 0);
		}

		// Token: 0x06007A50 RID: 31312 RVA: 0x002DF357 File Offset: 0x002DD557
		[ConversationDelegate(Speaker = true)]
		public static void PromoteIfBelow(DelegateContext Context)
		{
			Context.Target.PromoteIfBelow(The.Speaker.GetPrimaryFaction(false), Context.Value, true, false, true);
		}

		// Token: 0x06007A51 RID: 31313 RVA: 0x002DF37C File Offset: 0x002DD57C
		[ConversationDelegate(Speaker = true)]
		public static void EnableWorship(DelegateContext Context)
		{
			if (!Context.Value.IsNullOrEmpty())
			{
				Dictionary<string, string> dictionary = Context.Value.CachedDictionaryExpansion();
				string text;
				dictionary.TryGetValue("Name", out text);
				string primaryFaction;
				dictionary.TryGetValue("Faction", out primaryFaction);
				string text2;
				dictionary.TryGetValue("Power", out text2);
				int num = ((text2 != null) ? text2.RollCached() : 0);
				if (primaryFaction.IsNullOrEmpty() || primaryFaction == "*context*")
				{
					primaryFaction = The.Speaker.GetPrimaryFaction(false);
				}
				if (!text.IsNullOrEmpty() && !primaryFaction.IsNullOrEmpty())
				{
					Factions.RegisterWorshippable(text, primaryFaction, num);
				}
			}
		}

		// Token: 0x06007A52 RID: 31314 RVA: 0x002DF414 File Offset: 0x002DD614
		[ConversationDelegate(Speaker = true)]
		public static void PerformWorship(DelegateContext Context)
		{
			if (!Context.Value.IsNullOrEmpty())
			{
				Dictionary<string, string> dictionary = Context.Value.CachedDictionaryExpansion();
				string text;
				dictionary.TryGetValue("Name", out text);
				string primaryFaction;
				dictionary.TryGetValue("Faction", out primaryFaction);
				if (primaryFaction.IsNullOrEmpty() || primaryFaction == "*context*")
				{
					primaryFaction = The.Speaker.GetPrimaryFaction(false);
				}
				if (!text.IsNullOrEmpty() && !primaryFaction.IsNullOrEmpty())
				{
					Worshippable worshippable = Factions.FindWorshippable(primaryFaction, text);
					if (worshippable == null)
					{
						string text2;
						dictionary.TryGetValue("Power", out text2);
						int num = ((text2 != null) ? text2.RollCached() : 0);
						worshippable = Factions.RegisterWorshippable(text, primaryFaction, num);
					}
					if (worshippable != null)
					{
						WorshipPerformedEvent.Send(Context.Target, The.Speaker, worshippable, null);
					}
				}
			}
		}

		// Token: 0x06007A53 RID: 31315 RVA: 0x002DF4D4 File Offset: 0x002DD6D4
		[ConversationDelegate(Speaker = true)]
		public static void PerformBlasphemy(DelegateContext Context)
		{
			if (!Context.Value.IsNullOrEmpty())
			{
				Dictionary<string, string> dictionary = Context.Value.CachedDictionaryExpansion();
				string text;
				dictionary.TryGetValue("Name", out text);
				string primaryFaction;
				dictionary.TryGetValue("Faction", out primaryFaction);
				if (primaryFaction.IsNullOrEmpty() || primaryFaction == "*context*")
				{
					primaryFaction = The.Speaker.GetPrimaryFaction(false);
				}
				if (!text.IsNullOrEmpty() && !primaryFaction.IsNullOrEmpty())
				{
					Worshippable worshippable = Factions.FindWorshippable(primaryFaction, text);
					if (worshippable == null)
					{
						string text2;
						dictionary.TryGetValue("Power", out text2);
						int num = ((text2 != null) ? text2.RollCached() : 0);
						worshippable = Factions.RegisterWorshippable(text, primaryFaction, num);
					}
					if (worshippable != null)
					{
						BlasphemyPerformedEvent.Send(Context.Target, The.Speaker, worshippable, null);
					}
				}
			}
		}

		// Token: 0x06007A54 RID: 31316 RVA: 0x002DF592 File Offset: 0x002DD792
		[ConversationDelegate]
		public static IConversationPart StartQuest(DelegateContext Context)
		{
			return new QuestHandler(Context.Value, null, -1, 1);
		}

		// Token: 0x06007A55 RID: 31317 RVA: 0x002DF5A2 File Offset: 0x002DD7A2
		[ConversationDelegate]
		public static IConversationPart IfHaveSecret(DelegateContext Context)
		{
			return new SecretHandler(Context.Value, false, false);
		}

		// Token: 0x06007A56 RID: 31318 RVA: 0x002DF5B1 File Offset: 0x002DD7B1
		[ConversationDelegate]
		public static IConversationPart RequireSecret(DelegateContext Context)
		{
			return new SecretHandler(Context.Value, true, false);
		}

		// Token: 0x06007A57 RID: 31319 RVA: 0x002DF5C0 File Offset: 0x002DD7C0
		[ConversationDelegate]
		public static IConversationPart FlagSecret(DelegateContext Context)
		{
			return new SecretHandler(Context.Value, false, true);
		}

		// Token: 0x06007A58 RID: 31320 RVA: 0x002DF5CF File Offset: 0x002DD7CF
		[ConversationDelegate]
		public static IConversationPart GiveItem(DelegateContext Context)
		{
			return new ReceiveItem(Context.Value);
		}

		// Token: 0x06007A59 RID: 31321 RVA: 0x002DF5DC File Offset: 0x002DD7DC
		[ConversationDelegate]
		public static IConversationPart TakeItem(DelegateContext Context)
		{
			return new TakeItem(Context.Value);
		}

		// Token: 0x06007A5A RID: 31322 RVA: 0x002DF5E9 File Offset: 0x002DD7E9
		[ConversationDelegate(Speaker = true)]
		public static IConversationPart RemoveItem(DelegateContext Context)
		{
			return new TakeItem
			{
				FromSpeaker = !Context.Target.IsPlayer(),
				Blueprints = Context.Value,
				Remove = true
			};
		}

		// Token: 0x06007A5B RID: 31323 RVA: 0x002DF618 File Offset: 0x002DD818
		[ConversationDelegate]
		public static IConversationPart CompleteQuestStep(DelegateContext Context)
		{
			string[] array = Context.Value.Split('~', StringSplitOptions.None);
			int num = -1;
			int num2 = array[1].IndexOf('|');
			if (num2 >= 0)
			{
				if (!int.TryParse(array[1].Substring(num2 + 1), out num))
				{
					num = -1;
				}
				array[1] = array[1].Substring(0, num2);
			}
			return new QuestHandler(array[0], array[1], num, 2);
		}

		// Token: 0x040037C9 RID: 14281
		[ModSensitiveStaticCache(false)]
		private static Dictionary<string, PredicateReceiver> _Predicates;

		// Token: 0x040037CA RID: 14282
		[ModSensitiveStaticCache(false)]
		private static Dictionary<string, ActionReceiver> _Actions;

		// Token: 0x040037CB RID: 14283
		[ModSensitiveStaticCache(false)]
		private static Dictionary<string, PartGeneratorReceiver> _PartGenerators;
	}
}
