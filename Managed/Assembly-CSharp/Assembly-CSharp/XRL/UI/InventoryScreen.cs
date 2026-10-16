using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using ConsoleLib.Console;
using Qud.API;
using XRL.Core;
using XRL.World;
using XRL.World.Parts;

namespace XRL.UI
{
	// Token: 0x020014A2 RID: 5282
	[UIView("Inventory", false, true, false, "Menu", "Inventory", false, 0, false)]
	public class InventoryScreen : IScreen, IWantsTextConsoleInit
	{
		// Token: 0x0600CF6E RID: 53102 RVA: 0x004BC6C2 File Offset: 0x004BA8C2
		public void Init(TextConsole console, ScreenBuffer buffer)
		{
			InventoryScreen.TextConsole = console;
			InventoryScreen.Buffer = buffer;
		}

		// Token: 0x0600CF6F RID: 53103 RVA: 0x004BC6D0 File Offset: 0x004BA8D0
		public static void ClearLists()
		{
			InventoryScreen.CategoryMap.Clear();
			InventoryScreen.SelectionList.Clear();
			InventoryScreen.CategorySelectionList.Clear();
			InventoryScreen.CategoryList.Clear();
			InventoryScreen.SortList.Clear();
		}

		// Token: 0x0600CF70 RID: 53104 RVA: 0x004BC704 File Offset: 0x004BA904
		public static void ResetNameCache(GameObject GO)
		{
			List<GameObject> objectsDirect = GO.Inventory.GetObjectsDirect();
			for (int i = 0; i < objectsDirect.Count; i++)
			{
				objectsDirect[i].ResetNameCache();
			}
		}

