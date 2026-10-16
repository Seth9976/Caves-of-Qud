using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using XRL.World;
using XRL.World.Effects;

namespace XRL.UI
{
	// Token: 0x020014AE RID: 5294
	public class FactionsScreen : IScreen
	{
		// Token: 0x0600D008 RID: 53256 RVA: 0x004C98B0 File Offset: 0x004C7AB0
		public static string FormatFactionReputation(string sFaction)
		{
			XRLGame game = The.Game;
			int? num;
			if (game == null)
			{
				num = null;
			}
			else
			{
				Reputation playerReputation = game.PlayerReputation;
				num = ((playerReputation != null) ? new int?(playerReputation.Get(sFaction)) : null);
			}
			int? num2 = num;
			int valueOrDefault = num2.GetValueOrDefault();
			char color = Reputation.GetColor(valueOrDefault);
			return string.Format("{{{{{0}|{1}}}}}", color, string.Format("{0,5}", valueOrDefault));
		}

		// Token: 0x0600D009 RID: 53257 RVA: 0x004C9920 File Offset: 0x004C7B20
		private static int WriteFaction(ScreenBuffer SB, GameObject GO, string sFaction, int bSelected, int x, int y)
		{
			Faction faction = Factions.Get(sFaction);
			if (!faction.Visible)
			{
				return 0;
			}
			if (bSelected == 1)
			{
				SB.Goto(x, y);
				SB.Write("> ", true, false, false, null, -1);
			}
			else
			{
				SB.Goto(x + 2, y);
			}
			TextBlock textBlock = new TextBlock(faction.DisplayName, 30, 5, false, 0);
			for (int i = 0; i < textBlock.Lines.Count; i++)
			{
				SB.Goto(x, y + i);
				if (bSelected == 1)
				{
					SB.Write("{{W|" + textBlock.Lines[i] + "}}", true, false, false, null, -1);
				}
				else
				{
					SB.Write(textBlock.Lines[i], true, false, false, null, -1);
				}
			}
			if (bSelected == 2)
			{
				SB.Goto(x + 30, y);
				SB.Write("> ", true, false, false, null, -1);
			}
			else
			{
				SB.Goto(x + 32, y);
			}
			SB.Write(FactionsScreen.FormatFactionReputation(sFaction), true, false, false, null, -1);
			return textBlock.Lines.Count;
		}

		// Token: 0x0600D00A RID: 53258 RVA: 0x004C9A36 File Offset: 0x004C7C36
		public string GetHeaderMessage()
		{
			if (The.Player != null && The.Player.HasEffect(typeof(WakingDream)))
			{
				return "{{C|The reputations of your former life are a mere memory and no longer relevant.}}";
			}
			return null;
		}

		// Token: 0x0600D00B RID: 53259 RVA: 0x004C9A5C File Offset: 0x004C7C5C
		public bool IsRelevant()
		{
			return The.Player != null && !The.Player.HasEffect(typeof(WakingDream));
		}

		// Token: 0x0600D00C RID: 53260 RVA: 0x004C9A80 File Offset: 0x004C7C80
		public static List<string> getFactionsByName()
		{
			List<string> list = new List<string>();
			foreach (Faction faction in Factions.Loop())
			{
				if (faction.Visible)
				{
					list.Add(faction.Name);
				}
			}
			list.Sort(new FactionNameComparer());
			return list;
		}

