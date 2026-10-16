using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using Qud.UI;
using XRL.Rules;
using XRL.World;

namespace XRL.UI
{
	// Token: 0x020014B4 RID: 5300
	[UIView("WorldCreationProgress", false, true, false, "Menu", null, false, 0, false)]
	public class WorldCreationProgress : IWantsTextConsoleInit
	{
		// Token: 0x0600D020 RID: 53280 RVA: 0x004CC0D0 File Offset: 0x004CA2D0
		public static void DrawProgressBar(ScreenBuffer Buffer, int xp, int yp, int Length, int Percentage)
		{
			Buffer.Goto(xp, yp);
			int num = (int)((float)Length * ((float)Percentage / 100f));
			for (int i = 0; i < num; i++)
			{
				Buffer.Write("&g^y±", true, false, false, null, -1);
			}
			if (Percentage >= 100)
			{
				Buffer.Write("&g^y±", true, false, false, null, -1);
				return;
			}
			Buffer.Write("&b^g°", true, false, false, null, -1);
			for (int j = 0; j < Length - num; j++)
			{
				Buffer.Write("&K°", true, false, false, null, -1);
			}
		}

		// Token: 0x0600D021 RID: 53281 RVA: 0x004CC15C File Offset: 0x004CA35C
		public static void Begin(int TotalSteps)
		{
			WorldCreationProgress.Steps = new List<StepEntry>();
			WorldCreationProgress.CurrentStep = -1;
			WorldCreationProgress.MaxSteps = TotalSteps;
			WorldCreationProgress.Page = BookUI.Books["Quotes"][Stat.Random(0, BookUI.Books["Quotes"].Count - 1)];
		}

		// Token: 0x0600D022 RID: 53282 RVA: 0x004CC1B4 File Offset: 0x004CA3B4
		public static void Draw(bool Last = false)
		{
			WorldCreationProgress._ScreenBuffer.Clear();
			WorldCreationProgress._ScreenBuffer.SingleBox(0, 0, 79, 24, ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
			WorldCreationProgress._ScreenBuffer.Goto(31, 0);
			WorldCreationProgress._ScreenBuffer.Write("[ Creating World ]", true, false, false, null, -1);
			for (int i = 0; i < WorldCreationProgress.Steps.Count; i++)
			{
				int num = 5;
				int num2 = 2 + i * 3;
				WorldCreationProgress._ScreenBuffer.Goto(num, num2);
				WorldCreationProgress._ScreenBuffer.Write(WorldCreationProgress.Steps[i].Text, true, false, false, null, -1);
				if (WorldCreationProgress.Steps[i].CurrentStep >= WorldCreationProgress.Steps[i].MaxSteps)
				{
					WorldCreationProgress._ScreenBuffer.Write(" : &GComplete!", true, false, false, null, -1);
				}
				else if (WorldCreationProgress.Steps[i].StepText != null && WorldCreationProgress.Steps[i].StepText != "")
				{
					WorldCreationProgress._ScreenBuffer.Write(" : " + WorldCreationProgress.Steps[i].StepText, true, false, false, null, -1);
				}
				WorldCreationProgress.DrawProgressBar(WorldCreationProgress._ScreenBuffer, num, num2 + 1, 68, WorldCreationProgress.Steps[i].CurrentStep * 100 / WorldCreationProgress.Steps[i].MaxSteps);
			}
			WorldCreationProgress._ScreenBuffer.Goto(3, 22);
			WorldCreationProgress.DrawProgressBar(WorldCreationProgress._ScreenBuffer, 3, 24, 74, Last ? 100 : (WorldCreationProgress.Steps.Count * 100 / WorldCreationProgress.MaxSteps));
			if (WorldCreationProgress.Page != null)
			{
				int num3 = 22 - WorldCreationProgress.Page.Lines.Count;
				foreach (string text in WorldCreationProgress.Page.Lines)
				{
					WorldCreationProgress._ScreenBuffer.Goto(5, num3);
					WorldCreationProgress._ScreenBuffer.Write("&y" + text, true, false, false, null, -1);
					num3++;
				}
			}
			WorldCreationProgress._TextConsole.DrawBuffer(WorldCreationProgress._ScreenBuffer, null, false);
		}

		// Token: 0x0600D023 RID: 53283 RVA: 0x004CC3F0 File Offset: 0x004CA5F0
		public static void NextStep(string Text, int TotalSteps)
		{
			if (GameManager.IsOnGameContext())
			{
				Event.ResetPool(true);
			}
			WorldGenerationScreen.AddMessage(Text);
			if (WorldCreationProgress.CurrentStep > -1)
			{
				WorldCreationProgress.Steps[WorldCreationProgress.CurrentStep].CurrentStep = WorldCreationProgress.Steps[WorldCreationProgress.CurrentStep].MaxSteps;
			}
			StepEntry stepEntry = new StepEntry();
			stepEntry.CurrentStep = 0;
			stepEntry.MaxSteps = TotalSteps;
			stepEntry.StepText = "";
			stepEntry.Text = Text;
			WorldCreationProgress.Steps.Add(stepEntry);
			WorldCreationProgress.CurrentStep++;
			WorldCreationProgress.Draw(false);
		}

		// Token: 0x0600D024 RID: 53284 RVA: 0x004CC484 File Offset: 0x004CA684
		public static void StepProgress(string StepText, bool Last = false)
		{
			WorldGenerationScreen.IncrementProgress();
			WorldGenerationScreen.AddMessage(StepText);
			WorldCreationProgress.Steps[WorldCreationProgress.CurrentStep].CurrentStep++;
			WorldCreationProgress.Steps[WorldCreationProgress.CurrentStep].StepText = StepText;
			WorldCreationProgress.Draw(Last);
		}

		// Token: 0x0600D025 RID: 53285 RVA: 0x004CC4D3 File Offset: 0x004CA6D3
		public void Init(TextConsole TextConsole_, ScreenBuffer ScreenBuffer_)
		{
			WorldCreationProgress._TextConsole = TextConsole_;
			WorldCreationProgress._ScreenBuffer = ScreenBuffer_;
		}

		// Token: 0x0400519C RID: 20892
		public static TextConsole _TextConsole;

		// Token: 0x0400519D RID: 20893
		public static ScreenBuffer _ScreenBuffer;

		// Token: 0x0400519E RID: 20894
		public static List<StepEntry> Steps;

		// Token: 0x0400519F RID: 20895
		public static int CurrentStep = -1;

		// Token: 0x040051A0 RID: 20896
		public static int MaxSteps = 0;

		// Token: 0x040051A1 RID: 20897
		public static BookPage Page = null;
	}
}
