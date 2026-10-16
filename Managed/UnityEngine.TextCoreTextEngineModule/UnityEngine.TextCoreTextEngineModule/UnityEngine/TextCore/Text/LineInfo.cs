using System;
using UnityEngine.Bindings;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000021 RID: 33
	[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
	internal struct LineInfo
	{
		// Token: 0x04000095 RID: 149
		internal int controlCharacterCount;

		// Token: 0x04000096 RID: 150
		public int characterCount;

		// Token: 0x04000097 RID: 151
		public int visibleCharacterCount;

		// Token: 0x04000098 RID: 152
		public int visibleSpaceCount;

		// Token: 0x04000099 RID: 153
		public int spaceCount;

		// Token: 0x0400009A RID: 154
		public int wordCount;

		// Token: 0x0400009B RID: 155
		public int firstCharacterIndex;

		// Token: 0x0400009C RID: 156
		public int firstVisibleCharacterIndex;

		// Token: 0x0400009D RID: 157
		public int lastCharacterIndex;

		// Token: 0x0400009E RID: 158
		public int lastVisibleCharacterIndex;

		// Token: 0x0400009F RID: 159
		public float length;

		// Token: 0x040000A0 RID: 160
		public float lineHeight;

		// Token: 0x040000A1 RID: 161
		public float ascender;

		// Token: 0x040000A2 RID: 162
		public float baseline;

		// Token: 0x040000A3 RID: 163
		public float descender;

		// Token: 0x040000A4 RID: 164
		public float maxAdvance;

		// Token: 0x040000A5 RID: 165
		public float width;

		// Token: 0x040000A6 RID: 166
		public float marginLeft;

		// Token: 0x040000A7 RID: 167
		public float marginRight;

		// Token: 0x040000A8 RID: 168
		public TextAlignment alignment;

		// Token: 0x040000A9 RID: 169
		public Extents lineExtents;
	}
}
