using System;
using System.IO;
using ConsoleLib.Console;
using Platform.IO;
using Qud.UI;
using UnityEngine;
using XRL.Core;
using XRL.World;

namespace XRL.UI
{
	// Token: 0x020014B9 RID: 5305
	public class GameSummaryUI
	{
		// Token: 0x0600D04B RID: 53323 RVA: 0x004CD9C4 File Offset: 0x004CBBC4
		public static void Show(int Score, string Details, string Cause, string Name, string Leaderboard, bool bReal)
		{
			ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(false);
			if (!string.IsNullOrEmpty(The.Game.GetStringGameState("EndgameCause", "")))
			{
				Cause = The.Game.GetStringGameState("EndgameCause", "");
			}
			for (;;)
			{
				IL_0038:
				string text = Details;
				if (Leaderboard != null && bReal)
				{
					if (LeaderboardManager.leaderboardresults.ContainsKey(Leaderboard))
					{
						text = Details.Replace("<%leaderboard%>", "&Cê &ySteam leaderboard results &Cê\n\n" + LeaderboardManager.leaderboardresults[Leaderboard]);
					}
					else
					{
						text = Details.Replace("<%leaderboard%>", "<requesting leaderboard results...>");
					}
				}
				bool flag = false;
				if (Options.ModernUI)
				{
					GameManager.Instance.PushGameView("GameSummaryScreen", true);
					bool result = GameSummaryScreen.ShowGameSummary(Name, Cause, text, bReal).Result;
					GameManager.Instance.PopGameView(false);
					if (bReal)
					{
						break;
					}
					UIManager.instance.currentWindow = null;
					GameManager.Instance.StaticEffecting = 4;
					flag = true;
				}
				GameManager.Instance.PushGameView("GameSummary", true);
				TextBlock textBlock = new TextBlock(text, 76, 99999, false, 0);
				bool flag2 = false;
				int num = 0;
				int num2 = 21;
				int num3 = Math.Max(0, textBlock.Lines.Count - num2);
				while (!flag2)
				{
					XRL.World.Event.ResetPool(false);
					scrapBuffer.Clear();
					scrapBuffer.SingleBox(0, 0, 79, 24, ConsoleLib.Console.ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
					for (int i = 1; i < 24; i++)
					{
						scrapBuffer.Goto(79, i);
						scrapBuffer.Write(177, ConsoleLib.Console.ColorUtility.Bright(0), 0);
					}
					if (textBlock.Lines.Count > num2)
					{
						int num4 = (int)Math.Ceiling((double)textBlock.Lines.Count / (double)num2);
						int num5 = (int)Math.Ceiling((double)(textBlock.Lines.Count + num2) / (double)num2);
						if (num5 <= 0)
						{
							num5 = 1;
						}
						int num6 = 23 / num5;
						if (num6 <= 0)
						{
							num6 = 1;
						}
						int num7 = (int)((double)(23 - num6) * ((double)num / (double)(textBlock.Lines.Count - 23)));
						num7++;
						for (int j = num7; j < num7 + num6; j++)
						{
							scrapBuffer.Goto(79, j);
							scrapBuffer.Write(219, ConsoleLib.Console.ColorUtility.Bright(7), 0);
						}
					}
					scrapBuffer.Goto(13, 0);
					scrapBuffer.Write("[ &wGame Summary && Chronology&y ]", true, false, false, null, -1);
					scrapBuffer.Goto(50, 0);
					scrapBuffer.Write(" &WESC&y - Exit ", true, false, false, null, -1);
					int num8 = num;
					while (num8 < num + num2 && num8 < textBlock.Lines.Count)
					{
						scrapBuffer.Goto(2, num8 - num + 2);
						scrapBuffer.Write(textBlock.Lines[num8], true, false, false, null, -1);
						num8++;
					}
					scrapBuffer.Goto(2, 24);
					scrapBuffer.Write(" [&Ws or F1&y - save tombstone file]", true, false, false, null, -1);
					Popup._TextConsole.DrawBuffer(scrapBuffer, null, false);
					if (flag)
					{
						break;
					}
					Keys keys = Keyboard.getvk(true, false, true);
					if (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "LeaderboardResultsUpdated")
					{
						goto IL_0038;
					}
					if (keys == Keys.M)
					{
						XRLCore.Core.Game.Player.Messages.Show();
					}
					if (keys == Keys.NumPad7 || (keys == Keys.NumPad9 && Keyboard.RawCode != Keys.Prior && Keyboard.RawCode != Keys.Next))
					{
						flag2 = true;
					}
					if (keys == Keys.NumPad2)
					{
						num++;
					}
					if (keys == Keys.Next || keys == Keys.Next || Keyboard.RawCode == Keys.Next || Keyboard.RawCode == Keys.Next)
					{
						num += 20;
					}
					if (keys == Keys.Prior || keys == Keys.Back || Keyboard.RawCode == Keys.Prior || Keyboard.RawCode == Keys.Prior)
					{
						num -= 20;
						if (num < 0)
						{
							num = 0;
						}
					}
					if (keys == Keys.NumPad8 && num > 0)
					{
						num--;
					}
					if (num > num3)
					{
						num = num3;
					}
					if (keys == Keys.S || keys == Keys.F1)
					{
						if (!bReal)
						{
							flag2 = true;
						}
						else
						{
							string text2 = string.Concat(new string[]
							{
								Name,
								"-",
								DateTime.Now.ToShortDateString(),
								"-",
								DateTime.Now.ToShortTimeString()
							});
							string text3 = new string(Platform.IO.Path.GetInvalidFileNameChars()) + new string(Platform.IO.Path.GetInvalidPathChars());
							for (int k = 0; k < text3.Length; k++)
							{
								text2 = text2.Replace(text3[k].ToString(), "");
							}
							text2 = DataManager.SavePath(text2 + ".txt");
							try
							{
								Debug.LogWarning("This is not ported to platform service yet");
								using (Stream stream = Platform.IO.File.Open(text2, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
								{
									using (TextWriter textWriter = new StreamWriter(stream))
									{
										for (int l = 0; l < textBlock.Lines.Count; l++)
										{
											textWriter.WriteLine(ConsoleLib.Console.ColorUtility.StripFormatting(textBlock.Lines[l]));
										}
									}
								}
								Popup.Show("Your tombstone file was saved:\n\n" + text2.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
							}
							catch (Exception)
							{
								Popup.Show("There was an error saving: " + text2.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
							}
						}
					}
					if (keys == Keys.Escape || keys == Keys.NumPad5)
					{
						flag2 = true;
					}
				}
				goto IL_053D;
			}
			return;
			IL_053D:
			GameManager.Instance.PopGameView(false);
		}
	}
}
