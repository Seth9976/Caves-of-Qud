using System;

namespace UnityEngine
{
	// Token: 0x0200000D RID: 13
	public struct WheelFrictionCurve
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002878 File Offset: 0x00000A78
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002890 File Offset: 0x00000A90
		public float extremumSlip
		{
			get
			{
				return this.m_ExtremumSlip;
			}
			set
			{
				this.m_ExtremumSlip = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000044 RID: 68 RVA: 0x0000289C File Offset: 0x00000A9C
		// (set) Token: 0x06000045 RID: 69 RVA: 0x000028B4 File Offset: 0x00000AB4
		public float extremumValue
		{
			get
			{
				return this.m_ExtremumValue;
			}
			set
			{
				this.m_ExtremumValue = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000028C0 File Offset: 0x00000AC0
		// (set) Token: 0x06000047 RID: 71 RVA: 0x000028D8 File Offset: 0x00000AD8
		public float asymptoteSlip
		{
			get
			{
				return this.m_AsymptoteSlip;
			}
			set
			{
				this.m_AsymptoteSlip = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000028E4 File Offset: 0x00000AE4
		// (set) Token: 0x06000049 RID: 73 RVA: 0x000028FC File Offset: 0x00000AFC
		public float asymptoteValue
		{
			get
			{
				return this.m_AsymptoteValue;
			}
			set
			{
				this.m_AsymptoteValue = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002908 File Offset: 0x00000B08
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002920 File Offset: 0x00000B20
		public float stiffness
		{
			get
			{
				return this.m_Stiffness;
			}
			set
			{
				this.m_Stiffness = value;
			}
		}

		// Token: 0x04000030 RID: 48
		private float m_ExtremumSlip;

		// Token: 0x04000031 RID: 49
		private float m_ExtremumValue;

		// Token: 0x04000032 RID: 50
		private float m_AsymptoteSlip;

		// Token: 0x04000033 RID: 51
		private float m_AsymptoteValue;

		// Token: 0x04000034 RID: 52
		private float m_Stiffness;
	}
}
