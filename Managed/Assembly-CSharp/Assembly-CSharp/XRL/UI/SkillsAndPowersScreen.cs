using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using XRL.World;
using XRL.World.Parts;
using XRL.World.Parts.Skill;
using XRL.World.Skills;

namespace XRL.UI
{
	// Token: 0x020014AD RID: 5293
	public class SkillsAndPowersScreen : BaseScreen, IScreen
	{
		// Token: 0x0600CFFE RID: 53246 RVA: 0x004C820C File Offset: 0x004C640C
		public static bool HasAnyPower(GameObject GO, SkillEntry Skill)
		{
			foreach (PowerEntry powerEntry in Skill.Powers.Values)
			{
				if (GO.HasPart(powerEntry.Class))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600CFFF RID: 53247 RVA: 0x004C8274 File Offset: 0x004C6474
		private static int SkillNodePos(SkillEntry Skill)
		{
			if (SkillsAndPowersScreen.Nodes != null)
			{
				int i = 0;
				int count = SkillsAndPowersScreen.Nodes.Count;
				while (i < count)
				{
					if (SkillsAndPowersScreen.Nodes[i].Skill == Skill)
					{
						return i;
					}
					i++;
				}
			}
			return -1;
		}

		// Token: 0x0600D000 RID: 53248 RVA: 0x004C82B8 File Offset: 0x004C64B8
		private static int PowerNodePos(PowerEntry Power)
		{
			if (SkillsAndPowersScreen.Nodes != null)
			{
				int i = 0;
				int count = SkillsAndPowersScreen.Nodes.Count;
				while (i < count)
				{
					if (SkillsAndPowersScreen.Nodes[i].Power == Power)
					{
						return i;
					}
					i++;
				}
			}
			return -1;
		}

		// Token: 0x0600D001 RID: 53249 RVA: 0x004C82FC File Offset: 0x004C64FC
		private static SPNode OldSkillNode(List<SPNode> OldNodes, SkillEntry Skill)
		{
			if (OldNodes != null)
			{
				int i = 0;
				int count = OldNodes.Count;
				while (i < count)
				{
					if (OldNodes[i].Skill == Skill)
					{
						return OldNodes[i];
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x0600D002 RID: 53250 RVA: 0x004C8338 File Offset: 0x004C6538
		private static SPNode OldPowerNode(List<SPNode> OldNodes, PowerEntry Power)
		{
			if (OldNodes != null)
			{
				int i = 0;
				int count = OldNodes.Count;
				while (i < count)
				{
					if (OldNodes[i].Power == Power)
					{
						return OldNodes[i];
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x0600D003 RID: 53251 RVA: 0x004C8374 File Offset: 0x004C6574
		private static void AddSkillNodes(GameObject GO, SkillEntry Skill, List<SPNode> OldNodes, bool Expand)
		{
			int num = SkillsAndPowersScreen.SkillNodePos(Skill);
			XRLGame game = The.Game;
			if (game != null && game.HasBooleanGameState("UI_Expand_Skill_" + Skill.Name))
			{
				XRLGame game2 = The.Game;
				Expand = ((game2 != null) ? game2.GetBooleanGameState("UI_Expand_Skill_" + Skill.Name, false) : Expand);
			}
			SPNode spnode;
			if (num == -1)
			{
				num = SkillsAndPowersScreen.Nodes.Count;
				spnode = SkillsAndPowersScreen.OldSkillNode(OldNodes, Skill) ?? new SPNode(Skill, null, Expand, null);
				SkillsAndPowersScreen.Nodes.Add(spnode);
			}
			else
			{
				spnode = SkillsAndPowersScreen.Nodes[num];
			}
			int num2 = num;
			foreach (PowerEntry powerEntry in Skill.Powers.Values)
			{
				if (!Skill.Hidden || GO.HasSkill(powerEntry.Class))
				{
					int num3 = SkillsAndPowersScreen.PowerNodePos(powerEntry);
					if (num3 == -1)
					{
						SPNode spnode2 = SkillsAndPowersScreen.OldPowerNode(OldNodes, powerEntry);
						SPNode spnode3 = spnode2 ?? new SPNode(null, powerEntry, true, spnode);
						if (num2 >= SkillsAndPowersScreen.Nodes.Count - 1)
						{
							num3 = SkillsAndPowersScreen.Nodes.Count;
							SkillsAndPowersScreen.Nodes.Add(spnode3);
						}
						else
						{
							num3 = num2 + 1;
							SkillsAndPowersScreen.Nodes.Insert(num3, spnode3);
						}
						if (spnode2 == null && !spnode.Expand && Expand)
						{
							spnode.Expand = true;
						}
					}
					num2 = num3;
				}
			}
		}

		// Token: 0x0600D004 RID: 53252 RVA: 0x004C84F4 File Offset: 0x004C66F4
		public static void SelectNode(SPNode node, GameObject GO)
		{
			SkillEntry skillEntry = null;
			PowerEntry powerEntry = null;
			string text;
			string text2;
			string text3;
			int num;
			if (node.Power == null)
			{
				text = node.Skill.Name;
				text2 = node.Skill.Class;
				text3 = "skill";
				num = node.Skill.Cost;
				skillEntry = node.Skill;
			}
			else if (!GO.HasSkill(node.ParentNode.Skill.Class))
			{
				if (Popup.ShowYesNoCancel("You do not have the skill associated with that power. Would you like to purchase the required skill?", "Sounds/UI/ui_notification", true, DialogResult.Cancel) != DialogResult.Yes)
				{
					return;
				}
				text = node.ParentNode.Skill.Name;
				text2 = node.ParentNode.Skill.Class;
				text3 = "skill";
				num = node.ParentNode.Skill.Cost;
				skillEntry = node.ParentNode.Skill;
			}
			else
			{
				text = node.Power.Name;
				text2 = node.Power.Class;
				text3 = "power";
				num = node.Power.Cost;
				powerEntry = node.Power;
			}
			if (GO.HasPart(text2))
			{
				Popup.Show("You already have that " + text3 + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				return;
			}
			if ((powerEntry != null && powerEntry.IsSkillInitiatory) || (skillEntry != null && skillEntry.Initiatory))
			{
				Popup.Show("You must be initiated into this " + text3 + " in order to learn it.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				return;
			}
			if (GO.Stat("SP", 0) < num)
			{
				Popup.Show("You don't have enough skill points to buy that " + text3 + "!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				return;
			}
			if ((powerEntry == null || powerEntry.MeetsRequirements(GO, true)) && (skillEntry == null || skillEntry.MeetsRequirements(GO, true)))
			{
				string text4 = "XRL.World.Parts.Skill." + text2;
				Type type = ModManager.ResolveType(text4, false, false, true);
				if (type == null)
				{
					Popup.Show("No implementation for " + text4, null, "Sounds/UI/ui_notification", true, true, true, true, null);
					return;
				}
				if (text3 != "power")
				{
					foreach (PowerEntry powerEntry2 in skillEntry.Powers.Values)
					{
						if (powerEntry2.Cost == 0 && !powerEntry2.MeetsAttributeMinimum(GO, true))
						{
							return;
						}
					}
				}
				if (Popup.ShowYesNo(string.Concat(new string[]
				{
					"Are you sure you want to buy ",
					text,
					" for {{C|",
					num.ToString(),
					"}} sp?"
				}), "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes)
				{
					BaseSkill baseSkill = Activator.CreateInstance(type) as BaseSkill;
					GO.GetPart<Skills>().AddSkill(baseSkill, null, null);
					GO.GetStat("SP").Penalty += num;
					if (node.Skill != null)
					{
						node.Expand = true;
					}
					if (!string.IsNullOrEmpty(text))
					{
						MetricsManager.LogEvent("Gameplay:Skill:Purchase:" + text);
					}
				}
			}
		}

		// Token: 0x0600D005 RID: 53253 RVA: 0x004C87F0 File Offset: 0x004C69F0
		public static void BuildNodes(GameObject GO, bool Rebuild = false)
		{
			List<SPNode> nodes = SkillsAndPowersScreen.Nodes;
			if (Rebuild || SkillsAndPowersScreen.Nodes == null)
			{
				SkillsAndPowersScreen.Nodes = new List<SPNode>();
			}
			if (GO != null)
			{
				foreach (SkillEntry skillEntry in SkillFactory.Factory.SkillList.Values)
				{
					bool flag = GO.HasSkill(skillEntry.Class);
					if ((!skillEntry.Hidden || flag) && (flag || SkillsAndPowersScreen.HasAnyPower(GO, skillEntry)))
					{
						SkillsAndPowersScreen.AddSkillNodes(GO, skillEntry, nodes, true);
					}
				}
			}
			if (GO != null)
			{
				foreach (SkillEntry skillEntry2 in SkillFactory.Factory.SkillList.Values)
				{
					bool flag2 = GO.HasSkill(skillEntry2.Class);
					if ((!skillEntry2.Hidden || flag2) && !flag2 && !SkillsAndPowersScreen.HasAnyPower(GO, skillEntry2))
					{
						SkillsAndPowersScreen.AddSkillNodes(GO, skillEntry2, nodes, false);
					}
				}
			}
		}

		// Token: 0x0600D006 RID: 53254 RVA: 0x004C890C File Offset: 0x004C6B0C
		public ScreenReturn Show(GameObject GO)
		{
			GameManager.Instance.PushGameView("SkillsAndPowers", true);
			ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(false);
			bool flag = false;
			Keys keys = Keys.None;
			int num = 0;
			int num2 = 0;
			SkillsAndPowersScreen.BuildNodes(GO, true);
			int num3 = 3;
			foreach (SPNode spnode in SkillsAndPowersScreen.Nodes)
			{
				string description = spnode.Description;
				if (description != null)
				{
					TextBlock textBlock = new TextBlock(description.Replace("\r\n", "\n"), 73, 12, false, 0);
					if (textBlock.Lines.Count > num3)
					{
						num3 = textBlock.Lines.Count;
					}
				}
			}
			string text = "< {{W|7}} Tinkering | Character {{W|9}} >";
			if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
			{
				text = string.Concat(new string[]
				{
					"< {{W|",
					ControlManager.getCommandInputDescription("Page Left", false, false, ControlManager.InputDeviceType.Unknown, false),
					"}} Tinkering | Character {{W|",
					ControlManager.getCommandInputDescription("Page Right", false, false, ControlManager.InputDeviceType.Unknown, false),
					"}} >"
				});
			}
			while (!flag)
			{
				int num6;
				for (;;)
				{
					Event.ResetPool(true);
					scrapBuffer.Clear();
					scrapBuffer.SingleBox(0, 0, 79, 24, ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
					scrapBuffer.Goto(13, 0);
					scrapBuffer.Write("[ {{W|Buy Skills}} - {{C|" + GO.Stat("SP", 0).ToString() + "}}sp remaining ]", true, false, false, null, -1);
					scrapBuffer.Goto(79 - ColorUtility.StripFormatting(text).Length, 24);
					scrapBuffer.Write(text, true, false, false, null, -1);
					scrapBuffer.Goto(60, 0);
					scrapBuffer.Write(" {{W|" + ControlManager.getCommandInputDescription("Cancel", false, false, ControlManager.InputDeviceType.Unknown, false) + "}} to exit ", true, false, false, null, -1);
					if (num2 < num)
					{
						num = num2;
					}
					int num4 = 2;
					int num5 = num;
					num6 = 0;
					while (num4 <= 22 - num3 && num5 <= SkillsAndPowersScreen.Nodes.Count)
					{
						while (num5 < SkillsAndPowersScreen.Nodes.Count && !SkillsAndPowersScreen.Nodes[num5].Visible)
						{
							num5++;
						}
						if (num5 < SkillsAndPowersScreen.Nodes.Count)
						{
							if (SkillsAndPowersScreen.Nodes[num5].Skill != null)
							{
								scrapBuffer.Goto(4, num4);
								if (SkillsAndPowersScreen.Nodes[num5].Expand)
								{
									scrapBuffer.Write("[-] ", true, false, false, null, -1);
								}
								else
								{
									scrapBuffer.Write("[+] ", true, false, false, null, -1);
								}
								bool flag2 = SkillsAndPowersScreen.HasAnyPower(GO, SkillsAndPowersScreen.Nodes[num5].Skill);
								if (GO.HasPart(SkillsAndPowersScreen.Nodes[num5].Skill.Class))
								{
									scrapBuffer.Write("{{W|" + SkillsAndPowersScreen.Nodes[num5].Skill.Name + "}}", true, false, false, null, -1);
								}
								else if (SkillsAndPowersScreen.Nodes[num5].Skill.Initiatory)
								{
									scrapBuffer.Write("{{w|" + SkillsAndPowersScreen.Nodes[num5].Skill.Name + "}}", true, false, false, null, -1);
								}
								else if (flag2)
								{
									if (SkillsAndPowersScreen.Nodes[num5].Skill.Cost <= GO.Stat("SP", 0))
									{
										scrapBuffer.Write(string.Concat(new string[]
										{
											"[{{C|",
											SkillsAndPowersScreen.Nodes[num5].Skill.Cost.ToString(),
											"}}sp] {{W|",
											SkillsAndPowersScreen.Nodes[num5].Skill.Name,
											"}}"
										}), true, false, false, null, -1);
									}
									else
									{
										scrapBuffer.Write(string.Concat(new string[]
										{
											"[{{R|",
											SkillsAndPowersScreen.Nodes[num5].Skill.Cost.ToString(),
											"}}sp] {{W|",
											SkillsAndPowersScreen.Nodes[num5].Skill.Name,
											"}}"
										}), true, false, false, null, -1);
									}
								}
								else if (SkillsAndPowersScreen.Nodes[num5].Skill.Cost <= GO.Stat("SP", 0))
								{
									scrapBuffer.Write(string.Concat(new string[]
									{
										"[{{C|",
										SkillsAndPowersScreen.Nodes[num5].Skill.Cost.ToString(),
										"}}sp] {{w|",
										SkillsAndPowersScreen.Nodes[num5].Skill.Name,
										"}}"
									}), true, false, false, null, -1);
								}
								else
								{
									scrapBuffer.Write(string.Concat(new string[]
									{
										"[{{R|",
										SkillsAndPowersScreen.Nodes[num5].Skill.Cost.ToString(),
										"}}sp] {{w|",
										SkillsAndPowersScreen.Nodes[num5].Skill.Name,
										"}}"
									}), true, false, false, null, -1);
								}
							}
							else
							{
								scrapBuffer.Goto(4, num4);
								string text2 = "";
								PowerEntry power = SkillsAndPowersScreen.Nodes[num5].Power;
								if (GO.HasPart(power.Class))
								{
									scrapBuffer.Write(" - {{G|" + power.Name + "}}", true, false, false, null, -1);
								}
								else
								{
									if (power.Requires != null)
									{
										foreach (string text3 in power.Requires.CachedCommaExpansion())
										{
											string text4 = text3;
											bool flag3 = false;
											IBaseSkillEntry baseSkillEntry;
											if (SkillFactory.Factory.TryGetFirstEntry(text3, out baseSkillEntry))
											{
												if (power.IsSkillInitiatory)
												{
													int num7 = power.ParentSkill.PowerList.IndexOf(power);
													if (num7 > 0 && power.ParentSkill.PowerList[num7 - 1] == baseSkillEntry)
													{
														continue;
													}
												}
												text4 = baseSkillEntry.Name;
												flag3 = GO.HasSkill(text3);
											}
											else if (MutationFactory.HasMutation(text3))
											{
												text4 = MutationFactory.GetMutationEntryByName(text3).Name;
												flag3 = GO.HasPart(text3);
											}
											if (flag3)
											{
												text2 = text2 + ", {{G|" + text4 + "}}";
											}
											else
											{
												text2 = text2 + ", {{R|" + text4 + "}}";
											}
										}
									}
									if (power.Exclusion != null)
									{
										foreach (string text5 in power.Exclusion.CachedCommaExpansion())
										{
											string text6 = text5;
											bool flag4 = false;
											IBaseSkillEntry baseSkillEntry2;
											if (SkillFactory.Factory.TryGetFirstEntry(text5, out baseSkillEntry2))
											{
												text6 = baseSkillEntry2.Name;
												flag4 = !GO.HasSkill(text5);
											}
											else if (MutationFactory.HasMutation(text5))
											{
												text6 = MutationFactory.GetMutationEntryByName(text5).Name;
												flag4 = !GO.HasPart(text5);
											}
											if (flag4)
											{
												text2 = text2 + ", Ex: {{g|" + text6 + "}}";
											}
											else
											{
												text2 = text2 + ", Ex: {{R|" + text6 + "}}";
											}
										}
									}
									scrapBuffer.Write(" - ", true, false, false, null, -1);
									scrapBuffer.Write(power.Render(GO), true, false, false, null, -1);
									scrapBuffer.Write(text2, true, false, false, null, -1);
								}
							}
							num6 = num5;
						}
						if (num5 == num2)
						{
							scrapBuffer.Goto(2, num4);
							scrapBuffer.Write("{{Y|>}}", true, false, false, null, -1);
						}
						num4++;
						num5++;
					}
					if (num2 <= num6 || num >= SkillsAndPowersScreen.Nodes.Count)
					{
						break;
					}
					for (int i = num + 1; i < SkillsAndPowersScreen.Nodes.Count; i++)
					{
						if (SkillsAndPowersScreen.Nodes[i].Visible)
						{
							num = i;
							break;
						}
					}
				}
				int num8 = 0;
				int num9 = 0;
				for (int j = 0; j < SkillsAndPowersScreen.Nodes.Count; j++)
				{
					if (SkillsAndPowersScreen.Nodes[j].Visible)
					{
						num8++;
						num9 = j;
					}
				}
				scrapBuffer.Goto(2, 24);
				if (SkillsAndPowersScreen.Nodes[num2].Power != null && GO.HasPart(SkillsAndPowersScreen.Nodes[num2].Power.Class))
				{
					if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
					{
						scrapBuffer.Write(" [{{K|" + ControlManager.getCommandInputDescription("Accept", false, false, ControlManager.InputDeviceType.Unknown, false) + "}}-Buy] ", true, false, false, null, -1);
					}
					else
					{
						scrapBuffer.Write(" [{{W|8}}-Up {{W|2}}-Down {{W|4}}-Collapse {{W|6}}-Expand {{K|Space}}-Buy] ", true, false, false, null, -1);
					}
				}
				else if (SkillsAndPowersScreen.Nodes[num2].Skill != null && GO.HasPart(SkillsAndPowersScreen.Nodes[num2].Skill.Class))
				{
					if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
					{
						scrapBuffer.Write(" [{{K|" + ControlManager.getCommandInputDescription("Accept", false, false, ControlManager.InputDeviceType.Unknown, false) + "}}-Buy] ", true, false, false, null, -1);
					}
					else
					{
						scrapBuffer.Write(" [{{W|8}}-Up {{W|2}}-Down {{W|4}}-Collapse {{W|6}}-Expand {{K|Space}}-Buy] ", true, false, false, null, -1);
					}
				}
				else if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
				{
					scrapBuffer.Write(" [{{W|" + ControlManager.getCommandInputDescription("Accept", false, false, ControlManager.InputDeviceType.Unknown, false) + "}}-Buy] ", true, false, false, null, -1);
				}
				else
				{
					scrapBuffer.Write(" [{{W|8}}-Up {{W|2}}-Down {{W|4}}-Collapse {{W|6}}-Expand {{W|Space}}-Buy] ", true, false, false, null, -1);
				}
				string description2 = SkillsAndPowersScreen.Nodes[num2].Description;
				string text7;
				if (SkillsAndPowersScreen.Nodes[num2].Skill != null)
				{
					text7 = SkillsAndPowersScreen.Nodes[num2].Skill.Name;
				}
				else
				{
					text7 = SkillsAndPowersScreen.Nodes[num2].Power.Name;
				}
				if (num != 0)
				{
					scrapBuffer.Goto(4, 1);
					scrapBuffer.Write("{{W|<More...>}}", true, false, false, null, -1);
				}
				if (num6 != num9)
				{
					scrapBuffer.Goto(4, 19);
					scrapBuffer.Write("{{W|<More...>}}", true, false, false, null, -1);
				}
				TextBlock textBlock2 = new TextBlock(description2.Replace("\r\n", "\n"), 73, 12, false, 0);
				int num10 = 24 - num3;
				scrapBuffer.SingleBox(0, num10 - 1, 79, num10 - 1, ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
				scrapBuffer.Fill(1, num10, 78, 23, 32, ColorUtility.MakeColor(TextColor.Black, TextColor.Black), false, false);
				scrapBuffer.Goto(4, num10 - 1);
				scrapBuffer.Goto(0, num10 - 1);
				scrapBuffer.Write(195);
				scrapBuffer.Goto(79, num10 - 1);
				scrapBuffer.Write(180);
				scrapBuffer.Goto(4, num10 - 1);
				scrapBuffer.Write("{{W|" + text7 + "}}", true, false, false, null, -1);
				for (int k = 0; k < textBlock2.Lines.Count; k++)
				{
					scrapBuffer.Goto(2, num10 + k);
					scrapBuffer.Write(textBlock2.Lines[k], true, false, false, null, -1);
				}
				Popup._TextConsole.DrawBuffer(scrapBuffer, null, false);
				keys = Keyboard.getvk(true, false, true);
				if (keys == Keys.NumPad7 || (keys == Keys.NumPad9 && Keyboard.RawCode != Keys.Prior && Keyboard.RawCode != Keys.Next))
				{
					flag = true;
				}
				if (keys == Keys.NumPad2)
				{
					for (int l = num2 + 1; l < SkillsAndPowersScreen.Nodes.Count; l++)
					{
						if (SkillsAndPowersScreen.Nodes[l].Visible)
						{
							num2 = l;
							break;
						}
					}
				}
				if (keys == Keys.NumPad8)
				{
					for (int m = num2 - 1; m >= 0; m--)
					{
						if (SkillsAndPowersScreen.Nodes[m].Visible)
						{
							num2 = m;
							break;
						}
					}
				}
				if (keys == Keys.Prior)
				{
					if (num2 == num)
					{
						int n = 0;
						int num11 = 21 - num3;
						while (n < num11)
						{
							if (num2 <= 0)
							{
								break;
							}
							for (int num12 = num2 - 1; num12 >= 0; num12--)
							{
								if (SkillsAndPowersScreen.Nodes[num12].Visible)
								{
									num2 = num12;
									break;
								}
							}
							n++;
						}
					}
					else
					{
						num2 = num;
					}
				}
				if (keys == Keys.Next)
				{
					if (num2 == num6)
					{
						int num13 = 0;
						int num14 = 21 - num3;
						while (num13 < num14)
						{
							if (num2 >= SkillsAndPowersScreen.Nodes.Count)
							{
								break;
							}
							for (int num15 = num2 + 1; num15 < SkillsAndPowersScreen.Nodes.Count; num15++)
							{
								if (SkillsAndPowersScreen.Nodes[num15].Visible)
								{
									num2 = num15;
									break;
								}
							}
							num13++;
						}
					}
					else
					{
						num2 = num6;
					}
				}
				if (keys == Keys.NumPad4)
				{
					if (SkillsAndPowersScreen.Nodes[num2].Skill != null)
					{
						SkillsAndPowersScreen.Nodes[num2].Expand = false;
					}
					else
					{
						SkillsAndPowersScreen.Nodes[num2].ParentNode.Expand = false;
						for (int num16 = num2 - 1; num16 >= 0; num16--)
						{
							if (SkillsAndPowersScreen.Nodes[num16].Visible)
							{
								num2 = num16;
								break;
							}
						}
					}
				}
				if (keys == Keys.OemMinus || keys == Keys.Subtract || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:V Negative"))
				{
					foreach (SPNode spnode2 in SkillsAndPowersScreen.Nodes)
					{
						spnode2.Expand = false;
					}
					for (int num17 = num2 - 1; num17 >= 0; num17--)
					{
						if (SkillsAndPowersScreen.Nodes[num17].Visible)
						{
							num2 = num17;
							break;
						}
					}
				}
				if (keys == Keys.Oemplus || keys == Keys.Add || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:V Positive"))
				{
					foreach (SPNode spnode3 in SkillsAndPowersScreen.Nodes)
					{
						spnode3.Expand = true;
					}
				}
				if (keys == Keys.NumPad6)
				{
					SkillsAndPowersScreen.Nodes[num2].Expand = true;
				}
				if (keys == Keys.OemQuestion)
				{
					if (SkillsAndPowersScreen.Nodes[num2].Skill != null)
					{
						Popup.Show(SkillsAndPowersScreen.Nodes[num2].Skill.GetFormattedDescription(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					else
					{
						Popup.Show(SkillsAndPowersScreen.Nodes[num2].Power.GetFormattedDescription(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
				}
				if (keys == Keys.Space || keys == Keys.Enter)
				{
					SkillsAndPowersScreen.SelectNode(SkillsAndPowersScreen.Nodes[num2], GO);
					SkillsAndPowersScreen.BuildNodes(GO, false);
					SkillEntry skill = SkillsAndPowersScreen.Nodes[num2].Skill;
					if (skill != null && SkillsAndPowersScreen.Nodes[num2].Skill != skill)
					{
						int num18 = 0;
						while (num18 < SkillsAndPowersScreen.Nodes.Count)
						{
							if (SkillsAndPowersScreen.Nodes[num18].Skill == skill)
							{
								num2 = num18;
								if (num2 < num)
								{
									num = num2;
									break;
								}
								break;
							}
							else
							{
								num18++;
							}
						}
					}
				}
				if (keys == Keys.Escape || keys == Keys.NumPad5 || keys == Keys.NumPad9 || keys == Keys.NumPad7)
				{
					flag = true;
				}
			}
			GameManager.Instance.PopGameView(false);
			if (keys == Keys.NumPad7 || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Page Left"))
			{
				return ScreenReturn.Previous;
			}
			if (keys == Keys.NumPad9 || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Page Right"))
			{
				return ScreenReturn.Next;
			}
			return ScreenReturn.Exit;
		}

		// Token: 0x04005195 RID: 20885
		public static List<SPNode> Nodes;
	}
}
