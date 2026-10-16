using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConsoleLib.Console;
using Genkit;
using Qud.UI;
using UnityEngine;
using XRL.Core;
using XRL.Language;
using XRL.Messages;
using XRL.UI.Framework;
using XRL.World;

namespace XRL.UI
{
	// Token: 0x020014CE RID: 5326
	[UIView("Popup", false, false, false, "Menu", null, false, 0, false)]
	public class Popup : IWantsTextConsoleInit
	{
		// Token: 0x0600D095 RID: 53397 RVA: 0x004D2AC2 File Offset: 0x004D0CC2
		[Obsolete("Will be removed, no longer used.")]
		public static void StartThinkingThread(string String)
		{
		}

		// Token: 0x0600D096 RID: 53398 RVA: 0x004D2AC4 File Offset: 0x004D0CC4
		[Obsolete("Will be removed, no longer used.")]
		public static void StartThinkingThread(object oString)
		{
		}

		// Token: 0x0600D097 RID: 53399 RVA: 0x004D2AC6 File Offset: 0x004D0CC6
		[Obsolete("Will be removed, no longer used.")]
		public static void PauseThinking()
		{
			Loading.SetHideLoadStatus(true);
		}

		// Token: 0x0600D098 RID: 53400 RVA: 0x004D2ACE File Offset: 0x004D0CCE
		[Obsolete("Will be removed, no longer used.")]
		public static void ResumeThinking()
		{
			Loading.SetHideLoadStatus(false);
		}

		// Token: 0x0600D099 RID: 53401 RVA: 0x004D2AD6 File Offset: 0x004D0CD6
		[Obsolete("Will be removed, no longer used.  See XRL.UI.Loading.SetLoadingStatus()")]
		public static void StartThinking(string DisplayString)
		{
			Loading.SetLoadingStatus(DisplayString, false);
		}

		// Token: 0x0600D09A RID: 53402 RVA: 0x004D2ADF File Offset: 0x004D0CDF
		[Obsolete("Will be removed, no longer used.  See XRL.UI.Loading.SetLoadingStatus()")]
		public static void EndThinking()
		{
			Loading.SetLoadingStatus(null, false);
		}

		// Token: 0x0600D09B RID: 53403 RVA: 0x004D2AE8 File Offset: 0x004D0CE8
		public void Init(TextConsole TextConsole_, ScreenBuffer ScreenBuffer_)
		{
			Popup._TextConsole = TextConsole_;
			Popup._ScreenBuffer = ScreenBuffer_;
		}

