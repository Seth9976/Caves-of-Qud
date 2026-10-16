using System;

namespace UnityEngine
{
	// Token: 0x02000007 RID: 7
	public struct SoftJointLimitSpring
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002658 File Offset: 0x00000858
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002670 File Offset: 0x00000870
		public float spring
		{
			get
			{
				return this.m_Spring;
			}
			set
			{
				this.m_Spring = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000267C File Offset: 0x0000087C
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002694 File Offset: 0x00000894
		public float damper
		{
			get
			{
				return this.m_Damper;
			}
			set
			{
				this.m_Damper = value;
			}
		}

		// Token: 0x04000018 RID: 24
		private float m_Spring;

		// Token: 0x04000019 RID: 25
		private float m_Damper;
	}
}
