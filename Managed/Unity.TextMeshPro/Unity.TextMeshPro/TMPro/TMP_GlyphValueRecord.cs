using System;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

namespace TMPro
{
	// Token: 0x02000032 RID: 50
	[Serializable]
	public struct TMP_GlyphValueRecord
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x000099D8 File Offset: 0x00007BD8
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x000099E0 File Offset: 0x00007BE0
		public float xPlacement
		{
			get
			{
				return this.m_XPlacement;
			}
			set
			{
				this.m_XPlacement = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x000099E9 File Offset: 0x00007BE9
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x000099F1 File Offset: 0x00007BF1
		public float yPlacement
		{
			get
			{
				return this.m_YPlacement;
			}
			set
			{
				this.m_YPlacement = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001AA RID: 426 RVA: 0x000099FA File Offset: 0x00007BFA
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00009A02 File Offset: 0x00007C02
		public float xAdvance
		{
			get
			{
				return this.m_XAdvance;
			}
			set
			{
				this.m_XAdvance = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00009A0B File Offset: 0x00007C0B
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00009A13 File Offset: 0x00007C13
		public float yAdvance
		{
			get
			{
				return this.m_YAdvance;
			}
			set
			{
				this.m_YAdvance = value;
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00009A1C File Offset: 0x00007C1C
		public TMP_GlyphValueRecord(float xPlacement, float yPlacement, float xAdvance, float yAdvance)
		{
			this.m_XPlacement = xPlacement;
			this.m_YPlacement = yPlacement;
			this.m_XAdvance = xAdvance;
			this.m_YAdvance = yAdvance;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00009A3B File Offset: 0x00007C3B
		internal TMP_GlyphValueRecord(GlyphValueRecord_Legacy valueRecord)
		{
			this.m_XPlacement = valueRecord.xPlacement;
			this.m_YPlacement = valueRecord.yPlacement;
			this.m_XAdvance = valueRecord.xAdvance;
			this.m_YAdvance = valueRecord.yAdvance;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00009A6D File Offset: 0x00007C6D
		internal TMP_GlyphValueRecord(GlyphValueRecord valueRecord)
		{
			this.m_XPlacement = valueRecord.xPlacement;
			this.m_YPlacement = valueRecord.yPlacement;
			this.m_XAdvance = valueRecord.xAdvance;
			this.m_YAdvance = valueRecord.yAdvance;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00009AA4 File Offset: 0x00007CA4
		public static TMP_GlyphValueRecord operator +(TMP_GlyphValueRecord a, TMP_GlyphValueRecord b)
		{
			TMP_GlyphValueRecord tmp_GlyphValueRecord;
			tmp_GlyphValueRecord.m_XPlacement = a.xPlacement + b.xPlacement;
			tmp_GlyphValueRecord.m_YPlacement = a.yPlacement + b.yPlacement;
			tmp_GlyphValueRecord.m_XAdvance = a.xAdvance + b.xAdvance;
			tmp_GlyphValueRecord.m_YAdvance = a.yAdvance + b.yAdvance;
			return tmp_GlyphValueRecord;
		}

		// Token: 0x04000134 RID: 308
		[SerializeField]
		internal float m_XPlacement;

		// Token: 0x04000135 RID: 309
		[SerializeField]
		internal float m_YPlacement;

		// Token: 0x04000136 RID: 310
		[SerializeField]
		internal float m_XAdvance;

		// Token: 0x04000137 RID: 311
		[SerializeField]
		internal float m_YAdvance;
	}
}
