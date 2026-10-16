using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000AC RID: 172
public class exBitmapFont : ScriptableObject
{
	// Token: 0x06000622 RID: 1570 RVA: 0x0002C968 File Offset: 0x0002AB68
	public void Reset()
	{
		this.rawFontGUID = "";
		this.texture = null;
		this.charInfos.Clear();
		this.kernings.Clear();
		this.baseLine = 0;
		this.lineHeight = 0;
		this.size = 0;
		this.charInfoTable = null;
		this.kerningTable = null;
	}

	// Token: 0x06000623 RID: 1571 RVA: 0x0002C9C0 File Offset: 0x0002ABC0
	public void RebuildCharInfoTable()
	{
		if (this.charInfoTable == null)
		{
			this.charInfoTable = new Dictionary<int, exBitmapFont.CharInfo>(this.charInfos.Count);
		}
		this.charInfoTable.Clear();
		for (int i = 0; i < this.charInfos.Count; i++)
		{
			exBitmapFont.CharInfo charInfo = this.charInfos[i];
			this.charInfoTable[charInfo.id] = charInfo;
		}
	}

	// Token: 0x06000624 RID: 1572 RVA: 0x0002CA2C File Offset: 0x0002AC2C
	public exBitmapFont.CharInfo GetCharInfo(char _symbol)
	{
		if (this.charInfoTable == null || this.charInfoTable.Count == 0)
		{
			this.RebuildCharInfoTable();
		}
		exBitmapFont.CharInfo charInfo;
		if (this.charInfoTable.TryGetValue((int)_symbol, out charInfo))
		{
			return charInfo;
		}
		return null;
	}

	// Token: 0x06000625 RID: 1573 RVA: 0x0002CA68 File Offset: 0x0002AC68
	public void RebuildKerningTable()
	{
		if (this.kerningTable == null)
		{
			this.kerningTable = new Dictionary<exBitmapFont.KerningTableKey, int>(this.kernings.Count, exBitmapFont.KerningTableKey.Comparer.instance);
		}
		this.kerningTable.Clear();
		for (int i = 0; i < this.kernings.Count; i++)
		{
			exBitmapFont.KerningInfo kerningInfo = this.kernings[i];
			this.kerningTable[new exBitmapFont.KerningTableKey((char)kerningInfo.first, (char)kerningInfo.second)] = kerningInfo.amount;
		}
	}

	// Token: 0x06000626 RID: 1574 RVA: 0x0002CAEC File Offset: 0x0002ACEC
	public int GetKerning(char _first, char _second)
	{
		if (this.kerningTable == null)
		{
			this.RebuildKerningTable();
		}
		int num;
		if (this.kerningTable.TryGetValue(new exBitmapFont.KerningTableKey(_first, _second), out num))
		{
			return num;
		}
		return 0;
	}

	// Token: 0x04000484 RID: 1156
	public string rawFontGUID = "";

	// Token: 0x04000485 RID: 1157
	public string rawTextureGUID = "";

	// Token: 0x04000486 RID: 1158
	public string rawAtlasGUID = "";

	// Token: 0x04000487 RID: 1159
	public Texture2D texture;

	// Token: 0x04000488 RID: 1160
	public List<exBitmapFont.CharInfo> charInfos = new List<exBitmapFont.CharInfo>();

	// Token: 0x04000489 RID: 1161
	public List<exBitmapFont.KerningInfo> kernings = new List<exBitmapFont.KerningInfo>();

	// Token: 0x0400048A RID: 1162
	public int baseLine;

	// Token: 0x0400048B RID: 1163
	public int lineHeight;

	// Token: 0x0400048C RID: 1164
	public int size;

	// Token: 0x0400048D RID: 1165
	protected Dictionary<int, exBitmapFont.CharInfo> charInfoTable;

	// Token: 0x0400048E RID: 1166
	protected Dictionary<exBitmapFont.KerningTableKey, int> kerningTable;

