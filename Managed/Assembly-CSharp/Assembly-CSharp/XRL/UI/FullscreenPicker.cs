using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using XRL.Rules;
using XRL.World;

namespace XRL.UI
{
	// Token: 0x02001480 RID: 5248
	public class FullscreenPicker
	{
		// Token: 0x0600CE94 RID: 52884 RVA: 0x004B2390 File Offset: 0x004B0590
		public FullscreenPicker(ScreenBuffer SB, TextConsole C, List<FullscreenPickerEntry> Entries)
		{
			this.SB = SB;
			this.Entries = Entries;
			this.C = C;
		}

		// Token: 0x0600CE95 RID: 52885 RVA: 0x004B23B0 File Offset: 0x004B05B0
		public int DoInput()
		{
			Keys keys = Keyboard.getvk(true, false, true);
			if (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event.StartsWith("Select:"))
			{
				this.nSelected = Convert.ToInt32(Keyboard.CurrentMouseEvent.Event.Split(':', StringSplitOptions.None)[1]);
				return this.nSelected;
			}
			if (keys == Keys.F1 || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Random"))
			{
				this.nSelected = Stat.Random(0, this.Entries.Count - 1);
			}
			if (keys == Keys.Enter)
			{
				keys = Keys.Space;
			}
			if (keys == Keys.Escape || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "RightClick"))
			{
				return -1;
			}
			if (keys >= Keys.A && keys <= Keys.Z)
			{
				int num = keys - Keys.A;
				if (num <= this.Entries.Count - 1)
				{
					return num;
				}
			}
			if (keys == Keys.Space)
			{
				return this.nSelected;
			}
			if (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event.StartsWith("Pick:"))
			{
				return int.Parse(Keyboard.CurrentMouseEvent.Event.Split(':', StringSplitOptions.None)[1]);
			}
			if (keys == Keys.NumPad8 && this.nSelected > 0)
			{
				this.nSelected--;
			}
			if (keys == Keys.NumPad2 && this.nSelected < this.Entries.Count - 1)
			{
				this.nSelected++;
			}
			return -2;
		}

		// Token: 0x0600CE96 RID: 52886 RVA: 0x004B2520 File Offset: 0x004B0720
		public void RenderFrame()
		{
			Event.ResetPool(true);
			GameManager.Instance.ClearRegions();
			this.SB.Clear();
			this.SB.SingleBox(0, 0, 79, 24, ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
			this.SB.Goto(20, 0);
			this.SB.Write("[ &WCharacter Creation &y- &YChoose Genotype&y ]", true, false, false, null, -1);
			this.SB.Goto(60, 24);
			this.SB.Write(" &WESC&y to go back ", true, false, false, null, -1);
			int num = 2;
			for (int i = 0; i < this.Entries.Count; i++)
			{
				int num2 = num;
				if (this.nSelected == i)
				{
					this.SB.Goto(1, num);
					this.SB.Write("&Y> ", true, false, false, null, -1);
				}
				this.SB.Goto(3, num);
				this.SB.Write("&W" + ((char)(65 + i)).ToString() + "&y) " + this.Entries[i].Title, true, false, false, null, -1);
				num++;
				for (int j = 0; j < this.Entries[i].Lines.Count; j++)
				{
					this.SB.Goto(3, num);
					this.SB.Write(this.Entries[i].Lines[j], true, false, false, null, -1);
					num++;
				}
				GameManager.Instance.AddRegion(3, num2, 40, num, "Pick:" + i.ToString(), null, "Select:" + i.ToString());
				num++;
			}
			this.SB.Goto(3, 24);
			this.SB.Write(" press &WF1&y for a random selection ", true, false, false, null, -1);
			GameManager.Instance.AddRegion(3, 24, 40, 24, "Random", null, null);
			this.C.DrawBuffer(this.SB, null, false);
		}

		// Token: 0x0400502A RID: 20522
		public const int RESULT_CANCEL = -1;

		// Token: 0x0400502B RID: 20523
		public const int RESULT_NOOP = -2;

		// Token: 0x0400502C RID: 20524
		private int nSelected;

		// Token: 0x0400502D RID: 20525
		private TextConsole C;

		// Token: 0x0400502E RID: 20526
		private ScreenBuffer SB;

		// Token: 0x0400502F RID: 20527
		private List<FullscreenPickerEntry> Entries;
	}
}
