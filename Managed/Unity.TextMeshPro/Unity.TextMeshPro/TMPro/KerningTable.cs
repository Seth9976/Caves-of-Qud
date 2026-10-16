using System;
using System.Collections.Generic;
using System.Linq;

namespace TMPro
{
	// Token: 0x0200002D RID: 45
	[Serializable]
	public class KerningTable
	{
		// Token: 0x06000183 RID: 387 RVA: 0x00008F07 File Offset: 0x00007107
		public KerningTable()
		{
			this.kerningPairs = new List<KerningPair>();
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00008F1C File Offset: 0x0000711C
		public void AddKerningPair()
		{
			if (this.kerningPairs.Count == 0)
			{
				this.kerningPairs.Add(new KerningPair(0U, 0U, 0f));
				return;
			}
			uint firstGlyph = this.kerningPairs.Last<KerningPair>().firstGlyph;
			uint secondGlyph = this.kerningPairs.Last<KerningPair>().secondGlyph;
			float xOffset = this.kerningPairs.Last<KerningPair>().xOffset;
			this.kerningPairs.Add(new KerningPair(firstGlyph, secondGlyph, xOffset));
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00008F94 File Offset: 0x00007194
		public int AddKerningPair(uint first, uint second, float offset)
		{
			if (this.kerningPairs.FindIndex((KerningPair item) => item.firstGlyph == first && item.secondGlyph == second) == -1)
			{
				this.kerningPairs.Add(new KerningPair(first, second, offset));
				return 0;
			}
			return -1;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00008FF0 File Offset: 0x000071F0
		public int AddGlyphPairAdjustmentRecord(uint first, GlyphValueRecord_Legacy firstAdjustments, uint second, GlyphValueRecord_Legacy secondAdjustments)
		{
			if (this.kerningPairs.FindIndex((KerningPair item) => item.firstGlyph == first && item.secondGlyph == second) == -1)
			{
				this.kerningPairs.Add(new KerningPair(first, firstAdjustments, second, secondAdjustments));
				return 0;
			}
			return -1;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00009050 File Offset: 0x00007250
		public void RemoveKerningPair(int left, int right)
		{
			int num = this.kerningPairs.FindIndex((KerningPair item) => (ulong)item.firstGlyph == (ulong)((long)left) && (ulong)item.secondGlyph == (ulong)((long)right));
			if (num != -1)
			{
				this.kerningPairs.RemoveAt(num);
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00009099 File Offset: 0x00007299
		public void RemoveKerningPair(int index)
		{
			this.kerningPairs.RemoveAt(index);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000090A8 File Offset: 0x000072A8
		public void SortKerningPairs()
		{
			if (this.kerningPairs.Count > 0)
			{
				this.kerningPairs = (from s in this.kerningPairs
					orderby s.firstGlyph, s.secondGlyph
					select s).ToList<KerningPair>();
			}
		}

		// Token: 0x04000123 RID: 291
		public List<KerningPair> kerningPairs;
	}
}
