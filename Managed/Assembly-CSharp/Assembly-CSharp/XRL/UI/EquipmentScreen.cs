using System;
using System.Collections.Generic;
using System.Text;
using ConsoleLib.Console;
using Qud.API;
using XRL.Core;
using XRL.Language;
using XRL.World;
using XRL.World.Anatomy;
using XRL.World.Parts;

namespace XRL.UI
{
	// Token: 0x0200149E RID: 5278
	public class EquipmentScreen : IScreen
	{
		// Token: 0x0600CF62 RID: 53090 RVA: 0x004BB65C File Offset: 0x004B985C
		public static void ShowBodypartEquipUI(GameObject GO, BodyPart SelectedBodyPart)
		{
			Inventory inventory = GO.Inventory;
			if (inventory != null)
			{
				GameObject gameObject = null;
				EquipmentScreen.EquipmentList.Clear();
				inventory.GetEquipmentListForSlot(EquipmentScreen.EquipmentList, SelectedBodyPart.Type, false, true, false);
				if (EquipmentScreen.EquipmentList.Count > 0)
				{
					string text = null;
					if (SelectedBodyPart.Type == "Hand")
					{
						text = "Melee Weapons,Shields,Light Sources,Tools";
					}
					if (SelectedBodyPart.Type == "Thrown Weapon")
					{
						text = "Thrown Weapons,Grenades,Melee Weapons,Light Sources";
					}
					gameObject = PickItem.ShowPicker(EquipmentScreen.EquipmentList, text, PickItem.PickItemDialogStyle.SelectItemDialog, GO, null, null, null, true, null, false, true, false);
				}
				else
				{
					Popup.Show("You don't have anything to use in that slot.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				if (gameObject != null)
				{
					GO.FireEvent(Event.New("CommandEquipObject", "Object", gameObject, "BodyPart", SelectedBodyPart));
					return;
				}
			}
			else
			{
				Popup.Show("You have no inventory!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
		}

		// Token: 0x0600CF63 RID: 53091 RVA: 0x004BB73C File Offset: 0x004B993C
		public ScreenReturn Show(GameObject GO)
		{
			GameManager.Instance.PushGameView("Equipment", true);
			ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(false);
			Keys keys = Keys.None;
			int num = 0;
			int num2 = 0;
			EquipmentScreen.ScreenTab screenTab = EquipmentScreen.ScreenTab.Equipment;
			bool flag = false;
			Body body = GO.Body;
			for (;;)
			{
				IL_002B:
				bool flag2 = false;
				List<BodyPart> list = new List<BodyPart>();
				List<GameObject> list2 = new List<GameObject>();
				List<GameObject> list3 = new List<GameObject>();
				List<GameObject> list4 = new List<GameObject>();
				Dictionary<char, int> dictionary = new Dictionary<char, int>();
				foreach (BodyPart bodyPart in body.LoopParts())
				{
					if (screenTab == EquipmentScreen.ScreenTab.Equipment)
					{
						if (bodyPart.Equipped != null)
						{
							list.Add(bodyPart);
							list3.Add(bodyPart.Equipped);
							list4.Add(bodyPart.Equipped);
							list2.Add(null);
						}
						else if (bodyPart.DefaultBehavior != null)
						{
							list.Add(bodyPart);
							list3.Add(bodyPart.DefaultBehavior);
							list4.Add(null);
							list2.Add(null);
						}
						else
						{
							list.Add(bodyPart);
							list3.Add(null);
							list4.Add(null);
							list2.Add(null);
						}
					}
					if (bodyPart.Cybernetics != null)
					{
						if (screenTab == EquipmentScreen.ScreenTab.Cybernetics)
						{
							list.Add(bodyPart);
							list3.Add(null);
							list4.Add(bodyPart.Cybernetics);
							list2.Add(bodyPart.Cybernetics);
						}
						flag2 = true;
					}
				}
				if (num >= list.Count)
				{
					num = list.Count - 1;
				}
				string text = "< {{W|7}} Inventory | Factions {{W|9}} >";
				if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
				{
					text = string.Concat(new string[]
					{
						"< {{W|",
						ControlManager.getCommandInputDescription("Page Left", false, false, ControlManager.InputDeviceType.Unknown, false),
						"}} Inventory | Factions {{W|",
						ControlManager.getCommandInputDescription("Page Right", false, false, ControlManager.InputDeviceType.Unknown, false),
						"}} >"
					});
				}
				HotkeySpread hotkeySpread = HotkeySpread.get(new string[] { "Menus", "UINav" });
				bool flag3 = GO.AreHostilesNearby();
				while (!flag)
				{
					Event.ResetPool(false);
					List<GameObject> list5 = Event.NewGameObjectList();
					if (!XRLCore.Core.Game.Running)
					{
						goto Block_4;
					}
					scrapBuffer.Clear();
					scrapBuffer.SingleBox(0, 0, 79, 24, ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
					if (screenTab == EquipmentScreen.ScreenTab.Equipment)
					{
						scrapBuffer.Goto(35, 0);
						scrapBuffer.Write("[ {{W|Equipment}} ]", true, false, false, null, -1);
					}
					else
					{
						scrapBuffer.Goto(35, 0);
						scrapBuffer.Write("[ {{W|Cybernetics}} ]", true, false, false, null, -1);
					}
					scrapBuffer.Goto(60, 0);
					scrapBuffer.Write(" {{W|" + ControlManager.getCommandInputDescription("Cancel", false, false, ControlManager.InputDeviceType.Unknown, false) + "}} to exit ", true, false, false, null, -1);
					scrapBuffer.Goto(79 - ColorUtility.StripFormatting(text).Length, 24);
					scrapBuffer.Write(text, true, false, false, null, -1);
					scrapBuffer.Goto(25, 24);
					if (flag3)
					{
						goto IL_02FF;
					}
					BodyPart bodyPart2 = list[num];
					if (bodyPart2 != null && bodyPart2.Abstract)
					{
						goto IL_02FF;
					}
					BodyPart bodyPart3 = list[num];
					if (bodyPart3 != null && bodyPart3.Extrinsic)
					{
						goto IL_02FF;
					}
					scrapBuffer.Goto(20, 24);
					scrapBuffer.Write("[{{W|" + ControlManager.getCommandInputDescription("CmdInsert", false, false, ControlManager.InputDeviceType.Unknown, false) + " - Set primary limb}}]", true, false, false, null, -1);
					IL_0369:
					int num3 = 22;
					int num4 = 2;
					if (flag2)
					{
						scrapBuffer.Goto(3, num4);
						if (screenTab == EquipmentScreen.ScreenTab.Cybernetics)
						{
							scrapBuffer.Write("{{K|Equipment}} {{Y|Cybernetics}}", true, false, false, null, -1);
						}
						else
						{
							scrapBuffer.Write("{{Y|Equipment}} {{K|Cybernetics}}", true, false, false, null, -1);
						}
						num3 -= 2;
						num4 += 2;
					}
					if (list == null)
					{
						continue;
					}
					dictionary.Clear();
					int num5 = num2;
					while (num5 < list.Count && num5 - num2 < num3)
					{
						if (num == num5)
						{
							scrapBuffer.Goto(27, num4 + num5 - num2);
							scrapBuffer.Write("{{K|>}}", true, false, false, null, -1);
						}
						scrapBuffer.Goto(1, num4 + num5 - num2);
						string text2;
						if (num == num5)
						{
							text2 = "{{Y|>}}{{W|";
						}
						else
						{
							text2 = " {{w|";
						}
						EquipmentScreen.SB.Clear();
						if (list2[num5] == null)
						{
							if (Options.IndentBodyParts)
							{
								for (int i = body.GetPartDepth(list[num5]); i >= 1; i--)
								{
									EquipmentScreen.SB.Append(' ');
								}
							}
							EquipmentScreen.SB.Append(list[num5].GetCardinalDescription());
						}
						else
						{
							EquipmentScreen.SB.Append(Grammar.MakeTitleCase(list[num5].GetCardinalName()));
						}
						if (list[num5].Primary)
						{
							EquipmentScreen.SB.Append(" {{G|*}}");
						}
						char c = hotkeySpread.charAt(num5);
						if (c == '\0')
						{
							c = ' ';
						}
						else
						{
							dictionary.Add(c, num5);
						}
						if (list2[num5] != null)
						{
							if (num == num5)
							{
								scrapBuffer.Write(text2 + c.ToString() + "}}) " + EquipmentScreen.SB.ToString(), true, false, false, null, -1);
								scrapBuffer.Goto(28, num4 + num5 - num2);
								scrapBuffer.Write(list2[num5].RenderForUI(null, false));
								scrapBuffer.Goto(30, num4 + num5 - num2);
								scrapBuffer.Write(list2[num5].DisplayName, true, false, false, null, -1);
							}
							else
							{
								scrapBuffer.Write(string.Concat(new string[]
								{
									text2,
									"}}{{K|",
									c.ToString(),
									") ",
									EquipmentScreen.SB.ToString(),
									"}}"
								}), true, false, false, null, -1);
								scrapBuffer.Goto(28, num4 + num5 - num2);
								scrapBuffer.Write(list2[num5].RenderForUI(null, false), null, null, "&K", "&K", new char?('K'), true, false, false);
								scrapBuffer.Goto(30, num4 + num5 - num2);
								scrapBuffer.Write("{{K|" + list2[num5].DisplayNameStripped + "}}", true, false, false, null, -1);
							}
						}
						else
						{
							scrapBuffer.Write(text2 + c.ToString() + "}}) " + EquipmentScreen.SB.ToString(), true, false, false, null, -1);
							if (list[num5].Equipped == null)
							{
								if (list[num5].DefaultBehavior == null)
								{
									scrapBuffer.Goto(28, num4 + num5 - num2);
									if (num == num5)
									{
										scrapBuffer.Write("{{Y|-}}", true, false, false, null, -1);
									}
									else
									{
										scrapBuffer.Write("{{K|-}}", true, false, false, null, -1);
									}
								}
								else
								{
									scrapBuffer.Goto(28, num4 + num5 - num2);
									scrapBuffer.Write(list[num5].DefaultBehavior.RenderForUI(null, false), null, null, "&K", "&K", new char?('K'), true, false, false);
									scrapBuffer.Write(" ", true, false, false, null, -1);
									scrapBuffer.Goto(30, num4 + num5 - num2);
									scrapBuffer.Write("{{K|" + list[num5].DefaultBehavior.DisplayName + "}}", true, false, false, null, -1);
								}
							}
							else if (list[num5].Equipped != null && list5.CleanContains(list[num5].Equipped))
							{
								scrapBuffer.Goto(28, num4 + num5 - num2);
								scrapBuffer.Write(list[num5].Equipped.RenderForUI(null, false), null, null, "&K", "&K", new char?('K'), true, false, false);
								scrapBuffer.Write(" ", true, false, false, null, -1);
								scrapBuffer.Goto(30, num4 + num5 - num2);
								scrapBuffer.Write("{{K|" + list[num5].Equipped.DisplayNameStripped + "}}", true, false, false, null, -1);
							}
							else if (list[num5].Equipped != null && list[num5].Equipped.HasTag("RenderImplantGreyInEquipment") && !list5.CleanContains(list[num5].Equipped))
							{
								if (list[num5].Equipped.IsImplant)
								{
									if (list[num5].Equipped.GetPart<CyberneticsBaseItem>().ImplantedOn != null)
									{
										scrapBuffer.Goto(28, num4 + num5 - num2);
										scrapBuffer.Write(list[num5].Equipped.RenderForUI(null, false), null, null, "&K", "&K", new char?('K'), true, false, false);
										scrapBuffer.Write(" ", true, false, false, null, -1);
										scrapBuffer.Goto(30, num4 + num5 - num2);
										scrapBuffer.Write("{{K|" + list[num5].Equipped.DisplayNameStripped + "}}", true, false, false, null, -1);
									}
									else
									{
										list5.Add(list[num5].Equipped);
										scrapBuffer.Goto(28, num4 + num5 - num2);
										scrapBuffer.Write(list[num5].Equipped.RenderForUI(null, false));
										scrapBuffer.Write(" ", true, false, false, null, -1);
										scrapBuffer.Goto(30, num4 + num5 - num2);
										scrapBuffer.Write(list[num5].Equipped.DisplayName, true, false, false, null, -1);
									}
								}
							}
							else
							{
								list5.Add(list[num5].Equipped);
								scrapBuffer.Goto(28, num4 + num5 - num2);
								scrapBuffer.Write(list[num5].Equipped.RenderForUI(null, false));
								scrapBuffer.Goto(30, num4 + num5 - num2);
								scrapBuffer.Write(list[num5].Equipped.DisplayName, true, false, false, null, -1);
							}
						}
						num5++;
					}
					if (num2 + num3 < list.Count)
					{
						scrapBuffer.Goto(2, 24);
						scrapBuffer.Write("<more...>", true, false, false, null, -1);
					}
					if (num2 > 0)
					{
						scrapBuffer.Goto(2, 0);
						scrapBuffer.Write("<more...>", true, false, false, null, -1);
					}
					Popup._TextConsole.DrawBuffer(scrapBuffer, null, false);
					keys = Keyboard.getvk(Options.MapDirectionsToKeypad, false, true);
					ScreenBuffer.ClearImposterSuppression();
					char c2 = (char.ToLower((char)Keyboard.Char).ToString() + " ").ToLower()[0];
					if (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "RightClick")
					{
						flag = true;
					}
					if (keys == Keys.Escape || keys == Keys.NumPad5)
					{
						flag = true;
					}
					else if (keys == Keys.NumPad7 || (keys == Keys.NumPad9 && Keyboard.RawCode != Keys.Prior && Keyboard.RawCode != Keys.Next))
					{
						flag = true;
					}
					else if (keys == Keys.Prior)
					{
						if (num == num2)
						{
							num2 = Math.Max(num2 - num3, 0);
						}
						num = num2;
					}
					else if (keys == Keys.Next)
					{
						if (num == num2 + num3 - 1 && list.Count > num2 + num3)
						{
							num2 = Math.Min(num2 + num3, list.Count - num3);
						}
						num = Math.Min(num2 + num3 - 1, list.Count - 1);
					}
					else if (keys == Keys.NumPad4 || keys == Keys.NumPad6)
					{
						if (flag2)
						{
							if (screenTab == EquipmentScreen.ScreenTab.Cybernetics)
							{
								screenTab = EquipmentScreen.ScreenTab.Equipment;
							}
							else
							{
								screenTab = EquipmentScreen.ScreenTab.Cybernetics;
							}
							num = 0;
							num2 = 0;
							goto IL_002B;
						}
						if (keys == Keys.NumPad4 && Options.GetOption("OptionPressingRightInInventoryEquips", "Yes") == "Yes" && list3[num] != null)
						{
							BodyPart bodyPart4 = list[num];
							GO.FireEvent(Event.New("CommandUnequipObject", "BodyPart", bodyPart4));
							goto IL_002B;
						}
					}
					else if (keys != Keys.F2)
					{
						if (keys == Keys.NumPad8)
						{
							if (num - num2 <= 0 && num2 > 0)
							{
								num2--;
							}
							if (num > 0)
							{
								num--;
							}
						}
						else if (keys == Keys.NumPad2 && num < list.Count - 1)
						{
							if (num - num2 == num3 - 1)
							{
								num2++;
							}
							num++;
						}
					}
					if (keys == Keys.Enter)
					{
						keys = Keys.Space;
					}
					if (keys == Keys.Tab || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdInsert") || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:Toggle"))
					{
						list[num].SetAsPreferredDefault(false);
					}
					if (keys == (Keys.Space | Keys.Control))
					{
						if (list3[num] != null)
						{
							InventoryActionEvent.Check(list3[num], GO, list3[num], "Look", false, false, false, false, false, 0, 0, 0, null, null, null, null);
							goto IL_002B;
						}
						continue;
					}
					else
					{
						if ((keys != Keys.Space && (keys < Keys.A || keys > Keys.Z)) || (!dictionary.ContainsKey(c2) && keys != Keys.Space))
						{
							continue;
						}
						int num6;
						if (keys == Keys.Space)
						{
							num6 = num;
						}
						else
						{
							num6 = dictionary[c2];
						}
						if (list4[num6] != null)
						{
							EquipmentAPI.TwiddleObject(GO, list4[num6], ref flag, false, false, false);
							goto IL_002B;
						}
						EquipmentScreen.ShowBodypartEquipUI(GO, list[num6]);
						goto IL_002B;
					}
					IL_02FF:
					scrapBuffer.Goto(20, 24);
					scrapBuffer.Write("[{{K|" + ControlManager.getCommandInputDescription("CmdInsert", false, false, ControlManager.InputDeviceType.Unknown, false) + " - Set primary limb}}]", true, false, false, null, -1);
					goto IL_0369;
				}
				goto Block_70;
			}
			Block_4:
			GameManager.Instance.PopGameView(false);
			return ScreenReturn.Exit;
			Block_70:
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

		// Token: 0x0400508D RID: 20621
		public static List<GameObject> EquipmentList = new List<GameObject>(16);

		// Token: 0x0400508E RID: 20622
		private static StringBuilder SB = new StringBuilder();

		// Token: 0x02001DA9 RID: 7593
		private enum ScreenTab
		{
			// Token: 0x04007395 RID: 29589
			Equipment,
			// Token: 0x04007396 RID: 29590
			Cybernetics
		}
	}
}
