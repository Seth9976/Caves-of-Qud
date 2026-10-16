using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleLib.Console;
using Cysharp.Text;
using Qud.UI;
using XRL.Core;
using XRL.World;

namespace XRL.UI
{
	// Token: 0x020014C0 RID: 5312
	public class OptionsUI
	{
		// Token: 0x0600D06A RID: 53354 RVA: 0x004CEB38 File Offset: 0x004CCD38
		private static void BuildNodes(List<OptionsUI.OptionNode> NodeList)
		{
			NodeList.Clear();
			foreach (string text in Options.OptionsByCategory.Keys)
			{
				OptionsUI.OptionNode optionNode = new OptionsUI.OptionNode(text, true, null);
				NodeList.Add(optionNode);
				foreach (GameOption gameOption in Options.OptionsByCategory[text])
				{
					NodeList.Add(new OptionsUI.OptionNode(gameOption, true, optionNode));
				}
			}
		}

		// Token: 0x0600D06B RID: 53355 RVA: 0x004CEBF0 File Offset: 0x004CCDF0
		public static ScreenReturn Show()
		{
			if (Options.ModernUI)
			{
				ControlManager.ResetInput(false, true);
				bool result = SingletonWindowBase<OptionsScreen>.instance.OptionsMenu().Result;
			}
			else
			{
				GameManager.Instance.PushGameView("Options", true);
				ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(false);
				List<OptionsUI.OptionNode> list = new List<OptionsUI.OptionNode>();
				OptionsUI.BuildNodes(list);
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = false;
				int num = 0;
				int num2 = 0;
				while (!flag)
				{
					int num4;
					int num5;
					for (;;)
					{
						IL_005E:
						Event.ResetPool(true);
						scrapBuffer.Clear();
						scrapBuffer.SingleBox(0, 0, 79, 24, ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
						scrapBuffer.Goto(13, 0);
						scrapBuffer.Write("[ &wGame Options&y ]", true, false, false, null, -1);
						scrapBuffer.Goto(50, 0);
						if (CapabilityManager.AllowKeyboardHotkeys)
						{
							scrapBuffer.Write(" &WESC&y - Exit ", true, false, false, null, -1);
						}
						int num3 = 2;
						num4 = 0;
						ConsoleTreeNode<OptionsUI.OptionNode>.NextVisible(list, ref num, 0);
						if (num2 < num)
						{
							num = num2;
						}
						num5 = num;
						while (num3 <= 21 && num5 <= list.Count)
						{
							bool flag5 = false;
							bool flag6 = false;
							string text = null;
							while (num5 < list.Count && !list[num5].Visible)
							{
								num5++;
							}
							if (num5 < list.Count)
							{
								if (list[num5].Option == null)
								{
									scrapBuffer.Goto(4, num3);
									if (list[num5].Expand)
									{
										scrapBuffer.Write("[-] ", true, false, false, null, -1);
									}
									else
									{
										scrapBuffer.Write("[+] ", true, false, false, null, -1);
									}
									scrapBuffer.Write("&C" + list[num5].Category + "&y", true, false, false, null, -1);
								}
								else
								{
									text = list[num5].Option.DisplayText;
									if (list[num5].Option.Type == "Checkbox")
									{
										scrapBuffer.Goto(6, num3);
										flag6 = true;
										if (Options.GetOption(list[num5].Option.ID, "").EqualsNoCase("Yes"))
										{
											scrapBuffer.Write("[&gû&y] ", true, false, false, null, -1);
										}
										else
										{
											scrapBuffer.Write("[&g &y] ", true, false, false, null, -1);
										}
									}
									else if (list[num5].Option.Type == "Button")
									{
										scrapBuffer.Goto(6, num3);
										scrapBuffer.Write("<&Y", true, false, false, null, -1);
										scrapBuffer.Write(list[num5].Option.DisplayText, true, false, false, null, -1);
										scrapBuffer.Write("&y>", true, false, false, null, -1);
									}
									else if (list[num5].Option.Type == "Slider")
									{
										int num6 = (Convert.ToInt32(Options.GetOption(list[num5].Option.ID, "")) - list[num5].Option.Min) / list[num5].Option.Increment;
										int num7 = (list[num5].Option.Max - list[num5].Option.Min) / list[num5].Option.Increment;
										scrapBuffer.Goto(6, num3);
										scrapBuffer.Write("[", true, false, false, null, -1);
										for (int i = 0; i < num6; i++)
										{
											scrapBuffer.Write("#", true, false, false, null, -1);
										}
										for (int j = num6; j < num7; j++)
										{
											scrapBuffer.Write(" ", true, false, false, null, -1);
										}
										scrapBuffer.Write("]", true, false, false, null, -1);
										flag5 = 7 + num7 + text.Length >= 60;
										if (flag5)
										{
											if (num3 < 21)
											{
												num3++;
												scrapBuffer.Goto(8, num3);
												flag6 = true;
											}
											else if (num5 == num2)
											{
												num++;
												goto IL_005E;
											}
										}
										else
										{
											scrapBuffer.Write(" ", true, false, false, null, -1);
											flag6 = true;
										}
									}
									else if (list[num5].Option.Type == "Combo")
									{
										scrapBuffer.Goto(6, num3);
										scrapBuffer.Write("[", true, false, false, null, -1);
										int num8 = 7;
										for (int k = 0; k < list[num5].Option.Values.Length; k++)
										{
											string text2 = list[num5].Option.Values[k];
											string text3 = list[num5].Option.DisplayValues[k];
											num8 += text2.Length + 2;
											scrapBuffer.Write(" ", true, false, false, null, -1);
											if (text2 == list[num5].GetValue())
											{
												scrapBuffer.Write("&W" + text3, true, false, false, null, -1);
											}
											else
											{
												scrapBuffer.Write("&K" + text3, true, false, false, null, -1);
											}
											scrapBuffer.Write(" ", true, false, false, null, -1);
										}
										scrapBuffer.Write("]", true, false, false, null, -1);
										flag5 = num8 + text.Length >= 60;
										if (flag5)
										{
											if (num3 < 21)
											{
												num3++;
												scrapBuffer.Goto(8, num3);
												flag6 = true;
											}
											else if (num5 == num2)
											{
												num++;
												goto IL_005E;
											}
										}
										else
										{
											scrapBuffer.Write(" ", true, false, false, null, -1);
											flag6 = true;
										}
									}
									else if (list[num5].Option.Type == "BigCombo")
									{
										int num9 = 0;
										if (list[num5].GetValue() == "*Max")
										{
											num9 = list[num5].Option.Values.Length - 1;
										}
										int num10 = list[num5].Option.Values.IndexOf(list[num5].GetValue());
										if (num10 > -1)
										{
											num9 = num10;
										}
										scrapBuffer.Goto(6, num3);
										if (num9 > 0)
										{
											scrapBuffer.Write("&W<< &K[more]  ", true, false, false, null, -1);
										}
										if (num9 > 0)
										{
											scrapBuffer.Write("&K" + list[num5].Option.DisplayValues[num9 - 1], true, false, false, null, -1);
										}
										scrapBuffer.Write("  &W" + list[num5].Option.Values[num9] + "  ", true, false, false, null, -1);
										if (num9 < list[num5].Option.Values.Length - 1)
										{
											scrapBuffer.Write("  &K" + list[num5].Option.DisplayValues[num9 + 1], true, false, false, null, -1);
										}
										if (num9 < list[num5].Option.Values.Length - 1)
										{
											scrapBuffer.Write("  &K[more] &W>>", true, false, false, null, -1);
										}
										num3++;
										scrapBuffer.Goto(8, num3);
										flag6 = true;
									}
								}
								if (flag6)
								{
									if (num5 == num2)
									{
										scrapBuffer.Write("&Y" + text, true, false, false, null, -1);
									}
									else
									{
										scrapBuffer.Write(text, true, false, false, null, -1);
									}
								}
								num4 = num5;
							}
							if (num5 == num2)
							{
								scrapBuffer.Goto(2, flag5 ? (num3 - 1) : num3);
								scrapBuffer.Write("&Y>", true, false, false, null, -1);
							}
							num3++;
							num5++;
						}
						if (flag4)
						{
							num2 = num4;
							flag4 = false;
						}
						else
						{
							if (CapabilityManager.AllowKeyboardHotkeys)
							{
								scrapBuffer.Goto(2, 24);
								scrapBuffer.Write(" [&WSpace&y-change option] ", true, false, false, null, -1);
							}
							if (num != 0)
							{
								scrapBuffer.Goto(4, 1);
								scrapBuffer.Write("&W<More...>", true, false, false, null, -1);
							}
							if (num5 < list.Count)
							{
								scrapBuffer.Goto(4, 22);
								scrapBuffer.Write("&W<More...>", true, false, false, null, -1);
							}
							if (num2 > num4 && num < list.Count)
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
					Popup._TextConsole.DrawBuffer(scrapBuffer, null, false);
					Keys keys = Keyboard.getvk(true, false, true);
					if (keys == Keys.NumPad7 || (keys == Keys.NumPad9 && Keyboard.RawCode != Keys.Prior && Keyboard.RawCode != Keys.Next))
					{
						flag = true;
					}
					if (keys == Keys.NumPad2 && num2 < list.Count - 1)
					{
						ConsoleTreeNode<OptionsUI.OptionNode>.NextVisible(list, ref num2, 1);
					}
					if (keys == Keys.NumPad8 && num2 > 0)
					{
						ConsoleTreeNode<OptionsUI.OptionNode>.PrevVisible(list, ref num2, -1);
					}
					if (keys == Keys.Next)
					{
						if (num4 == num2 && num5 < list.Count)
						{
							ConsoleTreeNode<OptionsUI.OptionNode>.NextVisible(list, ref num2, 1);
							num = Math.Min(num2, list.Count - 20);
							flag4 = true;
						}
						else
						{
							num2 = num4;
						}
					}
					if (keys == Keys.Prior)
					{
						if (num == num2 && num > 0)
						{
							ConsoleTreeNode<OptionsUI.OptionNode>.PrevVisible(list, ref num2, -1);
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
						list.ForEach(delegate(OptionsUI.OptionNode n)
						{
							n.Expand = false;
						});
						ConsoleTreeNode<OptionsUI.OptionNode>.PrevVisible(list, ref num2, 0);
					}
					if (keys == Keys.Oemplus || keys == Keys.Add || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:V Positive"))
					{
						list.ForEach(delegate(OptionsUI.OptionNode n)
						{
							n.Expand = true;
						});
					}
					OptionsUI.OptionNode optionNode = list[num2];
					GameOption option = optionNode.Option;
					if (keys == Keys.NumPad4 || keys == Keys.Left)
					{
						if (option != null && (option.Type == "Combo" || option.Type == "BigCombo"))
						{
							int num11 = option.Values.IndexOf(optionNode.GetValue());
							num11--;
							if (num11 < 0)
							{
								num11 = option.Values.Length - 1;
							}
							if (num11 >= option.Values.Length)
							{
								num11 = 0;
							}
							if (option.Restart)
							{
								OptionsUI.RestartOptions.Add(option);
							}
							Options.SetOption(option.ID, option.Values[num11]);
						}
						else if (option != null && option.Type == "Slider")
						{
							int num12 = Convert.ToInt32(optionNode.GetValue()) - option.Increment;
							if (num12 > option.Max)
							{
								num12 = option.Max;
							}
							if (num12 < option.Min)
							{
								num12 = option.Min;
							}
							if (option.Restart)
							{
								OptionsUI.RestartOptions.Add(option);
							}
							Options.SetOption(option.ID, num12.ToStringCached());
						}
						else if (optionNode.Category != "")
						{
							optionNode.Expand = false;
						}
						else
						{
							optionNode.ParentNode.Expand = false;
							ConsoleTreeNode<OptionsUI.OptionNode>.PrevVisible(list, ref num2, 0);
						}
					}
					if (keys == Keys.NumPad6 || keys == Keys.Right)
					{
						if (option != null && (option.Type == "Combo" || option.Type == "BigCombo"))
						{
							int num13 = option.Values.IndexOf(optionNode.GetValue());
							num13++;
							if (num13 < 0)
							{
								num13 = option.Values.Length - 1;
							}
							if (num13 >= option.Values.Length)
							{
								num13 = 0;
							}
							if (option.Restart)
							{
								OptionsUI.RestartOptions.Add(option);
							}
							Options.SetOption(option.ID, option.Values[num13]);
						}
						else if (option != null && option.Type == "Slider")
						{
							int num14 = Convert.ToInt32(optionNode.GetValue()) + option.Increment;
							if (num14 > option.Max)
							{
								num14 = option.Max;
							}
							if (num14 < option.Min)
							{
								num14 = option.Min;
							}
							if (option.Restart)
							{
								OptionsUI.RestartOptions.Add(option);
							}
							Options.SetOption(option.ID, num14.ToString());
						}
						else
						{
							optionNode.Expand = true;
						}
					}
					if ((keys == Keys.Space || keys == Keys.Enter) && option != null)
					{
						if (option.Restart)
						{
							OptionsUI.RestartOptions.Add(option);
						}
						if (option.Type == "Checkbox")
						{
							Options.SetOption(option.ID, (Options.GetOption(option.ID, "") == "Yes") ? "No" : "Yes");
						}
						if (option.ID == "OptionsPrereleaseInputManager")
						{
							ControlManager.ResetInput(false, true);
						}
						if (option.Type == "Button")
						{
							Task<bool> task = option.OnClick.Invoke(null, null) as Task<bool>;
							if (task != null)
							{
								task.Wait();
							}
						}
					}
					if (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Keybindings")
					{
						KeyMappingUI.Show();
					}
					if (keys == Keys.Escape || keys == Keys.NumPad5 || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "RightClick"))
					{
						if (flag2 && Popup.ShowYesNo("Would you like to save your changes?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes)
						{
							CommandBindingManager.SaveKeymap(CommandBindingManager.CurrentMap, Environment.UserName + ".Keymap2.json");
						}
						try
						{
							if (Options.GetOption("OptionUseTiles", "") == "Yes")
							{
								Globals.RenderMode = RenderModeType.Tiles;
							}
							else
							{
								Globals.RenderMode = RenderModeType.Text;
							}
							if (Options.GetOption("OptionAnalytics", "") == "Yes")
							{
								Globals.EnableMetrics = true;
							}
							else
							{
								Globals.EnableMetrics = false;
							}
						}
						catch
						{
						}
						flag = true;
					}
				}
				GameManager.Instance.PopGameView(false);
			}
			if (OptionsUI.RestartOptions.Count != 0)
			{
				using (Utf16ValueStringBuilder utf16ValueStringBuilder = ZString.CreateStringBuilder())
				{
					utf16ValueStringBuilder.Append("These options require a game restart to take effect:\n");
					foreach (GameOption gameOption in OptionsUI.RestartOptions)
					{
						utf16ValueStringBuilder.Append('\n');
						utf16ValueStringBuilder.Append("{{g|");
						utf16ValueStringBuilder.Append("* ");
						utf16ValueStringBuilder.Append(gameOption.DisplayText);
						utf16ValueStringBuilder.Append("}}");
					}
					OptionsUI.RestartOptions.Clear();
					utf16ValueStringBuilder.Append("\n\nDo you want to do so now?");
					if (Popup.ShowYesNo(utf16ValueStringBuilder.ToString(), "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes)
					{
						GameManager.Restart();
					}
				}
			}
			return ScreenReturn.Exit;
		}

		// Token: 0x040051E3 RID: 20963
		public static HashSet<GameOption> RestartOptions = new HashSet<GameOption>();

		// Token: 0x02001DD2 RID: 7634
		private class OptionNode : ConsoleTreeNode<OptionsUI.OptionNode>
		{
			// Token: 0x17000FF8 RID: 4088
			// (get) Token: 0x0600F56B RID: 62827 RVA: 0x00555D90 File Offset: 0x00553F90
			public override bool Visible
			{
				get
				{
					if (base.Visible)
					{
						GameOption option = this.Option;
						bool? flag;
						if (option == null)
						{
							flag = null;
						}
						else
						{
							GameOption.RequiresSpec requires = option.Requires;
							flag = ((requires != null) ? new bool?(requires.RequirementsMet) : null);
						}
						return flag ?? true;
					}
					return false;
				}
			}

			// Token: 0x0600F56C RID: 62828 RVA: 0x00555DED File Offset: 0x00553FED
			public OptionNode(GameOption Option, bool Expand, OptionsUI.OptionNode ParentNode)
				: base("", Expand, ParentNode)
			{
				this.Option = Option;
			}

			// Token: 0x0600F56D RID: 62829 RVA: 0x00555E03 File Offset: 0x00554003
			public OptionNode(string Category, bool Expand, OptionsUI.OptionNode ParentNode)
				: base(Category, Expand, ParentNode)
			{
			}

			// Token: 0x0600F56E RID: 62830 RVA: 0x00555E0E File Offset: 0x0055400E
			public string GetValue()
			{
				if (this.Option == null)
				{
					return null;
				}
				return Options.GetOption(this.Option.ID, "");
			}

			// Token: 0x0400742C RID: 29740
			public GameOption Option;
		}
	}
}
