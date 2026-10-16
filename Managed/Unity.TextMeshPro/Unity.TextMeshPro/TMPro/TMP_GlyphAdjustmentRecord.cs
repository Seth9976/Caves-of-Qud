using System;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

namespace TMPro
{
	// Token: 0x02000033 RID: 51
	[Serializable]
	public struct TMP_GlyphAdjustmentRecord
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00009B0A File Offset: 0x00007D0A
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x00009B12 File Offset: 0x00007D12
		public uint glyphIndex
		{
			get
			{
				return this.m_GlyphIndex;
			}
			set
			{
				this.m_GlyphIndex = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00009B1B File Offset: 0x00007D1B
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x00009B23 File Offset: 0x00007D23
		public TMP_GlyphValueRecord glyphValueRecord
		{
			get
			{
				return this.m_GlyphValueRecord;
			}
			set
			{
				this.m_GlyphValueRecord = value;
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00009B2C File Offset: 0x00007D2C
		public TMP_GlyphAdjustmentRecord(uint glyphIndex, TMP_GlyphValueRecord glyphValueRecord)
		{
			this.m_GlyphIndex = glyphIndex;
			this.m_GlyphValueRecord = glyphValueRecord;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00009B3C File Offset: 0x00007D3C
		internal TMP_GlyphAdjustmentRecord(GlyphAdjustmentRecord adjustmentRecord)
		{
			this.m_GlyphIndex = adjustmentRecord.glyphIndex;
			this.m_GlyphValueRecord = new TMP_GlyphValueRecord(adjustmentRecord.glyphValueRecord);
		}

		// Token: 0x04000138 RID: 312
		[SerializeField]
		internal uint m_GlyphIndex;

		// Token: 0x04000139 RID: 313
		[SerializeField]
		internal TMP_GlyphValueRecord m_GlyphValueRecord;
	}
}
