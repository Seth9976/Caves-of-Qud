using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using XRL.Language;
using XRL.World;
using XRL.World.Anatomy;
using XRL.World.Capabilities;
using XRL.World.Parts;
using XRL.World.Parts.Skill;
using XRL.World.Tinkering;

namespace XRL.UI
{
	// Token: 0x020014D6 RID: 5334
	public class TinkeringScreen : IScreen
	{
		// Token: 0x0600D0EA RID: 53482 RVA: 0x004D7C0D File Offset: 0x004D5E0D
		public ScreenReturn Show(GameObject GO)
		{
			return this.Show(GO, null, null);
		}

		// Token: 0x0600D0EB RID: 53483 RVA: 0x004D7C18 File Offset: 0x004D5E18
		public ScreenReturn Show(GameObject GO, GameObject ForModdingOf = null, IEvent FromEvent = null)
		{
			if (Options.ModernCharacterSheet && Options.ModernUI)
			{
				return ScreenReturn.Exit;
			}
			GameManager.Instance.PushGameView("Tinkering", true);
			ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(false);
			Keys keys = Keys.None;
			bool flag = false;
			string text = "Build";
			int num = 0;
			int num2 = 0;
			List<GameObject> list = null;
			if (ForModdingOf != null)
			{
				list = new List<GameObject> { ForModdingOf };
				text = "Mod";
			}
			BitLocker bitLocker = ((GO.GetPart<Tinkering>() == null) ? GO.GetPart<BitLocker>() : GO.RequirePart<BitLocker>(false));
			List<TinkerData> list2 = new List<TinkerData>(64);
			List<TinkerData> ModRecipes = new List<TinkerData>(64);
			List<GameObject> ModdableItems = null;
			Dictionary<GameObject, List<TinkerData>> ItemMods = null;
			Dictionary<GameObject, bool> ItemExpanded = null;
			int TotalModLines = 0;
			int num3 = 15;
			int num4 = 0;
			foreach (TinkerData tinkerData in TinkerData.KnownRecipes)
			{
				if (tinkerData.Type == "Build")
				{
					list2.Add(tinkerData);
				}
				else if (tinkerData.Type == "Mod")
				{
					ModRecipes.Add(tinkerData);
				}
			}
			list2.Sort((TinkerData a, TinkerData b) => ColorUtility.CompareExceptFormattingAndCase(a.DisplayName, b.DisplayName));
			ModRecipes.Sort((TinkerData a, TinkerData b) => ColorUtility.CompareExceptFormattingAndCase(a.DisplayName, b.DisplayName));
			string text2 = "< {{W|7}} Journal | Skills {{W|9}} >";
			if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
			{
				text2 = string.Concat(new string[]
				{
					"< {{W|",
					ControlManager.getCommandInputDescription("Page Left", false, false, ControlManager.InputDeviceType.Unknown, false),
					"}} Journal | Skills {{W|",
					ControlManager.getCommandInputDescription("Page Right", false, false, ControlManager.InputDeviceType.Unknown, false),
					"}} >"
				});
			}
			BitCost bitCost = new BitCost();
			BitCost bitCost2 = new BitCost();
			Action<GameObject> <>9__2;
			while (!flag)
			{
				Event.ResetPool(true);
				scrapBuffer.Clear();
				bool flag2 = GO.AreHostilesNearby();
				scrapBuffer.SingleBox(0, 0, 79, 24, ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
				scrapBuffer.Goto(35, 0);
				scrapBuffer.Write("[ {{W|Tinkering}} ]", true, false, false, null, -1);
				if (flag2)
				{
					scrapBuffer.WriteAt(10, 0, " {{R|hostiles nearby}} ", true);
				}
				if (bitLocker != null)
				{
					scrapBuffer.SingleBox(51, 0, 79, 16, ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
				}
				if (list == null)
				{
					if (text == "Build")
					{
						scrapBuffer.Goto(2, 1);
						scrapBuffer.Write("{{Y|>}} {{W|Build}}    {{w|Mod}}", true, false, false, null, -1);
					}
					else
					{
						scrapBuffer.Goto(2, 1);
						scrapBuffer.Write("  {{w|Build}}  {{Y|>}} {{W|Mod}}", true, false, false, null, -1);
					}
				}
				TinkerData tinkerData2 = null;
				GameObject gameObject = null;
				bitCost.Clear();
				if (text == "Mod")
				{
					bool flag3 = false;
					if (!GO.HasSkill("Tinkering"))
					{
						scrapBuffer.Goto(4, 4);
						scrapBuffer.Write("You don't have the Tinkering skill.", true, false, false, null, -1);
					}
					else if (ModRecipes.Count == 0)
					{
						scrapBuffer.Goto(4, 4);
						scrapBuffer.Write("You don't have any modification schematics.", true, false, false, null, -1);
					}
					else
					{
						if (ModdableItems == null)
						{
							TotalModLines = 0;
							ModdableItems = new List<GameObject>(128);
							ItemExpanded = new Dictionary<GameObject, bool>(128);
							ItemMods = new Dictionary<GameObject, List<TinkerData>>(64);
							List<TinkerData> list3 = new List<TinkerData>(ModRecipes);
							Action<GameObject> action;
							if ((action = <>9__2) == null)
							{
								action = (<>9__2 = delegate(GameObject obj)
								{
									string text10 = ItemModding.ModKey(obj);
									if (text10 != null && obj.Understood())
									{
										for (int k = 0; k < ModRecipes.Count; k++)
										{
											if (ModRecipes[k].CanMod(text10) && ItemModding.ModificationApplicable(ModRecipes[k].PartName, obj, The.Player, null))
											{
												int num17;
												if (!ItemMods.ContainsKey(obj))
												{
													ModdableItems.Add(obj);
													ItemMods.Add(obj, new List<TinkerData>(8));
													ItemExpanded.Add(obj, false);
													num17 = TotalModLines;
													TotalModLines = num17 + 1;
												}
												ItemMods[obj].Add(ModRecipes[k]);
												num17 = TotalModLines;
												TotalModLines = num17 + 1;
											}
										}
									}
								});
							}
							Action<GameObject> action2 = action;
							if (list == null)
							{
								GO.Inventory.ForeachObject(action2);
								GO.Body.ForeachEquippedObject(action2);
								if (list3.Count > 0)
								{
									GameObject gameObject2 = GameObject.CreateSample("DummyTinkerScreenObject");
									ModdableItems.Add(gameObject2);
									ItemMods.Add(gameObject2, new List<TinkerData>());
									ItemExpanded.Add(gameObject2, false);
									int i = TotalModLines;
									TotalModLines = i + 1;
									for (int j = 0; j < list3.Count; j++)
									{
										ItemMods[gameObject2].Add(list3[j]);
										i = TotalModLines;
										TotalModLines = i + 1;
									}
								}
							}
							else
							{
								foreach (GameObject gameObject3 in list)
								{
									action2(gameObject3);
								}
							}
						}
						if (ModdableItems.Count == 0)
						{
							scrapBuffer.Goto(4, 4);
							if (list != null)
							{
								flag = true;
								break;
							}
							scrapBuffer.Write("You don't have any moddable items.", true, false, false, null, -1);
						}
						else
						{
							int num5 = 3;
							int num6 = 0;
							int num7 = 0;
							while (num7 < ModdableItems.Count && num5 < num3)
							{
								if (num6 >= num2)
								{
									scrapBuffer.Goto(4, num5);
									scrapBuffer.Write(StringFormat.ClipLine(ModdableItems[num7].DisplayName, 46, true, null), true, false, false, null, -1);
									if (num6 == num)
									{
										scrapBuffer.Goto(2, num5);
										scrapBuffer.Write("{{Y|>}}", true, false, false, null, -1);
										flag3 = true;
									}
									num5++;
								}
								num6++;
								int num8 = 0;
								while (num8 < ItemMods[ModdableItems[num7]].Count && num5 < num3)
								{
									if (num6 >= num2)
									{
										int num9 = Tier.Constrain(ItemMods[ModdableItems[num7]][num8].Tier);
										int num10 = Tier.Constrain(ModdableItems[num7].GetModificationSlotsUsed() - ModdableItems[num7].GetIntProperty("NoCostMods", 0) + ModdableItems[num7].GetTechTier());
										bitCost2.Clear();
										bitCost2.Increment(BitType.TierBits[num9]);
										bitCost2.Increment(BitType.TierBits[num10]);
										ModifyBitCostEvent.Process(GO, bitCost2, "Mod");
										if (num6 == num)
										{
											scrapBuffer.Goto(2, num5);
											scrapBuffer.Write("{{Y|>}}", true, false, false, null, -1);
											bitCost2.CopyTo(bitCost);
											tinkerData2 = ItemMods[ModdableItems[num7]][num8];
											gameObject = ModdableItems[num7];
											flag3 = true;
										}
										scrapBuffer.Goto(4, num5);
										scrapBuffer.Write("  ", true, false, false, null, -1);
										scrapBuffer.Write(ItemMods[ModdableItems[num7]][num8].DisplayName, true, false, false, null, -1);
										scrapBuffer.Write(" <", true, false, false, null, -1);
										scrapBuffer.Write(bitCost2.ToString(), true, false, false, null, -1);
										scrapBuffer.Write(">", true, false, false, null, -1);
										num5++;
									}
									num6++;
									num8++;
								}
								num7++;
							}
						}
					}
					if (!flag3 && num > 0)
					{
						num--;
						continue;
					}
					scrapBuffer.SingleBox(0, 16, 80, 24, ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
					if (tinkerData2 != null)
					{
						string text3 = StringFormat.ClipText("{{rules|" + ItemModding.GetModificationDescription(tinkerData2.Blueprint, gameObject) + "}}", 76, true, true, false);
						scrapBuffer.Goto(2, 18);
						scrapBuffer.WriteBlockWithNewlines(text3, 999, 0, false);
					}
					scrapBuffer.WriteAt(3, 24, string.Concat(new string[]
					{
						" ",
						Markup.Color("keybind", ControlManager.getCommandInputDescription("Accept", false, false, ControlManager.InputDeviceType.Unknown, false)),
						" Mod Item  ",
						Markup.Color("keybind", ControlManager.getCommandInputDescription("CmdInsert", false, false, ControlManager.InputDeviceType.Unknown, false)),
						" List Mods  ",
						Markup.Color("keybind", ControlManager.getCommandInputDescription("Cancel", false, false, ControlManager.InputDeviceType.Unknown, false)),
						" Exit "
					}), true);
				}
				else if (text == "Build")
				{
					if (list2.Count > 0)
					{
						tinkerData2 = list2[num];
					}
					if (tinkerData2 != null)
					{
						bitCost.Import(TinkerItem.GetBitCostFor(tinkerData2.Blueprint));
						ModifyBitCostEvent.Process(GO, bitCost, "Build");
					}
					if (!GO.HasSkill("Tinkering"))
					{
						scrapBuffer.Goto(4, 4);
						scrapBuffer.Write("You don't have the Tinkering skill.", true, false, false, null, -1);
					}
					else if (list2.Count == 0)
					{
						scrapBuffer.Goto(4, 4);
						scrapBuffer.Write("You don't have any item schematics.", true, false, false, null, -1);
					}
					else
					{
						int num11 = num2;
						while (num11 < list2.Count && num11 - num2 < 12)
						{
							scrapBuffer.Goto(4, 3 + (num11 - num2));
							if (list2[num11].DisplayName.IsNullOrEmpty())
							{
								scrapBuffer.Write(list2[num11].Blueprint, true, false, false, null, -1);
							}
							else
							{
								scrapBuffer.Write(list2[num11].DisplayName, true, false, false, null, -1);
							}
							string text4;
							if (num11 == num)
							{
								text4 = bitCost.ToString();
							}
							else
							{
								bitCost2.Clear();
								bitCost2.Import(TinkerItem.GetBitCostFor(list2[num11].Blueprint));
								ModifyBitCostEvent.Process(GO, bitCost2, "Build");
								text4 = bitCost2.ToString();
							}
							scrapBuffer.Goto(50 - ColorUtility.LengthExceptFormatting(text4), 3 + (num11 - num2));
							if (num11 == num)
							{
								scrapBuffer.Write("{{^K|" + text4 + "}}", true, false, false, null, -1);
							}
							else
							{
								scrapBuffer.Write(text4, true, false, false, null, -1);
							}
							if (num11 == num)
							{
								scrapBuffer.Goto(2, 3 + (num11 - num2));
								scrapBuffer.Write("{{Y|>}}", true, false, false, null, -1);
							}
							num11++;
						}
						scrapBuffer.SingleBox(0, 16, 80, 24, ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
						if (tinkerData2 != null)
						{
							scrapBuffer.Goto(2, 16);
							scrapBuffer.Write(list2[num].LongDisplayName, true, false, false, null, -1);
							scrapBuffer.Goto(2, 17);
							scrapBuffer.WriteBlockWithNewlines(list2[num].Description, 8, num4, true);
						}
					}
					scrapBuffer.WriteAt(3, 24, string.Concat(new string[]
					{
						" ",
						Markup.Color("keybind", ControlManager.getCommandInputDescription("Accept", false, false, ControlManager.InputDeviceType.Unknown, false)),
						" Build  ",
						Markup.Color("keybind", ControlManager.getCommandInputDescription("V Positive", false, false, ControlManager.InputDeviceType.Unknown, false)),
						"/",
						Markup.Color("keybind", ControlManager.getCommandInputDescription("V Negative", false, false, ControlManager.InputDeviceType.Unknown, false)),
						" Scroll  ",
						Markup.Color("keybind", ControlManager.getCommandInputDescription("Cancel", false, false, ControlManager.InputDeviceType.Unknown, false)),
						" Exit "
					}), true);
				}
				if (bitLocker != null)
				{
					scrapBuffer.WriteAt(53, 0, " Bit Locker ", true);
					int num5 = 1;
					foreach (char c in BitType.BitOrder)
					{
						BitType bitType = BitType.BitMap[c];
						string text5 = string.Concat(new string[]
						{
							"{{",
							bitType.Color.ToString(),
							"|",
							(Options.AlphanumericBits ? BitType.CharTranslateBit(bitType.Color) : '\a').ToString(),
							" ",
							bitType.Description,
							"}}"
						});
						int bitCount = bitLocker.GetBitCount(c);
						string text6;
						if (bitCount == 0)
						{
							text6 = "{{K|0}}";
						}
						else if (bitCount >= 1000000)
						{
							text6 = "{{C|" + (bitCount / 1000000).ToString() + "M}}";
						}
						else if (bitCount >= 10000)
						{
							text6 = "{{C|" + (bitCount / 1000).ToString() + "K}}";
						}
						else
						{
							text6 = "{{C|" + bitCount.ToString() + "}}";
						}
						scrapBuffer.Goto(52, num5);
						scrapBuffer.Write(text5, true, false, false, null, -1);
						scrapBuffer.Goto(79 - ColorUtility.LengthExceptFormatting(text6), num5);
						scrapBuffer.Write(text6, true, false, false, null, -1);
						if (tinkerData2 != null)
						{
							scrapBuffer.Goto(52, num5);
							int num12;
							if (bitCost.TryGetValue(c, out num12) && num12 > 0)
							{
								if (bitCount >= num12)
								{
									scrapBuffer.Write("{{G|û}}", true, false, false, null, -1);
								}
								else
								{
									scrapBuffer.Write("{{R|X}}", true, false, false, null, -1);
								}
							}
							else
							{
								scrapBuffer.Write("{{K|-}}", true, false, false, null, -1);
							}
						}
						num5++;
					}
					if (tinkerData2 != null && !tinkerData2.Ingredient.IsNullOrEmpty())
					{
						bool flag4 = false;
						string[] array = tinkerData2.Ingredient.Split(',', StringSplitOptions.None);
						int num13 = 14;
						if (array.Length > 1)
						{
							num13--;
						}
						num5 = num13;
						foreach (string text7 in array)
						{
							if (GO.Inventory.FindObjectByBlueprint(text7) != null)
							{
								scrapBuffer.Goto(52, num5);
								num5++;
								scrapBuffer.Write("{{G|û}} ", true, false, false, null, -1);
								scrapBuffer.Write(TinkeringHelpers.TinkeredItemShortDisplayName(text7), true, false, false, null, -1);
								flag4 = true;
								break;
							}
						}
						if (!flag4)
						{
							foreach (string text8 in array)
							{
								if (num5 != num13)
								{
									scrapBuffer.Goto(52, num5);
									scrapBuffer.Write("-or-", true, false, false, null, -1);
									num5++;
								}
								scrapBuffer.Goto(52, num5);
								num5++;
								scrapBuffer.Write("{{R|X}} ", true, false, false, null, -1);
								scrapBuffer.Write(TinkeringHelpers.TinkeredItemShortDisplayName(text8), true, false, false, null, -1);
							}
						}
					}
				}
				if (list == null)
				{
					scrapBuffer.Goto(79 - ColorUtility.StripFormatting(text2).Length, 24);
					scrapBuffer.Write(text2, true, false, false, null, -1);
				}
				scrapBuffer.WriteAt(51, 0, "Â", true);
				scrapBuffer.WriteAt(0, 16, "Ã", true);
				scrapBuffer.WriteAt(79, 16, "\u00b4", true);
				scrapBuffer.WriteAt(51, 16, "Á", true);
				Popup._TextConsole.DrawBuffer(scrapBuffer, null, false);
				keys = Keyboard.getvk(Options.MapDirectionsToKeypad, false, true);
				if (list == null && (keys == Keys.NumPad7 || (keys == Keys.NumPad9 && Keyboard.RawCode != Keys.Prior && Keyboard.RawCode != Keys.Next)))
				{
					flag = true;
				}
				if (keys == Keys.Escape || keys == Keys.NumPad5)
				{
					flag = true;
				}
				if (list == null && (keys == Keys.NumPad4 || keys == Keys.NumPad6))
				{
					num4 = 0;
					num2 = 0;
					num = 0;
					if (text == "Build")
					{
						text = "Mod";
					}
					else
					{
						text = "Build";
					}
				}
				if ((keys == Keys.L || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdInsert")) && text == "Mod" && ModRecipes.Count > 0)
				{
					string text9 = "";
					foreach (TinkerData tinkerData3 in ModRecipes)
					{
						if (text9 != "")
						{
							text9 += "\n";
						}
						text9 += tinkerData3.DisplayName;
					}
					Popup.Show(text9, null, "Sounds/UI/ui_notification", true, true, true, false, null);
				}
				if ((keys == Keys.Oemplus || keys == Keys.Add || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:V Positive")) && tinkerData2 != null && num4 + 6 < tinkerData2.DescriptionLineCount)
				{
					num4++;
				}
				if ((keys == Keys.OemMinus || keys == Keys.Subtract || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:V Positive")) && num4 > 0)
				{
					num4--;
				}
				if (keys == Keys.NumPad8)
				{
					num4 = 0;
					if (num == num2)
					{
						if (num2 > 0)
						{
							num2--;
							num--;
						}
					}
					else if (num > 0)
					{
						num--;
					}
				}
				if (keys == Keys.NumPad2)
				{
					num4 = 0;
					int num14 = list2.Count - 1;
					if (text == "Mod")
					{
						num14 = TotalModLines - 1;
					}
					if (num < num14)
					{
						num++;
					}
					if (num - num2 >= num3 - 3)
					{
						num2++;
					}
				}
				if (keys == Keys.Prior)
				{
					num4 = 0;
					if (num == num2)
					{
						num2 = (num = Math.Max(num2 - num3 + 3, 0));
					}
					else
					{
						num = num2;
					}
				}
				if (keys == Keys.Next)
				{
					num4 = 0;
					int num15 = ((text == "Mod") ? (TotalModLines - 1) : (list2.Count - 1));
					int num16 = num2 + num3 - 4;
					if (num == num16)
					{
						num = Math.Min(num + num3 - 3, num15);
						num2 = Math.Max(num - num3 + 4, 0);
					}
					else
					{
						num = Math.Min(num16, num15);
					}
				}
				if (text == "Mod" && (keys == Keys.Space || keys == Keys.Enter))
				{
					bool flag5 = false;
					if (!TinkeringScreen.PerformUITinkerMod(GO, gameObject, tinkerData2, bitCost, FromEvent, ref flag5, list) || FromEvent.InterfaceExitRequested())
					{
						flag = true;
					}
					if (flag5)
					{
						ModdableItems = null;
					}
				}
				if (text == "Build" && (keys == Keys.Space || keys == Keys.Enter) && (!TinkeringScreen.PerformUITinkerBuild(GO, tinkerData2, FromEvent) || FromEvent.InterfaceExitRequested()))
				{
					flag = true;
				}
			}
			GameManager.Instance.PopGameView(false);
			if (list == null)
			{
				if (keys == Keys.NumPad7 || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Page Left"))
				{
					return ScreenReturn.Previous;
				}
				if (keys == Keys.NumPad9 || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Page Right"))
				{
					return ScreenReturn.Next;
				}
			}
			return ScreenReturn.Exit;
		}

		// Token: 0x0600D0EC RID: 53484 RVA: 0x004D8E70 File Offset: 0x004D7070
		public static bool PerformUITinkerMod(GameObject GO, GameObject ModObject, TinkerData Data, BitCost ActiveCost, IEvent FromEvent, ref bool didMod, List<GameObject> Scope = null)
		{
			if (ModObject == null)
			{
				return true;
			}
			if (Data == null)
			{
				return true;
			}
			BitLocker bitLocker = ((GO.GetPart<Tinkering>() == null) ? GO.GetPart<BitLocker>() : GO.RequirePart<BitLocker>(false));
			if (bitLocker == null)
			{
				return true;
			}
			bool flag = GO.AreHostilesNearby();
			if (Data != null && ModObject != null && ModObject.Blueprint != "DummyTinkerScreenObject")
			{
				GameObject player = The.Player;
				Inventory inventory = player.Inventory;
				BodyPart bodyPart = null;
				GameObject gameObject = null;
				GameObject gameObject2 = null;
				if (!Data.Ingredient.IsNullOrEmpty())
				{
					List<string> list = Data.Ingredient.CachedCommaExpansion();
					foreach (string text in list)
					{
						gameObject = inventory.FindObjectByBlueprint(text, new Predicate<GameObject>(XRL.World.Parts.Temporary.IsNotTemporary));
						if (gameObject != null)
						{
							break;
						}
						if (gameObject2 == null)
						{
							gameObject2 = inventory.FindObjectByBlueprint(text);
						}
					}
					if (gameObject == null)
					{
						if (gameObject2 != null)
						{
							Popup.ShowFail((gameObject2.HasProperName ? "" : "Your ") + gameObject2.ShortDisplayName + gameObject2.Is + " too unstable to craft with.", true, true, true);
						}
						else
						{
							string text2 = "";
							foreach (string text3 in list)
							{
								if (text2 != "")
								{
									text2 += " or ";
								}
								text2 += TinkeringHelpers.TinkeredItemShortDisplayName(text3);
							}
							Popup.ShowFail("You don't have the required ingredient: " + text2 + "!", true, true, true);
						}
						return true;
					}
				}
				bool flag2 = bitLocker.HasBits(ActiveCost);
				string sifrahItemModding = Options.SifrahItemModding;
				if (!flag2 && sifrahItemModding == "Never")
				{
					Popup.ShowFail("You don't have the required <" + ActiveCost.ToString() + "> bits! You have:\n\n " + bitLocker.GetBitsString(), true, true, true);
					return true;
				}
				if (flag && GO.FireEvent("CombatPreventsTinkering"))
				{
					GO.Fail("You can't tinker with hostiles nearby!");
					return true;
				}
				if (!GO.CheckFrozen(false, false, false, null))
				{
					return true;
				}
				int num = 0;
				bool flag3 = false;
				if (sifrahItemModding == "Always")
				{
					flag3 = true;
				}
				else if (sifrahItemModding != "Never")
				{
					DialogResult dialogResult = Popup.ShowYesNoCancel("Do you want to play a game of Sifrah to mod " + ModObject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + "? You can potentially improve the mod's performance and add capabilities to the item, and the cost of playing Sifrah will replace the normal modding cost." + (flag2 ? "" : (" You do not have the required <" + ActiveCost.ToString() + " bits to perform the mod normally.")), "Sounds/UI/ui_notification", true, DialogResult.Cancel);
					if (dialogResult == DialogResult.Yes)
					{
						flag3 = true;
					}
					else if (dialogResult == DialogResult.Cancel || (!flag2 && dialogResult == DialogResult.No))
					{
						return true;
					}
				}
				if (flag3)
				{
					ItemModdingSifrah itemModdingSifrah = new ItemModdingSifrah(ModObject, ActiveCost.GetHighestTier(), ModObject.GetModificationSlotsUsed(), The.Player.Stat("Intelligence", 0));
					itemModdingSifrah.Play(ModObject);
					if (itemModdingSifrah.InterfaceExitRequested)
					{
						return false;
					}
					if (!itemModdingSifrah.ApplyMod)
					{
						return true;
					}
					num = itemModdingSifrah.Performance;
				}
				try
				{
					if (ModObject.Equipped == player)
					{
						bodyPart = player.FindEquippedObject(ModObject);
						if (bodyPart == null)
						{
							MetricsManager.LogError(string.Concat(new string[] { "could not find equipping part for ", ModObject.Blueprint, " ", ModObject.DebugName, " tracked as equipped on player" }));
							return true;
						}
						Event @event = Event.New("CommandUnequipObject", 0, 0, 0);
						@event.SetParameter("BodyPart", bodyPart);
						@event.SetParameter("EnergyCost", 0);
						@event.SetParameter("Context", "Tinkering");
						@event.SetFlag("NoStack", true);
						if (!player.FireEvent(@event))
						{
							Popup.ShowFail("You can't unequip " + ModObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", true, true, true);
							return true;
						}
					}
					if (!Data.Ingredient.IsNullOrEmpty())
					{
						gameObject.SplitStack(1, GO, false);
						if (!inventory.FireEvent(Event.New("CommandRemoveObject", "Object", gameObject)))
						{
							Popup.ShowFail("You cannot use the ingredient!", true, true, true);
							return true;
						}
					}
					if (!flag3)
					{
						bitLocker.UseBits(ActiveCost);
					}
					GameObject gameObject3 = ModObject.SplitStack(1, GO, false);
					if (gameObject3 != null && Scope != null)
					{
						Scope.Add(gameObject3);
					}
					int num2 = ModObject.GetTier();
					int num3 = 0;
					if (num != 0)
					{
						if (num > 0)
						{
							for (int i = 0; i < num; i++)
							{
								if ((num * 5 + ((num2 >= 8) ? 25 : 5)).in100() && RelicGenerator.ApplyBasicBestowal(ModObject, null, 1, null, false, true))
								{
									num3++;
								}
								else
								{
									num2++;
								}
							}
						}
						else
						{
							num2 += num;
						}
						Tier.Constrain(ref num2);
					}
					string text4 = ModObject.t(int.MaxValue, null, null, false, false, false, false, true, true, true, false, null, false, true, false, null, true, null, false);
					IModification modification;
					didMod = ItemModding.ApplyModification(ModObject, Data.PartName, out modification, num2, true, The.Player, false);
					if (didMod)
					{
						ModObject.MakeUnderstood(false);
						SoundManager.PlayUISound("Sounds/Abilities/sfx_ability_tinkerModItem", 1f, false, false, SoundRequest.SoundEffectType.None);
						Popup.Show(string.Concat(new string[]
						{
							"You mod ",
							text4,
							" to be {{C|",
							modification.GetModificationDisplayName() ?? Data.DisplayName,
							"}}."
						}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					if (num3 > 0)
					{
						Popup.Show(ModObject.Does("seem", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " to have taken on new qualities.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						InventoryActionEvent.Check(ModObject, player, ModObject, "Look", false, false, false, false, false, 0, 0, 0, null, null, null, null);
					}
					if (ModObject.Equipped == null && ModObject.InInventory == null)
					{
						player.ReceiveObject(ModObject, false, "Tinkering");
					}
				}
				catch (Exception ex)
				{
					MetricsManager.LogError("Exception applying mod", ex);
				}
				finally
				{
					if (GameObject.Validate(ref ModObject) && bodyPart != null && bodyPart.Equipped == null)
					{
						Event event2 = Event.New("CommandEquipObject", 0, 0, 0);
						event2.SetParameter("Object", ModObject);
						event2.SetParameter("BodyPart", bodyPart);
						event2.SetParameter("EnergyCost", 0);
						event2.SetParameter("Context", "Tinkering");
						player.FireEvent(event2);
					}
				}
			}
			GO.UseEnergy(1000, "Skill Tinkering Mod", null, null, false);
			if (flag)
			{
				if (FromEvent != null)
				{
					FromEvent.RequestInterfaceExit();
				}
				return false;
			}
			return true;
		}

		// Token: 0x0600D0ED RID: 53485 RVA: 0x004D958C File Offset: 0x004D778C
		public static bool PerformUITinkerBuild(GameObject GO, TinkerData Data, IEvent FromEvent)
		{
			bool flag = GO.AreHostilesNearby();
			if (Data != null)
			{
				BitLocker bitLocker = ((GO.GetPart<Tinkering>() == null) ? GO.GetPart<BitLocker>() : GO.RequirePart<BitLocker>(false));
				if (bitLocker == null)
				{
					return true;
				}
				BitCost bitCost = new BitCost();
				bitCost.Import(TinkerItem.GetBitCostFor(Data.Blueprint));
				ModifyBitCostEvent.Process(GO, bitCost, "Build");
				Inventory inventory = GO.Inventory;
				GameObject gameObject = null;
				bool flag2 = true;
				if (!Data.Ingredient.IsNullOrEmpty())
				{
					string[] array = Data.Ingredient.Split(',', StringSplitOptions.None);
					foreach (string text in array)
					{
						gameObject = inventory.FindObjectByBlueprint(text);
						if (gameObject != null)
						{
							break;
						}
					}
					if (gameObject == null)
					{
						string text2 = "";
						foreach (string text3 in array)
						{
							if (text2 != "")
							{
								text2 += " or ";
							}
							text2 += TinkeringHelpers.TinkeredItemShortDisplayName(text3);
						}
						Popup.ShowFail("You don't have the required ingredient: " + text2 + "!", true, true, true);
						flag2 = false;
					}
				}
				if (flag2)
				{
					if (!GO.HasSkill(DataDisk.GetRequiredSkill(Data.Tier)))
					{
						GO.Fail("You don't have the required skill: " + DataDisk.GetRequiredSkillHumanReadable(Data.Tier) + "!");
					}
					else if (!bitLocker.HasBits(bitCost))
					{
						GO.Fail("You don't have the required <" + bitCost.ToString() + "> bits! You have:\n\n" + bitLocker.GetBitsString());
					}
					else if (flag && GO.FireEvent("CombatPreventsTinkering"))
					{
						GO.Fail("You can't tinker with hostiles nearby!");
					}
					else if (GO.CanMoveExtremities("Tinker", true, false, true))
					{
						GameObject gameObject2 = GameObject.CreateSample(Data.Blueprint);
						try
						{
							gameObject2.MakeUnderstood(false);
							bool flag3 = false;
							int @for = GetTinkeringBonusEvent.GetFor(GO, gameObject2, "BonusMod", 0, 0, ref flag3, true, false);
							if (flag3)
							{
								return false;
							}
							if (!Data.Ingredient.IsNullOrEmpty())
							{
								gameObject.SplitStack(1, GO, false);
								if (!inventory.FireEvent(Event.New("CommandRemoveObject", "Object", gameObject)))
								{
									GO.Fail("You cannot use the ingredient!");
									gameObject.CheckStack();
									return false;
								}
							}
							bitLocker.UseBits(bitCost);
							TinkerItem part = gameObject2.GetPart<TinkerItem>();
							GameObject gameObject3 = null;
							for (int j = 0; j < Math.Max(part.NumberMade, 1); j++)
							{
								gameObject3 = GameObject.Create(Data.Blueprint, 0, @for.in100() ? 1 : 0, null, null, null, "Tinkering", null);
								TinkeringHelpers.ProcessTinkeredItem(gameObject3, The.Player);
								inventory.AddObject(gameObject3, null, false, false, true, null);
								try
								{
									TakenEvent.Send(gameObject3, The.Player, "Tinkering");
									TookEvent.Send(gameObject3, The.Player, "Tinkering");
								}
								catch (Exception ex)
								{
									MetricsManager.LogException("Tinkering taken events", ex, "game_exception");
								}
							}
							SoundManager.PlayUISound("sfx_ability_buildRecipeItem", 1f, false, false, SoundRequest.SoundEffectType.None);
							if (part.NumberMade > 1)
							{
								Popup.Show(string.Concat(new string[]
								{
									"You tinker up ",
									Grammar.Cardinal(part.NumberMade),
									" ",
									Grammar.Pluralize(gameObject2.ShortDisplayName),
									"!"
								}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
							}
							else
							{
								Popup.Show("You tinker up " + gameObject3.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + "!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
							}
							GO.UseEnergy(1000, "Skill Tinkering Build", null, null, false);
							if (flag && FromEvent != null)
							{
								FromEvent.RequestInterfaceExit();
								return false;
							}
						}
						finally
						{
							if (GameObject.Validate(ref gameObject2))
							{
								gameObject2.Obliterate(null, false, null);
							}
						}
						return true;
					}
				}
			}
			return true;
		}
	}
}