		// Token: 0x0600CF71 RID: 53105 RVA: 0x004BC73C File Offset: 0x004BA93C
		public static void RebuildLists(GameObject GO)
		{
			InventoryScreen.hotkeySpread.restart();
			Inventory inventory = GO.Inventory;
			InventoryScreen.CategoryMap.Clear();
			InventoryScreen.SelectionList.Clear();
			InventoryScreen.itemsSkippedByFilter = 0;
			if (!InventoryScreen.Categories.CleanContains("Category"))
			{
				InventoryScreen.Categories.Add("Category");
			}
			List<GameObject> objectsDirect = inventory.GetObjectsDirect();
			for (int i = 0; i < objectsDirect.Count; i++)
			{
				GameObject gameObject = objectsDirect[i];
				if (!gameObject.HasTag("HiddenInInventory"))
				{
					if (InventoryScreen.filterString != "" && !gameObject.GetDisplayName(2147483647, null, null, false, false, false, false, true, false, true, false, false, false, false, false, false, null, false, false, false, false, null, false, null, false, true).Contains(InventoryScreen.filterString, CompareOptions.IgnoreCase))
					{
						InventoryScreen.itemsSkippedByFilter++;
					}
					else
					{
						gameObject.Seen();
						string inventoryCategory = gameObject.GetInventoryCategory(false);
						if (!InventoryScreen.CategoryList.ContainsKey(inventoryCategory))
						{
							InventoryScreen.CategoryList.Add(inventoryCategory, new InventoryCategory(inventoryCategory, true));
							InventoryScreen.Categories.Add(inventoryCategory);
						}
						if (!InventoryScreen.CategoryMap.ContainsKey(inventoryCategory))
						{
							InventoryScreen.CategoryMap.Add(inventoryCategory, new List<GameObject>());
						}
						InventoryScreen.CategoryMap[inventoryCategory].Add(gameObject);
					}
				}
			}
			List<string> list = new List<string>();
			foreach (string text in InventoryScreen.CategoryList.Keys)
			{
				if (!InventoryScreen.CategoryMap.ContainsKey(text))
				{
					list.Add(text);
				}
				else if (InventoryScreen.CategoryMap[text].Count == 0)
				{
					list.Add(text);
				}
			}
			foreach (string text2 in list)
			{
				if (InventoryScreen.CategoryList.ContainsKey(text2))
				{
					InventoryScreen.CategoryList.Remove(text2);
				}
				if (InventoryScreen.CategoryMap.ContainsKey(text2))
				{
					InventoryScreen.CategoryMap.Remove(text2);
				}
			}
			using (Dictionary<string, List<GameObject>>.ValueCollection.Enumerator enumerator3 = InventoryScreen.CategoryMap.Values.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					enumerator3.Current.Sort((GameObject a, GameObject b) => a.SortVs(b, null, false, true, true, false, null));
				}
				goto IL_0287;
			}
			IL_027B:
			InventoryScreen.CategorySort--;
			IL_0287:
			if (InventoryScreen.CategorySort >= InventoryScreen.Categories.Count)
			{
				goto IL_027B;
			}
			if (InventoryScreen.CategorySort == -1)
			{
				InventoryScreen.SortList = inventory.GetObjects();
				InventoryScreen.SortList.Sort((GameObject a, GameObject b) => a.SortVs(b, null, false, true, true, false, null));
			}
			else if (InventoryScreen.Categories[InventoryScreen.CategorySort] == "Category")
			{
				InventoryScreen.SortList = inventory.GetObjects();
				InventoryScreen.SortList.Sort((GameObject a, GameObject b) => a.SortVs(b, null, true, true, true, false, null));
			}
			else
			{
				if (InventoryScreen.CategoryMap.ContainsKey(InventoryScreen.Categories[InventoryScreen.CategorySort]))
				{
					InventoryScreen.SortList = InventoryScreen.CategoryMap[InventoryScreen.Categories[InventoryScreen.CategorySort]];
				}
				InventoryScreen.SortList.Sort((GameObject a, GameObject b) => a.SortVs(b, null, false, true, true, false, null));
			}
			int num = 0;
			InventoryScreen.bMore = false;
			if (InventoryScreen.CategorySort != -1 && InventoryScreen.Categories[InventoryScreen.CategorySort] == "Category")
			{
				InventoryScreen.CategorySelectionList.Clear();
				int num2 = 0;
				List<string> list2 = new List<string>();
				foreach (string text3 in InventoryScreen.CategoryList.Keys)
				{
					list2.Add(text3);
				}
				list2.Sort();
				for (int j = 0; j < list2.Count; j++)
				{
					string text4 = list2[j];
					InventoryCategory inventoryCategory2 = InventoryScreen.CategoryList[text4];
					if (InventoryScreen.forceCategorySelect != null && inventoryCategory2 == InventoryScreen.forceCategorySelect)
					{
						if (num2 < InventoryScreen.StartObject)
						{
							InventoryScreen.StartObject = num2;
						}
						InventoryScreen.Selected = num2 - InventoryScreen.StartObject;
						InventoryScreen.forceCategorySelect = null;
					}
					if (num2 >= InventoryScreen.StartObject && num2 <= 21 + InventoryScreen.StartObject)
					{
						InventoryScreen.CategorySelectionList.Add(InventoryScreen.hotkeySpread.ch(), new CategorySelectionListEntry(inventoryCategory2));
						InventoryScreen.hotkeySpread.next();
						num++;
					}
					if (inventoryCategory2.Expanded && InventoryScreen.CategoryMap.ContainsKey(inventoryCategory2.Name))
					{
						foreach (GameObject gameObject2 in InventoryScreen.CategoryMap[inventoryCategory2.Name])
						{
							num2++;
							num++;
							if (num2 >= InventoryScreen.StartObject && num2 <= 21 + InventoryScreen.StartObject)
							{
								InventoryScreen.CategorySelectionList.Add(InventoryScreen.hotkeySpread.ch(), new CategorySelectionListEntry(gameObject2));
								InventoryScreen.hotkeySpread.next();
							}
							else if (num2 > 21 + InventoryScreen.StartObject)
							{
								InventoryScreen.bMore = true;
								break;
							}
						}
					}
					if (InventoryScreen.CategoryList.ContainsKey(text4))
					{
						InventoryScreen.CategoryList[text4].Weight = 0;
						InventoryScreen.CategoryList[text4].Items = 0;
					}
					if (InventoryScreen.CategoryMap.ContainsKey(inventoryCategory2.Name))
					{
						foreach (GameObject gameObject3 in InventoryScreen.CategoryMap[inventoryCategory2.Name])
						{
							if (gameObject3.Physics != null)
							{
								InventoryScreen.CategoryList[text4].Weight += gameObject3.Physics.Weight;
							}
							InventoryScreen.CategoryList[text4].Items++;
						}
					}
					if (num2 > 21 + InventoryScreen.StartObject)
					{
						InventoryScreen.bMore = true;
						return;
					}
					num2++;
				}
				return;
			}
			if (inventory != null)
			{
				int num3 = 0;
				foreach (GameObject gameObject4 in InventoryScreen.SortList)
				{
					if (num3 >= InventoryScreen.StartObject && num3 <= 21 + InventoryScreen.StartObject)
					{
						InventoryScreen.SelectionList.Add(InventoryScreen.hotkeySpread.ch(), gameObject4);
						InventoryScreen.hotkeySpread.next();
					}
					num3++;
					if (num3 > 21 + InventoryScreen.StartObject)
					{
						InventoryScreen.bMore = true;
						break;
					}
				}
			}
		}

