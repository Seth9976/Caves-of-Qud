using System;
using System.Collections.Generic;
using UnityEngine;

namespace TMPro.SpriteAssetUtilities
{
	// Token: 0x02000085 RID: 133
	public class TexturePacker_JsonArray
	{
		// Token: 0x020000BE RID: 190
		[Serializable]
		public struct SpriteFrame
		{
			// Token: 0x06000717 RID: 1815 RVA: 0x00044818 File Offset: 0x00042A18
			public override string ToString()
			{
				return string.Concat(new string[]
				{
					"x: ",
					this.x.ToString("f2"),
					" y: ",
					this.y.ToString("f2"),
					" h: ",
					this.h.ToString("f2"),
					" w: ",
					this.w.ToString("f2")
				});
			}

			// Token: 0x040006A2 RID: 1698
			public float x;

			// Token: 0x040006A3 RID: 1699
			public float y;

			// Token: 0x040006A4 RID: 1700
			public float w;

			// Token: 0x040006A5 RID: 1701
			public float h;
		}

		// Token: 0x020000BF RID: 191
		[Serializable]
		public struct SpriteSize
		{
			// Token: 0x06000718 RID: 1816 RVA: 0x0004489C File Offset: 0x00042A9C
			public override string ToString()
			{
				return "w: " + this.w.ToString("f2") + " h: " + this.h.ToString("f2");
			}

			// Token: 0x040006A6 RID: 1702
			public float w;

			// Token: 0x040006A7 RID: 1703
			public float h;
		}

		// Token: 0x020000C0 RID: 192
		[Serializable]
		public struct Frame
		{
			// Token: 0x040006A8 RID: 1704
			public string filename;

			// Token: 0x040006A9 RID: 1705
			public TexturePacker_JsonArray.SpriteFrame frame;

			// Token: 0x040006AA RID: 1706
			public bool rotated;

			// Token: 0x040006AB RID: 1707
			public bool trimmed;

			// Token: 0x040006AC RID: 1708
			public TexturePacker_JsonArray.SpriteFrame spriteSourceSize;

			// Token: 0x040006AD RID: 1709
			public TexturePacker_JsonArray.SpriteSize sourceSize;

			// Token: 0x040006AE RID: 1710
			public Vector2 pivot;
		}

		// Token: 0x020000C1 RID: 193
		[Serializable]
		public struct Meta
		{
			// Token: 0x040006AF RID: 1711
			public string app;

			// Token: 0x040006B0 RID: 1712
			public string version;

			// Token: 0x040006B1 RID: 1713
			public string image;

			// Token: 0x040006B2 RID: 1714
			public string format;

			// Token: 0x040006B3 RID: 1715
			public TexturePacker_JsonArray.SpriteSize size;

			// Token: 0x040006B4 RID: 1716
			public float scale;

			// Token: 0x040006B5 RID: 1717
			public string smartupdate;
		}

		// Token: 0x020000C2 RID: 194
		[Serializable]
		public class SpriteDataObject
		{
			// Token: 0x040006B6 RID: 1718
			public List<TexturePacker_JsonArray.Frame> frames;

			// Token: 0x040006B7 RID: 1719
			public TexturePacker_JsonArray.Meta meta;
		}
	}
}
