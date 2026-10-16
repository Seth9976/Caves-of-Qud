using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.TextCore.LowLevel;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200001E RID: 30
	[Serializable]
	public class FontFeatureTable
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000055 RID: 85 RVA: 0x000043EC File Offset: 0x000025EC
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00004404 File Offset: 0x00002604
		internal List<MultipleSubstitutionRecord> multipleSubstitutionRecords
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

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00004410 File Offset: 0x00002610
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00004428 File Offset: 0x00002628
		internal List<LigatureSubstitutionRecord> ligatureRecords
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

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00004432 File Offset: 0x00002632
		internal List<GlyphPairAdjustmentRecord> glyphPairAdjustmentRecords
		{
			get
			{
				return this.m_GlyphPairAdjustmentRecords;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005A RID: 90 RVA: 0x0000443C File Offset: 0x0000263C
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00004454 File Offset: 0x00002654
		internal List<MarkToBaseAdjustmentRecord> MarkToBaseAdjustmentRecords
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

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00004460 File Offset: 0x00002660
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00004478 File Offset: 0x00002678
		internal List<MarkToMarkAdjustmentRecord> MarkToMarkAdjustmentRecords
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

		// Token: 0x0600005E RID: 94 RVA: 0x00004484 File Offset: 0x00002684
		internal FontFeatureTable()
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

		// Token: 0x0600005F RID: 95 RVA: 0x000044F4 File Offset: 0x000026F4
		public void SortGlyphPairAdjustmentRecords()
		{
			bool flag = this.m_GlyphPairAdjustmentRecords.Count > 1;
			if (flag)
			{
				this.m_GlyphPairAdjustmentRecords = (from s in this.m_GlyphPairAdjustmentRecords
					orderby s.firstAdjustmentRecord.glyphIndex, s.secondAdjustmentRecord.glyphIndex
					select s).ToList<GlyphPairAdjustmentRecord>();
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004570 File Offset: 0x00002770
		public void SortMarkToBaseAdjustmentRecords()
		{
			bool flag = this.m_MarkToBaseAdjustmentRecords.Count > 0;
			if (flag)
			{
				this.m_MarkToBaseAdjustmentRecords = (from s in this.m_MarkToBaseAdjustmentRecords
					orderby s.baseGlyphID, s.markGlyphID
					select s).ToList<MarkToBaseAdjustmentRecord>();
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000045EC File Offset: 0x000027EC
		public void SortMarkToMarkAdjustmentRecords()
		{
			bool flag = this.m_MarkToMarkAdjustmentRecords.Count > 0;
			if (flag)
			{
				this.m_MarkToMarkAdjustmentRecords = (from s in this.m_MarkToMarkAdjustmentRecords
					orderby s.baseMarkGlyphID, s.combiningMarkGlyphID
					select s).ToList<MarkToMarkAdjustmentRecord>();
			}
		}

		// Token: 0x04000083 RID: 131
		[SerializeField]
		internal List<MultipleSubstitutionRecord> m_MultipleSubstitutionRecords;

		// Token: 0x04000084 RID: 132
		[SerializeField]
		internal List<LigatureSubstitutionRecord> m_LigatureSubstitutionRecords;

		// Token: 0x04000085 RID: 133
		[SerializeField]
		private List<GlyphPairAdjustmentRecord> m_GlyphPairAdjustmentRecords;

		// Token: 0x04000086 RID: 134
		[SerializeField]
		internal List<MarkToBaseAdjustmentRecord> m_MarkToBaseAdjustmentRecords;

		// Token: 0x04000087 RID: 135
		[SerializeField]
		internal List<MarkToMarkAdjustmentRecord> m_MarkToMarkAdjustmentRecords;

		// Token: 0x04000088 RID: 136
		internal Dictionary<uint, List<LigatureSubstitutionRecord>> m_LigatureSubstitutionRecordLookup;

		// Token: 0x04000089 RID: 137
		internal Dictionary<uint, GlyphPairAdjustmentRecord> m_GlyphPairAdjustmentRecordLookup;

		// Token: 0x0400008A RID: 138
		internal Dictionary<uint, MarkToBaseAdjustmentRecord> m_MarkToBaseAdjustmentRecordLookup;

		// Token: 0x0400008B RID: 139
		internal Dictionary<uint, MarkToMarkAdjustmentRecord> m_MarkToMarkAdjustmentRecordLookup;
	}
}