		// Token: 0x0600CF72 RID: 53106 RVA: 0x004BCE84 File Offset: 0x004BB084
		public ScreenReturn Show(GameObject GO)
		{
			GameManager.Instance.PushGameView("Inventory", true);
			InventoryScreen.filterString = "";
			InventoryScreen.ResetNameCache(GO);
			InventoryScreen.filterString = "";
			Keys keys = Keys.None;
			bool flag = false;
			InventoryScreen.StartObject = 0;
			InventoryScreen.Selected = 0;
			InventoryScreen.currentMaxWeight = GO.GetMaxCarriedWeight();
			InventoryScreen.hotkeySpread = HotkeySpread.get(new string[] { "Menus", "UINav" });
			string text = "< {{W|7}} Character | Equipment {{W|9}} >";
			if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
			{
				text = string.Concat(new string[]
				{
					"< {{W|",
					ControlManager.getCommandInputDescription("Page Left", false, false, ControlManager.InputDeviceType.Unknown, false),
					"}} Character | Equipment {{W|",
					ControlManager.getCommandInputDescription("Page Right", false, false, ControlManager.InputDeviceType.Unknown, false),
					"}} >"
				});
			}
			Dictionary<char, int> dictionary = new Dictionary<char, int>();
			while (!flag)
			{
				GameObject gameObject;
				IEvent @event;
				bool flag2;
				for (;;)
				{
					Event.ResetPool(false);
					InventoryScreen.RebuildLists(GO);
					int num;
					for (;;)
					{
						InventoryScreen.Buffer.Clear();
						InventoryScreen.Buffer.SingleBox(0, 0, 79, 24, ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
						InventoryScreen.Buffer.Goto(35, 0);
						InventoryScreen.Buffer.Write("[ {{W|Inventory}} ]", true, false, false, null, -1);
						if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
						{
							InventoryScreen.Buffer.Goto(60, 0);
							InventoryScreen.Buffer.Write(" {{W|" + ControlManager.getCommandInputDescription("Back", false, false, ControlManager.InputDeviceType.Unknown, false) + "}} to exit ", true, false, false, null, -1);
						}
						else
						{
							InventoryScreen.Buffer.Goto(60, 0);
							InventoryScreen.Buffer.Write(" {{W|ESC}} or {{W|5}} to exit ", true, false, false, null, -1);
						}
						InventoryScreen.Buffer.Goto(79 - ColorUtility.StripFormatting(text).Length, 24);
						InventoryScreen.Buffer.Write(text, true, false, false, null, -1);
						if (InventoryScreen.StartObject > 0)
						{
							InventoryScreen.Buffer.Goto(1, 0);
							if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
							{
								InventoryScreen.Buffer.Write("<more...>", true, false, false, null, -1);
							}
							else
							{
								InventoryScreen.Buffer.Write("<{{W|8}} to scroll up>", true, false, false, null, -1);
							}
						}
						StringBuilder stringBuilder = Event.NewStringBuilder(null);
						stringBuilder.Append("Total weight: {{Y|").Append(GO.GetCarriedWeight()).Append(" {{y|/}}  ")
							.Append(InventoryScreen.currentMaxWeight)
							.Append(" lbs.}}");
						InventoryScreen.Buffer.Goto(79 - ColorUtility.LengthExceptFormatting(stringBuilder), 23);
						InventoryScreen.Buffer.Write(stringBuilder.ToString(), true, false, false, null, -1);
						dictionary.Clear();
						num = 0;
						InventoryCategory inventoryCategory = null;
						gameObject = null;
						int num2 = 1;
						int num3 = 1;
						foreach (char c in InventoryScreen.CategorySelectionList.Keys)
						{
							if (InventoryScreen.CategorySelectionList[c].Category != null)
							{
								InventoryScreen.Buffer.Goto(num3, num2 + num);
								string text2;
								if (num == InventoryScreen.Selected)
								{
									text2 = "{{Y|>}}";
									inventoryCategory = InventoryScreen.CategorySelectionList[c].Category;
								}
								else
								{
									text2 = " ";
								}
								StringBuilder stringBuilder2 = Event.NewStringBuilder(null);
								StringBuilder stringBuilder3 = Event.NewStringBuilder(null);
								char c2 = ((num == InventoryScreen.Selected) ? 'Y' : 'K');
								stringBuilder3.Append("{{").Append(c2).Append('|');
								if (Options.ShowNumberOfItems)
								{
									stringBuilder3.Append(", ").Append(InventoryScreen.CategorySelectionList[c].Category.Items).Append((InventoryScreen.CategorySelectionList[c].Category.Items == 1) ? " item" : " items");
								}
								stringBuilder3.Append("}}");
								stringBuilder2.Append(" {{").Append((num == InventoryScreen.Selected) ? 'Y' : 'y').Append("|[")
									.Append(InventoryScreen.CategorySelectionList[c].Category.Weight)
									.Append("#]}}");
								if (InventoryScreen.CategorySelectionList[c].Category.Expanded)
								{
									if (num == InventoryScreen.Selected)
									{
										StringBuilder stringBuilder4 = Event.NewStringBuilder(null);
										stringBuilder4.Append(text2).Append(c).Append(") [-] {{K|[{{Y|")
											.Append(InventoryScreen.CategorySelectionList[c].Category.Name)
											.Append(stringBuilder3)
											.Append("}}]}}");
										InventoryScreen.Buffer.Write(stringBuilder4.ToString(), true, false, false, null, -1);
									}
									else
									{
										StringBuilder stringBuilder5 = Event.NewStringBuilder(null);
										stringBuilder5.Append(text2).Append(c).Append(") [-] {{K|[")
											.Append(InventoryScreen.CategorySelectionList[c].Category.Name)
											.Append(stringBuilder3)
											.Append("]}}");
										InventoryScreen.Buffer.Write(stringBuilder5.ToString(), true, false, false, null, -1);
									}
								}
								else if (num == InventoryScreen.Selected)
								{
									StringBuilder stringBuilder6 = Event.NewStringBuilder(null);
									stringBuilder6.Append(text2).Append(c).Append(") [+] {{K|[{{Y|")
										.Append(InventoryScreen.CategorySelectionList[c].Category.Name)
										.Append(stringBuilder3)
										.Append("}}]}}");
									InventoryScreen.Buffer.Write(stringBuilder6.ToString(), true, false, false, null, -1);
								}
								else
								{
									StringBuilder stringBuilder7 = Event.NewStringBuilder(null);
									stringBuilder7.Append(text2).Append(c).Append(") [+] {{K|[")
										.Append(InventoryScreen.CategorySelectionList[c].Category.Name)
										.Append(stringBuilder3)
										.Append("]}}");
									InventoryScreen.Buffer.Write(stringBuilder7.ToString(), true, false, false, null, -1);
								}
								InventoryScreen.Buffer.Goto(79 - ColorUtility.LengthExceptFormatting(stringBuilder2), num2 + num);
								InventoryScreen.Buffer.Write(stringBuilder2, -1);
								dictionary.Add(c, num);
								num++;
							}
							else
							{
								GameObject @object = InventoryScreen.CategorySelectionList[c].Object;
								string text3;
								if (num == InventoryScreen.Selected)
								{
									text3 = "{{Y|>}}  ";
									gameObject = @object;
								}
								else
								{
									text3 = "   ";
								}
								InventoryScreen.Buffer.Goto(num3, num2 + num);
								InventoryScreen.Buffer.Write(text3 + c.ToString() + ")", true, false, false, null, -1);
								InventoryScreen.Buffer.Goto(num3 + 6, num2 + num);
								if (@object.Render != null)
								{
									if (!GO.IsConfused)
									{
										InventoryScreen.Buffer.Write(@object.RenderForUI(null, false));
									}
									else
									{
										InventoryScreen.Buffer.Write(" ", true, false, false, null, -1);
									}
									InventoryScreen.Buffer.Goto(num3 + 8, num2 + num);
								}
								InventoryScreen.Buffer.Write(@object.DisplayName, true, false, false, null, -1);
								int weight = @object.Weight;
								StringBuilder stringBuilder8 = Event.NewStringBuilder(null);
								stringBuilder8.Append(" {{").Append((num == InventoryScreen.Selected) ? 'Y' : 'K').Append("|")
									.Append(weight)
									.Append("#}}")
									.Append('³');
								InventoryScreen.Buffer.Goto(80 - ColorUtility.LengthExceptFormatting(stringBuilder8), num2 + num);
								InventoryScreen.Buffer.Write(stringBuilder8, -1);
								dictionary.Add(c, num);
								num++;
							}
						}
						if (num == 0 && InventoryScreen.StartObject != 0)
						{
							break;
						}
						if (InventoryScreen.Selected >= num)
						{
							goto Block_8;
						}
						if (InventoryScreen.bMore)
						{
							InventoryScreen.Buffer.Goto(num3, 24);
							if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
							{
								InventoryScreen.Buffer.Write("<...more>", true, false, false, null, -1);
							}
							else
							{
								InventoryScreen.Buffer.Write("<{{W|2}} to scroll down>", true, false, false, null, -1);
							}
						}
						if (InventoryScreen.itemsSkippedByFilter > 0)
						{
							InventoryScreen.Buffer.Goto(3, 23);
							InventoryScreen.Buffer.Write(InventoryScreen.itemsSkippedByFilter.ToString() + " items hidden by filter", true, false, false, null, -1);
						}
						if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Keyboard)
						{
							InventoryScreen.Buffer.Goto(20, 24);
							InventoryScreen.Buffer.Write("[{{W|?}} view quick keys]", true, false, false, null, -1);
						}
						InventoryScreen.TextConsole.DrawBuffer(InventoryScreen.Buffer, null, false);
						if (!XRLCore.Core.Game.Running)
						{
							goto Block_13;
						}
						@event = null;
						keys = Keyboard.getvk(Options.MapDirectionsToKeypad, true, true);
						ScreenBuffer.ClearImposterSuppression();
						flag2 = false;
						char c3 = ("" + ((char)Keyboard.Char).ToString() + " ").ToLower()[0];
						if (keys == Keys.Enter)
						{
							keys = Keys.Space;
						}
						if (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "RightClick")
						{
							flag = true;
						}
						if (keys == Keys.OemQuestion || c3 == '?')
						{
							goto IL_0902;
						}
						if (keys == (Keys)131137)
						{
							goto Block_18;
						}
						if (keys == (Keys)131140)
						{
							goto Block_20;
						}
						if (keys == (Keys)131142 || c3 == ',')
						{
							goto IL_09B0;
						}
						if (keys == (Keys)131154)
						{
							goto Block_25;
						}
						if (keys == (Keys)131152)
						{
							goto Block_27;
						}
						if (keys == Keys.NumPad7 || (keys == Keys.NumPad9 && Keyboard.RawCode != Keys.Prior && Keyboard.RawCode != Keys.Next))
						{
							goto IL_0A70;
						}
						if (keys == Keys.Escape || keys == Keys.NumPad5 || (keys == Keys.NumPad9 && Keyboard.RawCode != Keys.Prior && Keyboard.RawCode != Keys.Next))
						{
							goto IL_0A98;
						}
						if (keys == Keys.NumPad8)
						{
							if (InventoryScreen.Selected <= 0)
							{
								goto IL_0ABD;
							}
							InventoryScreen.Selected--;
						}
						else
						{
							if (keys != Keys.NumPad2)
							{
								goto IL_0B15;
							}
							if (InventoryScreen.Selected >= num - 1)
							{
								goto IL_0AFA;
							}
							InventoryScreen.Selected++;
						}
					}
					InventoryScreen.StartObject = 0;
					continue;
					Block_8:
					InventoryScreen.Selected = num - 1;
				}
				IL_0E8A:
				if (!flag && (flag2 || (@event != null && @event.InterfaceExitRequested())))
				{
					flag = true;
					continue;
				}
				continue;
				IL_0902:
				Popup.Show("Inventory quick keys\r\n\r\n&WCtrl+D&y - Drop\r\n&WCtrl+A&y - Eat\r\n&WCtrl+E&y - Auto-equip\r\n&WCtrl+R&y - Drink\r\n&WCtrl+P&y - Apply\n&WCtrl+F or ,&y - Filter", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				goto IL_0E8A;
				Block_18:
				if (gameObject != null)
				{
					InventoryActionEvent.Check(out @event, gameObject, GO, gameObject, "Eat", false, false, false, false, false, 0, 0, 0, null, null, null, null);
					InventoryScreen.ResetNameCache(GO);
					goto IL_0E8A;
				}
				goto IL_0E8A;
				Block_20:
				if (gameObject == null)
				{
					goto IL_0E8A;
				}
				Zone currentZone = GO.CurrentZone;
				if (currentZone == null || !currentZone.IsWorldMap())
				{
					InventoryActionEvent.Check(ref flag2, GO, GO, gameObject, "CommandDropObject", false, false, false, false, false, 0, 0, 0, null, null, null, null);
					InventoryScreen.ResetNameCache(GO);
					goto IL_0E8A;
				}
				goto IL_0E8A;
				IL_09B0:
				InventoryScreen.filterString = Popup.AskString("Enter text to filter inventory by item name.", InventoryScreen.filterString, "Sounds/UI/ui_notification", null, null, 80, 0, false, true, null);
				InventoryScreen.ClearLists();
				goto IL_0E8A;
				Block_25:
				if (gameObject != null)
				{
					InventoryActionEvent.Check(out @event, gameObject, GO, gameObject, "Drink", false, false, false, false, false, 0, 0, 0, null, null, null, null);
					InventoryScreen.ResetNameCache(GO);
					goto IL_0E8A;
				}
				goto IL_0E8A;
				Block_27:
				if (gameObject != null)
				{
					InventoryActionEvent.Check(out @event, gameObject, GO, gameObject, "Apply", false, false, false, false, false, 0, 0, 0, null, null, null, null);
					InventoryScreen.ResetNameCache(GO);
					goto IL_0E8A;
				}
				goto IL_0E8A;
				IL_0A70:
				flag = true;
				goto IL_0E8A;
				IL_0A98:
				flag = true;
				goto IL_0E8A;
				IL_0ABD:
				if (InventoryScreen.StartObject > 0)
				{
					InventoryScreen.StartObject--;
					goto IL_0E8A;
				}
				goto IL_0E8A;
				IL_0AFA:
				if (InventoryScreen.bMore)
				{
					InventoryScreen.StartObject++;
					goto IL_0E8A;
				}
				goto IL_0E8A;
				IL_0B15:
				if (keys == Keys.Next || keys == Keys.Next || Keyboard.RawCode == Keys.Next || Keyboard.RawCode == Keys.Next)
				{
					int num;
					if (InventoryScreen.Selected < num - 1)
					{
						InventoryScreen.Selected = num - 1;
						goto IL_0E8A;
					}
					if (InventoryScreen.bMore)
					{
						InventoryScreen.StartObject += 21;
						goto IL_0E8A;
					}
					goto IL_0E8A;
				}
				else if (keys == Keys.Prior || keys == Keys.Back || Keyboard.RawCode == Keys.Prior || Keyboard.RawCode == Keys.Back)
				{
					if (InventoryScreen.Selected > 0)
					{
						InventoryScreen.Selected = 0;
						goto IL_0E8A;
					}
					InventoryScreen.StartObject -= 21;
					if (InventoryScreen.StartObject < 0)
					{
						InventoryScreen.StartObject = 0;
						goto IL_0E8A;
					}
					goto IL_0E8A;
				}
				else
				{
					if (keys == Keys.Subtract || keys == Keys.OemMinus || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:V Negative"))
					{
						using (Dictionary<string, InventoryCategory>.ValueCollection.Enumerator enumerator2 = InventoryScreen.CategoryList.Values.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								InventoryCategory inventoryCategory2 = enumerator2.Current;
								inventoryCategory2.Expanded = false;
							}
							goto IL_0E8A;
						}
					}
					if (keys == Keys.Add || keys == Keys.Oemplus || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:V Positive"))
					{
						using (Dictionary<string, InventoryCategory>.ValueCollection.Enumerator enumerator2 = InventoryScreen.CategoryList.Values.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								InventoryCategory inventoryCategory3 = enumerator2.Current;
								inventoryCategory3.Expanded = true;
							}
							goto IL_0E8A;
						}
					}
					if (gameObject != null)
					{
						if ((Keyboard.vkCode == Keys.Right || keys == Keys.NumPad6 || keys == (Keys)131141) && Options.GetOption("OptionPressingRightInInventoryEquips", "Yes") == "Yes" && GO.AutoEquip(gameObject, false, false, false))
						{
							InventoryScreen.ResetNameCache(GO);
						}
						if (Keyboard.vkCode == Keys.Left || keys == Keys.NumPad4 || keys == (Keys)131141)
						{
							foreach (KeyValuePair<string, List<GameObject>> keyValuePair in InventoryScreen.CategoryMap)
							{
								if (keyValuePair.Value.Contains(gameObject))
								{
									InventoryScreen.CategoryList[keyValuePair.Key].Expanded = false;
									InventoryScreen.forceCategorySelect = InventoryScreen.CategoryList[keyValuePair.Key];
									break;
								}
							}
						}
						if (keys == Keys.Space)
						{
							EquipmentAPI.TwiddleObject(GO, gameObject, ref flag, false, false, false);
							InventoryScreen.ResetNameCache(GO);
						}
						if (keys == Keys.Tab)
						{
							InventoryActionEvent.Check(gameObject, GO, gameObject, "Look", false, false, false, false, false, 0, 0, 0, null, null, null, null);
							InventoryScreen.ResetNameCache(GO);
						}
					}
					InventoryCategory inventoryCategory;
					if (inventoryCategory != null)
					{
						if (keys == Keys.NumPad4)
						{
							inventoryCategory.Expanded = false;
						}
						if (keys == Keys.NumPad6)
						{
							inventoryCategory.Expanded = true;
						}
						if (keys == Keys.Space)
						{
							inventoryCategory.Expanded = !inventoryCategory.Expanded;
						}
					}
					char c3;
					if (keys < Keys.A || keys > Keys.Z || !InventoryScreen.CategorySelectionList.ContainsKey(c3))
					{
						goto IL_0E8A;
					}
					if (InventoryScreen.Selected == dictionary[c3] && (!InventoryScreen.CategorySelectionList.ContainsKey(c3) || InventoryScreen.CategorySelectionList[c3].Category == null))
					{
						EquipmentAPI.TwiddleObject(GO, gameObject, ref flag, false, false, false);
						InventoryScreen.ResetNameCache(GO);
						goto IL_0E8A;
					}
					InventoryScreen.Selected = dictionary[c3];
					if (InventoryScreen.CategorySelectionList.ContainsKey(c3) && InventoryScreen.CategorySelectionList[c3].Category != null)
					{
						InventoryScreen.CategorySelectionList[c3].Category.Expanded = !InventoryScreen.CategorySelectionList[c3].Category.Expanded;
						goto IL_0E8A;
					}
					goto IL_0E8A;
				}
				Block_13:
				GameManager.Instance.PopGameView(false);
				return ScreenReturn.Exit;
			}
			InventoryScreen.ClearLists();
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

		// Token: 0x04005097 RID: 20631
		private static Dictionary<char, GameObject> SelectionList = new Dictionary<char, GameObject>();

		// Token: 0x04005098 RID: 20632
		private static Dictionary<char, CategorySelectionListEntry> CategorySelectionList = new Dictionary<char, CategorySelectionListEntry>();

		// Token: 0x04005099 RID: 20633
		private static Dictionary<string, List<GameObject>> CategoryMap = new Dictionary<string, List<GameObject>>();

		// Token: 0x0400509A RID: 20634
		private static Dictionary<string, InventoryCategory> CategoryList = new Dictionary<string, InventoryCategory>();

		// Token: 0x0400509B RID: 20635
		private static List<GameObject> SortList;

		// Token: 0x0400509C RID: 20636
		private static List<string> Categories = new List<string>();

		// Token: 0x0400509D RID: 20637
		private static int StartObject = 0;

		// Token: 0x0400509E RID: 20638
		private static int CategorySort = 0;

		// Token: 0x0400509F RID: 20639
		private static bool bMore = false;

		// Token: 0x040050A0 RID: 20640
		private static InventoryCategory forceCategorySelect = null;

		// Token: 0x040050A1 RID: 20641
		private static TextConsole TextConsole;

		// Token: 0x040050A2 RID: 20642
		private static ScreenBuffer Buffer;

		// Token: 0x040050A3 RID: 20643
		public static int itemsSkippedByFilter = 0;

		// Token: 0x040050A4 RID: 20644
		public static int Selected = 0;

		// Token: 0x040050A5 RID: 20645
		public static int currentMaxWeight;

		// Token: 0x040050A6 RID: 20646
		public static string filterString = "";

		// Token: 0x040050A7 RID: 20647
		private static HotkeySpread hotkeySpread;
	}
}
