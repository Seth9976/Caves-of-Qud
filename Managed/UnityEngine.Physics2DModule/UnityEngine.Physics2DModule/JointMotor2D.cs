using System;

namespace UnityEngine
{
	// Token: 0x0200001E RID: 30
	public struct JointMotor2D
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000275 RID: 629 RVA: 0x00007E54 File Offset: 0x00006054
		// (set) Token: 0x06000276 RID: 630 RVA: 0x00007E6C File Offset: 0x0000606C
		public float motorSpeed
		{
			get
			{
				return this.m_MotorSpeed;
			}
			set
			{
				this.m_MotorSpeed = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000277 RID: 631 RVA: 0x00007E78 File Offset: 0x00006078
		// (set) Token: 0x06000278 RID: 632 RVA: 0x00007E90 File Offset: 0x00006090
		public float maxMotorTorque
		{
			get
			{
				return this.m_MaximumMotorTorque;
			}
			set
			{
				this.m_MaximumMotorTorque = value;
			}
		}

		// Token: 0x0400008B RID: 139
		private float m_MotorSpeed;

		// Token: 0x0400008C RID: 140
		private float m_MaximumMotorTorque;
	}
}
