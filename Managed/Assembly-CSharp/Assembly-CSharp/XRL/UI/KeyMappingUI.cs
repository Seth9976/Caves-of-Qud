using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleLib.Console;
using Qud.UI;
using XRL.World;

namespace XRL.UI
{
	// Token: 0x020014A5 RID: 5285
	[UIView("KeymappingLegacy", false, true, false, "Keybind", "Keymapping", false, 0, false)]
	public class KeyMappingUI : IWantsTextConsoleInit
	{
		// Token: 0x0600CF8A RID: 53130 RVA: 0x004C1669 File Offset: 0x004BF869
		public void Init(TextConsole console, ScreenBuffer buffer)
		{
			KeyMappingUI.Buffer = buffer;
			KeyMappingUI.TextConsole = console;
		}

		// Token: 0x0600CF8B RID: 53131 RVA: 0x004C1678 File Offset: 0x004BF878
		private static async Task BuildNodes(List<KeyMappingUI.KeyNode> NodeList, ControlManager.InputDeviceType deviceType)
		{
			await The.UiContext;
			NodeList.Clear();
			foreach (string text in CommandBindingManager.CategoriesInOrder)
			{
				KeyMappingUI.KeyNode keyNode = new KeyMappingUI.KeyNode(text, true, null);
				NodeList.Add(keyNode);
				foreach (GameCommand gameCommand in CommandBindingManager.CommandsByCategory[text])
				{
					KeyMappingUI.KeyNode keyNode2 = new KeyMappingUI.KeyNode(gameCommand, true, keyNode);
					string text2;
					string text3;
					CommandBindingManager.GetCommandBindings(gameCommand.ID, deviceType, out text2, out text3);
					keyNode2.primaryBinding = text2;
					keyNode2.secondaryBinding = text3;
					NodeList.Add(keyNode2);
				}
			}
		}

