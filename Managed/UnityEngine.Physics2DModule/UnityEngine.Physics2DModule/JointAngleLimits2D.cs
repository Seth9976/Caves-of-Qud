using System;

namespace UnityEngine
{
	// Token: 0x0200001C RID: 28
	public struct JointAngleLimits2D
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600026D RID: 621 RVA: 0x00007DC4 File Offset: 0x00005FC4
		// (set) Token: 0x0600026E RID: 622 RVA: 0x00007DDC File Offset: 0x00005FDC
		public float min
		{
			get
			{
				return this.m_LowerAngle;
			}
			set
			{
				this.m_LowerAngle = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00007DE8 File Offset: 0x00005FE8
		// (set) Token: 0x06000270 RID: 624 RVA: 0x00007E00 File Offset: 0x00006000
		public float max
		{
			get
			{
				return this.m_UpperAngle;
			}
			set
			{
				this.m_UpperAngle = value;
			}
		}

		// Token: 0x04000087 RID: 135
		private float m_LowerAngle;

		// Token: 0x04000088 RID: 136
		private float m_UpperAngle;
	}
}
