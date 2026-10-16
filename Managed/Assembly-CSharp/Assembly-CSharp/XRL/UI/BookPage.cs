using System;
using System.Collections.Generic;
using System.Text;

namespace XRL.UI
{
	// Token: 0x020014B6 RID: 5302
	public class BookPage
	{
		// Token: 0x0600D02F RID: 53295 RVA: 0x004CC658 File Offset: 0x004CA858
		public BookPage(string Title, string Data)
		{
			string[] array = Data.Split('\n', StringSplitOptions.None);
			for (int i = 0; i < array.Length; i++)
			{
				foreach (string text in StringFormat.ClipText(array[i], 77, true, true, false).Split('\n', StringSplitOptions.None))
				{
					this.Lines.Add(text.Replace("\r", "") + "\n");
				}
			}
			this.Title = Title;
		}

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x0600D030 RID: 53296 RVA: 0x004CC704 File Offset: 0x004CA904
		public string FullText
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string text in this.Lines)
				{
					stringBuilder.Append(text);
					stringBuilder.Append("\n");
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x0600D031 RID: 53297 RVA: 0x004CC770 File Offset: 0x004CA970
		public string RenderForModernUI
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string text in this.Lines)
				{
					stringBuilder.Append(text);
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x040051AC RID: 20908
		public string Format = "Manual";

		// Token: 0x040051AD RID: 20909
		public int LeftMargin = 2;

		// Token: 0x040051AE RID: 20910
		public int RightMargin = 2;

		// Token: 0x040051AF RID: 20911
		public int TopMargin;

		// Token: 0x040051B0 RID: 20912
		public int BottomMargin = 2;

		// Token: 0x040051B1 RID: 20913
		public string Title;

		// Token: 0x040051B2 RID: 20914
		public List<string> Lines = new List<string>();
	}
}
