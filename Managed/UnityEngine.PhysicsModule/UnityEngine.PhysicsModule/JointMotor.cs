using System;

namespace UnityEngine
{
	// Token: 0x02000009 RID: 9
	public struct JointMotor
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002750 File Offset: 0x00000950
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002768 File Offset: 0x00000968
		public float targetVelocity
		{
			get
			{
				return this.m_TargetVelocity;
			}
			set
			{
				this.m_TargetVelocity = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002774 File Offset: 0x00000974
		// (set) Token: 0x06000035 RID: 53 RVA: 0x0000278C File Offset: 0x0000098C
		public float force
		{
			get
			{
				return this.m_Force;
			}
			set
			{
				this.m_Force = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002798 File Offset: 0x00000998
		// (set) Token: 0x06000037 RID: 55 RVA: 0x000027B3 File Offset: 0x000009B3
		public bool freeSpin
		{
			get
			{
				return this.m_FreeSpin == 1;
			}
			set
			{
				this.m_FreeSpin = (value ? 1 : 0);
			}
		}

		// Token: 0x0400001E RID: 30
		private float m_TargetVelocity;

		// Token: 0x0400001F RID: 31
		private float m_Force;

		// Token: 0x04000020 RID: 32
		private int m_FreeSpin;
	}
}
