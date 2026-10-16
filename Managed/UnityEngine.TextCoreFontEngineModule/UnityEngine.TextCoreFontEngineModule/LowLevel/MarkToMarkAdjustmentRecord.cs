using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000026 RID: 38
	[UsedByNativeCode]
	[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
	[Serializable]
	internal struct MarkToMarkAdjustmentRecord
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00005FE8 File Offset: 0x000041E8
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00006000 File Offset: 0x00004200
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

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000600C File Offset: 0x0000420C
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00006024 File Offset: 0x00004224
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

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00006030 File Offset: 0x00004230
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00006048 File Offset: 0x00004248
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

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00006054 File Offset: 0x00004254
		// (set) Token: 0x06000190 RID: 400 RVA: 0x0000606C File Offset: 0x0000426C
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

		// Token: 0x040000DF RID: 223
		[SerializeField]
		[NativeName("baseMarkGlyphID")]
		private uint m_BaseMarkGlyphID;

		// Token: 0x040000E0 RID: 224
		[SerializeField]
		[NativeName("baseMarkAnchor")]
		private GlyphAnchorPoint m_BaseMarkGlyphAnchorPoint;

		// Token: 0x040000E1 RID: 225
		[SerializeField]
		[NativeName("combiningMarkGlyphID")]
		private uint m_CombiningMarkGlyphID;

		// Token: 0x040000E2 RID: 226
		[SerializeField]
		[NativeName("combiningMarkPositionAdjustment")]
		private MarkPositionAdjustment m_CombiningMarkPositionAdjustment;
	}
}