	// Token: 0x020015B9 RID: 5561
	[Serializable]
	public class CharInfo
	{
		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x0600D5EF RID: 54767 RVA: 0x004F7396 File Offset: 0x004F5596
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

		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x0600D5F0 RID: 54768 RVA: 0x004F73AD File Offset: 0x004F55AD
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

		// Token: 0x0600D5F1 RID: 54769 RVA: 0x004F73C4 File Offset: 0x004F55C4
		public CharInfo()
		{
		}

		// Token: 0x0600D5F2 RID: 54770 RVA: 0x004F7420 File Offset: 0x004F5620
		public CharInfo(exBitmapFont.CharInfo _c)
		{
			this.id = _c.id;
			this.x = _c.x;
			this.y = _c.y;
			this.width = _c.width;
			this.height = _c.height;
			this.xoffset = _c.xoffset;
			this.yoffset = _c.yoffset;
			this.xadvance = _c.xadvance;
			this.rotated = _c.rotated;
		}

		// Token: 0x04005782 RID: 22402
		public int id = -1;

		// Token: 0x04005783 RID: 22403
		public int trim_x = -1;

		// Token: 0x04005784 RID: 22404
		public int trim_y = -1;

		// Token: 0x04005785 RID: 22405
		public int x = -1;

		// Token: 0x04005786 RID: 22406
		public int y = -1;

		// Token: 0x04005787 RID: 22407
		public int width = -1;

		// Token: 0x04005788 RID: 22408
		public int height = -1;

		// Token: 0x04005789 RID: 22409
		public int xoffset = -1;

		// Token: 0x0400578A RID: 22410
		public int yoffset = -1;

		// Token: 0x0400578B RID: 22411
		public int xadvance = -1;

		// Token: 0x0400578C RID: 22412
		public bool rotated;
	}

	// Token: 0x020015BA RID: 5562
	[Serializable]
	public class KerningInfo
	{
		// Token: 0x0400578D RID: 22413
		public int first = -1;

		// Token: 0x0400578E RID: 22414
		public int second = -1;

		// Token: 0x0400578F RID: 22415
		public int amount = -1;
	}

	// Token: 0x020015BB RID: 5563
	public struct KerningTableKey
	{
		// Token: 0x0600D5F4 RID: 54772 RVA: 0x004F7502 File Offset: 0x004F5702
		public KerningTableKey(char _first, char _second)
		{
			this.first = _first;
			this.second = _second;
		}

		// Token: 0x04005790 RID: 22416
		public char first;

		// Token: 0x04005791 RID: 22417
		public char second;

		// Token: 0x02001E6F RID: 7791
		public class Comparer : IEqualityComparer<exBitmapFont.KerningTableKey>
		{
			// Token: 0x17001030 RID: 4144
			// (get) Token: 0x0600F733 RID: 63283 RVA: 0x0055B9EE File Offset: 0x00559BEE
			public static exBitmapFont.KerningTableKey.Comparer instance
			{
				get
				{
					if (exBitmapFont.KerningTableKey.Comparer.instance_ == null)
					{
						exBitmapFont.KerningTableKey.Comparer.instance_ = new exBitmapFont.KerningTableKey.Comparer();
					}
					return exBitmapFont.KerningTableKey.Comparer.instance_;
				}
			}

			// Token: 0x0600F734 RID: 63284 RVA: 0x0055BA06 File Offset: 0x00559C06
			public bool Equals(exBitmapFont.KerningTableKey _lhs, exBitmapFont.KerningTableKey _rhs)
			{
				return _lhs.first == _rhs.first && _lhs.second == _rhs.second;
			}

			// Token: 0x0600F735 RID: 63285 RVA: 0x0055BA26 File Offset: 0x00559C26
			public int GetHashCode(exBitmapFont.KerningTableKey _obj)
			{
				return (int)(((int)_obj.first << 16) ^ _obj.second);
			}

			// Token: 0x0400763F RID: 30271
			private static exBitmapFont.KerningTableKey.Comparer instance_;
		}
	}
}
