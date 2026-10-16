using System;

namespace ConsoleLib.Console
{
	// Token: 0x02001545 RID: 5445
	public class ScrollbarHelper
	{
		// Token: 0x0600D47B RID: 54395 RVA: 0x004EFE08 File Offset: 0x004EE008
		public static void Paint(ScreenBuffer buffer, int top, int left, int size, ScrollbarHelper.Orientation orient = ScrollbarHelper.Orientation.Vertical, int handleMin = 0, int handleMax = -999, int handleStart = 0, int handleEnd = 10, ScrollbarHelper.ScrollbarVisual visual = null)
		{
			visual = visual ?? ScrollbarHelper.DefaultVisual;
			if (handleMax == -999)
			{
				handleMax = size;
			}
			if (handleMax == 0)
			{
				handleMax = -1;
			}
			int num = (int)Math.Floor((double)((float)size * (float)(handleStart - handleMin) / (float)(handleMax - handleMin)));
			int num2 = (int)Math.Floor((double)((float)size * (float)(handleEnd - handleMin) / (float)(handleMax - handleMin)));
			num2 = Math.Min(num2, size);
			if (orient == ScrollbarHelper.Orientation.Vertical)
			{
				buffer.Fill(left, top, left, top + size, visual.BackgroundChar, visual.BackgroundColor, false, false);
				if (handleMax > 0)
				{
					buffer.Fill(left, top + num, left, top + num2, visual.ActiveChar, visual.ActiveColor, false, false);
					return;
				}
			}
			else if (orient == ScrollbarHelper.Orientation.Horizontal)
			{
				buffer.Fill(left, top, left + size, top, visual.BackgroundChar, visual.BackgroundColor, false, false);
				if (handleMax > 0)
				{
					buffer.Fill(left + num, top, left + num2, top, visual.ActiveChar, visual.ActiveColor, false, false);
				}
			}
		}

		// Token: 0x04005526 RID: 21798
		public const int SameAsSize = -999;

		// Token: 0x04005527 RID: 21799
		public static ScrollbarHelper.ScrollbarVisual DefaultVisual = new ScrollbarHelper.ScrollbarVisual();

		// Token: 0x02001E53 RID: 7763
		public enum Orientation
		{
			// Token: 0x0400762F RID: 30255
			Horizontal = 1,
			// Token: 0x04007630 RID: 30256
			Vertical
		}

		// Token: 0x02001E54 RID: 7764
		public class ScrollbarVisual
		{
			// Token: 0x04007631 RID: 30257
			public ushort BackgroundChar = 177;

			// Token: 0x04007632 RID: 30258
			public ushort ActiveChar = 219;

			// Token: 0x04007633 RID: 30259
			public ushort BackgroundColor = ColorUtility.MakeColor(ColorUtility.Bright(TextColor.Black), TextColor.Black);

			// Token: 0x04007634 RID: 30260
			public ushort ActiveColor = ColorUtility.MakeColor(ColorUtility.Bright(TextColor.Grey), TextColor.Black);
		}
	}
}
