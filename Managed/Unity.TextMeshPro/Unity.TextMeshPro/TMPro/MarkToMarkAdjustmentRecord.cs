using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x0200000B RID: 11
	[Serializable]
	public struct MarkToMarkAdjustmentRecord
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002421 File Offset: 0x00000621
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002429 File Offset: 0x00000629
		public uint baseMarkGlyphID
		{
			get
			{
				return this.m_BaseMarkGlyphID;
			}
			set
			{
				this.m_BaseMarkGlyphID = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002432 File Offset: 0x00000632
		// (set) Token: 0x06000027 RID: 39 RVA: 0x0000243A File Offset: 0x0000063A
		public GlyphAnchorPoint baseMarkGlyphAnchorPoint
		{
			get
			{
				return this.m_BaseMarkGlyphAnchorPoint;
			}
			set
			{
				this.m_BaseMarkGlyphAnchorPoint = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002443 File Offset: 0x00000643
		// (set) Token: 0x06000029 RID: 41 RVA: 0x0000244B File Offset: 0x0000064B
		public uint combiningMarkGlyphID
		{
			get
			{
				return this.m_CombiningMarkGlyphID;
			}
			set
			{
				this.m_CombiningMarkGlyphID = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002454 File Offset: 0x00000654
		// (set) Token: 0x0600002B RID: 43 RVA: 0x0000245C File Offset: 0x0000065C
		public MarkPositionAdjustment combiningMarkPositionAdjustment
		{
			get
			{
				return this.m_CombiningMarkPositionAdjustment;
			}
			set
			{
				this.m_CombiningMarkPositionAdjustment = value;
			}
		}

		// Token: 0x04000016 RID: 22
		[SerializeField]
		private uint m_BaseMarkGlyphID;

		// Token: 0x04000017 RID: 23
		[SerializeField]
		private GlyphAnchorPoint m_BaseMarkGlyphAnchorPoint;

		// Token: 0x04000018 RID: 24
		[SerializeField]
		private uint m_CombiningMarkGlyphID;

		// Token: 0x04000019 RID: 25
		[SerializeField]
		private MarkPositionAdjustment m_CombiningMarkPositionAdjustment;
	}
}
