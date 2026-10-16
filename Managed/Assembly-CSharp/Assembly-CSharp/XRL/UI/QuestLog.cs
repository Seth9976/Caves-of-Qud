using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.Console;
using XRL.Core;
using XRL.World;

namespace XRL.UI
{
	// Token: 0x020014AB RID: 5291
	public class QuestLog : IScreen
	{
		// Token: 0x0600CFEE RID: 53230 RVA: 0x004C74A8 File Offset: 0x004C56A8
		public static List<string> GetLinesForQuest(Quest Q, bool IncludeTitle = true, bool Clip = true, int ClipWidth = 74)
		{
			List<string> list = new List<string>();
			if (IncludeTitle)
			{
				list.Add(Q.DisplayName);
				list.Add("");
			}
			foreach (QuestStep questStep in Q.StepsByID.Values.OrderBy<QuestStep, int>((QuestStep x) => x.Ordinal))
			{
				if (!questStep.Hidden)
				{
					StringBuilder stringBuilder = Event.NewStringBuilder(null);
					int num = (Clip ? ClipWidth : int.MaxValue);
					if (questStep.Failed)
					{
						stringBuilder.Append("{{red|X}} {{black|");
					}
					else if (questStep.Finished)
					{
						stringBuilder.Append("{{green|û}} {{white|");
					}
					else
					{
						stringBuilder.Append("{{white|ù ");
					}
					if (questStep.Optional)
					{
						stringBuilder.Append("Optional: ");
					}
					stringBuilder.Append(questStep.Name).Append("}}");
					List<string> list2 = StringFormat.ClipTextToArray(Event.FinalizeString(stringBuilder), Clip ? (ClipWidth - 3) : int.MaxValue, out num, false, true, true, false);
					for (int i = 0; i < list2.Count; i++)
					{
						if (i == 0)
						{
							list.Add("{{white|" + list2[i] + "}}");
						}
						if (i > 0)
						{
							list.Add("   {{y|" + list2[i] + "}}");
						}
					}
					if (!questStep.Finished || !questStep.Collapse)
					{
						foreach (string text in StringFormat.ClipTextToArray(questStep.Text, Clip ? (ClipWidth - 3) : int.MaxValue, out num, true, true, true, false))
						{
							list.Add("   {{y|" + text + "}}");
						}
					}
					list.Add("");
				}
			}
			if (!string.IsNullOrEmpty(Q.BonusAtLevel))
			{
				foreach (string text2 in Q.BonusAtLevel.CachedCommaExpansion())
				{
					list.Add("  Bonus reward for completing this quest by level &C" + text2 + "&y.");
				}
			}
			return list;
		}

		// Token: 0x0600CFEF RID: 53231 RVA: 0x004C774C File Offset: 0x004C594C
		public ScreenReturn Show(GameObject GO)
		{
			GameManager.Instance.PushGameView("Quests", true);
			ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(true);
			bool flag = false;
			int num = 0;
			List<string> list = new List<string>();
			list.Add("");
			foreach (Quest quest in XRLCore.Core.Game.Quests.Values)
			{
				if (!XRLCore.Core.Game.FinishedQuests.ContainsKey(quest.ID))
				{
					list.AddRange(QuestLog.GetLinesForQuest(quest, true, true, 74));
					list.Add("");
				}
			}
			string text = "< {{W|7}} Factions | Journal {{W|9}} >";
			if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
			{
				text = string.Concat(new string[]
				{
					"< {{W|",
					ControlManager.getCommandInputDescription("Page Left", false, false, ControlManager.InputDeviceType.Unknown, false),
					"}} Factions | Journal {{W|",
					ControlManager.getCommandInputDescription("Page Right", false, false, ControlManager.InputDeviceType.Unknown, false),
					"}} >"
				});
			}
			while (!flag)
			{
				Event.ResetPool(true);
				scrapBuffer.Clear();
				scrapBuffer.SingleBox(0, 0, 79, 24, ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
				scrapBuffer.Goto(37, 0);
				scrapBuffer.Write("[ &WQuests&y ]", true, false, false, null, -1);
				scrapBuffer.Goto(79 - ColorUtility.StripFormatting(text).Length, 24);
				scrapBuffer.Write(text, true, false, false, null, -1);
				scrapBuffer.Goto(60, 0);
				scrapBuffer.Write(" {{W|" + ControlManager.getCommandInputDescription("Cancel", false, false, ControlManager.InputDeviceType.Unknown, false) + "}} to exit ", true, false, false, null, -1);
				int num2 = 1;
				for (int i = 0; i < 23; i++)
				{
					scrapBuffer.Goto(4, num2 + i);
					if (num + i < list.Count)
					{
						scrapBuffer.Write(list[num + i], true, false, false, null, -1);
					}
				}
				if (list.Count > 23)
				{
					int num3 = (int)(23f / (float)list.Count * 23f);
					int num4 = (int)((float)num / (float)list.Count * 23f);
					scrapBuffer.Fill(79, 1, 79, 23, 177, ColorUtility.MakeColor(ColorUtility.Bright(TextColor.Black), TextColor.Black), false, false);
					scrapBuffer.Fill(79, 1 + num4, 79, 1 + num4 + num3, 177, ColorUtility.MakeColor(TextColor.Grey, TextColor.Black), false, false);
				}
				Popup._TextConsole.DrawBuffer(scrapBuffer, null, false);
				Keys keys = Keyboard.getvk(Options.MapDirectionsToKeypad, false, true);
				if (keys == Keys.NumPad8 && num > 0)
				{
					num--;
				}
				else if (keys == Keys.NumPad2)
				{
					num++;
				}
				else if (keys == Keys.Prior)
				{
					num -= 23;
				}
				else if (keys == Keys.Next)
				{
					num += 23;
				}
				if (num > list.Count - 23)
				{
					num = list.Count - 23;
				}
				if (num < 0)
				{
					num = 0;
				}
				if (keys == Keys.Escape || keys == Keys.NumPad5)
				{
					GameManager.Instance.PopGameView(false);
					return ScreenReturn.Exit;
				}
				if (keys == Keys.Escape || keys == Keys.NumPad7 || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Page Left"))
				{
					GameManager.Instance.PopGameView(false);
					return ScreenReturn.Previous;
				}
				if (keys == Keys.Escape || keys == Keys.NumPad9 || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Page Right"))
				{
					GameManager.Instance.PopGameView(false);
					return ScreenReturn.Next;
				}
			}
			GameManager.Instance.PopGameView(false);
			return ScreenReturn.Exit;
		}
	}
}
