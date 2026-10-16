using System;

namespace UnityEngine
{
	// Token: 0x0200001F RID: 31
	public struct JointSuspension2D
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000279 RID: 633 RVA: 0x00007E9C File Offset: 0x0000609C
		// (set) Token: 0x0600027A RID: 634 RVA: 0x00007EB4 File Offset: 0x000060B4
		public float dampingRatio
		{
			get
			{
				return this.m_DampingRatio;
			}
			set
			{
				this.m_DampingRatio = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600027B RID: 635 RVA: 0x00007EC0 File Offset: 0x000060C0
		// (set) Token: 0x0600027C RID: 636 RVA: 0x00007ED8 File Offset: 0x000060D8
		public float frequency
		{
			get
			{
				return this.m_Frequency;
			}
			set
			{
				this.m_Frequency = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600027D RID: 637 RVA: 0x00007EE4 File Offset: 0x000060E4
		// (set) Token: 0x0600027E RID: 638 RVA: 0x00007EFC File Offset: 0x000060FC
		public float angle
		{
			get
			{
				return this.m_Angle;
			}
			set
			{
				this.m_Angle = value;
			}
		}

		// Token: 0x0400008D RID: 141
		private float m_DampingRatio;

		// Token: 0x0400008E RID: 142
		private float m_Frequency;

		// Token: 0x0400008F RID: 143
		private float m_Angle;
	}
}
