using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200017B RID: 379
	[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule" })]
	internal struct Internal_DrawTextureArguments
	{
		// Token: 0x040004EA RID: 1258
		public Rect screenRect;

		// Token: 0x040004EB RID: 1259
		public Rect sourceRect;

		// Token: 0x040004EC RID: 1260
		public int leftBorder;

		// Token: 0x040004ED RID: 1261
		public int rightBorder;

		// Token: 0x040004EE RID: 1262
		public int topBorder;

		// Token: 0x040004EF RID: 1263
		public int bottomBorder;

		// Token: 0x040004F0 RID: 1264
		public Color leftBorderColor;

		// Token: 0x040004F1 RID: 1265
		public Color rightBorderColor;

		// Token: 0x040004F2 RID: 1266
		public Color topBorderColor;

		// Token: 0x040004F3 RID: 1267
		public Color bottomBorderColor;

		// Token: 0x040004F4 RID: 1268
		public Color color;

		// Token: 0x040004F5 RID: 1269
		public Vector4 borderWidths;

		// Token: 0x040004F6 RID: 1270
		public Vector4 cornerRadiuses;

		// Token: 0x040004F7 RID: 1271
		public bool smoothCorners;

		// Token: 0x040004F8 RID: 1272
		public int pass;

		// Token: 0x040004F9 RID: 1273
		public Texture texture;

		// Token: 0x040004FA RID: 1274
		public Material mat;
	}
}
