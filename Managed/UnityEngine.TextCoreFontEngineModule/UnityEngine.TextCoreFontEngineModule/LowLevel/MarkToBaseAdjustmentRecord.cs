using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000025 RID: 37
	[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
	[UsedByNativeCode]
	[Serializable]
	internal struct MarkToBaseAdjustmentRecord
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00005F58 File Offset: 0x00004158
		// (set) Token: 0x06000182 RID: 386 RVA: 0x00005F70 File Offset: 0x00004170
		public uint baseGlyphID
		{
			get
			{
				return this.m_BaseGlyphID;
			}
			set
			{
				this.m_BaseGlyphID = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00005F7C File Offset: 0x0000417C
		// (set) Token: 0x06000184 RID: 388 RVA: 0x00005F94 File Offset: 0x00004194
		public GlyphAnchorPoint baseGlyphAnchorPoint
		{
			get
			{
				return this.m_BaseGlyphAnchorPoint;
			}
			set
			{
				this.m_BaseGlyphAnchorPoint = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00005FA0 File Offset: 0x000041A0
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00005FB8 File Offset: 0x000041B8
		public uint markGlyphID
		{
			get
			{
				return this.m_MarkGlyphID;
			}
			set
			{
				this.m_MarkGlyphID = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00005FC4 File Offset: 0x000041C4
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00005FDC File Offset: 0x000041DC
		public MarkPositionAdjustment markPositionAdjustment
		{
			get
			{
				return this.m_MarkPositionAdjustment;
			}
			set
			{
				this.m_MarkPositionAdjustment = value;
			}
		}

		// Token: 0x040000DB RID: 219
		[NativeName("baseGlyphID")]
		[SerializeField]
		private uint m_BaseGlyphID;

		// Token: 0x040000DC RID: 220
		[SerializeField]
		[NativeName("baseAnchor")]
		private GlyphAnchorPoint m_BaseGlyphAnchorPoint;

		// Token: 0x040000DD RID: 221
		[SerializeField]
		[NativeName("markGlyphID")]
		private uint m_MarkGlyphID;

		// Token: 0x040000DE RID: 222
		[SerializeField]
		[NativeName("markPositionAdjustment")]
		private MarkPositionAdjustment m_MarkPositionAdjustment;
	}
}
