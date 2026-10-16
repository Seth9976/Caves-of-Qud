using System;
using System.Collections.Generic;
using ex2D.Detail;
using UnityEngine;

// Token: 0x020000AE RID: 174
public class exTextureInfo : ScriptableObject
{
	// Token: 0x06000636 RID: 1590 RVA: 0x0002CE6C File Offset: 0x0002B06C
	public static exTextureInfo Create(Texture2D _texture)
	{
		exTextureInfo exTextureInfo = ScriptableObject.CreateInstance<exTextureInfo>();
		Rect rect = new Rect(0f, 0f, (float)_texture.width, (float)_texture.height);
		exTextureInfo.texture = _texture;
		exTextureInfo.rotated = false;
		exTextureInfo.trim = false;
		exTextureInfo.trim_x = (int)rect.x;
		exTextureInfo.trim_y = (int)rect.y;
		exTextureInfo.width = (int)rect.width;
		exTextureInfo.height = (int)rect.height;
		exTextureInfo.x = (int)rect.x;
		exTextureInfo.y = (int)rect.y;
		exTextureInfo.rawWidth = _texture.width;
		exTextureInfo.rawHeight = _texture.height;
		return exTextureInfo;
	}

	// Token: 0x1700009B RID: 155
	// (get) Token: 0x06000637 RID: 1591 RVA: 0x0002CF1E File Offset: 0x0002B11E
	public int rotatedWidth
	{
		get
		{
			if (this.rotated)
			{
				return this.height;
			}
			return this.width;
		}
	}

	// Token: 0x1700009C RID: 156
	// (get) Token: 0x06000638 RID: 1592 RVA: 0x0002CF35 File Offset: 0x0002B135
	public int rotatedHeight
	{
		get
		{
			if (this.rotated)
			{
				return this.width;
			}
			return this.height;
		}
	}

	// Token: 0x1700009D RID: 157
	// (get) Token: 0x06000639 RID: 1593 RVA: 0x0002CF4C File Offset: 0x0002B14C
	public bool hasBorder
	{
		get
		{
			return this.borderLeft != 0 || this.borderRight != 0 || this.borderTop != 0 || this.borderBottom != 0;
		}
	}

	// Token: 0x1700009E RID: 158
	// (get) Token: 0x0600063A RID: 1594 RVA: 0x0002CF71 File Offset: 0x0002B171
	public int diceUnitWidth
	{
		get
		{
			if (this.diceData != null && this.diceData.Count > 0)
			{
				return this.diceData[0];
			}
			return 0;
		}
	}

	// Token: 0x1700009F RID: 159
	// (get) Token: 0x0600063B RID: 1595 RVA: 0x0002CF97 File Offset: 0x0002B197
	public int diceUnitHeight
	{
		get
		{
			if (this.diceData != null && this.diceData.Count > 0)
			{
				return this.diceData[1];
			}
			return 0;
		}
	}

	// Token: 0x170000A0 RID: 160
	// (get) Token: 0x0600063C RID: 1596 RVA: 0x0002CFC0 File Offset: 0x0002B1C0
	public bool isDiced
	{
		get
		{
			return this.diceData != null && this.diceData.Count > 0 && ((this.diceData[0] > 0 && this.diceData[0] < this.width) || (this.diceData[1] > 0 && this.diceData[1] < this.height));
		}
	}

	// Token: 0x170000A1 RID: 161
	// (get) Token: 0x0600063D RID: 1597 RVA: 0x0002D02F File Offset: 0x0002B22F
	public DiceEnumerator dices
	{
		get
		{
			return new DiceEnumerator(this.diceData, this);
		}
	}

	// Token: 0x04000497 RID: 1175
	public string rawTextureGUID = "";

	// Token: 0x04000498 RID: 1176
	public string rawAtlasGUID = "";

	// Token: 0x04000499 RID: 1177
	public Texture2D texture;

	// Token: 0x0400049A RID: 1178
	public bool rotated;

	// Token: 0x0400049B RID: 1179
	public bool trim;

	// Token: 0x0400049C RID: 1180
	public int trimThreshold = 1;

	// Token: 0x0400049D RID: 1181
	public int trim_x;

	// Token: 0x0400049E RID: 1182
	public int trim_y;

	// Token: 0x0400049F RID: 1183
	public int rawWidth = 1;

	// Token: 0x040004A0 RID: 1184
	public int rawHeight = 1;

	// Token: 0x040004A1 RID: 1185
	public int x;

	// Token: 0x040004A2 RID: 1186
	public int y;

	// Token: 0x040004A3 RID: 1187
	public int width = 1;

	// Token: 0x040004A4 RID: 1188
	public int height = 1;

	// Token: 0x040004A5 RID: 1189
	public int borderLeft;

	// Token: 0x040004A6 RID: 1190
	public int borderRight;

	// Token: 0x040004A7 RID: 1191
	public int borderTop;

	// Token: 0x040004A8 RID: 1192
	public int borderBottom;

	// Token: 0x040004A9 RID: 1193
	public int ShaderMode;

	// Token: 0x040004AA RID: 1194
	[SerializeField]
	private List<int> diceData = new List<int>();

	// Token: 0x020015BF RID: 5567
	public enum DiceType
	{
		// Token: 0x040057A2 RID: 22434
		Empty,
		// Token: 0x040057A3 RID: 22435
		Max,
		// Token: 0x040057A4 RID: 22436
		Trimmed
	}

	// Token: 0x020015C0 RID: 5568
	public struct Dice
	{
		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x0600D5F9 RID: 54777 RVA: 0x004F756E File Offset: 0x004F576E
		public int rotatedWidth
		{
			get
			{
				if (!this.rotated)
				{
					return this.width;
				}
				return this.height;
			}
		}

		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x0600D5FA RID: 54778 RVA: 0x004F7585 File Offset: 0x004F5785
		public int rotatedHeight
		{
			get
			{
				if (!this.rotated)
				{
					return this.height;
				}
				return this.width;
			}
		}

		// Token: 0x040057A5 RID: 22437
		public exTextureInfo.DiceType sizeType;

		// Token: 0x040057A6 RID: 22438
		public int offset_x;

		// Token: 0x040057A7 RID: 22439
		public int offset_y;

		// Token: 0x040057A8 RID: 22440
		public int width;

		// Token: 0x040057A9 RID: 22441
		public int height;

		// Token: 0x040057AA RID: 22442
		public int x;

		// Token: 0x040057AB RID: 22443
		public int y;

		// Token: 0x040057AC RID: 22444
		public bool rotated;
	}
}
