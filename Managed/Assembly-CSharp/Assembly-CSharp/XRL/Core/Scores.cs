using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using ConsoleLib.Console;
using UnityEngine;
using XRL.UI;

namespace XRL.Core
{
	// Token: 0x0200063A RID: 1594
	[UIView("HighScores", false, false, false, "Menu", "HighScores", false, 0, false)]
	public class Scores : IWantsTextConsoleInit
	{
		// Token: 0x0600429A RID: 17050 RVA: 0x001604F5 File Offset: 0x0015E6F5
		public void Init(TextConsole console, ScreenBuffer buffer)
		{
			Scores.Console = console;
			Scores.Buffer = buffer;
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x0600429B RID: 17051 RVA: 0x00160504 File Offset: 0x0015E704
		public static Scoreboard2 Scoreboard
		{
			get
			{
				if (Scores._Scores != null)
				{
					return Scores._Scores;
				}
				Task<Scoreboard2> task = Scoreboard2.Load();
				task.Wait();
				Scores._Scores = task.Result;
				if (Scores._Scores == null)
				{
					Scores._Scores = new Scoreboard2();
				}
				if (Scores._Scores != null && Scores._Scores.Scores != null)
				{
					foreach (ScoreEntry2 scoreEntry in Scores._Scores.Scores)
					{
						if (scoreEntry != null)
						{
							scoreEntry.CheckVersion();
						}
					}
				}
				return Scores._Scores;
			}
		}

		// Token: 0x0600429C RID: 17052 RVA: 0x001605AC File Offset: 0x0015E7AC
		public static XRLGame Show()
		{
			GameManager.Instance.PushGameView("HighScores", true);
			int page = 0;
			int num = 0;
			int num2 = 0;
			int requestTop = 0;
			int num3 = 0;
			int playersPosition = -1;
			string currentBoard = null;
			string resultHandle = null;
			string text = null;
			string[] array = null;
			string[] array2 = null;
			if (array == null && page == 0 && array2 == null)
			{
				Scores.Scoreboard.Sort();
				List<string> list = new List<string>();
				int num4 = 0;
				foreach (ScoreEntry2 scoreEntry in Scores.Scoreboard.Scores)
				{
					num4++;
					if (num4 > 50)
					{
						break;
					}
				}
				array2 = list.ToArray();
				text = "Local Scores";
				playersPosition = 0;
			}
			StringBuilder stringBuilder = new StringBuilder();
			Dictionary<ScoreEntry2, List<string>> dictionary = new Dictionary<ScoreEntry2, List<string>>();
			for (int i = 0; i < Scores.Scoreboard.Scores.Count; i++)
			{
				stringBuilder.Length = 0;
				stringBuilder.Append(Scores.Scoreboard.Scores[i].Details);
				TextBlock textBlock = new TextBlock(stringBuilder, 75, 24, false, 0);
				dictionary.Add(Scores.Scoreboard.Scores[i], textBlock.Lines);
			}
			for (;;)
			{
				Scores.Buffer.Clear();
				Scores.Buffer.Goto(0, 0);
				Keys keys = Keys.None;
				if (GameManager.Instance.ModernUI)
				{
					if (page == 0)
					{
						array = array2;
					}
					if (array != null)
					{
						new List<string>(array);
						int playersPosition2 = playersPosition;
					}
				}
				Scores.Buffer.Goto(2, 0);
				if (page == 0)
				{
					Scores.Buffer.Write("&Y>Local Scores", true, false, false, null, -1);
				}
				else
				{
					Scores.Buffer.Write(" &yLocal Scores", true, false, false, null, -1);
				}
				Scores.Buffer.Goto(46, 0);
				if (page == 1)
				{
					Scores.Buffer.Write("&Y>Daily", true, false, false, null, -1);
				}
				else
				{
					Scores.Buffer.Write(" &yDaily", true, false, false, null, -1);
				}
				Scores.Buffer.Goto(54, 0);
				if (page == 2)
				{
					Scores.Buffer.Write("&Y>Daily (friends)", true, false, false, null, -1);
				}
				else
				{
					Scores.Buffer.Write(" &yDaily (friends)", true, false, false, null, -1);
				}
				if (page == 0)
				{
					if (Scores.Scoreboard.Scores.Count == 0)
					{
						Scores.Buffer.Goto(35, 12);
						Scores.Buffer.Write("No high scores!", true, false, false, null, -1);
						Scores.Console.DrawBuffer(Scores.Buffer, null, true);
						keys = Keyboard.getvk(true, false, true);
					}
					else
					{
						Scores.Scoreboard.Sort();
						int num5 = 0;
						Scores.Buffer.Goto(0, 2);
						int num6 = num2;
						while (num6 < Scores.Scoreboard.Scores.Count && num6 < num2 + 3)
						{
							stringBuilder.Length = 0;
							if (num6 == num)
							{
								stringBuilder.Append(">");
							}
							else
							{
								stringBuilder.Append(" ");
							}
							List<string> list2 = dictionary[Scores.Scoreboard.Scores[num6]];
							stringBuilder.Append(" #&G").Append(num6 + 1).Append("&y) ")
								.Append(list2[0]);
							Scores.Buffer.Write(stringBuilder, -1);
							Scores.Buffer.Write("\n", true, false, false, null, -1);
							int num7 = 1;
							while (num7 < Math.Min(list2.Count, 7) && Scores.Buffer.Y < 23)
							{
								Scores.Buffer.Write(list2[num7], true, false, false, null, -1);
								Scores.Buffer.Write("\n", true, false, false, null, -1);
								num7++;
							}
							if (Scores.Buffer.Y >= 23)
							{
								break;
							}
							string gameMode = Scores.Scoreboard.Scores[num6].GameMode;
							if (string.IsNullOrEmpty(gameMode))
							{
								Scores.Buffer.Write("This game was played in Classic mode.", true, false, false, null, -1);
							}
							else
							{
								Scores.Buffer.Write("This game was played in " + gameMode + " mode.", true, false, false, null, -1);
							}
							if (Scores.Buffer.Y >= 23)
							{
								break;
							}
							Scores.Buffer.Write("\n", true, false, false, null, -1);
							if (Scores.Buffer.Y >= 23)
							{
								break;
							}
							Scores.Buffer.Write("\n", true, false, false, null, -1);
							num5 = num6;
							num6++;
						}
						if (num2 < Scores.Scoreboard.Scores.Count - 2)
						{
							Scores.Buffer.Goto(0, 24);
							Scores.Buffer.Write("<more...>", true, false, false, null, -1);
						}
						if (CapabilityManager.AllowKeyboardHotkeys)
						{
							if (num >= 0 && num < Scores.Scoreboard.Scores.Count)
							{
								Scoreboard2 scoreboard = Scores.Scoreboard;
								bool? flag;
								if (scoreboard == null)
								{
									flag = null;
								}
								else
								{
									ScoreEntry2 scoreEntry2 = scoreboard.Scores[num];
									flag = ((scoreEntry2 != null) ? new bool?(scoreEntry2.HasCoda()) : null);
								}
								bool? flag2 = flag;
								if (flag2.GetValueOrDefault())
								{
									Scores.Buffer.Goto(43, 24);
									Scores.Buffer.Write("&Y[&WR&y - Revisit Epilogue&Y] &Y[&WD / Del&y - Delete&Y]", true, false, false, null, -1);
									goto IL_057B;
								}
							}
							Scores.Buffer.Goto(62, 24);
							Scores.Buffer.Write("&Y[&WD / Del&y - Delete&Y]", true, false, false, null, -1);
						}
						IL_057B:
						Scores.Console.DrawBuffer(Scores.Buffer, null, true);
						keys = Keyboard.getvk(true, false, true);
						if (keys == Keys.NumPad2 && num < Scores.Scoreboard.Scores.Count - 1)
						{
							num++;
							if (num > num5)
							{
								num2++;
							}
						}
						if (keys == Keys.Next)
						{
							num += 4;
							if (num > Scores.Scoreboard.Scores.Count - 1)
							{
								num = Scores.Scoreboard.Scores.Count - 1;
							}
							num2 = num - 2;
							if (num2 < 0)
							{
								num2 = 0;
							}
						}
						if (keys == Keys.NumPad8 && num > 0)
						{
							num--;
							if (num < num2)
							{
								num2 = num;
							}
						}
						if (keys == Keys.Prior)
						{
							num -= 4;
							if (num < 0)
							{
								num = 0;
							}
							num2 = num;
						}
						if (keys == Keys.R && num >= 0 && num < Scores.Scoreboard.Scores.Count && Scores.Scoreboard.Scores[num].HasCoda())
						{
							break;
						}
						if ((keys == Keys.Space || keys == Keys.Enter) && num >= 0 && num < Scores.Scoreboard.Scores.Count)
						{
							Popup.Show(Scores.Scoreboard.Scores[num].Details, null, "Sounds/UI/ui_notification", true, true, true, true, null);
						}
						if (page == 0 && keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdDelete" && num >= 0 && num < Scores.Scoreboard.Scores.Count && Popup.ShowYesNo("Are you sure you want to delete this?\n\n" + dictionary[Scores.Scoreboard.Scores[num]][0], "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes)
						{
							Scores.Scoreboard.Scores.Remove(Scores.Scoreboard.Scores[num]);
							Scores.Scoreboard.Save().Wait();
							if (num >= Scores.Scoreboard.Scores.Count)
							{
								num--;
							}
							if (num < num2)
							{
								num2 = num;
							}
						}
					}
				}
				else
				{
					if (resultHandle == null || array == null)
					{
						if (!PlatformManager.AreLeaderboardsOffline())
						{
							Scores.Buffer.Goto(35, 12);
							Scores.Buffer.Write("loading scores...", true, false, false, null, -1);
						}
						else
						{
							Scores.Buffer.Goto(28, 12);
							Scores.Buffer.Write("<not connected to provider>", true, false, false, null, -1);
						}
					}
					else
					{
						for (int j = 0; j < 20; j++)
						{
							Scores.Buffer.Goto(2, 2 + j);
							int num8 = requestTop + j;
							if (num8 < array.Length)
							{
								Scores.Buffer.Write(array[num8], true, false, false, null, -1);
							}
						}
						Scores.Buffer.Goto(2, 23);
						Scores.Buffer.Write("Page " + ((int)(Math.Ceiling((double)((float)requestTop / 20f)) + 1.0)).ToString() + " of " + ((int)Math.Ceiling((double)((float)array.Length / 20f))).ToString(), true, false, false, null, -1);
						Scores.Buffer.Goto(2, 24);
						Scores.Buffer.Write("&WDown&y-next page &WUp&y-previous page", true, false, false, null, -1);
						if (text != null)
						{
							Scores.Buffer.Goto(50, 23);
							Scores.Buffer.Write(text, true, false, false, null, -1);
						}
						Scores.Buffer.Goto(50, 24);
						Scores.Buffer.Write("&W7&y-previous board &W9&y-next board", true, false, false, null, -1);
					}
					Scores.Console.DrawBuffer(Scores.Buffer, null, false);
					keys = Keyboard.getvk(true, false, true);
				}
				bool flag3 = false;
				if (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event.StartsWith("Page:"))
				{
					int num9 = Convert.ToInt32(Keyboard.CurrentMouseEvent.Event.Split(':', StringSplitOptions.None)[1]);
					playersPosition = -1;
					page = num9;
					resultHandle = null;
					array = null;
					text = null;
					requestTop = 0;
					num3 = 0;
					if (page < 0)
					{
						page = 4;
					}
					flag3 = true;
				}
				if (keys == Keys.Left || keys == Keys.NumPad4)
				{
					playersPosition = -1;
					int num10 = page;
					page = num10 - 1;
					resultHandle = null;
					array = null;
					text = null;
					requestTop = 0;
					num3 = 0;
					if (page < 0)
					{
						page = 4;
					}
					flag3 = true;
				}
				if (keys == Keys.Right || keys == Keys.NumPad6)
				{
					playersPosition = -1;
					int num10 = page;
					page = num10 + 1;
					resultHandle = null;
					array = null;
					text = null;
					requestTop = 0;
					num3 = 0;
					if (page > 4)
					{
						page = 0;
					}
					flag3 = true;
				}
				if (keys == Keys.NumPad7 || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Page Left"))
				{
					num3--;
					playersPosition = -1;
					requestTop = 0;
					resultHandle = null;
					array = null;
					flag3 = true;
				}
				if (keys == Keys.NumPad9 || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Page Right"))
				{
					num3++;
					playersPosition = -1;
					requestTop = 0;
					resultHandle = null;
					array = null;
					flag3 = true;
				}
				if (keys == Keys.NumPad8 || keys == Keys.Up || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Page Up"))
				{
					requestTop -= 20;
					if (requestTop < 0)
					{
						requestTop = 0;
					}
				}
				if (keys == Keys.NumPad2 || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Page Down"))
				{
					requestTop -= 20;
					if (requestTop < 0)
					{
						requestTop = 0;
					}
				}
				if ((keys == Keys.NumPad2 || keys == Keys.Down) && array != null)
				{
					requestTop += 20;
					if (requestTop > Math.Max(0, array.Length - 20))
					{
						requestTop = Math.Max(0, array.Length - 20);
					}
				}
				if (flag3 && (CapabilityManager.CurrentPlatformClassification() == CapabilityManager.PlatformClassification.Console || !PlatformManager.AreLeaderboardsOffline()))
				{
					string boardID = null;
					if (page == 1)
					{
						CultureInfo cultureInfo = new CultureInfo("en-US");
						DateTime dateTime = DateTime.Now + new TimeSpan(num3, 0, 0, 0, 0);
						if (dateTime > DateTime.Now)
						{
							dateTime = DateTime.Now;
							num3--;
						}
						if (dateTime < new DateTime(2016, 11, 1))
						{
							dateTime = new DateTime(2016, 11, 1);
							num3++;
						}
						int dayOfYear = cultureInfo.Calendar.GetDayOfYear(dateTime);
						boardID = "daily:" + dateTime.Year.ToString() + ":" + dayOfYear.ToString();
						text = "Day " + dayOfYear.ToString() + " of " + dateTime.Year.ToString();
					}
					if (page == 2)
					{
						CultureInfo cultureInfo2 = new CultureInfo("en-US");
						DateTime dateTime2 = DateTime.Now + new TimeSpan(num3, 0, 0, 0, 0);
						if (dateTime2 > DateTime.Now)
						{
							dateTime2 = DateTime.Now;
							num3--;
						}
						if (dateTime2 < new DateTime(2016, 11, 1))
						{
							dateTime2 = new DateTime(2016, 11, 1);
							num3++;
						}
						int dayOfYear2 = cultureInfo2.Calendar.GetDayOfYear(dateTime2);
						boardID = "daily:" + dateTime2.Year.ToString() + ":" + dayOfYear2.ToString();
						text = "Day " + dayOfYear2.ToString() + " of " + dateTime2.Year.ToString();
					}
					try
					{
						if (boardID != null)
						{
							currentBoard = boardID;
							Func<string> getLeaderboard = null;
							Action<DialogResult> <>9__2;
							Action<LeaderboardScores> <>9__1;
							getLeaderboard = delegate
							{
								string boardID2 = boardID;
								int num11 = 0;
								int num12 = 9999;
								bool flag4 = page == 2 || page == 4;
								Action<LeaderboardScores> action;
								if ((action = <>9__1) == null)
								{
									action = (<>9__1 = delegate(LeaderboardScores result)
									{
										if (currentBoard == boardID)
										{
											switch (result.GetResult())
											{
											case LeaderboardScores.Result.Failure:
											{
												string text2 = "An error has occurred, would you like to retry?";
												string text3 = "Sounds/UI/ui_notification";
												bool flag5 = true;
												DialogResult dialogResult = DialogResult.Yes;
												Action<DialogResult> action2;
												if ((action2 = <>9__2) == null)
												{
													action2 = (<>9__2 = delegate(DialogResult result)
													{
														switch (result)
														{
														case DialogResult.Yes:
														case DialogResult.OK:
															getLeaderboard();
															return;
														}
														PlatformManager.SetOfflineMode(true);
													});
												}
												Popup.ShowYesNo(text2, text3, flag5, dialogResult, action2);
												return;
											}
											case LeaderboardScores.Result.Cancelled:
												return;
											}
											StringBuilder stringBuilder2 = new StringBuilder();
											int num13 = Math.Min(result.GetEntryCount(), 9999);
											for (int k = 0; k < num13; k++)
											{
												LeaderboardEntry leaderboardEntry = result.GetLeaderboardEntry(k);
												if (leaderboardEntry.isCurrentUser)
												{
													playersPosition = k;
													stringBuilder2.Append(string.Concat(new string[]
													{
														"&y",
														leaderboardEntry.rank.ToString(),
														": &W",
														leaderboardEntry.displayName,
														" &y(&w",
														leaderboardEntry.score.ToString(),
														"&y)\n"
													}));
												}
												else
												{
													stringBuilder2.Append(string.Concat(new string[]
													{
														"&y",
														leaderboardEntry.rank.ToString(),
														": &y",
														leaderboardEntry.displayName,
														" &y(&w",
														leaderboardEntry.score.ToString(),
														"&y)\n"
													}));
												}
											}
											if (playersPosition != -1)
											{
												requestTop = Math.Max(0, Math.Min(playersPosition - 9, num13 - 20));
											}
											if (!LeaderboardManager.leaderboardresults.ContainsKey(resultHandle))
											{
												LeaderboardManager.leaderboardresults.Add(resultHandle, "");
											}
											LeaderboardManager.leaderboardresults[resultHandle] = stringBuilder2.ToString();
											Debug.Log(stringBuilder2.ToString());
											Keyboard.PushMouseEvent("LeaderboardResultsUpdated", null);
										}
									});
								}
								return LeaderboardManager.GetLeaderboard(boardID2, num11, num12, flag4, action);
							};
							resultHandle = getLeaderboard();
						}
					}
					catch (Exception ex)
					{
						XRLCore.LogError("leaderboard", ex);
					}
				}
				if (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "LeaderboardResultsUpdated")
				{
					array = LeaderboardManager.leaderboardresults[resultHandle].Split('\n', StringSplitOptions.None);
				}
				if (keys == Keys.Escape || keys == Keys.NumPad5)
				{
					goto IL_0E25;
				}
			}
			return Scores.Scoreboard.Scores[num].LoadCoda().Result;
			IL_0E25:
			GameManager.Instance.PopGameView(true);
			return null;
		}

		// Token: 0x040020EF RID: 8431
		public static TextConsole Console;

		// Token: 0x040020F0 RID: 8432
		public static ScreenBuffer Buffer;

		// Token: 0x040020F1 RID: 8433
		public static Scoreboard2 _Scores;
	}
}
