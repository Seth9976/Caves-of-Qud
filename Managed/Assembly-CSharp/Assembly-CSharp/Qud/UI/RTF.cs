using System;
using ConsoleLib.Console;
using Cysharp.Text;
using XRL.UI;

namespace Qud.UI
{
	// Token: 0x0200041C RID: 1052
	public static class RTF
	{
		// Token: 0x060027F8 RID: 10232 RVA: 0x000CEAAC File Offset: 0x000CCCAC
		public static string FormatToRTF(string s, string opacity = "FF", int blockWrap = -1, bool stripFormatting = false)
		{
			if (Options.StripUIColorText || stripFormatting)
			{
				s = ColorUtility.StripFormatting(s);
			}
			Utf16ValueStringBuilder utf16ValueStringBuilder = ZString.CreateStringBuilder();
			string text;
			try
			{
				if (blockWrap > 0)
				{
					s = RTF.BlockWrap(s, blockWrap, 5000);
				}
				Sidebar.FormatToRTF(s, ref utf16ValueStringBuilder, opacity);
				text = utf16ValueStringBuilder.ToString();
			}
			finally
			{
				utf16ValueStringBuilder.Dispose();
			}
			return text;
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x000CEB14 File Offset: 0x000CCD14
		public static string BlockWrap(string s, int width, int maxLines)
		{
			return new TextBlock(s, width, maxLines, false, 0).ToString();
		}
	}
}
