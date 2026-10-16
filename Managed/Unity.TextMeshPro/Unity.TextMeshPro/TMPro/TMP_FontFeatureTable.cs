using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

namespace TMPro
{
	// Token: 0x02000030 RID: 48
	[Serializable]
	public class TMP_FontFeatureTable
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000198 RID: 408 RVA: 0x000097BA File Offset: 0x000079BA
		// (set) Token: 0x06000199 RID: 409 RVA: 0x000097C2 File Offset: 0x000079C2
		public List<MultipleSubstitutionRecord> multipleSubstitutionRecords
		{
			get
			{
				return this.m_MultipleSubstitutionRecords;
			}
			set
			{
				this.m_MultipleSubstitutionRecords = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600019A RID: 410 RVA: 0x000097CB File Offset: 0x000079CB
		// (set) Token: 0x0600019B RID: 411 RVA: 0x000097D3 File Offset: 0x000079D3
		public List<LigatureSubstitutionRecord> ligatureRecords
		{
			get
			{
				return this.m_LigatureSubstitutionRecords;
			}
			set
			{
				this.m_LigatureSubstitutionRecords = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600019C RID: 412 RVA: 0x000097DC File Offset: 0x000079DC
		// (set) Token: 0x0600019D RID: 413 RVA: 0x000097E4 File Offset: 0x000079E4
		public List<GlyphPairAdjustmentRecord> glyphPairAdjustmentRecords
		{
			get
			{
				return this.m_GlyphPairAdjustmentRecords;
			}
			set
			{
				this.m_GlyphPairAdjustmentRecords = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600019E RID: 414 RVA: 0x000097ED File Offset: 0x000079ED
		// (set) Token: 0x0600019F RID: 415 RVA: 0x000097F5 File Offset: 0x000079F5
		public List<MarkToBaseAdjustmentRecord> MarkToBaseAdjustmentRecords
		{
			get
			{
				return this.m_MarkToBaseAdjustmentRecords;
			}
			set
			{
				this.m_MarkToBaseAdjustmentRecords = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x000097FE File Offset: 0x000079FE
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x00009806 File Offset: 0x00007A06
		public List<MarkToMarkAdjustmentRecord> MarkToMarkAdjustmentRecords
		{
			get
			{
				return this.m_MarkToMarkAdjustmentRecords;
			}
			set
			{
				this.m_MarkToMarkAdjustmentRecords = value;
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00009810 File Offset: 0x00007A10
		public TMP_FontFeatureTable()
		{
			this.m_LigatureSubstitutionRecords = new List<LigatureSubstitutionRecord>();
			this.m_LigatureSubstitutionRecordLookup = new Dictionary<uint, List<LigatureSubstitutionRecord>>();
			this.m_GlyphPairAdjustmentRecords = new List<GlyphPairAdjustmentRecord>();
			this.m_GlyphPairAdjustmentRecordLookup = new Dictionary<uint, GlyphPairAdjustmentRecord>();
			this.m_MarkToBaseAdjustmentRecords = new List<MarkToBaseAdjustmentRecord>();
			this.m_MarkToBaseAdjustmentRecordLookup = new Dictionary<uint, MarkToBaseAdjustmentRecord>();
			this.m_MarkToMarkAdjustmentRecords = new List<MarkToMarkAdjustmentRecord>();
			this.m_MarkToMarkAdjustmentRecordLookup = new Dictionary<uint, MarkToMarkAdjustmentRecord>();
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000987C File Offset: 0x00007A7C
		public void SortGlyphPairAdjustmentRecords()
		{
			if (this.m_GlyphPairAdjustmentRecords.Count > 0)
			{
				this.m_GlyphPairAdjustmentRecords = (from s in this.m_GlyphPairAdjustmentRecords
					orderby s.firstAdjustmentRecord.glyphIndex, s.secondAdjustmentRecord.glyphIndex
					select s).ToList<GlyphPairAdjustmentRecord>();
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000098F0 File Offset: 0x00007AF0
		public void SortMarkToBaseAdjustmentRecords()
		{
			if (this.m_MarkToBaseAdjustmentRecords.Count > 0)
			{
				this.m_MarkToBaseAdjustmentRecords = (from s in this.m_MarkToBaseAdjustmentRecords
					orderby s.baseGlyphID, s.markGlyphID
					select s).ToList<MarkToBaseAdjustmentRecord>();
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00009964 File Offset: 0x00007B64
		public void SortMarkToMarkAdjustmentRecords()
		{
			if (this.m_MarkToMarkAdjustmentRecords.Count > 0)
			{
				this.m_MarkToMarkAdjustmentRecords = (from s in this.m_MarkToMarkAdjustmentRecords
					orderby s.baseMarkGlyphID, s.combiningMarkGlyphID
					select s).ToList<MarkToMarkAdjustmentRecord>();
			}
		}

		// Token: 0x04000127 RID: 295
		[SerializeField]
		internal List<MultipleSubstitutionRecord> m_MultipleSubstitutionRecords;

		// Token: 0x04000128 RID: 296
		[SerializeField]
		internal List<LigatureSubstitutionRecord> m_LigatureSubstitutionRecords;

		// Token: 0x04000129 RID: 297
		[SerializeField]
		internal List<GlyphPairAdjustmentRecord> m_GlyphPairAdjustmentRecords;

		// Token: 0x0400012A RID: 298
		[SerializeField]
		internal List<MarkToBaseAdjustmentRecord> m_MarkToBaseAdjustmentRecords;

		// Token: 0x0400012B RID: 299
		[SerializeField]
		internal List<MarkToMarkAdjustmentRecord> m_MarkToMarkAdjustmentRecords;

		// Token: 0x0400012C RID: 300
		internal Dictionary<uint, List<LigatureSubstitutionRecord>> m_LigatureSubstitutionRecordLookup;

		// Token: 0x0400012D RID: 301
		internal Dictionary<uint, GlyphPairAdjustmentRecord> m_GlyphPairAdjustmentRecordLookup;

		// Token: 0x0400012E RID: 302
		internal Dictionary<uint, MarkToBaseAdjustmentRecord> m_MarkToBaseAdjustmentRecordLookup;

		// Token: 0x0400012F RID: 303
		internal Dictionary<uint, MarkToMarkAdjustmentRecord> m_MarkToMarkAdjustmentRecordLookup;
	}
}
