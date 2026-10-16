using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.Console;
using HistoryKit;
using Qud.API;
using XRL.Language;
using XRL.Rules;
using XRL.World;
using XRL.World.Capabilities;
using XRL.World.Effects;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace XRL.UI
{
	// Token: 0x020014B2 RID: 5298
	public class StatusScreen : IScreen
	{
		// Token: 0x0600D015 RID: 53269 RVA: 0x004CA2D8 File Offset: 0x004C84D8
		private static void WriteStat(ScreenBuffer SB, GameObject GO, string Stat, bool bSelected, int x, int y)
		{
			SB.Goto(x, y);
			if (bSelected)
			{
				SB.Write("> {{W|" + Stat + "}}", true, false, false, null, -1);
			}
			else
			{
				SB.Write("  " + Stat, true, false, false, null, -1);
			}
			SB.Goto(x + 15, y);
			Statistic statistic = GO.Statistics[Stat];
			string text = "C";
			if (statistic.Value > statistic.BaseValue)
			{
				text = "G";
			}
			else if (statistic.Value < statistic.BaseValue)
			{
				text = "r";
			}
			SB.Write(string.Concat(new string[]
			{
				"{{",
				text,
				"|",
				statistic.Value.ToString(),
				"}}"
			}), true, false, false, null, -1);
			if (statistic.Modifier > 0)
			{
				SB.Write(" {{c|(+" + statistic.Modifier.ToString() + ")}}", true, false, false, null, -1);
				return;
			}
			SB.Write(" {{c|(" + statistic.Modifier.ToString() + ")}}", true, false, false, null, -1);
		}

		// Token: 0x0600D016 RID: 53270 RVA: 0x004CA414 File Offset: 0x004C8614
		public static void BuyStat(GameObject GO, string ChosenStat)
		{
			Statistic statistic = GO.Statistics[ChosenStat];
			int baseValue = statistic.BaseValue;
			int value = statistic.Value;
			string text;
			if (value == baseValue)
			{
				text = string.Concat(new string[]
				{
					"Your ",
					ChosenStat,
					" is {{C|",
					baseValue.ToString(),
					"}}"
				});
			}
			else if (value > baseValue)
			{
				text = string.Concat(new string[]
				{
					"Your base ",
					ChosenStat,
					" is {{C|",
					baseValue.ToString(),
					"}}, modified to {{G|",
					value.ToString(),
					"}}"
				});
			}
			else
			{
				text = string.Concat(new string[]
				{
					"Your base ",
					ChosenStat,
					" is {{C|",
					baseValue.ToString(),
					"}}, modified to {{R|",
					value.ToString(),
					"}}"
				});
			}
			text += ".\n\n";
			if (statistic.BaseValue >= 100)
			{
				Popup.Show(text + "You may not raise an attribute above 100.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				return;
			}
			if (GO.Stat("AP", 0) >= 1)
			{
				if (Popup.ShowYesNo(text + "It will cost {{C|1}} attribute point to increase " + ChosenStat + " by 1.\nDo you wish to increase this attribute?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes)
				{
					Statistic stat = GO.GetStat("AP");
					int num = stat.Penalty;
					stat.Penalty = num + 1;
					Statistic statistic2 = statistic;
					num = statistic2.BaseValue;
					statistic2.BaseValue = num + 1;
					Popup.Show(string.Concat(new string[]
					{
						"You have increased your ",
						ChosenStat,
						" to {{C|",
						statistic.Value.ToString(),
						"}}!"
					}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					MetricsManager.LogEvent("Gameplay:Statistic:Purchase:" + ChosenStat);
					return;
				}
			}
			else
			{
				Popup.Show(text + "You have no attribute points to raise this attribute.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
		}

		// Token: 0x0600D017 RID: 53271 RVA: 0x004CA610 File Offset: 0x004C8810
		public ScreenReturn Show(GameObject GO)
		{
			GameManager.Instance.PushGameView("Status", true);
			ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(false);
			List<BaseMutation> list = StatusScreen.GetMutationList(GO);
			GenotypeEntry genotypeEntry = GO.genotypeEntry;
			string text = ((genotypeEntry != null) ? genotypeEntry.DisplayName : null);
			SubtypeEntry subtypeEntry = GO.subtypeEntry;
			string text2 = ((subtypeEntry != null) ? subtypeEntry.DisplayName : null);
			Keys keys = Keys.None;
			bool flag = false;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			string text3 = "< {{W|7}} Skills | Inventory {{W|9}} >";
			if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
			{
				text3 = string.Concat(new string[]
				{
					"< {{W|",
					ControlManager.getCommandInputDescription("Page Left", false, false, ControlManager.InputDeviceType.Unknown, false),
					"}} Skills | Inventory {{W|",
					ControlManager.getCommandInputDescription("Page Right", false, false, ControlManager.InputDeviceType.Unknown, false),
					"}} >"
				});
			}
			while (!flag)
			{
				Event.ResetPool(true);
				scrapBuffer.Clear();
				scrapBuffer.SingleBox(0, 0, 79, 24, ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
				scrapBuffer.Goto(30, 0);
				scrapBuffer.Write("[ {{W|Character Sheet}} ]", true, false, false, null, -1);
				scrapBuffer.Goto(60, 0);
				scrapBuffer.Write(" {{W|" + ControlManager.getCommandInputDescription("Cancel", false, false, ControlManager.InputDeviceType.Unknown, false) + "}} to exit ", true, false, false, null, -1);
				scrapBuffer.Goto(79 - ColorUtility.StripFormatting(text3).Length, 24);
				scrapBuffer.Write(text3, true, false, false, null, -1);
				if (StatusScreen.TreatAsMutant(GO))
				{
					string @for = GetMutationTermEvent.GetFor(GO, null);
					if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
					{
						scrapBuffer.WriteAt(4, 24, " [{{W|" + ControlManager.getCommandInputDescription("Accept", false, false, ControlManager.InputDeviceType.Unknown, false) + "}}] Raise", true);
					}
					else
					{
						scrapBuffer.WriteAt(4, 24, " [{{W|space}}] Raise selected statistic or " + @for + " ", true);
					}
					scrapBuffer.Goto(3, 22);
					if (num == 0 && num2 == 7)
					{
						scrapBuffer.Write("{{Y|>}} ", true, false, false, null, -1);
					}
					else
					{
						scrapBuffer.Write("  ", true, false, false, null, -1);
					}
					if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
					{
						scrapBuffer.Write("Buy a new random " + @for + " for 4 MP", true, false, false, null, -1);
					}
					else
					{
						scrapBuffer.Write("{{W|M}} - Buy a new random " + @for + " for 4 MP", true, false, false, null, -1);
					}
					if (PronounSet.EnableSelection)
					{
						scrapBuffer.Goto(3, 23);
						if (num == 0 && num2 == 8)
						{
							scrapBuffer.Write("{{Y|>}} ", true, false, false, null, -1);
						}
						else
						{
							scrapBuffer.Write("  ", true, false, false, null, -1);
						}
						scrapBuffer.Write("{{W|P}} - Change pronoun set", true, false, false, null, -1);
					}
				}
				else
				{
					scrapBuffer.Goto(4, 24);
					if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
					{
						scrapBuffer.Write(" [{{W|" + ControlManager.getCommandInputDescription("Accept", false, false, ControlManager.InputDeviceType.Unknown, false) + "}}] Raise", true, false, false, null, -1);
					}
					else
					{
						scrapBuffer.Write(" [{{W|space}}] Raise selected statistic ", true, false, false, null, -1);
					}
					if (PronounSet.EnableSelection)
					{
						scrapBuffer.Goto(3, 22);
						if (num == 0 && num2 == 7)
						{
							scrapBuffer.Write("{{Y|>}} ", true, false, false, null, -1);
						}
						else
						{
							scrapBuffer.Write("  ", true, false, false, null, -1);
						}
						scrapBuffer.Write("{{W|P}} - Change pronoun set", true, false, false, null, -1);
					}
				}
				StatusScreen.WriteStat(scrapBuffer, GO, "Strength", num == 0 && num2 == 0, 3, 2);
				StatusScreen.WriteStat(scrapBuffer, GO, "Agility", num == 0 && num2 == 1, 3, 3);
				StatusScreen.WriteStat(scrapBuffer, GO, "Toughness", num == 0 && num2 == 2, 3, 4);
				StatusScreen.WriteStat(scrapBuffer, GO, "Intelligence", num == 0 && num2 == 3, 3, 5);
				StatusScreen.WriteStat(scrapBuffer, GO, "Willpower", num == 0 && num2 == 4, 3, 6);
				StatusScreen.WriteStat(scrapBuffer, GO, "Ego", num == 0 && num2 == 5, 3, 7);
				scrapBuffer.Goto(3, 9);
				Gender gender = GO.GetGender(false);
				if (gender != null && gender.Name != "nonspecific")
				{
					scrapBuffer.Write(Grammar.MakeTitleCase(gender.Name) + " ", true, false, false, null, -1);
				}
				if (!text.IsNullOrEmpty())
				{
					scrapBuffer.Write(text + " ", true, false, false, null, -1);
				}
				if (!text2.IsNullOrEmpty())
				{
					scrapBuffer.Write(text2 + " ", true, false, false, null, -1);
				}
				scrapBuffer.Goto(3, 11);
				if (GO.Statistics.ContainsKey("AcidResistance"))
				{
					if (GO.Statistics["AcidResistance"].Value > 0)
					{
						scrapBuffer.Write("Acid Resist: {{G|" + GO.Statistics["AcidResistance"].Value.ToString() + "}}", true, false, false, null, -1);
					}
					else if (GO.Statistics["AcidResistance"].Value < 0)
					{
						scrapBuffer.Write("Acid Resist: {{R|" + GO.Statistics["AcidResistance"].Value.ToString() + "}}", true, false, false, null, -1);
					}
					else
					{
						scrapBuffer.Write("Acid Resist: " + GO.Statistics["AcidResistance"].Value.ToString(), true, false, false, null, -1);
					}
				}
				scrapBuffer.Goto(3, 12);
				if (GO.Statistics.ContainsKey("ColdResistance"))
				{
					if (GO.Statistics["ColdResistance"].Value > 0)
					{
						scrapBuffer.Write("Cold Resist: {{G|" + GO.Statistics["ColdResistance"].Value.ToString() + "}}", true, false, false, null, -1);
					}
					else if (GO.Statistics["ColdResistance"].Value < 0)
					{
						scrapBuffer.Write("Cold Resist: {{R|" + GO.Statistics["ColdResistance"].Value.ToString() + "}}", true, false, false, null, -1);
					}
					else
					{
						scrapBuffer.Write("Cold Resist: " + GO.Statistics["ColdResistance"].Value.ToString(), true, false, false, null, -1);
					}
				}
				scrapBuffer.Goto(21, 11);
				if (GO.Statistics.ContainsKey("ElectricResistance"))
				{
					if (GO.Statistics["ElectricResistance"].Value > 0)
					{
						scrapBuffer.Write("Electrical Resist: {{G|" + GO.Statistics["ElectricResistance"].Value.ToString() + "}}", true, false, false, null, -1);
					}
					else if (GO.Statistics["ElectricResistance"].Value < 0)
					{
						scrapBuffer.Write("Electrical Resist: {{R|" + GO.Statistics["ElectricResistance"].Value.ToString() + "}}", true, false, false, null, -1);
					}
					else
					{
						scrapBuffer.Write("Electrical Resist: " + GO.Statistics["ElectricResistance"].Value.ToString(), true, false, false, null, -1);
					}
				}
				scrapBuffer.Goto(21, 12);
				if (GO.Statistics.ContainsKey("HeatResistance"))
				{
					if (GO.Statistics["HeatResistance"].Value > 0)
					{
						scrapBuffer.Write("Heat Resist: {{G|" + GO.Statistics["HeatResistance"].Value.ToString() + "}}", true, false, false, null, -1);
					}
					else if (GO.Statistics["HeatResistance"].Value < 0)
					{
						scrapBuffer.Write("Heat Resist: {{R|" + GO.Statistics["HeatResistance"].Value.ToString() + "}}", true, false, false, null, -1);
					}
					else
					{
						scrapBuffer.Write("Heat Resist: " + GO.Statistics["HeatResistance"].Value.ToString(), true, false, false, null, -1);
					}
				}
				scrapBuffer.Goto(3, 14);
				if (num == 0 && num2 == 6)
				{
					scrapBuffer.Write("{{Y|>}} ", true, false, false, null, -1);
				}
				else
				{
					scrapBuffer.Write("  ", true, false, false, null, -1);
				}
				scrapBuffer.Write("active {{W|e}}ffects [{{C|" + GO.Effects.Count<Effect>((Effect e) => e != null && !string.IsNullOrEmpty(e.GetDescription())).ToString() + "}}]", true, false, false, null, -1);
				int num4 = 0;
				int num5 = 2;
				bool flag2 = list != null && list.Count > 0;
				int psychicGlimmer = GO.GetPsychicGlimmer(null);
				if (flag2)
				{
					string text4;
					string text5;
					GetMutationTermEvent.GetFor(GO, out text4, out text5, null);
					string text6 = ColorUtility.CapitalizeExceptFormatting(Grammar.Pluralize(text4));
					scrapBuffer.WriteAt(45, 2, string.Concat(new string[] { "[ {{", text5, "|", text6, "}} ]" }), true);
					num5 = 4;
					if (num3 > 0)
					{
						scrapBuffer.Goto(45, 3);
						scrapBuffer.Write("{{W|<more...>}}", true, false, false, null, -1);
					}
					int num6 = 4;
					int num7 = num3;
					while (num7 < list.Count && num6 < 16)
					{
						BaseMutation baseMutation = list[num7];
						int level = baseMutation.Level;
						num5++;
						scrapBuffer.Goto(45, num6);
						if (num == 1 && num2 == num7)
						{
							scrapBuffer.Write("{{Y|>}} ", true, false, false, null, -1);
						}
						else
						{
							scrapBuffer.Write("  ", true, false, false, null, -1);
						}
						if (!baseMutation.ShouldShowLevel())
						{
							scrapBuffer.Write(baseMutation.GetDisplayName(true), true, false, false, null, -1);
						}
						else
						{
							string text7 = "C";
							if (level > baseMutation.BaseLevel)
							{
								if (level > baseMutation.GetMutationCap())
								{
									text7 = "M";
								}
								else
								{
									text7 = "G";
								}
							}
							else if (level < baseMutation.BaseLevel)
							{
								text7 = "R";
							}
							scrapBuffer.Write(Event.NewStringBuilder(null).Append(baseMutation.GetDisplayName(true)).Append(" ({{")
								.Append(text7)
								.Append('|')
								.Append(level)
								.Append("}})")
								.ToString(), true, false, false, null, -1);
						}
						num4++;
						num6++;
						num7++;
					}
					if (num7 < list.Count)
					{
						scrapBuffer.Goto(45, num6);
						scrapBuffer.Write("{{W|<more...>}}", true, false, false, null, -1);
					}
					if (PsychicGlimmer.Perceptible(psychicGlimmer))
					{
						if (num == 1 && num2 == list.Count)
						{
							scrapBuffer.Goto(45, num6 + 1);
							scrapBuffer.Write(">", true, false, false, null, -1);
						}
						scrapBuffer.Goto(47, num6 + 1);
						scrapBuffer.Write("Psychic Glimmer ({{R|" + psychicGlimmer.ToString() + "}})", true, false, false, null, -1);
						num5++;
					}
					num5 += 2;
				}
				if (!flag2 && PsychicGlimmer.Perceptible(psychicGlimmer))
				{
					scrapBuffer.Goto(45, num5);
					scrapBuffer.Write("[ {{M|Esoterica}} ]", true, false, false, null, -1);
					if (num == 1 && num2 == num4)
					{
						scrapBuffer.Goto(45, num5 + 2);
						scrapBuffer.Write(">", true, false, false, null, -1);
					}
					scrapBuffer.Goto(47, num5 + 2);
					scrapBuffer.Write("Psychic Glimmer ({{R|" + psychicGlimmer.ToString() + "}})", true, false, false, null, -1);
				}
				string text8;
				if (GO.GetIntProperty("Analgesia", 0) > 0)
				{
					text8 = Strings.WoundLevel(GO);
				}
				else
				{
					text8 = string.Concat(new string[]
					{
						"{{C|",
						GO.hitpoints.ToString(),
						"}} / {{C|",
						GO.baseHitpoints.ToString(),
						"}}"
					});
				}
				scrapBuffer.WriteAt(3, 16, "HP: " + text8, true);
				if (GO.HasStat("Level"))
				{
					scrapBuffer.WriteAt(3, 17, "Level: {{C|" + GO.Stat("Level", 0).ToString() + "}}", true);
				}
				if (GO.HasStat("XP") && GO.HasStat("Level"))
				{
					scrapBuffer.WriteAt(3, 18, string.Concat(new string[]
					{
						"XP: {{C|",
						GO.Stat("XP", 0).ToString(),
						"}} {{K|({{c|",
						(Leveler.GetXPForLevel(GO.Stat("Level", 0) + 1) - GO.Stat("XP", 0)).ToString(),
						"}} till next level)}}"
					}), true);
				}
				string text9 = "G";
				string text10 = "G";
				string text11 = "G";
				if (GO.Stat("SP", 0) == 0)
				{
					text9 = "K";
				}
				if (GO.Stat("AP", 0) == 0)
				{
					text10 = "K";
				}
				if (GO.Stat("MP", 0) == 0)
				{
					text11 = "K";
				}
				if (GO.HasStat("SP") && GO.HasStat("AP"))
				{
					if (StatusScreen.TreatAsMutant(GO))
					{
						scrapBuffer.WriteAt(3, 20, string.Concat(new string[]
						{
							"Skill points: {{",
							text9,
							"|",
							GO.Stat("SP", 0).ToString(),
							"}}, Attribute points: {{",
							text10,
							"|",
							GO.Stat("AP", 0).ToString(),
							"}}, Mutation points: {{",
							text11,
							"|",
							GO.Stat("MP", 0).ToString(),
							"}}"
						}), true);
					}
					else
					{
						scrapBuffer.WriteAt(3, 20, string.Concat(new string[]
						{
							"Skill points: {{",
							text9,
							"|",
							GO.Stat("SP", 0).ToString(),
							"}}, Attribute points: {{",
							text10,
							"|",
							GO.Stat("AP", 0).ToString(),
							"}}"
						}), true);
					}
				}
				Popup._TextConsole.DrawBuffer(scrapBuffer, null, false);
				keys = Keyboard.getvk(Options.MapDirectionsToKeypad, false, true);
				if (num == 3)
				{
					if (keys == Keys.NumPad4)
					{
						num2 = 0;
						num = 0;
					}
					if (keys == Keys.NumPad8)
					{
						num = 1;
					}
					if (keys == Keys.Space || keys == Keys.Enter)
					{
						Popup.Show("TODOJASON GLIMMER=" + GO.GetPsychicGlimmer(null).ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
				}
				else if (num == 1)
				{
					if (keys == Keys.NumPad4)
					{
						num = 0;
						num2 = 0;
					}
					if (keys == Keys.NumPad8)
					{
						if (num2 > 0)
						{
							num2--;
						}
						if (num3 > num2)
						{
							num3 = num2;
						}
					}
					if (keys == Keys.NumPad2)
					{
						if (num2 < list.Count - 1)
						{
							num2++;
							if (num3 + 12 <= num2)
							{
								num3 = num2 - 12 + 1;
							}
						}
						else if (num2 == list.Count - 1 && PsychicGlimmer.Perceptible(psychicGlimmer))
						{
							num2++;
						}
					}
					if (keys == Keys.Space || keys == Keys.Enter)
					{
						if (num2 >= list.Count)
						{
							Popup.Show(PsychicHunterSystem.GetPsychicGlimmerDescription(psychicGlimmer), null, "Sounds/UI/ui_notification", true, true, true, false, null);
						}
						else
						{
							StatusScreen.ShowMutationPopup(GO, list[num2]);
						}
					}
				}
				if (num == 0)
				{
					if (keys == Keys.NumPad6 && (list.Count > 0 || PsychicGlimmer.Perceptible(psychicGlimmer)))
					{
						num = 1;
						num2 = 0;
					}
					if (keys == Keys.NumPad8 && num2 > 0)
					{
						num2--;
					}
					if (keys == Keys.NumPad2)
					{
						int num8 = 6;
						if (StatusScreen.TreatAsMutant(GO))
						{
							num8++;
						}
						if (PronounSet.EnableSelection)
						{
							num8++;
						}
						if (num2 < num8)
						{
							num2++;
						}
					}
					if (keys == Keys.Space || keys == Keys.Enter)
					{
						string text12;
						if (num2 == 0)
						{
							text12 = "Strength";
						}
						else if (num2 == 1)
						{
							text12 = "Agility";
						}
						else if (num2 == 2)
						{
							text12 = "Toughness";
						}
						else if (num2 == 3)
						{
							text12 = "Intelligence";
						}
						else if (num2 == 4)
						{
							text12 = "Willpower";
						}
						else if (num2 == 5)
						{
							text12 = "Ego";
						}
						else
						{
							text12 = "";
						}
						if (text12 != "")
						{
							StatusScreen.BuyStat(GO, text12);
						}
					}
				}
				if (keys == Keys.E || ((keys == Keys.Space || keys == Keys.Enter) && num == 0 && num2 == 6))
				{
					GO.ShowActiveEffects();
				}
				if ((keys == Keys.P || ((keys == Keys.Space || keys == Keys.Enter) && num == 0 && num2 == (GO.IsTrueKin() ? 7 : 8))) && PronounSet.EnableSelection)
				{
					PronounAndGenderSets.ShowChangePronounSet(GO);
				}
				GO.Stat("MP", 0);
				if ((keys == Keys.M || ((keys == Keys.Space || keys == Keys.Enter) && num == 0 && StatusScreen.TreatAsMutant(GO) && num2 == 7)) && MutationsAPI.BuyRandomMutation(GO, 4, true, null))
				{
					list = StatusScreen.GetMutationList(GO);
				}
				if (keys == Keys.NumPad7 || keys == Keys.NumPad9)
				{
					flag = true;
				}
				else if (keys == Keys.Escape || keys == Keys.NumPad5)
				{
					flag = true;
				}
			}
			if (keys == Keys.NumPad7 || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Page Left"))
			{
				GameManager.Instance.PopGameView(false);
				return ScreenReturn.Previous;
			}
			if (keys == Keys.NumPad9 || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Page Right"))
			{
				GameManager.Instance.PopGameView(false);
				return ScreenReturn.Next;
			}
			GameManager.Instance.PopGameView(false);
			return ScreenReturn.Exit;
		}

		// Token: 0x0600D018 RID: 53272 RVA: 0x004CB7D7 File Offset: 0x004C99D7
		public static bool TreatAsMutant(GameObject GO)
		{
			return GO != null && (GO.IsMutant() || GO.Stat("MP", 0) > 0);
		}

		// Token: 0x0600D019 RID: 53273 RVA: 0x004CB7F7 File Offset: 0x004C99F7
		public static List<BaseMutation> GetMutationList(GameObject GO)
		{
			if (GO == null)
			{
				return new List<BaseMutation>();
			}
			return GO.GetPartsDescendedFrom<BaseMutation>((BaseMutation m) => m.Level > 0);
		}

		// Token: 0x0600D01A RID: 53274 RVA: 0x004CB828 File Offset: 0x004C9A28
		public static void ShowMutationPopup(GameObject GO, BaseMutation Mutation)
		{
			object obj = StatusScreen.TreatAsMutant(GO) && Mutation.BaseLevel > 0;
			string @for = GetMutationTermEvent.GetFor(GO, Mutation);
			bool flag = !Mutation.CanLevel() || Mutation.BaseLevel >= Mutation.GetMaxLevel();
			StringBuilder stringBuilder = Event.NewStringBuilder(Mutation.GetDescription());
			object obj2 = obj;
			if (obj2 == 0 || flag)
			{
				stringBuilder.Compound(Mutation.GetLevelText(Mutation.Level), "\n\n");
			}
			else
			{
				string levelText = Mutation.GetLevelText(Mutation.Level);
				string levelText2 = Mutation.GetLevelText(Mutation.Level + 1);
				if (!string.IsNullOrEmpty(levelText))
				{
					stringBuilder.Compound("{{w|This rank}}:\n", "\n\n").Append(Mutation.GetLevelText(Mutation.Level));
				}
				if (!string.IsNullOrEmpty(levelText2))
				{
					stringBuilder.Append("\n\n{{w|Next rank}}:\n").Append(Mutation.GetLevelText(Mutation.Level + 1));
				}
			}
			stringBuilder.Append('\n', 2);
			StatusScreen.AppendRankBoost(stringBuilder, GO, Mutation);
			if (obj2 != null)
			{
				if (!flag && Mutation.Level < Mutation.GetMutationCap())
				{
					if (GO.Stat("MP", 0) >= 1)
					{
						stringBuilder.Append("It will cost {{C|1}} mutation point to increase ").Append(Mutation.GetDisplayName(true)).Append("'s rank by 1.\nDo you wish to increase this ")
							.Append(Grammar.MakePossessive(@for))
							.Append(" rank?");
						if (Popup.ShowYesNo(stringBuilder.ToString(), "Sounds/UI/ui_notification", true, DialogResult.Yes, null) != DialogResult.Yes)
						{
							return;
						}
						SoundManager.PlayUISound("Sounds/Misc/sfx_characterMod_mutation_rankUp", 1f, false, false, SoundRequest.SoundEffectType.None);
						GO.GetPart<Mutations>().LevelMutation(Mutation, Mutation.BaseLevel + 1);
						stringBuilder.Clear().Append("You have increased ").Append(Grammar.MakePossessive(Mutation.GetDisplayName(true)))
							.Append(" base rank to {{C|")
							.Append(Mutation.BaseLevel)
							.Append("}}!")
							.Append('\n', 2);
						StatusScreen.AppendRankBoost(stringBuilder, GO, Mutation);
						GO.UseMP(1, "default");
					}
					else
					{
						stringBuilder.Append("{{C|You do not have enough mutation points to increase that " + Grammar.MakePossessive(@for) + " rank.}}");
					}
				}
				else if (Mutation.CanLevel())
				{
					stringBuilder.Append("{{C|You may not advance this " + Grammar.MakePossessive(@for) + " rank yet.}}");
				}
			}
			Popup.Show(stringBuilder.ToString().TrimEnd(), null, "Sounds/UI/ui_notification", true, true, true, false, null);
		}

		// Token: 0x0600D01B RID: 53275 RVA: 0x004CBA78 File Offset: 0x004C9C78
		private static void AppendRankBoost(StringBuilder SB, GameObject GO, BaseMutation Mutation)
		{
			foreach (BaseMutation.LevelCalculation levelCalculation in Mutation.GetLevelCalculations())
			{
				if (!string.IsNullOrEmpty(levelCalculation.reason))
				{
					SB.AppendMarkupNode(StatusScreen.RankBoostSigilColorCodes[levelCalculation.sigil], levelCalculation.reason);
					SB.AppendLine();
				}
			}
			if (!Mutation.GetLevelCalculations().IsNullOrEmpty<BaseMutation.LevelCalculation>())
			{
				SB.AppendLine();
			}
		}

		// Token: 0x0600D01C RID: 53276 RVA: 0x004CBB0C File Offset: 0x004C9D0C
		public static bool BuyRandomMutation(GameObject GO)
		{
			Mutations part = GO.GetPart<Mutations>();
			List<MutationEntry> Selections = part.GetMutatePool(null, false);
			GO.WithSeededRandom<List<MutationEntry>>((Random rng) => Selections.ShuffleInPlace<MutationEntry>(rng), "RandomMutationBuy");
			int TargetSelectionCount = GlobalConfig.GetIntSetting("RandomBuyMutationCount", 3);
			TargetSelectionCount = GetRandomBuyMutationCountEvent.GetFor(GO, TargetSelectionCount);
			if (Selections.Count > TargetSelectionCount)
			{
				List<MutationEntry> list = new List<MutationEntry>(Selections.Where<MutationEntry>((MutationEntry e) => e.Cost >= 2));
				if (list.Count < TargetSelectionCount)
				{
					int filler = TargetSelectionCount - list.Count;
					list.AddRange(Selections.Where<MutationEntry>(delegate(MutationEntry e)
					{
						if (e.Cost < 2)
						{
							int filler2 = filler;
							filler = filler2 - 1;
							return filler2 > 0;
						}
						return false;
					}));
				}
				Selections = list;
			}
			if (Selections.Count > TargetSelectionCount)
			{
				Selections.RemoveRange(TargetSelectionCount, Selections.Count - TargetSelectionCount);
			}
			List<int> extraLimb = new List<int>();
			if (GO.IsChimera())
			{
				int @for = GetRandomBuyChimericBodyPartRollsEvent.GetFor(GO, 1);
				Action<Random> <>9__3;
				for (int i = 0; i < @for; i++)
				{
					Action<Random> action;
					if ((action = <>9__3) == null)
					{
						action = (<>9__3 = delegate(Random rng)
						{
							extraLimb.Add(rng.Next(0, TargetSelectionCount));
						});
					}
					GO.WithSeededRandom(action, "RandomMutationBuy");
				}
			}
			string for2 = GetMutationTermEvent.GetFor(GO, null);
			if (Selections.Count > 0)
			{
				string[] array = new string[Selections.Count];
				List<BaseMutation> list2 = new List<BaseMutation>(Selections.Count);
				for (int j = 0; j < Selections.Count; j++)
				{
					list2.Add(Selections[j].Mutation);
					array[j] = string.Concat(new string[]
					{
						"{{W|",
						list2[j].GetDisplayName(true),
						"}}",
						extraLimb.Contains(j) ? "{{G| + grow a new body part}}" : "",
						" {{y|- ",
						list2[j].GetDescription(),
						"}}\n",
						list2[j].GetLevelText(1)
					});
				}
				int num = 0;
				BaseMutation baseMutation;
				for (;;)
				{
					num = Popup.PickOption("", "Choose " + Grammar.A(for2, false) + ".", "", "Sounds/Misc/sfx_characterMod_mutation_windowPopup", array, null, null, null, null, null, null, 1, 78, num, -1, false, false, false, true, false, null, null);
					if (num >= 0)
					{
						baseMutation = Selections[num].CreateInstance();
						if (!baseMutation.Variant.IsNullOrEmpty() || !baseMutation.HasVariants || !baseMutation.CanSelectVariant)
						{
							break;
						}
						baseMutation.SelectVariant(GO, true);
						if (!baseMutation.Variant.IsNullOrEmpty())
						{
							break;
						}
					}
				}
				Popup.Show("You gain " + baseMutation.GetDisplayName(true) + "!", null, "Sounds/Misc/sfx_characterMod_mutation_positive", true, true, true, true, null);
				part.AddMutation(baseMutation, 1, true);
				try
				{
					if (!GO.HasEffect<Dominated>())
					{
						Zone currentZone = The.Player.CurrentZone;
						string text = ((currentZone != null) ? currentZone.GetTerrainDisplayName(true) : null);
						if (text.IsNullOrEmpty())
						{
							text = "under the salt sun";
						}
						else
						{
							text = "around " + text;
						}
						JournalAPI.AddAccomplishment(string.Concat(new string[]
						{
							"Your genome destabilized and you gained the ",
							list2[num].GetDisplayName(true),
							" ",
							for2,
							"."
						}), HistoricStringExpander.ExpandString(string.Concat(new string[]
						{
							"<spice.commonPhrases.oneStarryNight.!random.capitalize>, =name= manifested a latent power inside ",
							GO.GetPronounProvider(false).Objective,
							" and joined the divine ranks of ",
							list2[num].GetBearerDescription(),
							"."
						}), null), string.Concat(new string[]
						{
							"While wandering ",
							text,
							", =name= stumbled upon a clan of ",
							list2[num].GetBearerDescription(),
							". Because of ",
							Grammar.MakePossessive(The.Player.BaseDisplayNameStripped),
							" <spice.elements.",
							The.Player.GetMythicDomain(5, null),
							".quality.!random>, they accepted ",
							The.Player.GetPronounProvider(false).Objective,
							" into their fold and taught ",
							The.Player.GetPronounProvider(false).Objective,
							" their secrets."
						}), null, "general", MuralCategory.BodyExperienceGood, MuralWeight.Medium, null, -1L, true);
					}
					if (extraLimb.Contains(num))
					{
						part.AddChimericBodyPart(false, "Chimera", null);
					}
					MetricsManager.LogEvent("Gameplay:Mutation:Purchase:" + list2[num].Name);
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("Post AddMutation", ex, "game_exception");
				}
				return true;
			}
			Popup.Show("You have all available " + Grammar.Pluralize(for2) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			return false;
		}

		// Token: 0x04005196 RID: 20886
		public const int MAX_MUTATION_SHOW = 12;

		// Token: 0x04005197 RID: 20887
		private static readonly Dictionary<char, string> RankBoostSigilColorCodes = new Dictionary<char, string>
		{
			{ '\0', "C" },
			{ '-', "R" },
			{ '+', "G" }
		};
	}
}
