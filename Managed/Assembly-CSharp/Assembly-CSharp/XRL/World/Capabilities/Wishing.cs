using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using ConsoleLib.Console;
using Genkit;
using HistoryKit;
using JoppaTutorial;
using Qud.API;
using Qud.UI;
using Sheeter;
using UnityEngine;
using XRL.Annals;
using XRL.Collections;
using XRL.Core;
using XRL.Language;
using XRL.Liquids;
using XRL.Messages;
using XRL.Rules;
using XRL.UI;
using XRL.Wish;
using XRL.World.AI;
using XRL.World.AI.GoalHandlers;
using XRL.World.Anatomy;
using XRL.World.Conversations.Parts;
using XRL.World.Effects;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;
using XRL.World.Parts.Skill;
using XRL.World.QuestManagers;
using XRL.World.Quests;
using XRL.World.Quests.GolemQuest;
using XRL.World.Skills.Cooking;
using XRL.World.Tinkering;
using XRL.World.ZoneBuilders;
using XRL.World.ZoneParts;

namespace XRL.World.Capabilities
{
	// Token: 0x020009FC RID: 2556
	public static class Wishing
	{
		// Token: 0x06006CEF RID: 27887 RVA: 0x00238B0C File Offset: 0x00236D0C
		public static void HandleWish(GameObject who, string Wish)
		{
			if (WishManager.HandleWish(Wish))
			{
				return;
			}
			XRLGame game = The.Game;
			if (Wish.StartsWith("conv:"))
			{
				string[] array = Wish.Split(':', StringSplitOptions.None);
				GameObject gameObject;
				if (array.Length >= 3)
				{
					gameObject = GameObjectFactory.Factory.CreateObject(array[2]);
				}
				else
				{
					gameObject = GameObjectFactory.Factory.CreateObject("BaseHumanoid");
				}
				ConversationUI.HaveConversation(array[1], gameObject, null, null, null, true, false, false);
				return;
			}
			if (Wish.StartsWith("startquest:"))
			{
				The.Game.StartQuest(Wish.Split(':', StringSplitOptions.None)[1], null, null, null);
				return;
			}
			if (Wish.StartsWith("completequest:"))
			{
				The.Game.CompleteQuest(Wish.Split(':', StringSplitOptions.None)[1]);
				return;
			}
			if (Wish.StartsWith("questdebug"))
			{
				for (;;)
				{
					IL_00F4:
					List<string> list = new List<string>();
					List<Quest> list2 = new List<Quest>();
					foreach (Quest quest in The.Game.Quests.Values)
					{
						if (!The.Game.FinishedQuests.ContainsKey(quest.ID))
						{
							list2.Add(quest);
							list.Add(quest.Name);
						}
					}
					int num = Popup.PickOption("<Quest Debug>", null, "", "Sounds/UI/ui_notification", list.ToArray(), null, null, null, null, null, null, 0, 60, 0, -1, true, false, false, true, false, null, null);
					if (num < 0)
					{
						break;
					}
					for (;;)
					{
						list = new List<string> { "info", "complete", "steps" };
						int num2 = Popup.PickOption("Debug: " + list2[num].Name, null, "", "Sounds/UI/ui_notification", list.ToArray(), null, null, null, null, null, null, 0, 60, 0, -1, true, false, false, true, false, null, null);
						if (num2 == 0)
						{
							break;
						}
						if (num2 == 1)
						{
							goto Block_8;
						}
						if (num2 != 2)
						{
							goto IL_00F4;
						}
						list = new List<string>();
						List<QuestStep> list3 = new List<QuestStep>();
						foreach (KeyValuePair<string, QuestStep> keyValuePair in list2[num].StepsByID)
						{
							list3.Add(keyValuePair.Value);
							list.Add(keyValuePair.Key);
						}
						int num3 = Popup.PickOption("Pick step from " + list2[num].Name, null, "", "Sounds/UI/ui_notification", list.ToArray(), null, null, null, null, null, null, 0, 60, 0, -1, true, false, false, true, false, null, null);
						if (num3 >= 0)
						{
							int num4;
							for (;;)
							{
								list = new List<string> { "info", "finish" };
								string text = "Debug step ";
								QuestStep questStep = list3[num3];
								num4 = Popup.PickOption(text + ((questStep != null) ? questStep.ToString() : null), null, "", "Sounds/UI/ui_notification", list.ToArray(), null, null, null, null, null, null, 0, 60, 0, -1, true, false, false, true, false, null, null);
								if (num4 != 0)
								{
									break;
								}
								Popup.Show(list3[num3].ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
							}
							if (num4 == 1)
							{
								game.FinishQuestStep(list2[num].ID, list3[num3].ID, -1, true, null);
							}
						}
					}
					Popup.Show(list2[num].ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					continue;
					Block_8:
					game.CompleteQuest(list2[num].ID);
				}
			}
			else
			{
				if (Wish.StartsWith("finishallquests"))
				{
					using (StringMap<Quest>.Enumerator enumerator3 = The.Game.Quests.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							KeyValuePair<string, Quest> keyValuePair2 = enumerator3.Current;
							keyValuePair2.Value.Finish();
						}
						return;
					}
				}
				if (Wish.StartsWith("finishqueststep:"))
				{
					The.Game.FinishQuestStep(Wish.Split(':', StringSplitOptions.None)[1], Wish.Split(':', StringSplitOptions.None)[2], -1, true, null);
					return;
				}
				if (Wish.StartsWith("geno:"))
				{
					Zone currentZone = who.CurrentZone;
					string text2 = Wish.Split(':', StringSplitOptions.None)[1];
					for (int i = 0; i < currentZone.Height; i++)
					{
						for (int j = 0; j < currentZone.Width; j++)
						{
							foreach (GameObject gameObject2 in currentZone.GetCell(j, i).GetObjectsWithPart("Physics"))
							{
								if (gameObject2.Blueprint.EqualsNoCase(text2))
								{
									gameObject2.Obliterate(null, false, null);
								}
								else if (gameObject2.DisplayNameOnlyStripped.EqualsNoCase(text2))
								{
									gameObject2.Obliterate(null, false, null);
								}
								else if (gameObject2.DisplayNameStripped.EqualsNoCase(text2))
								{
									gameObject2.Obliterate(null, false, null);
								}
							}
						}
					}
					return;
				}
				if (Wish.StartsWith("deathgeno:"))
				{
					Zone currentZone2 = who.CurrentZone;
					string text3 = Wish.Split(':', StringSplitOptions.None)[1];
					for (int k = 0; k < currentZone2.Height; k++)
					{
						for (int l = 0; l < currentZone2.Width; l++)
						{
							foreach (GameObject gameObject3 in currentZone2.GetCell(l, k).GetObjectsWithPart("Physics"))
							{
								if (gameObject3.Blueprint.EqualsNoCase(text3))
								{
									gameObject3.Die(who, "wished dead", null, null, false, null, null, false, false, null, null, null);
								}
								else if (gameObject3.DisplayNameOnlyStripped.EqualsNoCase(text3))
								{
									gameObject3.Die(who, "wished dead", null, null, false, null, null, false, false, null, null, null);
								}
								else if (gameObject3.DisplayNameStripped.EqualsNoCase(text3))
								{
									gameObject3.Die(who, "wished dead", null, null, false, null, null, false, false, null, null, null);
								}
							}
						}
					}
					return;
				}
				if (Wish == "destroy")
				{
					Cell cell = The.Player.Physics.PickDestinationCell(9999, AllowVis.OnlyVisible, true, false, false, true, PickTarget.PickStyle.EmptyCell, "Destroy which object?", false, null);
					if (cell != null)
					{
						cell.GetHighestRenderLayerObject().Destroy(null, false, false, null);
						return;
					}
				}
				else if (Wish == "obliterate")
				{
					Cell cell2 = The.Player.Physics.PickDestinationCell(9999, AllowVis.OnlyVisible, true, false, false, true, PickTarget.PickStyle.EmptyCell, "Obliterate which object?", false, null);
					if (cell2 != null)
					{
						cell2.GetHighestRenderLayerObject().Obliterate(null, false, null);
						return;
					}
				}
				else
				{
					if (Wish == "showcharset")
					{
						string[] array2 = new string[255];
						for (int m = 1; m <= 255; m++)
						{
							string text4;
							if (m == 10)
							{
								text4 = "(newline)";
							}
							else
							{
								text4 = ((char)m).ToString() ?? "";
							}
							array2[m - 1] = m.ToString().PadLeft(3, 'ÿ') + ": " + ConsoleLib.Console.ColorUtility.EscapeFormatting(text4);
						}
						Popup.PickOption("charset", null, "", "Sounds/UI/ui_notification", array2, null, null, null, null, null, null, 0, 60, 0, -1, true, false, false, true, false, null, null);
						return;
					}
					if (Wish == "die")
					{
						The.Player.Die(The.Player, "wished dead", null, null, false, null, null, false, false, null, null, null);
						return;
					}
					if (Wish == "collapseinterior")
					{
						InteriorZone active = InteriorZone.Active;
						if (active == null)
						{
							return;
						}
						GameObject parentObject = active.ParentObject;
						if (parentObject == null)
						{
							return;
						}
						parentObject.Die(who, "wished dead", null, null, false, null, null, false, false, null, null, null);
						return;
					}
					else
					{
						if (Wish.StartsWith("allaggroxp"))
						{
							int num5 = 0;
							Zone parentZone = who.CurrentCell.ParentZone;
							for (int n2 = 0; n2 < parentZone.Height; n2++)
							{
								for (int num6 = 0; num6 < parentZone.Width; num6++)
								{
									foreach (GameObject gameObject4 in parentZone.GetCell(num6, n2).GetObjectsWithPart("Brain"))
									{
										if (gameObject4.Brain.IsHostileTowards(The.Player))
										{
											num5 += gameObject4.AwardXPTo(The.Player, true, null, true);
										}
									}
								}
							}
							MessageQueue.AddPlayerMessage("Total xp: " + num5.ToString(), null, true);
							return;
						}
						if (Wish.StartsWith("testhero:"))
						{
							GameObject gameObject5 = GameObjectFactory.Factory.CreateObject(Wish.Split(':', StringSplitOptions.None)[1]);
							HeroMaker.MakeHero(gameObject5, null, null, -1, "Hero");
							who.CurrentCell.GetCellFromDirection("E", true).AddObject(gameObject5, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null).MakeActive(false);
							return;
						}
						if (Wish.StartsWith("testwarden:"))
						{
							GameObject gameObject6 = GameObjectFactory.Factory.CreateObject(Wish.Split(':', StringSplitOptions.None)[1]);
							HeroMaker.MakeHero(gameObject6, null, null, -1, "Hero");
							string text5 = HistoricStringExpander.ExpandString("<spice.villages.warden.introDialog.!random>", null);
							gameObject6.SetIntProperty("SuppressSimpleConversation", 1, false);
							ConversationsAPI.addSimpleConversationToObject(gameObject6, text5, "Live and drink.", null, null, null, true, true);
							TakeOnRoleEvent.Send(gameObject6, "Warden");
							gameObject6.RequirePart<Interesting>(false);
							gameObject6.SetIntProperty("VillageWarden", 1, false);
							gameObject6.SetIntProperty("NamedVillager", 1, false);
							gameObject6.Brain.Mobile = true;
							gameObject6.Brain.Factions = "";
							gameObject6.Brain.Allegiance.Clear();
							gameObject6.Brain.Allegiance.Add("Wardens", 100);
							gameObject6.Brain.Allegiance.Hostile = false;
							gameObject6.Brain.Allegiance.Calm = true;
							who.CurrentCell.GetCellFromDirection("E", true).AddObject(gameObject6, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null).MakeActive(false);
							return;
						}
						if (Wish.StartsWith("animatedhero"))
						{
							GameObject anAnimatedObject = EncountersAPI.GetAnAnimatedObject();
							HeroMaker.MakeHero(anAnimatedObject, null, null, -1, "Hero");
							who.CurrentCell.GetCellFromDirection("E", true).AddObject(anAnimatedObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null).MakeActive(false);
							return;
						}
						if (Wish.StartsWith("showstringproperty:"))
						{
							string text6 = Wish.Split(':', StringSplitOptions.None)[1];
							if (who.HasStringProperty(text6))
							{
								Popup.Show(who.GetStringProperty(text6, null), null, "Sounds/UI/ui_notification", true, true, true, true, null);
								return;
							}
							Popup.Show("no string property '" + text6 + "' found", null, "Sounds/UI/ui_notification", true, true, true, true, null);
							return;
						}
						else
						{
							if (Wish.StartsWith("setintproperty "))
							{
								string[] array3 = Wish.Split(' ', StringSplitOptions.None);
								who.SetIntProperty(array3[1], Convert.ToInt32(array3[2]), false);
								return;
							}
							if (Wish.StartsWith("pushgameview "))
							{
								string text7 = Wish.Split(' ', StringSplitOptions.None)[1];
								GameManager.Instance.PushGameView(text7, true);
								return;
							}
							if (Wish.StartsWith("showintproperty:"))
							{
								string text8 = Wish.Split(':', StringSplitOptions.None)[1];
								if (who.HasIntProperty(text8))
								{
									int num7 = who.GetIntProperty(text8, 0);
									Popup.Show(num7.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
									return;
								}
								Popup.Show("no int property '" + text8 + "' found", null, "Sounds/UI/ui_notification", true, true, true, true, null);
								return;
							}
							else if (Wish == "fire")
							{
								Cell cell3 = who.Physics.PickDirection("Fire", null);
								if (cell3 != null)
								{
									GameObject gameObject7 = cell3.GetCombatTarget(who, true, false, false, 0, null, null, null, null, null, true, false, null) ?? cell3.GetCombatTarget(who, true, false, false, 5, null, null, null, null, null, true, false, null);
									if (gameObject7 != null && gameObject7.Physics != null)
									{
										gameObject7.Physics.Temperature = gameObject7.Physics.FlameTemperature + 200;
										return;
									}
								}
							}
							else if (Wish == "zap")
							{
								Cell cell4 = who.Physics.PickDirection("Zap", null);
								if (cell4 != null)
								{
									who.Discharge(cell4, 3, 0, "5d6", null, who, who, null, null, null, null, null, 0, false, false, false);
									return;
								}
							}
							else
							{
								if (Wish == "gas")
								{
									using (List<Cell>.Enumerator enumerator5 = The.PlayerCell.ParentZone.GetCells().GetEnumerator())
									{
										while (enumerator5.MoveNext())
										{
											Cell cell5 = enumerator5.Current;
											cell5.AddObject("MiasmaticAsh200", null, null, null, null);
										}
										return;
									}
								}
								if (Wish == "gas2")
								{
									using (List<Cell>.Enumerator enumerator5 = The.PlayerCell.ParentZone.GetCells().GetEnumerator())
									{
										while (enumerator5.MoveNext())
										{
											Cell cell6 = enumerator5.Current;
											cell6.AddObject("GlitterGas80", null, null, null, null);
										}
										return;
									}
								}
								if (Wish == "gas3")
								{
									using (List<Cell>.Enumerator enumerator5 = The.PlayerCell.ParentZone.GetCells().GetEnumerator())
									{
										while (enumerator5.MoveNext())
										{
											Cell cell7 = enumerator5.Current;
											cell7.AddObject("SaltyWaterExtraDeepPool", null, null, null, null);
										}
										return;
									}
								}
								if (Wish == "gates")
								{
									List<List<string>> list4 = The.Game.GetObjectGameState("JoppaWorldTeleportGate2Rings") as List<List<string>>;
									List<List<string>> list5 = The.Game.GetObjectGameState("JoppaWorldTeleportGate3Rings") as List<List<string>>;
									List<List<string>> list6 = The.Game.GetObjectGameState("JoppaWorldTeleportGate4Rings") as List<List<string>>;
									List<string> list7 = The.Game.GetObjectGameState("JoppaWorldTeleportGateSecants") as List<string>;
									List<string> list8 = The.Game.GetObjectGameState("JoppaWorldTeleportGateZones") as List<string>;
									StringBuilder stringBuilder = Event.NewStringBuilder(null);
									if (list4 != null && list4.Count > 0)
									{
										stringBuilder.Compound("2-rings:\n", "\n");
										using (List<List<string>>.Enumerator enumerator6 = list4.GetEnumerator())
										{
											while (enumerator6.MoveNext())
											{
												List<string> list9 = enumerator6.Current;
												stringBuilder.Append('\n').Append(The.ZoneManager.GetZoneProperty(list9[0], "TeleportGateName", false, null) ?? "").Append(' ')
													.Append(list9[0])
													.Append(The.ZoneManager.HasZoneProperty(list9[0], "TeleportGateCandidateNameRoot") ? " (" : "")
													.Append(The.ZoneManager.GetZoneProperty(list9[0], "TeleportGateCandidateNameRoot", false, null) ?? "")
													.Append(The.ZoneManager.HasZoneProperty(list9[0], "TeleportGateCandidateNameRoot") ? ")" : "")
													.Append(' ')
													.Append(The.ZoneManager.GetZoneDisplayName(list9[0], false, false, true, true))
													.Append('\n')
													.Append(The.ZoneManager.GetZoneProperty(list9[1], "TeleportGateName", false, null) ?? "")
													.Append(' ')
													.Append(list9[1])
													.Append(The.ZoneManager.HasZoneProperty(list9[1], "TeleportGateCandidateNameRoot") ? " (" : "")
													.Append(The.ZoneManager.GetZoneProperty(list9[1], "TeleportGateCandidateNameRoot", false, null) ?? "")
													.Append(The.ZoneManager.HasZoneProperty(list9[1], "TeleportGateCandidateNameRoot") ? ")" : "")
													.Append(' ')
													.Append(The.ZoneManager.GetZoneDisplayName(list9[1], false, false, true, true))
													.Append('\n');
											}
											goto IL_1179;
										}
									}
									stringBuilder.Compound("No 2-rings found!\n", '\n');
									IL_1179:
									if (list5 != null && list5.Count > 0)
									{
										stringBuilder.Compound("3-rings:\n", "\n");
										using (List<List<string>>.Enumerator enumerator6 = list5.GetEnumerator())
										{
											while (enumerator6.MoveNext())
											{
												List<string> list10 = enumerator6.Current;
												stringBuilder.Append('\n').Append(The.ZoneManager.GetZoneProperty(list10[0], "TeleportGateName", false, null) ?? "").Append(' ')
													.Append(list10[0])
													.Append(The.ZoneManager.HasZoneProperty(list10[0], "TeleportGateCandidateNameRoot") ? " (" : "")
													.Append(The.ZoneManager.GetZoneProperty(list10[0], "TeleportGateCandidateNameRoot", false, null) ?? "")
													.Append(The.ZoneManager.HasZoneProperty(list10[0], "TeleportGateCandidateNameRoot") ? ")" : "")
													.Append(' ')
													.Append(The.ZoneManager.GetZoneDisplayName(list10[0], false, false, true, true))
													.Append('\n')
													.Append(The.ZoneManager.GetZoneProperty(list10[1], "TeleportGateName", false, null) ?? "")
													.Append(' ')
													.Append(list10[1])
													.Append(The.ZoneManager.HasZoneProperty(list10[1], "TeleportGateCandidateNameRoot") ? " (" : "")
													.Append(The.ZoneManager.GetZoneProperty(list10[1], "TeleportGateCandidateNameRoot", false, null) ?? "")
													.Append(The.ZoneManager.HasZoneProperty(list10[1], "TeleportGateCandidateNameRoot") ? ")" : "")
													.Append(' ')
													.Append(The.ZoneManager.GetZoneDisplayName(list10[1], false, false, true, true))
													.Append('\n')
													.Append(The.ZoneManager.GetZoneProperty(list10[2], "TeleportGateName", false, null) ?? "")
													.Append(' ')
													.Append(list10[2])
													.Append(The.ZoneManager.HasZoneProperty(list10[2], "TeleportGateCandidateNameRoot") ? " (" : "")
													.Append(The.ZoneManager.GetZoneProperty(list10[2], "TeleportGateCandidateNameRoot", false, null) ?? "")
													.Append(The.ZoneManager.HasZoneProperty(list10[2], "TeleportGateCandidateNameRoot") ? ")" : "")
													.Append(' ')
													.Append(The.ZoneManager.GetZoneDisplayName(list10[2], false, false, true, true))
													.Append('\n');
											}
											goto IL_1488;
										}
									}
									stringBuilder.Compound("No 3-rings found!\n", '\n');
									IL_1488:
									if (list6 != null && list6.Count > 0)
									{
										stringBuilder.Compound("4-rings:\n", "\n");
										using (List<List<string>>.Enumerator enumerator6 = list6.GetEnumerator())
										{
											while (enumerator6.MoveNext())
											{
												List<string> list11 = enumerator6.Current;
												stringBuilder.Append('\n').Append(The.ZoneManager.GetZoneProperty(list11[0], "TeleportGateName", false, null) ?? "").Append(' ')
													.Append(list11[0])
													.Append(The.ZoneManager.HasZoneProperty(list11[0], "TeleportGateCandidateNameRoot") ? " (" : "")
													.Append(The.ZoneManager.GetZoneProperty(list11[0], "TeleportGateCandidateNameRoot", false, null) ?? "")
													.Append(The.ZoneManager.HasZoneProperty(list11[0], "TeleportGateCandidateNameRoot") ? ")" : "")
													.Append(' ')
													.Append(The.ZoneManager.GetZoneDisplayName(list11[0], false, false, true, true))
													.Append('\n')
													.Append(The.ZoneManager.GetZoneProperty(list11[1], "TeleportGateName", false, null) ?? "")
													.Append(' ')
													.Append(list11[1])
													.Append(The.ZoneManager.HasZoneProperty(list11[1], "TeleportGateCandidateNameRoot") ? " (" : "")
													.Append(The.ZoneManager.GetZoneProperty(list11[1], "TeleportGateCandidateNameRoot", false, null) ?? "")
													.Append(The.ZoneManager.HasZoneProperty(list11[1], "TeleportGateCandidateNameRoot") ? ")" : "")
													.Append(' ')
													.Append(The.ZoneManager.GetZoneDisplayName(list11[1], false, false, true, true))
													.Append('\n')
													.Append(The.ZoneManager.GetZoneProperty(list11[2], "TeleportGateName", false, null) ?? "")
													.Append(' ')
													.Append(list11[2])
													.Append(The.ZoneManager.HasZoneProperty(list11[2], "TeleportGateCandidateNameRoot") ? " (" : "")
													.Append(The.ZoneManager.GetZoneProperty(list11[2], "TeleportGateCandidateNameRoot", false, null) ?? "")
													.Append(The.ZoneManager.HasZoneProperty(list11[2], "TeleportGateCandidateNameRoot") ? ")" : "")
													.Append(' ')
													.Append(The.ZoneManager.GetZoneDisplayName(list11[2], false, false, true, true))
													.Append('\n')
													.Append(The.ZoneManager.GetZoneProperty(list11[3], "TeleportGateName", false, null) ?? "")
													.Append(' ')
													.Append(list11[3])
													.Append(The.ZoneManager.HasZoneProperty(list11[3], "TeleportGateCandidateNameRoot") ? " (" : "")
													.Append(The.ZoneManager.GetZoneProperty(list11[3], "TeleportGateCandidateNameRoot", false, null) ?? "")
													.Append(The.ZoneManager.HasZoneProperty(list11[3], "TeleportGateCandidateNameRoot") ? ")" : "")
													.Append(' ')
													.Append(The.ZoneManager.GetZoneDisplayName(list11[3], false, false, true, true))
													.Append('\n');
											}
											goto IL_1876;
										}
									}
									stringBuilder.Compound("No 4-rings found!\n", '\n');
									IL_1876:
									if (list7 != null && list7.Count > 0)
									{
										stringBuilder.Compound("Secants:\n", "\n");
										using (List<string>.Enumerator enumerator7 = list7.GetEnumerator())
										{
											while (enumerator7.MoveNext())
											{
												string text9 = enumerator7.Current;
												stringBuilder.Append('\n').Append(text9).Append(' ')
													.Append(The.ZoneManager.GetZoneDisplayName(text9, false, false, true, true))
													.Append('\n');
												string text10 = The.ZoneManager.GetZoneProperty(text9, "TeleportGateDestinationZone", false, null) as string;
												if (!string.IsNullOrEmpty(text10))
												{
													stringBuilder.Append("to ").Append(text10).Append(' ')
														.Append(The.ZoneManager.GetZoneDisplayName(text10, false, false, true, true))
														.Append('\n');
												}
												else
												{
													stringBuilder.Append("no destination set\n");
												}
											}
											goto IL_197B;
										}
									}
									stringBuilder.Compound("No secants found!\n", '\n');
									IL_197B:
									if (list8 != null && list8.Count > 0)
									{
										stringBuilder.Compound(list8.Count, "\n").Append(" total gate zones in the above:\n\n");
										using (List<string>.Enumerator enumerator7 = list8.GetEnumerator())
										{
											while (enumerator7.MoveNext())
											{
												string text11 = enumerator7.Current;
												stringBuilder.Append(text11).Append(' ').Append(The.ZoneManager.GetZoneDisplayName(text11, false, false, true, true))
													.Append('\n');
											}
											goto IL_1A13;
										}
									}
									stringBuilder.Compound("No gate zones found!\n", '\n');
									IL_1A13:
									Popup.Show(stringBuilder.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
									return;
								}
								if (Wish == "sparks")
								{
									who.Sparksplatter();
									return;
								}
								if (Wish == "groundliquid")
								{
									Cell currentCell = The.Player.CurrentCell;
									if (currentCell != null)
									{
										Popup.Show("[" + (currentCell.GroundLiquid ?? "null") + "]", null, "Sounds/UI/ui_notification", true, true, true, true, null);
										return;
									}
								}
								else
								{
									if (Wish == "testmarkup")
									{
										MessageQueue.AddPlayerMessage(Markup.Transform("{{blue|blue blue blue blue blue blue blue blue blue blue blue {{rainbow|rainbow rainbow {{random|random1}} {{random|random2}} rainbow rainbow rainbow}} blue blue}} gray gray", false), null, true);
										return;
									}
									if (Wish == "showcooldownminima")
									{
										StringBuilder stringBuilder2 = Event.NewStringBuilder(null);
										int num8 = 20;
										int num9 = 10;
										while (num8 <= 600)
										{
											int num10 = ActivatedAbilities.MinimumValueForCooldown(num8);
											stringBuilder2.Append(num8).Append(": ").Append(num10)
												.Append(" (")
												.Append((int)Math.Round((double)num10 * 100.0 / (double)num8, MidpointRounding.AwayFromZero))
												.Append("%)\n");
											num8 += num9;
											num9 += 10;
										}
										Popup.Show(stringBuilder2.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
										return;
									}
									if (Wish.StartsWith("zoomnodes"))
									{
										int num7 = The.Game.GetIntGameState("zoomnodes", 0);
										MessageQueue.AddPlayerMessage(num7.ToString(), null, true);
										return;
									}
									if (Wish.StartsWith("find:"))
									{
										string text12 = Wish.Split(':', StringSplitOptions.None)[1];
										StringBuilder stringBuilder3 = Event.NewStringBuilder(null);
										List<GameObject> objects = who.Physics.CurrentCell.ParentZone.GetObjects(text12);
										if (objects.Count == 0)
										{
											stringBuilder3.Append("no ").Append(text12).Append(" found in zone");
										}
										else
										{
											foreach (GameObject gameObject8 in objects)
											{
												stringBuilder3.Append(gameObject8.Physics.CurrentCell.X).Append(' ').Append(gameObject8.Physics.CurrentCell.Y)
													.Append('\n');
											}
										}
										Popup.Show(stringBuilder3.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
										return;
									}
									if (Wish == "testcardinal")
									{
										StringBuilder stringBuilder4 = Event.NewStringBuilder(null);
										for (int num11 = 0; num11 <= 150; num11++)
										{
											stringBuilder4.Append(num11).Append(": ").Append(Grammar.Cardinal(num11))
												.Append('\n');
										}
										Popup.Show(stringBuilder4.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
										return;
									}
									if (Wish == "testordinal")
									{
										StringBuilder stringBuilder5 = Event.NewStringBuilder(null);
										for (int num12 = 0; num12 <= 150; num12++)
										{
											stringBuilder5.Append(num12).Append(": ").Append(Grammar.Ordinal(num12))
												.Append('\n');
										}
										Popup.Show(stringBuilder5.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
										return;
									}
									if (Wish == "testpets")
									{
										bool flag = false;
										foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList.Where<GameObjectBlueprint>((GameObjectBlueprint bp) => bp.HasTag("Creature", false) && !bp.HasTag("BaseObject", false) && !bp.HasTag("ExcludeFromDynamicEncounters", false) && !bp.HasTag("ExcludeFromVillagePopulations", false) && !bp.HasTag("Merchant", false)))
										{
											GameObject gameObject9 = GameObjectFactory.Factory.CreateObject(gameObjectBlueprint.Name);
											if (gameObject9.DisplayName.StartsWith("["))
											{
												Popup.Show(gameObject9.Blueprint + ": " + gameObject9.DisplayName, null, "Sounds/UI/ui_notification", true, true, true, true, null);
												flag = true;
											}
										}
										if (!flag)
										{
											Popup.Show("No problems found.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
											return;
										}
									}
									else if (Wish == "testobjects")
									{
										bool flag2 = false;
										foreach (GameObjectBlueprint gameObjectBlueprint2 in GameObjectFactory.Factory.BlueprintList.Where<GameObjectBlueprint>((GameObjectBlueprint bp) => !bp.HasTag("BaseObject", false) && !bp.HasTag("ExcludeFromDynamicEncounters", false)))
										{
											GameObject gameObject10 = GameObjectFactory.Factory.CreateObject(gameObjectBlueprint2.Name);
											if (gameObject10.DisplayName.StartsWith("["))
											{
												Popup.Show(gameObject10.Blueprint + ": " + gameObject10.DisplayName, null, "Sounds/UI/ui_notification", true, true, true, true, null);
												flag2 = true;
											}
										}
										if (!flag2)
										{
											Popup.Show("No problems found.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
											return;
										}
									}
									else
									{
										if (Wish == "showgenders")
										{
											List<Gender> all = Gender.GetAll();
											StringBuilder stringBuilder6 = Event.NewStringBuilder(null);
											for (int num13 = 0; num13 < all.Count; num13++)
											{
												stringBuilder6.Length = 0;
												stringBuilder6.Append(num13 + 1).Append('/').Append(all.Count)
													.Append("\n\n");
												all[num13].GetSummary(stringBuilder6);
												Popup.Show(stringBuilder6.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
											}
											return;
										}
										if (Wish == "showmygender")
										{
											Popup.Show(who.GetGender(false).GetSummary(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
											return;
										}
										if (Wish == "showpronounsets")
										{
											List<PronounSet> all2 = PronounSet.GetAll();
											StringBuilder stringBuilder7 = Event.NewStringBuilder(null);
											for (int num14 = 0; num14 < all2.Count; num14++)
											{
												stringBuilder7.Length = 0;
												stringBuilder7.Append(num14 + 1).Append('/').Append(all2.Count)
													.Append("\n\n");
												all2[num14].GetSummary(stringBuilder7);
												Popup.Show(stringBuilder7.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
											}
											return;
										}
										if (Wish == "powergrid")
										{
											new PowerGrid().BuildZone(who.Physics.CurrentCell.ParentZone);
											return;
										}
										if (Wish == "powergriddebug")
										{
											new PowerGrid
											{
												ShowPathfinding = true,
												ShowPathWeights = true
											}.BuildZone(who.Physics.CurrentCell.ParentZone);
											return;
										}
										if (Wish == "powergridruin")
										{
											new PowerGrid
											{
												DamageChance = "20-40",
												DamageIsBreakageChance = "30-80",
												MissingConsumers = "2-15",
												MissingProducers = "2-5",
												Noise = ((Stat.Random(0, 1) == 0) ? Stat.Random(0, 10) : Stat.Random(0, 80))
											}.BuildZone(who.Physics.CurrentCell.ParentZone);
											return;
										}
										if (Wish == "hydraulics")
										{
											new Hydraulics().BuildZone(who.Physics.CurrentCell.ParentZone);
											return;
										}
										if (Wish == "1hp")
										{
											Statistic stat = The.Player.GetStat("Hitpoints");
											stat.Penalty += stat.Value - 1;
											return;
										}
										if (Wish == "ambientstabilization")
										{
											AmbientStabilization ambientStabilization = The.Player.CurrentZone.RequirePart<AmbientStabilization>(false);
											Popup.Show("Have ambient stabilization at strength " + ambientStabilization.Strength.ToString() + ((ambientStabilization.ParentZone == The.Player.CurrentZone) ? " with coherent zone" : " with zone mismatch") + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
											return;
										}
										if (Wish == "showworshippables")
										{
											string text13 = "";
											foreach (Worshippable worshippable in Factions.GetWorshippables())
											{
												text13 = text13 + worshippable.ToString() + "\n\n";
											}
											Popup.Show(text13, null, "Sounds/UI/ui_notification", true, true, true, true, null);
											return;
										}
										if (Wish == "showworship")
										{
											string text14 = "";
											foreach (WorshipTracking worshipTracking in The.Game.PlayerReputation.GetWorshipTracking())
											{
												text14 = text14 + worshipTracking.ToString() + "\n\n";
											}
											Popup.Show(text14, null, "Sounds/UI/ui_notification", true, true, true, true, null);
											return;
										}
										if (Wish == "showblasphemy")
										{
											string text15 = "";
											foreach (WorshipTracking worshipTracking2 in The.Game.PlayerReputation.GetBlasphemyTracking())
											{
												text15 = text15 + worshipTracking2.ToString() + "\n\n";
											}
											Popup.Show(text15, null, "Sounds/UI/ui_notification", true, true, true, true, null);
											return;
										}
										if (Wish == "zonenamedata")
										{
											Zone currentZone3 = The.Player.CurrentZone;
											Popup.Show(string.Concat(new string[]
											{
												"DisplayName: ",
												currentZone3.DisplayName,
												"\nBaseDisplayName: ",
												currentZone3.BaseDisplayName,
												"\nReferenceDisplayName: ",
												currentZone3.ReferenceDisplayName,
												"\nNameContext: ",
												currentZone3.NameContext,
												"\nHasProperName: ",
												currentZone3.HasProperName.ToString(),
												"\nDefiniteArticle: ",
												currentZone3.DefiniteArticle,
												"\nIndefiniteArticle: ",
												currentZone3.IndefiniteArticle,
												"\nIncludeContextInZoneDisplay: ",
												currentZone3.IncludeContextInZoneDisplay.ToString(),
												"\nIncludeStratumInZoneDisplay: ",
												currentZone3.IncludeStratumInZoneDisplay.ToString(),
												"\nNamedByPlayer: ",
												currentZone3.NamedByPlayer.ToString(),
												"\n"
											}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
											return;
										}
										if (Wish == "zoneparts")
										{
											Zone currentZone4 = The.Player.CurrentZone;
											if (currentZone4.Parts == null)
											{
												Popup.Show("Zone has null parts list.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
												return;
											}
											if (currentZone4.Parts.Count == 0)
											{
												Popup.Show("Zone has no parts.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
												return;
											}
											string text16 = "";
											int num15 = 0;
											int count = currentZone4.Parts.Count;
											while (num15 < count)
											{
												text16 = text16 + "\n" + currentZone4.Parts[num15].Name;
												num15++;
											}
											Popup.Show(text16, null, "Sounds/UI/ui_notification", true, true, true, true, null);
											return;
										}
										else
										{
											if (Wish == "hydraulicsmetal")
											{
												new Hydraulics
												{
													ConduitBlueprint = "MetalHydraulicPipe"
												}.BuildZone(who.Physics.CurrentCell.ParentZone);
												return;
											}
											if (Wish == "hydraulicsplastic")
											{
												new Hydraulics
												{
													ConduitBlueprint = "PlasticHydraulicPipe"
												}.BuildZone(who.Physics.CurrentCell.ParentZone);
												return;
											}
											if (Wish == "hydraulicsglass")
											{
												new Hydraulics
												{
													ConduitBlueprint = "GlassHydraulicPipe"
												}.BuildZone(who.Physics.CurrentCell.ParentZone);
												return;
											}
											if (Wish == "hydraulicsruin")
											{
												new Hydraulics
												{
													DamageChance = "10-20",
													DamageIsBreakageChance = "30-80",
													MissingConsumers = "1-8",
													MissingProducers = "1-3",
													Noise = ((Stat.Random(0, 1) == 0) ? Stat.Random(0, 10) : Stat.Random(0, 80))
												}.BuildZone(who.Physics.CurrentCell.ParentZone);
												return;
											}
											if (Wish == "mechpower")
											{
												new MechanicalPower().BuildZone(who.Physics.CurrentCell.ParentZone);
												return;
											}
											if (Wish == "showrandomfaction")
											{
												Popup.Show(Factions.GetRandomFaction().Name, null, "Sounds/UI/ui_notification", true, true, true, true, null);
												return;
											}
											if (Wish == "showrandomoldfaction")
											{
												Popup.Show(Factions.GetRandomOldFaction().Name, null, "Sounds/UI/ui_notification", true, true, true, true, null);
												return;
											}
											if (Wish == "showrandomfactionexceptbeasts")
											{
												Popup.Show(Factions.GetRandomFaction("Beasts").Name, null, "Sounds/UI/ui_notification", true, true, true, true, null);
												return;
											}
											if (Wish == "showrandomfactionshortname")
											{
												Popup.Show(Factions.GetRandomFaction((Faction f) => f.Name.Length <= 8).Name, null, "Sounds/UI/ui_notification", true, true, true, true, null);
												return;
											}
											if (Wish == "teststringbuilder")
											{
												object obj3 = new Action<StringBuilder, string>(delegate(StringBuilder sb, string s)
												{
													if (!sb.Contains(s))
													{
														Popup.Show(string.Concat(new string[]
														{
															"'",
															sb.ToString(),
															"' should contain '",
															s,
															"' but doesn't"
														}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
													}
												});
												Action<StringBuilder, string> action = delegate(StringBuilder sb, string s)
												{
													if (sb.Contains(s))
													{
														Popup.Show(string.Concat(new string[]
														{
															"'",
															sb.ToString(),
															"' shouldn't contain '",
															s,
															"' but does"
														}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
													}
												};
												StringBuilder stringBuilder8 = new StringBuilder("abcde");
												object obj2 = obj3;
												obj2(stringBuilder8, "a");
												obj2(stringBuilder8, "b");
												obj2(stringBuilder8, "c");
												obj2(stringBuilder8, "d");
												obj2(stringBuilder8, "e");
												action(stringBuilder8, "f");
												obj2(stringBuilder8, "ab");
												obj2(stringBuilder8, "bc");
												obj2(stringBuilder8, "cd");
												obj2(stringBuilder8, "de");
												action(stringBuilder8, "aa");
												action(stringBuilder8, "ac");
												action(stringBuilder8, "ad");
												action(stringBuilder8, "ae");
												action(stringBuilder8, "af");
												action(stringBuilder8, "ea");
												action(stringBuilder8, "ba");
												action(stringBuilder8, "bb");
												action(stringBuilder8, "bd");
												action(stringBuilder8, "be");
												action(stringBuilder8, "bf");
												action(stringBuilder8, "ca");
												action(stringBuilder8, "cb");
												action(stringBuilder8, "cc");
												action(stringBuilder8, "ce");
												action(stringBuilder8, "cf");
												action(stringBuilder8, "da");
												action(stringBuilder8, "db");
												action(stringBuilder8, "dc");
												action(stringBuilder8, "dd");
												action(stringBuilder8, "df");
												action(stringBuilder8, "eb");
												action(stringBuilder8, "ec");
												action(stringBuilder8, "ed");
												action(stringBuilder8, "ee");
												action(stringBuilder8, "ef");
												obj2(stringBuilder8, "abc");
												obj2(stringBuilder8, "bcd");
												obj2(stringBuilder8, "cde");
												action(stringBuilder8, "def");
												obj2(stringBuilder8, "abcd");
												obj2(stringBuilder8, "bcde");
												action(stringBuilder8, "cdef");
												obj2(stringBuilder8, "abcde");
												action(stringBuilder8, "abcdef");
												StringBuilder stringBuilder9 = new StringBuilder("abcdee");
												obj2(stringBuilder9, "ab");
												obj2(stringBuilder9, "bc");
												obj2(stringBuilder9, "cd");
												obj2(stringBuilder9, "de");
												obj2(stringBuilder9, "ee");
												action(stringBuilder9, "ef");
												obj2(stringBuilder9, "abc");
												obj2(stringBuilder9, "bcd");
												obj2(stringBuilder9, "dee");
												action(stringBuilder9, "deee");
												action(stringBuilder9, "def");
												obj2(stringBuilder9, "abcd");
												obj2(stringBuilder9, "bcde");
												obj2(stringBuilder9, "cdee");
												action(stringBuilder9, "cdef");
												obj2(stringBuilder9, "abcde");
												obj2(stringBuilder9, "bcdee");
												action(stringBuilder9, "bcdef");
												action(stringBuilder9, "bcdeee");
												action(stringBuilder9, "cdeee");
												Popup.Show("Done.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
												return;
											}
											if (Wish == "testzoneparse")
											{
												string zoneID = who.CurrentZone.ZoneID;
												string text17;
												int num16;
												int num17;
												int num18;
												int num19;
												int num20;
												bool flag3 = ZoneID.Parse(zoneID, out text17, out num16, out num17, out num18, out num19, out num20);
												string text18 = ZoneID.Assemble(text17, num16, num17, num18, num19, num20);
												StringBuilder stringBuilder10 = Event.NewStringBuilder(null);
												stringBuilder10.Append("ZoneID: ").Append(zoneID).Append('\n')
													.Append("Parse result: ")
													.Append(flag3)
													.Append('\n')
													.Append("Components: ")
													.Append(text17)
													.Append(' ')
													.Append(num16)
													.Append(' ')
													.Append(num17)
													.Append(' ')
													.Append(num18)
													.Append(' ')
													.Append(num19)
													.Append(' ')
													.Append(num20)
													.Append('\n')
													.Append("Match on reassemble: ")
													.Append(text18 == zoneID);
												Popup.Show(stringBuilder10.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
												return;
											}
											if (Wish == "topevents")
											{
												Event.ShowTopEvents();
												return;
											}
											if (Wish == "achnotify")
											{
												using (Dictionary<string, AchievementInfo>.ValueCollection.Enumerator enumerator11 = AchievementManager.State.Achievements.Values.GetEnumerator())
												{
													while (enumerator11.MoveNext())
													{
														AchievementInfo achievementInfo = enumerator11.Current;
														if (achievementInfo.Progress != null)
														{
															achievementInfo.NotifyProgress();
														}
														achievementInfo.NotifyUnlock();
													}
													return;
												}
											}
											if (Wish == "statnotify")
											{
												using (Dictionary<string, AchievementInfo>.ValueCollection.Enumerator enumerator11 = AchievementManager.State.Achievements.Values.GetEnumerator())
												{
													while (enumerator11.MoveNext())
													{
														AchievementInfo achievementInfo2 = enumerator11.Current;
														if (achievementInfo2.Progress != null)
														{
															achievementInfo2.NotifyProgress();
														}
													}
													return;
												}
											}
											if (Wish == "testrig")
											{
												Mutations part = who.GetPart<Mutations>();
												part.AddMutation((BaseMutation)Activator.CreateInstance(typeof(Clairvoyance)), 12, true);
												part.AddMutation((BaseMutation)Activator.CreateInstance(typeof(Teleportation)), 12, true);
												Wishing.AddSkill("Survival");
												Wishing.AddSkill("Survival_Trailblazer");
												return;
											}
											if (Wish == "testhero")
											{
												GameObject gameObject11 = GameObjectFactory.Factory.CreateObject("Scrapbot");
												HeroMaker.MakeHero(gameObject11, null, null, -1, "Hero");
												who.Physics.CurrentCell.GetCellFromDirection("E", true).AddObject(gameObject11, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
												return;
											}
											if (Wish == "reload")
											{
												MinEvent.SuppressThreadWarning = true;
												GameManager.Instance.uiQueue.awaitTask(delegate
												{
													The.Core.HotloadConfiguration(false);
													GameManager.Instance.SetActiveLayersForNavCategory("Adventure", true);
												});
												MinEvent.SuppressThreadWarning = false;
												MessageQueue.AddPlayerMessage("Hotload complete.", null, true);
												return;
											}
											if (Wish == "xy")
											{
												Popup.Show(who.CurrentCell.X.ToString() + ", " + who.CurrentCell.Y.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
												return;
											}
											if (Wish == "rebuildfull")
											{
												The.Game.SetIntGameState("WorldSeed", Stat.RandomCosmetic(1, 2147483646));
												The.ZoneManager.RebuildActiveZone(Wish == "flushandrebuild");
												return;
											}
											if (Wish == "rebuild" || Wish == "flushandrebuild")
											{
												The.ZoneManager.RebuildActiveZone(Wish == "flushandrebuild");
												return;
											}
											if (Wish == "popuptest")
											{
												Popup.ShowYesNo("Test1", "Sounds/UI/ui_notification", true, DialogResult.Yes, null);
												Popup.ShowYesNo("Test2", "Sounds/UI/ui_notification", true, DialogResult.Yes, null);
												Popup.ShowYesNo("Test3", "Sounds/UI/ui_notification", true, DialogResult.Yes, null);
												return;
											}
											if (Wish == "nanoterm")
											{
												Cell currentCell2 = who.Physics.CurrentCell;
												currentCell2.GetCellFromDirection("E", true).AddObject("Nanowall1W", null, null, null, null);
												currentCell2.GetCellFromDirection("E", true).GetCellFromDirection("E", true).GetCellFromDirection("E", true)
													.AddObject("Nanowall1E", null, null, null, null);
												currentCell2.GetCellFromDirection("S", true).GetCellFromDirection("E", true).AddObject("Nanowall2W", null, null, null, null);
												currentCell2.GetCellFromDirection("S", true).GetCellFromDirection("E", true).GetCellFromDirection("E", true)
													.AddObject("CyberneticsFabTerminal", null, null, null, null);
												currentCell2.GetCellFromDirection("S", true).GetCellFromDirection("E", true).GetCellFromDirection("E", true)
													.GetCellFromDirection("E", true)
													.AddObject("Nanowall2E", null, null, null, null);
												currentCell2.GetCellFromDirection("S", true).GetCellFromDirection("S", true).GetCellFromDirection("E", true)
													.AddObject("Nanowall3W", null, null, null, null);
												currentCell2.GetCellFromDirection("S", true).GetCellFromDirection("S", true).GetCellFromDirection("E", true)
													.GetCellFromDirection("E", true)
													.AddObject("ArmNook", null, null, null, null);
												currentCell2.GetCellFromDirection("S", true).GetCellFromDirection("S", true).GetCellFromDirection("E", true)
													.GetCellFromDirection("E", true)
													.GetCellFromDirection("E", true)
													.AddObject("Nanowall3E", null, null, null, null);
												return;
											}
											if (Wish == "thirsty")
											{
												who.GetPart<Stomach>().Water = 0;
												return;
											}
											if (Wish == "hungry")
											{
												Stomach part2 = who.GetPart<Stomach>();
												part2.CookingCounter = part2.CalculateCookingIncrement();
												return;
											}
											if (Wish == "famished")
											{
												Stomach part3 = who.GetPart<Stomach>();
												part3.CookingCounter = part3.CalculateCookingIncrement() * 2;
												return;
											}
											if (Wish == "what")
											{
												Cell currentCell3 = who.Physics.CurrentCell;
												using (Container<GameObject>.Enumerator enumerator12 = who.Physics.CurrentCell.Objects.GetEnumerator())
												{
													while (enumerator12.MoveNext())
													{
														GameObject gameObject12 = enumerator12.Current;
														MessageQueue.AddPlayerMessage(gameObject12.Blueprint, null, true);
													}
													return;
												}
											}
											if (Wish == "where")
											{
												Cell currentCell4 = who.Physics.CurrentCell;
												MessageQueue.AddPlayerMessage(string.Concat(new string[]
												{
													currentCell4.X.ToString(),
													",",
													currentCell4.Y.ToString(),
													" in ",
													currentCell4.ParentZone.ZoneID
												}), null, true);
												return;
											}
											if (Wish == "bordertest")
											{
												for (int num21 = 0; num21 < 10000; num21++)
												{
													Popup._ScreenBuffer.ThickSingleBox(Stat.RandomCosmetic(-1000, 1000), Stat.RandomCosmetic(-1000, 1000), Stat.RandomCosmetic(-1000, 1000), Stat.RandomCosmetic(-1000, 1000), ConsoleLib.Console.ColorUtility.MakeColor(ConsoleLib.Console.ColorUtility.Bright(TextColor.Black), TextColor.Black));
												}
												return;
											}
											if (Wish == "curefungus")
											{
												using (List<BodyPart>.Enumerator enumerator13 = who.Body.GetParts().GetEnumerator())
												{
													while (enumerator13.MoveNext())
													{
														BodyPart bodyPart = enumerator13.Current;
														if (bodyPart.Equipped != null && bodyPart.Equipped.HasTag("FungalInfection"))
														{
															bodyPart.Equipped.Destroy(null, false, false, null);
														}
													}
													return;
												}
											}
											if (Wish == "cureironshank")
											{
												IronshankOnset ironshankOnset;
												if (who.TryGetEffect<IronshankOnset>(out ironshankOnset))
												{
													ironshankOnset.Duration = 0;
												}
												Ironshank ironshank;
												if (who.TryGetEffect<Ironshank>(out ironshank))
												{
													ironshank.Duration = 0;
													return;
												}
											}
											else if (Wish == "cureglotrot")
											{
												GlotrotOnset glotrotOnset;
												if (who.TryGetEffect<GlotrotOnset>(out glotrotOnset))
												{
													glotrotOnset.Duration = 0;
												}
												Glotrot glotrot;
												if (who.TryGetEffect<Glotrot>(out glotrot))
												{
													glotrot.Duration = 0;
													return;
												}
											}
											else
											{
												if (Wish == "glotrotonset")
												{
													who.ApplyEffect(new GlotrotOnset(), null);
													return;
												}
												if (Wish == "glotrot")
												{
													who.ApplyEffect(new Glotrot(), null);
													return;
												}
												if (Wish == "glotrotfinal")
												{
													who.ApplyEffect(new Glotrot(), null);
													who.GetEffect<Glotrot>().Stage = 3;
													return;
												}
												if (Wish == "ironshankonset")
												{
													who.ApplyEffect(new IronshankOnset(), null);
													return;
												}
												if (Wish == "ironshank")
												{
													who.ApplyEffect(new Ironshank(), null);
													return;
												}
												if (Wish == "monochromeonset")
												{
													who.ApplyEffect(new MonochromeOnset(), null);
													return;
												}
												if (Wish == "monochrome")
												{
													who.ApplyEffect(new Monochrome(), null);
													return;
												}
												if (Wish == "monochrome")
												{
													who.ApplyEffect(new Monochrome(), null);
													return;
												}
												if (Wish == "glotrotonset")
												{
													who.ApplyEffect(new GlotrotOnset(), null);
													return;
												}
												if (Wish == "resetgreyscale")
												{
													GameManager.Instance.GreyscaleLevel = 0;
													return;
												}
												if (Wish == "mazetest")
												{
													Keys keys = Popup.ShowBlock("1) random maze\n2) recursive backtrack maze", null, "Sounds/UI/ui_notification", true, true, true, true, null);
													if (keys == Keys.D1)
													{
														RandomMaze.Generate(80, 25, Stat.Random(0, 2147483646)).Test(true);
													}
													if (keys == Keys.D2)
													{
														RecursiveBacktrackerMaze.Generate(80, 25, true, Stat.Random(0, 2147483646)).Test(true);
														return;
													}
												}
												else
												{
													if (Wish == "tunneltest")
													{
														do
														{
															int num22 = 5;
															int num23 = 3;
															int num7 = Stat.Random(0, 2);
															string text19 = num7.ToString();
															num7 = Stat.Random(0, 2);
															TunnelMaker tunnelMaker = new TunnelMaker(num22, num23, text19, num7.ToString(), "NES");
															tunnelMaker.CreateTunnel();
															ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(false);
															for (int num24 = 0; num24 < tunnelMaker.Height; num24++)
															{
																for (int num25 = 0; num25 < tunnelMaker.Width; num25++)
																{
																	scrapBuffer.Goto(num25, num24);
																	if (tunnelMaker.Map[num25, num24] == "")
																	{
																		scrapBuffer.Write(".", true, false, false, null, -1);
																	}
																	if (tunnelMaker.Map[num25, num24].Contains("N") && tunnelMaker.Map[num25, num24].Contains("S"))
																	{
																		scrapBuffer.Write(186);
																	}
																	if (tunnelMaker.Map[num25, num24].Contains("E") && tunnelMaker.Map[num25, num24].Contains("W"))
																	{
																		scrapBuffer.Write(205);
																	}
																	if (tunnelMaker.Map[num25, num24].Contains("E") && tunnelMaker.Map[num25, num24].Contains("N"))
																	{
																		scrapBuffer.Write(200);
																	}
																	if (tunnelMaker.Map[num25, num24].Contains("W") && tunnelMaker.Map[num25, num24].Contains("N"))
																	{
																		scrapBuffer.Write(188);
																	}
																	if (tunnelMaker.Map[num25, num24].Contains("E") && tunnelMaker.Map[num25, num24].Contains("S"))
																	{
																		scrapBuffer.Write(201);
																	}
																	if (tunnelMaker.Map[num25, num24].Contains("W") && tunnelMaker.Map[num25, num24].Contains("S"))
																	{
																		scrapBuffer.Write(187);
																	}
																	if (tunnelMaker.Map[num25, num24] == "N")
																	{
																		scrapBuffer.Write(208);
																	}
																	if (tunnelMaker.Map[num25, num24] == "S")
																	{
																		scrapBuffer.Write(210);
																	}
																	if (tunnelMaker.Map[num25, num24] == "E")
																	{
																		scrapBuffer.Write(198);
																	}
																	if (tunnelMaker.Map[num25, num24] == "W")
																	{
																		scrapBuffer.Write(181);
																	}
																}
															}
															Popup._TextConsole.DrawBuffer(scrapBuffer, null, false);
														}
														while (Keyboard.getch() != 120);
														return;
													}
													if (Wish.StartsWith("rebuildbody:"))
													{
														string text20 = Wish.Split(':', StringSplitOptions.None)[1];
														if (!who.Body.Rebuild(text20))
														{
															Popup.Show("Failed to rebuild body as " + text20, null, "Sounds/UI/ui_notification", true, true, true, true, null);
															return;
														}
													}
													else
													{
														if (Wish == "bodyparttypes")
														{
															StringBuilder stringBuilder11 = Event.NewStringBuilder(null);
															who.Body.TypeDump(stringBuilder11);
															Popup.Show(stringBuilder11.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
															return;
														}
														if (Wish.StartsWith("xpmul:"))
														{
															The.Core.XPMul = (float)Convert.ToDouble(Wish.Split(':', StringSplitOptions.None)[1]);
															return;
														}
														if (Wish.StartsWith("xp:"))
														{
															Popup.Suppress = true;
															who.AwardXP(Convert.ToInt32(Wish.Split(':', StringSplitOptions.None)[1]), -1, 0, int.MaxValue, null, null, null, null, null, null);
															Popup.Suppress = false;
															return;
														}
														if (Wish.StartsWith("xpverbose:"))
														{
															who.AwardXP(Convert.ToInt32(Wish.Split(':', StringSplitOptions.None)[1]), -1, 0, int.MaxValue, null, null, null, null, null, null);
															return;
														}
														if (Wish == "cleaneffects")
														{
															if (who.Effects == null)
															{
																return;
															}
															using (Container<Effect>.Enumerator enumerator14 = who.Effects.GetEnumerator())
															{
																while (enumerator14.MoveNext())
																{
																	Effect effect = enumerator14.Current;
																	effect.Duration = 0;
																}
																return;
															}
														}
														if (Wish == "clean")
														{
															if (who.Effects != null)
															{
																foreach (Effect effect2 in who.Effects)
																{
																	effect2.Duration = 0;
																}
															}
															who.Statistics["Strength"].Penalty = 0;
															who.Statistics["Agility"].Penalty = 0;
															who.Statistics["Intelligence"].Penalty = 0;
															who.Statistics["Toughness"].Penalty = 0;
															who.Statistics["Willpower"].Penalty = 0;
															who.Statistics["Ego"].Penalty = 0;
															who.Statistics["Speed"].Penalty = 0;
															return;
														}
														if (Wish == "websplat")
														{
															for (int num26 = 0; num26 < 8; num26++)
															{
																Cryobarrio1.Websplat(Stat.Random(0, 79), Stat.Random(0, 24), who.Physics.CurrentCell.ParentZone, "PhaseWeb");
															}
															return;
														}
														if (Wish == "sultantomb1")
														{
															The.ZoneManager.SetActiveZone("JoppaWorld.53.3.0.2.6");
															who.SystemMoveTo(The.ZoneManager.ActiveZone.GetCell(39, 14), null, false, true, false, false);
															The.ZoneManager.ProcessGoToPartyLeader();
															return;
														}
														if (Wish == "sultantomb6")
														{
															The.ZoneManager.SetActiveZone("JoppaWorld.53.3.1.0.1");
															who.SystemMoveTo(The.ZoneManager.ActiveZone.GetCell(39, 14), null, false, true, false, false);
															The.ZoneManager.ProcessGoToPartyLeader();
															return;
														}
														if (Wish == "stage2" || Wish == "stage3a" || Wish == "stage3" || Wish == "stage4" || Wish == "stage5" || Wish == "stage6" || Wish == "stage8" || Wish.StartsWith("stage9") || Wish == "stage10" || Wish == "stage11" || Wish == "tombbeta" || Wish == "fastforwardtomb" || Wish == "tombbetastart" || Wish == "tombbetaend" || Wish == "tombbetainside" || Wish == "reefjump" || Wish == "pregolem" || Wish == "golemstart" || Wish == "posttomb" || Wish == "startmoonstair" || Wish == "postgolem")
														{
															Popup.Suppress = true;
															ItemNaming.Suppress = true;
															who.AwardXP(15000, -1, 0, int.MaxValue, null, null, null, null, null, null);
															game.CompleteQuest("Fetch Argyve a Knickknack");
															game.CompleteQuest("Fetch Argyve Another Knickknack");
															game.CompleteQuest("Weirdwire Conduit... Eureka!");
															game.StartQuest("A Canticle for Barathrum", null, null, null);
															XRL.World.Parts.Physics physics = who.Physics;
															if (Wish == "stage2")
															{
																The.ZoneManager.SetActiveZone("JoppaWorld.22.14.1.1.10");
																who.SystemMoveTo(The.ZoneManager.ActiveZone.GetCell(0, 0), null, false, true, false, false);
																The.ZoneManager.ProcessGoToPartyLeader();
																who.ReceiveObjectsFromPopulation("Junk 1", Stat.Random(1, 4), null, false, 0, 0, null, null, null, null, null);
																who.ReceiveObjectsFromPopulation("Meds 1", Stat.Random(1, 4), null, false, 0, 0, null, null, null, null, null);
															}
															if (Wish == "stage3")
															{
																who.ReceiveObject("Steel Plate Mail", false, 0, 0, null, null, null, null);
																who.ReceiveObject("Steel Buckler", false, 0, 0, null, null, null, null);
																who.ReceiveObject("Steel Boots", false, 0, 0, null, null, null, null);
																who.ReceiveObject("Steel Gauntlets", false, 0, 0, null, null, null, null);
																if (who.HasSkill("Cudgel"))
																{
																	who.ReceiveObject("Cudgel3", false, 0, 0, null, null, null, null);
																}
																if (who.HasSkill("ShortBlades"))
																{
																	who.ReceiveObject("Dagger3", false, 0, 0, null, null, null, null);
																}
																if (who.HasSkill("LongBlades"))
																{
																	who.ReceiveObject("Long Sword3", false, 0, 0, null, null, null, null);
																}
																if (who.HasSkill("Axe"))
																{
																	who.ReceiveObject("Battle Axe3", false, 0, 0, null, null, null, null);
																}
																who.ReceiveObjectsFromPopulation("Junk 1", Stat.Random(1, 4), null, false, 0, 0, null, null, null, null, null);
																who.ReceiveObjectsFromPopulation("Junk 2", Stat.Random(1, 3), null, false, 0, 0, null, null, null, null, null);
																who.ReceiveObjectsFromPopulation("Meds 1", Stat.Random(1, 3), null, false, 0, 0, null, null, null, null, null);
																who.ReceiveObjectsFromPopulation("Meds 2", Stat.Random(1, 2), null, false, 0, 0, null, null, null, null, null);
															}
															if (Wish == "stage4")
															{
																who.ReceiveObject("Carbide Plate Armor", false, 0, 0, null, null, null, null);
																who.ReceiveObject("Steel Buckler", false, 0, 0, null, null, null, null);
																who.ReceiveObject("Chain Coif", false, 0, 0, null, null, null, null);
																who.ReceiveObject("Steel Boots", false, 0, 0, null, null, null, null);
																who.ReceiveObject("Steel Gauntlets", false, 0, 0, null, null, null, null);
																who.ReceiveObject("MasterworkCarbine", false, 0, 0, null, null, null, null);
																who.ReceiveObject("UbernostrumTonic", false, 0, 0, null, null, null, null);
																who.ReceiveObject("Sowers_Seed", 12, false, 0, 0, null, null, null, null);
																who.ReceiveObject("Fixit Spray", false, 0, 0, null, null, null, null);
																who.ReceiveObject("SalveTonic", 6, false, 0, 0, null, null, null, null);
																who.ReceiveObject("Floating Glowsphere", false, 0, 0, null, null, null, null);
																who.ReceiveObject("Ironweave Cloak", false, 0, 0, null, null, null, null);
																if (who.HasSkill("Cudgel"))
																{
																	who.ReceiveObject("Cudgel4", false, 0, 0, null, null, null, null);
																}
																if (who.HasSkill("ShortBlades"))
																{
																	who.ReceiveObject("Dagger4", false, 0, 0, null, null, null, null);
																}
																if (who.HasSkill("LongBlades"))
																{
																	who.ReceiveObject("Long Sword4", false, 0, 0, null, null, null, null);
																}
																if (who.HasSkill("Axe"))
																{
																	who.ReceiveObject("Battle Axe4", false, 0, 0, null, null, null, null);
																}
																who.ReceiveObjectsFromPopulation("Junk 3", 2, null, false, 0, 0, null, null, null, null, null);
																who.ReceiveObjectsFromPopulation("Junk 4", 2, null, false, 0, 0, null, null, null, null, null);
																who.ReceiveObjectsFromPopulation("Meds 3", 3, null, false, 0, 0, null, null, null, null, null);
																who.ReceiveObjectsFromPopulation("Meds 4", 2, null, false, 0, 0, null, null, null, null, null);
																who.ReceiveObjectsFromPopulation("Artifact 3", 3, null, false, 0, 0, null, null, null, null, null);
																who.ReceiveObjectsFromPopulation("Artifact 4", 2, null, false, 0, 0, null, null, null, null, null);
															}
															who.ReceiveObject("Droid Scrambler", false, 0, 0, null, null, null, null);
															who.ReceiveObject("Joppa Recoiler", false, 0, 0, null, null, null, null);
															who.ReceiveObject("Borderlands Revolver", false, 0, 0, null, null, null, null);
															who.ReceiveObject("Lead Slug", 500, false, 0, 0, null, null, null, null);
															if (Wish == "stage3a")
															{
																who.AwardXP(15000, -1, 0, int.MaxValue, null, null, null, null, null, null);
																game.CompleteQuest("A Canticle for Barathrum");
																game.StartQuest("Decoding the Signal", null, null, null);
																game.FinishedQuestStep("Decoding the Signal~Decode the Signal");
																The.ZoneManager.SetActiveZone("JoppaWorld.22.14.1.0.13");
																The.ZoneManager.ActiveZone.GetCell(33, 16).AddObject(physics.ParentObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
																The.ZoneManager.ProcessGoToPartyLeader();
																GritGateScripts.OpenRank1Doors();
															}
															if (Wish == "stage3")
															{
																who.AwardXP(15000, -1, 0, int.MaxValue, null, null, null, null, null, null);
																game.CompleteQuest("A Canticle for Barathrum");
																game.CompleteQuest("Decoding the Signal");
																game.CompleteQuest("More Than a Willing Spirit");
																The.ZoneManager.SetActiveZone("JoppaWorld.22.14.1.0.13");
																The.ZoneManager.ActiveZone.GetCell(33, 16).AddObject(physics.ParentObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
																The.ZoneManager.ProcessGoToPartyLeader();
																GritGateScripts.OpenRank1Doors();
															}
															if (Wish == "stage4")
															{
																who.AwardXP(75000, -1, 0, int.MaxValue, null, null, null, null, null, null);
																The.ZoneManager.SetActiveZone("JoppaWorld");
																The.ZoneManager.ActiveZone.GetCell(25, 3).AddObject(physics.ParentObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
																The.ZoneManager.ProcessGoToPartyLeader();
																GritGateScripts.OpenRank0Doors();
																GritGateScripts.OpenRank1Doors();
																GritGateScripts.OpenRank2Doors();
															}
															if (Wish == "stage5")
															{
																who.AwardXP(210000, -1, 0, int.MaxValue, null, null, null, null, null, null);
																game.CompleteQuest("A Canticle for Barathrum");
																game.CompleteQuest("More Than a Willing Spirit");
																game.CompleteQuest("Decoding the Signal");
																game.StartQuest("The Earl of Omonporch", null, null, null);
																game.FinishQuestStep("The Earl of Omonporch", "Travel to Omonporch", -1, true, null);
																game.FinishQuestStep("The Earl of Omonporch", "Secure the Spindle", -1, true, null);
																The.ZoneManager.SetActiveZone("JoppaWorld.22.14.1.0.13");
																The.ZoneManager.ActiveZone.GetCell(31, 21).AddObject(physics.ParentObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
																The.ZoneManager.ProcessGoToPartyLeader();
																GritGateScripts.PromoteToJourneyfriend();
																GritGateScripts.OpenRank0Doors();
																GritGateScripts.OpenRank1Doors();
																GritGateScripts.OpenRank2Doors();
															}
															if (Wish == "stage6")
															{
																who.AwardXP(210000, -1, 0, int.MaxValue, null, null, null, null, null, null);
																game.CompleteQuest("A Canticle for Barathrum");
																game.CompleteQuest("Decoding the Signal");
																game.CompleteQuest("More Than a Willing Spirit");
																game.CompleteQuest("The Earl of Omonporch");
																game.SetIntGameState("ForcePostEarlSpawn", 1);
																game.CompleteQuest("A Call to Arms");
																game.CompleteQuest("The Assessment");
																game.StartQuest("Pax Klanq, I Presume?", null, null, null);
																The.Player.ReceiveObject("BarathrumKey", false, 0, 0, null, null, null, null);
																GritGateScripts.PromoteToJourneyfriend();
																GritGateScripts.OpenRank1Doors();
																The.ZoneManager.SetActiveZone("JoppaWorld");
																The.ZoneManager.ActiveZone.GetCell(48, 19).AddObject(physics.ParentObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
																The.ZoneManager.ProcessGoToPartyLeader();
																GritGateScripts.OpenRank0Doors();
																GritGateScripts.OpenRank1Doors();
																GritGateScripts.OpenRank2Doors();
															}
															if (Wish == "stage8" || Wish == "tombbetastart")
															{
																The.Player.ReceivePopulation("TombSupply");
																who.AwardXP(240000, -1, 0, int.MaxValue, null, null, null, null, null, null);
																game.CompleteQuest("A Canticle for Barathrum");
																game.CompleteQuest("Decoding the Signal");
																game.CompleteQuest("More Than a Willing Spirit");
																game.CompleteQuest("The Earl of Omonporch");
																game.SetIntGameState("ForcePostEarlSpawn", 1);
																game.CompleteQuest("A Call to Arms");
																game.CompleteQuest("The Assessment");
																game.CompleteQuest("Pax Klanq, I Presume?");
																The.Player.ReceiveObject("BarathrumKey", false, 0, 0, null, null, null, null);
																GritGateScripts.PromoteToJourneyfriend();
																GritGateScripts.OpenRank0Doors();
																GritGateScripts.OpenRank1Doors();
																GritGateScripts.OpenRank2Doors();
																The.ZoneManager.SetActiveZone("JoppaWorld");
																The.ZoneManager.SetActiveZone("JoppaWorld.22.14.1.0.14");
																The.ZoneManager.ActiveZone.GetCell(18, 8).AddObject(physics.ParentObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
																The.ZoneManager.ProcessGoToPartyLeader();
															}
															if (Wish == "stage9")
															{
																The.Player.ReceivePopulation("TombSupply");
																who.AwardXP(240000, -1, 0, int.MaxValue, null, null, null, null, null, null);
																game.CompleteQuest("A Canticle for Barathrum");
																game.CompleteQuest("Decoding the Signal");
																game.CompleteQuest("More Than a Willing Spirit");
																game.CompleteQuest("The Earl of Omonporch");
																game.SetIntGameState("ForcePostEarlSpawn", 1);
																game.CompleteQuest("A Call to Arms");
																game.CompleteQuest("Pax Klanq, I Presume?");
																game.StartQuest("Tomb of the Eaters", null, null, null);
																The.Player.ReceiveObject("BarathrumKey", false, 0, 0, null, null, null, null);
																GritGateScripts.PromoteToJourneyfriend();
																GritGateScripts.OpenRank0Doors();
																GritGateScripts.OpenRank1Doors();
																GritGateScripts.OpenRank2Doors();
																The.ZoneManager.SetActiveZone("JoppaWorld");
																The.ZoneManager.ActiveZone.GetCell(53, 4).AddObject(physics.ParentObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
																The.ZoneManager.ProcessGoToPartyLeader();
															}
															if (Wish == "stage9b" || Wish == "stage9c" || Wish == "stage9d" || Wish == "stage9e" || Wish == "stage9f" || Wish == "stage9u" || Wish == "stage10")
															{
																The.Player.ReceivePopulation("TombSupply");
																who.AwardXP(240000, -1, 0, int.MaxValue, null, null, null, null, null, null);
																game.CompleteQuest("A Canticle for Barathrum");
																game.CompleteQuest("Decoding the Signal");
																game.CompleteQuest("More Than a Willing Spirit");
																game.CompleteQuest("The Earl of Omonporch");
																game.SetIntGameState("ForcePostEarlSpawn", 1);
																game.CompleteQuest("A Call to Arms");
																game.CompleteQuest("Pax Klanq, I Presume?");
																game.StartQuest("Tomb of the Eaters", null, null, null);
																game.FinishQuestStep("Tomb of the Eaters", "Recover the Mark of Death", -1, true, null);
																game.FinishQuestStep("Tomb of the Eaters", "Inscribe the Mark", -1, true, null);
																game.FinishQuestStep("Tomb of the Eaters", "Enter the Tomb of the Eaters", -1, true, null);
																The.Player.ToggleMarkOfDeath();
																MessageQueue.AddPlayerMessage("Mark of death now " + The.Player.HasMarkOfDeath(null).ToString(), null, true);
																The.Player.ReceiveObject("BarathrumKey", false, 0, 0, null, null, null, null);
																GritGateScripts.PromoteToJourneyfriend();
																GritGateScripts.OpenRank0Doors();
																GritGateScripts.OpenRank1Doors();
																GritGateScripts.OpenRank2Doors();
																The.ZoneManager.SetActiveZone("JoppaWorld");
																if (Wish == "stage9b")
																{
																	The.ZoneManager.SetActiveZone("JoppaWorld.53.3.0.2.9");
																}
																if (Wish == "stage9c")
																{
																	The.ZoneManager.SetActiveZone("JoppaWorld.53.3.2.2.9");
																}
																if (Wish == "stage9d")
																{
																	The.ZoneManager.SetActiveZone("JoppaWorld.53.3.1.2.9");
																}
																if (Wish == "stage9e")
																{
																	The.ZoneManager.SetActiveZone("JoppaWorld.53.3.0.2.8");
																}
																if (Wish == "stage9f")
																{
																	The.ZoneManager.SetActiveZone("JoppaWorld.53.3.2.0.0");
																}
																if (Wish == "stage9u")
																{
																	The.ZoneManager.SetActiveZone("JoppaWorld.53.3.0.2.12");
																}
																The.ZoneManager.ActiveZone.GetCell(45, 24).AddObject(physics.ParentObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
																The.ZoneManager.ProcessGoToPartyLeader();
															}
															if (Wish == "tombbeta" || Wish == "tombbetainside" || Wish == "tombbetaend" || Wish == "fastforwardtomb")
															{
																The.Player.ReceivePopulation("TombSupply");
																who.AwardXP(240000, -1, 0, int.MaxValue, null, null, null, null, null, null);
																game.CompleteQuest("A Canticle for Barathrum");
																game.CompleteQuest("Decoding the Signal");
																game.CompleteQuest("More Than a Willing Spirit");
																game.CompleteQuest("The Earl of Omonporch");
																game.SetIntGameState("ForcePostEarlSpawn", 1);
																game.CompleteQuest("A Call to Arms");
																game.CompleteQuest("Pax Klanq, I Presume?");
																game.StartQuest("Tomb of the Eaters", null, null, null);
																game.FinishQuestStep("Tomb of the Eaters", "Recover the Mark of Death", -1, true, null);
																game.FinishQuestStep("Tomb of the Eaters", "Inscribe the Mark", -1, true, null);
																game.FinishQuestStep("Tomb of the Eaters", "Enter the Tomb of the Eaters", -1, true, null);
																The.Player.ToggleMarkOfDeath();
																MessageQueue.AddPlayerMessage("Mark of death now " + The.Player.HasMarkOfDeath(null).ToString(), null, true);
																The.Player.ReceiveObject("BarathrumKey", false, 0, 0, null, null, null, null);
																GritGateScripts.PromoteToJourneyfriend();
																GritGateScripts.OpenRank0Doors();
																GritGateScripts.OpenRank1Doors();
																GritGateScripts.OpenRank2Doors();
																The.ZoneManager.SetActiveZone("JoppaWorld");
																if (Wish == "tombbetaend")
																{
																	The.ZoneManager.SetActiveZone("JoppaWorld.53.3.1.0.0");
																	The.ZoneManager.ActiveZone.GetCell(45, 12).AddObject(physics.ParentObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
																}
																else
																{
																	The.ZoneManager.SetActiveZone("JoppaWorld.53.4.0.0.11");
																	The.ZoneManager.ActiveZone.GetCell(45, 0).AddObject(physics.ParentObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
																}
																The.ZoneManager.ProcessGoToPartyLeader();
															}
															if (Wish == "stage11" || Wish == "reefjump")
															{
																The.Player.ReceivePopulation("ReefBetaSupply");
																who.AwardXP(350000, -1, 0, int.MaxValue, null, null, null, null, null, null);
																game.CompleteQuest("A Canticle for Barathrum");
																game.CompleteQuest("Decoding the Signal");
																game.CompleteQuest("More Than a Willing Spirit");
																game.CompleteQuest("The Earl of Omonporch");
																game.SetIntGameState("ForcePostEarlSpawn", 1);
																game.CompleteQuest("A Call to Arms");
																game.CompleteQuest("Pax Klanq, I Presume?");
																game.StartQuest("Tomb of the Eaters", null, null, null);
																game.FinishQuestStep("Tomb of the Eaters", "Recover the Mark of Death", -1, true, null);
																game.FinishQuestStep("Tomb of the Eaters", "Inscribe the Mark", -1, true, null);
																game.FinishQuestStep("Tomb of the Eaters", "Enter the Tomb of the Eaters", -1, true, null);
																game.FinishQuestStep("Tomb of the Eaters", "Ascend the Tomb and Cross into Brightsheol", -1, true, null);
																game.FinishQuestStep("Tomb of the Eaters", "Disable the Spindle's Magnetic Field", -1, true, null);
																game.FinishQuestStep("Tomb of the Eaters", "Return to Grit Gate", -1, true, null);
																The.Player.ReceiveObject("BarathrumKey", false, 0, 0, null, null, null, null);
																GritGateScripts.PromoteToJourneyfriend();
																GritGateScripts.OpenRank0Doors();
																GritGateScripts.OpenRank1Doors();
																GritGateScripts.OpenRank2Doors();
																The.ZoneManager.SetActiveZone("JoppaWorld");
																GritGateScripts.PromoteToJourneyfriend();
																ThinWorld.ReturnBody(The.Player);
																game.SetBooleanGameState("Recame", true);
															}
															if (Wish == "pregolem" || Wish == "posttomb")
															{
																The.Player.ReceivePopulation("ReefBetaSupply");
																who.AwardXP(600000, -1, 0, int.MaxValue, null, null, null, null, null, null);
																game.CompleteQuest("A Canticle for Barathrum");
																game.CompleteQuest("Decoding the Signal");
																game.CompleteQuest("More Than a Willing Spirit");
																game.CompleteQuest("The Earl of Omonporch");
																game.SetIntGameState("ForcePostEarlSpawn", 1);
																game.CompleteQuest("A Call to Arms");
																game.CompleteQuest("Pax Klanq, I Presume?");
																game.StartQuest("Tomb of the Eaters", null, null, null);
																game.FinishQuestStep("Tomb of the Eaters", "Recover the Mark of Death", -1, true, null);
																game.FinishQuestStep("Tomb of the Eaters", "Inscribe the Mark", -1, true, null);
																game.FinishQuestStep("Tomb of the Eaters", "Enter the Tomb of the Eaters", -1, true, null);
																game.FinishQuestStep("Tomb of the Eaters", "Ascend the Tomb and Cross into Brightsheol", -1, true, null);
																game.FinishQuestStep("Tomb of the Eaters", "Disable the Spindle's Magnetic Field", -1, true, null);
																game.SetBooleanGameState("Recame", true);
																The.Player.ReceiveObject("BarathrumKey", false, 0, 0, null, null, null, null);
																GritGateScripts.PromoteToJourneyfriend();
																GritGateScripts.OpenRank0Doors();
																GritGateScripts.OpenRank1Doors();
																GritGateScripts.OpenRank2Doors();
																The.ZoneManager.SetActiveZone("JoppaWorld");
																The.ZoneManager.SetActiveZone("JoppaWorld.22.14.1.0.14");
																The.ZoneManager.ActiveZone.GetCell(18, 8).AddObject(physics.ParentObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
																The.ZoneManager.ProcessGoToPartyLeader();
															}
															if (Wish == "golemstart" || Wish == "startmoonstair")
															{
																The.Player.ReceivePopulation("ReefBetaSupply");
																who.AwardXP(600000, -1, 0, int.MaxValue, null, null, null, null, null, null);
																game.CompleteQuest("A Canticle for Barathrum");
																game.CompleteQuest("Decoding the Signal");
																game.CompleteQuest("More Than a Willing Spirit");
																game.CompleteQuest("The Earl of Omonporch");
																game.SetIntGameState("ForcePostEarlSpawn", 1);
																game.CompleteQuest("A Call to Arms");
																game.CompleteQuest("Pax Klanq, I Presume?");
																game.CompleteQuest("Tomb of the Eaters");
																game.SetBooleanGameState("Recame", true);
																game.StartQuest("The Golem", null, null, null);
																The.Player.ReceiveObject("BarathrumKey", false, 0, 0, null, null, null, null);
																GritGateScripts.PromoteToJourneyfriend();
																GritGateScripts.OpenRank0Doors();
																GritGateScripts.OpenRank1Doors();
																GritGateScripts.OpenRank2Doors();
																The.ZoneManager.SetActiveZone("JoppaWorld");
																The.ZoneManager.SetActiveZone("JoppaWorld.22.14.1.0.14");
																The.ZoneManager.ActiveZone.GetCell(18, 8).AddObject(physics.ParentObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
																The.ZoneManager.ProcessGoToPartyLeader();
															}
															if (Wish == "postgolem")
															{
																game.CompleteQuest("A Canticle for Barathrum");
																game.CompleteQuest("Decoding the Signal");
																game.CompleteQuest("More Than a Willing Spirit");
																game.CompleteQuest("The Earl of Omonporch");
																game.SetIntGameState("ForcePostEarlSpawn", 1);
																game.CompleteQuest("A Call to Arms");
																game.CompleteQuest("Pax Klanq, I Presume?");
																game.CompleteQuest("Tomb of the Eaters");
																int num27 = Leveler.GetXPForLevel(37) - The.Player.GetStatValue("XP", 0);
																The.Player.AwardXP(num27, -1, 0, int.MaxValue, null, null, null, null, null, null);
																if (!game.GetBooleanGameState("Recame", false))
																{
																	BodyPart body = The.Player.Body.GetBody();
																	string text21 = "Floating Nearby";
																	int num28 = 0;
																	string text22 = null;
																	string text23 = null;
																	string text24 = null;
																	string text25 = null;
																	string text26 = null;
																	bool? flag4 = new bool?(true);
																	body.AddPart(text21, num28, text22, text23, text24, text25, text26, null, null, null, null, null, null, null, null, flag4, null, null, null, null, true);
																	game.SetBooleanGameState("Recame", true);
																}
																The.Player.ReceiveObject("BarathrumKey", false, 0, 0, null, null, null, null);
																GritGateScripts.PromoteToJourneyfriend();
																GritGateScripts.OpenRank0Doors();
																GritGateScripts.OpenRank1Doors();
																GritGateScripts.OpenRank2Doors();
																The.ZoneManager.SetActiveZone("JoppaWorld");
																The.ZoneManager.SetActiveZone("JoppaWorld.22.14.1.0.14");
																Cell cell8 = The.ZoneManager.ActiveZone.GetCell(18, 8);
																Cell cell9 = The.ZoneManager.ActiveZone.GetCell(21, 10);
																cell8.AddObject(physics.ParentObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
																The.ZoneManager.ProcessGoToPartyLeader();
																if (!The.Game.HasFinishedQuest("The Golem"))
																{
																	try
																	{
																		GolemQuestSelection.PlaceFinalMound(cell9);
																		The.Game.CompleteQuest("The Golem");
																	}
																	catch (Exception ex)
																	{
																		MetricsManager.LogException("Postgolem", ex, "game_exception");
																	}
																}
															}
															foreach (GameObject gameObject13 in who.GetInventoryAndEquipment())
															{
																gameObject13.MakeUnderstood(false);
															}
															Brain brain = who.Brain;
															if (brain != null)
															{
																brain.PerformEquip(false, true, null);
															}
															Popup.Suppress = false;
															ItemNaming.Suppress = false;
															return;
														}
														if (Wish == "golemquest:soup")
														{
															The.Player.ReceiveObject(GameObjectFactory.Factory.CreateObject("Phial", delegate(GameObject x)
															{
																LiquidVolume liquidVolume = x.LiquidVolume;
																liquidVolume.InitialLiquid = "proteangunk-1000";
																liquidVolume.MaxVolume = 20;
																liquidVolume.StartVolume = "20";
															}), false, null);
															return;
														}
														if (Wish == "reequip")
														{
															who.Brain.PerformReequip(false, true, false, null);
															return;
														}
														if (!(Wish == "maket2") && !(Wish == "worldbmp"))
														{
															if (Wish == "garbagetest")
															{
																for (int num29 = 0; num29 < 1000000; num29++)
																{
																	MessageQueue.AddPlayerMessage("garbage", null, true);
																}
																return;
															}
															if (Wish == "playeronly")
															{
																The.Game.ActionManager.ActionQueue.Clear();
																The.Game.ActionManager.ActionQueue.Enqueue(who);
																The.Game.ActionManager.ActionQueue.Enqueue(null);
																MessageQueue.AddPlayerMessage("Removed everyone but the player from the action queue.", null, true);
																return;
															}
															if (Wish.StartsWith("confusion"))
															{
																string[] array4 = Wish.Split(':', StringSplitOptions.None);
																int num30 = ((array4.Length >= 2) ? int.Parse(array4[1]) : 10);
																int num31 = ((array4.Length >= 3) ? int.Parse(array4[2]) : 5);
																who.ApplyEffect(new XRL.World.Effects.Confused(num30, num31, num31 + 2, "Confusion"), null);
																return;
															}
															if (Wish.StartsWith("roll:"))
															{
																int num7 = Stat.Roll(Wish.Split(':', StringSplitOptions.None)[1], null);
																Popup.Show(num7.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
																return;
															}
															if (Wish.StartsWith("rollmin:"))
															{
																int num7 = Stat.RollMin(Wish.Split(':', StringSplitOptions.None)[1]);
																Popup.Show(num7.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
																return;
															}
															if (Wish.StartsWith("rollmax:"))
															{
																int num7 = Stat.RollMax(Wish.Split(':', StringSplitOptions.None)[1]);
																Popup.Show(num7.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
																return;
															}
															if (Wish.StartsWith("rollcached:"))
															{
																int num7 = Stat.RollCached(Wish.Split(':', StringSplitOptions.None)[1]);
																Popup.Show(num7.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
																return;
															}
															if (Wish.StartsWith("rollmincached:"))
															{
																int num7 = Stat.RollMinCached(Wish.Split(':', StringSplitOptions.None)[1]);
																Popup.Show(num7.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
																return;
															}
															if (Wish.StartsWith("rollmaxcached:"))
															{
																int num7 = Stat.RollMaxCached(Wish.Split(':', StringSplitOptions.None)[1]);
																Popup.Show(num7.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
																return;
															}
															if (Wish.StartsWith("godown:"))
															{
																int num32 = int.Parse(Wish.Split(':', StringSplitOptions.None)[1]);
																Zone zoneFromDirection = who.CurrentZone.GetZoneFromDirection("D", num32);
																Point2D pos2D = who.CurrentCell.Pos2D;
																who.Physics.CurrentCell.RemoveObject(who, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
																zoneFromDirection.GetCell(pos2D).AddObject(who, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
																The.ZoneManager.SetActiveZone(zoneFromDirection);
																The.ZoneManager.ProcessGoToPartyLeader();
																return;
															}
															if (Wish == "sherlock")
															{
																game.CompleteQuest("Find Eskhind");
																game.StartQuest("Kith and Kin", null, null, null);
																JournalAPI.Observations.Where<JournalObservation>((JournalObservation o) => o.Has("hindrenclue") && o.Has("free")).ToList<JournalObservation>().Shuffle<JournalObservation>(null)
																	.Take<JournalObservation>(5)
																	.ToList<JournalObservation>()
																	.ForEach(delegate(JournalObservation o)
																	{
																		JournalAPI.RevealObservation(o);
																	});
																JournalAPI.Observations.Where<JournalObservation>(delegate(JournalObservation o)
																{
																	if (o.Has("hindrenclue"))
																	{
																		return o.Attributes.Any<string>((string p) => p.StartsWith("motive:"));
																	}
																	return false;
																}).ToList<JournalObservation>().Shuffle<JournalObservation>(null)
																	.Take<JournalObservation>(5)
																	.ToList<JournalObservation>()
																	.ForEach(delegate(JournalObservation o)
																	{
																		JournalAPI.RevealObservation(o);
																	});
																KithAndKinGameState.Instance.foundClue();
																if (The.ActiveZone.FindObject("Neelahind") == null)
																{
																	who.GetCurrentCell().GetCellFromDirection("NW", true).AddObject("Neelahind", null, null, null, null);
																	return;
																}
															}
															else
															{
																if (Wish == "revealobservations")
																{
																	Popup.Suppress = true;
																	JournalAPI.Observations.ForEach(delegate(JournalObservation o)
																	{
																		JournalAPI.RevealObservation(o);
																	});
																	Popup.Suppress = false;
																	return;
																}
																if (Wish == "revealmapnotes")
																{
																	Popup.Suppress = true;
																	JournalAPI.MapNotes.ForEach(delegate(JournalMapNote o)
																	{
																		JournalAPI.RevealMapNote(o, false, null);
																	});
																	Popup.Suppress = false;
																	return;
																}
																if (Wish == "calm")
																{
																	The.Core.Calm = !The.Core.Calm;
																	MessageQueue.AddPlayerMessage("Calm now " + The.Core.Calm.ToString(), null, true);
																	return;
																}
																if (Wish == "minime")
																{
																	EvilTwin.CreateEvilTwin(who, "Mini", null, "{{c|You sense a diminutive presence nearby.}}", "&K", null, null, true, "It's miniature you.", null);
																	return;
																}
																if (Wish == "blink")
																{
																	who.TeleportTo(who.Physics.PickDestinationCell(9999, AllowVis.OnlyExplored, false, false, false, true, PickTarget.PickStyle.EmptyCell, "Teleport", false, null), new int?(0), true, false, false, false, "disappear", "appear");
																	return;
																}
																if (Wish == "license")
																{
																	MessageQueue.AddPlayerMessage("License tier now " + who.ModIntProperty("CyberneticsLicenses", 20, false).ToString(), null, true);
																	return;
																}
																if (Wish.StartsWith("license:"))
																{
																	int num33 = int.Parse(Wish.Split(':', StringSplitOptions.None)[1]);
																	if (num33 != 0)
																	{
																		MessageQueue.AddPlayerMessage("License tier now " + who.ModIntProperty("CyberneticsLicenses", num33, false).ToString(), null, true);
																		return;
																	}
																}
																else
																{
																	if (Wish == "impl")
																	{
																		who.ImplosionSplat(12);
																		return;
																	}
																	if (Wish.StartsWith("impl:"))
																	{
																		int num34 = int.Parse(Wish.Split(':', StringSplitOptions.None)[1]);
																		if (num34 != 0)
																		{
																			who.ImplosionSplat(num34);
																			return;
																		}
																	}
																	else
																	{
																		if (Wish == "gainmp")
																		{
																			Popup.Show(who.GainMP(1).ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
																			return;
																		}
																		if (Wish.StartsWith("gainmp:"))
																		{
																			int num35 = int.Parse(Wish.Split(':', StringSplitOptions.None)[1]);
																			if (num35 != 0)
																			{
																				Popup.Show(who.GainMP(num35).ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
																				return;
																			}
																		}
																		else
																		{
																			if (Wish == "bits")
																			{
																				who.RequirePart<BitLocker>(false).AddAllBits(20);
																				return;
																			}
																			if (Wish.StartsWith("bits:"))
																			{
																				int num36 = int.Parse(Wish.Split(':', StringSplitOptions.None)[1]);
																				if (num36 > 0)
																				{
																					who.RequirePart<BitLocker>(false).AddAllBits(num36);
																					return;
																				}
																			}
																			else
																			{
																				if (Wish == "smartass")
																				{
																					Popup.Suppress = true;
																					try
																					{
																						Wishing.AddSkill("Tinkering");
																						Wishing.AddSkill("Tinkering_GadgetInspector");
																						Wishing.AddSkill("Tinkering_Repair");
																						Wishing.AddSkill("Tinkering_ReverseEngineer");
																						Wishing.AddSkill("Tinkering_Scavenger");
																						Wishing.AddSkill("Tinkering_Disassemble");
																						Wishing.AddSkill("Tinkering_LayMine");
																						Wishing.AddSkill("Tinkering_DeployTurret");
																						Wishing.AddSkill("Tinkering_Tinker1");
																						Wishing.AddSkill("Tinkering_Tinker2");
																						Wishing.AddSkill("Tinkering_Tinker3");
																						using (List<TinkerData>.Enumerator enumerator15 = TinkerData.TinkerRecipes.GetEnumerator())
																						{
																							while (enumerator15.MoveNext())
																							{
																								TinkerData tinkerData = enumerator15.Current;
																								if (!TinkerData.KnownRecipes.CleanContains(tinkerData))
																								{
																									TinkerData.KnownRecipes.Add(tinkerData);
																								}
																							}
																							return;
																						}
																					}
																					finally
																					{
																						Popup.Suppress = false;
																					}
																				}
																				if (Wish == "cloacasurprise")
																				{
																					CookingDomainSpecial_UnitSlogTransform.ApplyTo(who);
																					return;
																				}
																				if (Wish == "crystaldelight")
																				{
																					CookingDomainSpecial_UnitCrystalTransform.ApplyTo(who);
																					return;
																				}
																				if (Wish == "showrecipes")
																				{
																					StringBuilder stringBuilder12 = Event.NewStringBuilder(null);
																					foreach (TinkerData tinkerData2 in TinkerData.TinkerRecipes)
																					{
																						stringBuilder12.Append("DisplayName: ").Append(tinkerData2.DisplayName ?? "NULL").Append("\nBlueprint: ")
																							.Append(tinkerData2.Blueprint ?? "NULL")
																							.Append("\nCategory: ")
																							.Append(tinkerData2.Category ?? "NULL")
																							.Append("\nType: ")
																							.Append(tinkerData2.Type ?? "NULL")
																							.Append("\nTier: ")
																							.Append(tinkerData2.Tier)
																							.Append("\nCost: ")
																							.Append(tinkerData2.Cost)
																							.Append("\nIngredient: ")
																							.Append(tinkerData2.Ingredient ?? "NULL")
																							.Append("\nDescriptionLineCount: ")
																							.Append(tinkerData2.DescriptionLineCount)
																							.Append("\n\n");
																					}
																					Popup.Show(stringBuilder12.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
																					return;
																				}
																				if (Wish == "showmodrecipes")
																				{
																					StringBuilder stringBuilder13 = Event.NewStringBuilder(null);
																					foreach (TinkerData tinkerData3 in TinkerData.TinkerRecipes)
																					{
																						if (tinkerData3.Type == "Mod")
																						{
																							stringBuilder13.Append("DisplayName: ").Append(tinkerData3.DisplayName ?? "NULL").Append("\nBlueprint: ")
																								.Append(tinkerData3.Blueprint ?? "NULL")
																								.Append("\nCategory: ")
																								.Append(tinkerData3.Category ?? "NULL")
																								.Append("\nType: ")
																								.Append(tinkerData3.Type ?? "NULL")
																								.Append("\nTier: ")
																								.Append(tinkerData3.Tier)
																								.Append("\nCost: ")
																								.Append(tinkerData3.Cost)
																								.Append("\nIngredient: ")
																								.Append(tinkerData3.Ingredient ?? "NULL")
																								.Append("\nDescriptionLineCount: ")
																								.Append(tinkerData3.DescriptionLineCount)
																								.Append("\n\n");
																						}
																					}
																					Popup.Show(stringBuilder13.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
																					return;
																				}
																				if (Wish == "findduplicaterecipes")
																				{
																					Dictionary<string, int> dictionary = new Dictionary<string, int>();
																					foreach (TinkerData tinkerData4 in TinkerData.TinkerRecipes)
																					{
																						string blueprint = tinkerData4.Blueprint;
																						string text27 = (blueprint.StartsWith("[") ? blueprint : GameObjectFactory.Factory.CreateSampleObject(blueprint, null).Render.DisplayName);
																						if (dictionary.ContainsKey(text27))
																						{
																							Dictionary<string, int> dictionary2 = dictionary;
																							string text28 = text27;
																							int num7 = dictionary2[text28];
																							dictionary2[text28] = num7 + 1;
																						}
																						else
																						{
																							dictionary.Add(text27, 1);
																						}
																					}
																					StringBuilder stringBuilder14 = Event.NewStringBuilder(null);
																					bool flag5 = false;
																					foreach (string text29 in dictionary.Keys)
																					{
																						if (dictionary[text29] > 1)
																						{
																							stringBuilder14.Append(text29).Append(" (").Append(dictionary[text29])
																								.Append(")\n");
																							flag5 = true;
																						}
																					}
																					Popup.Show(flag5 ? stringBuilder14.ToString() : "no duplicate recipes found", null, "Sounds/UI/ui_notification", true, true, true, true, null);
																					return;
																				}
																				if (Wish == "cluber")
																				{
																					Wishing.AddSkill("Cudgel");
																					Wishing.AddSkill("Cudgel_Expertise");
																					Wishing.AddSkill("Cudgel_Backswing");
																					Wishing.AddSkill("Cudgel_Bludgeon");
																					Wishing.AddSkill("Cudgel_ChargingStrike");
																					Wishing.AddSkill("Cudgel_Conk");
																					Wishing.AddSkill("Cudgel_Slam");
																					Wishing.AddSkill("Cudgel_SmashUp");
																					return;
																				}
																				if (Wish == "fencer")
																				{
																					Wishing.AddSkill("LongBlades");
																					Wishing.AddSkill("LongBladesDuelingStance");
																					Wishing.AddSkill("LongBladesImprovedAggressiveStance");
																					Wishing.AddSkill("LongBladesImprovedDefensiveStance");
																					Wishing.AddSkill("LongBladesImprovedDuelistStance");
																					Wishing.AddSkill("LongBladesDuelingStance");
																					Wishing.AddSkill("LongBladesLunge");
																					Wishing.AddSkill("LongBladesProficiency");
																					Wishing.AddSkill("LongBladesSwipe");
																					Wishing.AddSkill("LongBladesDeathblow");
																					return;
																				}
																				if (Wish == "axer")
																				{
																					Wishing.AddSkill("Axe");
																					Wishing.AddSkill("Axe_Expertise");
																					Wishing.AddSkill("Axe_Cleave");
																					Wishing.AddSkill("Cudgel_ChargingStrike");
																					Wishing.AddSkill("Axe_Dismember");
																					Wishing.AddSkill("Axe_HookAndDrag");
																					Wishing.AddSkill("Axe_Decapitate");
																					Wishing.AddSkill("Axe_Berserk");
																					return;
																				}
																				if (Wish == "sblader")
																				{
																					Wishing.AddSkill("ShortBlades");
																					Wishing.AddSkill("ShortBlades_Expertise");
																					Wishing.AddSkill("ShortBlades_Hobble");
																					Wishing.AddSkill("ShortBlades_Jab");
																					Wishing.AddSkill("ShortBlades_Bloodletter");
																					Wishing.AddSkill("ShortBlades_Shank");
																					Wishing.AddSkill("ShortBlades_PointedCircle");
																					Wishing.AddSkill("ShortBlades_Rejoinder");
																					return;
																				}
																				if (Wish == "wandermode")
																				{
																					using (IEnumerator<Faction> enumerator17 = Factions.Loop().GetEnumerator())
																					{
																						while (enumerator17.MoveNext())
																						{
																							Faction faction = enumerator17.Current;
																							if (The.Game.PlayerReputation.Get(faction) < 0)
																							{
																								The.Game.PlayerReputation.Set(faction, 0);
																							}
																						}
																						return;
																					}
																				}
																				if (Wish == "skillpoints")
																				{
																					who.Statistics["SP"].BaseValue = 20000;
																					return;
																				}
																				if (Wish == "traveler")
																				{
																					using (IEnumerator<Faction> enumerator17 = Factions.Loop().GetEnumerator())
																					{
																						while (enumerator17.MoveNext())
																						{
																							Faction faction2 = enumerator17.Current;
																							game.PlayerReputation.Set(faction2, 0);
																						}
																						return;
																					}
																				}
																				if (Wish == "togglementalshields")
																				{
																					MentalShield.Disabled = !MentalShield.Disabled;
																					return;
																				}
																				if (Wish == "trip")
																				{
																					who.ApplyEffect(new Prone(), null);
																					return;
																				}
																				if (Wish == "pro")
																				{
																					who.Statistics["Strength"].BaseValue = 40;
																					who.Statistics["Intelligence"].BaseValue = 40;
																					who.Statistics["Ego"].BaseValue = 40;
																					who.Statistics["Agility"].BaseValue = 40;
																					who.Statistics["Toughness"].BaseValue = 40;
																					who.Statistics["Willpower"].BaseValue = 40;
																					return;
																				}
																				if (Wish == "where?")
																				{
																					MessageQueue.AddPlayerMessage(who.CurrentZone.ZoneID, null, true);
																					XRLCore.SetClipboard(who.CurrentZone.ZoneID);
																					return;
																				}
																				if (Wish.StartsWith("factionrep") || Wish.StartsWith("reputation"))
																				{
																					string[] array5 = Wish.Split(' ', StringSplitOptions.None);
																					if (array5.Length > 3 || Wish.Contains(":"))
																					{
																						array5 = Wish.Split(':', StringSplitOptions.None);
																					}
																					game.PlayerReputation.Modify(array5[1], Convert.ToInt32(array5[2]), "Wish", null, null, false, false);
																					return;
																				}
																				if (!(Wish == "memtest"))
																				{
																					if (Wish == "leadslugs")
																					{
																						who.ReceiveObject("Lead Slug", 10000, false, 0, 0, null, null, null, null);
																						return;
																					}
																					if (Wish == "pistoltest")
																					{
																						who.ReceiveObject("Lead Slug", 10000, false, 0, 0, null, null, null, null);
																						who.ReceiveObject("Chain Pistol", 2, false, 0, 0, null, null, null, null);
																						Wishing.AddSkill("Akimbo");
																						Wishing.AddSkill("Steady Hand");
																						return;
																					}
																					if (Wish.StartsWith("regionalize"))
																					{
																						new RegionPopulator().BuildZone(who.Physics.CurrentCell.ParentZone);
																						return;
																					}
																					if (Wish.StartsWith("smartitem:"))
																					{
																						WishResult wishResult = WishSearcher.SearchForBlueprint(Wish);
																						GameObject gameObject14 = GameObjectFactory.Factory.CreateObject(wishResult.Result, 0, 0, null, null, null, "Wish", null);
																						gameObject14.AddPart<SmartItem>(new SmartItem(), true, false);
																						who.Physics.CurrentCell.AddObject(gameObject14, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
																						return;
																					}
																					if (Wish.StartsWith("seed:"))
																					{
																						int num37 = Convert.ToInt32(Wish.Split(':', StringSplitOptions.None)[1]);
																						game.SetIntGameState("WorldSeed", num37);
																						return;
																					}
																					if (Wish.StartsWith("setintgamestate:"))
																					{
																						string[] array6 = Wish.Split(':', StringSplitOptions.None);
																						int num38 = ((array6.Length > 2) ? int.Parse(array6[2]) : 1);
																						The.Game.SetIntGameState(array6[1], num38);
																						return;
																					}
																					if (Wish.StartsWith("setboolgamestate:"))
																					{
																						string[] array7 = Wish.Split(':', StringSplitOptions.None);
																						bool flag6 = array7.Length <= 2 || bool.Parse(array7[2]);
																						The.Game.SetBooleanGameState(array7[1], flag6);
																						return;
																					}
																					if (Wish == "glowcrust")
																					{
																						FungalSporeInfection.ApplyFungalInfection(who, "LuminousInfection", null);
																						return;
																					}
																					if (Wish == "mumblemouth")
																					{
																						FungalSporeInfection.ApplyFungalInfection(who, "MumblesInfection", null);
																						return;
																					}
																					if (Wish == "waxflab")
																					{
																						FungalSporeInfection.ApplyFungalInfection(who, "WaxInfection", null);
																						return;
																					}
																					if (Wish == "testmadness")
																					{
																						who.Physics.CurrentCell.ParentZone.FindClosestObjectWithPart(who, "Brain", true, false).Brain.PushGoal(new PaxKlanqMadness());
																						return;
																					}
																					if (Wish == "randomitems")
																					{
																						for (int num39 = 0; num39 < 20; num39++)
																						{
																							who.GetCurrentCell().AddObject(EncountersAPI.GetAnItem(null), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
																						}
																						return;
																					}
																					if (Wish.StartsWith("sultantest:"))
																					{
																						string[] array8 = Wish.Split(':', StringSplitOptions.None)[1].Split(',', StringSplitOptions.None);
																						Zone zone = The.ZoneManager.GetZone("JoppaWorld");
																						ZoneManager zoneManager = The.ZoneManager;
																						History sultanHistory = The.Game.sultanHistory;
																						Cell cell10 = zone.GetCell(who.GetCurrentCell().Pos2D);
																						SultanDungeonArgs sultanDungeonArgs = new SultanDungeonArgs();
																						HistoricEntity historicEntity = sultanHistory.CreateEntity(sultanHistory.currentYear);
																						historicEntity.ApplyEvent(new InitializeRegion(5), long.MinValue);
																						HistoricEntitySnapshot currentSnapshot = historicEntity.GetCurrentSnapshot();
																						foreach (string text30 in new List<string>(currentSnapshot.properties.Keys))
																						{
																							if (text30 != "name" && text30 != "newName" && text30 != "period")
																							{
																								currentSnapshot.properties.Remove(text30);
																							}
																						}
																						currentSnapshot.listProperties.Clear();
																						currentSnapshot.listProperties.Add("testAttributes", new List<string>());
																						currentSnapshot.listProperties["testAttributes"].AddRange(array8);
																						sultanDungeonArgs.UpdateFromEntity(currentSnapshot, "SultanDungeons_");
																						string property = currentSnapshot.GetProperty("name", "unknown");
																						The.Game.SetObjectGameState("sultanDungeonArgs_" + property, sultanDungeonArgs);
																						Vector2i vector2i = new Vector2i(cell10.X, cell10.Y);
																						string text31 = Grammar.MakeTitleCase(property);
																						int num40 = 10;
																						for (int num41 = 0; num41 < num40; num41++)
																						{
																							HistoricEntity historicEntity2 = sultanHistory.CreateEntity(sultanHistory.currentYear);
																							historicEntity2.ApplyEvent(new InitializeLocation(property, 5), long.MinValue);
																							string property2 = historicEntity2.GetCurrentSnapshot().GetProperty("name", "unknown");
																							string[] array9 = new string[6];
																							array9[0] = "JoppaWorld.";
																							array9[1] = vector2i.x.ToString();
																							array9[2] = ".";
																							array9[3] = vector2i.y.ToString();
																							array9[4] = ".1.1.";
																							int num42 = 5;
																							int num7 = num41 + 10;
																							array9[num42] = num7.ToString();
																							string text32 = string.Concat(array9);
																							if (num41 == 0)
																							{
																								zoneManager.SetZoneName(text32, text31, null, null, null, null, true, true);
																							}
																							else
																							{
																								zoneManager.SetZoneName(text32, "liminal floor", text31, null, null, null, false, true);
																							}
																							string text33 = "";
																							if (num41 < num40 - 1)
																							{
																								text33 += "D";
																							}
																							if (num41 > 0)
																							{
																								text33 += "U";
																							}
																							zoneManager.ClearZoneBuilders(text32);
																							zoneManager.SetZoneProperty(text32, "SkipTerrainBuilders", true);
																							zoneManager.AddZoneMidBuilder(text32, "SultanDungeon", "locationName", property2, "regionName", property, "stairs", text33);
																						}
																						return;
																					}
																					if (Wish == "sultanreveal")
																					{
																						Popup.Suppress = true;
																						ItemNaming.Suppress = true;
																						Zone zone2 = The.ZoneManager.GetZone("JoppaWorld");
																						for (int num43 = 0; num43 < 25; num43++)
																						{
																							for (int num44 = 0; num44 < 80; num44++)
																							{
																								zone2.GetCell(num44, num43).GetFirstObjectWithPart("TerrainTravel").FireEvent("SultanReveal");
																							}
																						}
																						Popup.Suppress = false;
																						ItemNaming.Suppress = false;
																						return;
																					}
																					if (Wish == "villagereveal")
																					{
																						Popup.Suppress = true;
																						ItemNaming.Suppress = true;
																						Zone zone3 = The.ZoneManager.GetZone("JoppaWorld");
																						for (int num45 = 0; num45 < 25; num45++)
																						{
																							for (int num46 = 0; num46 < 80; num46++)
																							{
																								zone3.GetCell(num46, num45).GetFirstObjectWithPart("TerrainTravel").FireEvent("VillageReveal");
																							}
																						}
																						Popup.Suppress = false;
																						ItemNaming.Suppress = false;
																						return;
																					}
																					if (Wish == "zonebuilders")
																					{
																						IEnumerable<ZoneBuilderBlueprint> buildersFor = The.ZoneManager.GetBuildersFor(The.ZoneManager.ActiveZone);
																						StringBuilder stringBuilder15 = new StringBuilder();
																						foreach (ZoneBuilderBlueprint zoneBuilderBlueprint in buildersFor)
																						{
																							stringBuilder15.Append(zoneBuilderBlueprint.Class);
																							if (zoneBuilderBlueprint.Parameters != null && zoneBuilderBlueprint.Parameters.Count > 0)
																							{
																								stringBuilder15.Append(" [");
																								foreach (KeyValuePair<string, object> keyValuePair3 in zoneBuilderBlueprint.Parameters)
																								{
																									if (stringBuilder15[stringBuilder15.Length - 1] != '[')
																									{
																										stringBuilder15.Append(", ");
																									}
																									stringBuilder15.Append(keyValuePair3.Key).Append(": ");
																									stringBuilder15.Append(keyValuePair3.Value.ToString());
																								}
																								stringBuilder15.Append(']');
																							}
																							stringBuilder15.Append('\n');
																						}
																						Popup.Show(stringBuilder15.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
																						return;
																					}
																					if (Wish == "zoneconnections")
																					{
																						Zone activeZone = The.ActiveZone;
																						ScreenBuffer scrapBuffer2 = ScreenBuffer.GetScrapBuffer1(false);
																						scrapBuffer2.RenderBase();
																						foreach (ZoneConnection zoneConnection in activeZone.EnumerateConnections())
																						{
																							CachedZoneConnection cachedZoneConnection = zoneConnection as CachedZoneConnection;
																							if (cachedZoneConnection != null)
																							{
																								scrapBuffer2.WriteAt(zoneConnection.X, zoneConnection.Y, "{{R|" + cachedZoneConnection.TargetDirection + "}}", true);
																							}
																							else
																							{
																								scrapBuffer2.WriteAt(zoneConnection.X, zoneConnection.Y, "{{M|X}}", true);
																							}
																						}
																						scrapBuffer2.Draw();
																						Keyboard.getch();
																						return;
																					}
																					if (Wish == "freezezones")
																					{
																						foreach (Zone zone4 in The.ZoneManager.CachedZones.Values)
																						{
																							if (!zone4.IsActive())
																							{
																								zone4.LastActive = 0L;
																							}
																						}
																						The.ZoneManager.CheckCached(true, false);
																						return;
																					}
																					if (Wish == "clearfrozen")
																					{
																						The.ZoneManager.ClearFrozen();
																						return;
																					}
																					if (Wish == "sultanhistory")
																					{
																						using (List<JournalSultanNote>.Enumerator enumerator22 = JournalAPI.GetSultanNotes(null).GetEnumerator())
																						{
																							while (enumerator22.MoveNext())
																							{
																								JournalSultanNote journalSultanNote = enumerator22.Current;
																								journalSultanNote.Reveal(null, true);
																							}
																							return;
																						}
																					}
																					if (Wish == "sultanquests")
																					{
																						HistoricEntityList entitiesWherePropertyEquals = The.Game.sultanHistory.GetEntitiesWherePropertyEquals("type", "sultan");
																						for (int num47 = 0; num47 < entitiesWherePropertyEquals.entities.Count; num47++)
																						{
																							for (int num48 = 0; num48 < entitiesWherePropertyEquals.entities[num47].events.Count; num48++)
																							{
																								entitiesWherePropertyEquals.entities[num47].events[num48].Reveal(null);
																							}
																						}
																						return;
																					}
																					if (Wish == "reveal1sultanhistory")
																					{
																						string text34 = null;
																						using (List<JournalSultanNote>.Enumerator enumerator22 = JournalAPI.GetSultanNotes(null).GetEnumerator())
																						{
																							while (enumerator22.MoveNext())
																							{
																								JournalSultanNote journalSultanNote2 = enumerator22.Current;
																								if (text34 == null)
																								{
																									text34 = journalSultanNote2.SultanID;
																								}
																								if (journalSultanNote2.SultanID == text34)
																								{
																									journalSultanNote2.Reveal(null, false);
																								}
																							}
																							return;
																						}
																					}
																					if (Wish == "revealallsecrets")
																					{
																						using (IEnumerator<IBaseJournalEntry> enumerator23 = JournalAPI.GetAllNotes().GetEnumerator())
																						{
																							while (enumerator23.MoveNext())
																							{
																								IBaseJournalEntry baseJournalEntry = enumerator23.Current;
																								baseJournalEntry.Reveal(null, true);
																							}
																							return;
																						}
																					}
																					if (Wish == "glass")
																					{
																						History sultanHistory2 = The.Game.sultanHistory;
																						HistoricEntityList entitiesWithProperty = sultanHistory2.GetEntitiesWithProperty("itemType");
																						for (int num49 = 0; num49 < entitiesWithProperty.entities.Count; num49++)
																						{
																							HistoricEntitySnapshot currentSnapshot2 = entitiesWithProperty.entities[num49].GetCurrentSnapshot();
																							global::UnityEngine.Debug.Log("New historic relic: " + currentSnapshot2.GetProperty("name", "unknown"));
																							who.GetCurrentCell().AddObject(RelicGenerator.GenerateRelic(currentSnapshot2, Stat.Random(1, 8), null, false), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
																						}
																						List<string> list12 = new List<string>();
																						list12.Add("glass");
																						sultanHistory2.GetEntitiesWherePropertyEquals("type", "region").GetRandomElement(null).GetCurrentSnapshot();
																						List<string> list13 = new List<string>();
																						list13.Add(list12.GetRandomElement<string>(null));
																						using (List<string>.Enumerator enumerator7 = RelicGenerator.GetTypes().GetEnumerator())
																						{
																							while (enumerator7.MoveNext())
																							{
																								string text35 = enumerator7.Current;
																								who.GetCurrentCell().AddObject(RelicGenerator.GenerateRelic(text35, Stat.Random(1, 8), null, list13, null, null, null, null), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
																							}
																							return;
																						}
																					}
																					if (Wish.StartsWith("findmod:"))
																					{
																						string[] array10 = Wish.Split(':', StringSplitOptions.None);
																						ModInfo modBySpec = ModManager.GetModBySpec(array10[1]);
																						if (modBySpec == null)
																						{
																							Popup.Show("No mod found for " + array10[1], null, "Sounds/UI/ui_notification", true, true, true, true, null);
																							return;
																						}
																						string text36 = "Found mod";
																						List<string> list14 = new List<string>();
																						if (modBySpec.ID.IsNullOrEmpty())
																						{
																							list14.Add("no ID");
																						}
																						else
																						{
																							text36 = text36 + " " + modBySpec.ID;
																						}
																						if (modBySpec.WorkshopInfo != null && modBySpec.WorkshopInfo.WorkshopId > 0UL)
																						{
																							text36 = text36 + " workshop ID " + modBySpec.WorkshopInfo.WorkshopId.ToString();
																						}
																						else
																						{
																							list14.Add("no workshop ID");
																						}
																						if (modBySpec.IsScripting)
																						{
																							list14.Add("scripting");
																						}
																						if (modBySpec.Path != null)
																						{
																							text36 = text36 + " in " + modBySpec.Path;
																						}
																						else
																						{
																							list14.Add("no file path");
																						}
																						if (list14.Count > 0)
																						{
																							text36 = text36 + " (" + string.Join(", ", list14.ToArray()) + ")";
																						}
																						Popup.Show(text36, null, "Sounds/UI/ui_notification", true, true, true, true, null);
																						return;
																					}
																					else
																					{
																						if (Wish.StartsWith("randomrelic:"))
																						{
																							string[] array11 = Wish.Split(':', StringSplitOptions.None);
																							for (int num50 = 0; num50 < Convert.ToInt32(array11[1]); num50++)
																							{
																								who.GetCurrentCell().AddObject(RelicGenerator.GenerateRelic(Stat.Random(1, 8), true), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
																							}
																							return;
																						}
																						if (Wish == "sultanrelics")
																						{
																							HistoricEntityList entitiesWithProperty2 = The.Game.sultanHistory.GetEntitiesWithProperty("itemType");
																							for (int num51 = 0; num51 < entitiesWithProperty2.entities.Count; num51++)
																							{
																								HistoricEntitySnapshot currentSnapshot3 = entitiesWithProperty2.entities[num51].GetCurrentSnapshot();
																								global::UnityEngine.Debug.Log("New historic relic: " + currentSnapshot3.GetProperty("name", "unknown"));
																								who.GetCurrentCell().AddObject(RelicGenerator.GenerateRelic(currentSnapshot3, null, false), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
																							}
																							return;
																						}
																						if (Wish == "relic")
																						{
																							History sultanHistory3 = The.Game.sultanHistory;
																							HistoricEntityList entitiesWithProperty3 = sultanHistory3.GetEntitiesWithProperty("itemType");
																							for (int num52 = 0; num52 < entitiesWithProperty3.entities.Count; num52++)
																							{
																								HistoricEntitySnapshot currentSnapshot4 = entitiesWithProperty3.entities[num52].GetCurrentSnapshot();
																								global::UnityEngine.Debug.Log("New historic relic: " + currentSnapshot4.GetProperty("name", "unknown"));
																								who.GetCurrentCell().AddObject(RelicGenerator.GenerateRelic(currentSnapshot4, null, false), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
																							}
																							List<string> list15 = new List<string>();
																							list15.Add("chance");
																							list15.Add("might");
																							list15.Add("scholarship");
																							list15.Add("time");
																							list15.Add("ice");
																							list15.Add("stars");
																							list15.Add("salt");
																							list15.Add("jewels");
																							list15.Add("glass");
																							list15.Add("circuitry");
																							list15.Add("travel");
																							sultanHistory3.GetEntitiesWherePropertyEquals("type", "region").GetRandomElement(null).GetCurrentSnapshot();
																							List<string> list16 = new List<string>();
																							list16.Add(list15.GetRandomElement<string>(null));
																							using (List<string>.Enumerator enumerator7 = RelicGenerator.GetTypes().GetEnumerator())
																							{
																								while (enumerator7.MoveNext())
																								{
																									string text37 = enumerator7.Current;
																									who.GetCurrentCell().AddObject(RelicGenerator.GenerateRelic(text37, Stat.Random(1, 8), null, list16, null, null, null, null), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
																								}
																								return;
																							}
																						}
																						if (Wish == "shownamingchances")
																						{
																							StringBuilder stringBuilder16 = Event.NewStringBuilder(null);
																							Dictionary<GameObject, int> chances = ItemNaming.GetNamingChances(The.Player);
																							List<GameObject> list17 = Event.NewGameObjectList();
																							list17.AddRange(chances.Keys);
																							list17.Sort((GameObject a, GameObject b) => chances[b].CompareTo(chances[a]));
																							foreach (GameObject gameObject15 in list17)
																							{
																								stringBuilder16.Append(chances[gameObject15]).Append(": ").Append(gameObject15.DisplayName)
																									.Append("&y\n");
																							}
																							Popup.Show(stringBuilder16.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
																							return;
																						}
																						if (Wish == "whatami")
																						{
																							MessageQueue.AddPlayerMessage("I am a: " + who.Blueprint, null, true);
																							return;
																						}
																						if (Wish == "cool")
																						{
																							The.Core.cool = !The.Core.cool;
																							The.ActionManager.TickAbilityCooldowns(9999999);
																							MessageQueue.AddPlayerMessage("Coolmode now " + The.Core.cool.ToString(), null, true);
																							return;
																						}
																						if (Wish.StartsWith("stat:"))
																						{
																							string[] array12 = Wish.Split(':', StringSplitOptions.None);
																							who.Statistics[array12[1]].BaseValue += Convert.ToInt32(array12[2]);
																							string[] array13 = new string[5];
																							array13[0] = "Added ";
																							int num53 = 1;
																							int num7 = Convert.ToInt32(array12[2]);
																							array13[num53] = num7.ToString();
																							array13[2] = " to ";
																							array13[3] = array12[1];
																							array13[4] = "'s BaseValue";
																							MessageQueue.AddPlayerMessage(string.Concat(array13), null, true);
																							return;
																						}
																						if (Wish.StartsWith("statbonus:"))
																						{
																							string[] array14 = Wish.Split(':', StringSplitOptions.None);
																							who.Statistics[array14[1]].Bonus += Convert.ToInt32(array14[2]);
																							string[] array15 = new string[5];
																							array15[0] = "Added ";
																							int num54 = 1;
																							int num7 = Convert.ToInt32(array14[2]);
																							array15[num54] = num7.ToString();
																							array15[2] = " to ";
																							array15[3] = array14[1];
																							array15[4] = "'s Bonus";
																							MessageQueue.AddPlayerMessage(string.Concat(array15), null, true);
																							return;
																						}
																						if (Wish.StartsWith("statpenality:") || Wish.StartsWith("statpenalty:"))
																						{
																							string[] array16 = Wish.Split(':', StringSplitOptions.None);
																							who.Statistics[array16[1]].Penalty += Convert.ToInt32(array16[2]);
																							string[] array17 = new string[5];
																							array17[0] = "Added ";
																							int num55 = 1;
																							int num7 = Convert.ToInt32(array16[2]);
																							array17[num55] = num7.ToString();
																							array17[2] = " to ";
																							array17[3] = array16[1];
																							array17[4] = "'s Penalty";
																							MessageQueue.AddPlayerMessage(string.Concat(array17), null, true);
																							return;
																						}
																						if (Wish == "testearl")
																						{
																							string[] array18 = new string[5];
																							List<string> list18 = new List<string>();
																							list18.Add("Dogs");
																							list18.Add("Cannibals");
																							list18.Add("Dromad");
																							list18.Add("Girsh");
																							List<string> list19 = new List<string>();
																							for (int num56 = 0; num56 < list18.Count; num56++)
																							{
																								list19.Add(Faction.GetFormattedName(list18[num56]));
																							}
																							array18[0] = "Share the burden across all allies. [-&C50&y reputation with each attending faction]";
																							array18[1] = "Share the burden between two allies. [-&C100&y reputation with two attending factions of your choice]";
																							array18[2] = "Spare one faction of all obligation by betraying a second faction and selling their secrets to Asphodel. [-&C800&y with the betrayed faction, +&C200&y reputation with the spared faction + a faction heirloom]";
																							array18[3] = "Invoke the Chaos Spiel. [????????, +&C300&y reputation with &Chighly &Centropic &Cbeings&y]";
																							array18[4] = "Take time to weigh the options.";
																							char[] array19 = new char[] { 'a', 'b', 'c', 'd', 'e' };
																							string text38 = "";
																							for (int num57 = 0; num57 < list18.Count; num57++)
																							{
																								text38 = text38 + list18[num57] + ", ";
																							}
																							Popup.PickOption("", "The First Council of Omonporch has begun. Choose how to appease Asphodel.", "", "Sounds/UI/ui_notification", array18, array19, null, null, null, null, null, 1, 75, 0, -1, false, false, false, true, false, null, null);
																							return;
																						}
																						if (Wish == "bookfuck")
																						{
																							for (int num58 = 0; num58 < 10; num58++)
																							{
																								GameObject gameObject16 = GameObjectFactory.Factory.CreateObject("StandaloneMarkovBook");
																								StringBuilder stringBuilder17 = new StringBuilder();
																								gameObject16.GetPart<MarkovBook>().GeneratePages();
																								stringBuilder17.Append("{\\rtf1\\ansi\r\n\r\n    \\pgbrdrt\r\n    \\brdrart1\r\n    \\pgbrdrb\r\n    \\brdrart1\r\n    \\pgbrdrl\r\n    \\brdrart1\r\n    \\pgbrdrr\r\n    \\brdrart1\r\n\r\n    {\\fonttbl\\f0\\froman Georgia;}\\f0\\pard");
																								stringBuilder17.Append("{\\pard\\qc\\fs36 ");
																								stringBuilder17.AppendLine(gameObject16.DisplayNameOnlyStripped);
																								stringBuilder17.Append(" \\par}");
																								stringBuilder17.AppendLine();
																								stringBuilder17.AppendLine("\\par");
																								foreach (BookPage bookPage in gameObject16.GetPart<MarkovBook>().Pages)
																								{
																									stringBuilder17.AppendLine(bookPage.FullText.Replace("\n", " ").Replace("\r", ""));
																									stringBuilder17.AppendLine("\\par\\fs24");
																								}
																								stringBuilder17.Append("{\\footer\\pard\\qc\\fs18 Using Predictive Text to Generate Lore in {\\pard\\qc\\fs18\\i Caves of Qud} \\par}");
																								stringBuilder17.Append("}");
																								File.WriteAllText(DataManager.SavePath("book_" + num58.ToString() + ".rtf"), stringBuilder17.ToString());
																							}
																							return;
																						}
																						if (Wish == "bookfuckonefile")
																						{
																							StringBuilder stringBuilder18 = new StringBuilder();
																							stringBuilder18.Append("{\\rtf1\\ansi\r\n\r\n    \\pgbrdrt\r\n    \\brdrart1\r\n    \\pgbrdrb\r\n    \\brdrart1\r\n    \\pgbrdrl\r\n    \\brdrart1\r\n    \\pgbrdrr\r\n    \\brdrart1\r\n\r\n    {\\fonttbl\\f0\\froman Georgia;}\\f0\\pard");
																							for (int num59 = 0; num59 < 300; num59++)
																							{
																								GameObject gameObject17 = GameObjectFactory.Factory.CreateObject("StandaloneMarkovBook");
																								gameObject17.GetPart<MarkovBook>().GeneratePages();
																								stringBuilder18.Append("{\\pard\\qc\\fs36 ");
																								stringBuilder18.AppendLine(gameObject17.DisplayNameOnlyStripped);
																								stringBuilder18.Append(" \\par}");
																								stringBuilder18.AppendLine();
																								stringBuilder18.AppendLine("\\par");
																								foreach (BookPage bookPage2 in gameObject17.GetPart<MarkovBook>().Pages)
																								{
																									stringBuilder18.AppendLine(bookPage2.FullText.Replace("\n", " ").Replace("\r", ""));
																									stringBuilder18.AppendLine("\\par\\fs24");
																								}
																								stringBuilder18.Append("{\\footer\\pard\\qc\\fs18 Using Predictive Text to Generate Lore in {\\pard\\qc\\fs18\\i Caves of Qud} \\par}");
																								stringBuilder18.Append("\\pard \\insrsid \\page \\par");
																							}
																							stringBuilder18.Append("}");
																							File.WriteAllText(DataManager.SavePath("bigbook.rtf"), stringBuilder18.ToString());
																							return;
																						}
																						if (Wish == "clearfactionmembership")
																						{
																							who.Brain.Allegiance.Clear();
																							MessageQueue.AddPlayerMessage("Cleared faction membership", null, true);
																							return;
																						}
																						if (Wish == "allbox")
																						{
																							GameObject gameObject18 = GameObjectFactory.Factory.CreateObject("Chest");
																							Inventory inventory = gameObject18.Inventory;
																							foreach (string text39 in GameObjectFactory.Factory.Blueprints.Keys)
																							{
																								if (GameObjectFactory.Factory.Blueprints[text39].InheritsFrom("Item"))
																								{
																									inventory.AddObject(GameObjectFactory.Factory.CreateObject(text39), null, false, false, true, null);
																								}
																							}
																							who.GetCurrentCell().GetCellFromDirection("N", true).AddObject(gameObject18, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
																							return;
																						}
																						if (Wish == "fungone")
																						{
																							who.RemoveEffect<FungalSporeInfection>();
																							who.RemoveEffect<SporeCloudPoison>();
																							return;
																						}
																						if (Wish == "test437")
																						{
																							StringBuilder stringBuilder19 = new StringBuilder();
																							char c2 = '\0';
																							for (int num60 = 0; num60 < 16; num60++)
																							{
																								for (int num61 = 0; num61 < 16; num61++)
																								{
																									stringBuilder19.Append(c2);
																									c2 += '\u0001';
																								}
																								stringBuilder19.AppendLine();
																							}
																							Popup.Show(stringBuilder19.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
																							return;
																						}
																						if (Wish == "spend")
																						{
																							who.RandomlySpendPoints(int.MaxValue, int.MaxValue, int.MaxValue, null);
																							return;
																						}
																						if (Wish == "playerlevelmob")
																						{
																							who.GetCurrentCell().GetCellFromDirection("NW", true).AddObject(EncountersAPI.GetCreatureAroundPlayerLevel(null), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
																							return;
																						}
																						if (Wish == "auditblueprints")
																						{
																							using (List<GameObjectBlueprint>.Enumerator enumerator26 = GameObjectFactory.Factory.BlueprintList.GetEnumerator())
																							{
																								while (enumerator26.MoveNext())
																								{
																									GameObjectBlueprint gameObjectBlueprint3 = enumerator26.Current;
																									if (gameObjectBlueprint3.HasTag("BaseObject", false) && gameObjectBlueprint3.HasTag("ExcludeFromDynamicEncounters", false) && gameObjectBlueprint3.GetTag("ExcludeFromDynamicEncounters", "") == "*noinherit")
																									{
																										Popup.Show(gameObjectBlueprint3.Name + " has BaseObject and ExcludeFromDynamicEncounters with *noinherit", null, "Sounds/UI/ui_notification", true, true, true, true, null);
																									}
																									string text40 = gameObjectBlueprint3.DisplayName();
																									if (text40 != null && (text40.StartsWith("[") || text40.EndsWith("]")) && !gameObjectBlueprint3.HasTag("BaseObject", false) && !gameObjectBlueprint3.HasTag("ExcludeFromDynamicEncounters", false))
																									{
																										Popup.Show(gameObjectBlueprint3.Name + " has display name " + text40 + " and neither BaseObject nor ExcludeFromDynamicEncounters", null, "Sounds/UI/ui_notification", true, true, true, true, null);
																									}
																								}
																								return;
																							}
																						}
																						if (Wish == "auditrenderlayers")
																						{
																							using (List<GameObjectBlueprint>.Enumerator enumerator26 = GameObjectFactory.Factory.BlueprintList.GetEnumerator())
																							{
																								while (enumerator26.MoveNext())
																								{
																									GameObjectBlueprint gameObjectBlueprint4 = enumerator26.Current;
																									if (gameObjectBlueprint4.GetPartParameter<bool>("Physics", "Solid", false) && !gameObjectBlueprint4.InheritsFrom("Terrain"))
																									{
																										int partParameter = gameObjectBlueprint4.GetPartParameter<int>("Render", "RenderLayer", -1);
																										if (partParameter < 6)
																										{
																											if (partParameter == -1)
																											{
																												Popup.Show(gameObjectBlueprint4.Name + " is solid and has unparseable RenderLayer", null, "Sounds/UI/ui_notification", true, true, true, true, null);
																											}
																											else
																											{
																												Popup.Show(gameObjectBlueprint4.Name + " is solid and has RenderLayer " + partParameter.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
																											}
																										}
																									}
																								}
																								return;
																							}
																						}
																						if (Wish == "findfarmers")
																						{
																							string text41 = "";
																							foreach (GameObjectBlueprint gameObjectBlueprint5 in GameObjectFactory.Factory.BlueprintList)
																							{
																								if (!gameObjectBlueprint5.Tags.ContainsKey("BaseObject"))
																								{
																									GamePartBlueprint part4 = gameObjectBlueprint5.GetPart("Render");
																									string text42;
																									if (part4 != null && part4.TryGetParameter<string>("Tile", out text42) && text42 == "Assets_Content_Textures_Creatures_sw_farmer.bmp")
																									{
																										text41 = text41 + gameObjectBlueprint5.Name + "\n";
																									}
																								}
																							}
																							Popup.Show(text41, null, "Sounds/UI/ui_notification", true, true, true, true, null);
																							return;
																						}
																						if (Wish.StartsWith("dismember:"))
																						{
																							string text43 = Wish.Split(':', StringSplitOptions.None)[1];
																							Body body2 = who.Body;
																							BodyPart bodyPart2;
																							if ((bodyPart2 = body2.GetPartByName(text43)) == null && (bodyPart2 = body2.GetPartByDescription(text43)) == null)
																							{
																								bodyPart2 = body2.GetPartByName(text43.ToLower()) ?? body2.GetPartByDescription(Grammar.MakeTitleCase(text43));
																							}
																							BodyPart bodyPart3 = bodyPart2;
																							if (bodyPart3 == null)
																							{
																								Popup.Show("Could not find body part by name or description: " + text43, null, "Sounds/UI/ui_notification", true, true, true, true, null);
																								return;
																							}
																							if (bodyPart3.IsSeverable())
																							{
																								Axe_Dismember.Dismember(who, who, null, bodyPart3, null, null, "sfx_characterTrigger_dismember", true, false, false, true);
																								return;
																							}
																							if (bodyPart3.Integral)
																							{
																								Popup.Show(string.Concat(new string[]
																								{
																									"Your ",
																									bodyPart3.Name,
																									" ",
																									bodyPart3.Plural ? "are" : "is",
																									" an integral part of your body and ",
																									bodyPart3.Plural ? "are" : "is",
																									" not dismemberable."
																								}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
																								return;
																							}
																							if (bodyPart3.DependsOn != null || bodyPart3.RequiresType != null)
																							{
																								Popup.Show(string.Concat(new string[]
																								{
																									"Your ",
																									bodyPart3.Name,
																									" ",
																									bodyPart3.Plural ? "are" : "is",
																									" not directly dismemberable, instead being lost when other body parts are dismembered."
																								}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
																								return;
																							}
																							Popup.Show(string.Concat(new string[]
																							{
																								"Your ",
																								bodyPart3.Name,
																								" ",
																								bodyPart3.Plural ? "are" : "is",
																								" not dismemberable."
																							}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
																							return;
																						}
																						else if (Wish == "clearach!!!")
																						{
																							if (Popup.ShowYesNo("Are you sure you want to RESET ALL YOUR ACHIEVEMENTS?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes)
																							{
																								AchievementManager.Reset();
																								return;
																							}
																						}
																						else if (Wish.StartsWith("regeneratedefaultequipment"))
																						{
																							GameObject who2 = who;
																							if (who2 == null)
																							{
																								return;
																							}
																							Body body3 = who2.Body;
																							if (body3 == null)
																							{
																								return;
																							}
																							body3.RegenerateDefaultEquipment();
																							return;
																						}
																						else
																						{
																							if (Wish.StartsWith("cooktestunits:"))
																							{
																								GameObject who3 = who;
																								ProceduralCookingEffect proceduralCookingEffect = ProceduralCookingEffect.CreateJustUnits(new List<string>(Wish.Split(':', StringSplitOptions.None)[1].Split(',', StringSplitOptions.None)));
																								proceduralCookingEffect.Init(who3);
																								who3.ApplyEffect(proceduralCookingEffect, null);
																								return;
																							}
																							if (Wish.StartsWith("cooktestfull:"))
																							{
																								GameObject who4 = who;
																								string[] array20 = Wish.Split(':', StringSplitOptions.None)[1].Split(',', StringSplitOptions.None);
																								ProceduralCookingEffectWithTrigger proceduralCookingEffectWithTrigger = ProceduralCookingEffect.CreateBaseAndTriggeredAction(array20[0], array20[1], array20[2]);
																								proceduralCookingEffectWithTrigger.Init(who4);
																								who4.ApplyEffect(proceduralCookingEffectWithTrigger, null);
																								return;
																							}
																							if (Wish == "purgeobjectcache!")
																							{
																								The.ZoneManager.CachedObjects.Clear();
																								MessageQueue.AddPlayerMessage("Purged object cache.", null, true);
																								return;
																							}
																							if (Wish.StartsWith("unequip:"))
																							{
																								string text44 = Wish.Split(':', StringSplitOptions.None)[1];
																								GameObject who5 = who;
																								GameObject equipped = who5.Body.GetPartByName(text44).Equipped;
																								who5.Body.GetPartByName(text44)._Equipped = null;
																								who5.Inventory.AddObject(equipped, null, false, false, true, null);
																								return;
																							}
																							if (Wish == "objtest")
																							{
																								GameObject gameObject19 = null;
																								Stopwatch stopwatch = Stopwatch.StartNew();
																								for (int num62 = 0; num62 < 100; num62++)
																								{
																									gameObject19 = GameObjectFactory.Factory.CreateObject("Tam");
																								}
																								stopwatch.Stop();
																								Stopwatch stopwatch2 = Stopwatch.StartNew();
																								for (int num63 = 0; num63 < 100; num63++)
																								{
																									gameObject19 = gameObject19.DeepCopy(false, false, null);
																								}
																								stopwatch2.Stop();
																								global::UnityEngine.Debug.Log("MS create: " + stopwatch.Elapsed.ToString());
																								global::UnityEngine.Debug.Log("MS clone: " + stopwatch2.Elapsed.ToString());
																								return;
																							}
																							if (Wish == "memcheck")
																							{
																								MessageQueue.AddPlayerMessage("Before Total Memory: " + GC.GetTotalMemory(false).ToString(), null, true);
																								MessageQueue.AddPlayerMessage("After Total Memory: " + GC.GetTotalMemory(true).ToString(), null, true);
																								return;
																							}
																							if (Wish == "makevillage")
																							{
																								Cell currentCell5 = who.Physics.CurrentCell;
																								Zone parentZone2 = who.Physics.CurrentCell.ParentZone;
																								who.Physics.CurrentCell.RemoveObject(who, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
																								try
																								{
																									Village village = new Village();
																									History sultanHistory4 = The.Game.sultanHistory;
																									sultanHistory4.currentYear = (long)(1000 + Stat.Random(400, 900));
																									HistoricEntity historicEntity3 = sultanHistory4.CreateEntity(sultanHistory4.currentYear);
																									historicEntity3.ApplyEvent(new InitializeVillage(new string[]
																									{
																										"BananaGrove", "BaroqueRuins", "DesertCanyon", "Flowerfields", "Hills", "Jungle", "LakeHinnom", "MoonStair", "Mountains", "PalladiumReef",
																										"Ruins", "Saltdunes", "Saltmarsh", "Water"
																									}.GetRandomElement<string>(null), null, null, null, null, false), long.MinValue);
																									village.villageEntity = historicEntity3;
																									village.BuildZone(parentZone2);
																								}
																								catch (Exception ex2)
																								{
																									global::UnityEngine.Debug.LogException(ex2);
																								}
																								currentCell5.AddObject(who, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
																								return;
																							}
																							if (Wish == "villageprops")
																							{
																								MessageQueue.AddPlayerMessage("Listing villageEntity if one exists...", null, true);
																								using (IEnumerator<ZoneBuilderBlueprint> enumerator18 = The.ZoneManager.GetBuildersFor(who.GetCurrentCell().ParentZone).GetEnumerator())
																								{
																									while (enumerator18.MoveNext())
																									{
																										ZoneBuilderBlueprint zoneBuilderBlueprint2 = enumerator18.Current;
																										if (zoneBuilderBlueprint2.Class == "Village")
																										{
																											HistoricEntitySnapshot currentSnapshot5 = (zoneBuilderBlueprint2.Parameters["villageEntity"] as HistoricEntity).GetCurrentSnapshot();
																											MessageQueue.AddPlayerMessage(currentSnapshot5.ToString(), null, true);
																											global::UnityEngine.Debug.Log(currentSnapshot5.ToString());
																										}
																									}
																									return;
																								}
																							}
																							if (Wish.StartsWith("listtags:"))
																							{
																								string text45 = Wish.Split(':', StringSplitOptions.None)[1];
																								MessageQueue.AddPlayerMessage("Listing the tags of " + text45 + "...", null, true);
																								using (Dictionary<string, string>.Enumerator enumerator27 = GameObjectFactory.Factory.Blueprints[text45].Tags.GetEnumerator())
																								{
																									while (enumerator27.MoveNext())
																									{
																										KeyValuePair<string, string> keyValuePair4 = enumerator27.Current;
																										MessageQueue.AddPlayerMessage(keyValuePair4.Key + " = " + keyValuePair4.Value, null, true);
																									}
																									return;
																								}
																							}
																							if (Wish.StartsWith("goto:"))
																							{
																								string text46 = Wish.Split(':', StringSplitOptions.None)[1];
																								Zone zone5 = The.ZoneManager.GetZone(text46);
																								Point2D pos2D2 = who.Physics.CurrentCell.Pos2D;
																								who.Physics.CurrentCell.RemoveObject(who.Physics.ParentObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
																								zone5.GetCell(pos2D2).AddObject(who, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
																								The.ZoneManager.SetActiveZone(zone5);
																								The.ZoneManager.ProcessGoToPartyLeader();
																								return;
																							}
																							if (Wish.StartsWith("revealsecret:"))
																							{
																								JournalMapNote mapNote = JournalAPI.GetMapNote(Wish.Split(':', StringSplitOptions.None)[1]);
																								if (mapNote != null && !mapNote.Revealed)
																								{
																									JournalAPI.RevealMapNote(mapNote, false, null);
																									return;
																								}
																							}
																							else
																							{
																								if (Wish == "sheeter")
																								{
																									global::Sheeter.Sheeter.MonsterSheeter();
																									return;
																								}
																								if (Wish == "factionsheeter")
																								{
																									global::Sheeter.Sheeter.FactionSheeter();
																									return;
																								}
																								if (Wish.StartsWith("removepart:"))
																								{
																									string text47 = Wish.Split(':', StringSplitOptions.None)[1];
																									who.RemovePart(text47);
																									MessageQueue.AddPlayerMessage("Removed part " + text47 + " from player body.", null, true);
																									return;
																								}
																								if (Wish.StartsWith("spawn:"))
																								{
																									string[] array21 = Wish.Split(':', StringSplitOptions.None);
																									for (int num64 = 0; num64 < 16; num64++)
																									{
																										The.Game.ActionManager.AddActiveObject(who.CurrentCell.ParentZone.GetCells((Cell c) => !c.Explored).ShuffleInPlace<Cell>(null).GetRandomElement<Cell>(null)
																											.AddObject(array21[1], null, null, null, null));
																									}
																									return;
																								}
																								if (Wish.StartsWith("othowander1"))
																								{
																									OthoWander1.begin();
																									return;
																								}
																								if (Wish == "reshephgospel")
																								{
																									JournalAPI.GetNotesForResheph().ForEach(delegate(JournalSultanNote g)
																									{
																										g.Reveal(null, false);
																									});
																									return;
																								}
																								if (Wish == "markofdeath")
																								{
																									The.Player.ToggleMarkOfDeath();
																									MessageQueue.AddPlayerMessage("Mark of death now " + The.Player.HasMarkOfDeath(null).ToString(), null, true);
																									return;
																								}
																								if (Wish == "markofdeath?")
																								{
																									MessageQueue.AddPlayerMessage("Mark of death is: " + The.Game.GetStringGameState("MarkOfDeath", ""), null, true);
																									if (The.Player.HasMarkOfDeath(null))
																									{
																										MessageQueue.AddPlayerMessage("HAS mark of death", null, true);
																										return;
																									}
																									MessageQueue.AddPlayerMessage("DOES NOT HAVE mark of death", null, true);
																									return;
																								}
																								else
																								{
																									if (Wish == "goclam")
																									{
																										Cell randomElement = The.Game.RequireSystem<ClamSystem>(() => new ClamSystem()).GetClamZone().GetCells()
																											.GetRandomElement<Cell>(null);
																										The.Player.SetLongProperty("ClamTeleportTurn", The.Game.Turns);
																										The.Player.TeleportTo(randomElement, new int?(0), true, false, false, false, "disappear", "appear");
																										return;
																									}
																									if (Wish == "hydropon")
																									{
																										The.Player.ZoneTeleport(The.Game.GetStringGameState("HydroponZoneID", ""), -1, -1, null, null, null, null, "You are transported!", true, null);
																										return;
																									}
																									if (Wish == "hollowtree")
																									{
																										The.Player.ZoneTeleport(The.Game.GetStringGameState("HollowTreeZoneId", ""), -1, -1, null, null, null, null, "You are transported!", true, null);
																										return;
																									}
																									if (Wish == "gritgate")
																									{
																										The.Player.ZoneTeleport("JoppaWorld.22.14.1.0.13", -1, -1, null, null, null, null, "You are transported!", true, null);
																										GritGateScripts.OpenRank0Doors();
																										return;
																									}
																									if (Wish == "thinworld")
																									{
																										GameObject player = The.Player;
																										player.GetCurrentCell().GetCellFromDirection("NW", true).AddObject("SultanSarcophagusWPeriod6", null, null, null, null);
																										ThinWorld.TransitToThinWorld(player.GetCurrentCell().GetCellFromDirection("N", true).AddObject("SultanSarcophagusEPeriod6", null, null, null, null), false);
																										return;
																									}
																									if (Wish == "thinworldx")
																									{
																										GameObject player2 = The.Player;
																										player2.GetCurrentCell().GetCellFromDirection("NW", true).AddObject("SultanSarcophagusWPeriod6", null, null, null, null);
																										ThinWorld.TransitToThinWorld(player2.GetCurrentCell().GetCellFromDirection("N", true).AddObject("SultanSarcophagusEPeriod6", null, null, null, null), true);
																										return;
																									}
																									if (Wish == "returntoqud")
																									{
																										ThinWorld.ReturnToQud();
																										return;
																									}
																									if (Wish == "somethinggoeswrong")
																									{
																										ThinWorld.SomethingGoesWrong(The.Player);
																										return;
																									}
																									if (Wish == "sultanmuralwalltest")
																									{
																										using (List<Cell>.Enumerator enumerator5 = The.Player.Physics.CurrentCell.ParentZone.GetCells().GetEnumerator())
																										{
																											while (enumerator5.MoveNext())
																											{
																												Cell cell11 = enumerator5.Current;
																												if (cell11.Y % 2 == 0)
																												{
																													cell11.ClearWalls();
																													cell11.AddObject("SultanMuralWall", null, null, null, null);
																												}
																											}
																											return;
																										}
																									}
																									if (Wish == "sultanmuralwalltest2")
																									{
																										Zone parentZone3 = The.Player.Physics.CurrentCell.ParentZone;
																										for (int num65 = 0; num65 < 12; num65++)
																										{
																											Cell cell12 = parentZone3.GetCell(num65, 0);
																											cell12.ClearWalls();
																											cell12.AddObject("SultanMuralWall", null, null, null, null);
																										}
																										return;
																									}
																									if (Wish == "zonetier")
																									{
																										int num7 = The.Player.CurrentCell.ParentZone.NewTier;
																										MessageQueue.AddPlayerMessage(num7.ToString(), null, true);
																										return;
																									}
																									if (Wish == "exception")
																									{
																										int num66 = 0;
																										int num67 = 10 / num66;
																										return;
																									}
																									if (Wish == "iamconfused")
																									{
																										XRLCore.player.ApplyEffect(new XRL.World.Effects.Confused(80, 10, 0, "Confusion"), null);
																										return;
																									}
																									if (Wish == "clearprefs")
																									{
																										GameManager.Instance.uiQueue.queueTask(delegate
																										{
																											PlayerPrefs.DeleteAll();
																										}, 0);
																										MessageQueue.AddPlayerMessage("Player prefs cleared", null, true);
																										return;
																									}
																									if (Wish.StartsWith("placeobjecttest:"))
																									{
																										string[] array22 = Wish.Split(':', StringSplitOptions.None);
																										if (GameObjectFactory.Factory.CreateObject(array22[1]) == null)
																										{
																											return;
																										}
																										for (int num68 = 0; num68 < 200; num68++)
																										{
																											ZoneBuilderSandbox.PlaceObject(array22[1], The.Player.CurrentZone);
																										}
																										return;
																									}
																									else
																									{
																										if (Wish == "allloved")
																										{
																											foreach (Faction faction3 in Factions.Loop())
																											{
																												The.Game.PlayerReputation.Set(faction3, 2000);
																											}
																											MessageQueue.AddPlayerMessage("Factionrep set to all loved.", null, true);
																											return;
																										}
																										if (Wish == "allhated")
																										{
																											foreach (Faction faction4 in Factions.Loop())
																											{
																												The.Game.PlayerReputation.Set(faction4, -2000);
																											}
																											MessageQueue.AddPlayerMessage("Factionrep set to all hated.", null, true);
																											return;
																										}
																										if (Wish == "cloacasurprise")
																										{
																											new CloacaSurprise().ApplyEffectsTo(The.Player, true);
																											return;
																										}
																										if (Wish == "cryotube")
																										{
																											new PossibleCryotube
																											{
																												Chance = 10000
																											}.BuildZone(The.Player.CurrentZone);
																											return;
																										}
																										if (Wish.StartsWith("redrock:"))
																										{
																											string text48 = "JoppaWorld.11.20.1.1." + Wish.Split(':', StringSplitOptions.None)[1];
																											The.Player.SystemMoveTo(The.ZoneManager.GetZone(text48).GetCell(40, 24), null, false, true, false, false);
																											return;
																										}
																										if (Wish == "ensurevoids")
																										{
																											ZoneBuilderSandbox.EnsureAllVoidsConnected(The.Player.CurrentZone, false, 0.2f, null);
																											return;
																										}
																										if (Wish == "ydfreehold")
																										{
																											string text49 = "JoppaWorld.67.17.1.1.10";
																											The.Player.SystemMoveTo(The.ZoneManager.GetZone(text49).GetCell(40, 23), null, false, true, false, false);
																											return;
																										}
																										if (Wish == "eyn")
																										{
																											string text50 = "JoppaWorld.76.5.1.1.10";
																											The.Player.SystemMoveTo(The.ZoneManager.GetZone(text50).GetCell(40, 14), null, false, true, false, false);
																											return;
																										}
																										if (Wish == "moonstair")
																										{
																											string text51 = "JoppaWorld.75.13.1.1.10";
																											The.Player.SystemMoveTo(The.ZoneManager.GetZone(text51).GetCell(40, 24), null, false, true, false, false);
																											return;
																										}
																										if (Wish == "moonstair2")
																										{
																											string text52 = "JoppaWorld.76.5.1.1.11";
																											The.Player.SystemMoveTo(The.ZoneManager.GetZone(text52).GetCell(31, 9), null, false, true, false, false);
																											return;
																										}
																										if (Wish == "delaytest")
																										{
																											The.Player.PlayWorldSound("Sounds/Grenade/sfx_grenade_flashbang_explode", 0.5f, 0f, false, 4f, 1f, 1f, int.MaxValue);
																											CombatJuice.playWorldSound(The.Player, "Sounds/Grenade/sfx_grenade_flashbang_explode", 0.5f, 0f, 0f, 3f);
																											return;
																										}
																										if (Wish == "classreport")
																										{
																											HashSet<string> hashSet = new HashSet<string>();
																											string text53 = "";
																											foreach (GameObjectBlueprint gameObjectBlueprint6 in GameObjectFactory.Factory.BlueprintList)
																											{
																												gameObjectBlueprint6.Name == "BaseReptile";
																												string text54 = gameObjectBlueprint6.GetTag("Class", "").ToLower();
																												string text55 = gameObjectBlueprint6.GetTag("Species", "").ToLower();
																												if (!string.IsNullOrEmpty(text54) && !hashSet.Contains(text54))
																												{
																													text53 = text53 + ", " + text54;
																													hashSet.Add(text54);
																												}
																												if (!string.IsNullOrEmpty(text55) && !hashSet.Contains(text55))
																												{
																													text53 = text53 + ", " + text55;
																													hashSet.Add(text55);
																												}
																											}
																											global::UnityEngine.Debug.Log(text53);
																											return;
																										}
																										if (Wish == "findimportant")
																										{
																											foreach (GameObject gameObject20 in The.ZoneManager.ActiveZone.FindObjects((GameObject o) => o.IsImportant()).ToList<GameObject>())
																											{
																												MessageQueue.AddPlayerMessage(gameObject20.DisplayName + " at " + gameObject20.CurrentCell.ToString(), null, true);
																											}
																											using (List<GameObject>.Enumerator enumerator4 = The.ZoneManager.ActiveZone.FindObjects((GameObject o) => o.HasObjectInInventory((GameObject o) => o.IsImportant(), 1)).GetEnumerator())
																											{
																												while (enumerator4.MoveNext())
																												{
																													enumerator4.Current.ForeachInventoryAndEquipment(delegate(GameObject obj)
																													{
																														if (obj.IsImportant())
																														{
																															MessageQueue.AddPlayerMessage(obj.DisplayName + " at " + obj.CurrentCell.ToString(), null, true);
																														}
																													});
																												}
																												return;
																											}
																										}
																										if (Wish == "fetchimportant")
																										{
																											List<GameObject> gos = new List<GameObject>();
																											foreach (GameObject gameObject21 in The.ZoneManager.ActiveZone.FindObjects((GameObject o) => o.IsImportant()).ToList<GameObject>())
																											{
																												gos.Add(gameObject21);
																											}
																											Action<GameObject> <>9__34;
																											foreach (GameObject gameObject22 in The.ZoneManager.ActiveZone.FindObjects((GameObject o) => o.HasObjectInInventory((GameObject o) => o.IsImportant(), 1)))
																											{
																												Action<GameObject> action2;
																												if ((action2 = <>9__34) == null)
																												{
																													action2 = (<>9__34 = delegate(GameObject obj)
																													{
																														if (obj.IsImportant())
																														{
																															gos.Add(obj);
																														}
																													});
																												}
																												gameObject22.ForeachInventoryAndEquipment(action2);
																											}
																											gos.ForEach(delegate(GameObject go)
																											{
																												go.DirectMoveTo(The.Player.CurrentCell, 0, false, false, false, null);
																											});
																											return;
																										}
																										if (Wish == "sfxtest")
																										{
																											SoundManager.PlaySound("Sounds/Missile/Fires/Rifles/sfx_missile_laserRifle_fire", 0f, 1f, 1f, SoundRequest.SoundEffectType.FullPanRightToLeft, 0f);
																											return;
																										}
																										if (Wish.StartsWith("disablelayer:"))
																										{
																											ControlManager.DisableLayer(Wish.Split(':', StringSplitOptions.None)[1]);
																											return;
																										}
																										if (Wish.StartsWith("enablelayer:"))
																										{
																											ControlManager.EnableLayer(Wish.Split(':', StringSplitOptions.None)[1]);
																											return;
																										}
																										if (Wish == "testpax")
																										{
																											PaxInfectLimb.Infect(The.Player);
																											SpreadPax.StartQuest();
																											return;
																										}
																										if (Wish == "everycachedquestitem")
																										{
																											using (Dictionary<string, GameObject>.Enumerator enumerator28 = The.Game.ZoneManager.CachedObjects.GetEnumerator())
																											{
																												while (enumerator28.MoveNext())
																												{
																													KeyValuePair<string, GameObject> keyValuePair5 = enumerator28.Current;
																													GameObject value = keyValuePair5.Value;
																													if (value.IsImportant())
																													{
																														The.Player.ReceiveObject(value, false, null);
																													}
																												}
																												return;
																											}
																										}
																										if (Wish.StartsWith("rapid:"))
																										{
																											Leveler.RapidAdvancement(Convert.ToInt32(Wish.Split(':', StringSplitOptions.None)[1]), The.Player);
																											return;
																										}
																										if (Wish == "unexplore")
																										{
																											The.Player.CurrentZone.UnexploreAll();
																											The.Player.CurrentZone.GetObjects().ForEach(delegate(GameObject o)
																											{
																												o.RemoveProperty("Autoexplored");
																											});
																											return;
																										}
																										if (Wish.StartsWith("deathpopuptest:"))
																										{
																											string[] array23 = Wish.Split(":", StringSplitOptions.None);
																											string text56 = array23[1];
																											List<QudMenuItem> list20 = null;
																											Action<QudMenuItem> action3 = null;
																											List<QudMenuItem> list21 = null;
																											string text57 = null;
																											string text58 = null;
																											int num69 = 0;
																											IRenderable renderable = new Renderable(null, array23[2], "f", "&W", array23[3], new char?(array23[4][0]));
																											Popup.WaitNewPopupMessage(text56, list20, action3, list21, text57, text58, num69, array23[5], null, renderable, false, true, null, null, false);
																											return;
																										}
																										if (Wish == "deathpopuptest")
																										{
																											Popup.WaitNewPopupMessage("You were immolated by a dawnglider.", null, null, null, null, null, 0, "You died.", null, new Renderable(null, "Mutations/flaming_ray.bmp", "f", "&W", "&W", new char?('R')), false, true, null, null, false);
																											return;
																										}
																										if (Wish.StartsWith("deathtestcategory:"))
																										{
																											using (List<GameObject>.Enumerator enumerator4 = The.Player.CurrentZone.GetObjects().GetEnumerator())
																											{
																												while (enumerator4.MoveNext())
																												{
																													GameObject gameObject23 = enumerator4.Current;
																													if (gameObject23.IsCombatObject() && !gameObject23.IsPlayer())
																													{
																														gameObject23.Die(null, null, null, null, false, null, null, false, false, null, null, Wish.Split(':', StringSplitOptions.None)[1]);
																													}
																												}
																												return;
																											}
																										}
																										if (Wish == "deathtest:decapitated")
																										{
																											using (List<GameObject>.Enumerator enumerator4 = The.Player.CurrentZone.GetObjects().GetEnumerator())
																											{
																												while (enumerator4.MoveNext())
																												{
																													GameObject gameObject24 = enumerator4.Current;
																													if (gameObject24.IsCombatObject() && !gameObject24.IsPlayer())
																													{
																														string tile = gameObject24.Render.Tile;
																														string tileForegroundColor = gameObject24.Render.GetTileForegroundColor();
																														char detailColor = gameObject24.Render.getDetailColor();
																														Cell currentCell6 = gameObject24.CurrentCell;
																														gameObject24.Die(null, null, null, null, false, null, null, false, false, null, null, null);
																														CombatJuice.playPrefabAnimation(currentCell6.Location, "Deaths/DeathVFXDecapitated", null, string.Concat(new string[]
																														{
																															tile,
																															";",
																															tileForegroundColor,
																															";",
																															detailColor.ToString()
																														}), null, false);
																													}
																												}
																												return;
																											}
																										}
																										if (Wish == "deathtestlased")
																										{
																											Popup.WaitNewPopupMessage("You were lased to death by a crypt sitter.", null, null, null, null, null, 0, "You died.", null, new Renderable(null, "Mutations/light_manipulation.bmp", "f", "&R", "&R", new char?('Y')), false, true, null, null, false);
																											return;
																										}
																										if (Wish == "deathtest:gem")
																										{
																											List<GameObject> list22 = (from o in The.Player.CurrentZone.GetObjects()
																												where o.IsCombatObject() && !o.IsPlayer()
																												select o).ToList<GameObject>();
																											list22.Sort((GameObject a, GameObject b) => a.DistanceTo(The.Player) - b.DistanceTo(The.Player));
																											using (List<GameObject>.Enumerator enumerator4 = list22.GetEnumerator())
																											{
																												if (!enumerator4.MoveNext())
																												{
																													return;
																												}
																												GameObject gameObject25 = enumerator4.Current;
																												string tile2 = gameObject25.Render.Tile;
																												string tileForegroundColor2 = gameObject25.Render.GetTileForegroundColor();
																												char detailColor2 = gameObject25.Render.getDetailColor();
																												Cell currentCell7 = gameObject25.CurrentCell;
																												gameObject25.Die(null, null, null, null, false, null, null, false, false, null, null, null);
																												GameObject gameObject26 = GameObject.Create(PopulationManager.RollOneFrom("Gemstones", null, null).Blueprint);
																												currentCell7.AddObject(gameObject26, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
																												CombatJuice.playPrefabAnimation(gameObject26, "Deaths/DeathVFXTransmuted", gameObject26.ID, string.Concat(new string[]
																												{
																													tile2,
																													";",
																													tileForegroundColor2,
																													";",
																													detailColor2.ToString(),
																													";",
																													gameObject26.Render.Tile,
																													";",
																													gameObject26.Render.GetTileForegroundColor(),
																													";",
																													gameObject26.Render.getDetailColor().ToString()
																												}), null, false);
																												return;
																											}
																										}
																										if (Wish == "ignoreme")
																										{
																											The.Core.IgnoreMe = !The.Core.IgnoreMe;
																											MessageQueue.AddPlayerMessage("ignore now " + The.Core.IgnoreMe.ToString(), null, true);
																											return;
																										}
																										if (Wish.StartsWith("pan:"))
																										{
																											GameManager.Instance.uiQueue.queueTask(delegate
																											{
																												GameManager.Instance.cinematicPan(Wish.Split(':', StringSplitOptions.None)[1]);
																											}, 0);
																											return;
																										}
																										if (Wish.StartsWith("gosecret:"))
																										{
																											string secretID = Wish.Split(':', StringSplitOptions.None)[1];
																											string zoneID2 = ((from n in JournalAPI.GetAllNotes()
																												where n.ID == secretID
																												select n).First<IBaseJournalEntry>() as JournalMapNote).ZoneID;
																											The.Player.SystemLongDistanceMoveTo(The.ZoneManager.GetZone(zoneID2).GetCell(40, 24), null, false, true);
																											return;
																										}
																										if (Wish == "go:shuglair")
																										{
																											string zoneID3 = ((from n in JournalAPI.GetAllNotes()
																												where n.ID == "$shugruithlair"
																												select n).First<IBaseJournalEntry>() as JournalMapNote).ZoneID;
																											The.Player.SystemLongDistanceMoveTo(The.ZoneManager.GetZone(zoneID3).GetCell(40, 24), null, false, true);
																											return;
																										}
																										if (Wish == "go:shug")
																										{
																											string zoneID4 = ((from n in JournalAPI.GetAllNotes()
																												where n.ID == "$shugruithmouth"
																												select n).First<IBaseJournalEntry>() as JournalMapNote).ZoneID;
																											The.Player.SystemLongDistanceMoveTo(The.ZoneManager.GetZone(zoneID4).GetCell(40, 24), null, false, true);
																											return;
																										}
																										if (Wish == "go:agolgot")
																										{
																											string zoneID5 = JournalAPI.MapNotes.Find((JournalMapNote x) => x.ID == "$agolgotlair").ZoneID;
																											The.Player.SystemLongDistanceMoveTo(The.ZoneManager.GetZone(zoneID5).GetCell(40, 24), null, false, true);
																											return;
																										}
																										if (Wish == "go:bethsaida")
																										{
																											string zoneID6 = JournalAPI.MapNotes.Find((JournalMapNote x) => x.ID == "$bethsaidalair").ZoneID;
																											The.Player.SystemLongDistanceMoveTo(The.ZoneManager.GetZone(zoneID6).GetCell(40, 24), null, false, true);
																											return;
																										}
																										if (Wish == "go:rermadon")
																										{
																											string zoneID7 = JournalAPI.MapNotes.Find((JournalMapNote x) => x.ID == "$rermadonlair").ZoneID;
																											The.Player.SystemLongDistanceMoveTo(The.ZoneManager.GetZone(zoneID7).GetCell(40, 24), null, false, true);
																											return;
																										}
																										if (Wish == "go:qas" || Wish == "go:qon" || Wish == "go:qasqon")
																										{
																											string zoneID8 = JournalAPI.MapNotes.Find((JournalMapNote x) => x.ID == "$qasqonlair").ZoneID;
																											The.Player.SystemLongDistanceMoveTo(The.ZoneManager.GetZone(zoneID8).GetCell(40, 24), null, false, true);
																											return;
																										}
																										if (Wish == "techscan")
																										{
																											The.Player.ModIntProperty("TechScannerEquipped", 1, false);
																											return;
																										}
																										if (Wish == "bioscan")
																										{
																											The.Player.ModIntProperty("BioScannerEquipped", 1, false);
																											return;
																										}
																										if (Wish == "structurescan")
																										{
																											The.Player.ModIntProperty("StructureScannerEquipped", 1, false);
																											return;
																										}
																										if (Wish == "assassins")
																										{
																											PsychicHunterSystem.CreateExtradimensionalSoloHunters(The.Player.CurrentZone, 24, null, true, false, true, true);
																											return;
																										}
																										if (Wish == "testacc")
																										{
																											JournalAPI.AddAccomplishment("Test ACC", null, null, null, "general", MuralCategory.Generic, MuralWeight.Medium, null, -1L, true);
																											return;
																										}
																										if (Wish == "unloadunusedassets")
																										{
																											MessageQueue.AddPlayerMessage("Unloading unused assets...", null, true);
																											GameManager.Instance.uiQueue.queueTask(delegate
																											{
																												SoundManager.ClearCache();
																												Resources.UnloadUnusedAssets();
																											}, 0);
																											return;
																										}
																										if (Wish == "sbtest")
																										{
																											MessageQueue.AddPlayerMessage("Starting memtest", null, true);
																											for (int num70 = 0; num70 < 100000; num70++)
																											{
																												StringBuilder stringBuilder20 = new StringBuilder();
																												for (int num71 = 0; num71 < 1000; num71++)
																												{
																													stringBuilder20.Append("?");
																												}
																												stringBuilder20.ToString();
																												Wishing.memtest.Add(stringBuilder20);
																											}
																											MessageQueue.AddPlayerMessage("Ending memtest", null, true);
																											return;
																										}
																										if (Wish == "tut:joppa")
																										{
																											TutorialManager.AdvanceStep(new ExploreJoppa());
																											The.Player.SystemMoveTo(The.ZoneManager.GetZone("JoppaWorld.11.22.1.1.10").GetCell(37, 22), null, false, true, false, false);
																											return;
																										}
																										if (Wish == "tut:bear")
																										{
																											TutorialManager.AdvanceStep(new FightBear());
																											The.Player.CurrentCell.GetCellFromDirection("NE", true).GetCellFromDirection("NE", true).AddObject("TutorialBear", null, null, null, null);
																											return;
																										}
																										if (Wish == "tut:clear")
																										{
																											TutorialManager.AdvanceStep(null);
																											return;
																										}
																										if (Wish == "tut:camp")
																										{
																											TutorialManager.AdvanceStep(null);
																											The.Player.SystemMoveTo(The.ZoneManager.GetZone("JoppaWorld.11.24.1.0.10").GetCell(48, 11), null, false, true, false, false);
																											TutorialManager.AdvanceStep(new MakeCamp());
																											return;
																										}
																										if (Wish == "tut:worldmap")
																										{
																											TutorialManager.AdvanceStep(new ExploreWorldMap());
																											The.Player.SystemMoveTo(The.ZoneManager.GetZone("JoppaWorld").GetCell(11, 24), null, false, true, false, false);
																											return;
																										}
																										if (Wish == "tut:books")
																										{
																											TutorialManager.AdvanceStep(null);
																											The.Player.SystemMoveTo(The.ZoneManager.GetZone("JoppaWorld.11.24.1.0.11").GetCell(38, 17), null, false, true, false, false);
																											TutorialManager.AdvanceStep(new GetBooks());
																											return;
																										}
																										if (Wish == "clearcooldowns")
																										{
																											The.Player.GetPart<ActivatedAbilities>().ClearCooldowns();
																											return;
																										}
																										if (Wish == "forcethawcurrent")
																										{
																											Location2D location = The.PlayerCell.Location;
																											The.Player.SystemMoveTo(null, null, false, true, false, false);
																											Zone zone6;
																											ZoneManager.instance.ForceTryThawZone(The.Player.CurrentCell.ParentZone.ZoneID, out zone6);
																											zone6.GetCell(location).AddObject(The.Player, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
																											zone6.SetActive();
																											return;
																										}
																										if (Wish == "thwomp")
																										{
																											GameManager.Instance.ThinWorldDistorting = GameManager.Instance.MAX_THIN_WORLD_DISTORTING;
																											return;
																										}
																										if (Wish == "renderdelay")
																										{
																											The.Core.RenderDelay(5000, true, true, false);
																											return;
																										}
																										if (Wish.StartsWith("controllerfont:"))
																										{
																											Options.SetOption("OptionControllerFont", Wish.Split(':', StringSplitOptions.None)[1]);
																											return;
																										}
																										if (Wish == "rendertest")
																										{
																											GameManager.Instance.uiQueue.queueTask(delegate
																											{
																												The.Core.RenderBase(true, false);
																											}, 0);
																											return;
																										}
																										if (Wish == "idkfa" || Wish == "godmode")
																										{
																											The.Core.IDKFA = !The.Core.IDKFA;
																											MessageQueue.AddPlayerMessage("Godmode now " + The.Core.IDKFA.ToString(), null, true);
																											return;
																										}
																										if (Wish == "allfire")
																										{
																											The.Core.IDKFA = !The.Core.IDKFA;
																											The.Player.CurrentZone.ForeachObject(delegate(GameObject o)
																											{
																												if (o.Physics.IsReal)
																												{
																													o.Physics.Temperature = o.Physics.FlameTemperature + 100;
																												}
																											});
																											return;
																										}
																										if (Wish == "navweight")
																										{
																											int num7 = The.Player.CurrentCell.GetNavigationWeightFor(The.Player, false, false, false, false, false);
																											Popup.Show(num7.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
																											return;
																										}
																										if (Wish == "clearnavcache")
																										{
																											The.ZoneManager.ActiveZone.FlushNavigationCaches();
																											return;
																										}
																										if (Wish == "eviltwin")
																										{
																											GameObject player3 = The.Player;
																											string defect_PREFIX = EvilTwin.DEFECT_PREFIX;
																											Cell cell13 = null;
																											string text28 = EvilTwin.DEFECT_DESCRIPTION;
																											EvilTwin.CreateEvilTwin(player3, defect_PREFIX, cell13, EvilTwin.DEFECT_MESSAGE, "&K", null, null, true, text28, null);
																											return;
																										}
																										if (Wish == "roadtest")
																										{
																											Zone currentZone5 = The.Player.CurrentZone;
																											currentZone5.AddZoneConnection("-", 0, 14, "Road", null);
																											currentZone5.AddZoneConnection("-", 79, 14, "Road", null);
																											new RoadBuilder().BuildZone(currentZone5);
																											return;
																										}
																										if (Wish == "rivertest")
																										{
																											Zone currentZone6 = The.Player.CurrentZone;
																											currentZone6.AddZoneConnection("-", 40, 0, "RiverMouth", null);
																											currentZone6.AddZoneConnection("-", 40, 24, "RiverMouth", null);
																											new RiverBuilder().BuildZone(currentZone6);
																											return;
																										}
																										if (Wish == "testendmessage")
																										{
																											PaxKlanqIPresumeSystem.UnderConstructionMessage();
																											return;
																										}
																										if (Wish == "crossintobright")
																										{
																											ThinWorld.CrossIntoBrightsheol();
																											return;
																										}
																										if (Wish == "eventtest")
																										{
																											Stopwatch stopwatch3 = new Stopwatch();
																											stopwatch3.Reset();
																											stopwatch3.Start();
																											EndTurnEvent endTurnEvent = new EndTurnEvent();
																											for (int num72 = 0; num72 < 100000; num72++)
																											{
																												The.Player.HandleEvent(endTurnEvent);
																											}
																											global::UnityEngine.Debug.Log("1m events in " + stopwatch3.Elapsed.ToString());
																											return;
																										}
																										if (Wish == "shove")
																										{
																											string text59 = PickDirection.ShowPicker("Shove");
																											The.Player.GetCurrentCell().GetCellFromDirection(text59, true).GetObjectsWithPart("Physics")[0].Move(text59, true, false, false, false, true, true, null, null, false, null, null, null, false, false, null, null, 0);
																											return;
																										}
																										if (Wish == "makestranger")
																										{
																											string text60 = PickDirection.ShowPicker("Make Stranger");
																											GameObject combatTarget = The.Player.GetCurrentCell().GetCellFromDirection(text60, true).GetCombatTarget(The.Player, false, false, false, 0, null, null, null, null, null, true, false, null);
																											if (((combatTarget != null) ? combatTarget.Brain : null) != null)
																											{
																												combatTarget.Brain.Factions = "Strangers-100";
																												return;
																											}
																										}
																										else
																										{
																											if (Wish == "weather")
																											{
																												Zone parentZone4 = The.Player.CurrentCell.ParentZone;
																												StringBuilder stringBuilder21 = Event.NewStringBuilder(null);
																												stringBuilder21.Append("HasWeather: ").Append(parentZone4.HasWeather).Append('\n')
																													.Append("WindSpeed: ")
																													.Append(parentZone4.WindSpeed)
																													.Append('\n')
																													.Append("WindDirections: ")
																													.Append(parentZone4.WindDirections)
																													.Append('\n')
																													.Append("WindDuration: ")
																													.Append(parentZone4.WindDuration)
																													.Append('\n')
																													.Append("CurrentWindSpeed: ")
																													.Append(parentZone4.CurrentWindSpeed)
																													.Append('\n')
																													.Append("CurrentWindDirection: ")
																													.Append(parentZone4.CurrentWindDirection)
																													.Append('\n')
																													.Append("NextWindChange: +")
																													.Append(parentZone4.NextWindChange - The.Game.TimeTicks)
																													.Append('\n');
																												Popup.Show(stringBuilder21.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
																												return;
																											}
																											if (Wish == "fungalvision")
																											{
																												FungalVisionary.VisionLevel = 1;
																												return;
																											}
																											if (Wish == "glitchtest")
																											{
																												LiquidWarmStatic.GlitchZone(The.PlayerCell.ParentZone);
																												return;
																											}
																											if (Wish == "masterchef")
																											{
																												Popup.Suppress = true;
																												who.ReceiveObject("Fermented Yuckwheat Stem", 10, false, 0, 0, null, null, null, null);
																												who.ReceiveObject("Voider Gland Paste", 10, false, 0, 0, null, null, null, null);
																												who.ReceiveObjectsFromPopulation("Ingredients_MidTiers", 20, null, false, 0, 0, null, null, null, null, null);
																												List<string> list23 = new List<string>
																												{
																													"Phase Silk", "Starapple Preserves", "Cured Dawnglider Tail", "Pickled Mushrooms", "Fermented Yondercane", "Spark Tick Plasma", "Spine Fruit Jam", "Vinewafer Sheaf", "Goat Jerky", "Beetle Jerky",
																													"Pickles", "Fire Ant Gaster Paste", "Voider Gland Paste", "Congealed Shade Oil", "CiderPool"
																												};
																												for (int num73 = 0; num73 < 6; num73++)
																												{
																													string text61 = list23.GetRandomElement<string>(null);
																													string text62;
																													do
																													{
																														text62 = list23.GetRandomElement<string>(null);
																													}
																													while (text62 == text61);
																													string text63;
																													do
																													{
																														text63 = list23.GetRandomElement<string>(null);
																													}
																													while (text63 == text61 || text63 == text62);
																													CookingGameState.LearnRecipe(CookingRecipe.FromIngredients(new List<GameObject>
																													{
																														GameObjectFactory.Factory.CreateObject(text61),
																														GameObjectFactory.Factory.CreateObject(text62),
																														GameObjectFactory.Factory.CreateObject(text63)
																													}, null, who.DisplayNameOnlyStripped, "generic"), The.Player);
																												}
																												for (int num74 = 0; num74 < 3; num74++)
																												{
																													string text61 = list23.GetRandomElement<string>(null);
																													string text62;
																													do
																													{
																														text62 = list23.GetRandomElement<string>(null);
																													}
																													while (text62 == text61);
																													string text63;
																													do
																													{
																														text63 = list23.GetRandomElement<string>(null);
																													}
																													while (text63 == text61 || text63 == text62);
																													CookingGameState.LearnRecipe(CookingRecipe.FromIngredients(new List<string> { text61, text62 }, null, who.DisplayNameOnlyStripped), The.Player);
																												}
																												for (int num75 = 0; num75 < 1; num75++)
																												{
																													string text61 = list23.GetRandomElement<string>(null);
																													string text62;
																													do
																													{
																														text62 = list23.GetRandomElement<string>(null);
																													}
																													while (text62 == text61);
																													string text63;
																													do
																													{
																														text63 = list23.GetRandomElement<string>(null);
																													}
																													while (text63 == text61 || text63 == text62);
																													CookingGameState.LearnRecipe(CookingRecipe.FromIngredients(new List<string> { text61 }, null, who.DisplayNameOnlyStripped), The.Player);
																												}
																												CookingGameState.LearnRecipe(new HotandSpiny(), null);
																												Wishing.AddSkill("CookingAndGathering");
																												Wishing.AddSkill("CookingAndGathering_Harvestry");
																												Wishing.AddSkill("CookingAndGathering_Butchery");
																												Wishing.AddSkill("CookingAndGathering_Spicer");
																												Wishing.AddSkill("CookingAndGathering_CarbideChef");
																												MessageQueue.AddPlayerMessage("Added cooking knowledge.", null, true);
																												Popup.Suppress = false;
																												return;
																											}
																											if (Wish == "masterchef2")
																											{
																												Popup.Suppress = true;
																												who.ReceiveObject("Fermented Yuckwheat Stem", 10, false, 0, 0, null, null, null, null);
																												who.ReceiveObject("Voider Gland Paste", 10, false, 0, 0, null, null, null, null);
																												List<string> list24 = new List<string> { "Starapple", "Bear Jerky", "FluxWaterskin_Ingredient" };
																												who.ReceiveObject("Phase Silk", 100, false, 0, 0, null, null, null, null);
																												who.ReceiveObject("Starapple", 100, false, 0, 0, null, null, null, null);
																												who.ReceiveObject("Bear Jerky", 100, false, 0, 0, null, null, null, null);
																												for (int num76 = 0; num76 < 6; num76++)
																												{
																													string text64 = list24.GetRandomElement<string>(null);
																													string text65;
																													do
																													{
																														text65 = list24.GetRandomElement<string>(null);
																													}
																													while (text65 == text64);
																													string text66;
																													do
																													{
																														text66 = list24.GetRandomElement<string>(null);
																													}
																													while (text66 == text64 || text66 == text65);
																													CookingGameState.LearnRecipe(CookingRecipe.FromIngredients(new List<GameObject>
																													{
																														GameObjectFactory.Factory.CreateObject(text64),
																														GameObjectFactory.Factory.CreateObject(text65),
																														GameObjectFactory.Factory.CreateObject(text66)
																													}, null, who.DisplayNameOnlyStripped, "generic"), The.Player);
																												}
																												for (int num77 = 0; num77 < 3; num77++)
																												{
																													string text64 = list24.GetRandomElement<string>(null);
																													string text65;
																													do
																													{
																														text65 = list24.GetRandomElement<string>(null);
																													}
																													while (text65 == text64);
																													string text66;
																													do
																													{
																														text66 = list24.GetRandomElement<string>(null);
																													}
																													while (text66 == text64 || text66 == text65);
																													CookingGameState.LearnRecipe(CookingRecipe.FromIngredients(new List<string> { text64, text65 }, null, who.DisplayNameOnlyStripped), The.Player);
																												}
																												for (int num78 = 0; num78 < 1; num78++)
																												{
																													string text64 = list24.GetRandomElement<string>(null);
																													string text65;
																													do
																													{
																														text65 = list24.GetRandomElement<string>(null);
																													}
																													while (text65 == text64);
																													string text66;
																													do
																													{
																														text66 = list24.GetRandomElement<string>(null);
																													}
																													while (text66 == text64 || text66 == text65);
																													CookingGameState.LearnRecipe(CookingRecipe.FromIngredients(new List<string> { text64 }, null, who.DisplayNameOnlyStripped), The.Player);
																												}
																												CookingGameState.LearnRecipe(new HotandSpiny(), null);
																												Wishing.AddSkill("CookingAndGathering");
																												Wishing.AddSkill("CookingAndGathering_Harvestry");
																												Wishing.AddSkill("CookingAndGathering_Butchery");
																												Wishing.AddSkill("CookingAndGathering_Spicer");
																												Wishing.AddSkill("CookingAndGathering_CarbideChef");
																												MessageQueue.AddPlayerMessage("Added cooking knowledge.", null, true);
																												Popup.Suppress = false;
																												return;
																											}
																											if (Wish.StartsWith("findobj:"))
																											{
																												GameObject gameObject27 = GameObject.FindByID(Wish.Split(':', StringSplitOptions.None)[1]);
																												if (gameObject27 == null)
																												{
																													Popup.ShowFail("Not found.", true, true, true);
																													return;
																												}
																												GameObject gameObject28;
																												Cell cell14;
																												BodyPart bodyPart4;
																												gameObject27.GetContext(out gameObject28, out cell14, out bodyPart4);
																												if (gameObject28 != null)
																												{
																													if (bodyPart4 != null)
																													{
																														Popup.ShowFail(string.Concat(new string[] { gameObject27.DebugName, " found in ", gameObject28.DebugName, " body part ", bodyPart4.Name }), true, true, true);
																														return;
																													}
																													Popup.ShowFail(gameObject27.DebugName + " found in " + gameObject28.DebugName, true, true, true);
																													return;
																												}
																												else
																												{
																													if (cell14 != null)
																													{
																														string[] array24 = new string[5];
																														array24[0] = gameObject27.DebugName;
																														array24[1] = " found in ";
																														array24[2] = cell14.ToString();
																														array24[3] = " of ";
																														int num79 = 4;
																														Zone parentZone5 = cell14.ParentZone;
																														array24[num79] = ((parentZone5 != null) ? parentZone5.ZoneID : null) ?? "NULL";
																														Popup.ShowFail(string.Concat(array24), true, true, true);
																														return;
																													}
																													Popup.ShowFail(gameObject27.DebugName + " found with no context", true, true, true);
																													return;
																												}
																											}
																											else if (Wish.StartsWith("gamestate:"))
																											{
																												string[] array25 = Wish.Split(':', StringSplitOptions.None);
																												if (array25.Length == 2)
																												{
																													MessageQueue.AddPlayerMessage(array25[1] + "=" + The.Game.GetStringGameState(array25[1], ""), null, true);
																													return;
																												}
																												if (array25[2] == "null")
																												{
																													The.Game.StringGameState.Set(array25[1], null);
																													return;
																												}
																												The.Game.StringGameState.Set(array25[1], array25[2]);
																												return;
																											}
																											else
																											{
																												if (Wish == "cyber")
																												{
																													who.GetCurrentCell().GetCellFromDirection("N", true).AddObject("CyberneticsTerminal2", null, null, null, null);
																													GameObject gameObject29 = who.GetCurrentCell().GetCellFromDirection("NW", true).AddObject("CyberneticsStationRack", null, null, null, null);
																													gameObject29.Inventory.AddObject(GameObjectFactory.Factory.CreateObject("DermalPlating"), null, false, false, true, null);
																													gameObject29.Inventory.AddObject(GameObjectFactory.Factory.CreateObject("DermalInsulation"), null, false, false, true, null);
																													gameObject29.Inventory.AddObject(GameObjectFactory.Factory.CreateObject("BiologicalIndexer"), null, false, false, true, null);
																													gameObject29.Inventory.AddObject(GameObjectFactory.Factory.CreateObject("TechnologicalIndexer"), null, false, false, true, null);
																													gameObject29.Inventory.AddObject(GameObjectFactory.Factory.CreateObject("NightVision"), null, false, false, true, null);
																													gameObject29.Inventory.AddObject(GameObjectFactory.Factory.CreateObject("HyperElasticAnkleTendons"), null, false, false, true, null);
																													gameObject29.Inventory.AddObject(GameObjectFactory.Factory.CreateObject("ParabolicMuscularSubroutine"), null, false, false, true, null);
																													gameObject29.Inventory.AddObject(GameObjectFactory.Factory.CreateObject("CherubicVisage"), null, false, false, true, null);
																													gameObject29.Inventory.AddObject(GameObjectFactory.Factory.CreateObject("TranslucentSkin"), null, false, false, true, null);
																													gameObject29.Inventory.AddObject(GameObjectFactory.Factory.CreateObject("StabilizerArmLocks"), null, false, false, true, null);
																													gameObject29.Inventory.AddObject(GameObjectFactory.Factory.CreateObject("RapidReleaseFingerFlexors"), null, false, false, true, null);
																													gameObject29.Inventory.AddObject(GameObjectFactory.Factory.CreateObject("CarbideHandBones"), null, false, false, true, null);
																													gameObject29.Inventory.AddObject(GameObjectFactory.Factory.CreateObject("Pentaceps"), null, false, false, true, null);
																													gameObject29.Inventory.AddObject(GameObjectFactory.Factory.CreateObject("MotorizedTreads"), null, false, false, true, null);
																													gameObject29.Inventory.AddObject(GameObjectFactory.Factory.CreateObject("InflatableAxons"), null, false, false, true, null);
																													gameObject29.Inventory.AddObject(GameObjectFactory.Factory.CreateObject("NocturnalApex"), null, false, false, true, null);
																													gameObject29.Inventory.AddObject(GameObjectFactory.Factory.CreateObject("ElectromagneticSensor"), null, false, false, true, null);
																													gameObject29.Inventory.AddObject(GameObjectFactory.Factory.CreateObject("MatterRecompositer"), null, false, false, true, null);
																													gameObject29.Inventory.AddObject(GameObjectFactory.Factory.CreateObject("AirCurrentMicrosensor"), null, false, false, true, null);
																													gameObject29.Inventory.AddObject(GameObjectFactory.Factory.CreateObject("CustomVisage"), null, false, false, true, null);
																													gameObject29.Inventory.AddObject(GameObjectFactory.Factory.CreateObject("GunRack"), null, false, false, true, null);
																													foreach (GameObject gameObject30 in gameObject29.Inventory.GetObjectsDirect())
																													{
																														gameObject30.MakeUnderstood(false);
																													}
																													who.ReceiveObject("CyberneticsCreditWedge", 12, false, 0, 0, null, null, null, null);
																													return;
																												}
																												if (Wish == "filljournal")
																												{
																													for (int num80 = 0; num80 < 100; num80++)
																													{
																														JournalAPI.AddAccomplishment("Afkfasdf ajsd fa fs adkf as dfas dfk asdlf a f asdf a" + Guid.NewGuid().ToString(), null, null, null, "general", MuralCategory.Generic, MuralWeight.Medium, null, -1L, true);
																													}
																													return;
																												}
																												if (Wish == "wavetilegen")
																												{
																													GameManager.Instance.uiQueue.queueTask(delegate
																													{
																														Texture2D texture2D = new Texture2D(80, 25, TextureFormat.ARGB32, false);
																														texture2D.filterMode = global::UnityEngine.FilterMode.Point;
																														Zone currentZone7 = who.CurrentZone;
																														for (int num88 = 0; num88 < currentZone7.Height; num88++)
																														{
																															for (int num89 = 0; num89 < currentZone7.Width; num89++)
																															{
																																Cell cell21 = currentZone7.GetCell(num89, num88);
																																if (cell21.HasWall())
																																{
																																	texture2D.SetPixel(num89, num88, new Color32(0, 0, 0, byte.MaxValue));
																																}
																																else if (cell21.HasObjectWithPart("Combat"))
																																{
																																	texture2D.SetPixel(num89, num88, new Color32(byte.MaxValue, 0, 0, byte.MaxValue));
																																}
																																else if (cell21.HasObjectWithPart("LiquidVolume"))
																																{
																																	texture2D.SetPixel(num89, num88, new Color32(0, 0, byte.MaxValue, byte.MaxValue));
																																}
																																else if (cell21.HasObjectWithPart("PlantProperties"))
																																{
																																	texture2D.SetPixel(num89, num88, new Color32(0, byte.MaxValue, 0, byte.MaxValue));
																																}
																																else if (cell21.HasObjectWithPart("Door"))
																																{
																																	texture2D.SetPixel(num89, num88, new Color32(byte.MaxValue, byte.MaxValue, 0, byte.MaxValue));
																																}
																																else if (cell21.HasObjectWithPart("Furniture"))
																																{
																																	texture2D.SetPixel(num89, num88, new Color32(byte.MaxValue, 0, byte.MaxValue, byte.MaxValue));
																																}
																																else
																																{
																																	texture2D.SetPixel(num89, num88, new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue));
																																}
																															}
																														}
																														File.WriteAllBytes(DataManager.SavePath("tilegen.png"), texture2D.EncodeToPNG());
																													}, 0);
																													return;
																												}
																												if (Wish == "slow")
																												{
																													who.Statistics["Speed"].BaseValue = 25;
																													return;
																												}
																												if (Wish == "fast")
																												{
																													who.Statistics["Speed"].BaseValue = 500;
																													return;
																												}
																												if (Wish.StartsWith("testpop"))
																												{
																													string[] array26 = Wish.Split(' ', StringSplitOptions.None);
																													List<PopulationResult> list25;
																													if (array26.Length <= 2)
																													{
																														list25 = PopulationManager.Generate(array26[1], null, null, null, null, null);
																													}
																													else
																													{
																														list25 = PopulationManager.Generate(array26[1], array26[2], array26[3], null, null, null);
																													}
																													MessageQueue.AddPlayerMessage("-- generating " + array26[1] + " ---", null, true);
																													using (List<PopulationResult>.Enumerator enumerator29 = list25.GetEnumerator())
																													{
																														while (enumerator29.MoveNext())
																														{
																															PopulationResult populationResult = enumerator29.Current;
																															MessageQueue.AddPlayerMessage(populationResult.Blueprint + " x" + populationResult.Number.ToString() + ((!string.IsNullOrEmpty(populationResult.Hint)) ? (" hint:" + populationResult.Hint) : ""), null, true);
																														}
																														return;
																													}
																												}
																												if (Wish == "maxmod")
																												{
																													The.Core.CheatMaxMod = !The.Core.CheatMaxMod;
																													return;
																												}
																												if (Wish == "night")
																												{
																													The.Game.TimeTicks += (long)((10000 - Calendar.CurrentDaySegment) / 10);
																													return;
																												}
																												if (Wish == "day")
																												{
																													The.Game.TimeTicks += (long)((3250 - Calendar.CurrentDaySegment) / 10);
																													return;
																												}
																												if (Wish == "restock")
																												{
																													The.ActiveZone.ForeachObject(delegate(GameObject obj)
																													{
																														GenericInventoryRestocker genericInventoryRestocker;
																														if (obj.TryGetPart<GenericInventoryRestocker>(out genericInventoryRestocker))
																														{
																															genericInventoryRestocker.PerformRestock(false);
																															genericInventoryRestocker.LastRestockTick = The.Game.TimeTicks;
																														}
																													});
																													return;
																												}
																												if (Wish == "degeneratemarkup")
																												{
																													Popup.Show("Degenerate markup here: {{text}}", null, "Sounds/UI/ui_notification", true, true, true, true, null);
																													return;
																												}
																												if (Wish == "daze")
																												{
																													The.Player.ApplyEffect(new Dazed(Stat.Random(2, 8)), null);
																													return;
																												}
																												if (Wish == "stun")
																												{
																													The.Player.ApplyEffect(new Stun(Stat.Random(2, 8), 30, false), null);
																													return;
																												}
																												if (Wish == "bleed")
																												{
																													game.Player.Body.ApplyEffect(new Bleeding(), null);
																													return;
																												}
																												if (Wish.StartsWith("bleed:"))
																												{
																													string text67 = Wish.Split(':', StringSplitOptions.None)[1];
																													game.Player.Body.ApplyEffect(new Bleeding("1", Convert.ToInt32(text67), null, true, false, false, false), null);
																													return;
																												}
																												if (Wish == "holobleed")
																												{
																													game.Player.Body.ApplyEffect(new HolographicBleeding(), null);
																													return;
																												}
																												if (Wish.StartsWith("holobleed:"))
																												{
																													string text68 = Wish.Split(':', StringSplitOptions.None)[1];
																													game.Player.Body.ApplyEffect(new HolographicBleeding("1", Convert.ToInt32(text68), null, true, false, false, false), null);
																													return;
																												}
																												if (Wish == "nosebleed")
																												{
																													game.Player.Body.ApplyEffect(new Nosebleed(), null);
																													return;
																												}
																												if (Wish.StartsWith("nosebleed:"))
																												{
																													string text69 = Wish.Split(':', StringSplitOptions.None)[1];
																													game.Player.Body.ApplyEffect(new Nosebleed("1", Convert.ToInt32(text69), null, true, false, false), null);
																													return;
																												}
																												if (Wish == "poison")
																												{
																													game.Player.Body.ApplyEffect(new Poisoned(30, "1d4", 1, null, false, false), null);
																													return;
																												}
																												if (Wish == "ill")
																												{
																													game.Player.Body.ApplyEffect(new Ill(30, 1, null, false, false), null);
																													return;
																												}
																												if (Wish == "dude")
																												{
																													GameObject oneCreatureFromZone = The.ZoneManager.GetOneCreatureFromZone(The.ZoneManager.ActiveZone.ZoneID);
																													if (oneCreatureFromZone != null)
																													{
																														Popup.Show(oneCreatureFromZone.DisplayName, null, "Sounds/UI/ui_notification", true, true, true, true, null);
																														return;
																													}
																												}
																												else
																												{
																													if (Wish.StartsWith("effect:"))
																													{
																														string text70 = Wish.Split(':', StringSplitOptions.None)[1];
																														who.ApplyEffect(Activator.CreateInstance(ModManager.ResolveType("XRL.World.Effects." + text70, false, false, true)) as Effect, null);
																														return;
																													}
																													if (Wish == "lost")
																													{
																														who.ApplyEffect(new Lost(), null);
																														return;
																													}
																													if (Wish == "notlost")
																													{
																														who.RemoveEffect<Lost>();
																														return;
																													}
																													if (Wish == "clone")
																													{
																														string text71 = PickDirection.ShowPicker("Clone");
																														if (text71 != null)
																														{
																															Cell cellFromDirection = who.Physics.CurrentCell.GetCellFromDirection(text71, true);
																															if (cellFromDirection != null)
																															{
																																GameObject gameObject31 = who.DeepCopy(false, false, null);
																																cellFromDirection.AddObject(gameObject31, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
																																return;
																															}
																														}
																													}
																													else if (Wish == "copy")
																													{
																														string text72 = PickDirection.ShowPicker("Copy");
																														if (text72 != null)
																														{
																															Cell cellFromDirection2 = who.Physics.CurrentCell.GetCellFromDirection(text72, true);
																															if (cellFromDirection2 != null)
																															{
																																GameObject combatTarget2 = cellFromDirection2.GetCombatTarget(null, false, false, false, 0, null, null, null, null, null, true, false, null);
																																combatTarget2.Physics.CurrentCell.GetEmptyAdjacentCells().GetRandomElement<Cell>(null).AddObject(combatTarget2.DeepCopy(false, false, null), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
																																return;
																															}
																														}
																													}
																													else
																													{
																														if (Wish == "RandomNoReceiveObject")
																														{
																															GameObject anObject = EncountersAPI.GetAnObject((GameObjectBlueprint o) => !o.HasTag("Item", false));
																															who.GetCurrentCell().GetFirstEmptyAdjacentCell().AddObject(anObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
																															return;
																														}
																														if (Wish == "expand")
																														{
																															Popup.Show(HistoricStringExpander.ExpandString("<spice.quests.questContext.itemNameMutation.!random>", null), null, "Sounds/UI/ui_notification", true, true, true, true, null);
																															return;
																														}
																														if (Wish == "explodingpalm")
																														{
																															string text73 = PickDirection.ShowPicker("Exploding Palm");
																															if (text73 != null)
																															{
																																Cell cellFromDirection3 = who.Physics.CurrentCell.GetCellFromDirection(text73, true);
																																if (cellFromDirection3 != null)
																																{
																																	GameObject firstObjectWithPart = cellFromDirection3.GetFirstObjectWithPart("Combat");
																																	for (int num81 = 0; num81 < 6; num81++)
																																	{
																																		Axe_Dismember.Dismember(who, firstObjectWithPart, null, null, null, null, "sfx_characterTrigger_dismember", false, false, false, false);
																																	}
																																	Axe_Decapitate.Decapitate(who, firstObjectWithPart, null, null, null, null, false, false);
																																	return;
																																}
																															}
																														}
																														else
																														{
																															if (Wish == "yoink")
																															{
																																string text74 = PickDirection.ShowPicker("Swap");
																																if (text74 == null)
																																{
																																	return;
																																}
																																XRL.World.Parts.Physics physics2 = who.Physics;
																																Cell cellFromDirection4 = physics2.CurrentCell.GetCellFromDirection(text74, true);
																																if (cellFromDirection4 == null)
																																{
																																	return;
																																}
																																using (Container<GameObject>.Enumerator enumerator12 = cellFromDirection4.Objects.GetEnumerator())
																																{
																																	while (enumerator12.MoveNext())
																																	{
																																		GameObject gameObject32 = enumerator12.Current;
																																		gameObject32.SystemMoveTo(physics2.currentCell, null, false, true, false, false);
																																	}
																																	return;
																																}
																															}
																															if (Wish == "swap")
																															{
																																string text75 = PickDirection.ShowPicker("Swap");
																																if (text75 != null)
																																{
																																	Cell cellFromDirection5 = who.Physics.CurrentCell.GetCellFromDirection(text75, true);
																																	if (cellFromDirection5 != null)
																																	{
																																		List<GameObject> objectsWithPart = cellFromDirection5.GetObjectsWithPart("Combat");
																																		if (objectsWithPart.Count > 0)
																																		{
																																			game.Player.Body = objectsWithPart[0];
																																			return;
																																		}
																																	}
																																}
																															}
																															else
																															{
																																if (Wish == "svardymstorm")
																																{
																																	The.Game.GetSystem<SvardymSystem>().BeginStorm();
																																	return;
																																}
																																if (Wish == "dismiss")
																																{
																																	string text76 = PickDirection.ShowPicker("Dismiss");
																																	if (text76 != null)
																																	{
																																		Cell cellFromDirection6 = who.CurrentCell.GetCellFromDirection(text76, true);
																																		if (cellFromDirection6 != null)
																																		{
																																			GameObject firstObjectWithPart2 = cellFromDirection6.GetFirstObjectWithPart("Brain", (GameObject o) => o != who);
																																			if (firstObjectWithPart2 != null)
																																			{
																																				firstObjectWithPart2.Brain.Goals.Clear();
																																				firstObjectWithPart2.SetAlliedLeader<AllyWish>(null, 0, false);
																																				GameObject gameObject33 = firstObjectWithPart2;
																																				string text77 = "are";
																																				int maxValue = int.MaxValue;
																																				string text78 = null;
																																				string text79 = null;
																																				string text80 = null;
																																				bool flag7 = false;
																																				bool flag8 = false;
																																				bool flag9 = false;
																																				bool flag10 = false;
																																				bool flag11 = false;
																																				bool flag12 = true;
																																				bool flag13 = true;
																																				bool flag14 = false;
																																				bool flag15 = false;
																																				string text81 = null;
																																				bool flag16 = false;
																																				bool flag17 = false;
																																				bool flag18 = true;
																																				bool? flag4 = null;
																																				MessageQueue.AddPlayerMessage(gameObject33.Does(text77, maxValue, text78, text79, text80, flag7, flag8, flag9, flag10, flag11, flag12, flag13, flag14, flag15, text81, flag16, flag17, flag18, flag4, true, null, false) + " no longer your follower.", null, true);
																																				return;
																																			}
																																		}
																																	}
																																}
																																else if (Wish == "beguile")
																																{
																																	string text82 = PickDirection.ShowPicker("Beguile");
																																	if (text82 != null)
																																	{
																																		Cell cellFromDirection7 = who.CurrentCell.GetCellFromDirection(text82, true);
																																		if (cellFromDirection7 != null)
																																		{
																																			GameObject firstObjectWithPart3 = cellFromDirection7.GetFirstObjectWithPart("Brain", (GameObject o) => o != who);
																																			if (firstObjectWithPart3 != null)
																																			{
																																				firstObjectWithPart3.Brain.Goals.Clear();
																																				firstObjectWithPart3.SetAlliedLeader<AllyWish>(who, 0, false);
																																				GameObject gameObject34 = firstObjectWithPart3;
																																				string text83 = "are";
																																				int maxValue2 = int.MaxValue;
																																				string text84 = null;
																																				string text85 = null;
																																				string text86 = null;
																																				bool flag19 = false;
																																				bool flag20 = false;
																																				bool flag21 = false;
																																				bool flag22 = false;
																																				bool flag23 = false;
																																				bool flag24 = true;
																																				bool flag25 = true;
																																				bool flag26 = false;
																																				bool flag27 = false;
																																				string text87 = null;
																																				bool flag28 = false;
																																				bool flag29 = false;
																																				bool flag30 = true;
																																				bool? flag4 = null;
																																				MessageQueue.AddPlayerMessage(gameObject34.Does(text83, maxValue2, text84, text85, text86, flag19, flag20, flag21, flag22, flag23, flag24, flag25, flag26, flag27, text87, flag28, flag29, flag30, flag4, true, null, false) + " now your follower.", null, true);
																																				return;
																																			}
																																		}
																																	}
																																}
																																else if (Wish == "blueprint")
																																{
																																	string text88 = PickDirection.ShowPicker("Blueprint");
																																	if (text88 != null)
																																	{
																																		Cell cellFromDirection8 = The.Player.CurrentCell.GetCellFromDirection(text88, true);
																																		if (cellFromDirection8 != null)
																																		{
																																			GameObject firstObject = cellFromDirection8.GetFirstObject();
																																			if (firstObject != null)
																																			{
																																				Popup.Show(firstObject.Blueprint, null, "Sounds/UI/ui_notification", true, true, true, true, null);
																																				return;
																																			}
																																		}
																																	}
																																}
																																else
																																{
																																	if (Wish == "supermutant")
																																	{
																																		foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
																																		{
																																			if (type.FullName.Contains("Parts.Mutation"))
																																			{
																																				Mutations part5 = who.GetPart<Mutations>();
																																				try
																																				{
																																					BaseMutation baseMutation = Activator.CreateInstance(type) as BaseMutation;
																																					if (baseMutation != null && baseMutation.Name != "FearAura" && baseMutation.GetMutationEntry() != null)
																																					{
																																						part5.AddMutation(baseMutation, 1, true);
																																					}
																																				}
																																				catch
																																				{
																																				}
																																			}
																																		}
																																		return;
																																	}
																																	if (Wish == "decapitateme")
																																	{
																																		Axe_Decapitate.Decapitate(who, who, null, null, null, null, false, false);
																																		return;
																																	}
																																	if (Wish == "optionspop")
																																	{
																																		Popup.PickOption("Title only - short", null, "", "Sounds/UI/ui_notification", new string[] { "Option 1", "Option 2", "Option 3" }, null, null, null, null, null, null, 0, 60, 0, -1, true, false, false, true, false, null, null);
																																		Popup.PickOption("Title - short - keymap", null, "", "Sounds/UI/ui_notification", new string[] { "Option 1", "Option 2", "Option 3" }, new char[] { 'a', 'b', 'c' }, null, null, null, null, null, 0, 60, 0, -1, true, false, false, true, false, null, null);
																																		Popup.PickOption("Title - newlines - no respect", null, "", "Sounds/UI/ui_notification", new string[] { "Option 1:\nHas multiple lines", "Option 2:\nHas multiple lines", "Option 3:\nHas multiple lines" }, null, null, null, null, null, null, 0, 60, 0, -1, true, false, false, true, false, null, null);
																																		Popup.PickOption("Title - newlines - no respect - keymap", null, "", "Sounds/UI/ui_notification", new string[] { "Option 1:\nHas multiple lines", "Option 2:\nHas multiple lines", "Option 3:\nHas multiple lines" }, new char[] { 'a', 'b', 'c' }, null, null, null, null, null, 0, 60, 0, -1, true, false, false, true, false, null, null);
																																		Popup.PickOption("Title - intro - newlines - no respect", "This is a multi line intro of 3 lines\nThe second line &KColors&r something\nbut probably doesnt bleed?", "", "Sounds/UI/ui_notification", new string[] { "Option 1:\nHas multiple lines", "Option 2:\nHas multiple lines", "Option 3:\nHas multiple lines" }, null, null, null, null, null, null, 0, 60, 0, -1, true, false, false, true, false, null, null);
																																		Popup.PickOption("Title - intro - newlines - no respect - spacing", "This is a multi line intro of 3 lines\nThe second line &KColors&r something\nbut probably doesnt bleed?", "", "Sounds/UI/ui_notification", new string[] { "Option 1:\nHas multiple lines", "Option 2:\nHas multiple lines", "Option 3:\nHas multiple lines" }, null, null, null, null, null, null, 1, 60, 0, -1, true, false, false, true, false, null, null);
																																		Popup.PickOption("Title - intro - newlines - respect - spacing", "This is a multi line intro of 3 lines\nThe second line &KColors&r something\nbut probably doesnt bleed?", "", "Sounds/UI/ui_notification", new string[] { "Option 1:\nHas multiple lines", "Option 2:\nHas multiple lines", "Option 3:\nHas multiple lines" }, null, null, null, null, null, null, 1, 60, 0, -1, true, true, false, true, false, null, null);
																																		Popup.PickOption("Title - intro - newlines - respect - spacing - scrolling", "This is a multi line intro of 3 lines\nThe second line &KColors&r something\nbut probably doesnt bleed?", "", "Sounds/UI/ui_notification", new string[]
																																		{
																																			"Option 1:\nHas multiple lines", "Option 2:\nHas multiple lines", "Option 3:\nHas multiple lines", "Option x:\nHas multiple lines", "Option x:\nHas multiple lines", "Option x:\nHas multiple lines", "Option x:\nHas multiple lines", "Option x:\nHas multiple lines", "Option x:\nHas multiple lines", "Option x:\nHas multiple lines",
																																			"Option x:\nHas multiple lines", "Option x:\nHas multiple lines", "Option x:\nHas multiple lines", "Option x:\nHas multiple lines", "Option x:\nHas multiple lines", "Option x:\nHas multiple lines"
																																		}, null, null, null, null, null, null, 1, 60, 0, -1, true, true, false, true, false, null, null);
																																		Popup.PickOption("Title - intro - newlines - respect - scrolling", "This is a multi line intro of 3 lines\nThe second line &KColors&r something\nbut probably doesnt bleed?", "", "Sounds/UI/ui_notification", new string[]
																																		{
																																			"Option 1:\nHas multiple lines", "Option 2:\nHas multiple lines", "Option 3:\nHas multiple lines", "Option x:\nHas multiple lines", "Option y:\nHas multiple lines", "Option z:\nHas multiple lines", "Option g:\nHas multiple lines", "Option x:\nHas multiple lines", "Option y:\nHas multiple lines", "Option z:\nHas multiple lines",
																																			"Option g:\nHas multiple lines", "Option x:\nHas multiple lines", "Option y:\nHas multiple lines", "Option z:\nHas multiple lines", "Option g:\nHas multiple lines", "Option x:\nHas multiple lines", "Option y:\nHas multiple lines", "Option z:\nHas multiple lines", "Option g:\nHas multiple lines", "Option x:\nHas multiple lines",
																																			"Option y:\nHas multiple lines", "Option z:\nHas multiple lines", "Option g:\nHas multiple lines", "Option x:\nHas multiple lines", "Option y:\nHas multiple lines", "Option z:\nHas multiple lines", "Option g:\nHas multiple lines", "Option x:\nHas multiple lines"
																																		}, null, null, null, null, null, null, 0, 60, 0, -1, true, true, false, true, false, null, null);
																																		Popup.PickOption("Title - intro - newlines - no respect - scrolling", "This is a multi line intro of 3 lines\nThe second line &KColors&r something\nbut probably doesnt bleed?", "", "Sounds/UI/ui_notification", new string[]
																																		{
																																			"Option 1:\nHas multiple lines", "Option 2:\nHas multiple lines", "Option 3:\nHas multiple lines", "Option x:\nHas multiple lines", "Option y:\nHas multiple lines", "Option z:\nHas multiple lines", "Option g:\nHas multiple lines", "Option x:\nHas multiple lines", "Option y:\nHas multiple lines", "Option z:\nHas multiple lines",
																																			"Option g:\nHas multiple lines", "Option x:\nHas multiple lines", "Option y:\nHas multiple lines", "Option z:\nHas multiple lines", "Option g:\nHas multiple lines", "Option x:\nHas multiple lines", "Option y:\nHas multiple lines", "Option z:\nHas multiple lines", "Option g:\nHas multiple lines", "Option x:\nHas multiple lines",
																																			"Option y:\nHas multiple lines", "Option z:\nHas multiple lines", "Option g:\nHas multiple lines", "Option x:\nHas multiple lines", "Option y:\nHas multiple lines", "Option z:\nHas multiple lines", "Option g:\nHas multiple lines", "Option x:\nHas multiple lines"
																																		}, null, null, null, null, null, null, 0, 60, 0, -1, true, false, false, true, false, null, null);
																																		return;
																																	}
																																	if (Wish.StartsWith("checkforpart:"))
																																	{
																																		string text89 = Wish.Split(':', StringSplitOptions.None)[1];
																																		Popup.Show(who.HasPart(text89) ? ("Player has " + text89 + " part") : ("No " + text89 + " part on player"), null, "Sounds/UI/ui_notification", true, true, true, true, null);
																																		return;
																																	}
																																	if (Wish.StartsWith("item:"))
																																	{
																																		string[] array27 = Wish.Split(':', StringSplitOptions.None);
																																		string result = WishSearcher.SearchForBlueprint(array27[1]).Result;
																																		int num82 = 1;
																																		if (array27.Length > 2)
																																		{
																																			num82 = Convert.ToInt32(array27[2]);
																																		}
																																		Cell cell15 = who.GetCurrentCell();
																																		if (!array27.Contains("here"))
																																		{
																																			cell15 = cell15.GetFirstEmptyAdjacentCell();
																																		}
																																		for (int num83 = 0; num83 < num82; num83++)
																																		{
																																			cell15.AddObject(GameObjectFactory.Factory.CreateObject(result, 0, 0, null, null, null, "Wish", null), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
																																		}
																																		return;
																																	}
																																	if (Wish.StartsWith("understandpartial:"))
																																	{
																																		string[] array28 = Wish.Split(':', StringSplitOptions.None);
																																		string result2 = WishSearcher.SearchForBlueprint(array28[1]).Result;
																																		GameObject gameObject35 = GameObjectFactory.Factory.CreateObject(result2, 0, 0, null, null, null, "Wish", null);
																																		gameObject35.SetIntProperty("PartiallyUnderstood", 1, false);
																																		Cell cell16 = who.GetCurrentCell();
																																		if (!array28.Contains("here"))
																																		{
																																			cell16 = cell16.GetFirstEmptyAdjacentCell();
																																		}
																																		cell16.AddObject(gameObject35, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
																																		return;
																																	}
																																	if (Wish.StartsWith("factionheirloom:"))
																																	{
																																		who.CurrentCell.AddObject(Factions.Get(Wish.Split(':', StringSplitOptions.None)[1]).GetHeirloom(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
																																		return;
																																	}
																																	if (Wish.StartsWith("getboolgamestate:"))
																																	{
																																		string[] array29 = Wish.Split(':', StringSplitOptions.None);
																																		bool booleanGameState = The.Game.GetBooleanGameState(array29[1], false);
																																		Popup.Show(array29[1] + ": \"" + booleanGameState.ToString() + "\"", null, "Sounds/UI/ui_notification", true, true, true, true, null);
																																		return;
																																	}
																																	if (Wish.StartsWith("setstringgamestate:"))
																																	{
																																		string[] array30 = Wish.Split(':', StringSplitOptions.None);
																																		The.Game.SetStringGameState(array30[1], array30[2]);
																																		return;
																																	}
																																	if (Wish.StartsWith("getstringgamestate:"))
																																	{
																																		string[] array31 = Wish.Split(':', StringSplitOptions.None);
																																		string stringGameState = The.Game.GetStringGameState(array31[1], "");
																																		if (stringGameState == null)
																																		{
																																			Popup.Show(array31[1] + ": null", null, "Sounds/UI/ui_notification", true, true, true, true, null);
																																			return;
																																		}
																																		Popup.Show(array31[1] + ": \"" + stringGameState + "\"", null, "Sounds/UI/ui_notification", true, true, true, true, null);
																																		return;
																																	}
																																	else
																																	{
																																		if (Wish.StartsWith("hasblueprintbeenseen:"))
																																		{
																																			string[] array32 = Wish.Split(':', StringSplitOptions.None);
																																			bool flag31 = The.Game.HasBlueprintBeenSeen(array32[1]);
																																			Popup.Show(array32[1] + ": " + (flag31 ? "yes" : "no"), null, "Sounds/UI/ui_notification", true, true, true, true, null);
																																			return;
																																		}
																																		if (Wish.StartsWith("pluralize:"))
																																		{
																																			Popup.Show(Grammar.Pluralize(Wish.Split(':', StringSplitOptions.None)[1]), null, "Sounds/UI/ui_notification", true, true, true, true, null);
																																			return;
																																		}
																																		if (Wish.StartsWith("a:"))
																																		{
																																			Popup.Show(Grammar.A(Wish.Split(':', StringSplitOptions.None)[1], false), null, "Sounds/UI/ui_notification", true, true, true, true, null);
																																			return;
																																		}
																																		if (Wish.StartsWith("opinion:"))
																																		{
																																			int num7 = Factions.Get(Wish.GetDelimitedSubstring(':', 1, false)).GetFeelingTowardsObject(The.Player, null);
																																			Popup.Show(num7.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
																																			return;
																																		}
																																		if (Wish.StartsWith("wordrel:"))
																																		{
																																			string text90 = Wish.Split(':', StringSplitOptions.None)[1];
																																			List<string> relatedWords = WordDataManager.GetRelatedWords(ref text90, true, null, null, 0, 0, int.MaxValue);
																																			if (relatedWords == null)
																																			{
																																				Popup.Show(text90 + ": no results", null, "Sounds/UI/ui_notification", true, false, true, false, null);
																																				return;
																																			}
																																			Popup.Show(text90 + ": " + string.Join(", ", relatedWords.ToArray()), null, "Sounds/UI/ui_notification", true, false, true, false, null);
																																			return;
																																		}
																																		else
																																		{
																																			if (Wish == "clearzone")
																																			{
																																				foreach (GameObject gameObject36 in The.ActiveZone.IterateObjects())
																																				{
																																					if (!gameObject36.IsPlayer())
																																					{
																																						gameObject36.Destroy(null, false, false, null);
																																					}
																																				}
																																				return;
																																			}
																																			if (Wish == "highpools")
																																			{
																																				Popup.Show(MinEvent.GetTopPoolCountReport(20), null, "Sounds/UI/ui_notification", true, true, true, true, null);
																																				return;
																																			}
																																			if (Wish == "enablemarkup")
																																			{
																																				Markup.Enabled = true;
																																				return;
																																			}
																																			if (Wish == "giganticme")
																																			{
																																				The.Player.IsGiganticCreature = true;
																																				return;
																																			}
																																			if (Wish == "disablemarkup")
																																			{
																																				Markup.Enabled = false;
																																				return;
																																			}
																																			if (Wish != "")
																																			{
																																				if (Wish == "objdump")
																																				{
																																					TextWriter textWriter = new StreamWriter(DataManager.SavePath("ObjectDump.txt"));
																																					foreach (string text91 in GameObjectFactory.Factory.Blueprints.Keys)
																																					{
																																						string text92 = "?";
																																						GameObject gameObject37 = GameObjectFactory.Factory.CreateObject(text91, -200, 0, null, null, null, null, null);
																																						if (gameObject37.HasPart("Description"))
																																						{
																																							text92 = gameObject37.GetPart<Description>()._Short;
																																						}
																																						gameObject37.MakeUnderstood(false);
																																						string displayNameOnly = gameObject37.DisplayNameOnly;
																																						textWriter.WriteLine(ConsoleLib.Console.ColorUtility.StripFormatting(displayNameOnly.Substring(0, displayNameOnly.Length - 2).Replace("[", "").Replace("]", "")) + "," + text92.Replace(',', ';'));
																																					}
																																					textWriter.Close();
																																					textWriter.Dispose();
																																				}
																																				WishResult wishResult2 = WishSearcher.SearchForWish(Wish);
																																				if (wishResult2.Type == WishResultType.Quest)
																																				{
																																					The.Game.StartQuest(wishResult2.Result, null, null, null);
																																					return;
																																				}
																																				if (wishResult2.Type == WishResultType.Mutation)
																																				{
																																					Type type2 = ModManager.ResolveType(wishResult2.Result, false, false, true);
																																					who.GetPart<Mutations>().AddMutation((BaseMutation)Activator.CreateInstance(type2), 1, true);
																																					return;
																																				}
																																				if (wishResult2.Type == WishResultType.Blueprint)
																																				{
																																					bool flag32 = false;
																																					int num84 = 1;
																																					if (char.ToUpper(Wish[Wish.Length - 1]) == 'S')
																																					{
																																						num84 = Stat.RandomCosmetic(2, 4);
																																					}
																																					for (int num85 = 0; num85 < num84; num85++)
																																					{
																																						foreach (Cell cell17 in who.CurrentCell.GetAdjacentCells(true))
																																						{
																																							if (cell17.IsEmpty())
																																							{
																																								GameObject gameObject38 = GameObjectFactory.Factory.CreateObject(wishResult2.Result, 0, 0, null, null, null, "Wish", null);
																																								cell17.AddObject(gameObject38, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
																																								gameObject38.MakeActive(false);
																																								flag32 = true;
																																								break;
																																							}
																																						}
																																					}
																																					if (!flag32)
																																					{
																																						Popup.Show("No adjacent empty squares to create your wish!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
																																						return;
																																					}
																																				}
																																				else if (wishResult2.Type == WishResultType.Zone)
																																				{
																																					Zone zone7 = The.ZoneManager.SetActiveZone(wishResult2.Result);
																																					Cell cell18 = null;
																																					for (int num86 = zone7.Height - 2; num86 >= 0; num86--)
																																					{
																																						for (int num87 = zone7.Width / 2; num87 >= 0; num87--)
																																						{
																																							Cell cell19 = zone7.GetCell(num87, num86);
																																							if (cell19.IsReachable() && cell19.IsEmptyOfSolid(true))
																																							{
																																								cell18 = cell19;
																																								break;
																																							}
																																							Cell cell20 = zone7.GetCell(40 - num87, num86);
																																							if (cell20.IsReachable() && cell20.IsEmptyOfSolid(true))
																																							{
																																								cell18 = cell20;
																																								break;
																																							}
																																						}
																																						if (cell18 != null)
																																						{
																																							break;
																																						}
																																					}
																																					who.SystemMoveTo(cell18, null, false, true, false, false);
																																					The.ZoneManager.ProcessGoToPartyLeader();
																																				}
																																			}
																																		}
																																	}
																																}
																															}
																														}
																													}
																												}
																											}
																										}
																									}
																								}
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06006CF0 RID: 27888 RVA: 0x00247240 File Offset: 0x00245440
		private static void AddSkill(string Class)
		{
			object obj = Activator.CreateInstance(ModManager.ResolveType("XRL.World.Parts.Skill." + Class, false, false, true));
			The.Player.GetPart<Skills>().AddSkill(obj as BaseSkill, null, null);
		}

		// Token: 0x040031DD RID: 12765
		private static List<StringBuilder> memtest = new List<StringBuilder>();
	}
}
