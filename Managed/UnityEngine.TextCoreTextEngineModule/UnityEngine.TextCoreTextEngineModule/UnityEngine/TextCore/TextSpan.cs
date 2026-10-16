using System;
using UnityEngine.Bindings;
using UnityEngine.TextCore.Text;

namespace UnityEngine.TextCore
{
	// Token: 0x02000007 RID: 7
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal struct TextSpan
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002640 File Offset: 0x00000840
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				string.Format("{0}: {1}\n", "color", this.color),
				string.Format("{0}: {1}\n", "fontStyle", this.fontStyle),
				string.Format("{0}: {1}\n", "fontWeight", this.fontWeight),
				string.Format("{0}: {1}\n", "linkID", this.linkID),
				string.Format("{0}: {1}\n", "fontSize", this.fontSize),
				string.Format("{0}: {1}", "fontAsset", this.fontAsset),
				string.Format("{0}: {1}\n", "startIndex", this.startIndex),
				string.Format("{0}: {1}", "length", this.length)
			});
		}

		// Token: 0x04000018 RID: 24
		public int startIndex;

		// Token: 0x04000019 RID: 25
		public int length;

		// Token: 0x0400001A RID: 26
		public IntPtr fontAsset;

		// Token: 0x0400001B RID: 27
		public int fontSize;

		// Token: 0x0400001C RID: 28
		public Color32 color;

		// Token: 0x0400001D RID: 29
		public FontStyles fontStyle;

		// Token: 0x0400001E RID: 30
		public TextFontWeight fontWeight;

		// Token: 0x0400001F RID: 31
		public int linkID;
	}
}
