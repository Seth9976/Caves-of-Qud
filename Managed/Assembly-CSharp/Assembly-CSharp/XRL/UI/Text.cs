using System;
using System.Collections.Generic;
using System.Text;
using ConsoleLib.Console;

namespace XRL.UI
{
	// Token: 0x020014D5 RID: 5333
	public class Text
	{
		// Token: 0x0600D0E4 RID: 53476 RVA: 0x004D7A18 File Offset: 0x004D5C18
		public static void Draw(ScreenBuffer _ScreenBuffer, string str, int x1, int y1, int x2, int y2)
		{
			int num = x2 - x1 + 1;
			int num2 = y2 - y1 + 1;
			List<string> lines = new TextBlock(str, num, num2, false, 0).Lines;
			int num3 = y1;
			while (num3 <= y2 && num3 - y1 < lines.Count)
			{
				_ScreenBuffer.Goto(x1, num3);
				_ScreenBuffer.Write(lines[num3 - y1], true, false, false, null, -1);
				num3++;
			}
		}

		// Token: 0x0600D0E5 RID: 53477 RVA: 0x004D7A7C File Offset: 0x004D5C7C
		public static List<string> DrawBottomToTop(ScreenBuffer _ScreenBuffer, StringBuilder str, int x1, int y1, int x2, int y2)
		{
			int num = x2 - x1 + 1;
			int num2 = y2 - y1 + 1;
			List<string> lines = new TextBlock(str, num, num2, true, 0).Lines;
			int num3 = 0;
			int num4 = y2;
			while (num4 >= y1 && num3 < lines.Count)
			{
				_ScreenBuffer.Goto(x1, num4);
				_ScreenBuffer.Write(lines[num3], true, false, false, null, -1);
				num4--;
				num3++;
			}
			return lines;
		}

		// Token: 0x0600D0E6 RID: 53478 RVA: 0x004D7AE8 File Offset: 0x004D5CE8
		public static List<string> DrawBottomToTop(ScreenBuffer _ScreenBuffer, ReadOnlySpan<char> str, int x1, int y1, int x2, int y2)
		{
			int num = x2 - x1 + 1;
			int num2 = y2 - y1 + 1;
			List<string> lines = new TextBlock(str, num, num2, true, 0).Lines;
			int num3 = 0;
			int num4 = y2;
			while (num4 >= y1 && num3 < lines.Count)
			{
				_ScreenBuffer.Goto(x1, num4);
				_ScreenBuffer.Write(lines[num3], true, false, false, null, -1);
				num4--;
				num3++;
			}
			return lines;
		}

		// Token: 0x0600D0E7 RID: 53479 RVA: 0x004D7B54 File Offset: 0x004D5D54
		public static List<string> DrawBottomToTop(ScreenBuffer _ScreenBuffer, string str, int x1, int y1, int x2, int y2)
		{
			int num = x2 - x1 + 1;
			int num2 = y2 - y1 + 1;
			List<string> lines = new TextBlock(str, num, num2, true, 0).Lines;
			int num3 = 0;
			int num4 = y2;
			while (num4 >= y1 && num3 < lines.Count)
			{
				_ScreenBuffer.Goto(x1, num4);
				_ScreenBuffer.Write(lines[num3], true, false, false, null, -1);
				num4--;
				num3++;
			}
			return lines;
		}

		// Token: 0x0600D0E8 RID: 53480 RVA: 0x004D7BC0 File Offset: 0x004D5DC0
		public static void DrawBottomToTop(ScreenBuffer _ScreenBuffer, List<string> Lines, int x1, int y1, int x2, int y2)
		{
			int num = 0;
			int num2 = y2;
			while (num2 >= y1 && num < Lines.Count)
			{
				_ScreenBuffer.Goto(x1, num2);
				_ScreenBuffer.Write(Lines[num], true, false, false, null, -1);
				num2--;
				num++;
			}
		}
	}
}
