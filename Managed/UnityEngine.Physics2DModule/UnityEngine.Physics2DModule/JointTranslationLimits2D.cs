using System;

namespace UnityEngine
{
	// Token: 0x0200001D RID: 29
	public struct JointTranslationLimits2D
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00007E0C File Offset: 0x0000600C
		// (set) Token: 0x06000272 RID: 626 RVA: 0x00007E24 File Offset: 0x00006024
		public float min
		{
			get
			{
				return this.m_LowerTranslation;
			}
			set
			{
				this.m_LowerTranslation = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00007E30 File Offset: 0x00006030
		// (set) Token: 0x06000274 RID: 628 RVA: 0x00007E48 File Offset: 0x00006048
		public float max
		{
			get
			{
				return this.m_UpperTranslation;
			}
			set
			{
				this.m_UpperTranslation = value;
			}
		}

		// Token: 0x04000089 RID: 137
		private float m_LowerTranslation;

		// Token: 0x0400008A RID: 138
		private float m_UpperTranslation;
	}
}