		// Token: 0x0600CF8C RID: 53132 RVA: 0x004C16C4 File Offset: 0x004BF8C4
		public static ScreenReturn Show()
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			ControlManager.InputDeviceType inputDeviceType = ControlManager.InputDeviceType.Keyboard;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			if (Options.ModernUI)
			{
				GameManager.Instance.PushGameView("Keybinds", true);
				Event.ResetPool(true);
				KeyMappingUI.Buffer.Clear();
				KeyMappingUI.Buffer.SingleBox(0, 0, 79, 24, ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
				KeyMappingUI.Buffer.Goto(13, 0);
				KeyMappingUI.Buffer.Write("[ {{w|Control Mapping}} ]", true, false, false, null, -1);
				KeyMappingUI.Buffer.Goto(3, 10);
				KeyMappingUI.Buffer.Write("Loading...", true, false, false, null, -1);
				Popup._TextConsole.DrawBuffer(KeyMappingUI.Buffer, null, false);
				ControlManager.ResetInput(false, true);
				bool result = SingletonWindowBase<KeybindsScreen>.instance.KeybindsMenu().Result;
				GameManager.Instance.PopGameView(true);
				return ScreenReturn.Exit;
			}
			GameManager.Instance.PushGameView("Keymapping", true);
			List<KeyMappingUI.KeyNode> list = new List<KeyMappingUI.KeyNode>();
			KeyMappingUI.BuildNodes(list, inputDeviceType).Wait();
			while (!flag)
			{
				int num5;
				int num6;
				for (;;)
				{
					Event.ResetPool(true);
					KeyMappingUI.Buffer.Clear();
					KeyMappingUI.Buffer.SingleBox(0, 0, 79, 24, ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
					KeyMappingUI.Buffer.Goto(13, 0);
					KeyMappingUI.Buffer.Write("[ {{w|Control Mapping}} ]", true, false, false, null, -1);
					KeyMappingUI.Buffer.Goto(50, 0);
					KeyMappingUI.Buffer.Write(" {{W|ESC}} - Exit ", true, false, false, null, -1);
					int num4 = 2;
					num5 = 0;
					ConsoleTreeNode<KeyMappingUI.KeyNode>.NextVisible(list, ref num, 0);
					if (num2 < num)
					{
						num = num2;
					}
					num6 = num;
					while (num4 <= 21 && num6 <= list.Count)
					{
						while (num6 < list.Count && list[num6].ParentNode != null && !list[num6].ParentNode.Expand)
						{
							num6++;
						}
						if (num6 < list.Count)
						{
							if (list[num6].Command == null)
							{
								KeyMappingUI.Buffer.Goto(4, num4);
								if (list[num6].Expand)
								{
									KeyMappingUI.Buffer.Write("[-] ", true, false, false, null, -1);
								}
								else
								{
									KeyMappingUI.Buffer.Write("[+] ", true, false, false, null, -1);
								}
								KeyMappingUI.Buffer.Write("{{C|" + list[num6].Category + "}}", true, false, false, null, -1);
							}
							else
							{
								KeyMappingUI.Buffer.Goto(4, num4);
								if (num6 == num2)
								{
									KeyMappingUI.Buffer.Write("  {{Y|" + list[num6].Command.DisplayText + "}}", true, false, false, null, -1);
								}
								else
								{
									KeyMappingUI.Buffer.Write("  " + list[num6].Command.DisplayText, true, false, false, null, -1);
								}
								KeyMappingUI.Buffer.Goto(35, num4);
								if (!string.IsNullOrEmpty(list[num6].primaryBinding))
								{
									if (num3 == 0 && num6 == num2)
									{
										KeyMappingUI.Buffer.Write("{{keybind|" + list[num6].primaryBinding + "}}", true, false, false, null, -1);
									}
									else
									{
										KeyMappingUI.Buffer.Write("{{dark keybind|" + list[num6].primaryBinding + "}}", true, false, false, null, -1);
									}
								}
								else if (num3 == 0 && num6 == num2)
								{
									KeyMappingUI.Buffer.Write("{{y|<none>}}", true, false, false, null, -1);
								}
								else
								{
									KeyMappingUI.Buffer.Write("{{K|<none>}}", true, false, false, null, -1);
								}
								KeyMappingUI.Buffer.Goto(60, num4);
								if (!string.IsNullOrEmpty(list[num6].secondaryBinding))
								{
									if (num3 == 1 && num6 == num2)
									{
										KeyMappingUI.Buffer.Write("{{keybind|" + list[num6].secondaryBinding + "}}", true, false, false, null, -1);
									}
									else
									{
										KeyMappingUI.Buffer.Write("{{dark keybind|" + list[num6].secondaryBinding + "}}", true, false, false, null, -1);
									}
								}
								else if (num3 == 1 && num6 == num2)
								{
									KeyMappingUI.Buffer.Write("{{y|<none>}}", true, false, false, null, -1);
								}
								else
								{
									KeyMappingUI.Buffer.Write("{{K|<none>}}", true, false, false, null, -1);
								}
							}
							num5 = num6;
						}
						if (num6 == num2)
						{
							KeyMappingUI.Buffer.Goto(2, num4);
							KeyMappingUI.Buffer.Write("{{Y|>}}", true, false, false, null, -1);
						}
						num4++;
						num6++;
					}
					if (flag4)
					{
						num2 = num5;
						flag4 = false;
					}
					else
					{
						KeyMappingUI.Buffer.Goto(2, 24);
						KeyMappingUI.Buffer.Write(" [{{W|Space}}-Assign] ", true, false, false, null, -1);
						KeyMappingUI.Buffer.Goto(17, 24);
						KeyMappingUI.Buffer.Write(" [{{W|" + ControlManager.getCommandInputDescription("CmdDelete", true, false, ControlManager.InputDeviceType.Unknown, false) + "}}-Delete binding] ", true, false, false, null, -1);
						KeyMappingUI.Buffer.Goto(32, 24);
						KeyMappingUI.Buffer.Write(" [{{W|" + ControlManager.getCommandInputDescription("CmdHelp", true, false, ControlManager.InputDeviceType.Unknown, false) + "}}-Load defaults] ", true, false, false, null, -1);
						if (num != 0)
						{
							KeyMappingUI.Buffer.Goto(4, 1);
							KeyMappingUI.Buffer.Write("{{W|<More...>}}", true, false, false, null, -1);
						}
						if (num6 < list.Count)
						{
							KeyMappingUI.Buffer.Goto(4, 22);
							KeyMappingUI.Buffer.Write("{{W|<More...>}}", true, false, false, null, -1);
						}
						if (num2 > num5 && num < list.Count)
						{
							num++;
						}
						else
						{
							if (!flag3)
							{
								break;
							}
							num2 = num;
							flag3 = false;
						}
					}
				}
				KeyMappingUI.TextConsole.DrawBuffer(KeyMappingUI.Buffer, null, false);
				Keys keys = Keyboard.getvk(false, false, true);
				if (keys == Keys.NumPad2)
				{
					ConsoleTreeNode<KeyMappingUI.KeyNode>.NextVisible(list, ref num2, 1);
				}
				if (keys == Keys.NumPad8)
				{
					ConsoleTreeNode<KeyMappingUI.KeyNode>.PrevVisible(list, ref num2, -1);
				}
				if (keys == Keys.Next)
				{
					if (num5 == num2 && num6 < list.Count)
					{
						ConsoleTreeNode<KeyMappingUI.KeyNode>.NextVisible(list, ref num2, 1);
						num = Math.Min(num2, list.Count - 20);
						flag4 = true;
					}
					else
					{
						num2 = num5;
					}
				}
				if (keys == Keys.Prior)
				{
					if (num == num2 && num > 0)
					{
						ConsoleTreeNode<KeyMappingUI.KeyNode>.PrevVisible(list, ref num2, -1);
						num = Math.Max(num2 - 21, 0);
						flag3 = true;
					}
					else
					{
						num2 = num;
					}
				}
				if (keys == Keys.OemMinus || keys == Keys.Subtract || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:V Negative"))
				{
					list.ForEach(delegate(KeyMappingUI.KeyNode n)
					{
						n.Expand = false;
					});
					ConsoleTreeNode<KeyMappingUI.KeyNode>.PrevVisible(list, ref num2, 0);
				}
				if (keys == Keys.Oemplus || keys == Keys.Add || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:V Positive"))
				{
					list.ForEach(delegate(KeyMappingUI.KeyNode n)
					{
						n.Expand = true;
					});
				}
				if (keys == Keys.NumPad4)
				{
					if (list[num2].Category != "")
					{
						list[num2].Expand = false;
					}
					else if (num3 == 1)
					{
						num3 = 0;
					}
					else
					{
						list[num2].ParentNode.Expand = false;
						ConsoleTreeNode<KeyMappingUI.KeyNode>.PrevVisible(list, ref num2, 0);
					}
				}
				if (keys == Keys.NumPad6)
				{
					if (list[num2].Category != "")
					{
						list[num2].Expand = true;
					}
					else
					{
						num3 = 1;
					}
				}
				if (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdDelete" && list[num2].Command != null && list[num2].Command.ID != "CmdSystemMenu")
				{
					if ((num3 == 0 && string.IsNullOrEmpty(list[num2].primaryBinding)) || (num3 == 1 && string.IsNullOrEmpty(list[num2].secondaryBinding)))
					{
						continue;
					}
					if (!list[num2].Command.CanRemoveBinding())
					{
						Popup.Show("Can not remove the last binding for " + Markup.Color("C", list[num2].Command.DisplayText) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						continue;
					}
					if (Popup.ShowYesNo("Are you sure you want to clear this binding for {{C|" + list[num2].Command.DisplayText + "}}?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes)
					{
						CommandBindingManager.RemoveCommandBindingAsync(list[num2].Command.ID, num3).Wait();
					}
					flag2 = true;
				}
				if ((Keyboard.RawCode == Keys.F1 || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdHelp")) && Popup.ShowYesNo("Are you sure you want to override your bindings with the default?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes)
				{
					CommandBindingManager.RestoreDefaultsAsync().Wait();
					flag2 = true;
				}
				if ((Keyboard.RawCode == Keys.Space || keys == Keys.Enter) && list[num2].Command != null && list[num2].Command.ID != "CmdSystemMenu")
				{
					ScreenBuffer.GetScrapBuffer1(false);
					Popup.RenderBlock("Press control to bind to {{C|" + list[num2].Command.DisplayText + "}}", "", true, true, null, true, false, true);
					KeybindsScreen.HandleRebindAsync(list[num2].Command, num3, inputDeviceType).Wait();
					flag2 = true;
				}
				if (keys == Keys.Escape || keys == Keys.NumPad5)
				{
					if (flag2)
					{
						if (Popup.ShowYesNo("Would you like to save your changes?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes)
						{
							CommandBindingManager.SaveCurrentKeymapAsync().Wait();
						}
						else
						{
							CommandBindingManager.LoadCurrentKeymapAsync(true).Wait();
						}
					}
					flag = true;
				}
				if (flag2)
				{
					KeyMappingUI.BuildNodes(list, inputDeviceType).Wait();
				}
			}
			GameManager.Instance.PopGameView(true);
			return ScreenReturn.Exit;
		}

		// Token: 0x040050C6 RID: 20678
		private static ScreenBuffer Buffer;

		// Token: 0x040050C7 RID: 20679
		private static TextConsole TextConsole;

		// Token: 0x02001DB2 RID: 7602
		private class KeyNode : ConsoleTreeNode<KeyMappingUI.KeyNode>
		{
			// Token: 0x0600F507 RID: 62727 RVA: 0x00554632 File Offset: 0x00552832
			public KeyNode(GameCommand Command, bool Expand, KeyMappingUI.KeyNode ParentNode)
				: base("", Expand, ParentNode)
			{
				this.Command = Command;
			}

			// Token: 0x0600F508 RID: 62728 RVA: 0x00554648 File Offset: 0x00552848
			public KeyNode(string Category, bool Expand, KeyMappingUI.KeyNode ParentNode)
				: base(Category, Expand, ParentNode)
			{
			}

			// Token: 0x040073B9 RID: 29625
			public GameCommand Command;

			// Token: 0x040073BA RID: 29626
			public string primaryBinding;

			// Token: 0x040073BB RID: 29627
			public string secondaryBinding;
		}
	}
}