		// Token: 0x0600D09C RID: 53404 RVA: 0x004D2AF8 File Offset: 0x004D0CF8
		public static void RenderDirect(int XPos, int YPos, ScreenBuffer Buffer, string Message, string BottomLineNoFormat, string BottomLine, int StartingLine)
		{
			Message = Markup.Transform(Message, false);
			if (Message.Contains("\r\n"))
			{
				Message = Message.Replace("\r\n", "\n");
			}
			if (Message.Contains("\r"))
			{
				Message = Message.Replace("\r", "\n");
			}
			string[] array = Message.Split('\n', StringSplitOptions.None);
			Popup.RenderDirectLines.Clear();
			for (int i = 0; i < array.Length; i++)
			{
				while (array[i].Length > 78)
				{
					string text = array[i].Substring(0, 78);
					string text2 = array[i].Substring(78, array[i].Length - 78);
					array[i] = text2;
					Popup.RenderDirectLines.Add(text);
				}
				Popup.RenderDirectLines.Add(array[i]);
			}
			int num = BottomLineNoFormat.Length + 2;
			for (int j = 0; j < Popup.RenderDirectLines.Count; j++)
			{
				if (Popup.RenderDirectLines[j].Length + 2 >= num)
				{
					num = Popup.RenderDirectLines[j].Length + 2;
				}
			}
			int num2 = num / 2;
			int num3 = Popup.RenderDirectLines.Count / 2;
			if (num3 < 1)
			{
				num3 = 1;
			}
			num3++;
			num2++;
			int num4 = 12 - num3;
			int num5 = 12 + num3 + 1;
			int num6 = 40 - num2;
			int num7 = 40 + num2;
			TextConsole.Constrain(ref num6, ref num7, ref num4, ref num5);
			Buffer.Fill(num6, num4, num7, num5, 32, ConsoleLib.Console.ColorUtility.MakeColor(TextColor.Black, TextColor.Black), false, false);
			Buffer.ThickSingleBox(num6, num4, num7, num5, ConsoleLib.Console.ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
			int num8 = StartingLine;
			while (num8 < Popup.RenderDirectLines.Count && num4 + 2 + num8 - StartingLine < 24)
			{
				Buffer.Goto(XPos + 2, YPos + 2 + num8 - StartingLine);
				if (StartingLine > 0 && num8 == StartingLine)
				{
					Buffer.Write("<up for more...>", true, false, false, null, -1);
				}
				else if (num4 + 2 + num8 - StartingLine == 23)
				{
					Buffer.Write("<down for more...>", true, false, false, null, -1);
				}
				else
				{
					Buffer.Write(Popup.RenderDirectLines[num8], true, false, false, null, -1);
				}
				num8++;
			}
			int num9 = num2 * 2;
			num9 -= BottomLineNoFormat.Length;
			num9 /= 2;
			num9++;
			Buffer.Goto(num6 + num9, num5);
			Buffer.Write(BottomLine, true, false, false, null, -1);
		}

		// Token: 0x0600D09D RID: 53405 RVA: 0x004D2D5C File Offset: 0x004D0F5C
		public static int Render(List<string> Message, string BottomLineNoFormat, string BottomLine, ScreenBuffer Buffer, int StartingLine)
		{
			int num = 22;
			int num2 = Message.Count;
			if (num2 > 20)
			{
				num2 = 20;
			}
			for (int i = 0; i < Message.Count; i++)
			{
				int num3 = ConsoleLib.Console.ColorUtility.LengthExceptFormatting(Message[i]) + 2;
				if (num3 > num)
				{
					num = num3;
				}
			}
			int num4 = num / 2;
			int num5 = Message.Count / 2;
			if (num5 < 1)
			{
				num5 = 1;
			}
			num5++;
			num4++;
			int num6 = 12 - num5;
			int num7 = 12 + num5 + 1;
			int num8 = 40 - num4;
			int num9 = 40 + num4;
			TextConsole.Constrain(ref num8, ref num9, ref num6, ref num7);
			Buffer.Fill(num8, num6, num9, num7 + 1, 32, ConsoleLib.Console.ColorUtility.MakeColor(TextColor.Black, TextColor.Black), false, false);
			Buffer.ThickSingleBox(num8, num6, num9, num7 + 1, ConsoleLib.Console.ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
			int num10 = StartingLine;
			while (num10 < Message.Count && num6 + 2 + num10 - StartingLine < 24)
			{
				Buffer.Goto(num8 + 2, num6 + 2 + num10 - StartingLine);
				if (StartingLine > 0 && num10 == StartingLine)
				{
					Buffer.Write("<up for more...>", true, false, false, null, -1);
				}
				else if (num6 + 2 + num10 - StartingLine == 23)
				{
					Buffer.Write("<down for more...>", true, false, false, null, -1);
				}
				else
				{
					Buffer.Write(Message[num10], true, false, false, null, -1);
				}
				num10++;
			}
			int num11 = num4 * 2;
			num11 -= BottomLineNoFormat.Length;
			num11 /= 2;
			num11++;
			Buffer.Goto(num8 + num11, num7);
			Buffer.Write(BottomLine, true, false, false, null, -1);
			return num2;
		}

		// Token: 0x0600D09E RID: 53406 RVA: 0x004D2EF0 File Offset: 0x004D10F0
		public static void Render(string Message, string BottomLineNoFormat, string BottomLine, ScreenBuffer Buffer, int StartingLine)
		{
			Message = Markup.Transform(Message, false);
			if (Message.Contains("\r\n"))
			{
				Message = Message.Replace("\r\n", "\n");
			}
			if (Message.Contains("\r"))
			{
				Message = Message.Replace("\r", "\n");
			}
			List<string> list = new List<string>(Message.Split('\n', StringSplitOptions.None));
			int num = 22;
			for (int i = 0; i < list.Count; i++)
			{
				int num2 = ConsoleLib.Console.ColorUtility.LengthExceptFormatting(list[i]) + 2;
				if (num2 > num)
				{
					num = num2;
				}
			}
			int num3 = num / 2;
			int num4 = list.Count / 2;
			if (num4 < 1)
			{
				num4 = 1;
			}
			num4++;
			num3++;
			int num5 = 12 - num4;
			int num6 = 12 + num4 + 1;
			int num7 = 40 - num3;
			int num8 = 40 + num3;
			TextConsole.Constrain(ref num7, ref num8, ref num5, ref num6);
			Buffer.Fill(num7, num5, num8, num6, 32, ConsoleLib.Console.ColorUtility.MakeColor(TextColor.Black, TextColor.Black), false, false);
			Buffer.ThickSingleBox(num7, num5, num8, num6, ConsoleLib.Console.ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
			int num9 = StartingLine;
			while (num9 < list.Count && num5 + 2 + num9 - StartingLine < 24)
			{
				Buffer.Goto(num7 + 2, num5 + 2 + num9 - StartingLine);
				if (StartingLine > 0 && num9 == StartingLine)
				{
					Buffer.Write("<up for more...>", true, false, false, null, -1);
				}
				else if (num5 + 2 + num9 - StartingLine == 23)
				{
					Buffer.Write("<down for more...>", true, false, false, null, -1);
				}
				else
				{
					Buffer.Write(list[num9], true, false, false, null, -1);
				}
				num9++;
			}
			int num10 = num3 * 2;
			num10 -= BottomLineNoFormat.Length;
			num10 /= 2;
			num10++;
			Buffer.Goto(num7 + num10, num6);
			Buffer.Write(BottomLine, true, false, false, null, -1);
		}

		// Token: 0x0600D09F RID: 53407 RVA: 0x004D30C0 File Offset: 0x004D12C0
		public static int RenderBlock(string Message, string BottomLineNoFormat, string BottomLine, ScreenBuffer Buffer, int StartingLine, int MinWidth = -1, int MinHeight = -1, string Title = null, bool BottomLineRegions = false, IRenderable Icon = null, bool centerIcon = true, bool rightIcon = false)
		{
			Message = Markup.Transform(Message, false);
			Title = Markup.Transform(Title, false);
			if (Message.Contains("\r\n"))
			{
				Message = Message.Replace("\r\n", "\n");
			}
			if (Message.Contains("\r"))
			{
				Message = Message.Replace("\r", "\n");
			}
			List<string> lines = new TextBlock(Message, 78, 5000, false, rightIcon ? (-3) : 0).Lines;
			int num = 22;
			if (Icon != null && centerIcon)
			{
				lines.Insert(0, "");
				lines.Insert(0, "");
			}
			if (Title != null)
			{
				int num2 = ConsoleLib.Console.ColorUtility.LengthExceptFormatting(Title) + 2;
				if (num2 > MinWidth)
				{
					MinWidth = num2;
				}
			}
			if (MinWidth > -1)
			{
				num = MinWidth;
			}
			for (int i = 0; i < lines.Count; i++)
			{
				int num3 = ConsoleLib.Console.ColorUtility.LengthExceptFormatting(lines[i]) + 2;
				if (i == 0 && Icon != null && !centerIcon)
				{
					num3 += 3;
				}
				if (num3 > num)
				{
					num = num3;
				}
			}
			int num4 = num / 2;
			int num5 = lines.Count / 2;
			num4++;
			if (MinHeight > -1)
			{
				num5 = MinHeight;
			}
			if (num5 < 2)
			{
				num5 = 2;
			}
			int num6 = 10 - num5;
			int num7 = num6 + lines.Count + 3;
			int num8 = 40 - num4;
			int num9 = 40 + num4;
			TextConsole.Constrain(ref num8, ref num9, ref num6, ref num7);
			if (num9 < MinWidth - 1)
			{
				num9 = MinHeight - 1;
			}
			if (num7 < MinHeight - 1)
			{
				num7 = MinHeight - 1;
			}
			Buffer.Fill(num8, num6, num9, num7, 32, ConsoleLib.Console.ColorUtility.MakeColor(TextColor.Black, TextColor.Black), false, false);
			Buffer.ThickSingleBox(num8, num6, num9, num7, ConsoleLib.Console.ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
			if (Title != null)
			{
				Buffer.Goto(num8 + 1, num6);
				Buffer.Write(Title, true, false, false, null, -1);
			}
			int num10 = StartingLine;
			while (num10 < lines.Count && num6 + 2 + num10 - StartingLine < 24)
			{
				int num11 = num8 + 2;
				int num12 = num6 + 2 + num10 - StartingLine;
				if (num10 == 0 && Icon != null && !centerIcon && !rightIcon)
				{
					Buffer.Goto(num11 + 2, num12);
				}
				else
				{
					Buffer.Goto(num11, num12);
				}
				if (StartingLine > 0 && num10 == StartingLine)
				{
					Buffer.Write("<up for more...>", true, false, false, null, -1);
				}
				else if (num6 + 2 + num10 - StartingLine == 23)
				{
					Buffer.Write("<down for more...>", true, false, false, null, -1);
				}
				else
				{
					Buffer.Write(lines[num10], true, false, false, null, -1);
				}
				if (num10 == 0 && Icon != null)
				{
					if (centerIcon)
					{
						Buffer.Goto(num11 + (num - 1) / 2, num12);
					}
					else if (rightIcon)
					{
						Buffer.Goto(num9 - 3, num12);
						Buffer.Write("   ", true, false, false, null, -1);
						Buffer.Goto(num9 - 2, num12);
					}
					else
					{
						Buffer.Goto(num11, num12);
					}
					Buffer.Write(Icon);
				}
				num10++;
			}
			int num13 = num4 * 2;
			num13 -= BottomLineNoFormat.Length;
			num13 /= 2;
			num13++;
			Buffer.Goto(num8 + num13, num7);
			Buffer.Write(BottomLine, true, false, false, null, -1);
			if (BottomLineRegions)
			{
				GameManager.Instance.ClearRegions();
				List<int> list = new List<int>();
				list.Add(0);
				for (int j = 0; j < BottomLineNoFormat.Length; j++)
				{
					if (BottomLineNoFormat[j] == ' ')
					{
						list.Add(j);
					}
				}
				list.Add(BottomLineNoFormat.Length);
				for (int k = 0; k < list.Count - 1; k++)
				{
					GameManager.Instance.AddRegion(num8 + num13 + list[k], num7 - 1, num8 + num13 + list[k + 1], num7 + 1, "LeftOption:" + (k + 1).ToString(), "RightOption:" + (k + 1).ToString(), null);
				}
			}
			return lines.Count;
		}

		// Token: 0x0600D0A0 RID: 53408 RVA: 0x004D34A8 File Offset: 0x004D16A8
		public static int RenderBlock(StringBuilder Message, string BottomLineNoFormat, string BottomLine, ScreenBuffer Buffer, int StartingLine, int MinWidth, int MinHeight, string Title)
		{
			Markup.Transform(Message, false);
			Title = Markup.Transform(Title, false);
			if (Message.Contains("\r\n"))
			{
				Message = Message.Replace("\r\n", "\n");
			}
			if (Message.Contains("\r"))
			{
				Message = Message.Replace("\r", "\n");
			}
			Popup._RenderBlock.Format(Message);
			List<string> lines = Popup._RenderBlock.Lines;
			int num = 22;
			if (Title != null)
			{
				int num2 = ConsoleLib.Console.ColorUtility.LengthExceptFormatting(Title) + 2;
				if (num2 > MinWidth)
				{
					MinWidth = num2;
				}
			}
			if (MinWidth > -1)
			{
				num = MinWidth;
			}
			for (int i = 0; i < lines.Count; i++)
			{
				int num3 = ConsoleLib.Console.ColorUtility.LengthExceptFormatting(lines[i]) + 2;
				if (num3 > num)
				{
					num = num3;
				}
			}
			int num4 = num / 2;
			int num5 = lines.Count / 2;
			num4++;
			if (MinHeight > -1)
			{
				num5 = MinHeight;
			}
			if (num5 < 2)
			{
				num5 = 2;
			}
			int num6 = 10 - num5;
			int num7 = num6 + lines.Count + 3;
			int num8 = 40 - num4;
			int num9 = 40 + num4;
			TextConsole.Constrain(ref num8, ref num9, ref num6, ref num7);
			Buffer.Fill(num8, num6, num9, num7, 32, ConsoleLib.Console.ColorUtility.MakeColor(TextColor.Black, TextColor.Black), false, false);
			Buffer.ThickSingleBox(num8, num6, num9, num7, ConsoleLib.Console.ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
			if (Title != null)
			{
				Buffer.Goto(num8 + 1, num6);
				Buffer.Write(Title, true, false, false, null, -1);
			}
			int num10 = StartingLine;
			while (num10 < lines.Count && num6 + 2 + num10 - StartingLine < 24)
			{
				Buffer.Goto(num8 + 2, num6 + 2 + num10 - StartingLine);
				if (StartingLine > 0 && num10 == StartingLine)
				{
					Buffer.Write("<up for more...>", true, false, false, null, -1);
				}
				else if (num6 + 2 + num10 - StartingLine == 23)
				{
					Buffer.Write("<down for more...>", true, false, false, null, -1);
				}
				else
				{
					Buffer.Write(lines[num10], true, false, false, null, -1);
				}
				num10++;
			}
			int num11 = num4 * 2;
			num11 -= BottomLineNoFormat.Length;
			num11 /= 2;
			num11++;
			Buffer.Goto(num8 + num11, num7);
			Buffer.Write(BottomLine, true, false, false, null, -1);
			return lines.Count;
		}

		// Token: 0x0600D0A1 RID: 53409 RVA: 0x004D36D8 File Offset: 0x004D18D8
		public static async Task ShowFailAsync(string Message, bool CopyScrap = true, bool Capitalize = true, bool DimBackground = true)
		{
			await Popup.ShowAsync(Message, CopyScrap, Capitalize, DimBackground, false, false);
		}

		// Token: 0x0600D0A2 RID: 53410 RVA: 0x004D3733 File Offset: 0x004D1933
		public static void ShowFail(string Message, bool CopyScrap = true, bool Capitalize = true, bool DimBackground = true)
		{
			Popup.Show(Message, null, "Sounds/UI/ui_notification", CopyScrap, Capitalize, DimBackground, false, null);
		}

		// Token: 0x0600D0A3 RID: 53411 RVA: 0x004D3748 File Offset: 0x004D1948
		public static async Task ShowKeybindAsync(string Message, CancellationToken cancelToken)
		{
			await Popup.NewPopupMessageAsync(Message, PopupMessage.AnyKey, null, null, null, 0, null, null, null, true, true, false, cancelToken, false, "", null, null, null);
		}

		// Token: 0x0600D0A4 RID: 53412 RVA: 0x004D3794 File Offset: 0x004D1994
		public static async Task ShowAsync(string Message, bool CopyScrap = true, bool Capitalize = true, bool DimBackground = true, bool LogMessage = true, bool PushView = false)
		{
			if (Popup.Suppress)
			{
				if (LogMessage)
				{
					MessageQueue.AddPlayerMessage(Message, null, true);
				}
			}
			else if (GameManager.IsOnGameContext() && !Options.ModernUI)
			{
				Popup.Show(Message, null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			else
			{
				await Popup.NewPopupMessageAsync(Message, PopupMessage.SingleButton, null, null, null, 0, null, null, null, true, true, PushView, default(CancellationToken), false, "", null, null, null);
			}
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x0600D0A5 RID: 53413 RVA: 0x004D37E9 File Offset: 0x004D19E9
		// (set) Token: 0x0600D0A6 RID: 53414 RVA: 0x004D37F0 File Offset: 0x004D19F0
		[Obsolete("Use Suppress field")]
		public static bool bSuppressPopups
		{
			get
			{
				return Popup.Suppress;
			}
			set
			{
				Popup.Suppress = value;
			}
		}

		// Token: 0x0600D0A7 RID: 53415 RVA: 0x004D37F8 File Offset: 0x004D19F8
		public static void Show(string Message, string Title = null, string Sound = "Sounds/UI/ui_notification", bool CopyScrap = true, bool Capitalize = true, bool DimBackground = true, bool LogMessage = true, Location2D PopupLocation = null)
		{
			if (Popup.Suppress || Popup._TextConsole == null)
			{
				if (LogMessage)
				{
					MessageQueue.AddPlayerMessage(Message, null, true);
				}
				return;
			}
			Popup.ShowBlock(Message, null, Sound, CopyScrap, Capitalize, DimBackground, LogMessage, PopupLocation);
		}

		// Token: 0x0600D0A8 RID: 53416 RVA: 0x004D3828 File Offset: 0x004D1A28
		public static Keys ShowBlockWithCopy(string Message, string Prompt, string Title, string CopyInfo, bool LogMessage = true)
		{
			string text = "World seed copied to your clipboard.";
			if (UIManager.UseNewPopups)
			{
				bool copied = false;
				Popup.WaitNewPopupMessage(Message, PopupMessage.CopyButton, delegate(QudMenuItem item)
				{
					if (item.command == "Copy")
					{
						ClipboardHelper.SetClipboardData(CopyInfo);
						copied = true;
					}
				}, null, Title, null, 0, null, null, null, true, true, null, null, false);
				Keyboard.ClearInput(false, true);
				if (copied)
				{
					Popup.Show(text, null, "Sounds/UI/ui_notification", false, true, true, true, null);
				}
				return Keys.Space;
			}
			GameManager.Instance.PushGameView("Popup:MessageBox", true);
			Message = Markup.Transform(Options.OverlayUI ? ("{{white|" + Message + "}}") : Message, false);
			Title = Markup.Transform(Title, false);
			if (LogMessage)
			{
				MessageQueue.AddPlayerMessage(Message, null, true);
			}
			int num = 0;
			Keys keys = Keys.Pause;
			while (keys == Keys.Pause || keys == Keys.NumPad2 || keys == Keys.NumPad8 || keys == Keys.Next || keys == Keys.Prior || keys == Keys.Next)
			{
				int num2 = Popup.RenderBlock(Message, ConsoleLib.Console.ColorUtility.StripFormatting(Prompt), Prompt, Popup.ScrapBuffer, num, -1, -1, Title, false, null, true, false);
				Popup._TextConsole.DrawBuffer(Popup.ScrapBuffer, null, false);
				keys = Keyboard.getvk(Options.MapDirectionsToKeypad, true, true);
				if (keys == Keys.NumPad2 && num2 - num > 20)
				{
					num++;
				}
				if (keys == Keys.NumPad8 && num > 0)
				{
					num--;
				}
				if (keys == Keys.Next || keys == Keys.Next)
				{
					num += 23;
				}
				if (keys == Keys.Prior)
				{
					num -= 23;
				}
				if (num < 0)
				{
					num = 0;
				}
				if (keys == Keys.C)
				{
					ClipboardHelper.SetClipboardData(CopyInfo);
					Popup.Show(text, null, "Sounds/UI/ui_notification", false, true, true, true, null);
				}
			}
			Keyboard.ClearInput(false, true);
			GameManager.Instance.PopGameView(true);
			Popup._TextConsole.DrawBuffer(Popup.ScrapBuffer2, null, false);
			Loading.SetHideLoadStatus(false);
			return keys;
		}

		// Token: 0x0600D0A9 RID: 53417 RVA: 0x004D39E9 File Offset: 0x004D1BE9
		public static void ShowProgress(Action<Progress> a)
		{
			new Progress().start(a);
		}

		// Token: 0x0600D0AA RID: 53418 RVA: 0x004D39F8 File Offset: 0x004D1BF8
		public static async Task<T> ShowProgressAsync<T>(Func<Progress, Task<T>> a)
		{
			return await new Progress().startAsync<T>(a);
		}

		// Token: 0x0600D0AB RID: 53419 RVA: 0x004D3A3C File Offset: 0x004D1C3C
		public static async Task<QudMenuItem> NewPopupMessageAsync(string message, List<QudMenuItem> buttons = null, List<QudMenuItem> options = null, string title = null, string inputDefault = null, int DefaultSelected = 0, string contextTitle = null, IRenderable contextRender = null, IRenderable afterRender = null, bool showContextFrame = true, bool EscapeNonMarkupFormatting = true, bool pushView = false, CancellationToken cancelToken = default(CancellationToken), bool askingNumber = false, string RestrictChars = "", string WantsSpecificPrompt = null, Location2D PopupLocation = null, string PopupID = null)
		{
			Popup.<>c__DisplayClass36_0 CS$<>8__locals1 = new Popup.<>c__DisplayClass36_0();
			CS$<>8__locals1.inputDefault = inputDefault;
			CS$<>8__locals1.message = message;
			CS$<>8__locals1.buttons = buttons;
			CS$<>8__locals1.options = options;
			CS$<>8__locals1.title = title;
			CS$<>8__locals1.EscapeNonMarkupFormatting = EscapeNonMarkupFormatting;
			CS$<>8__locals1.DefaultSelected = DefaultSelected;
			CS$<>8__locals1.contextRender = contextRender;
			CS$<>8__locals1.contextTitle = contextTitle;
			CS$<>8__locals1.afterRender = afterRender;
			CS$<>8__locals1.showContextFrame = showContextFrame;
			CS$<>8__locals1.pushView = pushView;
			CS$<>8__locals1.cancelToken = cancelToken;
			CS$<>8__locals1.askingNumber = askingNumber;
			CS$<>8__locals1.RestrictChars = RestrictChars;
			CS$<>8__locals1.WantsSpecificPrompt = WantsSpecificPrompt;
			CS$<>8__locals1.PopupLocation = PopupLocation;
			CS$<>8__locals1.PopupID = PopupID;
			CS$<>8__locals1.t = new TaskCompletionSource<QudMenuItem>();
			NavigationController instance = NavigationController.instance;
			GameManager.Instance.PushGameView("DynamicPopupMessage", true);
			ControlManager.ResetInput(false, true);
			return await instance.SuspendContextWhile<QudMenuItem>(delegate
			{
				Popup.<>c__DisplayClass36_0.<<NewPopupMessageAsync>b__0>d <<NewPopupMessageAsync>b__0>d;
				<<NewPopupMessageAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder<QudMenuItem>.Create();
				<<NewPopupMessageAsync>b__0>d.<>4__this = CS$<>8__locals1;
				<<NewPopupMessageAsync>b__0>d.<>1__state = -1;
				<<NewPopupMessageAsync>b__0>d.<>t__builder.Start<Popup.<>c__DisplayClass36_0.<<NewPopupMessageAsync>b__0>d>(ref <<NewPopupMessageAsync>b__0>d);
				return <<NewPopupMessageAsync>b__0>d.<>t__builder.Task;
			});
		}

		// Token: 0x0600D0AC RID: 53420 RVA: 0x004D3B18 File Offset: 0x004D1D18
		public static async void WaitNewPopupMessage(string message, List<QudMenuItem> buttons = null, Action<QudMenuItem> callback = null, List<QudMenuItem> options = null, string title = null, string inputDefault = null, int DefaultSelected = 0, string contextTitle = null, IRenderable contextRender = null, IRenderable afterRender = null, bool showContextFrame = true, bool EscapeNonMarkupFormatting = true, Location2D PopupLocation = null, string PopupID = null, bool AllowRenderMapBehind = false)
		{
			ControlManager.ResetInput(false, true);
			if (Thread.CurrentThread == GameManager.Instance.uiQueue.threadContext)
			{
				string message2 = message;
				List<QudMenuItem> buttons2 = buttons;
				List<QudMenuItem> options2 = options;
				string title2 = title;
				string inputDefault2 = inputDefault;
				int defaultSelected = DefaultSelected;
				string contextTitle2 = contextTitle;
				IRenderable contextRender2 = contextRender;
				bool escapeNonMarkupFormatting = EscapeNonMarkupFormatting;
				IRenderable afterRender2 = afterRender;
				bool showContextFrame2 = showContextFrame;
				bool flag = false;
				Location2D popupLocation = PopupLocation;
				string popupID = PopupID;
				QudMenuItem qudMenuItem = await Popup.NewPopupMessageAsync(message2, buttons2, options2, title2, inputDefault2, defaultSelected, contextTitle2, contextRender2, afterRender2, showContextFrame2, escapeNonMarkupFormatting, flag, default(CancellationToken), false, "", null, popupLocation, popupID);
				if (callback != null)
				{
					callback(qudMenuItem);
				}
			}
			else
			{
				TaskCompletionSource<QudMenuItem> complete = new TaskCompletionSource<QudMenuItem>();
				Action <>9__3;
				GameManager.Instance.uiQueue.awaitTask(delegate
				{
					PopupMessage messageWindow = UIManager.getWindow("PopupMessage") as PopupMessage;
					try
					{
						GameManager.Instance.PushGameView("PopupMessage", true);
						inputDefault = Sidebar.FromCP437(inputDefault);
						PopupMessage messageWindow2 = messageWindow;
						string message3 = message;
						List<QudMenuItem> list = buttons ?? PopupMessage.SingleButton;
						Action<QudMenuItem> action = delegate(QudMenuItem i)
						{
							try
							{
								if (inputDefault != null)
								{
									i.text = Sidebar.ToCP437(messageWindow.inputBox.text);
								}
								if (EscapeNonMarkupFormatting)
								{
									i.text = i.text.Replace("&", "&&").Replace("^", "^^");
								}
								complete.SetResult(i);
							}
							catch (Exception ex2)
							{
								complete.TrySetException(ex2);
							}
						};
						List<QudMenuItem> list2 = options ?? new List<QudMenuItem>();
						Action<QudMenuItem> action2 = delegate(QudMenuItem i)
						{
							try
							{
								if (inputDefault != null)
								{
									i.text = messageWindow.inputBox.text;
								}
								complete.SetResult(i);
							}
							catch (Exception ex3)
							{
								complete.TrySetException(ex3);
							}
						};
						string title3 = title;
						bool flag2 = inputDefault != null;
						string text2;
						if (!EscapeNonMarkupFormatting)
						{
							text2 = inputDefault;
						}
						else
						{
							string inputDefault3 = inputDefault;
							text2 = ((inputDefault3 != null) ? inputDefault3.Replace("&&", "&").Replace("^^", "^") : null);
						}
						int defaultSelected2 = DefaultSelected;
						Action action3;
						if ((action3 = <>9__3) == null)
						{
							action3 = (<>9__3 = delegate
							{
								complete.TrySetCanceled();
								Loading.SetHideLoadStatus(false);
							});
						}
						string contextTitle3 = contextTitle;
						IRenderable contextRender3 = contextRender;
						string text3 = contextTitle3;
						IRenderable afterRender3 = afterRender;
						bool showContextFrame3 = showContextFrame;
						bool flag3 = true;
						Location2D popupLocation2 = PopupLocation;
						string popupID2 = PopupID;
						messageWindow2.ShowPopup(message3, list, action, list2, action2, title3, flag2, text2, defaultSelected2, action3, contextRender3, text3, afterRender3, showContextFrame3, flag3, default(CancellationToken), false, "", null, popupLocation2, popupID2);
					}
					catch (Exception ex)
					{
						MetricsManager.LogError("Error showing popup", ex);
						complete.TrySetException(ex);
					}
					finally
					{
						ControlManager.ResetInput(false, true);
					}
				});
				if (AllowRenderMapBehind)
				{
					ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(false);
					while (!complete.Task.IsCompleted)
					{
						if (complete.Task.IsCanceled)
						{
							break;
						}
						complete.Task.Wait(33);
						XRLCore core = The.Core;
						if (core != null)
						{
							core.RenderBaseToBuffer(scrapBuffer);
						}
						scrapBuffer.Draw();
					}
				}
				else
				{
					complete.Task.Wait();
				}
				GameManager.Instance.PopGameView(true);
				if (complete.Task.IsCompleted)
				{
					if (callback != null)
					{
						callback(complete.Task.Result);
					}
				}
				else
				{
					if (complete.Task.IsFaulted)
					{
						throw complete.Task.Exception ?? new Exception("Await new popup exception");
					}
					string text = "Popup task was not completed some other way! ";
					TaskCompletionSource<QudMenuItem> complete2 = complete;
					Task<QudMenuItem> task = ((complete2 != null) ? complete2.Task : null);
					throw new Exception(text + ((task != null) ? task.ToString() : null));
				}
			}
		}

		// Token: 0x0600D0AD RID: 53421 RVA: 0x004D3BCC File Offset: 0x004D1DCC
		public static void RenderBlock(string Message, string Prompt, bool Capitalize = true, bool MuteBackground = true, IRenderable Icon = null, bool centerIcon = true, bool rightIcon = false, bool LogMessage = true)
		{
			Message = Markup.Transform(Message, false);
			if (Capitalize)
			{
				Message = ConsoleLib.Console.ColorUtility.CapitalizeExceptFormatting(Message);
			}
			Prompt = Markup.Transform(Prompt, false);
			if (LogMessage)
			{
				MessageQueue.AddPlayerMessage(Message, null, true);
			}
			int num = 0;
			Popup.ScrapBuffer.Copy(TextConsole.CurrentBuffer);
			Popup.ScrapBuffer2.Copy(TextConsole.CurrentBuffer);
			new TextBlock(Message, 80, 5000, false, 0);
			Popup.RenderBlock(Message, ConsoleLib.Console.ColorUtility.StripFormatting(Prompt), Prompt, Popup.ScrapBuffer, num, -1, -1, null, false, Icon, centerIcon, rightIcon);
			Popup._TextConsole.DrawBuffer(Popup.ScrapBuffer, null, true);
			if (Icon != null)
			{
				ScreenBuffer.ClearImposterSuppression();
			}
		}

		// Token: 0x0600D0AE RID: 53422 RVA: 0x004D3C78 File Offset: 0x004D1E78
		public static Keys ShowBlockPrompt(string Message, string Prompt, string Sound = "Sounds/UI/ui_notification", IRenderable Icon = null, bool Capitalize = true, bool MuteBackground = true, bool CenterIcon = true, bool RightIcon = false, bool LogMessage = true)
		{
			if (Popup.Suppress)
			{
				if (LogMessage)
				{
					MessageQueue.AddPlayerMessage(Message, null, true);
				}
				return Keys.Space;
			}
			SoundManager.PlayUISound(Sound, 1f, false, true, SoundRequest.SoundEffectType.None);
			Message = Markup.Transform(Message, false);
			if (Capitalize)
			{
				Message = ConsoleLib.Console.ColorUtility.CapitalizeExceptFormatting(Message);
			}
			Prompt = Markup.Transform(Prompt, false);
			if (LogMessage)
			{
				MessageQueue.AddPlayerMessage(Message, null, true);
			}
			if (UIManager.UseNewPopups || Popup.ScrapBuffer == null)
			{
				Popup.WaitNewPopupMessage(Message, null, null, null, Prompt, null, 0, null, null, null, true, true, null, null, false);
				return Keys.Space;
			}
			int num = 0;
			Popup.ScrapBuffer.Copy(TextConsole.CurrentBuffer);
			Popup.ScrapBuffer2.Copy(TextConsole.CurrentBuffer);
			new TextBlock(Message, 80, 5000, false, 0);
			GameManager.Instance.PushGameView("Popup:MessageBox", true);
			Keys keys = Keys.Pause;
			while (keys == Keys.Pause || keys == Keys.NumPad2 || keys == Keys.NumPad8 || keys == Keys.Next || keys == Keys.Prior || keys == Keys.Next)
			{
				int num2 = Popup.RenderBlock(Message, ConsoleLib.Console.ColorUtility.StripFormatting(Prompt), Prompt, Popup.ScrapBuffer, num, -1, -1, null, false, Icon, CenterIcon, RightIcon);
				Popup._TextConsole.DrawBuffer(Popup.ScrapBuffer, null, true);
				keys = Keyboard.getvk(Options.MapDirectionsToKeypad, false, true);
				if (Icon != null)
				{
					ScreenBuffer.ClearImposterSuppression();
				}
				if (keys == Keys.NumPad2 && num2 - num > 20)
				{
					num++;
				}
				if (keys == Keys.NumPad8 && num > 0)
				{
					num--;
				}
				if (keys == Keys.Next || keys == Keys.Next)
				{
					num += 23;
				}
				if (keys == Keys.Prior)
				{
					num -= 23;
				}
				if (num < 0)
				{
					num = 0;
				}
			}
			Keyboard.ClearInput(false, true);
			GameManager.Instance.PopGameView(true);
			Popup._TextConsole.DrawBuffer(Popup.ScrapBuffer2, null, true);
			return keys;
		}

		// Token: 0x0600D0AF RID: 53423 RVA: 0x004D3E1C File Offset: 0x004D201C
		public static Keys ShowBlockSpace(string Message, string Prompt, bool Capitalize = true, bool MuteBackground = true, IRenderable Icon = null, bool centerIcon = true, bool rightIcon = false, bool LogMessage = true)
		{
			if (Popup.Suppress)
			{
				if (LogMessage)
				{
					MessageQueue.AddPlayerMessage(Message, null, true);
				}
				return Keys.Space;
			}
			Loading.SetHideLoadStatus(true);
			Message = Markup.Transform(Message, false);
			if (Capitalize)
			{
				Message = ConsoleLib.Console.ColorUtility.CapitalizeExceptFormatting(Message);
			}
			Prompt = Markup.Transform(Prompt, false);
			if (LogMessage)
			{
				MessageQueue.AddPlayerMessage(Message, null, true);
			}
			int num = 0;
			new TextBlock(Message, 80, 5000, false, 0);
			if (UIManager.UseNewPopups)
			{
				Popup.WaitNewPopupMessage(Message, null, null, null, "", null, 0, null, null, null, true, true, null, null, false);
				return Keys.Space;
			}
			Popup.ScrapBuffer.Copy(TextConsole.CurrentBuffer);
			Popup.ScrapBuffer2.Copy(TextConsole.CurrentBuffer);
			GameManager.Instance.PushGameView("Popup:MessageBox", true);
			Keys keys = Keys.Pause;
			while (keys != Keys.Escape && keys != Keys.Space && keys != Keys.Enter && keys != Keys.Enter)
			{
				int num2 = Popup.RenderBlock(Message, ConsoleLib.Console.ColorUtility.StripFormatting(Prompt), Prompt, Popup.ScrapBuffer, num, -1, -1, null, false, Icon, centerIcon, rightIcon);
				Popup._TextConsole.DrawBuffer(Popup.ScrapBuffer, null, true);
				keys = Keyboard.getvk(Options.MapDirectionsToKeypad, false, true);
				if (keys == Keys.NumPad2 && num2 - num > 20)
				{
					num++;
				}
				if (keys == Keys.NumPad8 && num > 0)
				{
					num--;
				}
				if (keys == Keys.Next || keys == Keys.Next)
				{
					num += 23;
				}
				if (keys == Keys.Prior)
				{
					num -= 23;
				}
				if (num < 0)
				{
					num = 0;
				}
			}
			Keyboard.ClearInput(false, true);
			GameManager.Instance.PopGameView(true);
			Popup._TextConsole.DrawBuffer(Popup.ScrapBuffer2, null, true);
			return keys;
		}

		// Token: 0x0600D0B0 RID: 53424 RVA: 0x004D3F94 File Offset: 0x004D2194
		public static void ShowSpace(string Message, string Title = null, string Sound = "Sounds/UI/ui_notification", Renderable AfterRender = null, bool LogMessage = true, bool ShowContextFrame = true, string PopupID = null)
		{
			if (Popup.Suppress)
			{
				if (LogMessage)
				{
					MessageQueue.AddPlayerMessage(Message, null, true);
				}
				return;
			}
			SoundManager.PlayUISound(Sound, 1f, false, true, SoundRequest.SoundEffectType.None);
			Message = Markup.Transform(Message, false);
			MessageQueue.AddPlayerMessage(Message, null, true);
			if (UIManager.UseNewPopups)
			{
				Popup.WaitNewPopupMessage(Message, null, null, null, null, null, 0, Title, null, AfterRender, ShowContextFrame, true, null, PopupID, false);
				return;
			}
			new TextBlock(Message, 80, 5000, false, 0);
			int num = 0;
			Popup.ScrapBuffer.Copy(TextConsole.CurrentBuffer);
			Popup.ScrapBuffer2.Copy(TextConsole.CurrentBuffer);
			GameManager.Instance.PushGameView("Popup:MessageBox", true);
			Keyboard.ClearInput(false, true);
			List<QudMenuItem> singleButton = PopupMessage.SingleButton;
			string text = ConsoleLib.Console.ColorUtility.StripFormatting(singleButton[0].text);
			string text2 = singleButton[0].text;
			int num2 = 95;
			while (Keyboard.vkCode != Keys.Space && (num2 != 252 || !(Keyboard.CurrentMouseEvent.Event == "Accept")))
			{
				Popup.RenderBlock(Message, text, text2, Popup.ScrapBuffer, num, -1, -1, null, false, null, true, false);
				Popup._TextConsole.DrawBuffer(Popup.ScrapBuffer, null, true);
				num2 = Keyboard.getch();
			}
			Keyboard.ClearInput(false, true);
			GameManager.Instance.PopGameView(true);
			Popup._TextConsole.DrawBuffer(Popup.ScrapBuffer2, null, true);
		}

		// Token: 0x0600D0B1 RID: 53425 RVA: 0x004D40E0 File Offset: 0x004D22E0
		public static Keys ShowBlock(string Message, string Title = null, string Sound = "Sounds/UI/ui_notification", bool CopyScrap = true, bool Capitalize = true, bool DimBackground = true, bool LogMessage = true, Location2D PopupLocation = null)
		{
			if (Popup.Suppress)
			{
				if (LogMessage)
				{
					MessageQueue.AddPlayerMessage(Message, null, Capitalize);
				}
				return Keys.Space;
			}
			SoundManager.PlayUISound(Sound, 1f, false, true, SoundRequest.SoundEffectType.None);
			Message = Markup.Transform(Message, false);
			if (Capitalize)
			{
				Message = ConsoleLib.Console.ColorUtility.CapitalizeExceptFormatting(Message);
			}
			if (LogMessage)
			{
				MessageQueue.AddPlayerMessage(Message, null, true);
			}
			if (UIManager.UseNewPopups)
			{
				Popup.WaitNewPopupMessage(Message, null, null, null, Title, null, 0, null, null, null, true, true, PopupLocation, null, false);
				return Keys.Space;
			}
			int num = 0;
			if (CopyScrap)
			{
				Popup.ScrapBuffer.Copy(TextConsole.CurrentBuffer);
				Popup.ScrapBuffer2.Copy(TextConsole.CurrentBuffer);
			}
			GameManager.Instance.PushGameView("Popup:MessageBox", true);
			Keyboard.ClearInput(false, true);
			Keys keys = Keys.Pause;
			while (keys != Keys.Space && keys != Keys.Enter && keys != Keys.Escape)
			{
				int num2 = Popup.RenderBlock(Message, "[press space]", "[press {{W|space}}]", Popup.ScrapBuffer, num, -1, -1, Title, false, null, true, false);
				Popup._TextConsole.DrawBuffer(Popup.ScrapBuffer, null, true);
				try
				{
					keys = Keyboard.getvk(Options.MapDirectionsToKeypad, false, true);
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("hm", ex, "game_exception");
				}
				if (keys == Keys.NumPad2 && num2 - num > 20)
				{
					num++;
				}
				if (keys == Keys.NumPad8 && num > 0)
				{
					num--;
				}
				if (keys == Keys.Next || keys == Keys.Next)
				{
					num += 23;
				}
				if (keys == Keys.Prior)
				{
					num -= 23;
				}
				if (num < 0)
				{
					num = 0;
				}
			}
			Keyboard.ClearInput(false, true);
			GameManager.Instance.PopGameView(true);
			Popup._TextConsole.DrawBuffer(Popup.ScrapBuffer2, null, true);
			return keys;
		}

		// Token: 0x0600D0B2 RID: 53426 RVA: 0x004D4270 File Offset: 0x004D2470
		public static int? AskNumber(string Message, string Sound = "Sounds/UI/ui_notification", string RestrictChars = "", int Start = 0, int Min = 0, int Max = 2147483647)
		{
			SoundManager.PlayUISound(Sound, 1f, false, true, SoundRequest.SoundEffectType.None);
			if (RestrictChars == "")
			{
				if (Min < 0)
				{
					RestrictChars = "0123456789-";
				}
				else
				{
					RestrictChars = "0123456789";
				}
			}
			if (UIManager.UseNewPopups)
			{
				int? result = Popup.AskNumberAsync(Message, Start, Min, Max, RestrictChars, true).Result;
				if (result != null)
				{
					result = new int?(Math.Min(Max, result.Value));
					result = new int?(Math.Max(Min, result.Value));
				}
				return result;
			}
			Message = Markup.Transform(Message, false);
			string text = Start.ToString();
			int num = 0;
			text = Start.ToString();
			Popup.ScrapBuffer.Copy(TextConsole.CurrentBuffer);
			Popup.ScrapBuffer2.Copy(TextConsole.CurrentBuffer);
			GameManager.Instance.PushGameView("Popup:AskNumber", true);
			bool flag = false;
			for (;;)
			{
				Popup.ScrapBuffer.Copy(Popup.ScrapBuffer2);
				string text2;
				if (!flag)
				{
					text2 = "&y^K" + text + "^k";
				}
				else
				{
					text2 = text + "_";
				}
				Popup.RenderBlock(Message + "\n" + text2, "[\u0018\u0019\u001a\u001b adjust,Enter or space to confirm]", "[{{W|\u0018\u0019\u001a\u001b}} adjust, {{W|Enter}} or space to confirm]", Popup.ScrapBuffer, num, -1, -1, null, false, null, true, false);
				Popup._TextConsole.DrawBuffer(Popup.ScrapBuffer, null, false);
				int num2 = Keyboard.getch();
				if (Keyboard.vkCode == Keys.Enter || Keyboard.vkCode == Keys.Space)
				{
					goto IL_0441;
				}
				if (Keyboard.vkCode == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event.StartsWith("SubmitString:"))
				{
					break;
				}
				if (Keyboard.vkCode != Keys.MouseEvent)
				{
					if (Keyboard.vkCode == (Keys)131158)
					{
						string clipboardData = ClipboardHelper.GetClipboardData();
						text += clipboardData;
					}
					else
					{
						char c = (char)num2;
						if ((char.IsDigit(c) || char.IsLetter(c) || char.IsPunctuation(c) || char.IsSeparator(c) || char.IsSymbol(c) || num2 == 32 || (num2 == 45 && (!flag || text == "" || text == "0"))) && (RestrictChars == null || RestrictChars.IndexOf(c) != -1))
						{
							if (!flag)
							{
								text = "";
								flag = true;
							}
							text += c.ToString();
						}
					}
				}
				if ((Keyboard.vkCode == Keys.Back || (Keyboard.vkCode == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdDelete")) && text.Length > 0)
				{
					text = text.Substring(0, text.Length - 1);
				}
				if (Keyboard.vkCode == Keys.Escape || (Keyboard.vkCode == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "RightClick"))
				{
					goto IL_02FE;
				}
				if (Keyboard.vkCode == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "LeftClick")
				{
					goto IL_0441;
				}
				if (Keyboard.vkCode == Keys.NumPad8)
				{
					int num3 = 0;
					int.TryParse(text, out num3);
					num3 += 10;
					text = Math.Min(Max, num3).ToString();
				}
				if (Keyboard.vkCode == Keys.NumPad2)
				{
					int num4 = 0;
					int.TryParse(text, out num4);
					num4 -= 10;
					text = Math.Max(Min, num4).ToString();
				}
				if (Keyboard.vkCode == Keys.NumPad6)
				{
					int num5 = 0;
					int.TryParse(text, out num5);
					num5++;
					text = Math.Min(Max, num5).ToString();
				}
				if (Keyboard.vkCode == Keys.NumPad4)
				{
					int num6 = 0;
					int.TryParse(text, out num6);
					num6--;
					text = Math.Max(Min, num6).ToString();
				}
				int num7;
				if (int.TryParse(text, out num7))
				{
					num7 = Math.Min(Max, num7);
					num7 = Math.Max(Min, num7);
					text = num7.ToString();
				}
			}
			text = Keyboard.CurrentMouseEvent.Event.Substring(Keyboard.CurrentMouseEvent.Event.IndexOf(':') + 1);
			Keyboard.ClearInput(false, true);
			GameManager.Instance.PopGameView(true);
			Popup._TextConsole.DrawBuffer(Popup.ScrapBuffer2, null, true);
			Loading.SetHideLoadStatus(false);
			return new int?(Convert.ToInt32(text));
			IL_02FE:
			Keyboard.ClearInput(false, true);
			GameManager.Instance.PopGameView(true);
			Popup._TextConsole.DrawBuffer(Popup.ScrapBuffer2, null, true);
			Loading.SetHideLoadStatus(false);
			return null;
			IL_0441:
			Keyboard.ClearInput(false, true);
			GameManager.Instance.PopGameView(true);
			Popup._TextConsole.DrawBuffer(Popup.ScrapBuffer2, null, true);
			int num8;
			if (int.TryParse(text, out num8))
			{
				num8 = Math.Min(Max, num8);
				num8 = Math.Max(Min, num8);
				return new int?(num8);
			}
			return null;
		}

		// Token: 0x0600D0B3 RID: 53427 RVA: 0x004D4714 File Offset: 0x004D2914
		public static async Task<int?> AskNumberAsync(string Message, int Start = 0, int Min = 0, int Max = 2147483647, string RestrictChars = "", bool pushView = false)
		{
			ControlManager.ResetInput(false, true);
			if (ControlManager.activeControllerType != ControlManager.InputDeviceType.Keyboard)
			{
				if (pushView)
				{
					GameManager.Instance.PushGameView("ModernPopupGamepadAskNumber", true);
				}
				try
				{
					return await AskNumberScreen.show(Message, Start, Min, Max);
				}
				finally
				{
					if (pushView)
					{
						GameManager.Instance.PopGameView(true);
					}
				}
			}
			List<QudMenuItem> submitCancelButton = PopupMessage.SubmitCancelButton;
			int num = Start;
			if (pushView)
			{
				GameManager.Instance.PushGameView("ModernPopup:AskNumber", true);
			}
			int? num2;
			try
			{
				QudMenuItem qudMenuItem = await Popup.NewPopupMessageAsync(StringFormat.ClipText(Message, 60, true, true, false), submitCancelButton, null, null, num.ToString(), 0, null, null, null, true, true, false, default(CancellationToken), true, RestrictChars, null, null, null);
				if (qudMenuItem.command == "Cancel")
				{
					num2 = null;
				}
				else
				{
					num2 = new int?(Convert.ToInt32(qudMenuItem.text));
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogError("AskNumberAsync", ex);
				num2 = null;
			}
			finally
			{
				Keyboard.ClearInput(false, true);
				if (pushView)
				{
					GameManager.Instance.PopGameView(true);
				}
			}
			return num2;
		}

		// Token: 0x0600D0B4 RID: 53428 RVA: 0x004D4784 File Offset: 0x004D2984
		public static async Task<string> AskStringAsync(string Message, string Default = "", int MaxLength = 80, int MinLength = 0, string RestrictChars = null, bool ReturnNullForEscape = false, bool EscapeNonMarkupFormatting = true, bool? AllowColorize = null, bool pushView = false, string WantsSpecificPrompt = null)
		{
			ControlManager.ResetInput(false, true);
			List<QudMenuItem> buttons = ((AllowColorize ?? Options.GetOption("OptionEnableColorTextInput", "").EqualsNoCase("Yes")) ? PopupMessage.SubmitCancelColorButton : PopupMessage.SubmitCancelButton);
			if (WantsSpecificPrompt != null)
			{
				if (CapabilityManager.CurrentPlatformClassification() == CapabilityManager.PlatformClassification.PC)
				{
					buttons = PopupMessage.SubmitCancelHoldButton;
				}
				else if (Default == null)
				{
					buttons = PopupMessage.CancelHoldButton;
				}
			}
			string result = Default;
			if (pushView)
			{
				GameManager.Instance.PushGameView("ModernPopup:AskString", true);
			}
			string text2;
			try
			{
				QudMenuItem qudMenuItem;
				for (;;)
				{
					qudMenuItem = await Popup.NewPopupMessageAsync(StringFormat.ClipText(Message, 60, true, true, false), buttons, null, null, result, 0, null, null, null, true, true, false, default(CancellationToken), false, "", WantsSpecificPrompt, null, null);
					if (!(qudMenuItem.command == "Color"))
					{
						break;
					}
					result = qudMenuItem.text;
					string text = await Popup.ShowColorPickerAsync("Choose color", 0, null, 60, false, true, null, "", true, true, false, ConsoleLib.Console.ColorUtility.StripFormatting(result));
					if (!string.IsNullOrEmpty(text))
					{
						if (string.IsNullOrWhiteSpace(result))
						{
							result = " ";
						}
						result = string.Concat(new string[]
						{
							"{{",
							text,
							"|",
							ConsoleLib.Console.ColorUtility.StripFormatting(result),
							"}}"
						});
					}
				}
				if (qudMenuItem.command == "Cancel")
				{
					text2 = (ReturnNullForEscape ? null : "");
				}
				else
				{
					text2 = qudMenuItem.text;
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogError("AskStringAsync", ex);
				text2 = null;
			}
			finally
			{
				Keyboard.ClearInput(false, true);
				if (pushView)
				{
					GameManager.Instance.PopGameView(true);
				}
			}
			return text2;
		}

		// Token: 0x0600D0B5 RID: 53429 RVA: 0x004D47F4 File Offset: 0x004D29F4
		public static string AskString(string Message, string Default = "", string Sound = "Sounds/UI/ui_notification", string RestrictChars = null, string WantsSpecificPrompt = null, int MaxLength = 80, int MinLength = 0, bool ReturnNullForEscape = false, bool EscapeNonMarkupFormatting = true, bool? AllowColorize = null)
		{
			SoundManager.PlayUISound(Sound, 1f, false, true, SoundRequest.SoundEffectType.None);
			Message = Markup.Transform(Message, false);
			int num = 0;
			string text = Default ?? "";
			if (UIManager.UseNewPopups)
			{
				return Popup.AskStringAsync(Message, Default, MaxLength, MinLength, RestrictChars, ReturnNullForEscape, EscapeNonMarkupFormatting, AllowColorize, true, WantsSpecificPrompt).Result;
			}
			Popup.ScrapBuffer.Copy(TextConsole.CurrentBuffer);
			Popup.ScrapBuffer2.Copy(TextConsole.CurrentBuffer);
			GameManager.Instance.PushGameView("Popup:AskString", true);
			int num2 = 95;
			while (num2 != 13)
			{
				Popup.ScrapBuffer.Copy(Popup.ScrapBuffer2);
				Popup.RenderBlock(Message + "\n" + text + "_", "[Enter to confirm]", "[{{W|Enter}} to confirm]", Popup.ScrapBuffer, num, -1, -1, null, false, null, true, false);
				Popup._TextConsole.DrawBuffer(Popup.ScrapBuffer, null, true);
				num2 = Keyboard.getch();
				if (Keyboard.vkCode == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event.StartsWith("SubmitString:"))
				{
					text = Keyboard.CurrentMouseEvent.Event.Substring(Keyboard.CurrentMouseEvent.Event.IndexOf(':') + 1);
					if (text.Length >= MinLength)
					{
						Keyboard.ClearInput(false, true);
						GameManager.Instance.PopGameView(true);
						Popup._TextConsole.DrawBuffer(Popup.ScrapBuffer2, null, true);
						Loading.SetHideLoadStatus(false);
						return text;
					}
				}
				if (Keyboard.vkCode != Keys.MouseEvent)
				{
					if (Keyboard.vkCode == (Keys)131158)
					{
						string clipboardData = ClipboardHelper.GetClipboardData();
						text += clipboardData;
					}
					else
					{
						char c = (char)num2;
						if ((char.IsDigit(c) || char.IsLetter(c) || char.IsPunctuation(c) || char.IsSeparator(c) || char.IsSymbol(c) || num2 == 32) && text.Length < MaxLength && (RestrictChars == null || RestrictChars.IndexOf(c) != -1))
						{
							text += c.ToString();
							if (EscapeNonMarkupFormatting && (c == '&' || c == '^'))
							{
								text += c.ToString();
							}
						}
					}
				}
				if (Keyboard.vkCode == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdDelete")
				{
					if (EscapeNonMarkupFormatting && text.Length > 1 && (text.EndsWith("&&") || text.EndsWith("^^")))
					{
						text = text.Substring(0, text.Length - 2);
					}
					else if (text.Length > 0)
					{
						text = text.Substring(0, text.Length - 1);
					}
				}
				if ((Keyboard.vkCode == Keys.Escape || (Keyboard.vkCode == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "RightClick")) && MinLength <= 0)
				{
					Keyboard.ClearInput(false, true);
					GameManager.Instance.PopGameView(true);
					Popup._TextConsole.DrawBuffer(Popup.ScrapBuffer2, null, true);
					Loading.SetHideLoadStatus(false);
					if (!ReturnNullForEscape)
					{
						return "";
					}
					return null;
				}
				else if (Keyboard.vkCode == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "LeftClick" && text.Length >= MinLength)
				{
					break;
				}
			}
			Keyboard.ClearInput(false, true);
			GameManager.Instance.PopGameView(true);
			Popup._TextConsole.DrawBuffer(Popup.ScrapBuffer2, null, true);
			return text;
		}

		// Token: 0x0600D0B6 RID: 53430 RVA: 0x004D4B30 File Offset: 0x004D2D30
		public static XRL.World.GameObject PickGameObject(string Title, IList<XRL.World.GameObject> Objects, bool AllowEscape = false, bool ShowContext = false, bool UseYourself = true, Func<XRL.World.GameObject, string> ExtraLabels = null, int DefaultSelected = 0, bool ShortDisplayNames = false)
		{
			string[] array = new string[Objects.Count];
			char[] array2 = new char[Objects.Count];
			IRenderable[] array3 = new IRenderable[Objects.Count];
			char c = 'a';
			int i = 0;
			int count = Objects.Count;
			while (i < count)
			{
				string text;
				if (UseYourself && Objects[i] == The.Player)
				{
					text = "yourself";
				}
				else
				{
					text = Objects[i].GetDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, ShortDisplayNames, false, false, false, null, false, false, false, false, null, false, null, false, true);
					string text2 = ((ExtraLabels != null) ? ExtraLabels(Objects[i]) : null);
					string text3 = (ShowContext ? Objects[i].GetListDisplayContext(The.Player) : null);
					if (!text2.IsNullOrEmpty())
					{
						if (!text3.IsNullOrEmpty())
						{
							text = string.Concat(new string[] { text, " [", text2, ", ", text3, "]" });
						}
						else
						{
							text = text + " [" + text2 + "]";
						}
					}
					else if (!text3.IsNullOrEmpty())
					{
						text = text + " [" + text3 + "]";
					}
				}
				array[i] = text;
				char[] array4 = array2;
				int num = i;
				int num2;
				if (c > 'z')
				{
					num2 = 32;
				}
				else
				{
					c = (char)((num2 = (int)c) + 1);
				}
				array4[num] = num2;
				array3[i] = Popup.contextRender(Objects[i]);
				i++;
			}
			int num3 = Popup.PickOption(Title, null, "", "Sounds/UI/ui_notification", array, array2, array3, null, null, null, null, 0, 60, DefaultSelected, -1, AllowEscape, false, false, true, false, null, null);
			if (num3 < 0)
			{
				return null;
			}
			return Objects[num3];
		}

		// Token: 0x0600D0B7 RID: 53431 RVA: 0x004D4CE8 File Offset: 0x004D2EE8
		private static RenderEvent contextRender(XRL.World.GameObject context)
		{
			if (context == null)
			{
				return null;
			}
			return context.RenderForUI(null, false);
		}

		// Token: 0x0600D0B8 RID: 53432 RVA: 0x004D4CF7 File Offset: 0x004D2EF7
		[Obsolete("Use ShowConversation(IRenderable)")]
		public static int ShowConversation(string Title, XRL.World.GameObject Context, string Intro = null, List<string> Options = null, bool AllowTrade = false, bool AllowEscape = true, bool AllowRenderMapBehind = false)
		{
			return Popup.ShowConversation(Title, Context.RenderForUI(null, false), Intro, Options, AllowTrade, AllowEscape, AllowRenderMapBehind);
		}

		// Token: 0x0600D0B9 RID: 53433 RVA: 0x004D4D10 File Offset: 0x004D2F10
		public static int ShowConversation(string Title, IRenderable Icon = null, string Intro = null, List<string> Options = null, bool AllowTrade = false, bool AllowEscape = true, bool AllowRenderMapBehind = false)
		{
			List<QudMenuItem> list = new List<QudMenuItem>();
			if (AllowEscape)
			{
				list.Add(PopupMessage.CancelButton[0]);
			}
			if (AllowTrade)
			{
				list.Insert(0, PopupMessage.AcceptCancelTradeButton[0]);
			}
			list.Insert(0, PopupMessage.LookButton);
			int SelectedOption = 0;
			List<QudMenuItem> list2 = new List<QudMenuItem>(Options.Count);
			for (int i = 0; i < Options.Count; i++)
			{
				list2.Add(new QudMenuItem
				{
					text = ((i < 9 && CapabilityManager.AllowKeyboardHotkeys) ? ("{{w|[" + (i + 1).ToString() + "]}} ") : "") + Options[i] + "\n\n",
					command = "option:" + i.ToString(),
					hotkey = ((i < 9) ? ("Alpha" + (i + 1).ToString()) : null)
				});
			}
			Popup.WaitNewPopupMessage(Intro + "\n\n", list, delegate(QudMenuItem item)
			{
				if (item.command == "Cancel")
				{
					SelectedOption = -1;
				}
				if (item.command == "trade")
				{
					SelectedOption = -2;
				}
				if (item.command == "Look")
				{
					SelectedOption = -3;
				}
				string command = item.command;
				if (command != null && command.StartsWith("option:"))
				{
					SelectedOption = Convert.ToInt32(item.command.Substring("option:".Length));
				}
			}, list2, "", null, 0, Title, Icon, null, true, true, null, "Conversation:" + Title, AllowRenderMapBehind);
			return SelectedOption;
		}

		// Token: 0x0600D0BA RID: 53434 RVA: 0x004D4E5C File Offset: 0x004D305C
		[Obsolete("Use PickOptionAsync")]
		public static async Task<int> ShowOptionListAsync(string Title = "", IReadOnlyList<string> Options = null, IReadOnlyList<char> Hotkeys = null, int Spacing = 0, string Intro = null, int MaxWidth = 60, bool RespectOptionNewlines = false, bool AllowEscape = false, int DefaultSelected = 0, string SpacingText = "", XRL.World.GameObject context = null, IReadOnlyList<IRenderable> Icons = null, IRenderable IntroIcon = null, bool centerIntro = false, bool centerIntroIcon = true, int iconPosition = -1)
		{
			return await Popup.PickOptionAsync(Title, Intro, SpacingText, Options, Hotkeys, Icons, context, IntroIcon, Spacing, MaxWidth, DefaultSelected, iconPosition, RespectOptionNewlines, AllowEscape, centerIntro, centerIntroIcon);
		}

		// Token: 0x0600D0BB RID: 53435 RVA: 0x004D4F24 File Offset: 0x004D3124
		public static async Task<int> PickOptionAsync(string Title = "", string Intro = null, string SpacingText = "", IReadOnlyList<string> Options = null, IReadOnlyList<char> Hotkeys = null, IReadOnlyList<IRenderable> Icons = null, XRL.World.GameObject Context = null, IRenderable IntroIcon = null, int Spacing = 0, int MaxWidth = 60, int DefaultSelected = 0, int IconPosition = -1, bool RespectOptionNewlines = false, bool AllowEscape = false, bool CenterIntro = false, bool CenterIntroIcon = true)
		{
			NavigationController framework = NavigationController.instance;
			NavigationContext oldContext = framework.activeContext;
			framework.activeContext = NavigationController.instance.suspensionContext;
			int num = -1;
			try
			{
				TaskCompletionSource<int> t = new TaskCompletionSource<int>();
				Popup.PickOption(Title, Intro, SpacingText, "Sounds/UI/ui_notification", Options, Hotkeys, Icons, null, Context, IntroIcon, delegate(int choice)
				{
					t.TrySetResult(choice);
				}, Spacing, MaxWidth, DefaultSelected, IconPosition, AllowEscape, RespectOptionNewlines, CenterIntro, CenterIntroIcon, true, null, null);
				num = await t.Task;
			}
			finally
			{
				framework.activeContext = oldContext;
			}
			return num;
		}

		// Token: 0x0600D0BC RID: 53436 RVA: 0x004D4FEC File Offset: 0x004D31EC
		[Obsolete("Use PickOption")]
		public static int ShowOptionList(string Title = "", IReadOnlyList<string> Options = null, IReadOnlyList<char> Hotkeys = null, int Spacing = 0, string Intro = null, int MaxWidth = 60, bool RespectOptionNewlines = false, bool AllowEscape = false, int DefaultSelected = 0, string SpacingText = "", Action<int> onResult = null, XRL.World.GameObject context = null, IReadOnlyList<IRenderable> Icons = null, IRenderable IntroIcon = null, IReadOnlyList<QudMenuItem> Buttons = null, bool centerIntro = false, bool centerIntroIcon = true, int iconPosition = -1, bool forceNewPopup = false)
		{
			return Popup.PickOption(Title, Intro, SpacingText, "Sounds/UI/ui_notification", Options, Hotkeys, Icons, Buttons, context, IntroIcon, onResult, Spacing, MaxWidth, DefaultSelected, iconPosition, AllowEscape, RespectOptionNewlines, centerIntro, centerIntroIcon, forceNewPopup, null, null);
		}

		// Token: 0x0600D0BD RID: 53437 RVA: 0x004D5028 File Offset: 0x004D3228
		public static int PickOption(string Title = "", string Intro = null, string SpacingText = "", string Sound = "Sounds/UI/ui_notification", IReadOnlyList<string> Options = null, IReadOnlyList<char> Hotkeys = null, IReadOnlyList<IRenderable> Icons = null, IReadOnlyList<QudMenuItem> Buttons = null, XRL.World.GameObject Context = null, IRenderable IntroIcon = null, Action<int> OnResult = null, int Spacing = 0, int MaxWidth = 60, int DefaultSelected = 0, int IconPosition = -1, bool AllowEscape = false, bool RespectOptionNewlines = false, bool CenterIntro = false, bool CenterIntroIcon = true, bool ForceNewPopup = false, Location2D PopupLocation = null, string PopupID = null)
		{
			SoundManager.PlayUISound(Sound, 1f, false, true, SoundRequest.SoundEffectType.None);
			if (Context != null && Intro == null && !UIManager.UseNewPopups)
			{
				Intro = Context.DisplayName;
			}
			Title = Markup.Transform(Title, false);
			Popup.ScrapBuffer.Copy(TextConsole.CurrentBuffer);
			if (MaxWidth > 74)
			{
				MaxWidth = 74;
			}
			int num = 0;
			int SelectedOption = DefaultSelected;
			int num2 = 20;
			int num3 = 0;
			if (Hotkeys != null)
			{
				num3 += 3;
			}
			if (Icons != null && IconPosition == -1)
			{
				num3 += 2;
			}
			Popup.PopupTextBuilder popupTextBuilder = new Popup.PopupTextBuilder
			{
				SpacingText = SpacingText,
				Spacing = Spacing
			};
			popupTextBuilder.RespectNewlines = RespectOptionNewlines;
			popupTextBuilder.MaxWidth = MaxWidth - 4;
			if (!Intro.IsNullOrEmpty())
			{
				popupTextBuilder.AddSection(Intro, -1);
				popupTextBuilder.AddSpacing(1, "");
			}
			popupTextBuilder.Padding = 2 + num3;
			for (int i = 0; i < Options.Count; i++)
			{
				popupTextBuilder.AddSection(Options[i], i);
				if (i != Options.Count - 1)
				{
					popupTextBuilder.AddSpacing(Spacing, null);
				}
			}
			int num4 = Math.Min(MaxWidth, Math.Max(ConsoleLib.Console.ColorUtility.LengthExceptFormatting(Title) + 2, popupTextBuilder.MaxClippedWidth + 4));
			if (popupTextBuilder.Lines.Count < num2)
			{
				num2 = popupTextBuilder.Lines.Count;
			}
			else
			{
				num = SelectedOption;
			}
			if (IntroIcon != null)
			{
				num2 += 2;
			}
			if (UIManager.UseNewPopups || ForceNewPopup)
			{
				List<QudMenuItem> list = new List<QudMenuItem>(Options.Count);
				for (int j = 0; j < Options.Count; j++)
				{
					list.Add(Popup.GetPopupOption(j, Options, Hotkeys, Icons));
				}
				IRenderable renderable;
				if (Context == null)
				{
					renderable = IntroIcon;
				}
				else
				{
					IRenderable renderable2 = Popup.contextRender(Context);
					renderable = renderable2;
				}
				IRenderable renderable3 = renderable;
				List<QudMenuItem> list2 = new List<QudMenuItem>();
				if (!Buttons.IsReadOnlyNullOrEmpty<QudMenuItem>())
				{
					list2.AddRange(Buttons);
				}
				if (AllowEscape)
				{
					list2.AddRange(PopupMessage.CancelButton);
				}
				if (string.IsNullOrEmpty(Title) && Context != null)
				{
					Title = ((Context != null) ? Context.DisplayName : null);
				}
				Popup.WaitNewPopupMessage(Intro, list2, delegate(QudMenuItem item)
				{
					if (item.command == "Cancel")
					{
						SelectedOption = -1;
					}
					else
					{
						string command = item.command;
						if (command != null && command.StartsWith("option:"))
						{
							SelectedOption = Convert.ToInt32(item.command.Substring("option:".Length));
						}
					}
					if (OnResult != null)
					{
						OnResult(SelectedOption);
					}
				}, list, (renderable3 == null) ? Title : null, null, DefaultSelected, (renderable3 == null) ? null : Title, renderable3, null, true, true, PopupLocation, PopupID, false);
				Keyboard.ClearInput(false, true);
				return SelectedOption;
			}
			Popup.ScrapBuffer.Copy(TextConsole.CurrentBuffer);
			Popup.ScrapBuffer2.Copy(TextConsole.CurrentBuffer);
			GameManager.Instance.PushGameView("Popup:Choice", true);
			Keys[] array = null;
			if (!Buttons.IsReadOnlyNullOrEmpty<QudMenuItem>())
			{
				array = new Keys[Buttons.Count];
				for (int k = 0; k < Buttons.Count; k++)
				{
					try
					{
						global::UnityEngine.KeyCode keyCode = Keyboard.ParseUnityEngineKeyCode(Buttons[k].hotkey);
						Keys keys;
						Keyboard.Keymap.TryGetValue(keyCode, out keys);
						array[k] = keys;
					}
					catch
					{
					}
				}
			}
			bool flag = true;
			int num5 = 0;
			int num6 = 0;
			while (flag || (Keyboard.RawCode != Keys.Space && Keyboard.RawCode != Keys.Enter && Keyboard.RawCode != Keys.Enter))
			{
				Popup.ScrapBuffer.Copy(Popup.ScrapBuffer2);
				int num7 = (80 - num4 - 4) / 2;
				int num8 = (25 - num2 - 2) / 2;
				int num9 = num7 + num4 + 4;
				int num10 = num8 + num2 + 3;
				Popup.ScrapBuffer.Fill(num7, num8, num9, num10, 32, ConsoleLib.Console.ColorUtility.MakeColor(TextColor.Black, TextColor.Black), false, false);
				Popup.ScrapBuffer.ThickSingleBox(num7, num8, num9, num10, ConsoleLib.Console.ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
				flag = false;
				int num11 = 0;
				if (IntroIcon != null)
				{
					if (CenterIntroIcon)
					{
						Popup.ScrapBuffer.Goto(num7 + 2 + (num4 - 1) / 2, num8 + num11 + 2);
					}
					else
					{
						Popup.ScrapBuffer.Goto(num7 + 2, num8 + num11 + 2);
					}
					Popup.ScrapBuffer.Write(IntroIcon);
					num11 += 2;
				}
				string[] selectionLines = popupTextBuilder.GetSelectionLines(-1);
				for (int l = 0; l < selectionLines.Length; l++)
				{
					if (CenterIntro)
					{
						Popup.ScrapBuffer.Goto(num7 + 2 + (num4 - ConsoleLib.Console.ColorUtility.LengthExceptFormatting(selectionLines[l])) / 2, num8 + num11 + 2 + l);
					}
					else
					{
						Popup.ScrapBuffer.Goto(num7 + 2, num8 + num11 + 2 + l);
					}
					Popup.ScrapBuffer.Write(selectionLines[l], true, false, false, null, -1);
				}
				if (selectionLines.Length != 0)
				{
					num11 += selectionLines.Length + 1;
				}
				int selectionStart = popupTextBuilder.GetSelectionStart(0);
				int num12 = num;
				while (num11 <= num2 && num12 < Options.Count)
				{
					bool flag2 = SelectedOption == num12;
					int selectionStart2 = popupTextBuilder.GetSelectionStart(num12);
					string[] selectionLines2 = popupTextBuilder.GetSelectionLines(num12);
					int num13 = 0;
					while (num13 < selectionLines2.Length && num11 <= num2)
					{
						bool flag3 = num13 == 0;
						if (flag2 && flag3)
						{
							Popup.ScrapBuffer.Goto(num7 + 2, num8 + 2 + num11);
							Popup.ScrapBuffer.Write("{{Y|>}} ", true, false, false, null, -1);
						}
						if (Hotkeys != null)
						{
							char c = Hotkeys[num12];
							if (flag3 && c != ' ')
							{
								Popup.ScrapBuffer.Goto(num7 + 4, num8 + 2 + num11);
								Popup.ScrapBuffer.Write((flag2 ? "{{W|" : "{{w|") + c.ToString() + "}}) ", true, false, false, null, -1);
							}
						}
						Popup.ScrapBuffer.Goto(num7 + num3 + 4, num8 + 2 + num11);
						Popup.ScrapBuffer.Write(StringFormat.SubstringNotCountingFormat(selectionLines2[num13], 0, num4 - num3 - 2), true, false, false, null, -1);
						if (num13 == 0 && Icons != null && num12 < Icons.Count && Icons[num12] != null)
						{
							if (IconPosition == -1)
							{
								Popup.ScrapBuffer.Goto(num7 + num3 + 2, num8 + 2 + num11);
							}
							else
							{
								Popup.ScrapBuffer.Goto(num7 + num3 + 2 + IconPosition, num8 + 2 + num11);
							}
							Popup.ScrapBuffer.Write(Icons[num12]);
						}
						num5 = selectionStart2 + num13;
						num11++;
						num13++;
					}
					if (num13 == selectionLines2.Length)
					{
						num6 = num12;
					}
					num12++;
				}
				if (popupTextBuilder.Lines.Count > num2)
				{
					if (num5 + 1 < popupTextBuilder.Lines.Count)
					{
						Popup.ScrapBuffer.Goto(num7 + 2, num10);
						Popup.ScrapBuffer.Write("{{W|<More...>}}", true, false, false, null, -1);
					}
					if (num > 0)
					{
						Popup.ScrapBuffer.Goto(num7 + 2, num8);
						Popup.ScrapBuffer.Write("{{W|<More...>}}", true, false, false, null, -1);
					}
					int num14 = num10 - num8 - 2;
					if (num14 > 0)
					{
						ScreenBuffer scrapBuffer = Popup.ScrapBuffer;
						int num15 = num8 + 1;
						int num16 = num9;
						int num17 = num14;
						ScrollbarHelper.Orientation orientation = ScrollbarHelper.Orientation.Vertical;
						int selectionStart3 = popupTextBuilder.GetSelectionStart(num);
						int num18 = num5;
						ScrollbarHelper.Paint(scrapBuffer, num15, num16, num17, orientation, selectionStart, popupTextBuilder.Lines.Count - 1, selectionStart3, num18, null);
					}
				}
				if (!Buttons.IsReadOnlyNullOrEmpty<QudMenuItem>())
				{
					Popup.ScrapBuffer.Goto(num7 + 14, num10);
					foreach (QudMenuItem qudMenuItem in Buttons)
					{
						Popup.ScrapBuffer.Write(qudMenuItem.text, true, false, false, null, -1);
						ScreenBuffer scrapBuffer2 = Popup.ScrapBuffer;
						int num18 = scrapBuffer2.X;
						scrapBuffer2.X = num18 + 1;
					}
				}
				if (!Title.IsNullOrEmpty())
				{
					Popup.ScrapBuffer.Goto(num7 + 2, num8);
					Popup.ScrapBuffer.Write(StringFormat.ClipLine(" " + Title, num4, false, null), true, false, false, null, -1);
				}
				Popup._TextConsole.DrawBuffer(Popup.ScrapBuffer, null, true);
				Keys keys2 = Keyboard.getvk(Options.MapDirectionsToKeypad, false, true);
				if (Icons != null || IntroIcon != null)
				{
					ScreenBuffer.ClearImposterSuppression();
				}
				if (Hotkeys != null)
				{
					for (int m = 0; m < Hotkeys.Count; m++)
					{
						if (Keyboard.Char == (int)Hotkeys[m] && Hotkeys[m] != ' ')
						{
							SelectedOption = m;
							Keyboard.RawCode = Keys.Space;
							break;
						}
					}
				}
				if (array != null)
				{
					for (int n = 0; n < array.Length; n++)
					{
						if (keys2 == array[n] && Buttons[n].command != null && Buttons[n].command.StartsWith("option:"))
						{
							Keyboard.ClearInput(false, true);
							return Convert.ToInt32(Buttons[n].command.Substring("option:".Length));
						}
					}
				}
				if (Buttons != null)
				{
					for (int num19 = 0; num19 < Buttons.Count; num19++)
					{
						if (Buttons[num19].command != null && keys2 == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event.StartsWith("Command:") && Keyboard.CurrentMouseEvent.Event == "Command:" + Buttons[num19].hotkey && Buttons[num19].command.StartsWith("option:"))
						{
							Keyboard.ClearInput(false, true);
							return Convert.ToInt32(Buttons[num19].command.Substring("option:".Length));
						}
					}
				}
				if (keys2 == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event.StartsWith("Choice:"))
				{
					SelectedOption = Convert.ToInt32(Keyboard.CurrentMouseEvent.Event.Split(':', StringSplitOptions.None)[1]);
					break;
				}
				if (AllowEscape && (keys2 == Keys.Escape || Keyboard.vkCode == Keys.Escape))
				{
					Popup._TextConsole.DrawBuffer(Popup.ScrapBuffer2, null, true);
					Keyboard.ClearInput(false, true);
					GameManager.Instance.PopGameView(true);
					return -1;
				}
				if (keys2 == Keys.NumPad2)
				{
					int num18 = SelectedOption;
					SelectedOption = num18 + 1;
				}
				if (keys2 == Keys.NumPad8)
				{
					int num18 = SelectedOption;
					SelectedOption = num18 - 1;
				}
				if (Keyboard.IsCommandKey("Page Down"))
				{
					num = (SelectedOption = num6 + 1);
				}
				if (Keyboard.IsCommandKey("Page Up"))
				{
					int selectionStart4 = popupTextBuilder.GetSelectionStart(num);
					int num20 = num2 - selectionStart;
					while (SelectedOption > 0 && popupTextBuilder.GetSelectionStart(SelectedOption) + num20 > selectionStart4)
					{
						int num18 = SelectedOption;
						SelectedOption = num18 - 1;
					}
					num = SelectedOption;
				}
				SelectedOption = Math.Max(0, Math.Min(Options.Count - 1, SelectedOption));
				if (num > SelectedOption)
				{
					num = SelectedOption;
				}
				if (SelectedOption > num6)
				{
					int num21 = num2 - selectionStart;
					int selectionStart5 = popupTextBuilder.GetSelectionStart(SelectedOption + 1);
					while (num < Options.Count && popupTextBuilder.GetSelectionStart(num) + num21 < selectionStart5)
					{
						num++;
					}
				}
			}
			Popup._TextConsole.DrawBuffer(Popup.ScrapBuffer2, null, true);
			Keyboard.ClearInput(false, true);
			GameManager.Instance.PopGameView(true);
			Loading.SetHideLoadStatus(false);
			if (Keyboard.vkCode == Keys.Escape)
			{
				return -1;
			}
			return SelectedOption;
		}

		// Token: 0x0600D0BE RID: 53438 RVA: 0x004D5B38 File Offset: 0x004D3D38
		public static QudMenuItem GetPopupOption(int Index, IReadOnlyList<string> Options, IReadOnlyList<char> Hotkeys = null, IReadOnlyList<IRenderable> Icons = null)
		{
			if (!CapabilityManager.AllowKeyboardHotkeys)
			{
				Hotkeys = null;
			}
			if (Options == null || Index >= Options.Count)
			{
				throw new ArgumentOutOfRangeException("Index");
			}
			QudMenuItem qudMenuItem = default(QudMenuItem);
			bool flag = !Hotkeys.IsReadOnlyNullOrEmpty<char>();
			char c = ((flag && Index < Hotkeys.Count) ? Hotkeys[Index] : '\0');
			if (c == ' ')
			{
				c = '\0';
			}
			qudMenuItem.icon = ((Icons != null && Index < Icons.Count) ? Icons[Index] : null);
			qudMenuItem.hotkey = ((c != '\0') ? ("char:" + c.ToString()) : "");
			qudMenuItem.command = "option:" + Index.ToString();
			if (!flag)
			{
				qudMenuItem.text = string.Concat(new string[] { "{{y|" + Options[Index] + "}}" });
			}
			else if (c == '\0')
			{
				qudMenuItem.text = string.Concat(new string[] { "    {{y|" + Options[Index] + "}}" });
			}
			else
			{
				qudMenuItem.text = string.Concat(new string[]
				{
					"{{W|[",
					c.ToString(),
					"]}} {{y|",
					Options[Index],
					"}}"
				});
			}
			return qudMenuItem;
		}

		// Token: 0x0600D0BF RID: 53439 RVA: 0x004D5C8C File Offset: 0x004D3E8C
		[return: TupleElementNames(new string[] { "Selected", "Amount" })]
		public static List<ValueTuple<int, int>> PickSeveral(string Title = "", string Intro = null, string SpacingText = "", string Sound = "Sounds/UI/ui_notification", IReadOnlyList<string> Options = null, IReadOnlyList<char> Hotkeys = null, IReadOnlyList<int> Stacks = null, IReadOnlyList<IRenderable> Icons = null, XRL.World.GameObject Context = null, IRenderable IntroIcon = null, Action<int> OnResult = null, int Amount = -1, int Spacing = 0, int MaxWidth = 60, int DefaultSelected = 0, int IconPosition = -1, bool RespectOptionNewlines = false, bool AllowEscape = false, bool CenterIntro = false, bool CenterIntroIcon = true, bool ForceNewPopup = false)
		{
			SoundManager.PlayUISound(Sound, 1f, false, true, SoundRequest.SoundEffectType.None);
			List<ValueTuple<int, int>> list = new List<ValueTuple<int, int>>();
			string[] array = new string[Options.Count];
			QudMenuItem[] array2 = new QudMenuItem[]
			{
				new QudMenuItem
				{
					text = "{{y|[" + ControlManager.getCommandInputFormatted("CmdDelete", true, false) + "] Accept}}",
					command = "option:-2",
					hotkey = "CmdDelete"
				},
				new QudMenuItem
				{
					command = "option:-3",
					hotkey = "Take All"
				}
			};
			for (;;)
			{
				for (int i = 0; i < array.Length; i++)
				{
					int num = 0;
					for (int j = list.Count - 1; j >= 0; j--)
					{
						if (list[j].Item1 == i)
						{
							num = list[j].Item2;
							break;
						}
					}
					string text;
					if (num == 0)
					{
						text = "{{y|[ ]}} ";
					}
					else if (Stacks != null && Stacks[i] > num)
					{
						text = "{{W|[" + num.ToString() + "]}} ";
					}
					else
					{
						text = "{{W|[þ]}} ";
					}
					array[i] = text + Options[i];
				}
				array2[1].text = ((list.Count == array.Length) ? ("{{y|[" + ControlManager.getCommandInputFormatted("Take All", true, false) + "] Deselect All}}") : ("{{y|[" + ControlManager.getCommandInputFormatted("Take All", true, false) + "] Select All}}"));
				int num2 = Popup.PickOption(Title, Intro, SpacingText, null, array, Hotkeys, Icons, array2, Context, IntroIcon, OnResult, Spacing, MaxWidth, DefaultSelected, IconPosition, AllowEscape, RespectOptionNewlines, CenterIntro, CenterIntroIcon, ForceNewPopup, null, null);
				if (num2 == -1)
				{
					break;
				}
				if (num2 == -2)
				{
					if (Amount < 0 || list.Count <= Amount)
					{
						goto IL_0313;
					}
					Popup.Show("You cannot select more than " + Grammar.Cardinal(Amount) + " options!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				else if (num2 == -3)
				{
					if (list.Count != array.Length)
					{
						list.Clear();
						for (int k = 0; k < Options.Count; k++)
						{
							list.Add(new ValueTuple<int, int>(k, (Stacks != null) ? Stacks[k] : 1));
						}
					}
					else
					{
						list.Clear();
					}
				}
				else
				{
					int num3 = -1;
					for (int l = list.Count - 1; l >= 0; l--)
					{
						if (list[l].Item1 == num2)
						{
							num3 = l;
							break;
						}
					}
					if (num3 >= 0)
					{
						list.RemoveAt(num3);
					}
					else if (Stacks != null && Stacks[num2] > 1)
					{
						int valueOrDefault = Popup.AskNumber("Select how many?", null, "", Stacks[num2], 0, Stacks[num2]).GetValueOrDefault();
						if (valueOrDefault != 0)
						{
							list.Add(new ValueTuple<int, int>(num2, valueOrDefault));
						}
					}
					else
					{
						list.Add(new ValueTuple<int, int>(num2, 1));
					}
					DefaultSelected = num2;
				}
			}
			return null;
			IL_0313:
			Keyboard.ClearInput(false, true);
			return list;
		}

		// Token: 0x0600D0C0 RID: 53440 RVA: 0x004D5FB4 File Offset: 0x004D41B4
		private static void SetupColorPickers(string previewContent, bool includeNone, bool includePatterns, bool allowBackground)
		{
			Popup.ColorPickerOptionStrings.Clear();
			Popup.ColorPickerOptions.Clear();
			Popup.ColorPickerKeymap.Clear();
			char c = 'a';
			if (includeNone)
			{
				if (string.IsNullOrWhiteSpace(previewContent))
				{
					Popup.ColorPickerOptionStrings.Add("none");
				}
				else
				{
					Popup.ColorPickerOptionStrings.Add(previewContent + " (no coloring)");
				}
				Popup.ColorPickerOptions.Add("");
				List<char> colorPickerKeymap = Popup.ColorPickerKeymap;
				char c2 = c;
				c = c2 + '\u0001';
				colorPickerKeymap.Add(c2);
			}
			StringBuilder stringBuilder = XRL.World.Event.NewStringBuilder(null);
			StringBuilder stringBuilder2 = XRL.World.Event.NewStringBuilder(null);
			foreach (SolidColor solidColor in MarkupShaders.PickerColors)
			{
				if (!allowBackground && solidColor.Background != null)
				{
					char? background = solidColor.Background;
					int? num = ((background != null) ? new int?((int)background.GetValueOrDefault()) : null);
					int num2 = 107;
					if (!((num.GetValueOrDefault() == num2) & (num != null)))
					{
						continue;
					}
				}
				stringBuilder.Clear();
				stringBuilder2.Clear();
				if (allowBackground)
				{
					solidColor.AssembleCode(stringBuilder2);
					stringBuilder.Append(stringBuilder2);
				}
				else
				{
					stringBuilder2.Append(solidColor.Foreground);
					solidColor.AssembleCode(stringBuilder);
				}
				if (string.IsNullOrWhiteSpace(previewContent))
				{
					stringBuilder.Append(solidColor.GetDisplayName());
				}
				else
				{
					stringBuilder.Append(previewContent).Append("^k&y (").Append(solidColor.GetDisplayName())
						.Append(")");
				}
				Popup.ColorPickerOptionStrings.Add(stringBuilder.ToString());
				Popup.ColorPickerOptions.Add(stringBuilder2.ToString());
				List<char> colorPickerKeymap2 = Popup.ColorPickerKeymap;
				char c3 = c;
				c = c3 + '\u0001';
				colorPickerKeymap2.Add(c3);
			}
			if (includePatterns)
			{
				List<IMarkupShader> list = new List<IMarkupShader>(MarkupShaders.PickerShaders);
				list.Sort((IMarkupShader a, IMarkupShader b) => string.Compare(a.GetDisplayName(), b.GetDisplayName(), StringComparison.CurrentCultureIgnoreCase));
				foreach (IMarkupShader markupShader in list)
				{
					if (string.IsNullOrWhiteSpace(previewContent))
					{
						Popup.ColorPickerOptionStrings.Add(stringBuilder.Clear().Append("{{").Append(markupShader.GetName())
							.Append('|')
							.Append(markupShader.GetDisplayName())
							.Append("}}")
							.ToString());
					}
					else
					{
						Popup.ColorPickerOptionStrings.Add(stringBuilder.Clear().Append("{{").Append(markupShader.GetName())
							.Append('|')
							.Append(previewContent)
							.Append("}} (")
							.Append(markupShader.GetDisplayName())
							.Append(")")
							.ToString());
					}
					Popup.ColorPickerOptions.Add(markupShader.GetName());
					Popup.ColorPickerKeymap.Add(' ');
				}
			}
		}

		// Token: 0x0600D0C1 RID: 53441 RVA: 0x004D62E0 File Offset: 0x004D44E0
		public static string ShowColorPicker(string Title, int Spacing = 0, string Intro = null, int MaxWidth = 60, bool RespectOptionNewlines = false, bool AllowEscape = false, string DefaultSelected = null, string SpacingText = "", bool includeNone = true, bool includePatterns = false, bool allowBackground = false, string previewContent = null)
		{
			Popup.SetupColorPickers(previewContent, includeNone, includePatterns, allowBackground);
			int num = Popup.PickOption(Title, Intro, SpacingText, "Sounds/UI/ui_notification", Popup.ColorPickerOptionStrings.ToArray(), Popup.ColorPickerKeymap.ToArray(), null, null, null, null, null, Spacing, MaxWidth, DefaultSelected.IsNullOrEmpty() ? 0 : Math.Max(Popup.ColorPickerOptions.IndexOf(DefaultSelected), 0), -1, AllowEscape, RespectOptionNewlines, false, true, false, null, null);
			if (num < 0)
			{
				return null;
			}
			Keyboard.ClearInput(false, true);
			return Popup.ColorPickerOptions[num];
		}

		// Token: 0x0600D0C2 RID: 53442 RVA: 0x004D6364 File Offset: 0x004D4564
		public static async Task<string> ShowColorPickerAsync(string Title, int Spacing = 0, string Intro = null, int MaxWidth = 60, bool RespectOptionNewlines = false, bool AllowEscape = false, string DefaultSelected = null, string SpacingText = "", bool includeNone = true, bool includePatterns = false, bool allowBackground = false, string previewContent = null)
		{
			Popup.SetupColorPickers(previewContent, includeNone, includePatterns, allowBackground);
			int num = await Popup.PickOptionAsync(Title, Intro, SpacingText, Popup.ColorPickerOptionStrings.ToArray(), Popup.ColorPickerKeymap.ToArray(), null, null, null, Spacing, MaxWidth, DefaultSelected.IsNullOrEmpty() ? 0 : Math.Max(Popup.ColorPickerOptions.IndexOf(DefaultSelected), 0), -1, RespectOptionNewlines, AllowEscape, false, true);
			string text;
			if (num < 0)
			{
				text = null;
			}
			else
			{
				Keyboard.ClearInput(false, true);
				text = Popup.ColorPickerOptions[num];
			}
			return text;
		}

		// Token: 0x0600D0C3 RID: 53443 RVA: 0x004D6407 File Offset: 0x004D4607
		public static DialogResult WarnYesNo(string Message, string Sound = "Sounds/UI/ui_notification_warning", bool AllowEscape = true, DialogResult Default = DialogResult.Cancel)
		{
			return Popup.ShowYesNo(Message, Sound, AllowEscape, Default, null);
		}

		// Token: 0x0600D0C4 RID: 53444 RVA: 0x004D6414 File Offset: 0x004D4614
		public static DialogResult ShowYesNo(string Message, string Sound = "Sounds/UI/ui_notification", bool AllowEscape = true, DialogResult defaultResult = DialogResult.Yes, Action<DialogResult> callback = null)
		{
			SoundManager.PlayUISound(Sound, 1f, false, true, SoundRequest.SoundEffectType.None);
			Message = Markup.Transform(Options.OverlayUI ? ("{{white|" + Message + "}}") : Message, false);
			Keyboard.ClearMouseEvents(false);
			int num = 0;
			DialogResult result = defaultResult;
			if (UIManager.UseNewPopups)
			{
				Popup.WaitNewPopupMessage(Message, PopupMessage.YesNoButton, delegate(QudMenuItem i)
				{
					if (i.command == "No")
					{
						result = DialogResult.No;
					}
					if (i.command == "Yes")
					{
						result = DialogResult.Yes;
					}
					if (i.command == "Cancel")
					{
						result = DialogResult.No;
					}
					Action<DialogResult> callback2 = callback;
					if (callback2 == null)
					{
						return;
					}
					callback2(result);
				}, null, null, null, 0, null, null, null, true, true, null, null, false);
				return result;
			}
			Popup.ScrapBuffer.Copy(TextConsole.CurrentBuffer);
			Popup.ScrapBuffer2.Copy(TextConsole.CurrentBuffer);
			new TextBlock(Message, 80, 5000, false, 0);
			GameManager.Instance.PushGameView("Popup:MessageBox", true);
			Keyboard.ClearInput(false, true);
			Keys keys = Keys.Space;
			bool flag = true;
			while (flag || ((Keyboard.vkCode != Keys.MouseEvent || !Keyboard.CurrentMouseEvent.Event.StartsWith("LeftOption:")) && Keyboard.RawCode != Keys.Y && Keyboard.RawCode != Keys.N && (Keyboard.vkCode != Keys.Escape || !AllowEscape) && Keyboard.vkCode != Keys.Space && Keyboard.vkCode != Keys.Enter))
			{
				flag = false;
				int num2 = Popup.RenderBlock(Message, "[Yes] [No]", "[{{W|Y}}es] [{{W|N}}o]", Popup.ScrapBuffer, num, -1, -1, null, true, null, true, false);
				Popup._TextConsole.DrawBuffer(Popup.ScrapBuffer, null, true);
				keys = Keyboard.getvk(Options.MapDirectionsToKeypad, false, true);
				if (keys == Keys.NumPad2 && num2 - num > 20)
				{
					num++;
				}
				if (keys == Keys.NumPad8 && num > 0)
				{
					num--;
				}
				if (keys == Keys.Next)
				{
					num += 23;
				}
				if (keys == Keys.Prior || keys == Keys.Next)
				{
					num -= 23;
				}
				if (num < 0)
				{
					num = 0;
				}
			}
			Popup._TextConsole.DrawBuffer(Popup.ScrapBuffer2, null, true);
			if (Keyboard.RawCode == Keys.Y || (Keyboard.vkCode == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "LeftOption:1"))
			{
				result = DialogResult.Yes;
			}
			if (Keyboard.RawCode == Keys.N || (keys == Keys.Escape && AllowEscape) || (Keyboard.vkCode == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "LeftOption:2"))
			{
				result = DialogResult.No;
			}
			Keyboard.ClearInput(false, true);
			GameManager.Instance.PopGameView(true);
			return result;
		}

		// Token: 0x0600D0C5 RID: 53445 RVA: 0x004D6660 File Offset: 0x004D4860
		public static async Task<DialogResult> ShowYesNoAsync(string Message)
		{
			DialogResult result = DialogResult.Cancel;
			QudMenuItem qudMenuItem = await Popup.NewPopupMessageAsync(Message, PopupMessage.YesNoButton, null, null, null, 0, null, null, null, true, true, false, default(CancellationToken), false, "", null, null, null);
			if (qudMenuItem.command == "No")
			{
				result = DialogResult.No;
			}
			if (qudMenuItem.command == "Yes")
			{
				result = DialogResult.Yes;
			}
			return result;
		}

		// Token: 0x0600D0C6 RID: 53446 RVA: 0x004D66A4 File Offset: 0x004D48A4
		public static async Task<DialogResult> ShowYesNoCancelAsync(string Message)
		{
			DialogResult result = DialogResult.Cancel;
			QudMenuItem qudMenuItem = await Popup.NewPopupMessageAsync(Message, PopupMessage.YesNoCancelButton, null, null, null, 0, null, null, null, true, true, false, default(CancellationToken), false, "", null, null, null);
			if (qudMenuItem.command == "No")
			{
				result = DialogResult.No;
			}
			if (qudMenuItem.command == "Yes")
			{
				result = DialogResult.Yes;
			}
			if (qudMenuItem.command == "Cancel")
			{
				result = DialogResult.Cancel;
			}
			return result;
		}

		// Token: 0x0600D0C7 RID: 53447 RVA: 0x004D66E7 File Offset: 0x004D48E7
		public static DialogResult WarnYesNoCancel(string Message, string Sound = "Sounds/UI/ui_notification_warning", bool AllowEscape = true, DialogResult Default = DialogResult.Cancel)
		{
			return Popup.ShowYesNoCancel(Message, Sound, AllowEscape, Default);
		}

		// Token: 0x0600D0C8 RID: 53448 RVA: 0x004D66F4 File Offset: 0x004D48F4
		public static DialogResult ShowYesNoCancel(string Message, string Sound = "Sounds/UI/ui_notification", bool AllowEscape = true, DialogResult defaultResult = DialogResult.Cancel)
		{
			SoundManager.PlayUISound(Sound, 1f, false, true, SoundRequest.SoundEffectType.None);
			Message = Markup.Transform(Options.OverlayUI ? ("{{white|" + Message + "}}") : Message, false);
			int num = 0;
			DialogResult result = defaultResult;
			if (UIManager.UseNewPopups)
			{
				Popup.WaitNewPopupMessage(Message, PopupMessage.YesNoCancelButton, delegate(QudMenuItem i)
				{
					if (i.command == "No")
					{
						result = DialogResult.No;
					}
					if (i.command == "Yes")
					{
						result = DialogResult.Yes;
					}
					if (i.command == "Cancel")
					{
						result = DialogResult.Cancel;
					}
				}, null, null, null, 0, null, null, null, true, true, null, null, false);
				return result;
			}
			Popup.ScrapBuffer.Copy(TextConsole.CurrentBuffer);
			Popup.ScrapBuffer2.Copy(TextConsole.CurrentBuffer);
			new TextBlock(Message, 80, 5000, false, 0);
			GameManager.Instance.PushGameView("Popup:MessageBox", true);
			Keyboard.ClearInput(false, true);
			while (Keyboard.RawCode != Keys.Y && Keyboard.RawCode != Keys.Enter && Keyboard.RawCode != Keys.N && (Keyboard.vkCode != Keys.MouseEvent || !Keyboard.CurrentMouseEvent.Event.StartsWith("LeftOption:")))
			{
				GameManager.Instance.ClearRegions();
				string text;
				string text2;
				if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
				{
					text = string.Concat(new string[]
					{
						"[",
						ControlManager.getCommandInputFormatted("Accept", true, false),
						"-Yes] [",
						ControlManager.getCommandInputFormatted("V Negative", true, false),
						"-No] [",
						ControlManager.getCommandInputFormatted("Cancel", true, false),
						"-Cancel]"
					});
					text2 = ConsoleLib.Console.ColorUtility.StripFormatting(text);
				}
				else
				{
					text2 = "[Yes] [No] [ESC-Cancel]";
					text = "[{{W|Y}}es] [{{W|N}}o] [{{W|ESC}}-Cancel]";
				}
				int num2 = Popup.RenderBlock(Message, text2, text, Popup.ScrapBuffer, num, -1, -1, null, true, null, true, false);
				Popup._TextConsole.DrawBuffer(Popup.ScrapBuffer, null, true);
				Keys keys = Keyboard.getvk(Options.MapDirectionsToKeypad, false, true);
				if (keys == Keys.Space)
				{
					Keyboard.RawCode = Keys.Y;
				}
				if (keys == Keys.Escape)
				{
					Keyboard.RawCode = Keys.Escape;
				}
				if (keys == Keys.MouseEvent)
				{
					if (Keyboard.CurrentMouseEvent.Event == "Command:Accept")
					{
						Keyboard.RawCode = Keys.Y;
					}
					if (Keyboard.CurrentMouseEvent.Event == "Command:V Negative")
					{
						Keyboard.RawCode = Keys.N;
					}
					if (Keyboard.CurrentMouseEvent.Event == "Command:Cancel")
					{
						Keyboard.RawCode = Keys.Escape;
					}
				}
				if (keys == Keys.NumPad2 && num2 - num > 20)
				{
					num++;
				}
				if (keys == Keys.NumPad8 && num > 0)
				{
					num--;
				}
				if (keys == Keys.Next)
				{
					num += 23;
				}
				if (keys == Keys.Prior || keys == Keys.Next)
				{
					num -= 23;
				}
				if ((keys == Keys.Escape || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "RightClick")) && AllowEscape)
				{
					break;
				}
				if (num < 0)
				{
					num = 0;
				}
			}
			Popup._TextConsole.DrawBuffer(Popup.ScrapBuffer2, null, true);
			if (Keyboard.RawCode == Keys.Y || Keyboard.RawCode == Keys.Enter || (Keyboard.vkCode == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "LeftOption:1"))
			{
				result = DialogResult.Yes;
			}
			if (Keyboard.RawCode == Keys.N || (Keyboard.vkCode == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "LeftOption:2"))
			{
				result = DialogResult.No;
			}
			Keyboard.ClearInput(false, true);
			GameManager.Instance.PopGameView(true);
			return result;
		}

		// Token: 0x0600D0C9 RID: 53449 RVA: 0x004D6A34 File Offset: 0x004D4C34
		public static void DisplayLoadError(SerializationReader Reader, string Loadable, int Errors = 1)
		{
			bool flag = Errors == 1;
			StringBuilder stringBuilder = XRL.World.Event.NewStringBuilder(null);
			stringBuilder.Append("There").Compound(flag ? "was" : "were").Compound(Errors)
				.Compound(flag ? "error" : "errors")
				.Compound("while loading this")
				.Compound(Loadable)
				.Append('.');
			if (Popup.DisplayedLoadError)
			{
				MessageQueue.AddPlayerMessage(XRL.World.Event.FinalizeString(stringBuilder), null, true);
			}
			bool flag2 = false;
			foreach (string text in Reader.GetSavedMods().Except<string>(ModManager.GetRunningMods()))
			{
				if (flag2)
				{
					stringBuilder.Append(", ");
				}
				else
				{
					flag2 = true;
					stringBuilder.Append("\n\nMissing mods: ");
				}
				stringBuilder.Append(ModManager.GetModTitle(text));
			}
			if (flag2)
			{
				stringBuilder.Append('.');
			}
			Popup.DisplayedLoadError = true;
			stringBuilder.Append("\n\nDo you want to examine these errors in the Player.log?");
			if (Popup.ShowYesNoCancel(XRL.World.Event.FinalizeString(stringBuilder), "Sounds/UI/ui_notification", true, DialogResult.Cancel) == DialogResult.Yes)
			{
				DataManager.Open(Path.Join(XRLCore.SavePath, "Player.log"));
			}
		}

		// Token: 0x04005241 RID: 21057
		public const string DEFAULT_SOUND = "Sounds/UI/ui_notification";

		// Token: 0x04005242 RID: 21058
		public const string PROMPT_SOUND = "Sounds/UI/ui_notification_question";

		// Token: 0x04005243 RID: 21059
		public const string WARN_SOUND = "Sounds/UI/ui_notification_warning";

		// Token: 0x04005244 RID: 21060
		public static TextConsole _TextConsole;

		// Token: 0x04005245 RID: 21061
		public static ScreenBuffer _ScreenBuffer;

		// Token: 0x04005246 RID: 21062
		[Obsolete("We don't use it. You shouldn't either.")]
		public static Thread ThinkingThread = null;

		// Token: 0x04005247 RID: 21063
		[Obsolete("We don't use it. You shouldn't either.  See XRL.UI.Loading.SetLoadStatus(string) or LoadTask(string, Action)")]
		public static string ThinkingString = "";

		// Token: 0x04005248 RID: 21064
		[Obsolete("We don't use it. You shouldn't either.  See XRL.UI.Loading.SetHideLoad(bool).")]
		public static bool DisableThinking = false;

		// Token: 0x04005249 RID: 21065
		[Obsolete("Not used.")]
		public static string LastThinking = "";

		// Token: 0x0400524A RID: 21066
		[Obsolete("Not used.")]
		public static bool bPaused = false;

		// Token: 0x0400524B RID: 21067
		[NonSerialized]
		private static List<string> RenderDirectLines = new List<string>();

		// Token: 0x0400524C RID: 21068
		public static TextBlock _RenderBlock = new TextBlock("", 78, 5000, false, 0);

		// Token: 0x0400524D RID: 21069
		public static bool Suppress = false;

		// Token: 0x0400524E RID: 21070
		[NonSerialized]
		private static List<string> ColorPickerOptionStrings = new List<string>(32);

		// Token: 0x0400524F RID: 21071
		[NonSerialized]
		private static List<string> ColorPickerOptions = new List<string>(32);

		// Token: 0x04005250 RID: 21072
		[NonSerialized]
		private static List<char> ColorPickerKeymap = new List<char>(32);

		// Token: 0x04005251 RID: 21073
		[NonSerialized]
		public static ScreenBuffer ScrapBuffer = ScreenBuffer.create(80, 25);

		// Token: 0x04005252 RID: 21074
		[NonSerialized]
		public static ScreenBuffer ScrapBuffer2 = ScreenBuffer.create(80, 25);

		// Token: 0x04005253 RID: 21075
		public static bool DisplayedLoadError = false;

		// Token: 0x04005254 RID: 21076
		public static string SPACING_DARK_LINE = "{{K|================================================================================}}";

		// Token: 0x04005255 RID: 21077
		public static string SPACING_BRIGHT_LINE = "{{Y|================================================================================}}";

		// Token: 0x04005256 RID: 21078
		public static string SPACING_GREY_RAINBOW_LINE = "{{K-y-Y-y sequence|================================================================================}}";

		// Token: 0x02001DD6 RID: 7638
		protected class PopupTextBuilder
		{
			// Token: 0x0600F579 RID: 62841 RVA: 0x00555EA4 File Offset: 0x005540A4
			public void AddSection(string text, int selection)
			{
				Popup.PopupTextBuilder.Section section = new Popup.PopupTextBuilder.Section();
				section.Selection = selection;
				section.StartOffset = this.Lines.Count;
				int num;
				this.Lines.AddRange(StringFormat.ClipTextToArray(text, this.MaxWidth - this.Padding, out num, this.RespectNewlines, true, false, true));
				this.MaxClippedWidth = Math.Max(this.MaxClippedWidth, num + this.Padding);
				section.EndOffset = this.Lines.Count - 1;
				this.Sections.Add(section);
			}

			// Token: 0x0600F57A RID: 62842 RVA: 0x00555F30 File Offset: 0x00554130
			public void AddSpacing(int lines, string text = null)
			{
				for (int i = 0; i < lines; i++)
				{
					this.Lines.Add(Markup.Transform(text ?? this.SpacingText, false));
				}
			}

			// Token: 0x0600F57B RID: 62843 RVA: 0x00555F6C File Offset: 0x0055416C
			public Popup.PopupTextBuilder.Section GetSection(int selection)
			{
				foreach (Popup.PopupTextBuilder.Section section in this.Sections)
				{
					if (section.Selection == selection)
					{
						return section;
					}
				}
				return null;
			}

			// Token: 0x0600F57C RID: 62844 RVA: 0x00555FC8 File Offset: 0x005541C8
			public string[] GetSelectionLines(int selection)
			{
				Popup.PopupTextBuilder.Section section = this.GetSection(selection);
				if (section == null)
				{
					return new string[0];
				}
				int num = section.Count + ((selection >= 0 && selection != this.Sections[this.Sections.Count - 1].Selection) ? this.Spacing : 0);
				return this.Lines.GetRange(section.StartOffset, num).ToArray();
			}

			// Token: 0x0600F57D RID: 62845 RVA: 0x00556034 File Offset: 0x00554234
			public int GetSelectionStart(int selection)
			{
				Popup.PopupTextBuilder.Section section = this.GetSection(selection);
				if (section == null)
				{
					return this.Lines.Count;
				}
				return section.StartOffset;
			}

			// Token: 0x04007436 RID: 29750
			public const int IntroSelection = -1;

			// Token: 0x04007437 RID: 29751
			public List<string> Lines = new List<string>(24);

			// Token: 0x04007438 RID: 29752
			public int MaxClippedWidth;

			// Token: 0x04007439 RID: 29753
			public int MaxWidth = 72;

			// Token: 0x0400743A RID: 29754
			public int Padding;

			// Token: 0x0400743B RID: 29755
			public bool RespectNewlines = true;

			// Token: 0x0400743C RID: 29756
			public int Spacing;

			// Token: 0x0400743D RID: 29757
			public string SpacingText = "";

			// Token: 0x0400743E RID: 29758
			public List<Popup.PopupTextBuilder.Section> Sections = new List<Popup.PopupTextBuilder.Section>();

			// Token: 0x02001E99 RID: 7833
			public class Section
			{
				// Token: 0x17001058 RID: 4184
				// (get) Token: 0x0600F7F5 RID: 63477 RVA: 0x0055D7D8 File Offset: 0x0055B9D8
				public int Count
				{
					get
					{
						return 1 + this.EndOffset - this.StartOffset;
					}
				}

				// Token: 0x040076CF RID: 30415
				public int Selection;

				// Token: 0x040076D0 RID: 30416
				public int StartOffset;

				// Token: 0x040076D1 RID: 30417
				public int EndOffset;
			}
		}
	}
}
