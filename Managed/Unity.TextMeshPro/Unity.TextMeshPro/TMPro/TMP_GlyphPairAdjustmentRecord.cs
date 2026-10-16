using System;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

namespace TMPro
{
	// Token: 0x02000034 RID: 52
	[Serializable]
	public class TMP_GlyphPairAdjustmentRecord
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00009B5D File Offset: 0x00007D5D
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x00009B65 File Offset: 0x00007D65
		public TMP_GlyphAdjustmentRecord firstAdjustmentRecord
		{
			get
			{
				return this.m_FirstAdjustmentRecord;
			}
			set
			{
				this.m_FirstAdjustmentRecord = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00009B6E File Offset: 0x00007D6E
		// (set) Token: 0x060001BB RID: 443 RVA: 0x00009B76 File Offset: 0x00007D76
		public TMP_GlyphAdjustmentRecord secondAdjustmentRecord
		{
			get
			{
				return this.m_SecondAdjustmentRecord;
			}
			set
			{
				this.m_SecondAdjustmentRecord = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00009B7F File Offset: 0x00007D7F
		// (set) Token: 0x060001BD RID: 445 RVA: 0x00009B87 File Offset: 0x00007D87
		public FontFeatureLookupFlags featureLookupFlags
		{
			get
			{
				return this.m_FeatureLookupFlags;
			}
			set
			{
				this.m_FeatureLookupFlags = value;
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00009B90 File Offset: 0x00007D90
		public TMP_GlyphPairAdjustmentRecord(TMP_GlyphAdjustmentRecord firstAdjustmentRecord, TMP_GlyphAdjustmentRecord secondAdjustmentRecord)
		{
			this.m_FirstAdjustmentRecord = firstAdjustmentRecord;
			this.m_SecondAdjustmentRecord = secondAdjustmentRecord;
			this.m_FeatureLookupFlags = FontFeatureLookupFlags.None;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00009BAD File Offset: 0x00007DAD
		internal TMP_GlyphPairAdjustmentRecord(GlyphPairAdjustmentRecord glyphPairAdjustmentRecord)
		{
			this.m_FirstAdjustmentRecord = new TMP_GlyphAdjustmentRecord(glyphPairAdjustmentRecord.firstAdjustmentRecord);
			this.m_SecondAdjustmentRecord = new TMP_GlyphAdjustmentRecord(glyphPairAdjustmentRecord.secondAdjustmentRecord);
			this.m_FeatureLookupFlags = FontFeatureLookupFlags.None;
		}

		// Token: 0x0400013A RID: 314
		[SerializeField]
		internal TMP_GlyphAdjustmentRecord m_FirstAdjustmentRecord;

		// Token: 0x0400013B RID: 315
		[SerializeField]
		internal TMP_GlyphAdjustmentRecord m_SecondAdjustmentRecord;

		// Token: 0x0400013C RID: 316
		[SerializeField]
		internal FontFeatureLookupFlags m_FeatureLookupFlags;
	}
}