		// Token: 0x0600D00D RID: 53261 RVA: 0x004C9AEC File Offset: 0x004C7CEC
		public ScreenReturn Show(GameObject GO)
		{
			GameManager.Instance.PushGameView("Factions", true);
			ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(false);
			List<string> factionsByName = new List<string>();
			int topRow = 0;
			int cursorRow = 0;
			int num = 0;
			foreach (Faction faction in Factions.Loop())
			{
				if (faction.Visible)
				{
					factionsByName.Add(faction.Name);
				}
			}
			factionsByName.Sort(new FactionNameComparer());
			bool flag = false;
			TextBlock TB = null;
			int tbStart = 0;
			int nDisplayed = 18;
			int num2 = 0;
			int num3 = -1;
			int num4 = -1;
			Action SetUpTextBlock = delegate
			{
				TB = new TextBlock(Faction.GetRepPageDescription(factionsByName[cursorRow]), 28, 9999, false, 0);
				tbStart = 0;
			};
			Action action = delegate
			{
				cursorRow = Math.Max(cursorRow - 1, 0);
				if (cursorRow == topRow - 1)
				{
					topRow = Math.Max(topRow - 1, 0);
				}
				SetUpTextBlock();
			};
			Action action2 = delegate
			{
				cursorRow = Math.Min(cursorRow + 1, factionsByName.Count - 1);
				if (cursorRow >= topRow + nDisplayed)
				{
					topRow = Math.Min(topRow + (1 + (18 - nDisplayed)), factionsByName.Count - 1);
				}
				SetUpTextBlock();
			};
			SetUpTextBlock();
			string text = "< {{W|7}} Equipment | Quests {{W|9}} >";
			if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
			{
				text = string.Concat(new string[]
				{
					"< {{W|",
					ControlManager.getCommandInputDescription("Page Left", false, false, ControlManager.InputDeviceType.Unknown, false),
					"}} Equipment | Quests {{W|",
					ControlManager.getCommandInputDescription("Page Right", false, false, ControlManager.InputDeviceType.Unknown, false),
					"}} >"
				});
			}
			while (!flag)
			{
				Event.ResetPool(true);
				scrapBuffer.Clear();
				scrapBuffer.SingleBox(0, 0, 79, 24, ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
				scrapBuffer.Goto(35, 0);
				scrapBuffer.Write("[ {{W|Reputation}} ]", true, false, false, null, -1);
				scrapBuffer.Goto(79 - ColorUtility.StripFormatting(text).Length, 24);
				scrapBuffer.Write(text, true, false, false, null, -1);
				scrapBuffer.Goto(60, 0);
				scrapBuffer.Write(" {{W|" + ControlManager.getCommandInputDescription("Cancel", false, false, ControlManager.InputDeviceType.Unknown, false) + "}} to exit ", true, false, false, null, -1);
				int num5 = 3;
				string headerMessage = this.GetHeaderMessage();
				if (!headerMessage.IsNullOrEmpty())
				{
					TextBlock textBlock = new TextBlock(headerMessage, 73, 5, false, 0);
					for (int i = 0; i < textBlock.Lines.Count; i++)
					{
						scrapBuffer.WriteAt(3, num5++, textBlock.Lines[i], true);
					}
					num5++;
				}
				scrapBuffer.Goto(5, num5);
				scrapBuffer.Write("{{W|Faction}}", true, false, false, null, -1);
				scrapBuffer.Goto(35, num5);
				scrapBuffer.Write("{{W|Reputation}}", true, false, false, null, -1);
				num5 += 2;
				int num6 = 23 - num5;
				int num7 = 0;
				while (num7 + tbStart < TB.Lines.Count && num7 < num6)
				{
					scrapBuffer.Goto(50, num5 + num7);
					scrapBuffer.Write(TB.Lines[num7 + tbStart], true, false, false, null, -1);
					num7++;
				}
				if (TB.Lines.Count > num6)
				{
					int num8 = (int)((float)num6 / (float)TB.Lines.Count * 23f);
					int num9 = (int)((float)tbStart / (float)TB.Lines.Count * 23f);
					scrapBuffer.Fill(79, 1, 79, 23, 177, ColorUtility.MakeColor(ColorUtility.Bright(TextColor.Black), TextColor.Black), false, false);
					scrapBuffer.Fill(79, 1 + num9, 79, 1 + num9 + num8, 177, ColorUtility.MakeColor(TextColor.Grey, TextColor.Black), false, false);
				}
				if (num == 2)
				{
					scrapBuffer.Goto(48, num5);
					scrapBuffer.Write("{{Y|>}}", true, false, false, null, -1);
				}
				nDisplayed = 23 - num5;
				int num10 = topRow;
				while (num10 < topRow + nDisplayed && num10 < factionsByName.Count)
				{
					int num11;
					if (num10 == cursorRow)
					{
						num11 = FactionsScreen.WriteFaction(scrapBuffer, GO, factionsByName[num10], 1 + num, 3, num5);
					}
					else
					{
						num11 = FactionsScreen.WriteFaction(scrapBuffer, GO, factionsByName[num10], 0, 3, num5);
					}
					num5 += num11;
					if (num11 > 0)
					{
						nDisplayed -= num11 - 1;
					}
					num2 = num10;
					num10++;
				}
				if (num3 != -1)
				{
					if (topRow + nDisplayed > num3 && cursorRow > 0)
					{
						action();
						continue;
					}
					num3 = -1;
				}
				if (num4 != -1)
				{
					if (cursorRow - nDisplayed < num4 && cursorRow < factionsByName.Count - 1)
					{
						action2();
						continue;
					}
					num4 = -1;
				}
				Popup._TextConsole.DrawBuffer(scrapBuffer, null, false);
				Keys keys = Keyboard.getvk(true, false, true);
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
				if (keys == Keys.Escape || keys == Keys.NumPad5)
				{
					GameManager.Instance.PopGameView(false);
					return ScreenReturn.Exit;
				}
				if (keys == Keys.Right || keys == Keys.NumPad6)
				{
					num++;
				}
				if (keys == Keys.Left || keys == Keys.NumPad4)
				{
					num--;
				}
				if (num < 0)
				{
					num = 0;
				}
				if (num > 2)
				{
					num = 2;
				}
				if (keys == Keys.Up || keys == Keys.NumPad8)
				{
					if (num < 2)
					{
						action();
					}
					else if (tbStart > 0)
					{
						int num12 = tbStart;
						tbStart = num12 - 1;
					}
				}
				if (keys == Keys.Down || keys == Keys.NumPad2)
				{
					if (num < 2)
					{
						action2();
					}
					else if (tbStart < TB.Lines.Count - num6)
					{
						int num12 = tbStart;
						tbStart = num12 + 1;
					}
				}
				if (keys == Keys.Prior)
				{
					if (num < 2)
					{
						if (cursorRow == topRow)
						{
							num3 = cursorRow;
						}
						else
						{
							cursorRow = topRow;
							SetUpTextBlock();
						}
					}
					else
					{
						tbStart = Math.Max(tbStart - num6, 0);
					}
				}
				if (keys == Keys.Next)
				{
					if (num < 2)
					{
						if (cursorRow == num2)
						{
							num4 = cursorRow;
						}
						else
						{
							cursorRow = num2;
							SetUpTextBlock();
						}
					}
					else
					{
						tbStart = Math.Max(Math.Min(tbStart + num6, TB.Lines.Count - num6), 0);
					}
				}
				if (keys == Keys.Enter || keys == Keys.Space)
				{
					if (num == 0)
					{
						factionsByName.Sort(new FactionNameComparer());
					}
					if (num == 1)
					{
						factionsByName.Sort(new FactionRepComparer());
					}
				}
			}
			GameManager.Instance.PopGameView(false);
			return ScreenReturn.Exit;
		}
	}
}
